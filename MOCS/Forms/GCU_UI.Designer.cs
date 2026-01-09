namespace MOCS.Forms
{
    partial class GCU_UI
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
            tabPage2 = new TabPage();
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            splitContainer2 = new SplitContainer();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            GCUdiagnose = new RichTextBox();
            GCUtabControl = new TabControl();
            tabPage3 = new TabPage();
            splitContainer3 = new SplitContainer();
            groupBox4 = new GroupBox();
            GCURecvMsg = new RichTextBox();
            groupBox5 = new GroupBox();
            GCUSendMsg = new RichTextBox();
            menuStrip1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            groupBox3.SuspendLayout();
            GCUtabControl.SuspendLayout();
            tabPage3.SuspendLayout();
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
            menuStrip1.Size = new Size(1894, 39);
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
            // tabPage2
            // 
            tabPage2.Controls.Add(splitContainer1);
            tabPage2.Location = new Point(8, 8);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1878, 917);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "状态监视";
            tabPage2.UseVisualStyleBackColor = true;
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
            splitContainer1.SplitterDistance = 1224;
            splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1224, 911);
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
            splitContainer2.Size = new Size(644, 911);
            splitContainer2.SplitterDistance = 566;
            splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(644, 566);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "操作控制";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(GCUdiagnose);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(644, 341);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "诊断信息";
            // 
            // GCUdiagnose
            // 
            GCUdiagnose.BackColor = SystemColors.Control;
            GCUdiagnose.Dock = DockStyle.Fill;
            GCUdiagnose.Location = new Point(3, 34);
            GCUdiagnose.Name = "GCUdiagnose";
            GCUdiagnose.Size = new Size(638, 304);
            GCUdiagnose.TabIndex = 0;
            GCUdiagnose.Text = "";
            // 
            // GCUtabControl
            // 
            GCUtabControl.Alignment = TabAlignment.Bottom;
            GCUtabControl.Controls.Add(tabPage2);
            GCUtabControl.Controls.Add(tabPage3);
            GCUtabControl.Dock = DockStyle.Fill;
            GCUtabControl.Location = new Point(0, 39);
            GCUtabControl.Name = "GCUtabControl";
            GCUtabControl.SelectedIndex = 0;
            GCUtabControl.Size = new Size(1894, 970);
            GCUtabControl.TabIndex = 1;
            GCUtabControl.TabStop = false;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(splitContainer3);
            tabPage3.Location = new Point(8, 8);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1878, 914);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "报文收发";
            tabPage3.UseVisualStyleBackColor = true;
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
            splitContainer3.SplitterDistance = 913;
            splitContainer3.TabIndex = 0;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(GCURecvMsg);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(0, 0);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(913, 908);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "接收报文";
            // 
            // GCURecvMsg
            // 
            GCURecvMsg.BackColor = SystemColors.Control;
            GCURecvMsg.Dock = DockStyle.Fill;
            GCURecvMsg.Location = new Point(3, 34);
            GCURecvMsg.Name = "GCURecvMsg";
            GCURecvMsg.Size = new Size(907, 871);
            GCURecvMsg.TabIndex = 0;
            GCURecvMsg.Text = "";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(GCUSendMsg);
            groupBox5.Dock = DockStyle.Fill;
            groupBox5.Location = new Point(0, 0);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(955, 908);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "发送报文";
            // 
            // GCUSendMsg
            // 
            GCUSendMsg.BackColor = SystemColors.Control;
            GCUSendMsg.Dock = DockStyle.Fill;
            GCUSendMsg.Location = new Point(3, 34);
            GCUSendMsg.Name = "GCUSendMsg";
            GCUSendMsg.Size = new Size(949, 871);
            GCUSendMsg.TabIndex = 0;
            GCUSendMsg.Text = "";
            // 
            // GCU_UI
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1894, 1009);
            Controls.Add(GCUtabControl);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "GCU_UI";
            Text = "GCU";
            Load += GCU_UI_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabPage2.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            GCUtabControl.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
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
        private TabPage tabPage2;
        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private SplitContainer splitContainer2;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private RichTextBox GCUdiagnose;
        private TabControl GCUtabControl;
        private TabPage tabPage3;
        private SplitContainer splitContainer3;
        private GroupBox groupBox4;
        private RichTextBox GCURecvMsg;
        private GroupBox groupBox5;
        private RichTextBox GCUSendMsg;
        private ToolStripMenuItem mOCSToolStripMenuItem;
        private ToolStripMenuItem mCUToolStripMenuItem;
        private ToolStripMenuItem lCUToolStripMenuItem;
        private ToolStripMenuItem gCUToolStripMenuItem;
        private ToolStripMenuItem vSPSToolStripMenuItem;
        private ToolStripMenuItem oBCToolStripMenuItem;
    }
}