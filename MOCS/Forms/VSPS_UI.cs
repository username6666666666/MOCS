using MOCS.Cores.VCU;
using MOCS.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MOCS.Forms
{
    public partial class VSPS_UI : Form
    {
        #region 私有字段（使用单例）
        private readonly VSPSInfo _vspsInfo = VSPSInfo.Instance;
        #endregion

        #region 构造函数
        public VSPS_UI()
        {
            InitializeComponent();
            // 绑定窗体生命周期事件
            this.Load += VSPS_UI_Load;
            this.FormClosed += VSPS_UI_FormClosed;
            // 初始化UI显示
            InitVspsUI();
        }
        #endregion

        #region 窗体生命周期事件
        private void VSPS_UI_Load(object sender, EventArgs e)
        {
            _vspsInfo.PropertyChanged += VspsInfo_PropertyChanged;

            // 绑定 VSPS 节点的收发报文 RTB
            MessageMonitor.Instance.BindRecvRTB("VSPS", VSPS);
            MessageMonitor.Instance.BindSendRTB("VSPS", VSPSSendMsg);
        }

        private void VSPS_UI_FormClosed(object sender, FormClosedEventArgs e)
        {
            _vspsInfo.PropertyChanged -= VspsInfo_PropertyChanged;

            // 解除 VSPS 节点 RTB 绑定
            MessageMonitor.Instance.UnbindRecvRTB("VSPS");
            MessageMonitor.Instance.UnbindSendRTB("VSPS");
        }
        #endregion

        #region 初始化
        private void InitVspsUI()
        {
            UpdateLifeUI();
            UpdateDirectionUI();
            UpdateSpeedUI();
            UpdateRelativePosUI();
        }
        #endregion

        #region 属性变更分发（switch模式）
        private void VspsInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(VSPSInfo.Life):
                    UpdateLifeUI();
                    break;
                case nameof(VSPSInfo.Forward):
                    UpdateDirectionUI();
                    break;
                case nameof(VSPSInfo.Speed):
                    UpdateSpeedUI();
                    break;
                case nameof(VSPSInfo.RelativePos):
                    UpdateRelativePosUI();
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

        #region UI更新方法
        private void UpdateLifeUI()
        {
            UpdateLabel(lblLifeCycle, _vspsInfo.Life.ToString());
        }

        private void UpdateDirectionUI()
        {
            UpdateLabel(lblDirection, _vspsInfo.Forward ? "正向" : "反向");
        }

        private void UpdateSpeedUI()
        {
            UpdateLabel(lblSpeed, $"{_vspsInfo.Speed} cm/s");
        }

        private void UpdateRelativePosUI()
        {
            UpdateLabel(lblRelaPos, $"{_vspsInfo.RelativePos} mm");
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

        #region 调试测试按钮
        private void button1_Click(object sender, EventArgs e)
        {
            _vspsInfo.Forward = !_vspsInfo.Forward;

            if (_vspsInfo.RelativePos + 10 > ushort.MaxValue)
            {
                _vspsInfo.RelativePos = 0;
            }
            else
            {
                _vspsInfo.RelativePos += 10;
            }
        }
        #endregion
    }
}
