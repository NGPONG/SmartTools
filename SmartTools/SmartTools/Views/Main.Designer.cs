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
            this.tpDefault = new System.Windows.Forms.TabPage();
            this.tsMaster = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.tcMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMaster
            // 
            this.tcMaster.Controls.Add(this.tpDefault);
            this.tcMaster.Depth = 0;
            this.tcMaster.Location = new System.Drawing.Point(12, 134);
            this.tcMaster.MouseState = MaterialSkin.MouseState.HOVER;
            this.tcMaster.Name = "tcMaster";
            this.tcMaster.SelectedIndex = 0;
            this.tcMaster.Size = new System.Drawing.Size(1236, 559);
            this.tcMaster.TabIndex = 0;
            // 
            // tpDefault
            // 
            this.tpDefault.Location = new System.Drawing.Point(4, 25);
            this.tpDefault.Name = "tpDefault";
            this.tpDefault.Padding = new System.Windows.Forms.Padding(3);
            this.tpDefault.Size = new System.Drawing.Size(1228, 530);
            this.tpDefault.TabIndex = 0;
            this.tpDefault.Text = "默认配置";
            this.tpDefault.UseVisualStyleBackColor = true;
            // 
            // tsMaster
            // 
            this.tsMaster.BaseTabControl = this.tcMaster;
            this.tsMaster.Depth = 0;
            this.tsMaster.Location = new System.Drawing.Point(-2, 64);
            this.tsMaster.MouseState = MaterialSkin.MouseState.HOVER;
            this.tsMaster.Name = "tsMaster";
            this.tsMaster.Size = new System.Drawing.Size(1264, 64);
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
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.AutoSize = true;
            this.materialRaisedButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Icon = null;
            this.materialRaisedButton1.Location = new System.Drawing.Point(1150, 735);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(98, 36);
            this.materialRaisedButton1.TabIndex = 4;
            this.materialRaisedButton1.Text = "开始启动";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1259, 779);
            this.Controls.Add(this.materialRaisedButton1);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.tsMaster);
            this.Controls.Add(this.tcMaster);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Smart Tool";
            this.tcMaster.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private MaterialSkin.Controls.MaterialTabControl tcMaster;
        private System.Windows.Forms.TabPage tpDefault;
        private MaterialSkin.Controls.MaterialTabSelector tsMaster;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
    }
}