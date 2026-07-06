using MOCS.Cores.MCU;
using MOCS.Cores.VCU;
using MOCS.Utils;
using NLog;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace MOCS.Forms
{
    public partial class MOCS_UI : Form
    {
        #region 私有字段（使用单例）
        private readonly MOCSStatus _mocsStatus = MOCSStatus.Instance;
        private readonly VSPSInfo _vspsInfo = VSPSInfo.Instance;
        private readonly OBCStatus _obcStatus = OBCStatus.Instance;
        private readonly EMSStatus _emsStatus = EMSStatus.Instance;

        // 通信接口（在构造函数中创建，在"开始编译"时启动）
        private MCUInterface? _mcuInterface;
        private VCUInterface? _vcuInterface;
        #endregion

        #region 构造函数
        public MOCS_UI()
        {
            InitializeComponent();

            // 创建通信接口实例
            InitCommunicationInterfaces();

            // 程序启动时，把主窗口自身注册到FormManager
            FormManager.RegisterOpenedForm(this);
            // 主窗口关闭时：关闭所有子窗口 + 终止程序
            this.FormClosing += (s, e) =>
            {
                FormManager.CloseAllForms();
            };
            // 绑定窗体生命周期事件
            this.Load += MOCS_UI_Load;
            this.FormClosed += MOCS_UI_FormClosed;
            // 初始化UI显示
            InitMOCSUI();
        }

        /// <summary>
        /// 初始化 MCUInterface 和 VCUInterface，配置 Logger 和 IP 地址
        /// </summary>
        private void InitCommunicationInterfaces()
        {
            var sysLogger = LogManager.GetLogger("DiagnosticLogger");
            var recvLogger = LogManager.GetLogger("ReceiveLogger");
            var sendLogger = LogManager.GetLogger("SendLogger");

            // 创建 MCU 接口（监听 6002 端口）
            _mcuInterface = new MCUInterface(sysLogger, recvLogger, sendLogger);
            // DebugTool 场景：绑定到 IPAddress.Any 以同时接收 127.0.0.1 和 192.168.43.x 的报文
            _mcuInterface.LocalIpAddress = IPAddress.Any;
            // DebugTool 场景：关闭自动发送（生命周期报文定时器），由用户手动触发发送
            _mcuInterface.AutoSendEnabled = false;

            // 创建 VCU 接口（监听 6001 端口）
            _vcuInterface = new VCUInterface(sysLogger, recvLogger, sendLogger);
            _vcuInterface.LocalIpAddress = IPAddress.Any;
            _vcuInterface.AutoSendEnabled = false;
        }
        #endregion

        #region 窗体生命周期事件
        private async void MOCS_UI_Load(object sender, EventArgs e)
        {
            // 注册 MOCS 节点并绑定"接收/发送"报文 RTB
            // MOCS 节点承担了"中心路由"角色：
            //   MOCSRecvMsg 显示所有"由 MOCS 接收"的报文（VCU/MCU → MOCS）
            //   MOCSSendMsg 显示所有"由 MOCS 发送"的报文（MOCS → VCU/MCU）
            MessageMonitor.Instance.RegisterNode("MOCS");
            MessageMonitor.Instance.BindRecvRTB("MOCS", MOCSRecvMsg);
            MessageMonitor.Instance.BindSendRTB("MOCS", MOCSSendMsg);

            // 订阅所有子系统的属性变更事件
            _mocsStatus.PropertyChanged += MocsStatus_PropertyChanged;
            _vspsInfo.PropertyChanged += VspsInfo_PropertyChanged;
            _obcStatus.PropertyChanged += ObcStatus_PropertyChanged;
            _emsStatus.PropertyChanged += EmsStatus_PropertyChanged;

            // 窗口加载后自动启动通信（绑定 UDP 端口监听）
            await StartCommunicationAsync();
        }

        private async void MOCS_UI_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 取消订阅，防止内存泄漏
            _mocsStatus.PropertyChanged -= MocsStatus_PropertyChanged;
            _vspsInfo.PropertyChanged -= VspsInfo_PropertyChanged;
            _obcStatus.PropertyChanged -= ObcStatus_PropertyChanged;
            _emsStatus.PropertyChanged -= EmsStatus_PropertyChanged;

            // 解除 MOCS 节点 RTB 绑定
            MessageMonitor.Instance.UnbindRecvRTB("MOCS");
            MessageMonitor.Instance.UnbindSendRTB("MOCS");

            // 停止通信并释放资源
            await StopCommunicationAsync();
        }
        #endregion

        #region 通信控制（"开始编译"/"停止编译"）
        /// <summary>
        /// 启动所有通信接口，开始监听 UDP 端口
        /// </summary>
        public async Task StartCommunicationAsync()
        {
            if (_mcuInterface != null)
            {
                // MCUInterface 通过状态机 Activate 触发 BeginCommunicate
                // FireActivate 内部已检查 CanFireActivate
                await _mcuInterface.FireActivate();
            }
            if (_vcuInterface != null)
            {
                await _vcuInterface.StartAsync();
            }
        }

        /// <summary>
        /// 停止所有通信接口
        /// </summary>
        public async Task StopCommunicationAsync()
        {
            if (_vcuInterface != null)
            {
                await _vcuInterface.StopAsync();
            }
            // MCUInterface 通过状态机 Deactivate 触发停止
            if (_mcuInterface != null)
            {
                await _mcuInterface.FireDeactivate();
            }
        }
        #endregion

        #region 初始化
        private void InitMOCSUI()
        {
            // MOCS 自身状态
            UpdateCurrentMaglevVehicleIdentifierUI();
            UpdateMaglevVehicleLeviCommandStatusUI();
            UpdateMaglevVehicleLeviStatusUI();
            UpdateExpectedSpeedTypeUI();
            UpdateExpectedSpeedUI();
            UpdateExpectedRunningDircetionUI();
            UpdateMOCSCh1StatusUI();
            UpdateMOCSCh2StatusUI();
            UpdateMCUStatusMsgRecvUI();
            UpdateMCUSendReasonUI();

            // VSPS 数据
            UpdateVSPSLifeUI();
            UpdateVSPSForwardUI();
            UpdateVSPSRelativePosUI();
            UpdateVSPSSpeedUI();
            UpdateVehiclePosUI();
            UpdateVehicleVelocityUI();

            // OBC 数据
            Update440VBatterySwitchUI();
            Update480VPowerSwitchUI();
            UpdateBattery440VCapacityUI();
            UpdateBattery110VCapacityUI();
            UpdateLeviatedUI();
            UpdateGuideEnabledUI();
            UpdatePantographEnergizedUI();
            Update25kWPowerFailedUI();
            Update5kWPowerFailedUI();

            // EMS 数据 (LCU1 和 GCU1 共用同一个实例，因为是同一类型的子系统)
            UpdateLCU1EMSFaultStatusUI();
            UpdateLCU1EMSWarningUI();
            UpdateLCU1EMSOperationStatusUI();
            UpdateLCU1OverloadStatusUI();
            UpdateLCU1GapUI();
            UpdateLCU1AccUI();
            UpdateLCU1UUI();
            UpdateLCU1I1UI();
            UpdateGCU1EMSFaultStatusUI();
            UpdateGCU1EMSWarningUI();
            UpdateGCU1EMSOperationStatusUI();
            UpdateGCU1OverloadStatusUI();
            UpdateGCU1GapUI();
            UpdateGCU1AccUI();
            UpdateGCU1UUI();
            UpdateGCU1I1UI();
        }
        #endregion

        #region 属性变更分发 - MOCSStatus
        private void MocsStatus_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MOCSStatus.CurrentMaglevVehicleIdentifier):
                    UpdateCurrentMaglevVehicleIdentifierUI();
                    break;
                case nameof(MOCSStatus.MaglevVehicleLeviCommandStatus):
                    UpdateMaglevVehicleLeviCommandStatusUI();
                    break;
                case nameof(MOCSStatus.MaglevVehicleLeviStatus):
                    UpdateMaglevVehicleLeviStatusUI();
                    break;
                case nameof(MOCSStatus.ExpectedSpeedType):
                    UpdateExpectedSpeedTypeUI();
                    break;
                case nameof(MOCSStatus.ExpectedSpeed):
                    UpdateExpectedSpeedUI();
                    break;
                case nameof(MOCSStatus.ExpectedRunningDircetion):
                    UpdateExpectedRunningDircetionUI();
                    break;
                case nameof(MOCSStatus.Channel1RecvStatus):
                    UpdateMOCSCh1StatusUI();
                    break;
                case nameof(MOCSStatus.Channel2RecvStatus):
                    UpdateMOCSCh2StatusUI();
                    break;
                case nameof(MOCSStatus.MCUStatusMessageRecvStatus):
                    UpdateMCUStatusMsgRecvUI();
                    break;
                case nameof(MOCSStatus.RequestForMCUStatusChangeReady):
                    UpdateMCUSendReasonUI();
                    break;
            }
        }
        #endregion

        #region 属性变更分发 - VSPSInfo
        private void VspsInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(VSPSInfo.Life):
                    UpdateVSPSLifeUI();
                    break;
                case nameof(VSPSInfo.Forward):
                    UpdateVSPSForwardUI();
                    break;
                case nameof(VSPSInfo.RelativePos):
                    UpdateVSPSRelativePosUI();
                    UpdateVehiclePosUI();
                    break;
                case nameof(VSPSInfo.Speed):
                    UpdateVSPSSpeedUI();
                    UpdateVehicleVelocityUI();
                    break;
            }
        }
        #endregion

        #region 属性变更分发 - OBCStatus
        private void ObcStatus_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OBCStatus.Is440VBatterySwitchClosed):
                    Update440VBatterySwitchUI();
                    break;
                case nameof(OBCStatus.Is480VPowerSwitchClosed):
                    Update480VPowerSwitchUI();
                    break;
                case nameof(OBCStatus.Battery440VCapacity):
                    UpdateBattery440VCapacityUI();
                    break;
                case nameof(OBCStatus.Battery110VCapacity):
                    UpdateBattery110VCapacityUI();
                    break;
                case nameof(OBCStatus.IsLeviated):
                    UpdateLeviatedUI();
                    break;
                case nameof(OBCStatus.IsGuideEnabled):
                    UpdateGuideEnabledUI();
                    break;
                case nameof(OBCStatus.IsPantographEnergized):
                    UpdatePantographEnergizedUI();
                    break;
                case nameof(OBCStatus.Is25kWPowerFailed):
                    Update25kWPowerFailedUI();
                    break;
                case nameof(OBCStatus.Is5kWPowerFailed):
                    Update5kWPowerFailedUI();
                    break;
            }
        }
        #endregion

        #region 属性变更分发 - EMSStatus
        private void EmsStatus_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(EMSStatus.EMSFaultStatus):
                    UpdateLCU1EMSFaultStatusUI();
                    UpdateGCU1EMSFaultStatusUI();
                    break;
                case nameof(EMSStatus.EMSWarning):
                    UpdateLCU1EMSWarningUI();
                    UpdateGCU1EMSWarningUI();
                    break;
                case nameof(EMSStatus.EMSOperationStatus):
                    UpdateLCU1EMSOperationStatusUI();
                    UpdateGCU1EMSOperationStatusUI();
                    break;
                case nameof(EMSStatus.OverloadStatus):
                    UpdateLCU1OverloadStatusUI();
                    UpdateGCU1OverloadStatusUI();
                    break;
                case nameof(EMSStatus.Gap):
                    UpdateLCU1GapUI();
                    UpdateGCU1GapUI();
                    break;
                case nameof(EMSStatus.Acc):
                    UpdateLCU1AccUI();
                    UpdateGCU1AccUI();
                    break;
                case nameof(EMSStatus.U):
                    UpdateLCU1UUI();
                    UpdateGCU1UUI();
                    break;
                case nameof(EMSStatus.I1):
                    UpdateLCU1I1UI();
                    UpdateGCU1I1UI();
                    break;
            }
        }
        #endregion

        #region 通用UI更新工具方法
        /// <summary>
        /// 线程安全更新Label文本
        /// </summary>
        private void UpdateLabel(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(() => label.Text = text));
            }
            else
            {
                label.Text = text;
            }
        }

        /// <summary>
        /// 线程安全更新Panel颜色（True=绿, False=红）
        /// </summary>
        private void UpdatePanel(Panel panel, bool isTrue)
        {
            if (panel.InvokeRequired)
            {
                panel.Invoke(new Action(() => panel.BackColor = isTrue ? Color.LimeGreen : Color.Red));
            }
            else
            {
                panel.BackColor = isTrue ? Color.LimeGreen : Color.Red;
            }
        }

        /// <summary>
        /// 线程安全更新Panel颜色（True=红, False=绿），用于故障指示
        /// </summary>
        private void UpdatePanelReverse(Panel panel, bool isTrue)
        {
            UpdatePanel(panel, !isTrue);
        }

        /// <summary>
        /// 枚举状态转换为颜色（Normal=绿, 其他=红）
        /// </summary>
        private void UpdatePanelByEnumStatus(Panel panel, Enum status)
        {
            bool isNormal = status.ToString() == "Normal" || status.ToString() == "DeEnergize";
            UpdatePanel(panel, isNormal);
        }
        #endregion

        #region UI更新方法 - 基本信息（MOCSStatus）
        private void UpdateCurrentMaglevVehicleIdentifierUI()
        {
            UpdateLabel(lblCurrentMaglevVehicleIdentifier, _mocsStatus.CurrentMaglevVehicleIdentifier.ToString());
        }

        private void UpdateMaglevVehicleLeviCommandStatusUI()
        {
            bool isActive = _mocsStatus.MaglevVehicleLeviCommandStatus.ToString().Contains("Active");
            UpdatePanel(pnlMaglevVehicleLeviCommandStatus, isActive);
        }

        private void UpdateMaglevVehicleLeviStatusUI()
        {
            bool isLeviated = _mocsStatus.MaglevVehicleLeviStatus.ToString().Contains("Leviated");
            UpdatePanel(pnlMaglevVehicleLeviStatus, isLeviated);
        }

        private void UpdateExpectedSpeedTypeUI()
        {
            UpdateLabel(lblExpectedSpeedType, _mocsStatus.ExpectedSpeedType.ToString());
        }

        private void UpdateExpectedSpeedUI()
        {
            UpdateLabel(lblExpectedSpeed, $"{_mocsStatus.ExpectedSpeed} cm/s");
        }

        private void UpdateExpectedRunningDircetionUI()
        {
            UpdateLabel(lblExpectedRunningDircetion, _mocsStatus.ExpectedRunningDircetion.ToString());
        }
        #endregion

        #region UI更新方法 - 通讯状态（MOCSStatus）
        private void UpdateMOCSCh1StatusUI()
        {
            bool isNormal = _mocsStatus.Channel1RecvStatus.ToString() == "Normal";
            UpdatePanel(pnlMOCSCh1Status, isNormal);
        }

        private void UpdateMOCSCh2StatusUI()
        {
            bool isNormal = _mocsStatus.Channel2RecvStatus.ToString() == "Normal";
            UpdatePanel(pnlMOCSCh2Status, isNormal);
        }

        private void UpdateMCUStatusMsgRecvUI()
        {
            bool isNormal = _mocsStatus.MCUStatusMessageRecvStatus.ToString() == "Normal";
            UpdatePanel(pnlMCUStatusMsgRecv, isNormal);
        }

        private void UpdateMCUSendReasonUI()
        {
            UpdateLabel(lblMCUSendReason, _mocsStatus.RequestForMCUStatusChangeReady.ToString());
        }
        #endregion

        #region UI更新方法 - 悬浮架状态（复用 VSPS 数据）
        private void UpdateVehiclePosUI()
        {
            UpdateLabel(lblCurrentMaglevVehiclePos, $"{_vspsInfo.RelativePos} mm");
        }

        private void UpdateVehicleVelocityUI()
        {
            UpdateLabel(lblCurrentMaglevVehicleVelocity, $"{_vspsInfo.Speed} cm/s");
        }

        // 悬浮架加速度和牵引能力暂无数据源，初始化为 --
        private void UpdateVehicleAccUI()
        {
            UpdateLabel(lblCurrentMaglevVehicleAcc, "-- cm/s²");
        }

        private void UpdateTractionCapacityUI()
        {
            UpdateLabel(lblTractionCapacity, "-- %");
        }
        #endregion

        #region UI更新方法 - VSPS 数据
        private void UpdateVSPSLifeUI()
        {
            UpdateLabel(lblVSPSLife, _vspsInfo.Life.ToString());
        }

        private void UpdateVSPSForwardUI()
        {
            UpdateLabel(lblVSPSForward, _vspsInfo.Forward ? "正向" : "反向");
        }

        private void UpdateVSPSRelativePosUI()
        {
            UpdateLabel(lblVSPSRelativePos, $"{_vspsInfo.RelativePos} mm");
        }

        private void UpdateVSPSSpeedUI()
        {
            UpdateLabel(lblVSPSSpeed, $"{_vspsInfo.Speed} cm/s");
        }
        #endregion

        #region UI更新方法 - 电源与电池（OBCStatus）
        private void Update440VBatterySwitchUI()
        {
            UpdatePanel(pnlIs440VBatterySwitchClosed, _obcStatus.Is440VBatterySwitchClosed);
        }

        private void Update480VPowerSwitchUI()
        {
            UpdatePanel(pnlIs480VPowerSwitchClosed, _obcStatus.Is480VPowerSwitchClosed);
        }

        private void UpdateBattery440VCapacityUI()
        {
            UpdateLabel(lblBattery440VCapacity, $"{_obcStatus.Battery440VCapacity:F1}%");
        }

        private void UpdateBattery110VCapacityUI()
        {
            UpdateLabel(lblBattery110VCapacity, $"{_obcStatus.Battery110VCapacity:F1}%");
        }

        private void Update25kWPowerFailedUI()
        {
            UpdatePanelReverse(pnlIs25kWPowerFailed, _obcStatus.Is25kWPowerFailed);
        }

        private void Update5kWPowerFailedUI()
        {
            UpdatePanelReverse(pnlIs5kWPowerFailed, _obcStatus.Is5kWPowerFailed);
        }
        #endregion

        #region UI更新方法 - 受流器（OBCStatus）
        private void UpdatePantographEnergizedUI()
        {
            UpdatePanel(pnlIsPantographEnergized, _obcStatus.IsPantographEnergized);
        }

        private void UpdateLeviatedUI()
        {
            UpdatePanel(pnlIsLeviated, _obcStatus.IsLeviated);
        }

        private void UpdateGuideEnabledUI()
        {
            UpdatePanel(pnlIsGuideEnabled, _obcStatus.IsGuideEnabled);
        }
        #endregion

        #region UI更新方法 - LCU1 状态（EMSStatus）
        private void UpdateLCU1EMSFaultStatusUI()
        {
            bool isNormal = _emsStatus.EMSFaultStatus.ToString() == "Normal";
            UpdatePanel(pnlLCU1EMSFaultStatus, isNormal);
        }

        private void UpdateLCU1EMSWarningUI()
        {
            bool isNormal = _emsStatus.EMSWarning.ToString() == "Normal";
            UpdatePanel(pnlLCU1EMSWarning, isNormal);
        }

        private void UpdateLCU1EMSOperationStatusUI()
        {
            bool isEnergized = _emsStatus.EMSOperationStatus.ToString() == "Energized";
            UpdatePanel(pnlLCU1EMSOperationStatus, isEnergized);
        }

        private void UpdateLCU1OverloadStatusUI()
        {
            bool isActivated = _emsStatus.OverloadStatus.ToString() == "Activate";
            UpdatePanel(pnlLCU1OverloadStatus, isActivated);
        }

        private void UpdateLCU1GapUI()
        {
            UpdateLabel(lblLCU1Gap, $"{_emsStatus.Gap:F2} mm");
        }

        private void UpdateLCU1AccUI()
        {
            UpdateLabel(lblLCU1Acc, $"{_emsStatus.Acc:F2} g");
        }

        private void UpdateLCU1UUI()
        {
            UpdateLabel(lblLCU1U, $"{_emsStatus.U:F1} V");
        }

        private void UpdateLCU1I1UI()
        {
            UpdateLabel(lblLCU1I1, $"{_emsStatus.I1:F1} A");
        }
        #endregion

        #region UI更新方法 - GCU1 状态（EMSStatus，共用同一数据）
        private void UpdateGCU1EMSFaultStatusUI()
        {
            bool isNormal = _emsStatus.EMSFaultStatus.ToString() == "Normal";
            UpdatePanel(pnlGCU1EMSFaultStatus, isNormal);
        }

        private void UpdateGCU1EMSWarningUI()
        {
            bool isNormal = _emsStatus.EMSWarning.ToString() == "Normal";
            UpdatePanel(pnlGCU1EMSWarning, isNormal);
        }

        private void UpdateGCU1EMSOperationStatusUI()
        {
            bool isEnergized = _emsStatus.EMSOperationStatus.ToString() == "Energized";
            UpdatePanel(pnlGCU1EMSOperationStatus, isEnergized);
        }

        private void UpdateGCU1OverloadStatusUI()
        {
            bool isActivated = _emsStatus.OverloadStatus.ToString() == "Activate";
            UpdatePanel(pnlGCU1OverloadStatus, isActivated);
        }

        private void UpdateGCU1GapUI()
        {
            UpdateLabel(lblGCU1Gap, $"{_emsStatus.Gap:F2} mm");
        }

        private void UpdateGCU1AccUI()
        {
            UpdateLabel(lblGCU1Acc, $"{_emsStatus.Acc:F2} g");
        }

        private void UpdateGCU1UUI()
        {
            UpdateLabel(lblGCU1U, $"{_emsStatus.U:F1} V");
        }

        private void UpdateGCU1I1UI()
        {
            UpdateLabel(lblGCU1I1, $"{_emsStatus.I1:F1} A");
        }
        #endregion

        #region 菜单栏点击事件
        private void mOCSToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<MOCS_UI>();
        private void mCUToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<MCU_UI>();
        private void vSPSToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<VSPS_UI>();
        private void lCUToolStripMenuItem1_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<LCU_UI>();
        private void lCUToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<GCU_UI>();
        private void oBCToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<OBC_UI>();
        #endregion
    }
}
