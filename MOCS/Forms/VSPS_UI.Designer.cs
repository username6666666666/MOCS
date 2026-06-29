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
            splitContainer4 = new SplitContainer();
            splitContainer5 = new SplitContainer();
            tableLayoutPanel1 = new TableLayoutPanel();
            label12 = new Label();
            label11 = new Label();
            lblComCycle = new Label();
            lblComID = new Label();
            lblPeerIP = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            lblIPAddress = new Label();
            splitContainer6 = new SplitContainer();
            button1 = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblRelaPos = new Label();
            lblSpeed = new Label();
            lblDirection = new Label();
            lblLifeCycle = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
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
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer5).BeginInit();
            splitContainer5.Panel1.SuspendLayout();
            splitContainer5.Panel2.SuspendLayout();
            splitContainer5.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer6).BeginInit();
            splitContainer6.Panel1.SuspendLayout();
            splitContainer6.Panel2.SuspendLayout();
            splitContainer6.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
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
            menuStrip1.Size = new Size(1894, 39);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // mOCSToolStripMenuItem
            // 
            mOCSToolStripMenuItem.Name = "mOCSToolStripMenuItem";
            mOCSToolStripMenuItem.Size = new Size(107, 35);
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
            // VSPSpanel
            // 
            VSPSpanel.Controls.Add(tabControl1);
            VSPSpanel.Dock = DockStyle.Fill;
            VSPSpanel.Location = new Point(0, 39);
            VSPSpanel.Name = "VSPSpanel";
            VSPSpanel.Size = new Size(1894, 970);
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
            tabControl1.Size = new Size(1894, 970);
            tabControl1.TabIndex = 0;
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
            splitContainer1.SplitterDistance = 1260;
            splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(splitContainer4);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1260, 911);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "状态信息";
            // 
            // splitContainer4
            // 
            splitContainer4.Dock = DockStyle.Fill;
            splitContainer4.Location = new Point(3, 34);
            splitContainer4.Name = "splitContainer4";
            splitContainer4.Orientation = Orientation.Horizontal;
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.Controls.Add(splitContainer5);
            splitContainer4.Size = new Size(1254, 874);
            splitContainer4.SplitterDistance = 131;
            splitContainer4.TabIndex = 0;
            // 
            // splitContainer5
            // 
            splitContainer5.Dock = DockStyle.Fill;
            splitContainer5.Location = new Point(0, 0);
            splitContainer5.Name = "splitContainer5";
            splitContainer5.Orientation = Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            splitContainer5.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer5.Panel2
            // 
            splitContainer5.Panel2.Controls.Add(splitContainer6);
            splitContainer5.Size = new Size(1254, 739);
            splitContainer5.SplitterDistance = 250;
            splitContainer5.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.Controls.Add(label12, 3, 2);
            tableLayoutPanel1.Controls.Add(label11, 1, 2);
            tableLayoutPanel1.Controls.Add(lblComCycle, 3, 1);
            tableLayoutPanel1.Controls.Add(lblComID, 1, 1);
            tableLayoutPanel1.Controls.Add(lblPeerIP, 3, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 2, 0);
            tableLayoutPanel1.Controls.Add(label3, 0, 1);
            tableLayoutPanel1.Controls.Add(label4, 2, 1);
            tableLayoutPanel1.Controls.Add(label5, 0, 2);
            tableLayoutPanel1.Controls.Add(label6, 2, 2);
            tableLayoutPanel1.Controls.Add(lblIPAddress, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(1254, 250);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.None;
            label12.AutoSize = true;
            label12.Location = new Point(1173, 192);
            label12.Name = "label12";
            label12.Size = new Size(34, 31);
            label12.TabIndex = 11;
            label12.Text = "--";
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.None;
            label11.AutoSize = true;
            label11.Location = new Point(546, 192);
            label11.Name = "label11";
            label11.Size = new Size(34, 31);
            label11.TabIndex = 10;
            label11.Text = "--";
            // 
            // lblComCycle
            // 
            lblComCycle.Anchor = AnchorStyles.None;
            lblComCycle.AutoSize = true;
            lblComCycle.Location = new Point(1146, 109);
            lblComCycle.Name = "lblComCycle";
            lblComCycle.Size = new Size(89, 31);
            lblComCycle.TabIndex = 9;
            lblComCycle.Text = "100ms";
            // 
            // lblComID
            // 
            lblComID.Anchor = AnchorStyles.None;
            lblComID.AutoSize = true;
            lblComID.Location = new Point(546, 109);
            lblComID.Name = "lblComID";
            lblComID.Size = new Size(34, 31);
            lblComID.TabIndex = 8;
            lblComID.Text = "--";
            // 
            // lblPeerIP
            // 
            lblPeerIP.Anchor = AnchorStyles.None;
            lblPeerIP.AutoSize = true;
            lblPeerIP.Location = new Point(1173, 26);
            lblPeerIP.Name = "lblPeerIP";
            lblPeerIP.Size = new Size(34, 31);
            lblPeerIP.TabIndex = 7;
            lblPeerIP.Text = "--";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(3, 26);
            label1.Name = "label1";
            label1.Size = new Size(84, 31);
            label1.TabIndex = 0;
            label1.Text = "IP地址";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(629, 26);
            label2.Name = "label2";
            label2.Size = new Size(84, 31);
            label2.TabIndex = 1;
            label2.Text = "对端IP";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(3, 109);
            label3.Name = "label3";
            label3.Size = new Size(92, 31);
            label3.TabIndex = 2;
            label3.Text = "ComID";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(629, 109);
            label4.Name = "label4";
            label4.Size = new Size(110, 31);
            label4.TabIndex = 3;
            label4.Text = "通信周期";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(3, 192);
            label5.Name = "label5";
            label5.Size = new Size(110, 31);
            label5.TabIndex = 4;
            label5.Text = "通信状态";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new Point(629, 192);
            label6.Name = "label6";
            label6.Size = new Size(169, 31);
            label6.TabIndex = 5;
            label6.Text = "VSPS运行状态";
            // 
            // lblIPAddress
            // 
            lblIPAddress.Anchor = AnchorStyles.None;
            lblIPAddress.AutoSize = true;
            lblIPAddress.Location = new Point(546, 26);
            lblIPAddress.Name = "lblIPAddress";
            lblIPAddress.Size = new Size(34, 31);
            lblIPAddress.TabIndex = 6;
            lblIPAddress.Text = "--";
            // 
            // splitContainer6
            // 
            splitContainer6.Dock = DockStyle.Fill;
            splitContainer6.Location = new Point(0, 0);
            splitContainer6.Name = "splitContainer6";
            splitContainer6.Orientation = Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            splitContainer6.Panel1.Controls.Add(button1);
            // 
            // splitContainer6.Panel2
            // 
            splitContainer6.Panel2.Controls.Add(tableLayoutPanel2);
            splitContainer6.Size = new Size(1254, 485);
            splitContainer6.SplitterDistance = 179;
            splitContainer6.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(690, 60);
            button1.Name = "button1";
            button1.Size = new Size(150, 46);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.Controls.Add(lblRelaPos, 3, 1);
            tableLayoutPanel2.Controls.Add(lblSpeed, 1, 1);
            tableLayoutPanel2.Controls.Add(lblDirection, 3, 0);
            tableLayoutPanel2.Controls.Add(lblLifeCycle, 1, 0);
            tableLayoutPanel2.Controls.Add(label7, 0, 0);
            tableLayoutPanel2.Controls.Add(label8, 2, 0);
            tableLayoutPanel2.Controls.Add(label9, 0, 1);
            tableLayoutPanel2.Controls.Add(label10, 2, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.Size = new Size(1254, 302);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // lblRelaPos
            // 
            lblRelaPos.Anchor = AnchorStyles.None;
            lblRelaPos.AutoSize = true;
            lblRelaPos.Location = new Point(1173, 211);
            lblRelaPos.Name = "lblRelaPos";
            lblRelaPos.Size = new Size(34, 31);
            lblRelaPos.TabIndex = 15;
            lblRelaPos.Text = "--";
            // 
            // lblSpeed
            // 
            lblSpeed.Anchor = AnchorStyles.None;
            lblSpeed.AutoSize = true;
            lblSpeed.Location = new Point(546, 211);
            lblSpeed.Name = "lblSpeed";
            lblSpeed.Size = new Size(34, 31);
            lblSpeed.TabIndex = 14;
            lblSpeed.Text = "--";
            // 
            // lblDirection
            // 
            lblDirection.Anchor = AnchorStyles.None;
            lblDirection.AutoSize = true;
            lblDirection.Location = new Point(1173, 60);
            lblDirection.Name = "lblDirection";
            lblDirection.Size = new Size(34, 31);
            lblDirection.TabIndex = 13;
            lblDirection.Text = "--";
            // 
            // lblLifeCycle
            // 
            lblLifeCycle.Anchor = AnchorStyles.None;
            lblLifeCycle.AutoSize = true;
            lblLifeCycle.Location = new Point(546, 60);
            lblLifeCycle.Name = "lblLifeCycle";
            lblLifeCycle.Size = new Size(34, 31);
            lblLifeCycle.TabIndex = 12;
            lblLifeCycle.Text = "--";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Location = new Point(3, 60);
            label7.Name = "label7";
            label7.Size = new Size(110, 31);
            label7.TabIndex = 0;
            label7.Text = "生命周期";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Location = new Point(629, 60);
            label8.Name = "label8";
            label8.Size = new Size(110, 31);
            label8.TabIndex = 1;
            label8.Text = "运行方向";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Left;
            label9.AutoSize = true;
            label9.Location = new Point(3, 211);
            label9.Name = "label9";
            label9.Size = new Size(62, 31);
            label9.TabIndex = 2;
            label9.Text = "速度";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Left;
            label10.AutoSize = true;
            label10.Location = new Point(629, 211);
            label10.Name = "label10";
            label10.Size = new Size(110, 31);
            label10.TabIndex = 3;
            label10.Text = "相对位置";
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
            splitContainer2.Size = new Size(608, 911);
            splitContainer2.SplitterDistance = 614;
            splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(608, 614);
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
            groupBox3.Size = new Size(608, 293);
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
            VSPSdiagnose.Size = new Size(602, 256);
            VSPSdiagnose.TabIndex = 0;
            VSPSdiagnose.Text = "";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(splitContainer3);
            tabPage2.Location = new Point(8, 8);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1878, 917);
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
            splitContainer3.Size = new Size(1872, 911);
            splitContainer3.SplitterDistance = 940;
            splitContainer3.TabIndex = 0;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(VSPS);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(0, 0);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(940, 911);
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
            VSPS.Size = new Size(934, 874);
            VSPS.TabIndex = 0;
            VSPS.Text = "";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(VSPSSendMsg);
            groupBox5.Dock = DockStyle.Fill;
            groupBox5.Location = new Point(0, 0);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(928, 911);
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
            VSPSSendMsg.Size = new Size(922, 874);
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
            groupBox1.ResumeLayout(false);
            splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
            splitContainer4.ResumeLayout(false);
            splitContainer5.Panel1.ResumeLayout(false);
            splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer5).EndInit();
            splitContainer5.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            splitContainer6.Panel1.ResumeLayout(false);
            splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer6).EndInit();
            splitContainer6.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
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
        private SplitContainer splitContainer4;
        private SplitContainer splitContainer5;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private SplitContainer splitContainer6;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label12;
        private Label label11;
        private Label lblComCycle;
        private Label lblComID;
        private Label lblPeerIP;
        private Label lblIPAddress;
        private Label lblRelaPos;
        private Label lblSpeed;
        private Label lblDirection;
        private Label lblLifeCycle;
        private Button button1;
    }
}