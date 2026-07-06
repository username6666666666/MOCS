using MOCS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOCS.Forms
{
    public partial class GCU_UI : Form
    {
        public GCU_UI()
        {
            InitializeComponent();
            this.Load += GCU_UI_Load;
            this.FormClosed += GCU_UI_FormClosed;
        }

        private void GCU_UI_Load(object sender, EventArgs e)
        {
            // 绑定 GCU 节点的收发报文 RTB
            MessageMonitor.Instance.BindRecvRTB("GCU", GCURecvMsg);
            MessageMonitor.Instance.BindSendRTB("GCU", GCUSendMsg);
        }

        private void GCU_UI_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 解除 GCU 节点 RTB 绑定
            MessageMonitor.Instance.UnbindRecvRTB("GCU");
            MessageMonitor.Instance.UnbindSendRTB("GCU");
        }

        #region ToolStripMenuItem 点击事件（所有菜单项）
        // 点击 MOCS 菜单项 → 打开/置顶主窗口
        private void mOCSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.OpenOrBringToFront<MOCS_UI>();
        }

        // 点击 MCU 菜单项 → 打开/置顶自身
        private void mCUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.OpenOrBringToFront<MCU_UI>();
        }

        // 点击 LCU 菜单项 → 打开/置顶LCU_UI
        private void lCUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.OpenOrBringToFront<LCU_UI>();
        }

        // 点击 GCU 菜单项 → 打开/置顶GCU_UI
        private void gCUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.OpenOrBringToFront<GCU_UI>();
        }

        // 点击 VSPS 菜单项 → 打开/置顶VSPS_UI
        private void vSPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.OpenOrBringToFront<VSPS_UI>();
        }

        // 点击 OBC 菜单项 → 打开/置顶OBC_UI
        private void oBCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.OpenOrBringToFront<OBC_UI>();
        }
        #endregion
    }
}
