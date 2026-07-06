using MOCS.Cores.MCU;

namespace MOCS.DebugTool;

public partial class MainForm : Form
{
    private readonly UdpSender _sender = new();
    private bool _disposed;

    public MainForm()
    {
        InitializeComponent();
        cmbMsgType.SelectedIndex = 0;
    }

    private void cmbMsgType_SelectedIndexChanged(object? sender, EventArgs e)
    {
        // 更新 UserData 输入框的提示和端口
        // 所有 MCU 报文默认走 6002，VCU 报文走 6001
        var info = GetMsgTypeInfo(cmbMsgType.SelectedIndex);
        cmbTarget.SelectedIndex = info.IsMCUPort ? 0 : 1;
        gbUserData.Text = info.UserDataHint;
    }

    private async void btnSend_Click(object? sender, EventArgs e)
    {
        try
        {
            var hex = txtUserData.Text.Trim();
            var info = GetMsgTypeInfo(cmbMsgType.SelectedIndex);

            byte[] frame;
            if (!string.IsNullOrEmpty(hex))
            {
                var userData = ParseHex(hex);
                frame = BuildFrameByIndex(cmbMsgType.SelectedIndex, userData);
            }
            else
            {
                // 空输入 → 直接返回，不发送任何报文
                return;
            }

            await SendFrameAsync(frame);
            LogSend(frame);
        }
        catch (Exception ex)
        {
            LogError($"发送失败: {ex.Message}");
        }
    }

    private async void btnSendRandom_Click(object? sender, EventArgs e)
    {
        try
        {
            var index = cmbMsgType.SelectedIndex;
            var (frame, userData) = BuildRandomFrameWithUserData(index);
            await SendFrameAsync(frame);
            var description = MessageDescriptor.DescribeByIndex(index, userData);
            LogSend(frame, description);
        }
        catch (Exception ex)
        {
            LogError($"发送失败: {ex.Message}");
        }
    }

    private void btnClear_Click(object? sender, EventArgs e)
    {
        rtbLog.Clear();
    }

    // ================================================================
    // 报文调度 —— 根据 ComboBox 索引路由到对应的 Build 方法
    // ================================================================

    private static byte[] BuildFrameByIndex(int index, byte[] userData)
    {
        return index switch
        {
            0 => MessageBuilder.BuildDCSStatusMsg(),
            1 => MessageBuilder.BuildChangeTractionStateMsg(new ChangeTractionState()),
            2 => MessageBuilder.BuildLogInMaglevVehicleMsg(new LogInMaglevVehicle()),
            3 => MessageBuilder.BuildLogOutMaglevVehicleMsg(new LogOutMaglevVehicle()),
            4 => MessageBuilder.BuildMaxSpeedCurveMsg(userData),
            5 => MessageBuilder.BuildTrackDataMsg(userData),
            6 => MessageBuilder.BuildRemoveTrackDataMsg(new RemoveTrackData()),
            7 => MessageBuilder.BuildRequestParkingPointStatusMsg(new RequestParkingPointStatus()),
            8 => MessageBuilder.BuildStepParkingPointMsg(new StepParkingPoint()),
            9 => MessageBuilder.BuildMCUStatusMsg(userData),
            10 => MessageBuilder.BuildMCUReplyMsg(userData),
            // MOCS→VCU 下行报文（EMS 控制 / OBC 控制）
            11 => MessageBuilder.BuildEMSControlMsg(userData),
            12 => MessageBuilder.BuildOBCControlMsg(userData),
            // VCU→MOCS 上行报文（EMS / VSPS / OBC）
            13 => MessageBuilder.BuildEMSStatusMsgA(0x21, userData),
            14 => MessageBuilder.BuildEMSStatusMsgB(0x61, userData),
            15 => MessageBuilder.BuildVSPSStatusMsg(userData),
            16 => MessageBuilder.BuildOBCMsg(userData),
            _ => throw new InvalidOperationException("未知报文类型"),
        };
    }

    private static byte[] BuildRandomFrameByIndex(int index)
    {
        return BuildRandomFrameWithUserData(index).Frame;
    }

