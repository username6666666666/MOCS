namespace MOCS.Forms
{
    partial class MCU_UI
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

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
            splitContainerStatus = new SplitContainer();
            gbCommStatus = new GroupBox();
            tableLayoutPanelComm = new TableLayoutPanel();
            lblCh1 = new Label();
            pnlCh1Status = new Panel();
            lblCh2 = new Label();
            pnlCh2Status = new Panel();
            lblSendReason = new Label();
            lblMCUSendReason = new Label();
            lblMsgRepeat = new Label();
            lblMsgNumToRepeat = new Label();
            lblSeqRef = new Label();
            lblSequenceNumRefPoint = new Label();
            lblReadiness = new Label();
            lblMCUStatusChangeReadiness = new Label();
            splitContainerMid = new SplitContainer();
            gbCurrentVehicle = new GroupBox();
            tableLayoutPanelCurrent = new TableLayoutPanel();
            lblCurrentId = new Label();
            lblCurrentMaglevVehicleIdentifier = new Label();
            lblCurrentClear = new Label();
            pnlClearCurrent = new Panel();
            lblDCSNum = new Label();
            lblCurrentPos = new Label();
            lblCurrentMaglevVehiclePos = new Label();
            lblCurrentVel = new Label();
            lblCurrentMaglevVehicleVelocity = new Label();
            lblCurrentAcc = new Label();
            lblCurrentMaglevVehicleAcc = new Label();
            lblCurrentTraction = new Label();
            lblTractionCapacityCurrent = new Label();
            lblCurrentSPR = new Label();
            lblCurrentMaglevVehicleSPRStatus = new Label();
            splitContainerBottom = new SplitContainer();
            gbVirtualVehicle = new GroupBox();
            tableLayoutPanelVirtual = new TableLayoutPanel();
            lblVirtualId = new Label();
            lblVirtualMaglevVehicleIdentifier = new Label();
            lblVirtualClear = new Label();
            pnlClearVirtual = new Panel();
            lblVirtualTraction = new Label();
            lblTractionCapacityVirtual = new Label();
            gbFaultParking = new GroupBox();
            tableLayoutPanelFault = new TableLayoutPanel();
            lblFaultStatus = new Label();
            lblMCUFaultStatus = new Label();
            lblParkingNum = new Label();
            lblParkingPointsNum = new Label();
            lblCurrentKI = new Label();
            lblCurrentKIIdentifiers = new Label();
            lblVirtualKI = new Label();
            lblVirtualKIIdentifiers = new Label();
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
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerStatus).BeginInit();
            splitContainerStatus.Panel1.SuspendLayout();
            splitContainerStatus.Panel2.SuspendLayout();
            splitContainerStatus.SuspendLayout();
            gbCommStatus.SuspendLayout();
            tableLayoutPanelComm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMid).BeginInit();
            splitContainerMid.Panel1.SuspendLayout();
            splitContainerMid.Panel2.SuspendLayout();
            splitContainerMid.SuspendLayout();
            gbCurrentVehicle.SuspendLayout();
            tableLayoutPanelCurrent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerBottom).BeginInit();
            splitContainerBottom.Panel1.SuspendLayout();
            splitContainerBottom.Panel2.SuspendLayout();
            splitContainerBottom.SuspendLayout();
            gbVirtualVehicle.SuspendLayout();
            tableLayoutPanelVirtual.SuspendLayout();
            gbFaultParking.SuspendLayout();
            tableLayoutPanelFault.SuspendLayout();
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
            splitContainer1.SplitterDistance = 1250;
            splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.Window;
            groupBox1.Controls.Add(splitContainerStatus);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1250, 911);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "状态信息";
            // 
            // splitContainerStatus
            // 
            splitContainerStatus.Dock = DockStyle.Fill;
            splitContainerStatus.Location = new Point(3, 34);
            splitContainerStatus.Name = "splitContainerStatus";
            splitContainerStatus.Orientation = Orientation.Horizontal;
            // 
            // splitContainerStatus.Panel1
            // 
            splitContainerStatus.Panel1.Controls.Add(gbCommStatus);
            // 
            // splitContainerStatus.Panel2
            // 
            splitContainerStatus.Panel2.Controls.Add(splitContainerMid);
            splitContainerStatus.Size = new Size(1244, 874);
            splitContainerStatus.SplitterDistance = 144;
            splitContainerStatus.TabIndex = 0;
            // 
            // gbCommStatus
            // 
            gbCommStatus.Controls.Add(tableLayoutPanelComm);
            gbCommStatus.Dock = DockStyle.Fill;
            gbCommStatus.Location = new Point(0, 0);
            gbCommStatus.Name = "gbCommStatus";
            gbCommStatus.Size = new Size(1244, 144);
            gbCommStatus.TabIndex = 0;
            gbCommStatus.TabStop = false;
            gbCommStatus.Text = "通讯状态";
            // 
            // tableLayoutPanelComm
            // 
            tableLayoutPanelComm.ColumnCount = 4;
            tableLayoutPanelComm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelComm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelComm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelComm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelComm.Controls.Add(lblCh1, 0, 0);
            tableLayoutPanelComm.Controls.Add(pnlCh1Status, 1, 0);
            tableLayoutPanelComm.Controls.Add(lblCh2, 2, 0);
            tableLayoutPanelComm.Controls.Add(pnlCh2Status, 3, 0);
            tableLayoutPanelComm.Controls.Add(lblSendReason, 0, 1);
            tableLayoutPanelComm.Controls.Add(lblMCUSendReason, 1, 1);
            tableLayoutPanelComm.Controls.Add(lblMsgRepeat, 2, 1);
            tableLayoutPanelComm.Controls.Add(lblMsgNumToRepeat, 3, 1);
            tableLayoutPanelComm.Controls.Add(lblSeqRef, 0, 2);
            tableLayoutPanelComm.Controls.Add(lblSequenceNumRefPoint, 1, 2);
            tableLayoutPanelComm.Controls.Add(lblReadiness, 2, 2);
            tableLayoutPanelComm.Controls.Add(lblMCUStatusChangeReadiness, 3, 2);
            tableLayoutPanelComm.Dock = DockStyle.Fill;
            tableLayoutPanelComm.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            tableLayoutPanelComm.Location = new Point(3, 34);
            tableLayoutPanelComm.Name = "tableLayoutPanelComm";
            tableLayoutPanelComm.RowCount = 3;
            tableLayoutPanelComm.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelComm.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelComm.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanelComm.Size = new Size(1238, 107);
            tableLayoutPanelComm.TabIndex = 0;
            // 
            // lblCh1
            // 
            lblCh1.Anchor = AnchorStyles.Left;
            lblCh1.AutoSize = true;
            lblCh1.Location = new Point(3, 2);
            lblCh1.Name = "lblCh1";
            lblCh1.Size = new Size(172, 31);
            lblCh1.TabIndex = 0;
            lblCh1.Text = "接收通道1状态";
            // 
            // pnlCh1Status
            // 
            pnlCh1Status.Anchor = AnchorStyles.None;
            pnlCh1Status.BackColor = Color.Red;
            pnlCh1Status.Location = new Point(448, 3);
            pnlCh1Status.Name = "pnlCh1Status";
            pnlCh1Status.Size = new Size(30, 29);
            pnlCh1Status.TabIndex = 1;
            // 
            // lblCh2
            // 
            lblCh2.Anchor = AnchorStyles.Left;
            lblCh2.AutoSize = true;
            lblCh2.Location = new Point(621, 2);
            lblCh2.Name = "lblCh2";
            lblCh2.Size = new Size(172, 31);
            lblCh2.TabIndex = 2;
            lblCh2.Text = "接收通道2状态";
            // 
            // pnlCh2Status
            // 
            pnlCh2Status.Anchor = AnchorStyles.None;
            pnlCh2Status.BackColor = Color.Red;
            pnlCh2Status.Location = new Point(1067, 3);
            pnlCh2Status.Name = "pnlCh2Status";
            pnlCh2Status.Size = new Size(30, 29);
            pnlCh2Status.TabIndex = 3;
            // 
            // lblSendReason
            // 
            lblSendReason.Anchor = AnchorStyles.Left;
            lblSendReason.AutoSize = true;
            lblSendReason.Location = new Point(3, 37);
            lblSendReason.Name = "lblSendReason";
            lblSendReason.Size = new Size(158, 31);
            lblSendReason.TabIndex = 4;
            lblSendReason.Text = "报文发送理由";
            // 
            // lblMCUSendReason
            // 
            lblMCUSendReason.Anchor = AnchorStyles.Left;
            lblMCUSendReason.AutoSize = true;
            lblMCUSendReason.Location = new Point(312, 37);
            lblMCUSendReason.Name = "lblMCUSendReason";
            lblMCUSendReason.Size = new Size(34, 31);
            lblMCUSendReason.TabIndex = 5;
            lblMCUSendReason.Text = "--";
            // 
            // lblMsgRepeat
            // 
            lblMsgRepeat.Anchor = AnchorStyles.Left;
            lblMsgRepeat.AutoSize = true;
            lblMsgRepeat.Location = new Point(621, 37);
            lblMsgRepeat.Name = "lblMsgRepeat";
            lblMsgRepeat.Size = new Size(254, 31);
            lblMsgRepeat.TabIndex = 6;
            lblMsgRepeat.Text = "需要重复发送报文数量";
            // 
            // lblMsgNumToRepeat
            // 
            lblMsgNumToRepeat.Anchor = AnchorStyles.Left;
            lblMsgNumToRepeat.AutoSize = true;
            lblMsgNumToRepeat.Location = new Point(930, 37);
            lblMsgNumToRepeat.Name = "lblMsgNumToRepeat";
            lblMsgNumToRepeat.Size = new Size(34, 31);
            lblMsgNumToRepeat.TabIndex = 7;
            lblMsgNumToRepeat.Text = "--";
            // 
            // lblSeqRef
            // 
            lblSeqRef.Anchor = AnchorStyles.Left;
            lblSeqRef.AutoSize = true;
            lblSeqRef.Location = new Point(3, 73);
            lblSeqRef.Name = "lblSeqRef";
            lblSeqRef.Size = new Size(199, 31);
            lblSeqRef.TabIndex = 8;
            lblSeqRef.Text = "A类序列号参考点";
            // 
            // lblSequenceNumRefPoint
            // 
            lblSequenceNumRefPoint.Anchor = AnchorStyles.Left;
            lblSequenceNumRefPoint.AutoSize = true;
            lblSequenceNumRefPoint.Location = new Point(312, 73);
            lblSequenceNumRefPoint.Name = "lblSequenceNumRefPoint";
            lblSequenceNumRefPoint.Size = new Size(34, 31);
            lblSequenceNumRefPoint.TabIndex = 9;
            lblSequenceNumRefPoint.Text = "--";
            // 
            // lblReadiness
            // 
            lblReadiness.Anchor = AnchorStyles.Left;
            lblReadiness.AutoSize = true;
            lblReadiness.Location = new Point(621, 73);
            lblReadiness.Name = "lblReadiness";
            lblReadiness.Size = new Size(254, 31);
            lblReadiness.TabIndex = 10;
            lblReadiness.Text = "牵引状态改变就绪信息";
            // 
            // lblMCUStatusChangeReadiness
            // 
            lblMCUStatusChangeReadiness.Anchor = AnchorStyles.Left;
            lblMCUStatusChangeReadiness.AutoSize = true;
            lblMCUStatusChangeReadiness.Location = new Point(930, 73);
            lblMCUStatusChangeReadiness.Name = "lblMCUStatusChangeReadiness";
            lblMCUStatusChangeReadiness.Size = new Size(34, 31);
            lblMCUStatusChangeReadiness.TabIndex = 11;
            lblMCUStatusChangeReadiness.Text = "--";
            // 
            // splitContainerMid
            // 
            splitContainerMid.Dock = DockStyle.Fill;
            splitContainerMid.Location = new Point(0, 0);
            splitContainerMid.Name = "splitContainerMid";
            // 
            // splitContainerMid.Panel1
            // 
            splitContainerMid.Panel1.Controls.Add(gbCurrentVehicle);
            // 
            // splitContainerMid.Panel2
            // 
            splitContainerMid.Panel2.Controls.Add(splitContainerBottom);
            splitContainerMid.Size = new Size(1244, 726);
            splitContainerMid.SplitterDistance = 600;
            splitContainerMid.TabIndex = 0;
            // 
            // gbCurrentVehicle
            // 
            gbCurrentVehicle.Controls.Add(tableLayoutPanelCurrent);
            gbCurrentVehicle.Dock = DockStyle.Fill;
            gbCurrentVehicle.Location = new Point(0, 0);
            gbCurrentVehicle.Name = "gbCurrentVehicle";
            gbCurrentVehicle.Size = new Size(600, 726);
            gbCurrentVehicle.TabIndex = 1;
            gbCurrentVehicle.TabStop = false;
            gbCurrentVehicle.Text = "当前悬浮架";
            // 
            // tableLayoutPanelCurrent
            // 
            tableLayoutPanelCurrent.ColumnCount = 4;
            tableLayoutPanelCurrent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelCurrent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelCurrent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelCurrent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelCurrent.Controls.Add(lblCurrentId, 0, 0);
            tableLayoutPanelCurrent.Controls.Add(lblCurrentMaglevVehicleIdentifier, 1, 0);
            tableLayoutPanelCurrent.Controls.Add(lblCurrentClear, 2, 0);
            tableLayoutPanelCurrent.Controls.Add(pnlClearCurrent, 3, 0);
            tableLayoutPanelCurrent.Controls.Add(lblCurrentPos, 2, 1);
            tableLayoutPanelCurrent.Controls.Add(lblCurrentMaglevVehiclePos, 3, 1);
            tableLayoutPanelCurrent.Controls.Add(lblCurrentVel, 0, 2);
            tableLayoutPanelCurrent.Controls.Add(lblCurrentMaglevVehicleVelocity, 1, 2);
            tableLayoutPanelCurrent.Controls.Add(lblCurrentAcc, 2, 2);
            tableLayoutPanelCurrent.Controls.Add(lblCurrentMaglevVehicleAcc, 3, 2);
            tableLayoutPanelCurrent.Controls.Add(lblCurrentTraction, 0, 3);
            tableLayoutPanelCurrent.Controls.Add(lblTractionCapacityCurrent, 1, 3);
            tableLayoutPanelCurrent.Controls.Add(lblCurrentSPR, 2, 3);
            tableLayoutPanelCurrent.Controls.Add(lblCurrentMaglevVehicleSPRStatus, 3, 3);
            tableLayoutPanelCurrent.Controls.Add(lblDCSNum, 0, 1);
            tableLayoutPanelCurrent.Dock = DockStyle.Fill;
            tableLayoutPanelCurrent.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            tableLayoutPanelCurrent.Location = new Point(3, 34);
            tableLayoutPanelCurrent.Name = "tableLayoutPanelCurrent";
            tableLayoutPanelCurrent.RowCount = 4;
            tableLayoutPanelCurrent.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanelCurrent.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanelCurrent.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanelCurrent.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanelCurrent.Size = new Size(594, 689);
            tableLayoutPanelCurrent.TabIndex = 0;
            // 
            // lblCurrentId
            // 
            lblCurrentId.Anchor = AnchorStyles.Left;
            lblCurrentId.AutoSize = true;
            lblCurrentId.Location = new Point(3, 55);
            lblCurrentId.Name = "lblCurrentId";
            lblCurrentId.Size = new Size(134, 62);
            lblCurrentId.TabIndex = 0;
            lblCurrentId.Text = "悬浮架标识号";
            // 
            // lblCurrentMaglevVehicleIdentifier
            // 
            lblCurrentMaglevVehicleIdentifier.Anchor = AnchorStyles.Left;
            lblCurrentMaglevVehicleIdentifier.AutoSize = true;
            lblCurrentMaglevVehicleIdentifier.Location = new Point(151, 70);
            lblCurrentMaglevVehicleIdentifier.Name = "lblCurrentMaglevVehicleIdentifier";
            lblCurrentMaglevVehicleIdentifier.Size = new Size(34, 31);
            lblCurrentMaglevVehicleIdentifier.TabIndex = 1;
            lblCurrentMaglevVehicleIdentifier.Text = "--";
            // 
            // lblCurrentClear
            // 
            lblCurrentClear.Anchor = AnchorStyles.Left;
            lblCurrentClear.AutoSize = true;
            lblCurrentClear.Location = new Point(299, 55);
            lblCurrentClear.Name = "lblCurrentClear";
            lblCurrentClear.Size = new Size(134, 62);
            lblCurrentClear.TabIndex = 2;
            lblCurrentClear.Text = "清除就绪信息标志";
            // 
            // pnlClearCurrent
            // 
            pnlClearCurrent.Anchor = AnchorStyles.None;
            pnlClearCurrent.BackColor = Color.Red;
            pnlClearCurrent.Location = new Point(504, 71);
            pnlClearCurrent.Name = "pnlClearCurrent";
            pnlClearCurrent.Size = new Size(30, 30);
            pnlClearCurrent.TabIndex = 3;
            // 
            // lblDCSNum
            // 
            lblDCSNum.Anchor = AnchorStyles.Left;
            lblDCSNum.AutoSize = true;
            lblDCSNum.Location = new Point(3, 242);
            lblDCSNum.Name = "lblDCSNum";
            lblDCSNum.Size = new Size(110, 31);
            lblDCSNum.TabIndex = 4;
            lblDCSNum.Text = "DCS编号";
            // 
            // lblCurrentPos
            // 
            lblCurrentPos.Anchor = AnchorStyles.Left;
            lblCurrentPos.AutoSize = true;
            lblCurrentPos.Location = new Point(299, 227);
            lblCurrentPos.Name = "lblCurrentPos";
            lblCurrentPos.Size = new Size(110, 62);
            lblCurrentPos.TabIndex = 6;
            lblCurrentPos.Text = "当前位置(cm)";
            // 
            // lblCurrentMaglevVehiclePos
            // 
            lblCurrentMaglevVehiclePos.Anchor = AnchorStyles.Left;
            lblCurrentMaglevVehiclePos.AutoSize = true;
            lblCurrentMaglevVehiclePos.Location = new Point(447, 242);
            lblCurrentMaglevVehiclePos.Name = "lblCurrentMaglevVehiclePos";
            lblCurrentMaglevVehiclePos.Size = new Size(34, 31);
            lblCurrentMaglevVehiclePos.TabIndex = 7;
            lblCurrentMaglevVehiclePos.Text = "--";
            // 
            // lblCurrentVel
            // 
            lblCurrentVel.Anchor = AnchorStyles.Left;
            lblCurrentVel.AutoSize = true;
            lblCurrentVel.Location = new Point(3, 399);
            lblCurrentVel.Name = "lblCurrentVel";
            lblCurrentVel.Size = new Size(110, 62);
            lblCurrentVel.TabIndex = 8;
            lblCurrentVel.Text = "实际速度(cm/s)";
            // 
            // lblCurrentMaglevVehicleVelocity
            // 
            lblCurrentMaglevVehicleVelocity.Anchor = AnchorStyles.Left;
            lblCurrentMaglevVehicleVelocity.AutoSize = true;
            lblCurrentMaglevVehicleVelocity.Location = new Point(151, 414);
            lblCurrentMaglevVehicleVelocity.Name = "lblCurrentMaglevVehicleVelocity";
            lblCurrentMaglevVehicleVelocity.Size = new Size(34, 31);
            lblCurrentMaglevVehicleVelocity.TabIndex = 9;
            lblCurrentMaglevVehicleVelocity.Text = "--";
            // 
            // lblCurrentAcc
            // 
            lblCurrentAcc.Anchor = AnchorStyles.Left;
            lblCurrentAcc.AutoSize = true;
            lblCurrentAcc.Location = new Point(299, 399);
            lblCurrentAcc.Name = "lblCurrentAcc";
            lblCurrentAcc.Size = new Size(134, 62);
            lblCurrentAcc.TabIndex = 10;
            lblCurrentAcc.Text = "实际加速度(cm/s²)";
            // 
            // lblCurrentMaglevVehicleAcc
            // 
            lblCurrentMaglevVehicleAcc.Anchor = AnchorStyles.Left;
            lblCurrentMaglevVehicleAcc.AutoSize = true;
            lblCurrentMaglevVehicleAcc.Location = new Point(447, 414);
            lblCurrentMaglevVehicleAcc.Name = "lblCurrentMaglevVehicleAcc";
            lblCurrentMaglevVehicleAcc.Size = new Size(34, 31);
            lblCurrentMaglevVehicleAcc.TabIndex = 11;
            lblCurrentMaglevVehicleAcc.Text = "--";
            // 
            // lblCurrentTraction
            // 
            lblCurrentTraction.Anchor = AnchorStyles.Left;
            lblCurrentTraction.AutoSize = true;
            lblCurrentTraction.Location = new Point(3, 571);
            lblCurrentTraction.Name = "lblCurrentTraction";
            lblCurrentTraction.Size = new Size(134, 62);
            lblCurrentTraction.TabIndex = 12;
            lblCurrentTraction.Text = "牵引能力百分比(%)";
            // 
            // lblTractionCapacityCurrent
            // 
            lblTractionCapacityCurrent.Anchor = AnchorStyles.Left;
            lblTractionCapacityCurrent.AutoSize = true;
            lblTractionCapacityCurrent.Location = new Point(151, 587);
            lblTractionCapacityCurrent.Name = "lblTractionCapacityCurrent";
            lblTractionCapacityCurrent.Size = new Size(34, 31);
            lblTractionCapacityCurrent.TabIndex = 13;
            lblTractionCapacityCurrent.Text = "--";
            // 
            // lblCurrentSPR
            // 
            lblCurrentSPR.Anchor = AnchorStyles.Left;
            lblCurrentSPR.AutoSize = true;
            lblCurrentSPR.Location = new Point(299, 571);
            lblCurrentSPR.Name = "lblCurrentSPR";
            lblCurrentSPR.Size = new Size(134, 62);
            lblCurrentSPR.TabIndex = 14;
            lblCurrentSPR.Text = "停车点运行状态";
            // 
            // lblCurrentMaglevVehicleSPRStatus
            // 
            lblCurrentMaglevVehicleSPRStatus.Anchor = AnchorStyles.Left;
            lblCurrentMaglevVehicleSPRStatus.AutoSize = true;
            lblCurrentMaglevVehicleSPRStatus.Location = new Point(447, 587);
            lblCurrentMaglevVehicleSPRStatus.Name = "lblCurrentMaglevVehicleSPRStatus";
            lblCurrentMaglevVehicleSPRStatus.Size = new Size(34, 31);
            lblCurrentMaglevVehicleSPRStatus.TabIndex = 15;
            lblCurrentMaglevVehicleSPRStatus.Text = "--";
            // 
            // splitContainerBottom
            // 
            splitContainerBottom.Dock = DockStyle.Fill;
            splitContainerBottom.Location = new Point(0, 0);
            splitContainerBottom.Name = "splitContainerBottom";
            // 
            // splitContainerBottom.Panel1
            // 
            splitContainerBottom.Panel1.Controls.Add(gbVirtualVehicle);
            // 
            // splitContainerBottom.Panel2
            // 
            splitContainerBottom.Panel2.Controls.Add(gbFaultParking);
            splitContainerBottom.Size = new Size(640, 726);
            splitContainerBottom.SplitterDistance = 320;
            splitContainerBottom.TabIndex = 0;
            // 
            // gbVirtualVehicle
            // 
            gbVirtualVehicle.Controls.Add(tableLayoutPanelVirtual);
            gbVirtualVehicle.Dock = DockStyle.Fill;
            gbVirtualVehicle.Location = new Point(0, 0);
            gbVirtualVehicle.Name = "gbVirtualVehicle";
            gbVirtualVehicle.Size = new Size(320, 726);
            gbVirtualVehicle.TabIndex = 2;
            gbVirtualVehicle.TabStop = false;
            gbVirtualVehicle.Text = "虚拟悬浮架";
            // 
            // tableLayoutPanelVirtual
            // 
            tableLayoutPanelVirtual.ColumnCount = 4;
            tableLayoutPanelVirtual.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelVirtual.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelVirtual.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelVirtual.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelVirtual.Controls.Add(lblVirtualId, 0, 0);
            tableLayoutPanelVirtual.Controls.Add(lblVirtualMaglevVehicleIdentifier, 1, 0);
            tableLayoutPanelVirtual.Controls.Add(lblVirtualClear, 2, 0);
            tableLayoutPanelVirtual.Controls.Add(pnlClearVirtual, 3, 0);
            tableLayoutPanelVirtual.Controls.Add(lblVirtualTraction, 0, 1);
            tableLayoutPanelVirtual.Controls.Add(lblTractionCapacityVirtual, 1, 1);
            tableLayoutPanelVirtual.Dock = DockStyle.Fill;
            tableLayoutPanelVirtual.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            tableLayoutPanelVirtual.Location = new Point(3, 34);
            tableLayoutPanelVirtual.Name = "tableLayoutPanelVirtual";
            tableLayoutPanelVirtual.RowCount = 2;
            tableLayoutPanelVirtual.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelVirtual.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelVirtual.Size = new Size(314, 689);
            tableLayoutPanelVirtual.TabIndex = 0;
            // 
            // lblVirtualId
            // 
            lblVirtualId.Anchor = AnchorStyles.Left;
            lblVirtualId.AutoSize = true;
            lblVirtualId.Location = new Point(3, 125);
            lblVirtualId.Name = "lblVirtualId";
            lblVirtualId.Size = new Size(62, 93);
            lblVirtualId.TabIndex = 0;
            lblVirtualId.Text = "悬浮架标识号";
            // 
            // lblVirtualMaglevVehicleIdentifier
            // 
            lblVirtualMaglevVehicleIdentifier.Anchor = AnchorStyles.Left;
            lblVirtualMaglevVehicleIdentifier.AutoSize = true;
            lblVirtualMaglevVehicleIdentifier.Location = new Point(81, 156);
            lblVirtualMaglevVehicleIdentifier.Name = "lblVirtualMaglevVehicleIdentifier";
            lblVirtualMaglevVehicleIdentifier.Size = new Size(34, 31);
            lblVirtualMaglevVehicleIdentifier.TabIndex = 1;
            lblVirtualMaglevVehicleIdentifier.Text = "--";
            // 
            // lblVirtualClear
            // 
            lblVirtualClear.Anchor = AnchorStyles.Left;
            lblVirtualClear.AutoSize = true;
            lblVirtualClear.Location = new Point(159, 110);
            lblVirtualClear.Name = "lblVirtualClear";
            lblVirtualClear.Size = new Size(62, 124);
            lblVirtualClear.TabIndex = 2;
            lblVirtualClear.Text = "清除就绪信息标志";
            // 
            // pnlClearVirtual
            // 
            pnlClearVirtual.Anchor = AnchorStyles.None;
            pnlClearVirtual.BackColor = Color.Red;
            pnlClearVirtual.Location = new Point(259, 157);
            pnlClearVirtual.Name = "pnlClearVirtual";
            pnlClearVirtual.Size = new Size(30, 30);
            pnlClearVirtual.TabIndex = 3;
            // 
            // lblVirtualTraction
            // 
            lblVirtualTraction.Anchor = AnchorStyles.Left;
            lblVirtualTraction.AutoSize = true;
            lblVirtualTraction.Location = new Point(3, 439);
            lblVirtualTraction.Name = "lblVirtualTraction";
            lblVirtualTraction.Size = new Size(62, 155);
            lblVirtualTraction.TabIndex = 4;
            lblVirtualTraction.Text = "牵引能力百分比(%)";
            // 
            // lblTractionCapacityVirtual
            // 
            lblTractionCapacityVirtual.Anchor = AnchorStyles.Left;
            lblTractionCapacityVirtual.AutoSize = true;
            lblTractionCapacityVirtual.Location = new Point(81, 501);
            lblTractionCapacityVirtual.Name = "lblTractionCapacityVirtual";
            lblTractionCapacityVirtual.Size = new Size(34, 31);
            lblTractionCapacityVirtual.TabIndex = 5;
            lblTractionCapacityVirtual.Text = "--";
            // 
            // gbFaultParking
            // 
            gbFaultParking.Controls.Add(tableLayoutPanelFault);
            gbFaultParking.Dock = DockStyle.Fill;
            gbFaultParking.Location = new Point(0, 0);
            gbFaultParking.Name = "gbFaultParking";
            gbFaultParking.Size = new Size(316, 726);
            gbFaultParking.TabIndex = 3;
            gbFaultParking.TabStop = false;
            gbFaultParking.Text = "故障与停车点";
            // 
            // tableLayoutPanelFault
            // 
            tableLayoutPanelFault.ColumnCount = 4;
            tableLayoutPanelFault.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelFault.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelFault.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelFault.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelFault.Controls.Add(lblFaultStatus, 0, 0);
            tableLayoutPanelFault.Controls.Add(lblMCUFaultStatus, 1, 0);
            tableLayoutPanelFault.Controls.Add(lblParkingNum, 2, 0);
            tableLayoutPanelFault.Controls.Add(lblParkingPointsNum, 3, 0);
            tableLayoutPanelFault.Controls.Add(lblCurrentKI, 0, 1);
            tableLayoutPanelFault.Controls.Add(lblCurrentKIIdentifiers, 1, 1);
            tableLayoutPanelFault.Controls.Add(lblVirtualKI, 2, 1);
            tableLayoutPanelFault.Controls.Add(lblVirtualKIIdentifiers, 3, 1);
            tableLayoutPanelFault.Dock = DockStyle.Fill;
            tableLayoutPanelFault.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            tableLayoutPanelFault.Location = new Point(3, 34);
            tableLayoutPanelFault.Name = "tableLayoutPanelFault";
            tableLayoutPanelFault.RowCount = 2;
            tableLayoutPanelFault.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelFault.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelFault.Size = new Size(310, 689);
            tableLayoutPanelFault.TabIndex = 0;
            // 
            // lblFaultStatus
            // 
            lblFaultStatus.Anchor = AnchorStyles.Left;
            lblFaultStatus.AutoSize = true;
            lblFaultStatus.Location = new Point(3, 125);
            lblFaultStatus.Name = "lblFaultStatus";
            lblFaultStatus.Size = new Size(62, 93);
            lblFaultStatus.TabIndex = 0;
            lblFaultStatus.Text = "牵引故障状态";
            // 
            // lblMCUFaultStatus
            // 
            lblMCUFaultStatus.Anchor = AnchorStyles.Left;
            lblMCUFaultStatus.AutoSize = true;
            lblMCUFaultStatus.Location = new Point(80, 156);
            lblMCUFaultStatus.Name = "lblMCUFaultStatus";
            lblMCUFaultStatus.Size = new Size(34, 31);
            lblMCUFaultStatus.TabIndex = 1;
            lblMCUFaultStatus.Text = "--";
            // 
            // lblParkingNum
            // 
            lblParkingNum.Anchor = AnchorStyles.Left;
            lblParkingNum.AutoSize = true;
            lblParkingNum.Location = new Point(157, 110);
            lblParkingNum.Name = "lblParkingNum";
            lblParkingNum.Size = new Size(62, 124);
            lblParkingNum.TabIndex = 2;
            lblParkingNum.Text = "停车点运行数量";
            // 
            // lblParkingPointsNum
            // 
            lblParkingPointsNum.Anchor = AnchorStyles.Left;
            lblParkingPointsNum.AutoSize = true;
            lblParkingPointsNum.Location = new Point(234, 156);
            lblParkingPointsNum.Name = "lblParkingPointsNum";
            lblParkingPointsNum.Size = new Size(34, 31);
            lblParkingPointsNum.TabIndex = 3;
            lblParkingPointsNum.Text = "--";
            // 
            // lblCurrentKI
            // 
            lblCurrentKI.Anchor = AnchorStyles.Left;
            lblCurrentKI.AutoSize = true;
            lblCurrentKI.Location = new Point(3, 454);
            lblCurrentKI.Name = "lblCurrentKI";
            lblCurrentKI.Size = new Size(68, 124);
            lblCurrentKI.TabIndex = 4;
            lblCurrentKI.Text = "当前KI标识符(5个)";
            // 
            // lblCurrentKIIdentifiers
            // 
            lblCurrentKIIdentifiers.Anchor = AnchorStyles.Left;
            lblCurrentKIIdentifiers.AutoSize = true;
            lblCurrentKIIdentifiers.Location = new Point(80, 501);
            lblCurrentKIIdentifiers.Name = "lblCurrentKIIdentifiers";
            lblCurrentKIIdentifiers.Size = new Size(34, 31);
            lblCurrentKIIdentifiers.TabIndex = 5;
            lblCurrentKIIdentifiers.Text = "--";
            // 
            // lblVirtualKI
            // 
            lblVirtualKI.Anchor = AnchorStyles.Left;
            lblVirtualKI.AutoSize = true;
            lblVirtualKI.Location = new Point(157, 454);
            lblVirtualKI.Name = "lblVirtualKI";
            lblVirtualKI.Size = new Size(68, 124);
            lblVirtualKI.TabIndex = 6;
            lblVirtualKI.Text = "虚拟KI标识符(5个)";
            // 
            // lblVirtualKIIdentifiers
            // 
            lblVirtualKIIdentifiers.Anchor = AnchorStyles.Left;
            lblVirtualKIIdentifiers.AutoSize = true;
            lblVirtualKIIdentifiers.Location = new Point(234, 501);
            lblVirtualKIIdentifiers.Name = "lblVirtualKIIdentifiers";
            lblVirtualKIIdentifiers.Size = new Size(34, 31);
            lblVirtualKIIdentifiers.TabIndex = 7;
            lblVirtualKIIdentifiers.Text = "--";
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
            splitContainer2.Size = new Size(618, 911);
            splitContainer2.SplitterDistance = 599;
            splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(618, 599);
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
            groupBox3.Size = new Size(618, 308);
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
            MCUdiagnose.Size = new Size(612, 271);
            MCUdiagnose.TabIndex = 0;
            MCUdiagnose.Text = "";
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
            splitContainer3.SplitterDistance = 893;
            splitContainer3.TabIndex = 0;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(MCURecvMsg);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(0, 0);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(893, 911);
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
            MCURecvMsg.Size = new Size(887, 874);
            MCURecvMsg.TabIndex = 0;
            MCURecvMsg.Text = "";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(MCUSendMsg);
            groupBox5.Dock = DockStyle.Fill;
            groupBox5.Location = new Point(0, 0);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(975, 911);
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
            MCUSendMsg.Size = new Size(969, 874);
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
            Text = "MCU";
            MCUmenuStrip.ResumeLayout(false);
            MCUmenuStrip.PerformLayout();
            MCUpanel.ResumeLayout(false);
            MCUtabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            splitContainerStatus.Panel1.ResumeLayout(false);
            splitContainerStatus.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerStatus).EndInit();
            splitContainerStatus.ResumeLayout(false);
            gbCommStatus.ResumeLayout(false);
            tableLayoutPanelComm.ResumeLayout(false);
            tableLayoutPanelComm.PerformLayout();
            splitContainerMid.Panel1.ResumeLayout(false);
            splitContainerMid.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMid).EndInit();
            splitContainerMid.ResumeLayout(false);
            gbCurrentVehicle.ResumeLayout(false);
            tableLayoutPanelCurrent.ResumeLayout(false);
            tableLayoutPanelCurrent.PerformLayout();
            splitContainerBottom.Panel1.ResumeLayout(false);
            splitContainerBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerBottom).EndInit();
            splitContainerBottom.ResumeLayout(false);
            gbVirtualVehicle.ResumeLayout(false);
            tableLayoutPanelVirtual.ResumeLayout(false);
            tableLayoutPanelVirtual.PerformLayout();
            gbFaultParking.ResumeLayout(false);
            tableLayoutPanelFault.ResumeLayout(false);
            tableLayoutPanelFault.PerformLayout();
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
        private ToolStripMenuItem mOCSToolStripMenuItem;
        private ToolStripMenuItem mCUToolStripMenuItem;
        private ToolStripMenuItem lCUToolStripMenuItem;
        private ToolStripMenuItem gCUToolStripMenuItem;
        private ToolStripMenuItem vSPSToolStripMenuItem;
        private ToolStripMenuItem oBCToolStripMenuItem;
        private Panel MCUpanel;
        private TabControl MCUtabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private RichTextBox MCUdiagnose;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private RichTextBox MCURecvMsg;
        private RichTextBox MCUSendMsg;
        // 状态信息内部容器
        private SplitContainer splitContainerStatus;
        private SplitContainer splitContainerMid;
        private SplitContainer splitContainerBottom;
        // 通讯状态
        private GroupBox gbCommStatus;
        private TableLayoutPanel tableLayoutPanelComm;
        private Label lblCh1;
        private Panel pnlCh1Status;
        private Label lblCh2;
        private Panel pnlCh2Status;
        private Label lblSendReason;
        private Label lblMCUSendReason;
        private Label lblMsgRepeat;
        private Label lblMsgNumToRepeat;
        private Label lblSeqRef;
        private Label lblSequenceNumRefPoint;
        private Label lblReadiness;
        private Label lblMCUStatusChangeReadiness;
        // 当前悬浮架
        private GroupBox gbCurrentVehicle;
        private TableLayoutPanel tableLayoutPanelCurrent;
        private Label lblCurrentId;
        private Label lblCurrentMaglevVehicleIdentifier;
        private Label lblCurrentClear;
        private Panel pnlClearCurrent;
        private Label lblDCSNum;
        private Label lblCurrentPos;
        private Label lblCurrentMaglevVehiclePos;
        private Label lblCurrentVel;
        private Label lblCurrentMaglevVehicleVelocity;
        private Label lblCurrentAcc;
        private Label lblCurrentMaglevVehicleAcc;
        private Label lblCurrentTraction;
        private Label lblTractionCapacityCurrent;
        private Label lblCurrentSPR;
        private Label lblCurrentMaglevVehicleSPRStatus;
        // 虚拟悬浮架
        private GroupBox gbVirtualVehicle;
        private TableLayoutPanel tableLayoutPanelVirtual;
        private Label lblVirtualId;
        private Label lblVirtualMaglevVehicleIdentifier;
        private Label lblVirtualClear;
        private Panel pnlClearVirtual;
        private Label lblVirtualTraction;
        private Label lblTractionCapacityVirtual;
        // 故障与停车点
        private GroupBox gbFaultParking;
        private TableLayoutPanel tableLayoutPanelFault;
        private Label lblFaultStatus;
        private Label lblMCUFaultStatus;
        private Label lblParkingNum;
        private Label lblParkingPointsNum;
        private Label lblCurrentKI;
        private Label lblCurrentKIIdentifiers;
        private Label lblVirtualKI;
        private Label lblVirtualKIIdentifiers;
    }
}
