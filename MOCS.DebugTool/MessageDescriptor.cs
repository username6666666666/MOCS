using System.Buffers.Binary;
using System.Collections;
using System.Text;
using MOCS.Cores.MCU;
using MOCS.Cores.VCU;

namespace MOCS.DebugTool;

/// <summary>
/// 报文参数描述器 —— 将随机生成的 UserData 字节按协议定义反向解析为人类可读的参数列表。
/// 解析逻辑与 MOCS 主程序 OnRecvXXXMsg / ToByteArray / ToCANMsg 完全一致。
/// 仅用于 DebugTool 本地日志显示，不影响 MOCS 主程序。
/// </summary>
public static class MessageDescriptor
{
    // ================================================================
    // 公共入口：根据 ComboBox 索引路由
    // ================================================================

    /// <summary>
    /// 根据报文类型索引返回 UserData 的参数描述字符串。
    /// </summary>
    public static string DescribeByIndex(int index, byte[] userData)
    {
        return index switch
        {
            // MCU 下行 (MOCS → MCU)
            0 => DescribeMOCSStatus(userData),
            1 => DescribeChangeTractionState(userData),
            2 => DescribeLogInMaglevVehicle(userData),
            3 => DescribeLogOutMaglevVehicle(userData),
            4 => DescribeMaxSpeedCurve(userData),
            5 => DescribeTrackData(userData),
            6 => DescribeRemoveTrackData(userData),
            7 => DescribeRequestParkingPointStatus(userData),
            8 => DescribeStepParkingPoint(userData),
            // MCU 上行 (MCU → MOCS)
            9 => DescribeMCUStatus(userData),
            10 => DescribeMCUReply(userData),
            // VCU 下行 (MOCS → VCU)
            11 => DescribeEMSControl(userData),
            12 => DescribeOBCControl(userData),
            // VCU 上行 (VCU → MOCS)
            13 => DescribeEMSStatusMsgA(userData),
            14 => DescribeEMSStatusMsgB(userData),
            15 => DescribeVSPSStatus(userData),
            16 => DescribeOBCStatus(userData),
            _ => $"未知报文类型索引: {index}",
        };
    }

    // ================================================================
    // 0. DCS状态报文 (MOCSStatus, MsgId=0x01, 148字节)
    // 参考 MOCSStatus.ToByteArray()
    // ================================================================

    public static string DescribeMOCSStatus(byte[] data)
    {
        if (data.Length < 19) return "(数据长度不足，无法解析 DCS状态报文)";

        var sb = new StringBuilder();
        sb.AppendLine("=== DCS状态报文 (0x01) 参数 ===");

        sb.AppendLine($"  通道1接收状态       = {(ChannelRecvStatusEnum)data[0]}");
        sb.AppendLine($"  通道2接收状态       = {(ChannelRecvStatusEnum)data[2]}");
        sb.AppendLine($"  牵引状态报文接收状态 = {(MCUStatusMessageRecvStatusEnum)data[4]}");
        sb.AppendLine($"  状态改变就绪请求     = {(RequestForMCUStatusChangeReadinessEnum)data[5]}");
        sb.AppendLine($"  上次应答序列号(A类)  = {BinaryPrimitives.ReadUInt16LittleEndian(data.AsSpan(6, 2))}");
        sb.AppendLine($"  可重复报文最大数量   = {data[8]}");
        sb.AppendLine($"  当前悬浮架标识号     = {data[9]}");
        sb.AppendLine($"  悬浮/落下命令状态    = {(MaglevVehicleLeviCommandStatusEnum)(data[10] & 0x03)}");
        sb.AppendLine($"  悬浮/落下状态        = {(MaglevVehicleLeviStatusEnum)(data[10] & 0x0C)}");
        sb.AppendLine($"  期望速度类型         = {(ExpectedSpeedTypeEnum)(data[11] & 0x03)}");
        sb.AppendLine($"  期望运行方向         = {(ExpectedRunningDircetionEnum)(data[11] & 0x30)}");
        sb.AppendLine($"  期望速度             = {BinaryPrimitives.ReadInt16LittleEndian(data.AsSpan(12, 2))}");
        sb.AppendLine($"  最大加速度限制       = {BinaryPrimitives.ReadInt16LittleEndian(data.AsSpan(14, 2))}");
        sb.AppendLine($"  最小速度             = {BinaryPrimitives.ReadInt16LittleEndian(data.AsSpan(16, 2))}");
        sb.AppendLine($"  诊断信息             = {(DiagnosticInfoEnum)data[18]}");

        return sb.ToString();
    }

