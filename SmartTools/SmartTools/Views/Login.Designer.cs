namespace SmartTools.Views
{
    partial class Login
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
            this.tpLogging = new System.Windows.Forms.TabPage();
            this.btnLogin = new MaterialSkin.Controls.MaterialFlatButton();
            this.txtUserName = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtUserPwd = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.tpActivation = new System.Windows.Forms.TabPage();
            this.btnActivation = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txtActivation_UserName = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtActivation_Email = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.tpRegistered = new System.Windows.Forms.TabPage();
            this.btnRegistered = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txtRegistered_Email = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtRegistered_UserName = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtRegistered_UserPwd = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.tpSetting = new System.Windows.Forms.TabPage();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.tcMaster.SuspendLayout();
            this.tpLogging.SuspendLayout();
            this.tpActivation.SuspendLayout();
            this.tpRegistered.SuspendLayout();
            this.tpSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMaster
            // 
            this.tcMaster.Controls.Add(this.tpLogging);
            this.tcMaster.Controls.Add(this.tpActivation);
            this.tcMaster.Controls.Add(this.tpRegistered);
            this.tcMaster.Controls.Add(this.tpSetting);
            this.tcMaster.Depth = 0;
            this.tcMaster.Location = new System.Drawing.Point(12, 110);
            this.tcMaster.MouseState = MaterialSkin.MouseState.HOVER;
            this.tcMaster.Name = "tcMaster";
            this.tcMaster.SelectedIndex = 0;
            this.tcMaster.Size = new System.Drawing.Size(532, 229);
            this.tcMaster.TabIndex = 0;
            // 
            // tpLogging
            // 
            this.tpLogging.BackColor = System.Drawing.Color.White;
            this.tpLogging.Controls.Add(this.btnLogin);
            this.tpLogging.Controls.Add(this.txtUserName);
            this.tpLogging.Controls.Add(this.txtUserPwd);
            this.tpLogging.ForeColor = System.Drawing.Color.White;
            this.tpLogging.Location = new System.Drawing.Point(4, 25);
            this.tpLogging.Name = "tpLogging";
            this.tpLogging.Padding = new System.Windows.Forms.Padding(3);
            this.tpLogging.Size = new System.Drawing.Size(524, 200);
            this.tpLogging.TabIndex = 0;
            this.tpLogging.Text = "登陆";
            // 
            // btnLogin
            // 
            this.btnLogin.AutoSize = true;
            this.btnLogin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLogin.Depth = 0;
            this.btnLogin.Icon = null;
            this.btnLogin.Location = new System.Drawing.Point(441, 158);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLogin.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Primary = true;
            this.btnLogin.Size = new System.Drawing.Size(72, 36);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Depth = 0;
            this.txtUserName.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtUserName.Hint = "Please Enter Your Account";
            this.txtUserName.Location = new System.Drawing.Point(7, 25);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtUserName.MaxLength = 32767;
            this.txtUserName.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.PasswordChar = '\0';
            this.txtUserName.SelectedText = "";
            this.txtUserName.SelectionLength = 0;
            this.txtUserName.SelectionStart = 0;
            this.txtUserName.Size = new System.Drawing.Size(511, 28);
            this.txtUserName.TabIndex = 4;
            this.txtUserName.TabStop = false;
            this.txtUserName.UseSystemPasswordChar = false;
            // 
            // txtUserPwd
            // 
            this.txtUserPwd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserPwd.Depth = 0;
            this.txtUserPwd.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtUserPwd.Hint = "Please Enter Your Password";
            this.txtUserPwd.Location = new System.Drawing.Point(7, 72);
            this.txtUserPwd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtUserPwd.MaxLength = 32767;
            this.txtUserPwd.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtUserPwd.Name = "txtUserPwd";
            this.txtUserPwd.PasswordChar = '\0';
            this.txtUserPwd.SelectedText = "";
            this.txtUserPwd.SelectionLength = 0;
            this.txtUserPwd.SelectionStart = 0;
            this.txtUserPwd.Size = new System.Drawing.Size(511, 28);
            this.txtUserPwd.TabIndex = 5;
            this.txtUserPwd.TabStop = false;
            this.txtUserPwd.UseSystemPasswordChar = true;
            // 
            // tpActivation
            // 
            this.tpActivation.BackColor = System.Drawing.Color.White;
            this.tpActivation.Controls.Add(this.btnActivation);
            this.tpActivation.Controls.Add(this.txtActivation_UserName);
            this.tpActivation.Controls.Add(this.txtActivation_Email);
            this.tpActivation.Location = new System.Drawing.Point(4, 25);
            this.tpActivation.Name = "tpActivation";
            this.tpActivation.Size = new System.Drawing.Size(524, 200);
            this.tpActivation.TabIndex = 2;
            this.tpActivation.Text = "账户激活";
            // 
            // btnActivation
            // 
            this.btnActivation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActivation.AutoSize = true;
            this.btnActivation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnActivation.Depth = 0;
            this.btnActivation.Icon = null;
            this.btnActivation.Location = new System.Drawing.Point(392, 158);
            this.btnActivation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnActivation.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnActivation.Name = "btnActivation";
            this.btnActivation.Primary = true;
            this.btnActivation.Size = new System.Drawing.Size(122, 36);
            this.btnActivation.TabIndex = 8;
            this.btnActivation.Text = "Activation";
            this.btnActivation.UseVisualStyleBackColor = true;
            // 
            // txtActivation_UserName
            // 
            this.txtActivation_UserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtActivation_UserName.Depth = 0;
            this.txtActivation_UserName.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtActivation_UserName.Hint = "Please Enter Your Account";
            this.txtActivation_UserName.Location = new System.Drawing.Point(7, 25);
            this.txtActivation_UserName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtActivation_UserName.MaxLength = 32767;
            this.txtActivation_UserName.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtActivation_UserName.Name = "txtActivation_UserName";
            this.txtActivation_UserName.PasswordChar = '\0';
            this.txtActivation_UserName.SelectedText = "";
            this.txtActivation_UserName.SelectionLength = 0;
            this.txtActivation_UserName.SelectionStart = 0;
            this.txtActivation_UserName.Size = new System.Drawing.Size(511, 28);
            this.txtActivation_UserName.TabIndex = 6;
            this.txtActivation_UserName.TabStop = false;
            this.txtActivation_UserName.UseSystemPasswordChar = false;
            // 
            // txtActivation_Email
            // 
            this.txtActivation_Email.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtActivation_Email.Depth = 0;
            this.txtActivation_Email.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtActivation_Email.Hint = "Please Enter Your Email";
            this.txtActivation_Email.Location = new System.Drawing.Point(7, 72);
            this.txtActivation_Email.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtActivation_Email.MaxLength = 32767;
            this.txtActivation_Email.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtActivation_Email.Name = "txtActivation_Email";
            this.txtActivation_Email.PasswordChar = '\0';
            this.txtActivation_Email.SelectedText = "";
            this.txtActivation_Email.SelectionLength = 0;
            this.txtActivation_Email.SelectionStart = 0;
            this.txtActivation_Email.Size = new System.Drawing.Size(511, 28);
            this.txtActivation_Email.TabIndex = 7;
            this.txtActivation_Email.TabStop = false;
            this.txtActivation_Email.UseSystemPasswordChar = true;
            // 
            // tpRegistered
            // 
            this.tpRegistered.BackColor = System.Drawing.Color.White;
            this.tpRegistered.Controls.Add(this.btnRegistered);
            this.tpRegistered.Controls.Add(this.txtRegistered_Email);
            this.tpRegistered.Controls.Add(this.txtRegistered_UserName);
            this.tpRegistered.Controls.Add(this.txtRegistered_UserPwd);
            this.tpRegistered.Location = new System.Drawing.Point(4, 25);
            this.tpRegistered.Name = "tpRegistered";
            this.tpRegistered.Size = new System.Drawing.Size(524, 200);
            this.tpRegistered.TabIndex = 3;
            this.tpRegistered.Text = "用户注册";
            // 
            // btnRegistered
            // 
            this.btnRegistered.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistered.AutoSize = true;
            this.btnRegistered.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRegistered.Depth = 0;
            this.btnRegistered.Icon = null;
            this.btnRegistered.Location = new System.Drawing.Point(392, 158);
            this.btnRegistered.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRegistered.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnRegistered.Name = "btnRegistered";
            this.btnRegistered.Primary = true;
            this.btnRegistered.Size = new System.Drawing.Size(121, 36);
            this.btnRegistered.TabIndex = 9;
            this.btnRegistered.Text = "Registered";
            this.btnRegistered.UseVisualStyleBackColor = true;
            this.btnRegistered.Click += new System.EventHandler(this.BtnRegistered_Click);
            // 
            // txtRegistered_Email
            // 
            this.txtRegistered_Email.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegistered_Email.Depth = 0;
            this.txtRegistered_Email.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtRegistered_Email.Hint = "Please Enter Your Email";
            this.txtRegistered_Email.Location = new System.Drawing.Point(7, 119);
            this.txtRegistered_Email.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtRegistered_Email.MaxLength = 32767;
            this.txtRegistered_Email.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtRegistered_Email.Name = "txtRegistered_Email";
            this.txtRegistered_Email.PasswordChar = '\0';
            this.txtRegistered_Email.SelectedText = "";
            this.txtRegistered_Email.SelectionLength = 0;
            this.txtRegistered_Email.SelectionStart = 0;
            this.txtRegistered_Email.Size = new System.Drawing.Size(511, 28);
            this.txtRegistered_Email.TabIndex = 8;
            this.txtRegistered_Email.TabStop = false;
            this.txtRegistered_Email.UseSystemPasswordChar = false;
            // 
            // txtRegistered_UserName
            // 
            this.txtRegistered_UserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegistered_UserName.Depth = 0;
            this.txtRegistered_UserName.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtRegistered_UserName.Hint = "Please Enter Your Account";
            this.txtRegistered_UserName.Location = new System.Drawing.Point(4, 25);
            this.txtRegistered_UserName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtRegistered_UserName.MaxLength = 32767;
            this.txtRegistered_UserName.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtRegistered_UserName.Name = "txtRegistered_UserName";
            this.txtRegistered_UserName.PasswordChar = '\0';
            this.txtRegistered_UserName.SelectedText = "";
            this.txtRegistered_UserName.SelectionLength = 0;
            this.txtRegistered_UserName.SelectionStart = 0;
            this.txtRegistered_UserName.Size = new System.Drawing.Size(511, 28);
            this.txtRegistered_UserName.TabIndex = 6;
            this.txtRegistered_UserName.TabStop = false;
            this.txtRegistered_UserName.UseSystemPasswordChar = false;
            // 
            // txtRegistered_UserPwd
            // 
            this.txtRegistered_UserPwd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegistered_UserPwd.Depth = 0;
            this.txtRegistered_UserPwd.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtRegistered_UserPwd.Hint = "Please Enter Your Password";
            this.txtRegistered_UserPwd.Location = new System.Drawing.Point(4, 72);
            this.txtRegistered_UserPwd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtRegistered_UserPwd.MaxLength = 32767;
            this.txtRegistered_UserPwd.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtRegistered_UserPwd.Name = "txtRegistered_UserPwd";
            this.txtRegistered_UserPwd.PasswordChar = '\0';
            this.txtRegistered_UserPwd.SelectedText = "";
            this.txtRegistered_UserPwd.SelectionLength = 0;
            this.txtRegistered_UserPwd.SelectionStart = 0;
            this.txtRegistered_UserPwd.Size = new System.Drawing.Size(511, 28);
            this.txtRegistered_UserPwd.TabIndex = 7;
            this.txtRegistered_UserPwd.TabStop = false;
            this.txtRegistered_UserPwd.UseSystemPasswordChar = true;
            // 
            // tpSetting
            // 
            this.tpSetting.BackColor = System.Drawing.Color.White;
            this.tpSetting.Controls.Add(this.materialRaisedButton1);
            this.tpSetting.Location = new System.Drawing.Point(4, 25);
            this.tpSetting.Name = "tpSetting";
            this.tpSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpSetting.Size = new System.Drawing.Size(524, 200);
            this.tpSetting.TabIndex = 1;
            this.tpSetting.Text = "系统设置";
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.materialRaisedButton1.AutoSize = true;
            this.materialRaisedButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Icon = null;
            this.materialRaisedButton1.Location = new System.Drawing.Point(10, 17);
            this.materialRaisedButton1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(222, 36);
            this.materialRaisedButton1.TabIndex = 22;
            this.materialRaisedButton1.Text = "Change color scheme";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.MaterialRaisedButton1_Click_1);
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = this.tcMaster;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Location = new System.Drawing.Point(-2, 64);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(560, 40);
            this.materialTabSelector1.TabIndex = 1;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(556, 324);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.tcMaster);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.Text = "Smart Tool";
            this.tcMaster.ResumeLayout(false);
            this.tpLogging.ResumeLayout(false);
            this.tpLogging.PerformLayout();
            this.tpActivation.ResumeLayout(false);
            this.tpActivation.PerformLayout();
            this.tpRegistered.ResumeLayout(false);
            this.tpRegistered.PerformLayout();
            this.tpSetting.ResumeLayout(false);
            this.tpSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabControl tcMaster;
        private System.Windows.Forms.TabPage tpLogging;
        private System.Windows.Forms.TabPage tpSetting;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtUserName;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtUserPwd;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
        private System.Windows.Forms.TabPage tpActivation;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtActivation_UserName;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtActivation_Email;
        private MaterialSkin.Controls.MaterialFlatButton btnLogin;
        private MaterialSkin.Controls.MaterialRaisedButton btnActivation;
        private System.Windows.Forms.TabPage tpRegistered;
        private MaterialSkin.Controls.MaterialRaisedButton btnRegistered;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtRegistered_Email;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtRegistered_UserName;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtRegistered_UserPwd;
    }
}