    /// <summary>
    private static (byte[] Frame, byte[] UserData) BuildRandomFrameWithUserData(int index)
    {
        byte[] userData;
        byte[] frame;

        switch (index)
        {
            // MOCS→MCU 下行报文
            case 0:
                userData = MessageBuilder.GenerateRandomMOCSStatus();
                frame = MessageBuilder.BuildDCSStatusMsg(userData);
                break;
            case 1:
                userData = MessageBuilder.GenerateRandomChangeTractionState();
                frame = MessageBuilder.BuildChangeTractionStateMsg(userData);
                break;
            case 2:
                userData = MessageBuilder.GenerateRandomLogInMaglevVehicle();
                frame = MessageBuilder.BuildLogInMaglevVehicleMsg(userData);
                break;
            case 3:
                userData = MessageBuilder.GenerateRandomLogOutMaglevVehicle();
                frame = MessageBuilder.BuildLogOutMaglevVehicleMsg(userData);
                break;
            case 4:
                userData = MessageBuilder.GenerateRandomBytes(16);
                frame = MessageBuilder.BuildMaxSpeedCurveMsg(userData);
                break;
            case 5:
                userData = MessageBuilder.GenerateRandomBytes(20);
                frame = MessageBuilder.BuildTrackDataMsg(userData);
                break;
            case 6:
                userData = MessageBuilder.GenerateRandomRemoveTrackData();
                frame = MessageBuilder.BuildRemoveTrackDataMsg(userData);
                break;
            case 7:
                userData = MessageBuilder.GenerateRandomRequestParkingPointStatus();
                frame = MessageBuilder.BuildRequestParkingPointStatusMsg(userData);
                break;
            case 8:
                userData = MessageBuilder.GenerateRandomStepParkingPoint();
                frame = MessageBuilder.BuildStepParkingPointMsg(userData);
                break;
            // MCU→MOCS 上行报文
            case 9:
                userData = MessageBuilder.GenerateRandomMCUStatus();
                frame = MessageBuilder.BuildMCUStatusMsg(userData);
                break;
            case 10:
                userData = MessageBuilder.GenerateRandomMCUReply();
                frame = MessageBuilder.BuildMCUReplyMsg(userData);
                break;
            // MOCS→VCU 下行报文
            case 11:
                userData = MessageBuilder.GenerateRandomEMSControl();
                frame = MessageBuilder.BuildEMSControlMsg(userData);
                break;
            case 12:
                userData = MessageBuilder.GenerateRandomOBCControl();
                frame = MessageBuilder.BuildOBCControlMsg(userData);
                break;
            // VCU→MOCS 上行报文
            case 13:
                {
                    userData = MessageBuilder.GenerateRandomEMSStatusMsgA();
                    frame = MessageBuilder.BuildEMSStatusMsgA(userData[0], userData[1..9]);
                }
                break;
            case 14:
                {
                    userData = MessageBuilder.GenerateRandomEMSStatusMsgB();
                    frame = MessageBuilder.BuildEMSStatusMsgB(userData[0], userData[1..9]);
                }
                break;
            case 15:
                userData = MessageBuilder.GenerateRandomVSPSStatus();
                frame = MessageBuilder.BuildVSPSStatusMsg(userData);
                break;
            case 16:
                userData = MessageBuilder.GenerateRandomOBCStatus();
                frame = MessageBuilder.BuildOBCMsg(userData);
                break;
            default:
                throw new InvalidOperationException("未知报文类型");
        }

        return (frame, userData);
    }

    // ================================================================
    // 报文类型元信息
    // ================================================================

    private sealed record MsgTypeInfo(
        string Name,
        string MsgIdHex,
        int UserDataLen,
        bool IsMCUPort,
        string UserDataHint);

