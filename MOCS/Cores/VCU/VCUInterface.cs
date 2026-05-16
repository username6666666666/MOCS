using System.Buffers.Binary;
using System.Collections;
using System.Net;
using MOCS.Coms;
using MOCS.Protocals;
using MOCS.Protocals.VehicleControl.MOCSToVehicle;
using MOCS.Protocals.VehicleControl.VehicleToMOCS;
using MOCS.Utils;
using NLog;
using Stateless;

namespace MOCS.Cores.VCU
{
    /// <summary>
    /// 车载控制器接口类
    /// </summary>
    public class VCUInterface
    {
        #region 面向界面层的公共接口

        public async Task StartAsync()
        {
            if (_vcInterfaceSM.CanFire(VCInterfaceTrigger.Activate))
            {
                await _vcInterfaceSM.FireAsync(VCInterfaceTrigger.Activate);
            }
        }

        public async Task StopAsync()
        {
            if (_vcInterfaceSM.CanFire(VCInterfaceTrigger.Deactivate))
            {
                await _vcInterfaceSM.FireAsync(VCInterfaceTrigger.Deactivate);
            }
        }

        #endregion

        public VCUInterface(ILogger syslogger, ILogger recvlogger, ILogger sendlogger)
        {
            SysLogger = syslogger;
            RecvLogger = recvlogger;
            SendLogger = sendlogger;
            _vcInterfaceSM = new StateMachine<VCInterfaceState, VCInterfaceTrigger>(
                VCInterfaceState.Stop
            );
            _EMSControlMsgSendTimer = new HighPrecisionTimer(
                new TimeSpan(0, 0, 0, 0, 100),
                EMSControlMsgSendTimeOut
            );
            _sequenceManager = new SequenceManager<ushort>(ushort.MaxValue);

            ConfigVCInterfaceStateMachine();
        }

        #region 重置和UI控件有绑定的信息
        private void RestoreAll()
        {
            // 重置LCU状态集合
            for (int i = 0; i < LCUNum - 1; i++)
            {
                LCUStatusCollection[i].Reset();
            }

            // 重置GCU状态集合
            for (int i = 0; i < GCUNum - 1; i++)
            {
                GCUStatusCollection[i].Reset();
            }

            VSPSInfoCollection.Reset();

            EMSControlField.Reset();
            SendOBCControlField.Reset();
            FeedBackOBCControlField.Reset();
            OBCStatusField.Reset();
        }
        #endregion

        #region 私有配置方法

        private void ConfigVCInterfaceStateMachine()
        {
            _vcInterfaceSM
                .Configure(VCInterfaceState.Stop)
                .OnEntryFrom(VCInterfaceTrigger.Deactivate, RestoreAll)
                .Permit(VCInterfaceTrigger.Activate, VCInterfaceState.Running)
                .OnExit(BeginCommunicate);

            _vcInterfaceSM
                .Configure(VCInterfaceState.Running)
                .Permit(VCInterfaceTrigger.Deactivate, VCInterfaceState.Stop)
                .InternalTransitionAsync(VCInterfaceTrigger.LCUMsgSend, SendMsgToVCU)
                .OnExitAsync(StopCommunicate);
        }

        private void ConfigMsgParsers()
        {
            _udpMsgSevice?.RegisterParser(0x81, EMSStatusMsgA.Parse);
            _udpMsgSevice?.RegisterParser(0x82, EMSStatusMsgB.Parse);
            _udpMsgSevice?.RegisterParser(0xE1, VSPSStatusMsg.Parse);
            _udpMsgSevice?.RegisterParser(0xF1, OBCMsg.Parse);
        }

        private void ConfigMsgHandlers()
        {
            _udpMsgSevice?.Subscribe<EMSStatusMsgA>(OnRecvEMSStatusMsgA);
            _udpMsgSevice?.Subscribe<EMSStatusMsgB>(OnRecvEMSStatusMsgB);
            _udpMsgSevice?.Subscribe<VSPSStatusMsg>(OnRecvVSPSStatusMsg);
            _udpMsgSevice?.Subscribe<OBCMsg>(OnRecvOBCStatusMsg);
        }

