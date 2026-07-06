using System.Net;
using System.Net.Sockets;

namespace MOCS.DebugTool;

/// <summary>
/// UDP 报文发送器，向 MOCS 的监听端口发送模拟报文
/// </summary>
public class UdpSender : IDisposable
{
    private readonly UdpClient _client;

    /// <summary>
    /// MOCS 的 VCU 监听端点 (192.168.43.1:6001)
    /// </summary>
    public IPEndPoint VCUEndPoint { get; }

    /// <summary>
    /// MOCS 的 MCU 监听端点 (192.168.43.2:6002)
    /// </summary>
    public IPEndPoint MCUEndPoint { get; }

    public UdpSender(
        string vcuIp = "127.0.0.1",
        int vcuPort = 6001,
        string mcuIp = "127.0.0.1",
        int mcuPort = 6002)
    {
        _client = new UdpClient();
        VCUEndPoint = new IPEndPoint(IPAddress.Parse(vcuIp), vcuPort);
        MCUEndPoint = new IPEndPoint(IPAddress.Parse(mcuIp), mcuPort);
    }

    /// <summary>
    /// 向 VCU 端口 (6001) 发送报文
    /// </summary>
    public async Task SendToVCUAsync(byte[] data)
    {
        await _client.SendAsync(data, data.Length, VCUEndPoint);
    }

    /// <summary>
    /// 向 MCU 端口 (6002) 发送报文
    /// </summary>
    public async Task SendToMCUAsync(byte[] data)
    {
        await _client.SendAsync(data, data.Length, MCUEndPoint);
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
