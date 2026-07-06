using MOCS.Cores.VCU;
using MOCS.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MOCS.Forms
{
    public partial class LCU_UI : Form
    {
        #region 私有字段（使用单例）
        private readonly EMSStatus _emsStatus = EMSStatus.Instance;
        #endregion

        #region 构造函数
        public LCU_UI()
        {
            InitializeComponent();
            // 绑定窗体生命周期事件
            this.Load += LCU_UI_Load;
            this.FormClosed += LCU_UI_FormClosed;
            // 初始化UI显示
            InitLCUUI();
        }
        #endregion

        #region 窗体生命周期事件
        private void LCU_UI_Load(object sender, EventArgs e)
        {
            _emsStatus.PropertyChanged += EmsStatus_PropertyChanged;

            // 绑定 LCU 节点的收发报文 RTB
            MessageMonitor.Instance.BindRecvRTB("LCU", LCURecvMsg);
            MessageMonitor.Instance.BindSendRTB("LCU", LCUSendMsg);
        }

        private void LCU_UI_FormClosed(object sender, FormClosedEventArgs e)
        {
            _emsStatus.PropertyChanged -= EmsStatus_PropertyChanged;

            // 解除 LCU 节点 RTB 绑定
            MessageMonitor.Instance.UnbindRecvRTB("LCU");
            MessageMonitor.Instance.UnbindSendRTB("LCU");
        }
        #endregion

        #region 初始化
        private void InitLCUUI()
        {
            // 状态信息
            UpdateLifeUI();
            UpdateEMSSysStatusUI();
            UpdateSysSwitchStatusUI();
            UpdateCutStatusUI();
            UpdateBrakeStatusUI();
            UpdateStabilityStatusUI();
            // 传感器数据
            UpdateGapUI();
            UpdateGap1UI();
            UpdateGap2UI();
            UpdateGap3UI();
            UpdateGap4UI();
            UpdateI1UI();
            UpdateI2UI();
            UpdateAccUI();
            UpdateAcc1UI();
            UpdateAcc2UI();
            UpdateUUI();
            UpdateTempUI();
            // 故障与预警
            UpdateEMSFaultStatusUI();
            UpdateOverloadStatusUI();
            UpdateCPUStatusUI();
            UpdateKM1StatusUI();
            UpdateKM2StatusUI();
            UpdateOverloadWarningStatusUI();
            UpdateGapWarnningStatusUI();
            UpdateEMSWarningUI();
            // 硬件及传感器状态
            UpdateGapSensorsStatusUI();
            UpdateAccSensorsStatusUI();
            UpdateKM1ContactStatusUI();
            UpdateKM2ContactStatusUI();
        }
        #endregion

        #region 属性变更分发（switch模式）
        private void EmsStatus_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(EMSStatus.Life):
                    UpdateLifeUI();
                    break;
                case nameof(EMSStatus.EMSSysStatus):
                    UpdateEMSSysStatusUI();
                    break;
                case nameof(EMSStatus.SysSwitchStatus):
                    UpdateSysSwitchStatusUI();
                    break;
                case nameof(EMSStatus.CutStatus):
                    UpdateCutStatusUI();
                    break;
                case nameof(EMSStatus.BrakeStatus):
                    UpdateBrakeStatusUI();
                    break;
                case nameof(EMSStatus.StabilityStatus):
                    UpdateStabilityStatusUI();
                    break;
                case nameof(EMSStatus.Gap):
                    UpdateGapUI();
                    break;
                case nameof(EMSStatus.Gap1):
                    UpdateGap1UI();
                    break;
                case nameof(EMSStatus.Gap2):
                    UpdateGap2UI();
                    break;
                case nameof(EMSStatus.Gap3):
                    UpdateGap3UI();
                    break;
                case nameof(EMSStatus.Gap4):
                    UpdateGap4UI();
                    break;
                case nameof(EMSStatus.I1):
                    UpdateI1UI();
                    break;
                case nameof(EMSStatus.I2):
                    UpdateI2UI();
                    break;
                case nameof(EMSStatus.Acc):
                    UpdateAccUI();
                    break;
                case nameof(EMSStatus.Acc1):
                    UpdateAcc1UI();
                    break;
                case nameof(EMSStatus.Acc2):
                    UpdateAcc2UI();
                    break;
                case nameof(EMSStatus.U):
                    UpdateUUI();
                    break;
                case nameof(EMSStatus.Temp):
                    UpdateTempUI();
                    break;
                case nameof(EMSStatus.EMSFaultStatus):
                    UpdateEMSFaultStatusUI();
                    break;
                case nameof(EMSStatus.OverloadStatus):
                    UpdateOverloadStatusUI();
                    break;
                case nameof(EMSStatus.CPUStatus):
                    UpdateCPUStatusUI();
                    break;
                case nameof(EMSStatus.KM1Status):
                    UpdateKM1StatusUI();
                    break;
                case nameof(EMSStatus.KM2Status):
                    UpdateKM2StatusUI();
                    break;
                case nameof(EMSStatus.OverloadWarningStatus):
                    UpdateOverloadWarningStatusUI();
                    break;
                case nameof(EMSStatus.GapWarnningStatus):
                    UpdateGapWarnningStatusUI();
                    break;
                case nameof(EMSStatus.EMSWarning):
                    UpdateEMSWarningUI();
                    break;
                case nameof(EMSStatus.GapSensorsStatus):
                    UpdateGapSensorsStatusUI();
                    break;
                case nameof(EMSStatus.AccSensorsStatus):
                    UpdateAccSensorsStatusUI();
                    break;
                case nameof(EMSStatus.KM1ContactStatus):
                    UpdateKM1ContactStatusUI();
                    break;
                case nameof(EMSStatus.KM2ContactStatus):
                    UpdateKM2ContactStatusUI();
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
        /// 线程安全更新Label文本和颜色
        /// </summary>
        private void UpdateLabelWithColor(Label label, string text, Color normalColor, Color faultColor, bool isNormal)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(() =>
                {
                    label.Text = text;
                    label.BackColor = isNormal ? normalColor : faultColor;
                }));
            }
            else
            {
                label.Text = text;
                label.BackColor = isNormal ? normalColor : faultColor;
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
        /// 线程安全更新Panel颜色（True=黄, False=绿），用于预警指示
        /// </summary>
        private void UpdatePanelWarning(Panel panel, bool isWarning)
        {
            if (panel.InvokeRequired)
            {
                panel.Invoke(new Action(() => panel.BackColor = isWarning ? Color.Yellow : Color.LimeGreen));
            }
            else
            {
                panel.BackColor = isWarning ? Color.Yellow : Color.LimeGreen;
            }
        }

        /// <summary>
        /// 线程安全更新枚举类型Label（文本+颜色）
        /// </summary>
        private void UpdateEnumLabel<T>(Label label, T value, Func<T, string> toText, Func<T, Color> getColor)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(() =>
                {
                    label.Text = toText(value);
                    label.BackColor = getColor(value);
                }));
            }
            else
            {
                label.Text = toText(value);
                label.BackColor = getColor(value);
            }
        }
        #endregion

        #region UI更新方法 - 状态信息
        private void UpdateLifeUI()
        {
            UpdateLabel(lblLifeCycle, _emsStatus.Life.ToString());
        }

        private void UpdateEMSSysStatusUI()
        {
            UpdateEnumLabel(lblLevSysStatus, _emsStatus.EMSSysStatus,
                v => v == EMSSysStatusEnum.Normal ? "正常" : "异常",
                v => v == EMSSysStatusEnum.Normal ? Color.LimeGreen : Color.Red);
        }

        private void UpdateSysSwitchStatusUI()
        {
            UpdateLabel(lblCtrlSysChangeStatus, _emsStatus.SysSwitchStatus.ToString());
        }

        private void UpdateCutStatusUI()
        {
            UpdateEnumLabel(lblQCStatus, _emsStatus.CutStatus,
                v => v == CutStatusEnum.Normal ? "正常" : "切除",
                v => v == CutStatusEnum.Normal ? Color.LimeGreen : Color.Red);
        }

        private void UpdateBrakeStatusUI()
        {
            UpdateLabel(lblBrakeStatus, _emsStatus.BrakeStatus.ToString());
        }

        private void UpdateStabilityStatusUI()
        {
            UpdateEnumLabel(lblSWStatus, _emsStatus.StabilityStatus,
                v => v == StabilityStatusEnum.Normal ? "稳定" : "失稳",
                v => v == StabilityStatusEnum.Normal ? Color.LimeGreen : Color.Red);
        }
        #endregion

        #region UI更新方法 - 传感器数据
        private void UpdateGapUI() => UpdateLabel(lblGap0Data, $"{_emsStatus.Gap:F1} mm");
        private void UpdateGap1UI() => UpdateLabel(lblGap1Data, $"{_emsStatus.Gap1:F1} mm");
        private void UpdateGap2UI() => UpdateLabel(lblGap2Data, $"{_emsStatus.Gap2:F1} mm");
        private void UpdateGap3UI() => UpdateLabel(lblGap3Data, $"{_emsStatus.Gap3:F1} mm");
        private void UpdateGap4UI() => UpdateLabel(lblGap4Data, $"{_emsStatus.Gap4:F1} mm");
        private void UpdateI1UI() => UpdateLabel(lblI1Data, $"{_emsStatus.I1:F1} A");
        private void UpdateI2UI() => UpdateLabel(lblI2Data, $"{_emsStatus.I2:F1} A");
        private void UpdateAccUI() => UpdateLabel(lblAcc0Data, $"{_emsStatus.Acc:F2} g");
        private void UpdateAcc1UI() => UpdateLabel(lblAcc1Data, $"{_emsStatus.Acc1:F2} g");
        private void UpdateAcc2UI() => UpdateLabel(lblAcc2Data, $"{_emsStatus.Acc2:F2} g");
        private void UpdateUUI() => UpdateLabel(lblUData, $"{_emsStatus.U:F1} V");
        private void UpdateTempUI() => UpdateLabel(lblTempData, $"{_emsStatus.Temp} °C");
        #endregion

        #region UI更新方法 - 故障与预警
        private void UpdateEMSFaultStatusUI()
        {
            UpdatePanelReverse(pnlQF_OKStatus, _emsStatus.EMSFaultStatus != EMSFaultStatusEnum.Normal);
            UpdateEnumLabel(lblControllerFaultStatus, _emsStatus.EMSFaultStatus,
                v => v == EMSFaultStatusEnum.Normal ? "正常" : v.ToString(),
                v => v == EMSFaultStatusEnum.Normal ? Color.LimeGreen : Color.Red);
        }

        private void UpdateOverloadStatusUI()
        {
            UpdatePanelReverse(pnlGL_OKStatus, _emsStatus.OverloadStatus != OverloadStatusEnum.Deactivate);
        }

        private void UpdateCPUStatusUI()
        {
            UpdatePanelReverse(pnlCPU_OKStatus, _emsStatus.CPUStatus != CPUStatusEnum.Normal);
        }

        private void UpdateKM1StatusUI()
        {
            UpdatePanelReverse(pnlKM1FaultStatus, _emsStatus.KM1Status != KMStatusEnum.Normal);
        }

        private void UpdateKM2StatusUI()
        {
            UpdatePanelReverse(pnlKM2FaultStatus, _emsStatus.KM2Status != KMStatusEnum.Normal);
        }

        private void UpdateOverloadWarningStatusUI()
        {
            UpdatePanelWarning(pnlGL_OK_W, _emsStatus.OverloadWarningStatus != OverloadWarningStatusEnum.Normal);
        }

        private void UpdateGapWarnningStatusUI()
        {
            UpdatePanelWarning(pnlGap_OK_W, _emsStatus.GapWarnningStatus != GapWarnningStatusEnum.Normal);
        }

        private void UpdateEMSWarningUI()
        {
            UpdatePanelReverse(pnlControllerWarning, _emsStatus.EMSWarning != EMSWarningEnum.Normal);
        }
        #endregion

        #region UI更新方法 - 硬件及传感器状态
        private void UpdateGapSensorsStatusUI()
        {
            UpdateEnumLabel(lblGapSensorStatus, _emsStatus.GapSensorsStatus,
                v => v.ToString(),
                v => v == GapSensorsStatusEnum.Normal ? Color.LimeGreen : Color.Red);
        }

        private void UpdateAccSensorsStatusUI()
        {
            UpdateEnumLabel(lblAccSensorStatus, _emsStatus.AccSensorsStatus,
                v => v.ToString(),
                v => v == AccSensorsStatusEnum.Normal ? Color.LimeGreen : Color.Red);
        }

        private void UpdateKM1ContactStatusUI()
        {
            UpdatePanel(pnlKM1Status, _emsStatus.KM1ContactStatus == KMContactStatusEnum.Closed);
        }

        private void UpdateKM2ContactStatusUI()
        {
            UpdatePanel(pnlKM2Status, _emsStatus.KM2ContactStatus == KMContactStatusEnum.Closed);
        }
        #endregion

        #region 子系统窗口流转
        private void mOCSToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<MOCS_UI>();
        private void mCUToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<MCU_UI>();
        private void lCUToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<LCU_UI>();
        private void gCUToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<GCU_UI>();
        private void vSPSToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<VSPS_UI>();
        private void oBCToolStripMenuItem_Click(object sender, EventArgs e) => FormManager.OpenOrBringToFront<OBC_UI>();
        #endregion
    }
}