    // ================================================================
    // 1. 改变牵引状态报文 (ChangeTractionState, MsgId=0x02, 2字节)
    // 参考 ChangeTractionState.ToByteArray()
    // ================================================================

    public static string DescribeChangeTractionState(byte[] data)
    {
        if (data.Length < 2) return "(数据长度不足)";

        var sb = new StringBuilder();
        sb.AppendLine("=== 改变牵引状态报文 (0x02) 参数 ===");
        sb.AppendLine($"  目标状态 = {(TargetStateEnum)data[0]}");
        return sb.ToString();
    }

    // ================================================================
    // 2. 悬浮架登录报文 (LogInMaglevVehicle, MsgId=0x03, 14字节)
    // 参考 LogInMaglevVehicle.ToByteArray()
    // ================================================================

    public static string DescribeLogInMaglevVehicle(byte[] data)
    {
        if (data.Length < 14) return "(数据长度不足)";

        var sb = new StringBuilder();
        sb.AppendLine("=== 悬浮架登录报文 (0x03) 参数 ===");
        sb.AppendLine($"  悬浮架标识号   = {data[0]}");
        sb.AppendLine($"  悬浮架类型     = {(MaglevVehicleTypeEnum)data[1]}");
        sb.AppendLine($"  MOCS编号       = {data[2]}");
        sb.AppendLine($"  悬浮架方向     = {(MaglevVehicleDirectionEnum)data[3]}");
        sb.AppendLine($"  当前位置       = {BinaryPrimitives.ReadInt32LittleEndian(data.AsSpan(4, 4))} cm");
        sb.AppendLine($"  悬浮架长度     = {BinaryPrimitives.ReadInt32LittleEndian(data.AsSpan(8, 4))} cm");
        sb.AppendLine($"  最大线路允许速度 = {BinaryPrimitives.ReadInt16LittleEndian(data.AsSpan(12, 2))} cm/s");
        return sb.ToString();
    }

    // ================================================================
    // 3. 悬浮架注销报文 (LogOutMaglevVehicle, MsgId=0x04, 2字节)
    // 参考 LogOutMaglevVehicle.ToByteArray()
    // ================================================================

    public static string DescribeLogOutMaglevVehicle(byte[] data)
    {
        if (data.Length < 2) return "(数据长度不足)";

        var sb = new StringBuilder();
        sb.AppendLine("=== 悬浮架注销报文 (0x04) 参数 ===");
        sb.AppendLine($"  悬浮架标识号 = {data[0]}");
        return sb.ToString();
    }

    // ================================================================
    // 4. 最大速度曲线报文 (MaxSpeedCurve, MsgId=0x05, 16字节)
    // 无对应字段定义类，纯二进制数据
    // ================================================================

    public static string DescribeMaxSpeedCurve(byte[] data)
    {
        var sb = new StringBuilder();
        sb.AppendLine("=== 最大速度曲线报文 (0x05) ===");
        sb.AppendLine("  (无字段定义，纯二进制数据)");
        return sb.ToString();
    }

    // ================================================================
    // 5. 线路数据报文 (TrackData, MsgId=0x06, 变长)
    // 无对应字段定义类，纯二进制数据
    // ================================================================

    public static string DescribeTrackData(byte[] data)
    {
        var sb = new StringBuilder();
        sb.AppendLine("=== 线路数据报文 (0x06) ===");
        sb.AppendLine($"  (无字段定义，纯二进制数据，长度={data.Length}字节)");
        return sb.ToString();
    }

    // ================================================================
    // 6. 线路数据删除报文 (RemoveTrackData, MsgId=0x07, 8字节)
    // 参考 RemoveTrackData.ToByteArray()
    // ================================================================

    public static string DescribeRemoveTrackData(byte[] data)
    {
        if (data.Length < 8) return "(数据长度不足)";

        var sb = new StringBuilder();
        sb.AppendLine("=== 线路数据删除报文 (0x07) 参数 ===");
        sb.AppendLine($"  KI标识号     = {BinaryPrimitives.ReadUInt16LittleEndian(data.AsSpan(0, 2))}");
        sb.AppendLine($"  删除范围标志 = {(RemoveFlagEnum)data[2]}");
        sb.AppendLine($"  静态公里标   = {BinaryPrimitives.ReadInt32LittleEndian(data.AsSpan(3, 4))}");
        return sb.ToString();
    }

