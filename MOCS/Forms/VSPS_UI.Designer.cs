namespace MOCS.Forms
{
    partial class VSPS_UI
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
            menuStrip1 = new MenuStrip();
            mOCSToolStripMenuItem = new ToolStripMenuItem();
            mCUToolStripMenuItem = new ToolStripMenuItem();
            lCUToolStripMenuItem = new ToolStripMenuItem();
            gCUToolStripMenuItem = new ToolStripMenuItem();
            vSPSToolStripMenuItem = new ToolStripMenuItem();
            oBCToolStripMenuItem = new ToolStripMenuItem();
            VSPSpanel = new Panel();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            splitContainer2 = new SplitContainer();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            VSPSdiagnose = new RichTextBox();
            tabPage2 = new TabPage();
            splitContainer3 = new SplitContainer();
            groupBox4 = new GroupBox();
            VSPS = new RichTextBox();
            groupBox5 = new GroupBox();
            VSPSSendMsg = new RichTextBox();
            menuStrip1.SuspendLayout();
            VSPSpanel.SuspendLayout();
            tabControl1.SuspendLayout();
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
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { mOCSToolStripMenuItem, mCUToolStripMenuItem, lCUToolStripMenuItem, gCUToolStripMenuItem, vSPSToolStripMenuItem, oBCToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1894, 42);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
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
            mCUToolStripMenuItem.Size = new Size(91, 38);
            mCUToolStripMenuItem.Text = "MCU";
            mCUToolStripMenuItem.Click += mCUToolStripMenuItem_Click;
            // 
            // lCUToolStripMenuItem
            // 
            lCUToolStripMenuItem.Name = "lCUToolStripMenuItem";
            lCUToolStripMenuItem.Size = new Size(80, 38);
            lCUToolStripMenuItem.Text = "LCU";
            lCUToolStripMenuItem.Click += lCUToolStripMenuItem_Click;
            // 
            // gCUToolStripMenuItem
            // 
            gCUToolStripMenuItem.Name = "gCUToolStripMenuItem";
            gCUToolStripMenuItem.Size = new Size(86, 38);
            gCUToolStripMenuItem.Text = "GCU";
            gCUToolStripMenuItem.Click += gCUToolStripMenuItem_Click;
            // 
            // vSPSToolStripMenuItem
            // 
            vSPSToolStripMenuItem.Name = "vSPSToolStripMenuItem";
            vSPSToolStripMenuItem.Size = new Size(93, 38);
            vSPSToolStripMenuItem.Text = "VSPS";
            vSPSToolStripMenuItem.Click += vSPSToolStripMenuItem_Click;
            // 
            // oBCToolStripMenuItem
            // 
            oBCToolStripMenuItem.Name = "oBCToolStripMenuItem";
            oBCToolStripMenuItem.Size = new Size(85, 38);
            oBCToolStripMenuItem.Text = "OBC";
            oBCToolStripMenuItem.Click += oBCToolStripMenuItem_Click;
            // 
            // VSPSpanel
            // 
            VSPSpanel.Controls.Add(tabControl1);
            VSPSpanel.Dock = DockStyle.Fill;
            VSPSpanel.Location = new Point(0, 42);
            VSPSpanel.Name = "VSPSpanel";
            VSPSpanel.Size = new Size(1894, 967);
            VSPSpanel.TabIndex = 1;
            // 
            // tabControl1
            // 
            tabControl1.Alignment = TabAlignment.Bottom;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1894, 967);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(splitContainer1);
            tabPage1.Location = new Point(8, 8);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1878, 914);
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
            splitContainer1.Size = new Size(1872, 908);
            splitContainer1.SplitterDistance = 1260;
            splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1260, 908);
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
            splitContainer2.Size = new Size(608, 908);
            splitContainer2.SplitterDistance = 612;
            splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(608, 612);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "操作控制";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(VSPSdiagnose);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(608, 292);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "诊断信息";
            // 
            // VSPSdiagnose
            // 
            VSPSdiagnose.BackColor = SystemColors.Control;
            VSPSdiagnose.Dock = DockStyle.Fill;
            VSPSdiagnose.Location = new Point(3, 34);
            VSPSdiagnose.Name = "VSPSdiagnose";
            VSPSdiagnose.Size = new Size(602, 255);
            VSPSdiagnose.TabIndex = 0;
            VSPSdiagnose.Text = "";
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
            splitContainer3.SplitterDistance = 940;
            splitContainer3.TabIndex = 0;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(VSPS);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(0, 0);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(940, 908);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "接收报文";
            // 
            // VSPS
            // 
            VSPS.BackColor = SystemColors.Control;
            VSPS.Dock = DockStyle.Fill;
            VSPS.Location = new Point(3, 34);
            VSPS.Name = "VSPS";
            VSPS.Size = new Size(934, 871);
            VSPS.TabIndex = 0;
            VSPS.Text = "";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(VSPSSendMsg);
            groupBox5.Dock = DockStyle.Fill;
            groupBox5.Location = new Point(0, 0);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(928, 908);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "发送报文";
            // 
            // VSPSSendMsg
            // 
            VSPSSendMsg.BackColor = SystemColors.Control;
            VSPSSendMsg.Dock = DockStyle.Fill;
            VSPSSendMsg.Location = new Point(3, 34);
            VSPSSendMsg.Name = "VSPSSendMsg";
            VSPSSendMsg.Size = new Size(922, 871);
            VSPSSendMsg.TabIndex = 0;
            VSPSSendMsg.Text = "";
            // 
            // VSPS_UI
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1894, 1009);
            Controls.Add(VSPSpanel);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "VSPS_UI";
            Text = "VSPS_UI";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            VSPSpanel.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
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

        private MenuStrip menuStrip1;
        private Panel VSPSpanel;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private SplitContainer splitContainer2;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private RichTextBox VSPSdiagnose;
        private TabPage tabPage2;
        private SplitContainer splitContainer3;
        private GroupBox groupBox4;
        private RichTextBox VSPS;
        private GroupBox groupBox5;
        private RichTextBox VSPSSendMsg;
        private ToolStripMenuItem mOCSToolStripMenuItem;
        private ToolStripMenuItem mCUToolStripMenuItem;
        private ToolStripMenuItem lCUToolStripMenuItem;
        private ToolStripMenuItem gCUToolStripMenuItem;
        private ToolStripMenuItem vSPSToolStripMenuItem;
        private ToolStripMenuItem oBCToolStripMenuItem;
    }
}