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
            this.tsMaster = new MaterialSkin.Controls.MaterialTabSelector();
            this.tcMaster = new MaterialSkin.Controls.MaterialTabControl();
            this.tabLogging = new System.Windows.Forms.TabPage();
            this.txtPwd = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtUserName = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.tabSetting = new System.Windows.Forms.TabPage();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.btnLogging = new MaterialSkin.Controls.MaterialFlatButton();
            this.btnRegistere = new MaterialSkin.Controls.MaterialFlatButton();
            this.tcMaster.SuspendLayout();
            this.tabLogging.SuspendLayout();
            this.tabSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMaster
            // 
            this.tsMaster.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tsMaster.BaseTabControl = this.tcMaster;
            this.tsMaster.Depth = 0;
            this.tsMaster.Location = new System.Drawing.Point(0, 64);
            this.tsMaster.MouseState = MaterialSkin.MouseState.HOVER;
            this.tsMaster.Name = "tsMaster";
            this.tsMaster.Size = new System.Drawing.Size(521, 31);
            this.tsMaster.TabIndex = 0;
            this.tsMaster.Text = "materialTabSelector1";
            // 
            // tcMaster
            // 
            this.tcMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMaster.Controls.Add(this.tabLogging);
            this.tcMaster.Controls.Add(this.tabSetting);
            this.tcMaster.Depth = 0;
            this.tcMaster.Location = new System.Drawing.Point(13, 102);
            this.tcMaster.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tcMaster.MouseState = MaterialSkin.MouseState.HOVER;
            this.tcMaster.Name = "tcMaster";
            this.tcMaster.SelectedIndex = 0;
            this.tcMaster.Size = new System.Drawing.Size(496, 171);
            this.tcMaster.TabIndex = 1;
            // 
            // tabLogging
            // 
            this.tabLogging.BackColor = System.Drawing.Color.White;
            this.tabLogging.Controls.Add(this.txtPwd);
            this.tabLogging.Controls.Add(this.txtUserName);
            this.tabLogging.Location = new System.Drawing.Point(4, 29);
            this.tabLogging.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabLogging.Name = "tabLogging";
            this.tabLogging.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabLogging.Size = new System.Drawing.Size(488, 138);
            this.tabLogging.TabIndex = 0;
            this.tabLogging.Text = "登录";
            // 
            // txtPwd
            // 
            this.txtPwd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPwd.Depth = 0;
            this.txtPwd.Hint = "Please Enter Your Password";
            this.txtPwd.Location = new System.Drawing.Point(2, 84);
            this.txtPwd.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '\0';
            this.txtPwd.SelectedText = "";
            this.txtPwd.SelectionLength = 0;
            this.txtPwd.SelectionStart = 0;
            this.txtPwd.Size = new System.Drawing.Size(485, 28);
            this.txtPwd.TabIndex = 1;
            this.txtPwd.UseSystemPasswordChar = true;
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Depth = 0;
            this.txtUserName.Hint = "Please Enter Your Account";
            this.txtUserName.Location = new System.Drawing.Point(1, 35);
            this.txtUserName.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.PasswordChar = '\0';
            this.txtUserName.SelectedText = "";
            this.txtUserName.SelectionLength = 0;
            this.txtUserName.SelectionStart = 0;
            this.txtUserName.Size = new System.Drawing.Size(485, 28);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.UseSystemPasswordChar = false;
            // 
            // tabSetting
            // 
            this.tabSetting.Controls.Add(this.materialRaisedButton1);
            this.tabSetting.Location = new System.Drawing.Point(4, 29);
            this.tabSetting.Name = "tabSetting";
            this.tabSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabSetting.Size = new System.Drawing.Size(488, 138);
            this.tabSetting.TabIndex = 1;
            this.tabSetting.Text = "系统设置";
            this.tabSetting.UseVisualStyleBackColor = true;
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Location = new System.Drawing.Point(6, 16);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(245, 41);
            this.materialRaisedButton1.TabIndex = 15;
            this.materialRaisedButton1.Text = "Change Color Scheme";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.MaterialRaisedButton1_Click);
            // 
            // materialDivider1
            // 
            this.materialDivider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(0, 291);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(521, 2);
            this.materialDivider1.TabIndex = 2;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // btnLogging
            // 
            this.btnLogging.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogging.AutoSize = true;
            this.btnLogging.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLogging.BackColor = System.Drawing.Color.White;
            this.btnLogging.Depth = 0;
            this.btnLogging.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogging.Location = new System.Drawing.Point(446, 306);
            this.btnLogging.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnLogging.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLogging.Name = "btnLogging";
            this.btnLogging.Primary = true;
            this.btnLogging.Size = new System.Drawing.Size(63, 36);
            this.btnLogging.TabIndex = 3;
            this.btnLogging.Text = "Login";
            this.btnLogging.UseVisualStyleBackColor = true;
            // 
            // btnRegistere
            // 
            this.btnRegistere.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistere.AutoSize = true;
            this.btnRegistere.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRegistere.Depth = 0;
            this.btnRegistere.Location = new System.Drawing.Point(324, 306);
            this.btnRegistere.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnRegistere.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnRegistere.Name = "btnRegistere";
            this.btnRegistere.Primary = false;
            this.btnRegistere.Size = new System.Drawing.Size(112, 36);
            this.btnRegistere.TabIndex = 14;
            this.btnRegistere.Text = "Registered";
            this.btnRegistere.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 356);
            this.Controls.Add(this.btnRegistere);
            this.Controls.Add(this.btnLogging);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.tcMaster);
            this.Controls.Add(this.tsMaster);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Login";
            this.Text = "Smat Tools ";
            this.tcMaster.ResumeLayout(false);
            this.tabLogging.ResumeLayout(false);
            this.tabSetting.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabSelector tsMaster;
        private MaterialSkin.Controls.MaterialTabControl tcMaster;
        private System.Windows.Forms.TabPage tabLogging;
        private System.Windows.Forms.TabPage tabSetting;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtPwd;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtUserName;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialFlatButton btnLogging;
        private MaterialSkin.Controls.MaterialFlatButton btnRegistere;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
    }
}