    // ================================================================
    // 7. 停车点运行状态请求报文 (RequestParkingPointStatus, MsgId=0x08, 4字节)
    // 参考 RequestParkingPointStatus.ToByteArray()
    // ================================================================

    public static string DescribeRequestParkingPointStatus(byte[] data)
    {
        if (data.Length < 4) return "(数据长度不足)";

        var sb = new StringBuilder();
        sb.AppendLine("=== 停车点运行状态请求报文 (0x08) 参数 ===");
        sb.AppendLine($"  KI标识号       = {BinaryPrimitives.ReadUInt16LittleEndian(data.AsSpan(0, 2))}");
        sb.AppendLine($"  悬浮架标识号   = {data[2]}");
        return sb.ToString();
    }

    // ================================================================
    // 8. 停车点运行步进报文 (StepParkingPoint, MsgId=0x09, 4字节)
    // 参考 StepParkingPoint.ToByteArray()
    // ================================================================

    public static string DescribeStepParkingPoint(byte[] data)
    {
        if (data.Length < 4) return "(数据长度不足)";

        var sb = new StringBuilder();
        sb.AppendLine("=== 停车点运行步进报文 (0x09) 参数 ===");
        sb.AppendLine($"  KI标识号       = {BinaryPrimitives.ReadUInt16LittleEndian(data.AsSpan(0, 2))}");
        sb.AppendLine($"  悬浮架标识号   = {data[2]}");
        return sb.ToString();
    }

    // ================================================================
    // 9. 牵引状态报文 (MCUStatus, MsgId=0x81, 164字节)
    // 参考 MCUInterface.OnRecvMCUStatusMsg()
    // ================================================================

    public static string DescribeMCUStatus(byte[] data)
    {
        if (data.Length < 27) return "(数据长度不足)";

        var sb = new StringBuilder();
        sb.AppendLine("=== 牵引状态报文 (0x81) 参数 ===");

        // 通信状态
        sb.AppendLine($"  通道1接收状态           = {(ChannelRecvStatusEnum)(data[0] & 0x3F)}");
        sb.AppendLine($"  通道2接收状态           = {(ChannelRecvStatusEnum)(data[2] & 0x3F)}");
        sb.AppendLine($"  发送理由               = {(MCUSendReasonEnum)data[4]}");
        sb.AppendLine($"  需重发报文数           = {data[5]}");

        // 序列号与就绪
        sb.AppendLine($"  A类序列号参考点         = {BinaryPrimitives.ReadUInt16LittleEndian(data.AsSpan(6, 2))}");
        sb.AppendLine($"  状态改变就绪信息        = {(MCUStatusChangeReadinessInfoEnum)(data[8] & 0x07)}");

        // 当前悬浮架
        sb.AppendLine($"  当前悬浮架ID           = {data[9]}");
        sb.AppendLine($"  清除就绪信息(当前)      = {(ClearMaglevVehicleReadinessInfoEnum)(data[10] & 0x03)}");
        sb.AppendLine($"  DCS编号                = {data[11]}");
        sb.AppendLine($"  当前位置               = {BinaryPrimitives.ReadInt32LittleEndian(data.AsSpan(12, 4))} cm");
        sb.AppendLine($"  当前速度               = {BinaryPrimitives.ReadInt16LittleEndian(data.AsSpan(16, 2))} cm/s");
        sb.AppendLine($"  当前加速度             = {BinaryPrimitives.ReadInt16LittleEndian(data.AsSpan(18, 2))} cm/s²");
        sb.AppendLine($"  当前牵引能力           = {data[20]}%");
        sb.AppendLine($"  停车点运行状态          = {(CurrentMaglevVehicleSPRStatusEnum)(data[21] & 0x0F)}");

        // 虚拟悬浮架
        sb.AppendLine($"  虚拟悬浮架ID           = {data[22]}");
        sb.AppendLine($"  清除就绪信息(虚拟)      = {(ClearMaglevVehicleReadinessInfoEnum)(data[23] & 0x03)}");
        sb.AppendLine($"  虚拟牵引能力           = {data[24]}%");

        // 故障与停车点
        sb.AppendLine($"  牵引故障状态           = {(MCUFaultStatusEnum)data[25]}");
        sb.AppendLine($"  已管理停车点数量        = {data[26]}");

        // 停车点调试信息（字节 27+）
        sb.AppendLine($"  --- 停车点调试信息 (字节 27-163, {data.Length - 27}B) ---");
        for (int i = 27; i < data.Length && i < 163; i++)
        {
            if (data[i] != 0)
            {
                sb.AppendLine($"    [{i}] = 0x{data[i]:X2} ({data[i]})");
            }
        }

        return sb.ToString();
    }

