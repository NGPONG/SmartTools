using SmartTools.Properties;

namespace SmartTools.Views
{
    partial class Main
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
            this.tcMaster = new MaterialSkin.Controls.MaterialTabControl();
            this.lblMoney_Title_Default = new System.Windows.Forms.TabPage();
            this.materialProgressSpinner1 = new MaterialSkin.Controls.MaterialProgressSpinner();
            this.materialFlatButton1 = new MaterialSkin.Controls.MaterialFlatButton();
            this.btnOpenBrowser = new MaterialSkin.Controls.MaterialFlatButton();
            this.lblUrl = new MaterialSkin.Controls.MaterialLabel();
            this.txtUrl = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.lblIsCycle = new MaterialSkin.Controls.MaterialLabel();
            this.cblblIsCycle = new MaterialSkin.Controls.MaterialCheckBox();
            this.mlvData_Default = new MaterialSkin.Controls.MaterialListView();
            this.chActionIndex_Default = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBetType_Default = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDelay_Default = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMoney_Default = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlProxy_Default = new System.Windows.Forms.Panel();
            this.txtPort_Default = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtIP_Default = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.lblPort_Default = new MaterialSkin.Controls.MaterialLabel();
            this.lblIP_Deafult = new MaterialSkin.Controls.MaterialLabel();
            this.lblUserProxy_Default = new MaterialSkin.Controls.MaterialLabel();
            this.cbUserProxy_Default = new MaterialSkin.Controls.MaterialCheckBox();
            this.lblMoneyWarning_Default = new MaterialSkin.Controls.MaterialLabel();
            this.txtMoneyWarning_Default = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.cbMoneyWarning_Default = new MaterialSkin.Controls.MaterialCheckBox();
            this.lbldescription_Default = new System.Windows.Forms.Label();
            this.txtMoney_Default = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.lblMoney_Default = new MaterialSkin.Controls.MaterialLabel();
            this.btnHow_Default = new MaterialSkin.Controls.MaterialFlatButton();
            this.cbConfigLock_Default = new MaterialSkin.Controls.MaterialCheckBox();
            this.lblConfigName_Default = new MaterialSkin.Controls.MaterialLabel();
            this.txtConfigName_Default = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtAuthentication_Default = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.lblAuthentication_Default = new MaterialSkin.Controls.MaterialLabel();
            this.tsMaster = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.btnStart = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnAdd = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnAddConfig = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.tcMaster.SuspendLayout();
            this.lblMoney_Title_Default.SuspendLayout();
            this.pnlProxy_Default.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMaster
            // 
            this.tcMaster.Controls.Add(this.lblMoney_Title_Default);
            this.tcMaster.Depth = 0;
            this.tcMaster.Location = new System.Drawing.Point(12, 134);
            this.tcMaster.MouseState = MaterialSkin.MouseState.HOVER;
            this.tcMaster.Name = "tcMaster";
            this.tcMaster.SelectedIndex = 0;
            this.tcMaster.Size = new System.Drawing.Size(906, 559);
            this.tcMaster.TabIndex = 0;
            // 
            // lblMoney_Title_Default
            // 
            this.lblMoney_Title_Default.BackColor = System.Drawing.Color.White;
            this.lblMoney_Title_Default.Controls.Add(this.materialProgressSpinner1);
            this.lblMoney_Title_Default.Controls.Add(this.materialFlatButton1);
            this.lblMoney_Title_Default.Controls.Add(this.btnOpenBrowser);
            this.lblMoney_Title_Default.Controls.Add(this.lblUrl);
            this.lblMoney_Title_Default.Controls.Add(this.txtUrl);
            this.lblMoney_Title_Default.Controls.Add(this.lblIsCycle);
            this.lblMoney_Title_Default.Controls.Add(this.cblblIsCycle);
            this.lblMoney_Title_Default.Controls.Add(this.mlvData_Default);
            this.lblMoney_Title_Default.Controls.Add(this.pnlProxy_Default);
            this.lblMoney_Title_Default.Controls.Add(this.lblUserProxy_Default);
            this.lblMoney_Title_Default.Controls.Add(this.cbUserProxy_Default);
            this.lblMoney_Title_Default.Controls.Add(this.lblMoneyWarning_Default);
            this.lblMoney_Title_Default.Controls.Add(this.txtMoneyWarning_Default);
            this.lblMoney_Title_Default.Controls.Add(this.cbMoneyWarning_Default);
            this.lblMoney_Title_Default.Controls.Add(this.lbldescription_Default);
            this.lblMoney_Title_Default.Controls.Add(this.txtMoney_Default);
            this.lblMoney_Title_Default.Controls.Add(this.lblMoney_Default);
            this.lblMoney_Title_Default.Controls.Add(this.btnHow_Default);
            this.lblMoney_Title_Default.Controls.Add(this.cbConfigLock_Default);
            this.lblMoney_Title_Default.Controls.Add(this.lblConfigName_Default);
            this.lblMoney_Title_Default.Controls.Add(this.txtConfigName_Default);
            this.lblMoney_Title_Default.Controls.Add(this.txtAuthentication_Default);
            this.lblMoney_Title_Default.Controls.Add(this.lblAuthentication_Default);
            this.lblMoney_Title_Default.Location = new System.Drawing.Point(4, 25);
            this.lblMoney_Title_Default.Name = "lblMoney_Title_Default";
            this.lblMoney_Title_Default.Padding = new System.Windows.Forms.Padding(3);
            this.lblMoney_Title_Default.Size = new System.Drawing.Size(898, 530);
            this.lblMoney_Title_Default.TabIndex = 0;
            this.lblMoney_Title_Default.Text = "默认配置";
            // 
            // materialProgressSpinner1
            // 
            this.materialProgressSpinner1.CustomBackground = false;
            this.materialProgressSpinner1.Location = new System.Drawing.Point(890, 27);
            this.materialProgressSpinner1.Maximum = 100;
            this.materialProgressSpinner1.Name = "materialProgressSpinner1";
            this.materialProgressSpinner1.Size = new System.Drawing.Size(16, 16);
            this.materialProgressSpinner1.TabIndex = 22;
            this.materialProgressSpinner1.Value = 50;
            this.materialProgressSpinner1.Visible = false;
            // 
            // materialFlatButton1
            // 
            this.materialFlatButton1.AutoSize = true;
            this.materialFlatButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton1.CustomFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.materialFlatButton1.Depth = 0;
            this.materialFlatButton1.Icon = null;
            this.materialFlatButton1.Location = new System.Drawing.Point(867, 185);
            this.materialFlatButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButton1.Name = "materialFlatButton1";
            this.materialFlatButton1.Primary = false;
            this.materialFlatButton1.Size = new System.Drawing.Size(32, 36);
            this.materialFlatButton1.TabIndex = 6;
            this.materialFlatButton1.Text = "+";
            this.materialFlatButton1.UseVisualStyleBackColor = true;
            // 
            // btnOpenBrowser
            // 
            this.btnOpenBrowser.AutoSize = true;
            this.btnOpenBrowser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOpenBrowser.CustomFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnOpenBrowser.Depth = 0;
            this.btnOpenBrowser.Icon = null;
            this.btnOpenBrowser.Location = new System.Drawing.Point(829, 18);
            this.btnOpenBrowser.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnOpenBrowser.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnOpenBrowser.Name = "btnOpenBrowser";
            this.btnOpenBrowser.Primary = false;
            this.btnOpenBrowser.Size = new System.Drawing.Size(60, 36);
            this.btnOpenBrowser.TabIndex = 21;
            this.btnOpenBrowser.Text = "打开";
            this.btnOpenBrowser.UseVisualStyleBackColor = true;
            this.btnOpenBrowser.Click += new System.EventHandler(this.BtnOpenBrowser_Click);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Depth = 0;
            this.lblUrl.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblUrl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblUrl.Location = new System.Drawing.Point(451, 23);
            this.lblUrl.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(50, 24);
            this.lblUrl.TabIndex = 20;
            this.lblUrl.Text = "网址";
            // 
            // txtUrl
            // 
            this.txtUrl.Depth = 0;
            this.txtUrl.Enabled = false;
            this.txtUrl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUrl.Hint = "";
            this.txtUrl.Location = new System.Drawing.Point(508, 16);
            this.txtUrl.MaxLength = 32767;
            this.txtUrl.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.PasswordChar = '\0';
            this.txtUrl.SelectedText = "";
            this.txtUrl.SelectionLength = 0;
            this.txtUrl.SelectionStart = 0;
            this.txtUrl.Size = new System.Drawing.Size(314, 30);
            this.txtUrl.TabIndex = 19;
            this.txtUrl.TabStop = false;
            this.txtUrl.UseSystemPasswordChar = false;
            // 
            // lblIsCycle
            // 
            this.lblIsCycle.AutoSize = true;
            this.lblIsCycle.Depth = 0;
            this.lblIsCycle.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblIsCycle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblIsCycle.Location = new System.Drawing.Point(37, 181);
            this.lblIsCycle.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblIsCycle.Name = "lblIsCycle";
            this.lblIsCycle.Size = new System.Drawing.Size(90, 24);
            this.lblIsCycle.TabIndex = 18;
            this.lblIsCycle.Text = "动作循环";
            // 
            // cblblIsCycle
            // 
            this.cblblIsCycle.AutoSize = true;
            this.cblblIsCycle.Depth = 0;
            this.cblblIsCycle.Font = new System.Drawing.Font("Roboto", 10F);
            this.cblblIsCycle.Location = new System.Drawing.Point(5, 178);
            this.cblblIsCycle.Margin = new System.Windows.Forms.Padding(0);
            this.cblblIsCycle.MouseLocation = new System.Drawing.Point(-1, -1);
            this.cblblIsCycle.MouseState = MaterialSkin.MouseState.HOVER;
            this.cblblIsCycle.Name = "cblblIsCycle";
            this.cblblIsCycle.Ripple = true;
            this.cblblIsCycle.Size = new System.Drawing.Size(26, 30);
            this.cblblIsCycle.TabIndex = 17;
            this.cblblIsCycle.UseVisualStyleBackColor = true;
            // 
            // mlvData_Default
            // 
            this.mlvData_Default.BackColor = System.Drawing.Color.White;
            this.mlvData_Default.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mlvData_Default.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chActionIndex_Default,
            this.chBetType_Default,
            this.chDelay_Default,
            this.chMoney_Default});
            this.mlvData_Default.Depth = 0;
            this.mlvData_Default.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mlvData_Default.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.mlvData_Default.FullRowSelect = true;
            this.mlvData_Default.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.mlvData_Default.HoveredItem = null;
            this.mlvData_Default.Location = new System.Drawing.Point(3, 182);
            this.mlvData_Default.MouseLocation = new System.Drawing.Point(-1, -1);
            this.mlvData_Default.MouseState = MaterialSkin.MouseState.OUT;
            this.mlvData_Default.Name = "mlvData_Default";
            this.mlvData_Default.OwnerDraw = true;
            this.mlvData_Default.Size = new System.Drawing.Size(892, 345);
            this.mlvData_Default.TabIndex = 16;
            this.mlvData_Default.UseCompatibleStateImageBehavior = false;
            this.mlvData_Default.View = System.Windows.Forms.View.Details;
            // 
            // chActionIndex_Default
            // 
            this.chActionIndex_Default.Text = "动作次数";
            this.chActionIndex_Default.Width = 200;
            // 
            // chBetType_Default
            // 
            this.chBetType_Default.Text = "动作类型";
            this.chBetType_Default.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chBetType_Default.Width = 200;
            // 
            // chDelay_Default
            // 
            this.chDelay_Default.Text = "延迟时间";
            this.chDelay_Default.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chDelay_Default.Width = 200;
            // 
            // chMoney_Default
            // 
            this.chMoney_Default.Text = "金额";
            this.chMoney_Default.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chMoney_Default.Width = 151;
            // 
            // pnlProxy_Default
            // 
            this.pnlProxy_Default.Controls.Add(this.txtPort_Default);
            this.pnlProxy_Default.Controls.Add(this.txtIP_Default);
            this.pnlProxy_Default.Controls.Add(this.lblPort_Default);
            this.pnlProxy_Default.Controls.Add(this.lblIP_Deafult);
            this.pnlProxy_Default.Location = new System.Drawing.Point(280, 161);
            this.pnlProxy_Default.Name = "pnlProxy_Default";
            this.pnlProxy_Default.Size = new System.Drawing.Size(1, 66);
            this.pnlProxy_Default.TabIndex = 15;
            // 
            // txtPort_Default
            // 
            this.txtPort_Default.Depth = 0;
            this.txtPort_Default.Hint = "";
            this.txtPort_Default.Location = new System.Drawing.Point(292, 17);
            this.txtPort_Default.MaxLength = 32767;
            this.txtPort_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtPort_Default.Name = "txtPort_Default";
            this.txtPort_Default.PasswordChar = '\0';
            this.txtPort_Default.SelectedText = "";
            this.txtPort_Default.SelectionLength = 0;
            this.txtPort_Default.SelectionStart = 0;
            this.txtPort_Default.Size = new System.Drawing.Size(70, 30);
            this.txtPort_Default.TabIndex = 11;
            this.txtPort_Default.TabStop = false;
            this.txtPort_Default.UseSystemPasswordChar = false;
            // 
            // txtIP_Default
            // 
            this.txtIP_Default.Depth = 0;
            this.txtIP_Default.Hint = "";
            this.txtIP_Default.Location = new System.Drawing.Point(39, 17);
            this.txtIP_Default.MaxLength = 32767;
            this.txtIP_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtIP_Default.Name = "txtIP_Default";
            this.txtIP_Default.PasswordChar = '\0';
            this.txtIP_Default.SelectedText = "";
            this.txtIP_Default.SelectionLength = 0;
            this.txtIP_Default.SelectionStart = 0;
            this.txtIP_Default.Size = new System.Drawing.Size(175, 30);
            this.txtIP_Default.TabIndex = 9;
            this.txtIP_Default.TabStop = false;
            this.txtIP_Default.UseSystemPasswordChar = false;
            // 
            // lblPort_Default
            // 
            this.lblPort_Default.AutoSize = true;
            this.lblPort_Default.Depth = 0;
            this.lblPort_Default.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblPort_Default.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblPort_Default.Location = new System.Drawing.Point(243, 21);
            this.lblPort_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblPort_Default.Name = "lblPort_Default";
            this.lblPort_Default.Size = new System.Drawing.Size(45, 24);
            this.lblPort_Default.TabIndex = 10;
            this.lblPort_Default.Text = "Port";
            // 
            // lblIP_Deafult
            // 
            this.lblIP_Deafult.AutoSize = true;
            this.lblIP_Deafult.Depth = 0;
            this.lblIP_Deafult.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblIP_Deafult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblIP_Deafult.Location = new System.Drawing.Point(9, 21);
            this.lblIP_Deafult.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblIP_Deafult.Name = "lblIP_Deafult";
            this.lblIP_Deafult.Size = new System.Drawing.Size(27, 24);
            this.lblIP_Deafult.TabIndex = 8;
            this.lblIP_Deafult.Text = "IP";
            // 
            // lblUserProxy_Default
            // 
            this.lblUserProxy_Default.AutoSize = true;
            this.lblUserProxy_Default.Depth = 0;
            this.lblUserProxy_Default.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblUserProxy_Default.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblUserProxy_Default.Location = new System.Drawing.Point(182, 182);
            this.lblUserProxy_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblUserProxy_Default.Name = "lblUserProxy_Default";
            this.lblUserProxy_Default.Size = new System.Drawing.Size(90, 24);
            this.lblUserProxy_Default.TabIndex = 14;
            this.lblUserProxy_Default.Text = "启用代理";
            // 
            // cbUserProxy_Default
            // 
            this.cbUserProxy_Default.AutoSize = true;
            this.cbUserProxy_Default.Depth = 0;
            this.cbUserProxy_Default.Font = new System.Drawing.Font("Roboto", 10F);
            this.cbUserProxy_Default.Location = new System.Drawing.Point(150, 178);
            this.cbUserProxy_Default.Margin = new System.Windows.Forms.Padding(0);
            this.cbUserProxy_Default.MouseLocation = new System.Drawing.Point(-1, -1);
            this.cbUserProxy_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.cbUserProxy_Default.Name = "cbUserProxy_Default";
            this.cbUserProxy_Default.Ripple = true;
            this.cbUserProxy_Default.Size = new System.Drawing.Size(26, 30);
            this.cbUserProxy_Default.TabIndex = 13;
            this.cbUserProxy_Default.UseVisualStyleBackColor = true;
            this.cbUserProxy_Default.CheckedChanged += new System.EventHandler(this.CbUserProxy_Default_CheckedChanged);
            // 
            // lblMoneyWarning_Default
            // 
            this.lblMoneyWarning_Default.AutoSize = true;
            this.lblMoneyWarning_Default.Depth = 0;
            this.lblMoneyWarning_Default.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblMoneyWarning_Default.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMoneyWarning_Default.Location = new System.Drawing.Point(520, 133);
            this.lblMoneyWarning_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblMoneyWarning_Default.Name = "lblMoneyWarning_Default";
            this.lblMoneyWarning_Default.Size = new System.Drawing.Size(90, 24);
            this.lblMoneyWarning_Default.TabIndex = 11;
            this.lblMoneyWarning_Default.Text = "余额预警";
            // 
            // txtMoneyWarning_Default
            // 
            this.txtMoneyWarning_Default.Depth = 0;
            this.txtMoneyWarning_Default.Enabled = false;
            this.txtMoneyWarning_Default.Hint = "";
            this.txtMoneyWarning_Default.Location = new System.Drawing.Point(616, 129);
            this.txtMoneyWarning_Default.MaxLength = 32767;
            this.txtMoneyWarning_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtMoneyWarning_Default.Name = "txtMoneyWarning_Default";
            this.txtMoneyWarning_Default.PasswordChar = '\0';
            this.txtMoneyWarning_Default.SelectedText = "";
            this.txtMoneyWarning_Default.SelectionLength = 0;
            this.txtMoneyWarning_Default.SelectionStart = 0;
            this.txtMoneyWarning_Default.Size = new System.Drawing.Size(60, 30);
            this.txtMoneyWarning_Default.TabIndex = 10;
            this.txtMoneyWarning_Default.TabStop = false;
            this.txtMoneyWarning_Default.UseSystemPasswordChar = false;
            // 
            // cbMoneyWarning_Default
            // 
            this.cbMoneyWarning_Default.AutoSize = true;
            this.cbMoneyWarning_Default.Checked = true;
            this.cbMoneyWarning_Default.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMoneyWarning_Default.Depth = 0;
            this.cbMoneyWarning_Default.Font = new System.Drawing.Font("Roboto", 10F);
            this.cbMoneyWarning_Default.Location = new System.Drawing.Point(488, 129);
            this.cbMoneyWarning_Default.Margin = new System.Windows.Forms.Padding(0);
            this.cbMoneyWarning_Default.MouseLocation = new System.Drawing.Point(-1, -1);
            this.cbMoneyWarning_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.cbMoneyWarning_Default.Name = "cbMoneyWarning_Default";
            this.cbMoneyWarning_Default.Ripple = true;
            this.cbMoneyWarning_Default.Size = new System.Drawing.Size(26, 30);
            this.cbMoneyWarning_Default.TabIndex = 9;
            this.cbMoneyWarning_Default.UseVisualStyleBackColor = true;
            this.cbMoneyWarning_Default.CheckedChanged += new System.EventHandler(this.CbMoneyWarning_Default_CheckedChanged);
            // 
            // lbldescription_Default
            // 
            this.lbldescription_Default.AutoSize = true;
            this.lbldescription_Default.Font = new System.Drawing.Font("微软雅黑", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbldescription_Default.ForeColor = System.Drawing.Color.Blue;
            this.lbldescription_Default.Location = new System.Drawing.Point(245, 136);
            this.lbldescription_Default.Name = "lbldescription_Default";
            this.lbldescription_Default.Size = new System.Drawing.Size(205, 19);
            this.lbldescription_Default.TabIndex = 8;
            this.lbldescription_Default.Text = "输入完成授权身份后会自动同步";
            // 
            // txtMoney_Default
            // 
            this.txtMoney_Default.Depth = 0;
            this.txtMoney_Default.Enabled = false;
            this.txtMoney_Default.Hint = "";
            this.txtMoney_Default.Location = new System.Drawing.Point(102, 127);
            this.txtMoney_Default.MaxLength = 32767;
            this.txtMoney_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtMoney_Default.Name = "txtMoney_Default";
            this.txtMoney_Default.PasswordChar = '\0';
            this.txtMoney_Default.SelectedText = "";
            this.txtMoney_Default.SelectionLength = 0;
            this.txtMoney_Default.SelectionStart = 0;
            this.txtMoney_Default.Size = new System.Drawing.Size(137, 30);
            this.txtMoney_Default.TabIndex = 7;
            this.txtMoney_Default.TabStop = false;
            this.txtMoney_Default.UseSystemPasswordChar = false;
            // 
            // lblMoney_Default
            // 
            this.lblMoney_Default.AutoSize = true;
            this.lblMoney_Default.Depth = 0;
            this.lblMoney_Default.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblMoney_Default.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMoney_Default.Location = new System.Drawing.Point(6, 131);
            this.lblMoney_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblMoney_Default.Name = "lblMoney_Default";
            this.lblMoney_Default.Size = new System.Drawing.Size(90, 24);
            this.lblMoney_Default.TabIndex = 6;
            this.lblMoney_Default.Text = "账户余额";
            // 
            // btnHow_Default
            // 
            this.btnHow_Default.AutoSize = true;
            this.btnHow_Default.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHow_Default.CustomFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnHow_Default.Depth = 0;
            this.btnHow_Default.Icon = null;
            this.btnHow_Default.Location = new System.Drawing.Point(612, 67);
            this.btnHow_Default.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnHow_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnHow_Default.Name = "btnHow_Default";
            this.btnHow_Default.Primary = false;
            this.btnHow_Default.Size = new System.Drawing.Size(165, 36);
            this.btnHow_Default.TabIndex = 5;
            this.btnHow_Default.Text = "How can i get it";
            this.btnHow_Default.UseVisualStyleBackColor = true;
            // 
            // cbConfigLock_Default
            // 
            this.cbConfigLock_Default.AutoSize = true;
            this.cbConfigLock_Default.Checked = true;
            this.cbConfigLock_Default.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbConfigLock_Default.Depth = 0;
            this.cbConfigLock_Default.Font = new System.Drawing.Font("Roboto", 10F);
            this.cbConfigLock_Default.Location = new System.Drawing.Point(359, 18);
            this.cbConfigLock_Default.Margin = new System.Windows.Forms.Padding(0);
            this.cbConfigLock_Default.MouseLocation = new System.Drawing.Point(-1, -1);
            this.cbConfigLock_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.cbConfigLock_Default.Name = "cbConfigLock_Default";
            this.cbConfigLock_Default.Ripple = true;
            this.cbConfigLock_Default.Size = new System.Drawing.Size(26, 30);
            this.cbConfigLock_Default.TabIndex = 4;
            this.cbConfigLock_Default.UseVisualStyleBackColor = true;
            this.cbConfigLock_Default.CheckedChanged += new System.EventHandler(this.CbConfigLock_Default_CheckedChanged);
            // 
            // lblConfigName_Default
            // 
            this.lblConfigName_Default.AutoSize = true;
            this.lblConfigName_Default.Depth = 0;
            this.lblConfigName_Default.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblConfigName_Default.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblConfigName_Default.Location = new System.Drawing.Point(6, 23);
            this.lblConfigName_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblConfigName_Default.Name = "lblConfigName_Default";
            this.lblConfigName_Default.Size = new System.Drawing.Size(90, 24);
            this.lblConfigName_Default.TabIndex = 3;
            this.lblConfigName_Default.Text = "配置名称";
            // 
            // txtConfigName_Default
            // 
            this.txtConfigName_Default.Depth = 0;
            this.txtConfigName_Default.Enabled = false;
            this.txtConfigName_Default.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtConfigName_Default.Hint = "";
            this.txtConfigName_Default.Location = new System.Drawing.Point(102, 16);
            this.txtConfigName_Default.MaxLength = 32767;
            this.txtConfigName_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtConfigName_Default.Name = "txtConfigName_Default";
            this.txtConfigName_Default.PasswordChar = '\0';
            this.txtConfigName_Default.SelectedText = "";
            this.txtConfigName_Default.SelectionLength = 0;
            this.txtConfigName_Default.SelectionStart = 0;
            this.txtConfigName_Default.Size = new System.Drawing.Size(236, 30);
            this.txtConfigName_Default.TabIndex = 2;
            this.txtConfigName_Default.TabStop = false;
            this.txtConfigName_Default.UseSystemPasswordChar = false;
            this.txtConfigName_Default.TextChanged += new System.EventHandler(this.TxtConfigName_Default_TextChanged);
            // 
            // txtAuthentication_Default
            // 
            this.txtAuthentication_Default.Depth = 0;
            this.txtAuthentication_Default.Hint = "";
            this.txtAuthentication_Default.Location = new System.Drawing.Point(102, 68);
            this.txtAuthentication_Default.MaxLength = 32767;
            this.txtAuthentication_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtAuthentication_Default.Name = "txtAuthentication_Default";
            this.txtAuthentication_Default.PasswordChar = '\0';
            this.txtAuthentication_Default.SelectedText = "";
            this.txtAuthentication_Default.SelectionLength = 0;
            this.txtAuthentication_Default.SelectionStart = 0;
            this.txtAuthentication_Default.Size = new System.Drawing.Size(499, 30);
            this.txtAuthentication_Default.TabIndex = 1;
            this.txtAuthentication_Default.TabStop = false;
            this.txtAuthentication_Default.UseSystemPasswordChar = false;
            // 
            // lblAuthentication_Default
            // 
            this.lblAuthentication_Default.AutoSize = true;
            this.lblAuthentication_Default.Depth = 0;
            this.lblAuthentication_Default.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblAuthentication_Default.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAuthentication_Default.Location = new System.Drawing.Point(6, 73);
            this.lblAuthentication_Default.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblAuthentication_Default.Name = "lblAuthentication_Default";
            this.lblAuthentication_Default.Size = new System.Drawing.Size(90, 24);
            this.lblAuthentication_Default.TabIndex = 0;
            this.lblAuthentication_Default.Text = "授权身份";
            // 
            // tsMaster
            // 
            this.tsMaster.BaseTabControl = this.tcMaster;
            this.tsMaster.Depth = 0;
            this.tsMaster.Location = new System.Drawing.Point(-2, 64);
            this.tsMaster.MouseState = MaterialSkin.MouseState.HOVER;
            this.tsMaster.Name = "tsMaster";
            this.tsMaster.Size = new System.Drawing.Size(948, 64);
            this.tsMaster.TabIndex = 1;
            this.tsMaster.Text = "materialTabSelector1";
            // 
            // materialDivider1
            // 
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(-2, 720);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(1264, 1);
            this.materialDivider1.TabIndex = 3;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // btnStart
            // 
            this.btnStart.AutoSize = true;
            this.btnStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStart.Depth = 0;
            this.btnStart.Icon = null;
            this.btnStart.Location = new System.Drawing.Point(859, 731);
            this.btnStart.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnStart.Name = "btnStart";
            this.btnStart.Primary = true;
            this.btnStart.Size = new System.Drawing.Size(60, 36);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAdd.Depth = 0;
            this.btnAdd.Icon = null;
            this.btnAdd.Location = new System.Drawing.Point(225, 731);
            this.btnAdd.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Primary = true;
            this.btnAdd.Size = new System.Drawing.Size(98, 36);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "新增动作";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnAddConfig
            // 
            this.btnAddConfig.AutoSize = true;
            this.btnAddConfig.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddConfig.Depth = 0;
            this.btnAddConfig.Icon = null;
            this.btnAddConfig.Location = new System.Drawing.Point(680, 731);
            this.btnAddConfig.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAddConfig.Name = "btnAddConfig";
            this.btnAddConfig.Primary = true;
            this.btnAddConfig.Size = new System.Drawing.Size(173, 36);
            this.btnAddConfig.TabIndex = 6;
            this.btnAddConfig.Text = "添加一页新的配置";
            this.btnAddConfig.UseVisualStyleBackColor = true;
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.AutoSize = true;
            this.materialRaisedButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Icon = null;
            this.materialRaisedButton1.Location = new System.Drawing.Point(482, 731);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(192, 36);
            this.materialRaisedButton1.TabIndex = 7;
            this.materialRaisedButton1.Text = "删除当前激活的配置";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.MaterialRaisedButton1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(937, 779);
            this.Controls.Add(this.materialRaisedButton1);
            this.Controls.Add(this.btnAddConfig);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.tsMaster);
            this.Controls.Add(this.tcMaster);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Smart Tool";
            this.tcMaster.ResumeLayout(false);
            this.lblMoney_Title_Default.ResumeLayout(false);
            this.lblMoney_Title_Default.PerformLayout();
            this.pnlProxy_Default.ResumeLayout(false);
            this.pnlProxy_Default.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl tcMaster;
        private System.Windows.Forms.TabPage lblMoney_Title_Default;
        private MaterialSkin.Controls.MaterialTabSelector tsMaster;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialRaisedButton btnStart;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtAuthentication_Default;
        private MaterialSkin.Controls.MaterialLabel lblAuthentication_Default;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtConfigName_Default;
        private MaterialSkin.Controls.MaterialLabel lblConfigName_Default;
        private MaterialSkin.Controls.MaterialCheckBox cbConfigLock_Default;
        private MaterialSkin.Controls.MaterialFlatButton btnHow_Default;
        private System.Windows.Forms.Label lbldescription_Default;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtMoney_Default;
        private MaterialSkin.Controls.MaterialLabel lblMoney_Default;
        private MaterialSkin.Controls.MaterialCheckBox cbMoneyWarning_Default;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtMoneyWarning_Default;
        private MaterialSkin.Controls.MaterialLabel lblMoneyWarning_Default;
        private MaterialSkin.Controls.MaterialLabel lblUserProxy_Default;
        private MaterialSkin.Controls.MaterialCheckBox cbUserProxy_Default;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtPort_Default;
        private MaterialSkin.Controls.MaterialLabel lblPort_Default;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtIP_Default;
        private MaterialSkin.Controls.MaterialLabel lblIP_Deafult;
        private System.Windows.Forms.Panel pnlProxy_Default;
        private MaterialSkin.Controls.MaterialListView mlvData_Default;
        private System.Windows.Forms.ColumnHeader chActionIndex_Default;
        private System.Windows.Forms.ColumnHeader chBetType_Default;
        private System.Windows.Forms.ColumnHeader chDelay_Default;
        private System.Windows.Forms.ColumnHeader chMoney_Default;
        private MaterialSkin.Controls.MaterialRaisedButton btnAdd;
        private MaterialSkin.Controls.MaterialLabel lblIsCycle;
        private MaterialSkin.Controls.MaterialCheckBox cblblIsCycle;
        private MaterialSkin.Controls.MaterialLabel lblUrl;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtUrl;
        private MaterialSkin.Controls.MaterialFlatButton btnOpenBrowser;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton1;
        private MaterialSkin.Controls.MaterialRaisedButton btnAddConfig;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
        private MaterialSkin.Controls.MaterialProgressSpinner materialProgressSpinner1;
    }
}