using System;
using System.Buffers.Binary;
using System.Net;
using MOCS.Coms;
using MOCS.Protocals;
using MOCS.Protocals.Propulsion.MCUToMOCS;
using MOCS.Protocals.Propulsion.MOCSToMCU;
using MOCS.Protocals.VehicleControl.VehicleToMOCS;
using MOCS.Utils;
using NLog;
using Stateless;

namespace MOCS.Cores.MCU
{
    public class MCUInterface
    {
        public bool IsLifeCycleRecTimeOutBeyondLimits { get; set; } = false;

        #region 面向界面层的公共接口

        public void RunPBSCommand()
        {
            _mcuInterfaceSM.Fire(_changeMCUStateTrigger, MCUStateChangeCommand.PBS);
        }

        public void RunPSIMCommand()
        {
            _mcuInterfaceSM.Fire(_changeMCUStateTrigger, MCUStateChangeCommand.PSIM);
        }

        public void RunPTPCommand()
        {
            _mcuInterfaceSM.Fire(_changeMCUStateTrigger, MCUStateChangeCommand.PTP);
        }

        public void RunPWSPCommand()
        {
            _mcuInterfaceSM.Fire(_changeMCUStateTrigger, MCUStateChangeCommand.PWSP);
        }

        public void RunPTOCommand()
        {
            _mcuInterfaceSM.Fire(_changeMCUStateTrigger, MCUStateChangeCommand.PTO);
        }

        public void RunPPTCommand()
        {
            _mcuInterfaceSM.Fire(_changeMCUStateTrigger, MCUStateChangeCommand.PPT);
        }

        public void LoginMaglevFrame() { }

        public void SignOutMaglevFrame() { }

        public void TransmitTrackData() { }

        public void DeleteTrackData() { }

        public void TransmitMaximumCurve() { }

        public void DeleteMaximumCurve() { }

        #endregion 面向界面层的公共接口

        public MCUInterface(ILogger syslogger, ILogger recvlogger, ILogger sendlogger)
        {
            SysLogger = syslogger;
            RecvLogger = recvlogger;
            SendLogger = sendlogger;

            _mcuInterfaceSM = new StateMachine<MCUInterfaceState, MCUInterfaceTrigger>(
                MCUInterfaceState.Stop
            );
            _mcuStateChangeMonitorSM = new StateMachine<
                MCUStateChangeMonitorState,
                MCUStateChangeMonitorTrigger
            >(MCUStateChangeMonitorState.Stop);
            _changeMCUStateTrigger = _mcuInterfaceSM.SetTriggerParameters<MCUStateChangeCommand>(
                MCUInterfaceTrigger.ChangeMCUState
            );
            _mcuLifeCycleSendTimer = new HighPrecisionTimer(
                new TimeSpan(0, 0, 3),
                MCULifeCycleSendTimeOut
            );
            _mcuLifeCycleRecvTimer = new HighPrecisionTimer(
                new TimeSpan(0, 0, 5),
                MCULifeCycleRecvTimeOut
            );

            _sequenceManager = new SequenceManager<ushort>(ushort.MaxValue);

            _mcuDesState = MCUInterfaceState.Initial;
            ConfigMCUInterfaceStateMachine();
            ConfigPCSMonitorStateMachine();
        }

        #region 私有配置方法
        private void ConfigMCUInterfaceStateMachine()
        {
            _changeMCUStateTrigger = _mcuInterfaceSM.SetTriggerParameters<MCUStateChangeCommand>(
                MCUInterfaceTrigger.ChangeMCUState
            );

            // -----------停止状态-------------
            // 进入时，初始化和接口有关的所有状态
            // 启动事件触发时，转移至“未连接状态”
            // 离开时，启动通信
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.Stop)
                .OnEntry(InitStatus)
                .Permit(MCUInterfaceTrigger.Activate, MCUInterfaceState.UnConnected)
                .OnExit(BeginCommunicate);