    // ================================================================
    // 10. 应答报文 (MCUReply, MsgId=0x82, 12字节)
    // 参考 MCUInterface.OnRecvMCUReplyMsg()
    // ================================================================

    public static string DescribeMCUReply(byte[] data)
    {
        if (data.Length < 12) return "(数据长度不足)";

        var sb = new StringBuilder();
        sb.AppendLine("=== 应答报文 (0x82) 参数 ===");
        sb.AppendLine($"  通道1接收状态     = {(ChannelRecvStatusEnum)(data[0] & 0x3F)}");
        sb.AppendLine($"  通道2接收状态     = {(ChannelRecvStatusEnum)(data[2] & 0x3F)}");
        sb.AppendLine($"  应答错误标识号    = {(MCUReplyErrorIdentifierEnum)data[4]}");
        sb.AppendLine($"  需重发报文数      = {data[5]}");
        sb.AppendLine($"  处理状态          = {(MCUProcessStatusEnum)data[6]}");
        sb.AppendLine($"  响应的报文MsgId   = 0x{data[7]:X2}");
        return sb.ToString();
    }

    // ================================================================
    // 11. EMS控制报文 (EMSControl, MsgId=0x21, 8字节 CAN数据)
    // 参考 EMSControl.ToCANMsg()
    // ================================================================

    public static string DescribeEMSControl(byte[] data)
    {
        if (data.Length < 8) return "(数据长度不足)";

        var sb = new StringBuilder();
        sb.AppendLine("=== EMS控制报文 (0x21) 参数 ===");

        // Byte 0: TrainDirection(高2位) | Life(低4位)
        sb.AppendLine($"  悬浮架运行方向   = {(TrainDirectionEnum)(data[0] & 0xC0)}");
        sb.AppendLine($"  生命周期信号     = {data[0] & 0x0F}");

        // Byte 1: ETStatus(bit1) | EBStatus(bit0)
        sb.AppendLine($"  电机牵引状态     = {(ETStatusEnum)(data[1] & 0x02)}");
        sb.AppendLine($"  电制动状态       = {(EBStatusEnum)(data[1] & 0x01)}");

        // Byte 2: Velocity
        sb.AppendLine($"  列车速度         = {data[2]} km/h");

        // Byte 3: EMSCmd(高3位) | ControllerId(低5位)
        sb.AppendLine($"  EMS控制命令      = {(EMSCmdEnum)(data[3] & 0xE0)}");
        sb.AppendLine($"  控制器编号[3]    = {data[3] & 0x1F}");

        // Byte 4: BrakeLevel(高3位) | ControllerId(低5位)
        sb.AppendLine($"  制动等级         = {data[4] >> 5}");
        sb.AppendLine($"  控制器编号[4]    = {data[4] & 0x1F}");

        // Byte 5-6: Distance (UInt16 BigEndian)
        sb.AppendLine($"  离站距离         = {BinaryPrimitives.ReadUInt16BigEndian(data.AsSpan(5, 2))} m");

        // Byte 7: CutCmd(高3位) | ControllerId(低5位)
        sb.AppendLine($"  切除命令         = {(CutCmdEnum)(data[7] & 0xE0)}");
        sb.AppendLine($"  控制器编号[7]    = {data[7] & 0x1F}");

        return sb.ToString();
    }

    // ================================================================
    // 12. OBC控制报文 (OBCControl, MsgId=0x71, 14字节)
    // 参考 OBCControl.ToBytesArray()
    // ================================================================