        private void ConfigureTimer(bool active, HighPrecisionTimer timer)
        {
            if (timer != null)
            {
                if (active)
                {
                    SysLogger.Info("启动EMS控制器生命周期报文发送定时器");
                    timer.Start();
                }
                else
                {
                    SysLogger.Info("关闭EMS控制器生命周期报文发送定时器");
                    timer.Stop();
                }
            }
        }

        #endregion

        private void BeginCommunicate()
        {
            _udpMsgSevice = new UdpMessageService<BaseMessage>(
                LocalIpAddress,
                LocalPort,
                RemoteIpAddress,
                RemotePort,
                RecvLogger,
                SendLogger
            );
            ConfigMsgParsers();
            ConfigMsgHandlers();
            ConfigureTimer(true, _EMSControlMsgSendTimer);
            _udpMsgSevice.StartListening();
            SysLogger.Info($"开始监听端口: {LocalPort}");
        }

        private async Task StopCommunicate()
        {
            ConfigureTimer(false, _EMSControlMsgSendTimer);
            if (_udpMsgSevice != null)
            {
                await _udpMsgSevice.DisposeAsync();
            }
            SysLogger.Info($"停止监听端口: {LocalPort}");
        }

        private async void EMSControlMsgSendTimeOut()
        {
            await _vcInterfaceSM.FireAsync(VCInterfaceTrigger.LCUMsgSend);
        }

        private async Task SendMsgToVCU()
        {
            await SendEMSControlMsgAsync();
            await SendOBCControlMsgAsync();
        }

        #region 消息发送方法
        private async Task SendEMSControlMsgAsync()
        {
            var sequenceNum = _sequenceManager.GetNextSequenceNum(PacketCategory.C);
            EMSControlMsg msg = new()
            {
                SequenceNumber = sequenceNum,
                MsgId = 0x21,
                UserData = EMSControlField.ToCANMsg(),
            };
            if (_udpMsgSevice != null)
            {
                await _udpMsgSevice.SendAsync(msg);
            }
        }

        private async Task SendOBCControlMsgAsync()
        {
            var sequenceNum = _sequenceManager.GetNextSequenceNum(PacketCategory.F);
            OBCMsg msg = new()
            {
                SequenceNumber = sequenceNum,
                MsgId = 0x71,
                UserData = SendOBCControlField.ToBytesArray(),
            };
            if (_udpMsgSevice != null)
            {
                await _udpMsgSevice.SendAsync(msg);
            }
        }
        #endregion

        #region 消息处理方法

        private void OnRecvEMSStatusMsgA(EMSStatusMsgA msg)
        {
            EMSStatus EMS;
            var CANID = msg.UserData.Span[0];
            if (CANID >= 0x21 && CANID <= 0x20 + LCUNum)
            {
                var index = (byte)(CANID - 0x21);
                EMS = LCUStatusCollection[index];
            }
            else if (CANID >= 0x41 && CANID <= 0x40 + GCUNum)
            {
                var index = (byte)(CANID - 0x41);
                EMS = GCUStatusCollection[index];
            }
            else
            {
                // EMS状态帧ID不合法
                return;
            }
            var CANData = msg.UserData.Span.Slice(1, 8);
            EMS.GapSensorsStatus = (GapSensorsStatusEnum)(CANData[0] & 0xE0);
            EMS.EMSCmd = (EMSCmdStatusEnum)(CANData[0] & 0x10);
            EMS.Life = (byte)(CANData[0] & 0x0F);
            EMS.EMSSysStatus = (EMSSysStatusEnum)(CANData[1] & 0x80);
            EMS.OverloadStatus = (OverloadStatusEnum)(CANData[1] & 0x40);
            EMS.AccSensorsStatus = (AccSensorsStatusEnum)(CANData[1] & 0x30);
            EMS.EMSOperationStatus = (EMSOperationStatusEnum)(CANData[1] & 0x08);
            EMS.EMSWarning = (EMSWarningEnum)(CANData[1] & 0x04);
            EMS.EMSFaultStatus = (EMSFaultStatusEnum)(CANData[1] & 0x03);
            EMS.KM2Status = (KMStatusEnum)(CANData[2] & 0x80);
            EMS.Temp = CANData[2] & 0x7F - 27;
            EMS.KM1ContactStatus = (KMContactStatusEnum)(CANData[3] & 0x80);
            // y = (20 / 127) * x
            EMS.Gap = (float)((CANData[3] & 0x7F) * 0.157480);
            EMS.KM2ContactStatus = (KMContactStatusEnum)(CANData[4] & 0x80);
            // y = (530 / 127) * x
            EMS.U = (float)((CANData[4] & 0x7F) * 4.173228);
            EMS.CPUStatus = (CPUStatusEnum)(CANData[5] & 0x80);
            // y = (160 / 127) * x
            EMS.I1 = (float)((CANData[5] & 0x7F) * 1.259842);
            EMS.KM1Status = (KMStatusEnum)(CANData[6] & 0x80);
            // y = (100 / 127) * x - 50
            EMS.Acc = (float)((CANData[6] & 0x7F) * 0.7874016 - 50.0);
            EMS.BrakeStatus = (BrakeStatusEnum)(CANData[7] & 0x20);
            EMS.SysSwitchStatus = (SysSwitchStatusEnum)(CANData[7] & 0x10);
            EMS.GapWarnningStatus = (GapWarnningStatusEnum)(CANData[7] & 0x08);
            EMS.OverloadWarningStatus = (OverloadWarningStatusEnum)(CANData[7] & 0x04);
            EMS.StabilityStatus = (StabilityStatusEnum)(CANData[7] & 0x02);
            EMS.CutStatus = (CutStatusEnum)(CANData[7] & 0x01);
        }

