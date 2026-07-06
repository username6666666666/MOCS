using MOCS.Protocals;

namespace MOCS.DebugTool;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        // ========== 控件声明 ==========
        lblMsgType = new Label();
        cmbMsgType = new ComboBox();
        lblTarget = new Label();
        cmbTarget = new ComboBox();
        gbUserData = new GroupBox();
        txtUserData = new TextBox();
        btnSend = new Button();
        btnSendRandom = new Button();
        btnClear = new Button();
        gbLog = new GroupBox();
        rtbLog = new RichTextBox();
        tableLayoutPanel1 = new TableLayoutPanel();
        pnlTop = new Panel();
        pnlButtons = new Panel();
        gbUserData.SuspendLayout();
        gbLog.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        pnlTop.SuspendLayout();
        pnlButtons.SuspendLayout();
        SuspendLayout();

        //
        // tableLayoutPanel1 - 主布局容器
        //
        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(pnlTop, 0, 0);
        tableLayoutPanel1.Controls.Add(gbUserData, 0, 1);
        tableLayoutPanel1.Controls.Add(pnlButtons, 0, 2);
        tableLayoutPanel1.Controls.Add(gbLog, 0, 3);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(12, 12);
        tableLayoutPanel1.Margin = new Padding(0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.Padding = new Padding(0);
        tableLayoutPanel1.RowCount = 4;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 130F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(876, 636);
        tableLayoutPanel1.TabIndex = 0;

        //
        // pnlTop - 报文类型 + 目标端口
        //
        pnlTop.Controls.Add(lblMsgType);
        pnlTop.Controls.Add(cmbMsgType);
        pnlTop.Controls.Add(lblTarget);
        pnlTop.Controls.Add(cmbTarget);
        pnlTop.Dock = DockStyle.Fill;
        pnlTop.Location = new Point(0, 0);
        pnlTop.Margin = new Padding(0, 0, 0, 0);
        pnlTop.Name = "pnlTop";
        pnlTop.Size = new Size(876, 36);
        pnlTop.TabIndex = 0;

        // lblMsgType
        lblMsgType.AutoSize = true;
        lblMsgType.Location = new Point(0, 10);
        lblMsgType.Name = "lblMsgType";
        lblMsgType.Size = new Size(62, 17);
        lblMsgType.TabIndex = 0;
        lblMsgType.Text = "报文类型:";
        lblMsgType.TextAlign = ContentAlignment.MiddleRight;

        // cmbMsgType
        cmbMsgType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbMsgType.FormattingEnabled = true;
        cmbMsgType.Items.AddRange(new object[] {
            "【DCS→MCU】DCS状态报文           (MsgId=0x01, 148B)",
            "【DCS→MCU】改变牵引状态报文       (MsgId=0x02, 2B)",
            "【DCS→MCU】悬浮架登录报文         (MsgId=0x03, 14B)",
            "【DCS→MCU】悬浮架注销报文         (MsgId=0x04, 2B)",
            "【DCS→MCU】最大速度曲线报文       (MsgId=0x05, 16B)",
            "【DCS→MCU】线路数据报文           (MsgId=0x06, 变长)",
            "【DCS→MCU】线路数据删除报文       (MsgId=0x07, 8B)",
            "【DCS→MCU】停车点运行状态请求报文 (MsgId=0x08, 4B)",
            "【DCS→MCU】停车点运行步进报文     (MsgId=0x09, 4B)",
            "【MCU→DCS】牵引状态报文           (MsgId=0x81, 164B)",
            "【MCU→DCS】应答报文               (MsgId=0x82, 12B)",
            "【MOCS→LCU】EMS控制报文           (MsgId=0x21, 8B)",
            "【MOCS→OBC】OBC控制报文           (MsgId=0x71, 14B)",
            "【LCU→VCU】EMS状态报文A           (MsgId=0x81, 9B)",
            "【LCU→VCU】EMS状态报文B           (MsgId=0x82, 9B)",
            "【VSPS→VCU】VSPS状态报文          (MsgId=0xE1, 12B)",
            "【OBC→VCU】OBC状态报文            (MsgId=0xF1, 16B)"});
        cmbMsgType.Location = new Point(68, 7);
        cmbMsgType.Name = "cmbMsgType";
        cmbMsgType.Size = new Size(440, 25);
        cmbMsgType.TabIndex = 1;
        cmbMsgType.SelectedIndexChanged += cmbMsgType_SelectedIndexChanged;

        // lblTarget
        lblTarget.AutoSize = true;
        lblTarget.Location = new Point(520, 10);
        lblTarget.Name = "lblTarget";
        lblTarget.Size = new Size(62, 17);
        lblTarget.TabIndex = 2;
        lblTarget.Text = "目标端口:";
        lblTarget.TextAlign = ContentAlignment.MiddleRight;

        // cmbTarget
        cmbTarget.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbTarget.FormattingEnabled = true;
        cmbTarget.Items.AddRange(new object[] { "6002 (MCU/DCS)", "6001 (VCU)" });
        cmbTarget.Location = new Point(588, 7);
        cmbTarget.Name = "cmbTarget";
        cmbTarget.Size = new Size(120, 25);
        cmbTarget.TabIndex = 3;

        //
        // gbUserData - UserData 输入面板
        //
        gbUserData.Controls.Add(txtUserData);
        gbUserData.Dock = DockStyle.Fill;
        gbUserData.Location = new Point(0, 36);
        gbUserData.Margin = new Padding(0, 0, 0, 0);
        gbUserData.Name = "gbUserData";
        gbUserData.Size = new Size(876, 130);
        gbUserData.TabIndex = 1;
        gbUserData.TabStop = false;
        gbUserData.Text = "UserData (十六进制，空格分隔，如: 01 02 03 ...)";

        // txtUserData
        txtUserData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        txtUserData.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtUserData.Location = new Point(10, 24);
        txtUserData.Multiline = true;
        txtUserData.Name = "txtUserData";
        txtUserData.PlaceholderText = "在此输入十六进制 UserData...";
        txtUserData.ScrollBars = ScrollBars.Vertical;
        txtUserData.Size = new Size(856, 96);
        txtUserData.TabIndex = 0;

        //
        // pnlButtons - 按钮面板
        //
        pnlButtons.Controls.Add(btnSend);
        pnlButtons.Controls.Add(btnSendRandom);
        pnlButtons.Controls.Add(btnClear);
        pnlButtons.Dock = DockStyle.Fill;
        pnlButtons.Location = new Point(0, 166);
        pnlButtons.Margin = new Padding(0, 0, 0, 0);
        pnlButtons.Name = "pnlButtons";
        pnlButtons.Size = new Size(876, 40);
        pnlButtons.TabIndex = 2;

        // btnSend
        btnSend.Location = new Point(0, 4);
        btnSend.Name = "btnSend";
        btnSend.Size = new Size(110, 32);
        btnSend.TabIndex = 0;
        btnSend.Text = "发送报文";
        btnSend.UseVisualStyleBackColor = true;
        btnSend.Click += btnSend_Click;

        // btnSendRandom
        btnSendRandom.Location = new Point(120, 4);
        btnSendRandom.Name = "btnSendRandom";
        btnSendRandom.Size = new Size(130, 32);
        btnSendRandom.TabIndex = 1;
        btnSendRandom.Text = "发送随机报文";
        btnSendRandom.UseVisualStyleBackColor = true;
        btnSendRandom.Click += btnSendRandom_Click;

        // btnClear
        btnClear.Location = new Point(260, 4);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(110, 32);
        btnClear.TabIndex = 2;
        btnClear.Text = "清空日志";
        btnClear.UseVisualStyleBackColor = true;
        btnClear.Click += btnClear_Click;

        //
        // gbLog - 日志面板
        //
        gbLog.Controls.Add(rtbLog);
        gbLog.Dock = DockStyle.Fill;
        gbLog.Location = new Point(0, 206);
        gbLog.Margin = new Padding(0, 0, 0, 0);
        gbLog.Name = "gbLog";
        gbLog.Size = new Size(876, 430);
        gbLog.TabIndex = 3;
        gbLog.TabStop = false;
        gbLog.Text = "发送日志";

        // rtbLog
        rtbLog.BackColor = Color.FromArgb(30, 30, 30);
        rtbLog.Dock = DockStyle.Fill;
        rtbLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        rtbLog.ForeColor = Color.LightGreen;
        rtbLog.Location = new Point(10, 24);
        rtbLog.Name = "rtbLog";
        rtbLog.ReadOnly = true;
        rtbLog.ScrollBars = RichTextBoxScrollBars.Vertical;
        rtbLog.Size = new Size(856, 396);
        rtbLog.TabIndex = 0;
        rtbLog.Text = "";

        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(900, 660);
        Controls.Add(tableLayoutPanel1);
        MinimumSize = new Size(680, 480);
        Name = "MainForm";
        Padding = new Padding(12);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "MOCS 调试工具 - 报文模拟发送";
        FormClosing += MainForm_FormClosing;
        gbUserData.ResumeLayout(false);
        gbUserData.PerformLayout();
        gbLog.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        pnlTop.ResumeLayout(false);
        pnlTop.PerformLayout();
        pnlButtons.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    // ========== 控件字段声明（设计器可识别） ==========
    private TableLayoutPanel tableLayoutPanel1;
    private Panel pnlTop;
    private Label lblMsgType;
    private ComboBox cmbMsgType;
    private Label lblTarget;
    private ComboBox cmbTarget;
    private GroupBox gbUserData;
    private TextBox txtUserData;
    private Panel pnlButtons;
    private Button btnSend;
    private Button btnSendRandom;
    private Button btnClear;
    private GroupBox gbLog;
    private RichTextBox rtbLog;
}