            // -----------未连接状态-------------
            // 进入时，发送MOCS状态报文
            // 生命周期报文发送事件触发时，发送MOCS状态报文
            // 生命周期报文接收事件触发时，转移至“连接状态”
            // 关闭事件触发时，转移至“关闭状态”
            // 离开时，如果目标状态为“关闭状态”，则停止通信
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.UnConnected)
                .OnEntryAsync(SendMOCSStatusMsgAsync)
                .InternalTransitionAsync(
                    MCUInterfaceTrigger.MOCSLifeCycleMsgSend,
                    SendMOCSStatusMsgAsync
                )
                .Permit(MCUInterfaceTrigger.MCULifeCycleMsgRecvd, MCUInterfaceState.Connected)
                .Permit(MCUInterfaceTrigger.Deactivate, MCUInterfaceState.Stop)
                .OnExitAsync(async t =>
                {
                    if (t.Destination == MCUInterfaceState.Stop)
                    {
                        await StopCommunicate();
                    }
                });

            // -----------连接状态-------------
            // 进入时，启动生命周期报文接收超时定时器
            // 自动转移至子状态“未知状态”
            // 生命周期报文发送事件触发时，发送MOCS状态报文
            // 生命周期报文接收超时事件触发时，判断是否超出上限
            // 如果为真，则转移至“未连接状态”
            // 关闭事件触发时，转移至“停止状态”
            // 离开时，关闭生命周期报文接收超时定时器
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.Connected)
                .OnEntry(t => ConfigureTimer(true, _mcuLifeCycleRecvTimer))
                .InitialTransition(MCUInterfaceState.UnKnown)
                .InternalTransitionAsync(
                    MCUInterfaceTrigger.MOCSLifeCycleMsgSend,
                    SendMOCSStatusMsgAsync
                )
                .PermitIf(
                    MCUInterfaceTrigger.MCULifeCycleMsgRecvTimeOut,
                    MCUInterfaceState.UnConnected,
                    () => IsLifeCycleRecTimeOutBeyondLimits
                )
                .Permit(MCUInterfaceTrigger.Deactivate, MCUInterfaceState.Stop)
                .OnExitAsync(async t =>
                {
                    if (t.Destination == MCUInterfaceState.Stop)
                    {
                        await StopCommunicate();
                    }
                });

            // -----------连接状态-------------
            // 配置为“连接状态”的子状态
            // 进入时，启动牵引系统状态转换监视
            // 牵引系统状态转换完成事件触发时，转移至“初始状态”
            // 离开时，关闭牵引系统状态转换监视
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.UnKnown)
                .SubstateOf(MCUInterfaceState.Connected)
                .OnEntry(ActivateMCUStateChangeMonitior)
                .Permit(MCUInterfaceTrigger.MCUStateHasChanged, MCUInterfaceState.Initial)
                .OnExit(DeactivateMCUStateChangeMonitor);

            // -----------初始状态-------------
            // 配置为“连接状态”的子状态
            // 牵引系统状态转换事件触发时，响应相应的转换命令
            // 牵引系统状态转换完成事件触发时，转移至“基本状态”
            // 离开时，关闭牵引系统状态转换监视
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.Initial)
                .SubstateOf(MCUInterfaceState.Connected)
                .InternalTransition(
                    _changeMCUStateTrigger,
                    (command, t) => ChangeMCUInitialState(command)
                )
                .Permit(MCUInterfaceTrigger.MCUStateHasChanged, MCUInterfaceState.Basic)
                .OnExit(DeactivateMCUStateChangeMonitor);

            // -----------基本状态-------------
            // 配置为“连接状态”的子状态
            // 牵引系统状态转换事件触发时，响应相应的转换命令
            // 牵引系统状态转换完成事件触发时，转移至与转换命令对应的状态
            // 离开时，关闭牵引系统状态转换监视
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.Basic)
                .SubstateOf(MCUInterfaceState.Connected)
                .InternalTransition(
                    _changeMCUStateTrigger,
                    (command, t) => ChangeMCUBasicState(command)
                )
                .PermitDynamic(MCUInterfaceTrigger.MCUStateHasChanged, () => _mcuDesState)
                .OnExit(DeactivateMCUStateChangeMonitor);

            // -----------带电等待状态-------------
            // 配置为“连接状态”的子状态
            // 牵引系统状态转换事件触发时，响应相应的转换命令
            // 牵引系统状态转换完成事件触发时，转移至与转换命令对应的状态
            // 离开时，关闭牵引系统状态转换监视
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.HotStandby)
                .SubstateOf(MCUInterfaceState.Connected)
                .InternalTransition(
                    _changeMCUStateTrigger,
                    (command, t) => ChangeMCUStandbyWithChargedState(command)
                )
                .PermitDynamic(MCUInterfaceTrigger.MCUStateHasChanged, () => _mcuDesState)
                .OnExit(DeactivateMCUStateChangeMonitor);

            // -----------准备测试状态-------------
            // 配置为“连接状态”的子状态
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.ReadyForTest)
                .SubstateOf(MCUInterfaceState.Connected);

            // -----------带电牵引测试状态-------------
            // 配置为“连接状态”的子状态
            // 牵引系统状态转换事件触发时，开始状态转移
            // 牵引系统状态转换完成事件触发时，转移至“基本状态”
            // 离开时，关闭牵引系统状态转换监视
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.HotTractionTest)
                .SubstateOf(MCUInterfaceState.Connected)
                .InternalTransition(
                    MCUInterfaceTrigger.ChangeMCUState,
                    ChangeMCUPropulsionTestWithChargedState
                )
                .Permit(MCUInterfaceTrigger.MCUStateHasChanged, MCUInterfaceState.Basic)
                .OnExit(DeactivateMCUStateChangeMonitor);

            // -----------不带电牵引测试状态-------------
            // 配置为“连接状态”的子状态
            // 牵引系统状态转换事件触发时，开始状态转移
            // 牵引系统状态转换完成事件触发时，转移至“基本状态”
            // 离开时，关闭牵引系统状态转换监视
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.DeadTractionTest)
                .SubstateOf(MCUInterfaceState.Connected)
                .InternalTransition(
                    MCUInterfaceTrigger.ChangeMCUState,
                    ChangeMCUPropulsionTestWithNoChargedState
                )
                .Permit(MCUInterfaceTrigger.MCUStateHasChanged, MCUInterfaceState.Basic)
                .OnExit(DeactivateMCUStateChangeMonitor);

            // -----------模拟运行状态-------------
            // 配置为“连接状态”的子状态
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.SimulatedRunning)
                .SubstateOf(MCUInterfaceState.Connected);
            //.InternalTransition(MCUInterfaceTrigger.ChangeMCUState, changeMCUSimulatedRunningState)
            //.OnExit(DeactivateMCUStateChangeMonitor);

            // -----------悬浮架运行状态-------------
            // 配置为“连接状态”的子状态
            _mcuInterfaceSM
                .Configure(MCUInterfaceState.MaglevFrameRunning)
                .SubstateOf(MCUInterfaceState.Connected)
                .InternalTransitionAsync(
                    MCUInterfaceTrigger.MaglevVehicleLoginIn,
                    SendLogInMaglevVehicleMsgAsync
                )
                .InternalTransitionAsync(
                    MCUInterfaceTrigger.MaglevVehicleSignOut,
                    SendLogOutMaglevVehicleMsgAsync
                )
                .InternalTransition(MCUInterfaceTrigger.TransmitTrackData, SendTransmitTrackDataMsg)
                .InternalTransitionAsync(
                    MCUInterfaceTrigger.DeleteTrackData,
                    SendRemoveTrackDataMsgAsync
                )
                .InternalTransition(
                    MCUInterfaceTrigger.TransmitMaximumCurve,
                    SendTransmitTrackDataMsg
                )
                .InternalTransition(
                    MCUInterfaceTrigger.DeleteMaximumCurve,
                    SendDeleteTranmitMaximumCurveMsg
                );

            // 注册状态转移期间调用的回调函数
            _mcuInterfaceSM.OnTransitioned(OnMCUInterfaceSMTransition);
        }

        private void ConfigPCSMonitorStateMachine()
        {
            _mcuStateChangeMonitorSM
                .Configure(MCUStateChangeMonitorState.Stop)
                .Permit(
                    MCUStateChangeMonitorTrigger.Activate,
                    MCUStateChangeMonitorState.Unchanged
                );

            _mcuStateChangeMonitorSM
                .Configure(MCUStateChangeMonitorState.Unchanged)
                .OnEntry(t => SendChangeMCUStateMsg(_mcuDesState))
                .Permit(MCUStateChangeMonitorTrigger.Executed, MCUStateChangeMonitorState.Changed);

            _mcuStateChangeMonitorSM
                .Configure(MCUStateChangeMonitorState.Changed)
                .OnEntry(OnMCUStateHasChanged);
        }

        private void ConfigureTimer(bool active, HighPrecisionTimer timer)
        {
            if (timer != null)
            {
                if (active)
                {
                    timer.Start();
                }
                else
                {
                    timer.Stop();
                }
            }
        }

        private void ConfigMsgParsers()
        {
            _udpMsgSevice?.RegisterParser(0x81, MCUStatusMsg.Parse);
            _udpMsgSevice?.RegisterParser(0x82, MCUReplyMsg.Parse);
        }

        private void ConfigMsgHandlers()
        {
            _udpMsgSevice?.Subscribe<MCUStatusMsg>(OnRecvMCUStatusMsg);
            _udpMsgSevice?.Subscribe<MCUReplyMsg>(OnRecvMCUReplyMsg);
        }

        #endregion

        private void InitStatus()
        {
            MCUStatusField.Reset();
            MCUReplyField.Reset();
        }

        //private void setDeleteMCUStateChangeAlreadyFlag(bool flag)
        //{
        //}

        private void OnMCUStateHasChanged()
        {
            //setDeleteMCUStateChangeAlreadyFlag(false);
            if (_mcuInterfaceSM.CanFire(MCUInterfaceTrigger.MCUStateHasChanged))
            {
                _mcuInterfaceSM.Fire(MCUInterfaceTrigger.MCUStateHasChanged);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Current MCUInterface State: {_mcuInterfaceSM.State.ToString()} "
                        + $"cannot deal with trigger: {MCUInterfaceTrigger.MCUStateHasChanged.ToString()}"
                );
            }
        }

        private void OnMCUInterfaceSMTransition(
            StateMachine<MCUInterfaceState, MCUInterfaceTrigger>.Transition transition
        )
        {
            // Debug状态下输出状态转移过程中的原状态、触发器、目标状态信息
#if DEBUG
            var source = transition.Source.ToString();
            var trigger = transition.Trigger.ToString();
            var destination = transition.Destination.ToString();
            var message = $"MCUInterface SM transition: {source} --({trigger})-> {destination}";
            System.Diagnostics.Debug.WriteLine(message);
#endif
        }

        #region 私有改变牵引状态方法

        private void ActivateMCUStateChangeMonitior()
        {
            _mcuStateChangeMonitorSM.Fire(MCUStateChangeMonitorTrigger.Activate);
        }

        private void DeactivateMCUStateChangeMonitor()
        {
            if (_mcuStateChangeMonitorSM.CanFire(MCUStateChangeMonitorTrigger.Deactivate))
            {
                _mcuStateChangeMonitorSM.Fire(MCUStateChangeMonitorTrigger.Deactivate);
            }
        }

        private void ChangeMCUInitialState(MCUStateChangeCommand command)
        {
            switch (command)
            {
                case MCUStateChangeCommand.PBS:
                    _mcuDesState = MCUInterfaceState.Basic;
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Current State is {_mcuInterfaceSM.State.ToString()}."
                            + $"Cannot process command: {command.ToString()} "
                    );
            }
            ActivateMCUStateChangeMonitior();
        }

        private void ChangeMCUBasicState(MCUStateChangeCommand command)
        {
            switch (command)
            {
                case MCUStateChangeCommand.PSIM:
                    _mcuDesState = MCUInterfaceState.SimulatedRunning;
                    break;

                case MCUStateChangeCommand.PT:
                    _mcuDesState = MCUInterfaceState.DeadTractionTest;
                    break;

                case MCUStateChangeCommand.PTP:
                    _mcuDesState = MCUInterfaceState.HotTractionTest;
                    break;

                case MCUStateChangeCommand.PWSP:
                    _mcuDesState = MCUInterfaceState.HotStandby;
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Current State is {_mcuInterfaceSM.State.ToString()}."
                            + $"Cannot process command: {command.ToString()} "
                    );
            }
            ActivateMCUStateChangeMonitior();
        }

        private void ChangeMCUStandbyWithChargedState(MCUStateChangeCommand command)
        {
            switch (command)
            {
                case MCUStateChangeCommand.PTO:
                    _mcuDesState = MCUInterfaceState.MaglevFrameRunning;
                    break;

                case MCUStateChangeCommand.PPT:
                    _mcuDesState = MCUInterfaceState.ReadyForTest;
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Current State is {_mcuInterfaceSM.State.ToString()}."
                            + $"Cannot process command: {command.ToString()} "
                    );
            }
            ActivateMCUStateChangeMonitior();
        }

        //private void changeMCUSimulatedRunningState()
        //{
        //    _mcuDesState = MCUInterfaceState.Basic;
        //    ActivateMCUStateChangeMonitior();
        //}

        private void ChangeMCUPropulsionTestWithChargedState()
        {
            _mcuDesState = MCUInterfaceState.Basic;
            ActivateMCUStateChangeMonitior();
        }

        private void ChangeMCUPropulsionTestWithNoChargedState()
        {
            _mcuDesState = MCUInterfaceState.Basic;
            ActivateMCUStateChangeMonitior();
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
            _udpMsgSevice.StartListening();
            ConfigureTimer(true, _mcuLifeCycleSendTimer);
            SysLogger.Info($"开始监听端口: {LocalPort}");
        }

        private async Task StopCommunicate()
        {
            ConfigureTimer(false, _mcuLifeCycleSendTimer);
            if (_udpMsgSevice != null)
            {
                await _udpMsgSevice.DisposeAsync();
            }
            SysLogger.Info($"停止监听端口: {LocalPort}");
        }

        private void MCULifeCycleSendTimeOut()
        {
            _mcuInterfaceSM.Fire(MCUInterfaceTrigger.MOCSLifeCycleMsgSend);
        }

        private void MCULifeCycleRecvTimeOut()
        {
            _mcuLifeCycleRecvTimeOutCounts++;
            if (_mcuLifeCycleRecvTimeOutCounts >= 2)
            {
                IsLifeCycleRecTimeOutBeyondLimits = true;
                _mcuInterfaceSM.Fire(MCUInterfaceTrigger.MCULifeCycleMsgRecvTimeOut);
            }
        }

        #region 私有报文发送方法
        private async Task SendMOCSStatusMsgAsync()
        {
            var sequenceNum = _sequenceManager.GetNextSequenceNum(PacketCategory.B);
            MOCSStatusMsg msg = new()
            {
                SequenceNumber = sequenceNum,
                UserData = MOCSStatusField.ToByteArray(),
            };
            if (_udpMsgSevice != null)
            {
                await _udpMsgSevice.SendAsync(msg);
            }
        }

        private void SendChangeMCUStateMsg(MCUInterfaceState destinationState) { }

        private async Task SendLogInMaglevVehicleMsgAsync()
        {
            var sequenceNum = _sequenceManager.GetNextSequenceNum(PacketCategory.A);
            LogInMaglevVehicleMsg msg = new()
            {
                SequenceNumber = sequenceNum,
                UserData = MaglevVehicleLoginField.ToByteArray(),
            };
            if (_udpMsgSevice != null)
            {
                await _udpMsgSevice.SendAsync(msg);
            }
        }

        private async Task SendLogOutMaglevVehicleMsgAsync()
        {
            var sequenceNum = _sequenceManager.GetNextSequenceNum(PacketCategory.A);
            LogInMaglevVehicleMsg msg = new()
            {
                SequenceNumber = sequenceNum,
                UserData = MaglevVehicleLogOutField.ToByteArray(),
            };
            if (_udpMsgSevice != null)
            {
                await _udpMsgSevice.SendAsync(msg);
            }
        }

        private void SendTransmitTrackDataMsg() { }

        private async Task SendRemoveTrackDataMsgAsync()
        {
            var sequenceNum = _sequenceManager.GetNextSequenceNum(PacketCategory.A);
            RemoveTrackDataMsg msg = new()
            {
                SequenceNumber = sequenceNum,
                UserData = RemoveTrackDataField.ToByteArray(),
            };
            if (_udpMsgSevice != null)
            {
                await _udpMsgSevice.SendAsync(msg);
            }
        }

        private async Task SendRequestParkingPointStatusMsgAsync()
        {
            var sequenceNum = _sequenceManager.GetNextSequenceNum(PacketCategory.A);
            RequestParkingPointStatusMsg msg = new()
            {
                SequenceNumber = sequenceNum,
                UserData = RequestParkingPointStatusField.ToByteArray(),
            };
            if (_udpMsgSevice != null)
            {
                await _udpMsgSevice.SendAsync(msg);
            }
        }

        private async Task SendStepParkingPointMsgAsync()
        {
            var sequenceNum = _sequenceManager.GetNextSequenceNum(PacketCategory.A);
            StepParkingPointMsg msg = new()
            {
                SequenceNumber = sequenceNum,
                UserData = StepParkingPointField.ToByteArray(),
            };
            if (_udpMsgSevice != null)
            {
                await _udpMsgSevice.SendAsync(msg);
            }
        }

        private void SendTransmitMaximumCurveMsg() { }

        private void SendDeleteTranmitMaximumCurveMsg() { }
        #endregion

        #region 私有报文处理方法

        private void OnRecvMCUStatusMsg(MCUStatusMsg msg)
        {
            var data = msg.UserData.Span;

            MCUStatusField.Channel1RecvStatus = (ChannelRecvStatusEnum)(data[0] & 0x3F);
            MCUStatusField.Channel2RecvStatus = (ChannelRecvStatusEnum)(data[2] & 0x3F);
            MCUStatusField.MCUSendReason = (MCUSendReasonEnum)data[4];
            MCUStatusField.MsgNumToRepeat = data[5];
            MCUStatusField.A_SequenceNumRefPoint = BinaryPrimitives.ReadUInt16LittleEndian(
                data.Slice(6, 2)
            );
            MCUStatusField.MCUStatusChangeReadinessInfo = (MCUStatusChangeReadinessInfoEnum)(
                data[8] & 0x07
            );
            MCUStatusField.CurrentMaglevVehicleIdentifier = data[9];
            MCUStatusField.ClearCurrentMaglevVehicleReadinessInfo =
                (ClearMaglevVehicleReadinessInfoEnum)(data[10] & 0x03);
            MCUStatusField.DCSNum = data[11];
            MCUStatusField.CurrentMaglevVehiclePos = BinaryPrimitives.ReadInt32LittleEndian(
                data.Slice(12, 4)
            );
            MCUStatusField.CurrentMaglevVehicleVelocity = BinaryPrimitives.ReadInt16LittleEndian(
                data.Slice(16, 2)
            );
            MCUStatusField.CurrentMaglevVehicleAcc = BinaryPrimitives.ReadInt16LittleEndian(
                data.Slice(18, 2)
            );
            MCUStatusField.TractionCapacityForCurrentVehicle = data[20];
            MCUStatusField.CurrentMaglevVehicleSPRStatus = (CurrentMaglevVehicleSPRStatusEnum)(
                data[21] & 0x0F
            );
            MCUStatusField.VirtualMaglevVehicleIdentifier = data[22];
            MCUStatusField.ClearVirtualMaglevVehicleReadinessInfo =
                (ClearMaglevVehicleReadinessInfoEnum)(data[23] & 0x03);
            MCUStatusField.TractionCapacityForVirtualVehicle = data[24];
            MCUStatusField.MCUFaultStatus = (MCUFaultStatusEnum)data[25];
            MCUStatusField.ParkingPointsNum = data[26];
            // TODO: 对“当前”悬浮架的5个停车点运行的调试信息
            // TODO: 对“虚拟”悬浮架的5个停车点运行的调试信息
        }

        private void OnRecvMCUReplyMsg(MCUReplyMsg msg)
        {
            var data = msg.UserData.Span;

            MCUReplyField.Channel1RecvStatus = (ChannelRecvStatusEnum)(data[0] & 0x3F);
            MCUReplyField.Channel2RecvStatus = (ChannelRecvStatusEnum)(data[2] & 0x3F);
            MCUReplyField.MCUReplyErrorIdentifier = (MCUReplyErrorIdentifierEnum)data[4];
            MCUReplyField.MsgNumToRepeat = data[5];
            MCUReplyField.MCUProcessStatus = (MCUProcessStatusEnum)data[6];
            MCUReplyField.ResponseMsgId = data[7];
            // TODO: 请求相关的其他数据
        }

        #endregion

        public IPAddress LocalIpAddress { get; set; } = IPAddress.Parse("192.168.43.2");
        public int LocalPort { get; set; } = 6002;
        public IPAddress RemoteIpAddress { get; set; } = IPAddress.Parse("192.168.43.5");
        public int RemotePort { get; set; } = 8008;

        private readonly StateMachine<MCUInterfaceState, MCUInterfaceTrigger> _mcuInterfaceSM;
        private readonly StateMachine<
            MCUStateChangeMonitorState,
            MCUStateChangeMonitorTrigger
        > _mcuStateChangeMonitorSM;

        private StateMachine<
            MCUInterfaceState,
            MCUInterfaceTrigger
        >.TriggerWithParameters<MCUStateChangeCommand> _changeMCUStateTrigger;
        private MCUInterfaceState _mcuDesState;

        private readonly HighPrecisionTimer _mcuLifeCycleSendTimer;
        private readonly HighPrecisionTimer _mcuLifeCycleRecvTimer;
        private int _mcuLifeCycleRecvTimeOutCounts = 0;

        private UdpMessageService<BaseMessage>? _udpMsgSevice;

        private SequenceManager<ushort> _sequenceManager;

        public MOCSStatus MOCSStatusField { get; set; } = new MOCSStatus();
        public LogInMaglevVehicle MaglevVehicleLoginField { get; set; } = new LogInMaglevVehicle();
        public LogOutMaglevVehicle MaglevVehicleLogOutField { get; set; } =
            new LogOutMaglevVehicle();
        public RemoveTrackData RemoveTrackDataField { get; set; } = new RemoveTrackData();
        public RequestParkingPointStatus RequestParkingPointStatusField { get; set; } =
            new RequestParkingPointStatus();

        public MCUStatus MCUStatusField { get; set; } = new MCUStatus();
        public MCUReply MCUReplyField { get; set; } = new MCUReply();
        public StepParkingPoint StepParkingPointField { get; set; } = new StepParkingPoint();

        // 日志记录器
        private readonly ILogger SysLogger;
        private readonly ILogger RecvLogger;
        private readonly ILogger SendLogger;
    }
}
