using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Accessibility;
using MOCS.Protocals;
using MOCS.Utils;
using NLog;

namespace MOCS.Coms
{
    public class UdpMessageService<TBaseMsg> : IDisposable, IAsyncDisposable
        where TBaseMsg : class
    {
        private readonly UdpClient _sender;
        private readonly UdpClient _receiver;

        // 根据接收报文的类型注册的回调集合
        private readonly MessageDispatcher _dispatcher;

        // 报文实例工厂类
        private readonly IMessageFactory<TBaseMsg> _messageFactory;

        private CancellationTokenSource? _cts;
        private Task? _receivingTask;
        private readonly object _sync = new();

        // 日志记录器
        private readonly ILogger _recvLogger;
        private readonly ILogger _sendLogger;

        public IPEndPoint RemoteEndPoint { get; }

        /// <summary>
        /// 节点名称（用于 MessageMonitor 按子系统区分日志）
        /// </summary>
        public string NodeName { get; }

        /// <summary>
        /// 发送成功事件：参数为（原始帧字节数组, 报文类型名）
        /// </summary>
        public event Action<byte[], string>? OnRawFrameSent;

        /// <summary>
        /// 接收到原始帧事件：参数为（原始帧字节数组, 报文类型名或 null）
        /// 无论解析成功/失败都会触发，供 UI 层按方向记录日志。
        /// </summary>
        public event Action<byte[], string?>? OnRawFrameReceived;

        public UdpMessageService(
            string nodeName,
            IPAddress localIp,
            int localPort,
            IPAddress remoteIp,
            int remotePort,
            ILogger recvLogger,
            ILogger sendLogger,
            IMessageFactory<TBaseMsg>? messageFactory = null
        )
        {
            NodeName = nodeName;
            _sender = new UdpClient();
            _receiver = new UdpClient(new IPEndPoint(localIp, localPort));
            RemoteEndPoint = new IPEndPoint(remoteIp, remotePort);
            _dispatcher = new MessageDispatcher();
            _recvLogger = recvLogger;
            _sendLogger = sendLogger;
            _messageFactory = messageFactory ?? new MessageFactory<TBaseMsg>();
        }


        public void RegisterParser(
            byte msgId,
            Func<ReadOnlyMemory<byte>, (TBaseMsg? message, string? error)> parser
        ) => _messageFactory.RegisterParser(msgId, parser);

        public void RegisterRangeParser(
            byte low,
            byte high,
            Func<ReadOnlyMemory<byte>, (TBaseMsg? message, string? error)> parser
        ) => _messageFactory.RegisterRangeParser(low, high, parser);

        public void Subscribe<T>(Action<T> handler)
            where T : TBaseMsg
        {
            _dispatcher.Subscribe(handler);
        }

        public void UnSubscribe<T>()
            where T : TBaseMsg
        {
            _dispatcher.UnSubscribe<T>();
        }

        public async Task SendAsync<T>(T msg)
            where T : TBaseMsg, IOutgoingMsg
        {
            ArgumentNullException.ThrowIfNull(msg);
            try
            {
                var payLoad = msg.ToByteArray();
                var bytes = _messageFactory.ToTransmitByteArray(payLoad);
                await _sender.SendAsync(bytes, bytes.Length, RemoteEndPoint);

                var msgType = typeof(T).Name;
                OnRawFrameSent?.Invoke(bytes, msgType);

                _sendLogger.Debug(
                    $"发送成功 - 目标: {RemoteEndPoint.ToString()}, 类型: {msgType}, 长度: {bytes.Length}字节, 数据: {BitConverter.ToString(bytes)}"
                );
            }
            catch (Exception ex)
            {
                _sendLogger.Error(ex, $"发送失败 - 类型: {typeof(T).Name}");
            }
        }


        public void StartListening()
        {
            lock (_sync)
            {
                if (_receivingTask?.IsCompleted == false)
                {
                    // 已监听
                    return;
                }
                _cts = new CancellationTokenSource();
                _receivingTask = Task.Run(() => ReceiveLoop(_cts.Token), _cts.Token);
            }
        }

        public async Task StopListeningAsync()
        {
            Task? runningTask = null;
            CancellationTokenSource? ctsToDispose = null;
            lock (_sync)
            {
                if (_cts == null)
                {
                    return;
                }
                ctsToDispose = _cts;
                runningTask = _receivingTask;
                _cts = null;
                _receivingTask = null;
            }

            ctsToDispose.Cancel();
            try
            {
                if (runningTask != null)
                {
                    await runningTask.ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
                // 正常取消
            }
            finally
            {
                ctsToDispose.Dispose();
            }
        }

        private async Task ReceiveLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var result = await _receiver.ReceiveAsync(token).ConfigureAwait(false);
                    var buffer = result.Buffer;
                    string? msgType = null;

                    if (_messageFactory.TryParseMessage(buffer, out var msg, out var err))
                    {
                        msgType = msg.GetType().Name;
                        await _dispatcher.Dispatch(msg).ConfigureAwait(false);
                    }
                    else
                    {
                        // 解析失败时尝试提取 msgId，便于 UI 层按语义方向显示日志
                        msgType = TryExtractMsgType(buffer);
                        _recvLogger.Warn(
                            $"报文解析失败 - 来源: {result.RemoteEndPoint}, 错误信息: {err}, 原始报文: {BitConverter.ToString(buffer)} "
                        );
                    }

                    // 无论解析成功/失败都触发原始帧接收事件，供 UI 记录日志
                    OnRawFrameReceived?.Invoke(buffer.ToArray(), msgType);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _recvLogger.Error(ex, "UDP接收报文时出现异常");
                }
            }
        }

        private static string? TryExtractMsgType(ReadOnlyMemory<byte> buffer)
        {
            // 协议帧结构: [head 1B][len 2B][seq 2B][msgId 1B][userdata...][crc 2B][tail 1B]
            // 索引 8 处为 msgId（0-based）
            if (buffer.Length < 9)
                return null;
            var msgId = buffer.Span[8];
            return $"MsgId=0x{msgId:X2}";
        }


        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _sender.Dispose();
            _receiver.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await StopListeningAsync().ConfigureAwait(false);
            _sender.Dispose();
            _receiver.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