        private void OnRecvEMSStatusMsgB(EMSStatusMsgB msg)
        {
            EMSStatus EMS;
            var CANID = msg.UserData.Span[0];
            if (CANID >= 0x61 && CANID <= 0x60 + LCUNum)
            {
                var index = (byte)(CANID - 0x61);
                EMS = LCUStatusCollection[index];
            }
            else if (CANID >= 0x81 && CANID <= 0x80 + GCUNum)
            {
                var index = (byte)(CANID - 0x81);
                EMS = GCUStatusCollection[index];
            }
            else
            {
                // EMS状态帧ID不合法
                return;
            }
            var CANData = msg.UserData.Span.Slice(1, 8);
            EMS.GapSensor1Status = (GapSensorStatusEnum)(CANData[1] & 0x80);
            EMS.Gap1 = (float)((CANData[1] & 0x7F) * 0.157480);
            EMS.GapSensor2Status = (GapSensorStatusEnum)(CANData[2] & 0x80);
            EMS.Gap2 = (float)((CANData[2] & 0x7F) * 0.157480);
            EMS.GapSensor3Status = (GapSensorStatusEnum)(CANData[3] & 0x80);
            EMS.Gap3 = (float)((CANData[3] & 0x7F) * 0.157480);
            EMS.GapSensor4Status = (GapSensorStatusEnum)(CANData[4] & 0x80);
            EMS.Gap4 = (float)((CANData[4] & 0x7F) * 0.157480);
            EMS.AccSensor1Status = (AccSensorStatusEnum)(CANData[5] & 0x80);
            EMS.Acc1 = (float)((CANData[5] & 0x7F) * 0.7874016 - 50.0);
            EMS.AccSensor2Status = (AccSensorStatusEnum)(CANData[6] & 0x80);
            EMS.Acc2 = (float)((CANData[6] & 0x7F) * 0.7874016 - 50.0);
            EMS.I2 = (float)((CANData[7] & 0x7F) * 1.259842);
        }

        private void OnRecvVSPSStatusMsg(VSPSStatusMsg msg)
        {
            var data = msg.UserData.Span;

            VSPSInfoCollection.Life = BinaryPrimitives.ReadUInt16BigEndian(data[..2]);
            VSPSInfoCollection.Forward = data[3] == 0x01;
            VSPSInfoCollection.RelativePos = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(4, 2));
            VSPSInfoCollection.Speed = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(6, 2));

