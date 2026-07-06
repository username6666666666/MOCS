using MOCS.Cores.MCU;
using MOCS.Utils;
using System.Windows.Forms;

namespace MOCS.Forms
{
    public partial class MCU_UI : Form
    {
        #region 私有字段（使用单例）
        private readonly MCUStatus _mcuStatus = MCUStatus.Instance;
        #endregion

        #region 构造函数与数据绑定
        public MCU_UI()
        {
            InitializeComponent();
            this.Load += MCU_UI_Load;
            this.FormClosing += MCU_UI_FormClosing;
        }

        private void MCU_UI_Load(object? sender, EventArgs e)
        {
            // 绑定 MCU 节点的收发报文 RTB（按方向分离显示）
            MessageMonitor.Instance.BindRecvRTB("MCU", MCURecvMsg);
            MessageMonitor.Instance.BindSendRTB("MCU", MCUSendMsg);

            // 订阅 MCUStatus 属性变化
            _mcuStatus.PropertyChanged += MCUStatus_PropertyChanged;

            // 初始化显示（全部状态为默认值）
            UpdateAllUI();
        }

        private void MCU_UI_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // 取消订阅，防止内存泄漏
            _mcuStatus.PropertyChanged -= MCUStatus_PropertyChanged;

            // 解除 MCU 节点 RTB 绑定
            MessageMonitor.Instance.UnbindRecvRTB("MCU");
            MessageMonitor.Instance.UnbindSendRTB("MCU");
        }

