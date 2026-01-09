namespace MOCS.Forms
{
    partial class MCU_UI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MCUmenuStrip = new MenuStrip();
            mOCSToolStripMenuItem = new ToolStripMenuItem();
            mCUToolStripMenuItem = new ToolStripMenuItem();
            lCUToolStripMenuItem = new ToolStripMenuItem();
            gCUToolStripMenuItem = new ToolStripMenuItem();
            vSPSToolStripMenuItem = new ToolStripMenuItem();
            oBCToolStripMenuItem = new ToolStripMenuItem();
            MCUpanel = new Panel();
            MCUtabControl = new TabControl();
            tabPage1 = new TabPage();
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            splitContainer2 = new SplitContainer();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            MCUdiagnose = new RichTextBox();
            tabPage2 = new TabPage();
            splitContainer3 = new SplitContainer();
            groupBox4 = new GroupBox();
            MCURecvMsg = new RichTextBox();
            groupBox5 = new GroupBox();
            MCUSendMsg = new RichTextBox();
            MCUmenuStrip.SuspendLayout();
            MCUpanel.SuspendLayout();
            MCUtabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            groupBox3.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // MCUmenuStrip
            // 
            MCUmenuStrip.ImageScalingSize = new Size(32, 32);
            MCUmenuStrip.Items.AddRange(new ToolStripItem[] { mOCSToolStripMenuItem, mCUToolStripMenuItem, lCUToolStripMenuItem, gCUToolStripMenuItem, vSPSToolStripMenuItem, oBCToolStripMenuItem });
            MCUmenuStrip.Location = new Point(0, 0);
            MCUmenuStrip.Name = "MCUmenuStrip";
            MCUmenuStrip.Size = new Size(1894, 39);
            MCUmenuStrip.TabIndex = 0;
            MCUmenuStrip.Text = "menuStrip1";
            // 
            // mOCSToolStripMenuItem
            // 
            mOCSToolStripMenuItem.Name = "mOCSToolStripMenuItem";
            mOCSToolStripMenuItem.Size = new Size(107, 38);
            mOCSToolStripMenuItem.Text = "MOCS";
            mOCSToolStripMenuItem.Click += mOCSToolStripMenuItem_Click;
            // 
            // mCUToolStripMenuItem
            // 
            mCUToolStripMenuItem.Name = "mCUToolStripMenuItem";
            mCUToolStripMenuItem.Size = new Size(91, 35);
            mCUToolStripMenuItem.Text = "MCU";
            mCUToolStripMenuItem.Click += mCUToolStripMenuItem_Click;
            // 
            // lCUToolStripMenuItem
            // 
            lCUToolStripMenuItem.Name = "lCUToolStripMenuItem";
            lCUToolStripMenuItem.Size = new Size(80, 35);
            lCUToolStripMenuItem.Text = "LCU";
            lCUToolStripMenuItem.Click += lCUToolStripMenuItem_Click;
            // 
            // gCUToolStripMenuItem
            // 
            gCUToolStripMenuItem.Name = "gCUToolStripMenuItem";
            gCUToolStripMenuItem.Size = new Size(86, 35);
            gCUToolStripMenuItem.Text = "GCU";
            gCUToolStripMenuItem.Click += gCUToolStripMenuItem_Click;
            // 
            // vSPSToolStripMenuItem
            // 
            vSPSToolStripMenuItem.Name = "vSPSToolStripMenuItem";
            vSPSToolStripMenuItem.Size = new Size(93, 35);
            vSPSToolStripMenuItem.Text = "VSPS";
            vSPSToolStripMenuItem.Click += vSPSToolStripMenuItem_Click;
            // 
            // oBCToolStripMenuItem
            // 
            oBCToolStripMenuItem.Name = "oBCToolStripMenuItem";
            oBCToolStripMenuItem.Size = new Size(85, 35);
            oBCToolStripMenuItem.Text = "OBC";
            oBCToolStripMenuItem.Click += oBCToolStripMenuItem_Click;
            // 
            // MCUpanel
            // 
            MCUpanel.Controls.Add(MCUtabControl);
            MCUpanel.Dock = DockStyle.Fill;
            MCUpanel.Location = new Point(0, 39);
            MCUpanel.Name = "MCUpanel";
            MCUpanel.Size = new Size(1894, 970);
            MCUpanel.TabIndex = 1;
            // 
            // MCUtabControl
            // 
            MCUtabControl.Alignment = TabAlignment.Bottom;
            MCUtabControl.Controls.Add(tabPage1);
            MCUtabControl.Controls.Add(tabPage2);
            MCUtabControl.Dock = DockStyle.Fill;
            MCUtabControl.Location = new Point(0, 0);
            MCUtabControl.Name = "MCUtabControl";
            MCUtabControl.SelectedIndex = 0;
            MCUtabControl.Size = new Size(1894, 970);
            MCUtabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(splitContainer1);
            tabPage1.Location = new Point(8, 8);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1878, 917);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "状态监视";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(1872, 911);
            splitContainer1.SplitterDistance = 1223;
            splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1223, 911);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "状态信息";
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(groupBox3);
            splitContainer2.Size = new Size(645, 911);
            splitContainer2.SplitterDistance = 604;
            splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(645, 604);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "操作控制";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(MCUdiagnose);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(645, 303);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "诊断信息";
            // 
            // MCUdiagnose
            // 
            MCUdiagnose.BackColor = SystemColors.Control;
            MCUdiagnose.Dock = DockStyle.Fill;
            MCUdiagnose.Location = new Point(3, 34);
            MCUdiagnose.Name = "MCUdiagnose";
            MCUdiagnose.Size = new Size(639, 266);
            MCUdiagnose.TabIndex = 0;
            MCUdiagnose.Text = "";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(splitContainer3);
            tabPage2.Location = new Point(8, 8);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1878, 914);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "报文收发";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(3, 3);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(groupBox4);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(groupBox5);
            splitContainer3.Size = new Size(1872, 908);
            splitContainer3.SplitterDistance = 893;
            splitContainer3.TabIndex = 0;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(MCURecvMsg);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(0, 0);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(893, 908);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "接收报文";
            // 
            // MCURecvMsg
            // 
            MCURecvMsg.BackColor = SystemColors.Control;
            MCURecvMsg.Dock = DockStyle.Fill;
            MCURecvMsg.Location = new Point(3, 34);
            MCURecvMsg.Name = "MCURecvMsg";
            MCURecvMsg.Size = new Size(887, 871);
            MCURecvMsg.TabIndex = 0;
            MCURecvMsg.Text = "";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(MCUSendMsg);
            groupBox5.Dock = DockStyle.Fill;
            groupBox5.Location = new Point(0, 0);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(975, 908);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "发送报文";
            // 
            // MCUSendMsg
            // 
            MCUSendMsg.BackColor = SystemColors.Control;
            MCUSendMsg.Dock = DockStyle.Fill;
            MCUSendMsg.Location = new Point(3, 34);
            MCUSendMsg.Name = "MCUSendMsg";
            MCUSendMsg.Size = new Size(969, 871);
            MCUSendMsg.TabIndex = 0;
            MCUSendMsg.Text = "";
            // 
            // MCU_UI
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1894, 1009);
            Controls.Add(MCUpanel);
            Controls.Add(MCUmenuStrip);
            MainMenuStrip = MCUmenuStrip;
            Name = "MCU_UI";
            Text = "MCU_UI";
            MCUmenuStrip.ResumeLayout(false);
            MCUmenuStrip.PerformLayout();
            MCUpanel.ResumeLayout(false);
            MCUtabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MCUmenuStrip;
        private Panel MCUpanel;
        private TabControl MCUtabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private RichTextBox MCUdiagnose;
        private SplitContainer splitContainer3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private RichTextBox MCURecvMsg;
        private RichTextBox MCUSendMsg;
        private ToolStripMenuItem mOCSToolStripMenuItem;
        private ToolStripMenuItem mCUToolStripMenuItem;
        private ToolStripMenuItem lCUToolStripMenuItem;
        private ToolStripMenuItem gCUToolStripMenuItem;
        private ToolStripMenuItem vSPSToolStripMenuItem;
        private ToolStripMenuItem oBCToolStripMenuItem;
    }
}