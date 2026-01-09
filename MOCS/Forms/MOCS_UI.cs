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
    public partial class MOCS_UI : Form
    {

        public MOCS_UI()
        {
            InitializeComponent();
            // 程序启动时，把主窗口自身注册到FormManager
            FormManager.RegisterOpenedForm(this);
            // 主窗口关闭时：关闭所有子窗口 + 终止程序
            this.FormClosing += (s, e) =>
            {
                FormManager.CloseAllForms();
                //Application.Exit(); // 确保程序完全退出
            };
        }

        #region 菜单栏点击事件（所有菜单项调用通用工具类）
        

        // 点击 MOCS 菜单项（打开/置顶自己）
        private void mOCSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.OpenOrBringToFront<MOCS_UI>();
        }

        // 点击 MCU 菜单项
        private void mCUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.OpenOrBringToFront<MCU_UI>();
        }

        // 点击 VSPS 菜单项
        private void vSPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.OpenOrBringToFront<VSPS_UI>();
        }

        // 点击 LCU 菜单项
        private void lCUToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormManager.OpenOrBringToFront<LCU_UI>();
        }

        // 点击 GCU 菜单项
        private void lCUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.OpenOrBringToFront<GCU_UI>();
        }

        // 点击 OBC 菜单项
        private void oBCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManager.OpenOrBringToFront<OBC_UI>();
        }
        #endregion
    }
}