        private void MCUStatus_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is not MCUStatus mcuStatus) return;

            // UI 更新必须在主线程上执行
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateUI(e.PropertyName)));
            }
            else
            {
                UpdateUI(e.PropertyName);
            }
        }

        /// <summary>
        /// 根据属性名更新对应UI
        /// </summary>
        private void UpdateUI(string? propertyName)
        {
            switch (propertyName)
            {
                // 通讯状态
                case nameof(MCUStatus.Channel1RecvStatus):
                    UpdateChannelStatus(pnlCh1Status, _mcuStatus.Channel1RecvStatus);
                    break;
                case nameof(MCUStatus.Channel2RecvStatus):
                    UpdateChannelStatus(pnlCh2Status, _mcuStatus.Channel2RecvStatus);
                    break;
                case nameof(MCUStatus.MCUSendReason):
                    lblMCUSendReason.Text = _mcuStatus.MCUSendReason.ToString();
                    break;
                case nameof(MCUStatus.MsgNumToRepeat):
                    lblMsgNumToRepeat.Text = _mcuStatus.MsgNumToRepeat.ToString();
                    break;
                case nameof(MCUStatus.A_SequenceNumRefPoint):
                    lblSequenceNumRefPoint.Text = _mcuStatus.A_SequenceNumRefPoint.ToString();
                    break;
                case nameof(MCUStatus.MCUStatusChangeReadinessInfo):
                    lblMCUStatusChangeReadiness.Text = _mcuStatus.MCUStatusChangeReadinessInfo.ToString();
                    break;

                // 当前悬浮架
                case nameof(MCUStatus.CurrentMaglevVehicleIdentifier):
                    lblCurrentMaglevVehicleIdentifier.Text = _mcuStatus.CurrentMaglevVehicleIdentifier.ToString();
                    break;
                case nameof(MCUStatus.ClearCurrentMaglevVehicleReadinessInfo):
                    UpdateClearFlag(pnlClearCurrent, _mcuStatus.ClearCurrentMaglevVehicleReadinessInfo);
                    break;
                case nameof(MCUStatus.DCSNum):
                    lblDCSNum.Text = _mcuStatus.DCSNum.ToString();
                    break;
                case nameof(MCUStatus.CurrentMaglevVehiclePos):
                    lblCurrentMaglevVehiclePos.Text = $"{_mcuStatus.CurrentMaglevVehiclePos} cm";
                    break;
                case nameof(MCUStatus.CurrentMaglevVehicleVelocity):
                    lblCurrentMaglevVehicleVelocity.Text = $"{_mcuStatus.CurrentMaglevVehicleVelocity} cm/s";
                    break;
                case nameof(MCUStatus.CurrentMaglevVehicleAcc):
                    lblCurrentMaglevVehicleAcc.Text = $"{_mcuStatus.CurrentMaglevVehicleAcc} cm/s²";
                    break;
                case nameof(MCUStatus.TractionCapacityForCurrentVehicle):
                    lblTractionCapacityCurrent.Text = $"{_mcuStatus.TractionCapacityForCurrentVehicle}%";
                    break;
                case nameof(MCUStatus.CurrentMaglevVehicleSPRStatus):
                    lblCurrentMaglevVehicleSPRStatus.Text = _mcuStatus.CurrentMaglevVehicleSPRStatus.ToString();
                    break;

                // 虚拟悬浮架
                case nameof(MCUStatus.VirtualMaglevVehicleIdentifier):
                    lblVirtualMaglevVehicleIdentifier.Text = _mcuStatus.VirtualMaglevVehicleIdentifier.ToString();
                    break;
                case nameof(MCUStatus.ClearVirtualMaglevVehicleReadinessInfo):
                    UpdateClearFlag(pnlClearVirtual, _mcuStatus.ClearVirtualMaglevVehicleReadinessInfo);
                    break;
                case nameof(MCUStatus.TractionCapacityForVirtualVehicle):
                    lblTractionCapacityVirtual.Text = $"{_mcuStatus.TractionCapacityForVirtualVehicle}%";
                    break;

                // 故障与停车点
                case nameof(MCUStatus.MCUFaultStatus):
                    lblMCUFaultStatus.Text = _mcuStatus.MCUFaultStatus.ToString();
                    UpdateFaultStatus(_mcuStatus.MCUFaultStatus);
                    break;
                case nameof(MCUStatus.ParkingPointsNum):
                    lblParkingPointsNum.Text = _mcuStatus.ParkingPointsNum.ToString();
                    break;
                case nameof(MCUStatus.CurrentKIIdentifiers):
                    lblCurrentKIIdentifiers.Text = string.Join(",", _mcuStatus.CurrentKIIdentifiers);
                    break;
                case nameof(MCUStatus.VirtualKIIdentifiers):
                    lblVirtualKIIdentifiers.Text = string.Join(",", _mcuStatus.VirtualKIIdentifiers);
                    break;
            }
        }

        /// <summary>
        /// 更新所有UI
        /// </summary>
        private void UpdateAllUI()
        {
            // 通讯状态
            UpdateChannelStatus(pnlCh1Status, _mcuStatus.Channel1RecvStatus);
            UpdateChannelStatus(pnlCh2Status, _mcuStatus.Channel2RecvStatus);
            lblMCUSendReason.Text = _mcuStatus.MCUSendReason.ToString();
            lblMsgNumToRepeat.Text = _mcuStatus.MsgNumToRepeat.ToString();
            lblSequenceNumRefPoint.Text = _mcuStatus.A_SequenceNumRefPoint.ToString();
            lblMCUStatusChangeReadiness.Text = _mcuStatus.MCUStatusChangeReadinessInfo.ToString();

            // 当前悬浮架
            lblCurrentMaglevVehicleIdentifier.Text = _mcuStatus.CurrentMaglevVehicleIdentifier.ToString();
            UpdateClearFlag(pnlClearCurrent, _mcuStatus.ClearCurrentMaglevVehicleReadinessInfo);
            lblDCSNum.Text = _mcuStatus.DCSNum.ToString();
            lblCurrentMaglevVehiclePos.Text = $"{_mcuStatus.CurrentMaglevVehiclePos} cm";
            lblCurrentMaglevVehicleVelocity.Text = $"{_mcuStatus.CurrentMaglevVehicleVelocity} cm/s";
            lblCurrentMaglevVehicleAcc.Text = $"{_mcuStatus.CurrentMaglevVehicleAcc} cm/s²";
            lblTractionCapacityCurrent.Text = $"{_mcuStatus.TractionCapacityForCurrentVehicle}%";
            lblCurrentMaglevVehicleSPRStatus.Text = _mcuStatus.CurrentMaglevVehicleSPRStatus.ToString();

            // 虚拟悬浮架
            lblVirtualMaglevVehicleIdentifier.Text = _mcuStatus.VirtualMaglevVehicleIdentifier.ToString();
            UpdateClearFlag(pnlClearVirtual, _mcuStatus.ClearVirtualMaglevVehicleReadinessInfo);
            lblTractionCapacityVirtual.Text = $"{_mcuStatus.TractionCapacityForVirtualVehicle}%";

            // 故障与停车点
            lblMCUFaultStatus.Text = _mcuStatus.MCUFaultStatus.ToString();
            UpdateFaultStatus(_mcuStatus.MCUFaultStatus);
            lblParkingPointsNum.Text = _mcuStatus.ParkingPointsNum.ToString();
            lblCurrentKIIdentifiers.Text = string.Join(",", _mcuStatus.CurrentKIIdentifiers);
            lblVirtualKIIdentifiers.Text = string.Join(",", _mcuStatus.VirtualKIIdentifiers);
        }

        /// <summary>
        /// 更新通道状态指示灯
        /// </summary>
        private void UpdateChannelStatus(Panel panel, ChannelRecvStatusEnum status)
        {
            if (status == ChannelRecvStatusEnum.Normal)
            {
                panel.BackColor = Color.Lime;
            }
            else
            {
                panel.BackColor = Color.Red;
            }
        }

        /// <summary>
        /// 更新清除标志指示灯
        /// </summary>
        private void UpdateClearFlag(Panel panel, ClearMaglevVehicleReadinessInfoEnum status)
        {
            if (status == ClearMaglevVehicleReadinessInfoEnum.None)
            {
                panel.BackColor = Color.Lime;
            }
            else
            {
                panel.BackColor = Color.Red;
            }
        }

        /// <summary>
        /// 更新故障状态
        /// </summary>
        private void UpdateFaultStatus(MCUFaultStatusEnum status)
        {
            if (status == MCUFaultStatusEnum.UnDefine)
            {
                lblMCUFaultStatus.ForeColor = Color.Gray;
            }
            else if ((byte)status != 0)
            {
                lblMCUFaultStatus.ForeColor = Color.Red;
            }
            else
            {
                lblMCUFaultStatus.ForeColor = Color.Black;
            }
        }
        #endregion

        #region 菜单栏点击事件
        private void mOCSToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<MOCS_UI>();
        private void mCUToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<MCU_UI>();
        private void vSPSToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<VSPS_UI>();
        private void lCUToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<LCU_UI>();
        private void gCUToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<GCU_UI>();
        private void oBCToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<OBC_UI>();
        #endregion
    }
}
