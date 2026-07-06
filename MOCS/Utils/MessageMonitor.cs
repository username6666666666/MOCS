using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MOCS.Utils
{
    /// <summary>
    /// 全局报文监视器（被动接收模式 + 缓存回填）
    /// 各 UI 模块将自身的 RTB 通过 BindRecvRTB / BindSendRTB 注册到对应节点名，
    /// 然后由 VCUInterface / MCUInterface 通过 RecordNodeFrame(sender, receiver, frame, msgType)
    /// 上报收发事件，按"发送方→发送RTB"、"接收方→接收RTB"路由。
    ///
    /// 缓存机制：所有报文无条件写入环形缓冲区，RTB 绑定时自动回填历史报文。
    /// </summary>
    public sealed class MessageMonitor : IDisposable
    {
        private static readonly Lazy<MessageMonitor> _instance =
            new Lazy<MessageMonitor>(() => new MessageMonitor());

        public static MessageMonitor Instance => _instance.Value;

        /// <summary>每个节点方向缓存的报文上限</summary>
        private const int MaxCachedFrames = 500;

        // 节点统计信息
        private readonly ConcurrentDictionary<string, NodeStats> _nodeStats = new();

        // 节点 -> 报文日志 RichTextBox 映射（旧接口 BindLogRTB，保留向后兼容）
        private readonly ConcurrentDictionary<string, RichTextBox> _logRTBs = new();

        // 节点 -> 发送报文日志 RichTextBox 映射（新接口 BindSendLogRTB）
        private readonly ConcurrentDictionary<string, RichTextBox> _sendLogRTBs = new();

        // 节点 -> 接收报文日志 RichTextBox 映射（新接口 BindRecvRTB）
        private readonly ConcurrentDictionary<string, RichTextBox> _recvLogRTBs = new();

        // ========== 报文缓存（环形缓冲区） ==========
        // 节点 -> 发送方向缓存帧列表
        private readonly ConcurrentDictionary<string, List<CachedFrame>> _sendCache = new();
        // 节点 -> 接收方向缓存帧列表
        private readonly ConcurrentDictionary<string, List<CachedFrame>> _recvCache = new();
        private readonly object _cacheLock = new();

        // 图表控件（可选）
        private Chart? _trafficChart;
        private Chart? _msgTypeChart;

        // 统计锁
        private readonly object _statsLock = new();

        // 定时刷新
        private System.Threading.Timer? _refreshTimer;

        private MessageMonitor() { }

        /// <summary>
        /// 缓存的报文帧（用于 RTB 绑定时回填）
        /// </summary>
        private readonly struct CachedFrame
        {
            public readonly byte[] RawBytes;
            public readonly string? MsgType;
            public readonly string SourceNode;

            public CachedFrame(byte[] rawBytes, string? msgType, string sourceNode)
            {
                RawBytes = rawBytes;
                MsgType = msgType;
                SourceNode = sourceNode;
            }
        }

        #region 节点注册

        /// <summary>
        /// 注册一个节点（供各 UI 模块在创建 UdpMessageService 后调用）
        /// </summary>
        public void RegisterNode(string nodeName)
        {
            _nodeStats.TryAdd(nodeName, new NodeStats(nodeName));
        }

        /// <summary>
        /// 移除一个节点
        /// </summary>
        public void UnregisterNode(string nodeName)
        {
            _nodeStats.TryRemove(nodeName, out _);
        }

        #endregion

        #region 报文日志绑定

        /// <summary>
        /// 为指定节点绑定"接收"报文日志 RichTextBox，并回填历史报文
        /// </summary>
        public void BindRecvRTB(string nodeName, RichTextBox rtb)
        {
            _recvLogRTBs[nodeName] = rtb;
            // 兼容老接口：未指定发送RTB时，复用同一RTB作为发送RTB
            _logRTBs[nodeName] = rtb;
            // 回填缓存中的历史报文
            ReplayCache(_recvCache, nodeName, rtb, isSend: false);
        }

        /// <summary>
        /// 解除指定节点的"接收"报文日志绑定
        /// </summary>
        public void UnbindRecvRTB(string nodeName)
        {
            _recvLogRTBs.TryRemove(nodeName, out _);
            _logRTBs.TryRemove(nodeName, out _);
        }

        /// <summary>
        /// 为指定节点绑定"发送"报文日志 RichTextBox，并回填历史报文
        /// </summary>
        public void BindSendRTB(string nodeName, RichTextBox rtb)
        {
            _sendLogRTBs[nodeName] = rtb;
            // 回填缓存中的历史报文
            ReplayCache(_sendCache, nodeName, rtb, isSend: true);
        }

        /// <summary>
        /// 解除指定节点的"发送"报文日志绑定
        /// </summary>
        public void UnbindSendRTB(string nodeName)
        {
            _sendLogRTBs.TryRemove(nodeName, out _);
        }

        /// <summary>
        /// 旧接口：仅绑定单一 RTB 兼容（接收与发送共用）
        /// 实际写入时若未独立绑定发送RTB，则发送方向也会写到这个RTB
        /// </summary>
        public void BindLogRTB(string nodeName, RichTextBox rtb)
        {
            BindRecvRTB(nodeName, rtb);
        }

        /// <summary>
        /// 旧接口：绑定"发送"RTB（兼容旧代码 BindSendLogRTB）
        /// </summary>
        public void BindSendLogRTB(string nodeName, RichTextBox rtb)
        {
            BindSendRTB(nodeName, rtb);
        }

        /// <summary>
        /// 旧接口：解除 RTB 绑定
        /// </summary>
        public void UnbindLogRTB(string nodeName)
        {
            UnbindRecvRTB(nodeName);
        }

        /// <summary>
        /// 旧接口：解除发送 RTB 绑定
        /// </summary>
        public void UnbindSendLogRTB(string nodeName)
        {
            UnbindSendRTB(nodeName);
        }

        /// <summary>
        /// 旧接口：绑定全局接收报文 RTB。映射到 MOCS 节点的"接收"RTB。
        /// </summary>
        public void BindGlobalRecvRTB(RichTextBox rtb)
        {
            BindRecvRTB("MOCS", rtb);
        }

        /// <summary>
        /// 旧接口：绑定全局发送报文 RTB。映射到 MOCS 节点的"发送"RTB。
        /// </summary>
        public void BindGlobalSendRTB(RichTextBox rtb)
        {
            BindSendRTB("MOCS", rtb);
        }

        /// <summary>
        /// 旧接口：解绑全局 RTB
        /// </summary>
        public void UnbindGlobalRTB()
        {
            UnbindRecvRTB("MOCS");
            UnbindSendRTB("MOCS");
        }

        #endregion

        #region 图表绑定

        /// <summary>
        /// 绑定流量图表（可选）
        /// </summary>
        public void BindTrafficChart(Chart chart)
        {
            _trafficChart = chart;
            InitTrafficChart();
        }

        /// <summary>
        /// 绑定消息类型分布图表（可选）
        /// </summary>
        public void BindMsgTypeChart(Chart chart)
        {
            _msgTypeChart = chart;
            InitMsgTypeChart();
        }

        #endregion

        #region 记录收发

        /// <summary>
        /// 记录一次发送统计
        /// </summary>
        public void RecordSent(string nodeName, string msgType, int byteCount)
        {
            _nodeStats.AddOrUpdate(
                nodeName,
                _ => CreateAndRecord(nodeName, msgType, byteCount, true),
                (_, stats) =>
                {
                    stats.Record(msgType, byteCount, true);
                    return stats;
                });
        }

        /// <summary>
        /// 记录一次接收统计
        /// </summary>
        public void RecordReceived(string nodeName, string msgType, int byteCount)
        {
            _nodeStats.AddOrUpdate(
                nodeName,
                _ => CreateAndRecord(nodeName, msgType, byteCount, false),
                (_, stats) =>
                {
                    stats.Record(msgType, byteCount, false);
                    return stats;
                });
        }

        /// <summary>
        /// 核心入口：按"发送方/接收方"路由一条原始报文到各节点 RTB。
        /// 报文无条件写入缓存；若 RTB 已绑定则实时写入，否则等绑定时回填。
        /// </summary>
        /// <param name="senderNode">报文发送方节点名（如 "MOCS"、"MCU"、"VCU"、"LCU1"、"VSPS" 等）</param>
        /// <param name="receiverNode">报文接收方节点名</param>
        /// <param name="rawBytes">原始帧字节</param>
        /// <param name="msgType">报文类型（可选）</param>
        public void RecordNodeFrame(
            string senderNode,
            string receiverNode,
            byte[] rawBytes,
            string? msgType = null)
        {
            if (string.IsNullOrEmpty(senderNode) || string.IsNullOrEmpty(receiverNode))
                return;

            // ---------- 无条件写入缓存 ----------
            CacheFrame(_sendCache, senderNode, rawBytes, msgType, senderNode);
            CacheFrame(_recvCache, receiverNode, rawBytes, msgType, senderNode);

            // ---------- 写入发送方的"发送"RTB（如已绑定） ----------
            if (_sendLogRTBs.TryGetValue(senderNode, out var senderRtb) && senderRtb != null)
            {
                MessageLogger.AppendSend(senderRtb, senderNode, rawBytes, msgType);
            }
            else if (_logRTBs.TryGetValue(senderNode, out var senderFallback) && senderFallback != null)
            {
                MessageLogger.AppendSend(senderFallback, senderNode, rawBytes, msgType);
            }

            // ---------- 写入接收方的"接收"RTB（如已绑定） ----------
            if (_recvLogRTBs.TryGetValue(receiverNode, out var recvRtb) && recvRtb != null)
            {
                MessageLogger.AppendRecv(recvRtb, receiverNode, rawBytes, msgType);
            }
            else if (_logRTBs.TryGetValue(receiverNode, out var recvFallback) && recvFallback != null)
            {
                MessageLogger.AppendRecv(recvFallback, receiverNode, rawBytes, msgType);
            }

            // 统计：发送方视为"sent"、接收方视为"received"
            if (!string.Equals(senderNode, receiverNode, StringComparison.Ordinal))
            {
                var msgId = rawBytes.Length >= 9 ? rawBytes[8] : (byte)0;
                var typeKey = msgType ?? $"MsgId=0x{msgId:X2}";
                RecordSent(senderNode, typeKey, rawBytes.Length);
                RecordReceived(receiverNode, typeKey, rawBytes.Length);
            }
        }

        /// <summary>
        /// 仅写入接收方的"接收"RTB + 缓存，不写发送方的"发送"RTB。
        /// 用于一对多广播场景（如 MOCS→LCU/GCU），避免发送方 RTB 重复写入。
        /// </summary>
        /// <param name="senderNode">报文发送方节点名（用于缓存 sourceNode）</param>
        /// <param name="receiverNode">报文接收方节点名</param>
        /// <param name="rawBytes">原始帧字节</param>
        /// <param name="msgType">报文类型（可选）</param>
        public void RecordRecvOnly(
            string senderNode,
            string receiverNode,
            byte[] rawBytes,
            string? msgType = null)
        {
            if (string.IsNullOrEmpty(senderNode) || string.IsNullOrEmpty(receiverNode))
                return;

            // 写入接收缓存
            CacheFrame(_recvCache, receiverNode, rawBytes, msgType, senderNode);

            // 写入接收方的"接收"RTB（如已绑定）
            if (_recvLogRTBs.TryGetValue(receiverNode, out var recvRtb) && recvRtb != null)
            {
                MessageLogger.AppendRecv(recvRtb, receiverNode, rawBytes, msgType);
            }
            else if (_logRTBs.TryGetValue(receiverNode, out var recvFallback) && recvFallback != null)
            {
                MessageLogger.AppendRecv(recvFallback, receiverNode, rawBytes, msgType);
            }

            // 统计接收方
            if (!string.Equals(senderNode, receiverNode, StringComparison.Ordinal))
            {
                var msgId = rawBytes.Length >= 9 ? rawBytes[8] : (byte)0;
                var typeKey = msgType ?? $"MsgId=0x{msgId:X2}";
                RecordReceived(receiverNode, typeKey, rawBytes.Length);
            }
        }

        /// <summary>
        /// 将报文帧写入指定方向的环形缓冲区
        /// </summary>
        private void CacheFrame(
            ConcurrentDictionary<string, List<CachedFrame>> cache,
            string nodeName,
            byte[] rawBytes,
            string? msgType,
            string sourceNode)
        {
            if (string.IsNullOrEmpty(nodeName) || rawBytes == null)
                return;

            var frame = new CachedFrame(rawBytes, msgType, sourceNode);
            lock (_cacheLock)
            {
                var list = cache.GetOrAdd(nodeName, _ => new List<CachedFrame>(MaxCachedFrames));
                if (list.Count >= MaxCachedFrames)
                {
                    // 移除最旧的一半
                    list.RemoveRange(0, MaxCachedFrames / 2);
                }
                list.Add(frame);
            }
        }

        /// <summary>
        /// 从缓存中回填指定节点指定方向的报文到 RTB
        /// </summary>
        private void ReplayCache(
            ConcurrentDictionary<string, List<CachedFrame>> cache,
            string nodeName,
            RichTextBox rtb,
            bool isSend)
        {
            List<CachedFrame>? frames;
            lock (_cacheLock)
            {
                if (!cache.TryGetValue(nodeName, out frames) || frames.Count == 0)
                    return;
                // 复制一份避免锁内长时间操作
                frames = new List<CachedFrame>(frames);
            }

            foreach (var frame in frames)
            {
                if (isSend)
                    MessageLogger.AppendSend(rtb, frame.SourceNode, frame.RawBytes, frame.MsgType);
                else
                    MessageLogger.AppendRecv(rtb, frame.SourceNode, frame.RawBytes, frame.MsgType);
            }
        }

        /// <summary>
        /// 向后兼容：根据 msgId 自行判定发送/接收方向。
        /// 优先使用 OnRawFrameSent/OnRawFrameReceived 的节点绑定。
        /// </summary>
        public void RecordRawLog(string nodeName, byte[] rawBytes, bool isSent, string? msgType = null)
        {
            // 旧实现：节点级 RTB 写入（保留兼容）
            if (!isSent && _recvLogRTBs.TryGetValue(nodeName, out var recvRtb) && recvRtb != null)
            {
                MessageLogger.AppendRecv(recvRtb, nodeName, rawBytes, msgType);
            }
            else if (!isSent && _logRTBs.TryGetValue(nodeName, out var recvFallback) && recvFallback != null)
            {
                MessageLogger.AppendRecv(recvFallback, nodeName, rawBytes, msgType);
            }

            if (isSent && _sendLogRTBs.TryGetValue(nodeName, out var sendRtb) && sendRtb != null)
            {
                MessageLogger.AppendSend(sendRtb, nodeName, rawBytes, msgType);
            }
            else if (isSent && _logRTBs.TryGetValue(nodeName, out var sendFallback) && sendFallback != null)
            {
                MessageLogger.AppendSend(sendFallback, nodeName, rawBytes, msgType);
            }

            // 统计
            var msgId = rawBytes.Length >= 9 ? rawBytes[8] : (byte)0;
            var typeKey = msgType ?? $"MsgId=0x{msgId:X2}";
            if (isSent)
                RecordSent(nodeName, typeKey, rawBytes.Length);
            else
                RecordReceived(nodeName, typeKey, rawBytes.Length);
        }

        private static NodeStats CreateAndRecord(string name, string msgType, int byteCount, bool isSent)
        {
            var stats = new NodeStats(name);
            stats.Record(msgType, byteCount, isSent);
            return stats;
        }

        #endregion

        #region 图表初始化

        private void InitTrafficChart()
        {
            if (_trafficChart == null) return;

            _trafficChart.Series.Clear();
            _trafficChart.ChartAreas.Clear();
            _trafficChart.Legends.Clear();

            var area = new ChartArea("TrafficArea")
            {
                BackColor = Color.FromArgb(30, 30, 30)
            };
            area.AxisX.LabelStyle.ForeColor = Color.LightGray;
            area.AxisY.LabelStyle.ForeColor = Color.LightGray;
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(60, 60, 60);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(60, 60, 60);
            _trafficChart.ChartAreas.Add(area);

            var legend = new Legend
            {
                BackColor = Color.Transparent,
                ForeColor = Color.LightGray
            };
            _trafficChart.Legends.Add(legend);
        }

        private void InitMsgTypeChart()
        {
            if (_msgTypeChart == null) return;

            _msgTypeChart.Series.Clear();
            _msgTypeChart.ChartAreas.Clear();
            _msgTypeChart.Legends.Clear();

            var area = new ChartArea("MsgTypeArea")
            {
                BackColor = Color.FromArgb(30, 30, 30)
            };
            area.AxisX.LabelStyle.ForeColor = Color.LightGray;
            area.AxisY.LabelStyle.ForeColor = Color.LightGray;
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(60, 60, 60);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(60, 60, 60);
            _msgTypeChart.ChartAreas.Add(area);

            var legend = new Legend
            {
                BackColor = Color.Transparent,
                ForeColor = Color.LightGray
            };
            _msgTypeChart.Legends.Add(legend);
        }

        #endregion

        #region 刷新控制

        /// <summary>
        /// 开始定时刷新（每 500ms 刷新图表）
        /// </summary>
        public void StartRefresh()
        {
            _refreshTimer?.Dispose();
            _refreshTimer = new System.Threading.Timer(
                _ => RefreshCharts(),
                null,
                500,
                500);
        }

        /// <summary>
        /// 停止定时刷新
        /// </summary>
        public void StopRefresh()
        {
            _refreshTimer?.Dispose();
            _refreshTimer = null;
        }

        #endregion

        #region 图表刷新

        private void RefreshCharts()
        {
            if (_trafficChart == null && _msgTypeChart == null) return;

            var allStats = _nodeStats.Values.ToList();

            if (_trafficChart != null)
                RefreshTrafficChart(allStats);

            if (_msgTypeChart != null)
                RefreshMsgTypeChart(allStats);
        }

        private void RefreshTrafficChart(List<NodeStats> allStats)
        {
            if (_trafficChart == null) return;

            if (_trafficChart.InvokeRequired)
            {
                _trafficChart.BeginInvoke(new Action(() => RefreshTrafficChart(allStats)));
                return;
            }

            _trafficChart.Series.Clear();

            // 每个节点创建两个 Series：发送和接收
            int idx = 0;
            var colors = new[] { Color.Cyan, Color.LightGreen, Color.Orange, Color.Magenta,
                                 Color.Yellow, Color.Lime, Color.DeepPink, Color.Aqua };

            foreach (var stats in allStats)
            {
                var colorIdx = idx % (colors.Length / 2) * 2;

                var sentSeries = new Series($"{stats.NodeName}_发送")
                {
                    ChartType = SeriesChartType.Column,
                    Color = colors[colorIdx]
                };
                sentSeries.Points.AddXY(stats.NodeName, stats.SentBytes);
                _trafficChart.Series.Add(sentSeries);

                var recvSeries = new Series($"{stats.NodeName}_接收")
                {
                    ChartType = SeriesChartType.Column,
                    Color = colors[colorIdx + 1]
                };
                recvSeries.Points.AddXY(stats.NodeName, stats.RecvBytes);
                _trafficChart.Series.Add(recvSeries);

                idx++;
            }
        }

        private void RefreshMsgTypeChart(List<NodeStats> allStats)
        {
            if (_msgTypeChart == null) return;

            if (_msgTypeChart.InvokeRequired)
            {
                _msgTypeChart.BeginInvoke(new Action(() => RefreshMsgTypeChart(allStats)));
                return;
            }

            _msgTypeChart.Series.Clear();

            // 汇总所有节点的消息类型统计
            var merged = new Dictionary<string, (int sendCount, int recvCount)>();
            foreach (var stats in allStats)
            {
                foreach (var kvp in stats.MsgTypeStats)
                {
                    if (merged.TryGetValue(kvp.Key, out var existing))
                    {
                        merged[kvp.Key] = (
                            existing.sendCount + kvp.Value.sendCount,
                            existing.recvCount + kvp.Value.recvCount);
                    }
                    else
                    {
                        merged[kvp.Key] = kvp.Value;
                    }
                }
            }

            var sendSeries = new Series("发送次数")
            {
                ChartType = SeriesChartType.Bar,
                Color = Color.LightGreen
            };
            var recvSeries = new Series("接收次数")
            {
                ChartType = SeriesChartType.Bar,
                Color = Color.Cyan
            };

            foreach (var kvp in merged.OrderByDescending(x => x.Value.sendCount + x.Value.recvCount).Take(15))
            {
                sendSeries.Points.AddXY(kvp.Key, kvp.Value.sendCount);
                recvSeries.Points.AddXY(kvp.Key, kvp.Value.recvCount);
            }

            _msgTypeChart.Series.Add(sendSeries);
            _msgTypeChart.Series.Add(recvSeries);
        }

        #endregion

        #region 获取统计摘要

        /// <summary>
        /// 获取所有节点的统计摘要文本
        /// </summary>
        public string GetSummary()
        {
            var allStats = _nodeStats.Values.OrderBy(s => s.NodeName).ToList();
            if (allStats.Count == 0) return "暂无节点注册";

            var lines = new List<string>
            {
                $"===== 报文统计摘要 ({DateTime.Now:HH:mm:ss}) ====="
            };

            long totalSent = 0, totalRecv = 0;
            int totalSentCount = 0, totalRecvCount = 0;

            foreach (var stats in allStats)
            {
                lines.Add($"  [{stats.NodeName}]");
                lines.Add($"    发送: {stats.SentCount} 条, {FormatBytes(stats.SentBytes)}");
                lines.Add($"    接收: {stats.RecvCount} 条, {FormatBytes(stats.RecvBytes)}");

                totalSent += stats.SentBytes;
                totalRecv += stats.RecvBytes;
                totalSentCount += stats.SentCount;
                totalRecvCount += stats.RecvCount;
            }

            lines.Add("  -----------------------------");
            lines.Add($"  总计发送: {totalSentCount} 条, {FormatBytes(totalSent)}");
            lines.Add($"  总计接收: {totalRecvCount} 条, {FormatBytes(totalRecv)}");

            return string.Join(Environment.NewLine, lines);
        }

        /// <summary>
        /// 获取指定节点的统计摘要
        /// </summary>
        public string GetNodeSummary(string nodeName)
        {
            if (!_nodeStats.TryGetValue(nodeName, out var stats))
                return $"节点 [{nodeName}] 未注册";

            return
                $"[{nodeName}] 发送: {stats.SentCount}条/{FormatBytes(stats.SentBytes)} | " +
                $"接收: {stats.RecvCount}条/{FormatBytes(stats.RecvBytes)}";
        }

        private static string FormatBytes(long bytes)
        {
            if (bytes < 1024) return $"{bytes} B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024.0:F1} KB";
            return $"{bytes / (1024.0 * 1024.0):F1} MB";
        }

        #endregion

        public void Dispose()
        {
            StopRefresh();
            _nodeStats.Clear();
            lock (_cacheLock)
            {
                _sendCache.Clear();
                _recvCache.Clear();
            }
        }
    }

    /// <summary>
    /// 单个节点的统计信息
    /// </summary>
    internal class NodeStats
    {
        public string NodeName { get; }
        public long SentBytes;
        public long RecvBytes;
        public int SentCount;
        public int RecvCount;

        // 消息类型 -> (发送次数, 接收次数)
        public readonly ConcurrentDictionary<string, (int sendCount, int recvCount)> MsgTypeStats = new();

        public NodeStats(string nodeName)
        {
            NodeName = nodeName;
        }

        public void Record(string msgType, int byteCount, bool isSent)
        {
            if (isSent)
            {
                Interlocked.Add(ref SentBytes, byteCount);
                Interlocked.Increment(ref SentCount);
            }
            else
            {
                Interlocked.Add(ref RecvBytes, byteCount);
                Interlocked.Increment(ref RecvCount);
            }

            MsgTypeStats.AddOrUpdate(
                msgType,
                _ => isSent ? (1, 0) : (0, 1),
                (_, existing) => isSent
                    ? (existing.sendCount + 1, existing.recvCount)
                    : (existing.sendCount, existing.recvCount + 1));
        }
    }
}
