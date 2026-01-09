namespace MOCS.Forms
{
    partial class MOCS_UI
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
            MOCSmenuStrip = new MenuStrip();
            mOCSToolStripMenuItem = new ToolStripMenuItem();
            mCUToolStripMenuItem = new ToolStripMenuItem();
            lCUToolStripMenuItem1 = new ToolStripMenuItem();
            lCUToolStripMenuItem = new ToolStripMenuItem();
            vSPSToolStripMenuItem = new ToolStripMenuItem();
            oBCToolStripMenuItem = new ToolStripMenuItem();
            MOCSpanel = new Panel();
            MOCStabControl = new TabControl();
            tabPage1 = new TabPage();
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            splitContainer2 = new SplitContainer();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            MOCSdiagnose = new RichTextBox();
            tabPage2 = new TabPage();
            splitContainer3 = new SplitContainer();
            groupBox4 = new GroupBox();
            MOCSRecvMsg = new RichTextBox();
            groupBox5 = new GroupBox();
            MOCSSendMsg = new RichTextBox();
            MOCSmenuStrip.SuspendLayout();
            MOCSpanel.SuspendLayout();
            MOCStabControl.SuspendLayout();
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
            // MOCSmenuStrip
            // 
            MOCSmenuStrip.ImageScalingSize = new Size(32, 32);
            MOCSmenuStrip.Items.AddRange(new ToolStripItem[] { mOCSToolStripMenuItem, mCUToolStripMenuItem, lCUToolStripMenuItem1, lCUToolStripMenuItem, vSPSToolStripMenuItem, oBCToolStripMenuItem });
            MOCSmenuStrip.Location = new Point(0, 0);
            MOCSmenuStrip.Name = "MOCSmenuStrip";
            MOCSmenuStrip.Size = new Size(1894, 42);
            MOCSmenuStrip.TabIndex = 0;
            MOCSmenuStrip.Text = "menuStrip1";
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
            // lCUToolStripMenuItem1
            // 
            lCUToolStripMenuItem1.Name = "lCUToolStripMenuItem1";
            lCUToolStripMenuItem1.Size = new Size(80, 38);
            lCUToolStripMenuItem1.Text = "LCU";
            lCUToolStripMenuItem1.Click += lCUToolStripMenuItem1_Click;
            // 
            // lCUToolStripMenuItem
            // 
            lCUToolStripMenuItem.Name = "lCUToolStripMenuItem";
            lCUToolStripMenuItem.Size = new Size(86, 38);
            lCUToolStripMenuItem.Text = "GCU";
            lCUToolStripMenuItem.Click += lCUToolStripMenuItem_Click;
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
            // MOCSpanel
            // 
            MOCSpanel.Controls.Add(MOCStabControl);
            MOCSpanel.Dock = DockStyle.Fill;
            MOCSpanel.Location = new Point(0, 42);
            MOCSpanel.Name = "MOCSpanel";
            MOCSpanel.Size = new Size(1894, 967);
            MOCSpanel.TabIndex = 1;
            // 
            // MOCStabControl
            // 
            MOCStabControl.Alignment = TabAlignment.Bottom;
            MOCStabControl.Controls.Add(tabPage1);
            MOCStabControl.Controls.Add(tabPage2);
            MOCStabControl.Dock = DockStyle.Fill;
            MOCStabControl.Location = new Point(0, 0);
            MOCStabControl.Multiline = true;
            MOCStabControl.Name = "MOCStabControl";
            MOCStabControl.SelectedIndex = 0;
            MOCStabControl.Size = new Size(1894, 967);
            MOCStabControl.TabIndex = 0;
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
            splitContainer1.SplitterDistance = 1172;
            splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1172, 908);
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
            splitContainer2.Size = new Size(696, 908);
            splitContainer2.SplitterDistance = 600;
            splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(696, 600);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "操作控制";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(MOCSdiagnose);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(696, 304);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "诊断信息";
            // 
            // MOCSdiagnose
            // 
            MOCSdiagnose.BackColor = SystemColors.Control;
            MOCSdiagnose.Dock = DockStyle.Fill;
            MOCSdiagnose.Location = new Point(3, 34);
            MOCSdiagnose.Name = "MOCSdiagnose";
            MOCSdiagnose.Size = new Size(690, 267);
            MOCSdiagnose.TabIndex = 0;
            MOCSdiagnose.Text = "";
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
            splitContainer3.SplitterDistance = 928;
            splitContainer3.TabIndex = 0;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(MOCSRecvMsg);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(0, 0);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(928, 908);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "接收报文";
            // 
            // MOCSRecvMsg
            // 
            MOCSRecvMsg.BackColor = SystemColors.Control;
            MOCSRecvMsg.Dock = DockStyle.Fill;
            MOCSRecvMsg.Location = new Point(3, 34);
            MOCSRecvMsg.Name = "MOCSRecvMsg";
            MOCSRecvMsg.Size = new Size(922, 871);
            MOCSRecvMsg.TabIndex = 0;
            MOCSRecvMsg.Text = "";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(MOCSSendMsg);
            groupBox5.Dock = DockStyle.Fill;
            groupBox5.Location = new Point(0, 0);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(940, 908);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "发送报文";
            // 
            // MOCSSendMsg
            // 
            MOCSSendMsg.BackColor = SystemColors.Control;
            MOCSSendMsg.Dock = DockStyle.Fill;
            MOCSSendMsg.Location = new Point(3, 34);
            MOCSSendMsg.Name = "MOCSSendMsg";
            MOCSSendMsg.Size = new Size(934, 871);
            MOCSSendMsg.TabIndex = 0;
            MOCSSendMsg.Text = "";
            // 
            // MOCS_UI
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1894, 1009);
            Controls.Add(MOCSpanel);
            Controls.Add(MOCSmenuStrip);
            Name = "MOCS_UI";
            Text = "MOCS";
            MOCSmenuStrip.ResumeLayout(false);
            MOCSmenuStrip.PerformLayout();
            MOCSpanel.ResumeLayout(false);
            MOCStabControl.ResumeLayout(false);
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

        private MenuStrip MOCSmenuStrip;
        private Panel MOCSpanel;
        private TabControl MOCStabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private RichTextBox MOCSdiagnose;
        private SplitContainer splitContainer3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private RichTextBox MOCSRecvMsg;
        private RichTextBox MOCSSendMsg;
        private ToolStripMenuItem mOCSToolStripMenuItem;
        private ToolStripMenuItem mCUToolStripMenuItem;
        private ToolStripMenuItem lCUToolStripMenuItem;
        private ToolStripMenuItem vSPSToolStripMenuItem;
        private ToolStripMenuItem oBCToolStripMenuItem;
        private ToolStripMenuItem lCUToolStripMenuItem1;
    }
}