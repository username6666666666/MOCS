using MOCS.Cores.VCU;
using MOCS.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MOCS.Forms
{
    public partial class OBC_UI : Form
    {
        #region 私有字段（使用单例）
        private readonly OBCStatus _obcStatus = OBCStatus.Instance;
        #endregion

        #region 构造函数
        public OBC_UI()
        {
            InitializeComponent();
            // 绑定窗体生命周期事件
            this.Load += OBC_UI_Load;
            this.FormClosed += OBC_UI_FormClosed;
            // 初始化UI显示
            InitOBCUI();
        }
        #endregion

        #region 窗体生命周期事件
        private void OBC_UI_Load(object sender, EventArgs e)
        {
            _obcStatus.PropertyChanged += ObcStatus_PropertyChanged;
        }

        private void OBC_UI_FormClosed(object sender, FormClosedEventArgs e)
        {
            _obcStatus.PropertyChanged -= ObcStatus_PropertyChanged;
        }
        #endregion

        #region 初始化
        private void InitOBCUI()
        {
            Update440VBatterySwitchUI();
            Update440VBatteryCapacityUI();
            Update480VPowerSwitchUI();
            UpdateDC330VCircuitBreakerUI();
            Update25kWPowerFailedUI();
            Update5kWPowerFailedUI();
            Update110VBatteryCapacityUI();
            UpdateGuideEnabledUI();
            UpdateLeviatedUI();
            UpdatePantographExtended1UI();
            UpdatePantographExtended2UI();
            UpdatePantographRetracted1UI();
            UpdatePantographRetracted2UI();
            UpdatePantographEnergizedUI();
        }
        #endregion

        #region 属性变更分发（switch模式）
        private void ObcStatus_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(OBCStatus.Is440VBatterySwitchClosed):
                    Update440VBatterySwitchUI();
                    break;
                case nameof(OBCStatus.Battery440VCapacity):
                    Update440VBatteryCapacityUI();
                    break;
                case nameof(OBCStatus.Is480VPowerSwitchClosed):
                    Update480VPowerSwitchUI();
                    break;
                case nameof(OBCStatus.IsDC330VCircuitBreakerEnabled):
                    UpdateDC330VCircuitBreakerUI();
                    break;
                case nameof(OBCStatus.Is25kWPowerFailed):
                    Update25kWPowerFailedUI();
                    break;
                case nameof(OBCStatus.Is5kWPowerFailed):
                    Update5kWPowerFailedUI();
                    break;
                case nameof(OBCStatus.Battery110VCapacity):
                    Update110VBatteryCapacityUI();
                    break;
                case nameof(OBCStatus.IsGuideEnabled):
                    UpdateGuideEnabledUI();
                    break;
                case nameof(OBCStatus.IsLeviated):
                    UpdateLeviatedUI();
                    break;
                case nameof(OBCStatus.IsPantographExtended1):
                    UpdatePantographExtended1UI();
                    break;
                case nameof(OBCStatus.IsPantographExtended2):
                    UpdatePantographExtended2UI();
                    break;
                case nameof(OBCStatus.IsPantographRetracted1):
                    UpdatePantographRetracted1UI();
                    break;
                case nameof(OBCStatus.IsPantographRetracted2):
                    UpdatePantographRetracted2UI();
                    break;
                case nameof(OBCStatus.IsPantographEnergized):
                    UpdatePantographEnergizedUI();
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
        #endregion

        #region UI更新方法 - 电源与电池区域
        private void Update440VBatterySwitchUI()
        {
            UpdatePanel(pnIs440VBatterySwitchClosed, _obcStatus.Is440VBatterySwitchClosed);
        }

        private void Update440VBatteryCapacityUI()
        {
            UpdateLabel(lblBattery440VCapacity, $"{_obcStatus.Battery440VCapacity:F1}%");
        }

        private void Update480VPowerSwitchUI()
        {
            UpdatePanel(pnlIs480VPowerSwitchClosed, _obcStatus.Is480VPowerSwitchClosed);
        }

        private void UpdateDC330VCircuitBreakerUI()
        {
            UpdatePanel(pnlIsDC330VCircuitBreakerEnabled, _obcStatus.IsDC330VCircuitBreakerEnabled);
        }

        private void Update25kWPowerFailedUI()
        {
            UpdatePanelReverse(pnlIs25kWPowerFailed, _obcStatus.Is25kWPowerFailed);
        }

        private void Update5kWPowerFailedUI()
        {
            UpdatePanelReverse(pnlIs5kWPowerFailed, _obcStatus.Is5kWPowerFailed);
        }

        private void Update110VBatteryCapacityUI()
        {
            UpdateLabel(lblBattery110VCapacity, $"{_obcStatus.Battery110VCapacity:F1}%");
        }
        #endregion

        #region UI更新方法 - 悬浮与导向区域
        private void UpdateGuideEnabledUI()
        {
            UpdatePanel(pnlIsGuideEnabled, _obcStatus.IsGuideEnabled);
        }

        private void UpdateLeviatedUI()
        {
            UpdatePanel(pnlIsLeviated, _obcStatus.IsLeviated);
        }
        #endregion

        #region UI更新方法 - 受流器区域
        private void UpdatePantographExtended1UI()
        {
            UpdatePanel(pnlIsPantographExtended1, _obcStatus.IsPantographExtended1);
        }

        private void UpdatePantographExtended2UI()
        {
            UpdatePanel(pnlIsPantographExtended2, _obcStatus.IsPantographExtended2);
        }

        private void UpdatePantographRetracted1UI()
        {
            UpdatePanel(pnlIsPantographRetracted1, _obcStatus.IsPantographRetracted1);
        }

        private void UpdatePantographRetracted2UI()
        {
            UpdatePanel(pnlIsPantographRetracted2, _obcStatus.IsPantographRetracted2);
        }

        private void UpdatePantographEnergizedUI()
        {
            UpdatePanel(pnlIsPantographEnergized, _obcStatus.IsPantographEnergized);
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