    private static MsgTypeInfo GetMsgTypeInfo(int index) => index switch
    {
        0 => new("DCS状态报文", "0x01", 148, true, "UserData (148字节) — 自动使用 MOCSStatus 默认字段值"),
        1 => new("改变牵引状态报文", "0x02", 2, true, "UserData (2字节) — 自动使用 ChangeTractionState 默认字段值"),
        2 => new("悬浮架登录报文", "0x03", 14, true, "UserData (14字节) — 自动使用 LogInMaglevVehicle 默认字段值"),
        3 => new("悬浮架注销报文", "0x04", 2, true, "UserData (2字节) — 自动使用 LogOutMaglevVehicle 默认字段值"),
        4 => new("最大速度曲线报文", "0x05", 16, true, "UserData (16字节，十六进制，空格分隔)"),
        5 => new("线路数据报文", "0x06", -1, true, "UserData (变长，偶数长度，十六进制，空格分隔)"),
        6 => new("线路数据删除报文", "0x07", 8, true, "UserData (8字节) — 自动使用 RemoveTrackData 默认字段值"),
        7 => new("停车点运行状态请求报文", "0x08", 4, true, "UserData (4字节) — 自动使用 RequestParkingPointStatus 默认字段值"),
        8 => new("停车点运行步进报文", "0x09", 4, true, "UserData (4字节) — 自动使用 StepParkingPoint 默认字段值"),
        9 => new("牵引状态报文", "0x81", 164, true, "UserData (164字节，十六进制，空格分隔)"),
        10 => new("应答报文", "0x82", 12, true, "UserData (12字节，十六进制，空格分隔)"),
        // MOCS→VCU 下行报文：目标端口自动切换为 6001(VCU)
        11 => new("EMS控制报文", "0x21", 8, false, "CAN数据 (8字节，十六进制，空格分隔) — MOCS→LCU/GCU"),
        12 => new("OBC控制报文", "0x71", 14, false, "UserData (14字节，十六进制，空格分隔) — MOCS→OBC"),
        // VCU→MOCS 上行报文：目标端口自动切换为 6001(VCU)
        13 => new("EMS状态报文A", "0x21", 8, false, "CAN数据 (8字节，十六进制，空格分隔) — CANID自动设为0x21"),
        14 => new("EMS状态报文B", "0x61", 8, false, "CAN数据 (8字节，十六进制，空格分隔) — CANID自动设为0x61"),
        15 => new("VSPS状态报文", "0xE1", 12, false, "UserData (12字节，十六进制，空格分隔)"),
        16 => new("OBC状态报文", "0xF1", 16, false, "UserData (16字节，十六进制，空格分隔)"),
        _ => throw new ArgumentOutOfRangeException(nameof(index)),
    };

    // ================================================================
    // UDP 发送
    // ================================================================

    private async Task SendFrameAsync(byte[] frame)
    {
        // 索引 0 = "6002 (MCU/DCS)", 索引 1 = "6001 (VCU)"
        if (cmbTarget.SelectedIndex == 1)
            await _sender.SendToVCUAsync(frame);
        else
            await _sender.SendToMCUAsync(frame);
    }

    // ================================================================
    // 日志
    // ================================================================

    private void LogSend(byte[] frame, string? description = null)
    {
        var now = DateTime.Now.ToString("HH:mm:ss.fff");
        var info = GetMsgTypeInfo(cmbMsgType.SelectedIndex);
        var target = cmbTarget.SelectedIndex == 1 ? "6001(VCU)" : "6002(MCU)";
        var hex = BitConverter.ToString(frame);
        var seq = frame.Length >= 2 ? BitConverter.ToUInt16(frame, 2) : (ushort)0;

        rtbLog.AppendText($"[{now}] → {target}  {info.Name}  Seq={seq}  Len={frame.Length}B\r\n");
        rtbLog.AppendText($"        {hex}\r\n");
        if (description != null)
        {
            rtbLog.SelectionColor = Color.Cyan;
            rtbLog.AppendText(description);
            rtbLog.SelectionColor = Color.LightGreen;
            rtbLog.AppendText("\r\n");
        }
        else
        {
            rtbLog.AppendText("\r\n");
        }
        rtbLog.ScrollToCaret();
    }

    private void LogError(string message)
    {
        var now = DateTime.Now.ToString("HH:mm:ss.fff");
        rtbLog.SelectionColor = Color.Red;
        rtbLog.AppendText($"[{now}] 错误: {message}\r\n\r\n");
        rtbLog.SelectionColor = Color.LightGreen;
        rtbLog.ScrollToCaret();
    }

    // ================================================================
    // 工具方法
    // ================================================================

    private static byte[] ParseHex(string hex)
    {
        hex = hex.Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "");
        if (hex.Length % 2 != 0)
            throw new ArgumentException("十六进制字符串长度必须为偶数");

        byte[] data = new byte[hex.Length / 2];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }
        return data;
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (!_disposed)
        {
            _sender.Dispose();
            _disposed = true;
        }
    }
}