            //SysLogger.Info($"方向: {VSPSInfoCollection.Forward}");
            //SysLogger.Info($"相对位置: {VSPSInfoCollection.RelativePos}");
            //SysLogger.Info($"速度: {VSPSInfoCollection.Speed}");
        }

        private void OnRecvOBCStatusMsg(OBCMsg msg)
        {
            var data = msg.UserData.Span;

            var bits = new BitArray(data.Slice(0, 8).ToArray());

            FeedBackOBCControlField.IsRemoteModeActivate = bits[1]; // 本地/远程切换（0：本地 1：远程）
            FeedBackOBCControlField.IsEmergencyStop = bits[2]; // 急停（0：消失 1：发生）

            FeedBackOBCControlField.IsBatteryEnable = bits[3]; // 电池启动（0：消失 1：发生）
            FeedBackOBCControlField.IsPowerSwitchClose = bits[5]; // 电源合闸（0：消失 1：发生）
            FeedBackOBCControlField.IsPantographExtend = bits[9]; // 受流器伸（0：消失 1：发生）
            FeedBackOBCControlField.IsLeviateActivate = bits[11]; // 悬浮起浮（0：消失 1：发生）
            FeedBackOBCControlField.IsGuideActivate = bits[12]; // 导向起浮（0：消失 1：发生）

            FeedBackOBCControlField.IsEmergencyModeActivate = bits[16]; // 紧急模式切换（0：正常模式 1：紧急模式）

            //FeedBackOBCControlField.IsBatteryEnable = bits[19]; // 电池启动（0：消失 1：发生）
            //FeedBackOBCControlField.IsPowerSwitchClose = bits[21]; // 电源合闸（0：消失 1：发生）
            //FeedBackOBCControlField.IsPantographExtend = bits[23]; // 受流器伸（0：消失 1：发生）
            //FeedBackOBCControlField.IsLeviateActivate = bits[25]; // 悬浮起浮（0：消失 1：发生）
            //FeedBackOBCControlField.IsGuideActivate = bits[26]; // 导向起浮（0：消失 1：发生）

            OBCStatusField.Is440VBatterySwitchClosed = bits[49];
            OBCStatusField.Is480VPowerSwitchClosed = bits[51];
            OBCStatusField.IsDC330VCircuitBreakerEnabled = bits[53];
            OBCStatusField.Is25kWPowerFailed = bits[54];
            OBCStatusField.Is5kWPowerFailed = bits[55];
            OBCStatusField.IsPantographEnergized = bits[56];
            OBCStatusField.IsPantographExtended2 = bits[58];
            OBCStatusField.IsPantographExtended1 = bits[60];
            OBCStatusField.IsLeviated = bits[62];
            OBCStatusField.IsGuideEnabled = bits[63];

            OBCStatusField.Battery440VCapacity = BinaryPrimitives.ReadInt16BigEndian(
                data.Slice(8, 2)
            );
            OBCStatusField.Battery110VCapacity = BinaryPrimitives.ReadInt16BigEndian(
                data.Slice(10, 2)
            );

            FeedBackOBCControlField.BrakeLevel = data[^1];
        }

        #endregion

        public IPAddress LocalIpAddress { get; set; } = IPAddress.Parse("192.168.43.1");
        public int LocalPort { get; set; } = 6001;
        public IPAddress RemoteIpAddress { get; set; } = IPAddress.Parse("192.168.43.10");
        public int RemotePort { get; set; } = 8001;

        private readonly StateMachine<VCInterfaceState, VCInterfaceTrigger> _vcInterfaceSM;
        private readonly HighPrecisionTimer _EMSControlMsgSendTimer;
        private UdpMessageService<BaseMessage>? _udpMsgSevice;

        private readonly SequenceManager<ushort> _sequenceManager;

        public static byte LCUNum { get; } = 6;
        public static byte GCUNum { get; } = 6;
        public EMSStatus[] LCUStatusCollection { get; } =
            new EMSStatus[] { new(), new(), new(), new(), new(), new() };
        public EMSStatus[] GCUStatusCollection { get; } =
            new EMSStatus[] { new(), new(), new(), new(), new(), new() };

        public static byte VSPSNums { get; } = 1;
        public VSPSInfo VSPSInfoCollection { get; } = new();

        public EMSControl EMSControlField { get; set; } = new EMSControl();
        public OBCControl SendOBCControlField { get; set; } = new OBCControl();
        public OBCControl FeedBackOBCControlField { get; set; } = new OBCControl();
        public OBCStatus OBCStatusField { get; set; } = new OBCStatus();

        // 日志记录器
        private readonly ILogger SysLogger;
        private readonly ILogger RecvLogger;
        private readonly ILogger SendLogger;
    }
}