    public static string DescribeOBCControl(byte[] data)
    {
        if (data.Length < 14) return "(数据长度不足)";

        var bits = new BitArray(data.Take(5).ToArray());

        var sb = new StringBuilder();
        sb.AppendLine("=== OBC控制报文 (0x71) 参数 ===");
        sb.AppendLine("  --- 控制命令 (BitArray, 40bit) ---");
        sb.AppendLine($"  远程模式切换 (bit1)    = {bits[1]}");
        sb.AppendLine($"  急停         (bit2)    = {bits[2]}");
        sb.AppendLine($"  电池启动     (bit3)    = {bits[3]}");
        sb.AppendLine($"  电池停止     (bit4)    = {bits[4]}");
        sb.AppendLine($"  电源合闸     (bit5)    = {bits[5]}");
        sb.AppendLine($"  电源分闸     (bit6)    = {bits[6]}");
        sb.AppendLine($"  受流器伸     (bit9)    = {bits[9]}");
        sb.AppendLine($"  受流器缩     (bit10)   = {bits[10]}");
        sb.AppendLine($"  悬浮起浮     (bit11)   = {bits[11]}");
        sb.AppendLine($"  导向起浮     (bit12)   = {bits[12]}");
        sb.AppendLine($"  紧急模式     (bit16)   = {bits[16]}");

        // 制动等级：bit30, bit31, bit32
        byte brakeLevel = 0;
        if (bits[30]) brakeLevel |= 0x01;
        if (bits[31]) brakeLevel |= 0x02;
        if (bits[32]) brakeLevel |= 0x04;
        sb.AppendLine($"  制动等级     (bit30-32)= {brakeLevel}");

        // Byte 13: BrakeLevel
        sb.AppendLine($"  --- 附加字段 ---");
        sb.AppendLine($"  制动等级(byte13)       = {data[13]}");

        return sb.ToString();
    }

    // ================================================================
    // 13. EMS状态报文A (EMSStatusMsgA, MsgId=0x81, UserData=10字节: CANID+8字节CAN数据+补齐)
    // 参考 VCUInterface.OnRecvEMSStatusMsgA()
    // ================================================================

    public static string DescribeEMSStatusMsgA(byte[] userData)
    {
        if (userData.Length < 9) return "(数据长度不足)";

        var canId = userData[0];
        var canData = userData.AsSpan(1, 8);

        var sb = new StringBuilder();
        sb.AppendLine($"=== EMS状态报文A (CANID=0x{canId:X2}) 参数 ===");

        // Byte 0: GapSensors(高3位) | EMSCmd(bit4) | Life(低4位)
        sb.AppendLine($"  间隙传感器阵列状态 = {(GapSensorsStatusEnum)(canData[0] & 0xE0)}");
        sb.AppendLine($"  EMS控制命令状态    = {(EMSCmdStatusEnum)(canData[0] & 0x10)}");
        sb.AppendLine($"  生命周期信号       = {canData[0] & 0x0F}");

        // Byte 1: EMSSysStatus(bit7) | Overload(bit6) | AccSensors(bits5-4) | Operation(bit3) | Warning(bit2) | Fault(bits1-0)
        sb.AppendLine($"  EMS系统状态        = {(EMSSysStatusEnum)(canData[1] & 0x80)}");
        sb.AppendLine($"  过流保护状态       = {(OverloadStatusEnum)(canData[1] & 0x40)}");
        sb.AppendLine($"  加速度传感器阵列状态 = {(AccSensorsStatusEnum)(canData[1] & 0x30)}");
        sb.AppendLine($"  EMS工作状态        = {(EMSOperationStatusEnum)(canData[1] & 0x08)}");
        sb.AppendLine($"  EMS警告状态        = {(EMSWarningEnum)(canData[1] & 0x04)}");
        sb.AppendLine($"  EMS故障状态        = {(EMSFaultStatusEnum)(canData[1] & 0x03)}");

        // Byte 2: KM2Status(bit7) | Temp(低7位) - 27
        sb.AppendLine($"  KM2接触器状态      = {(KMStatusEnum)(canData[2] & 0x80)}");
        sb.AppendLine($"  温度               = {(canData[2] & 0x7F) - 27} ℃");

        // Byte 3: KM1Contact(bit7) | Gap(低7位) * 20/127
        sb.AppendLine($"  KM1闭合状态        = {(KMContactStatusEnum)(canData[3] & 0x80)}");
        sb.AppendLine($"  间隙               = {(canData[3] & 0x7F) * 0.157480:F2} mm");

        // Byte 4: KM2Contact(bit7) | U(低7位) * 530/127
        sb.AppendLine($"  KM2闭合状态        = {(KMContactStatusEnum)(canData[4] & 0x80)}");
        sb.AppendLine($"  电压               = {(canData[4] & 0x7F) * 4.173228:F2} V");

        // Byte 5: CPUStatus(bit7) | I1(低7位) * 160/127
        sb.AppendLine($"  CPU状态            = {(CPUStatusEnum)(canData[5] & 0x80)}");
        sb.AppendLine($"  电流I1             = {(canData[5] & 0x7F) * 1.259842:F2} A");

        // Byte 6: KM1Status(bit7) | Acc(低7位) * 100/127 - 50
        sb.AppendLine($"  KM1接触器状态      = {(KMStatusEnum)(canData[6] & 0x80)}");
        sb.AppendLine($"  加速度             = {(canData[6] & 0x7F) * 0.7874016 - 50.0:F2} m/s²");

        // Byte 7: Brake(bit5) | SysSwitch(bit4) | GapWarn(bit3) | OverloadWarn(bit2) | Stability(bit1) | Cut(bit0)
        sb.AppendLine($"  制动状态           = {(BrakeStatusEnum)(canData[7] & 0x20)}");
        sb.AppendLine($"  系统切换状态       = {(SysSwitchStatusEnum)(canData[7] & 0x10)}");
        sb.AppendLine($"  间隙预警           = {(GapWarnningStatusEnum)(canData[7] & 0x08)}");
        sb.AppendLine($"  过流预警           = {(OverloadWarningStatusEnum)(canData[7] & 0x04)}");
        sb.AppendLine($"  失稳状态           = {(StabilityStatusEnum)(canData[7] & 0x02)}");
        sb.AppendLine($"  切除状态           = {(CutStatusEnum)(canData[7] & 0x01)}");

        return sb.ToString();
    }

