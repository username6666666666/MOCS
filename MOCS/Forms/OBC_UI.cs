using MOCS.Cores.VCU;
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
    public partial class OBC_UI : Form
    {
        private readonly OBCStatus _obcStatus = new OBCStatus();
        public OBC_UI()
        {
            InitializeComponent();
            // 绑定状态变化事件（监听所有属性变更）
            _obcStatus.PropertyChanged += ObcStatus_PropertyChanged;

            Update440VBatterySwitchUI();// 窗体加载时初始化UI（显示初始状态：分闸=红）
            Update440VBatteryCapacityUI(); //初始化电量显示
        }
        // 状态变化事件：判断哪个属性变了，更新对应UI
        private void ObcStatus_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // 440V蓄电池合闸指示的变化
            if (e.PropertyName == nameof(OBCStatus.Is440VBatterySwitchClosed))
            {
                Update440VBatterySwitchUI();
            }
            //440V蓄电池电量的变化
            else if (e.PropertyName == nameof(OBCStatus.Battery440VCapacity))
            {
                Update440VBatteryCapacityUI();
            }
        }


        // 440V蓄电池状态的UI更新方法（控制pnIs440VBatterySwitchClosed的颜色）
        private void Update440VBatterySwitchUI()
        {
            // 跨线程安全（避免外部线程修改属性时UI报错）
            if (InvokeRequired)
            {
                Invoke(new Action(Update440VBatterySwitchUI));
                return;
            }

            // 根据公共属性的值，设置Panel颜色
            pnIs440VBatterySwitchClosed.BackColor = _obcStatus.Is440VBatterySwitchClosed
                ? Color.LimeGreen // 合闸=绿
                : Color.Red;      // 分闸=红
        }

        // 440V蓄电池电量的UI更新方法（控制lbl440VBatteryCapacity的文本）
        private void Update440VBatteryCapacityUI()
        {
            // 跨线程安全（和合闸状态保持一致的写法）
            if (InvokeRequired)
            {
                Invoke(new Action(Update440VBatteryCapacityUI));
                return;
            }

            // 核心修改：只显示百分比数值（比如 "85.5%"），去掉前缀文字
            lblBattery440VCapacity.Text = $"{_obcStatus.Battery440VCapacity:F1}%";
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OthergroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //调试用：点击改变440V蓄电池开关状态
            // 取反当前值（点一次合闸，再点一次分闸）
            _obcStatus.Is440VBatterySwitchClosed = !_obcStatus.Is440VBatterySwitchClosed;

            // 可选：弹窗提示当前状态，方便核对
            string status = _obcStatus.Is440VBatterySwitchClosed ? "合闸（绿）" : "分闸（红）";
            MessageBox.Show($"当前440V状态：{status}", "调试提示");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void gbPowerBattery_Enter(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//调试用
        {
            // 1. 获取当前电量，每次加5%
            float currentCapacity = _obcStatus.Battery440VCapacity;
            float newCapacity = currentCapacity + 5;

            // 2. 限制电量上限为100%（避免超过100）
            if (newCapacity > 100)
            {
                newCapacity = 100; // 到100%后不再增加
                                   // 可选：弹窗提示电量已满
                MessageBox.Show("440V蓄电池电量已达100%！", "调试提示");
            }

            // 3. 更新电量值（会自动触发事件，更新Label显示）
            _obcStatus.Battery440VCapacity = newCapacity;

            // 4. 可选：弹窗提示当前电量，方便核对
            MessageBox.Show($"当前440V电量：{newCapacity:F1}%", "调试提示");
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