    // ================================================================
    // 14. EMS状态报文B (EMSStatusMsgB, MsgId=0x82, UserData=10字节: CANID+8字节CAN数据+补齐)
    // 参考 VCUInterface.OnRecvEMSStatusMsgB()
    // ================================================================

    public static string DescribeEMSStatusMsgB(byte[] userData)
    {
        if (userData.Length < 9) return "(数据长度不足)";

        var canId = userData[0];
        var canData = userData.AsSpan(1, 8);

        var sb = new StringBuilder();
        sb.AppendLine($"=== EMS状态报文B (CANID=0x{canId:X2}) 参数 ===");

        // Byte 1: GapSensor1(bit7) | Gap1(低7位) * 20/127
        sb.AppendLine($"  间隙传感器1状态     = {(GapSensorStatusEnum)(canData[1] & 0x80)}");
        sb.AppendLine($"  间隙1               = {(canData[1] & 0x7F) * 0.157480:F2} mm");

        // Byte 2: GapSensor2(bit7) | Gap2(低7位)
        sb.AppendLine($"  间隙传感器2状态     = {(GapSensorStatusEnum)(canData[2] & 0x80)}");
        sb.AppendLine($"  间隙2               = {(canData[2] & 0x7F) * 0.157480:F2} mm");

        // Byte 3: GapSensor3(bit7) | Gap3(低7位)
        sb.AppendLine($"  间隙传感器3状态     = {(GapSensorStatusEnum)(canData[3] & 0x80)}");
        sb.AppendLine($"  间隙3               = {(canData[3] & 0x7F) * 0.157480:F2} mm");

        // Byte 4: GapSensor4(bit7) | Gap4(低7位)
        sb.AppendLine($"  间隙传感器4状态     = {(GapSensorStatusEnum)(canData[4] & 0x80)}");
        sb.AppendLine($"  间隙4               = {(canData[4] & 0x7F) * 0.157480:F2} mm");

        // Byte 5: AccSensor1(bit7) | Acc1(低7位) * 100/127 - 50
        sb.AppendLine($"  加速度传感器1状态   = {(AccSensorStatusEnum)(canData[5] & 0x80)}");
        sb.AppendLine($"  加速度1             = {(canData[5] & 0x7F) * 0.7874016 - 50.0:F2} m/s²");

        // Byte 6: AccSensor2(bit7) | Acc2(低7位)
        sb.AppendLine($"  加速度传感器2状态   = {(AccSensorStatusEnum)(canData[6] & 0x80)}");
        sb.AppendLine($"  加速度2             = {(canData[6] & 0x7F) * 0.7874016 - 50.0:F2} m/s²");

        // Byte 7: I2(低7位) * 160/127
        sb.AppendLine($"  电流I2              = {(canData[7] & 0x7F) * 1.259842:F2} A");

        return sb.ToString();
    }

    // ================================================================
    // 15. VSPS状态报文 (VSPSStatus, MsgId=0xE1, UserData=12字节)
    // 参考 VCUInterface.OnRecvVSPSStatusMsg()
    // ================================================================

    public static string DescribeVSPSStatus(byte[] data)
    {
        if (data.Length < 12) return "(数据长度不足)";

        var sb = new StringBuilder();
        sb.AppendLine("=== VSPS状态报文 (0xE1) 参数 ===");

        // Byte 0-1: Life (UInt16 BigEndian)
        sb.AppendLine($"  生命周期信号   = {BinaryPrimitives.ReadUInt16BigEndian(data.AsSpan(0, 2))}");

        // Byte 2: 保留
        sb.AppendLine($"  保留字节[2]    = 0x{data[2]:X2}");

        // Byte 3: Forward (0x00=反向, 0x01=正向)
        sb.AppendLine($"  方向           = {(data[3] == 0x01 ? "正向" : "反向")}");

        // Byte 4-5: RelativePos (UInt16 BigEndian)
        sb.AppendLine($"  相对位置       = {BinaryPrimitives.ReadUInt16BigEndian(data.AsSpan(4, 2))}");

        // Byte 6-7: Speed (UInt16 BigEndian)
        sb.AppendLine($"  速度           = {BinaryPrimitives.ReadUInt16BigEndian(data.AsSpan(6, 2))}");

        return sb.ToString();
    }

    // ================================================================
    // 16. OBC状态报文 (OBCStatus, MsgId=0xF1, 16字节)
    // 参考 VCUInterface.OnRecvOBCStatusMsg()
    // ================================================================

    public static string DescribeOBCStatus(byte[] data)
    {
        if (data.Length < 16) return "(数据长度不足)";

        var bits = new BitArray(data.Take(8).ToArray());

        var sb = new StringBuilder();
        sb.AppendLine("=== OBC状态报文 (0xF1) 参数 ===");

        // 控制反馈状态位
        sb.AppendLine("  --- 控制反馈状态 ---");
        sb.AppendLine($"  远程模式切换  (bit1)  = {bits[1]}");
        sb.AppendLine($"  急停发生      (bit2)  = {bits[2]}");
        sb.AppendLine($"  电池启动      (bit3)  = {bits[3]}");
        sb.AppendLine($"  电源合闸      (bit5)  = {bits[5]}");
        sb.AppendLine($"  受流器伸      (bit9)  = {bits[9]}");
        sb.AppendLine($"  悬浮起浮      (bit11) = {bits[11]}");
        sb.AppendLine($"  导向起浮      (bit12) = {bits[12]}");
        sb.AppendLine($"  紧急模式      (bit16) = {bits[16]}");

        // OBC状态位
        sb.AppendLine("  --- OBC状态 ---");
        sb.AppendLine($"  440V电池开关闭合  (bit49) = {bits[49]}");
        sb.AppendLine($"  480V电源开关闭合  (bit51) = {bits[51]}");
        sb.AppendLine($"  DC330V断路器使能  (bit53) = {bits[53]}");
        sb.AppendLine($"  25kW电源故障      (bit54) = {bits[54]}");
        sb.AppendLine($"  5kW电源故障       (bit55) = {bits[55]}");
        sb.AppendLine($"  受流器带电        (bit56) = {bits[56]}");
        sb.AppendLine($"  受流器2升起       (bit58) = {bits[58]}");
        sb.AppendLine($"  受流器1升起       (bit60) = {bits[60]}");
        sb.AppendLine($"  悬浮状态          (bit62) = {bits[62]}");
        sb.AppendLine($"  导向使能          (bit63) = {bits[63]}");

        // 电池电量
        sb.AppendLine("  --- 电池电量 ---");
        sb.AppendLine($"  440V电池容量 = {BinaryPrimitives.ReadInt16BigEndian(data.AsSpan(8, 2))}%");
        sb.AppendLine($"  110V电池容量 = {BinaryPrimitives.ReadInt16BigEndian(data.AsSpan(10, 2))}%");

        // 制动等级
        sb.AppendLine($"  制动等级     = {data[15]}");

        return sb.ToString();
    }
}
