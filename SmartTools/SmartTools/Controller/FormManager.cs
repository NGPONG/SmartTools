using SmartTools.Model;
using SmartTools.Utils.Extensions;
using SmartTools.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTools.Controller
{
    public class FormManager
    {
        #region member
        private static FormManager manager = null;
        private static object locker = new object();
        #endregion

        private FormManager() { }
        public static FormManager Instance()
        {
            if (manager == null)
            {
                lock (locker)
                {
                    if (manager == null)
                    {
                        manager = new FormManager();
                    }
                }
            }

            return manager;
        }

        public Main GetDefaultMainForm()
        {
            Main mainForm = new Main();
            mainForm.FormClosed += FormController_FormClosed;
            return mainForm;
        }

        public void InitializeComponent(Main mainForm)
        {
            var tcMaster = new MaterialSkin.Controls.MaterialTabControl();
            var tsMaster = new MaterialSkin.Controls.MaterialTabSelector();
            var materialDivider = new MaterialSkin.Controls.MaterialDivider();
            var btnStart = new MaterialSkin.Controls.MaterialRaisedButton();
            var btnAdd = new MaterialSkin.Controls.MaterialRaisedButton();
            var mlvData = new MaterialSkin.Controls.MaterialListView();

            // 
            // START
            //
            tcMaster.SuspendLayout();
            mainForm.SuspendLayout();

            //
            // Form
            //
            mainForm.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            mainForm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            mainForm.BackColor = System.Drawing.Color.White;
            mainForm.ClientSize = new System.Drawing.Size(937, 779);
            mainForm.MaximizeBox = false;
            mainForm.Name = "Main";
            mainForm.Text = "Smart Tool";

            // Tab controler
            tcMaster.Depth = 0;
            tcMaster.Location = new System.Drawing.Point(12, 134);
            tcMaster.MouseState = MaterialSkin.MouseState.HOVER;
            tcMaster.Name = "tcMaster";
            tcMaster.SelectedIndex = 0;
            tcMaster.Size = new System.Drawing.Size(906, 559);
            tcMaster.TabIndex = 0;
            tsMaster.BaseTabControl = tcMaster;
            tsMaster.Depth = 0;
            tsMaster.Location = new System.Drawing.Point(-2, 64);
            tsMaster.MouseState = MaterialSkin.MouseState.HOVER;
            tsMaster.Name = "tsMaster";
            tsMaster.Size = new System.Drawing.Size(948, 64);
            tsMaster.TabIndex = 1;

            //
            // Configs
            //
            var configs = ConfigurationManager.Instance().Load().GetUserConfigs();
            if (configs == null)
            {
                CreateConfigControls("Default",
                                     "默认配置",
                                     tsMaster,
                                     tcMaster,
                                     mlvData);
            }
            else
            {
                var index = 1;
                foreach (var config in configs)
                {
                    CreateConfigControls($"Config{index.ToString()}",
                                         config.ConfigurationName,
                                         tsMaster,
                                         tcMaster,
                                         mlvData,
                                         config.Authentication,
                                         config.Url,
                                         config.StopMoney,
                                         config.IsCycle,
                                         config.Proxy,
                                         config.Action);
                }
            }

            // 
            // btnStart
            // 
            btnStart.AutoSize = true;
            btnStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            btnStart.Depth = 0;
            btnStart.Icon = null;
            btnStart.Location = new System.Drawing.Point(859, 731);
            btnStart.MouseState = MaterialSkin.MouseState.HOVER;
            btnStart.Name = "btnStart";
            btnStart.Primary = true;
            btnStart.Size = new System.Drawing.Size(98, 36);
            btnStart.TabIndex = 4;
            btnStart.Text = "开始";
            btnStart.UseVisualStyleBackColor = true;

            // 
            // btnAdd
            // 
            btnAdd.AutoSize = true;
            btnAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            btnAdd.Depth = 0;
            btnAdd.Icon = null;
            btnAdd.Location = new System.Drawing.Point(734, 731);
            btnAdd.MouseState = MaterialSkin.MouseState.HOVER;
            btnAdd.Name = "btnAdd";
            btnAdd.Primary = true;
            btnAdd.Size = new System.Drawing.Size(98, 36);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "新增动作";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += delegate(object sender, EventArgs e)
            {
                int lastItemIndex;
                if (mlvData.Items.Count == 0)
                {
                    lastItemIndex = 1;
                }
                else
                {
                    var lastListViewItem = mlvData.Items[mlvData.Items.Count - 1];
                    lastItemIndex = Convert.ToInt32(lastListViewItem.SubItems[0].Text);
                }

                var actionNew = CustomAction.GetDefaultCustomAction();
                actionNew.ActionIndex = mlvData.Items.Count == 0 ? lastItemIndex : ++lastItemIndex;

                var listViewItem = new ListViewItem(actionNew.ConvertToArrary());
                mlvData.Items.Add(listViewItem);

                mlvData.AddEmbeddedButton(delegate (object o, EventArgs args)
                {
                    mlvData.RemoveActiveItem(o as MaterialSkin.Controls.MaterialFlatButton);
                });
            };

            // 
            // materialDivider
            // 
            materialDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            materialDivider.Depth = 0;
            materialDivider.Location = new System.Drawing.Point(-2, 720);
            materialDivider.MouseState = MaterialSkin.MouseState.HOVER;
            materialDivider.Name = "materialDivider";
            materialDivider.Size = new System.Drawing.Size(1264, 1);
            materialDivider.TabIndex = 3;
            materialDivider.Text = "materialDivider";

            //
            // Add controls.
            // 
            mainForm.Controls.Add(btnAdd);
            mainForm.Controls.Add(btnStart);
            mainForm.Controls.Add(materialDivider);
            mainForm.Controls.Add(tsMaster);
            mainForm.Controls.Add(tcMaster);

            //
            // END
            //
            tcMaster.ResumeLayout(false);
            mainForm.ResumeLayout(false);
            mainForm.PerformLayout();
        }

        public void CreateConfigControls(string controlName, 
                                         string ConfigurationName, 
                                         MaterialSkin.Controls.MaterialTabSelector tsMaster,
                                         MaterialSkin.Controls.MaterialTabControl tcMaster,
                                         MaterialSkin.Controls.MaterialListView mlvData,
                                         string Authentication = "",
                                         string Url = "",
                                         double StopMoney = 0, 
                                         bool IsCycle = false, 
                                         Proxy proxy = null, 
                                         List<CustomAction> source = null)
        {
            var lblMoney_Title = new System.Windows.Forms.TabPage();
            var chActionIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            var chBetType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            var chDelay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            var chMoney = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            var lblIsCycle = new MaterialSkin.Controls.MaterialLabel();
            var cbIsCycle = new MaterialSkin.Controls.MaterialCheckBox();
            var pnlProxy = new System.Windows.Forms.Panel();
            var txtPort = new MaterialSkin.Controls.MaterialSingleLineTextField();
            var txtIP = new MaterialSkin.Controls.MaterialSingleLineTextField();
            var lblPort = new MaterialSkin.Controls.MaterialLabel();
            var lblIP = new MaterialSkin.Controls.MaterialLabel();
            var lblUserProxy = new MaterialSkin.Controls.MaterialLabel();
            var cbUserProxy = new MaterialSkin.Controls.MaterialCheckBox();
            var lblMoneyWarning = new MaterialSkin.Controls.MaterialLabel();
            var txtMoneyWarning = new MaterialSkin.Controls.MaterialSingleLineTextField();
            var cbMoneyWarning = new MaterialSkin.Controls.MaterialCheckBox();
            var lbldescription = new System.Windows.Forms.Label();
            var txtMoney = new MaterialSkin.Controls.MaterialSingleLineTextField();
            var lblMoney = new MaterialSkin.Controls.MaterialLabel();
            var btnHow = new MaterialSkin.Controls.MaterialFlatButton();
            var cbConfigLock = new MaterialSkin.Controls.MaterialCheckBox();
            var lblConfigName = new MaterialSkin.Controls.MaterialLabel();
            var txtConfigName = new MaterialSkin.Controls.MaterialSingleLineTextField();
            var txtAuthentication = new MaterialSkin.Controls.MaterialSingleLineTextField();
            var lblAuthentication = new MaterialSkin.Controls.MaterialLabel();
            var txtUrl = new MaterialSkin.Controls.MaterialSingleLineTextField();
            var lblUrl = new MaterialSkin.Controls.MaterialLabel();
            var btnOpenBrowser = new MaterialSkin.Controls.MaterialFlatButton();


            //
            // START
            // 
            lblMoney_Title.SuspendLayout();
            pnlProxy.SuspendLayout();
            tcMaster.Controls.Add(lblMoney_Title);

            // 
            // lblMoney_Title
            // 
            lblMoney_Title.BackColor = System.Drawing.Color.White;
            lblMoney_Title.Controls.Add(mlvData);
            lblMoney_Title.Controls.Add(pnlProxy);
            lblMoney_Title.Controls.Add(lblUserProxy);
            lblMoney_Title.Controls.Add(lblIsCycle);
            lblMoney_Title.Controls.Add(cbIsCycle);
            lblMoney_Title.Controls.Add(cbUserProxy);
            lblMoney_Title.Controls.Add(lblMoneyWarning);
            lblMoney_Title.Controls.Add(txtMoneyWarning);
            lblMoney_Title.Controls.Add(cbMoneyWarning);
            lblMoney_Title.Controls.Add(lbldescription);
            lblMoney_Title.Controls.Add(txtMoney);
            lblMoney_Title.Controls.Add(lblMoney);
            lblMoney_Title.Controls.Add(btnHow);
            lblMoney_Title.Controls.Add(cbConfigLock);
            lblMoney_Title.Controls.Add(lblConfigName);
            lblMoney_Title.Controls.Add(txtConfigName);
            lblMoney_Title.Controls.Add(txtAuthentication);
            lblMoney_Title.Controls.Add(lblAuthentication);
            lblMoney_Title.Controls.Add(txtUrl);
            lblMoney_Title.Controls.Add(lblUrl);
            lblMoney_Title.Controls.Add(btnOpenBrowser);
            lblMoney_Title.Location = new System.Drawing.Point(4, 25);
            lblMoney_Title.Name = $"lblMoney_Title_{controlName}";
            lblMoney_Title.Padding = new System.Windows.Forms.Padding(3);
            lblMoney_Title.Size = new System.Drawing.Size(898, 530);
            lblMoney_Title.TabIndex = 0;
            lblMoney_Title.Text = $"{ConfigurationName}";

            // 
            // lblIsCycle
            // 
            lblIsCycle.AutoSize = true;
            lblIsCycle.Depth = 0;
            lblIsCycle.Font = new System.Drawing.Font("Roboto", 11F);
            lblIsCycle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lblIsCycle.Location = new System.Drawing.Point(37, 181);
            lblIsCycle.MouseState = MaterialSkin.MouseState.HOVER;
            lblIsCycle.Name = $"lblIsCycle{controlName}";
            lblIsCycle.Size = new System.Drawing.Size(90, 24);
            lblIsCycle.TabIndex = 18;
            lblIsCycle.Text = "动作循环";

            // 
            // cblblIsCycle
            // 
            cbIsCycle.AutoSize = true;
            cbIsCycle.Depth = 0;
            cbIsCycle.Font = new System.Drawing.Font("Roboto", 10F);
            cbIsCycle.Location = new System.Drawing.Point(5, 178);
            cbIsCycle.Margin = new System.Windows.Forms.Padding(0);
            cbIsCycle.MouseLocation = new System.Drawing.Point(-1, -1);
            cbIsCycle.MouseState = MaterialSkin.MouseState.HOVER;
            cbIsCycle.Name = $"cblblIsCycle_{controlName}";
            cbIsCycle.Ripple = true;
            cbIsCycle.Size = new System.Drawing.Size(26, 30);
            cbIsCycle.TabIndex = 17;
            cbIsCycle.Checked = IsCycle;
            cbIsCycle.UseVisualStyleBackColor = true;

            // 
            // Column Header
            // 
            chActionIndex.Text = "动作次数";
            chActionIndex.Width = 200;
            chBetType.Text = "动作类型";
            chBetType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            chBetType.Width = 200;
            chDelay.Text = "延迟时间";
            chDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            chDelay.Width = 200;
            chMoney.Text = "金额";
            chMoney.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            chMoney.Width = 150;

            // 
            // mlvData
            // 
            mlvData.BackColor = System.Drawing.Color.White;
            mlvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            mlvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            chActionIndex,
            chBetType,
            chDelay,
            chMoney});
            mlvData.Depth = 0;
            mlvData.Dock = System.Windows.Forms.DockStyle.Bottom;
            mlvData.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            mlvData.FullRowSelect = true;
            mlvData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            mlvData.HoveredItem = null;
            mlvData.Location = new System.Drawing.Point(3, 239);
            mlvData.MouseLocation = new System.Drawing.Point(-1, -1);
            mlvData.MouseState = MaterialSkin.MouseState.OUT;
            mlvData.Name = $"mlvData_{controlName}";
            mlvData.OwnerDraw = true;
            mlvData.Size = new System.Drawing.Size(892, 345);
            mlvData.TabIndex = 16;
            mlvData.UseCompatibleStateImageBehavior = false;
            mlvData.View = System.Windows.Forms.View.Details;

            // 
            // txtPort
            // 
            txtPort.Depth = 0;
            txtPort.Hint = "";
            txtPort.Location = new System.Drawing.Point(292, 17);
            txtPort.MaxLength = 32767;
            txtPort.MouseState = MaterialSkin.MouseState.HOVER;
            txtPort.Name = $"txtPort_{controlName}";
            txtPort.PasswordChar = '\0';
            txtPort.SelectedText = "";
            txtPort.SelectionLength = 0;
            txtPort.SelectionStart = 0;
            txtPort.Size = new System.Drawing.Size(70, 28);
            txtPort.TabIndex = 11;
            txtPort.TabStop = false;
            txtPort.Text = proxy == null ? string.Empty : proxy.Port.ToString();
            txtPort.UseSystemPasswordChar = false;

            // 
            // txtIP
            // 
            txtIP.Depth = 0;
            txtIP.Hint = "";
            txtIP.Location = new System.Drawing.Point(39, 17);
            txtIP.MaxLength = 32767;
            txtIP.MouseState = MaterialSkin.MouseState.HOVER;
            txtIP.Name = $"txtIP_{controlName}";
            txtIP.PasswordChar = '\0';
            txtIP.SelectedText = "";
            txtIP.SelectionLength = 0;
            txtIP.SelectionStart = 0;
            txtIP.Size = new System.Drawing.Size(175, 28);
            txtIP.TabIndex = 9;
            txtIP.TabStop = false;
            txtIP.Text = proxy == null ? string.Empty : proxy.IP;
            txtIP.UseSystemPasswordChar = false;

            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Depth = 0;
            lblPort.Font = new System.Drawing.Font("Roboto", 11F);
            lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lblPort.Location = new System.Drawing.Point(243, 21);
            lblPort.MouseState = MaterialSkin.MouseState.HOVER;
            lblPort.Name = $"lblPort_{controlName}";
            lblPort.Size = new System.Drawing.Size(45, 24);
            lblPort.TabIndex = 10;
            lblPort.Text = "Port";

            // 
            // lblIP
            // 
            lblIP.AutoSize = true;
            lblIP.Depth = 0;
            lblIP.Font = new System.Drawing.Font("Roboto", 11F);
            lblIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lblIP.Location = new System.Drawing.Point(9, 21);
            lblIP.MouseState = MaterialSkin.MouseState.HOVER;
            lblIP.Name = $"lblIP_{controlName}";
            lblIP.Size = new System.Drawing.Size(27, 24);
            lblIP.TabIndex = 8;
            lblIP.Text = "IP";

            // 
            // pnlProxy
            // 
            pnlProxy.Controls.Add(txtPort);
            pnlProxy.Controls.Add(txtIP);
            pnlProxy.Controls.Add(lblPort);
            pnlProxy.Controls.Add(lblIP);
            pnlProxy.Location = new System.Drawing.Point(280, 161);
            pnlProxy.Name = $"pnlProxy_{controlName}";
            pnlProxy.Size = new System.Drawing.Size(1, 66);
            pnlProxy.TabIndex = 15;

            // 
            // lblUserProxy
            // 
            lblUserProxy.AutoSize = true;
            lblUserProxy.Depth = 0;
            lblUserProxy.Font = new System.Drawing.Font("Roboto", 11F);
            lblUserProxy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lblUserProxy.Location = new System.Drawing.Point(182, 182);
            lblUserProxy.MouseState = MaterialSkin.MouseState.HOVER;
            lblUserProxy.Name = $"lblUserProxy_{controlName}";
            lblUserProxy.Size = new System.Drawing.Size(90, 24);
            lblUserProxy.TabIndex = 14;
            lblUserProxy.Text = "启用代理";

            // 
            // cbUserProxy
            // 
            cbUserProxy.AutoSize = true;
            cbUserProxy.Depth = 0;
            cbUserProxy.Font = new System.Drawing.Font("Roboto", 10F);
            cbUserProxy.Location = new System.Drawing.Point(150, 178);
            cbUserProxy.Margin = new System.Windows.Forms.Padding(0);
            cbUserProxy.MouseLocation = new System.Drawing.Point(-1, -1);
            cbUserProxy.MouseState = MaterialSkin.MouseState.HOVER;
            cbUserProxy.Name = "cbUserProxy_Default";
            cbUserProxy.Ripple = true;
            cbUserProxy.Size = new System.Drawing.Size(26, 30);
            cbUserProxy.TabIndex = 13;
            cbUserProxy.UseVisualStyleBackColor = true;
            cbUserProxy.CheckedChanged += delegate (object sender, EventArgs e)
            {
                if (cbUserProxy.Checked)
                {
                    System.Threading.Timer timer = null;
                    timer = new System.Threading.Timer((object state) =>
                    {
                        Action action = () =>
                        {
                            pnlProxy.Width += 9;
                        };
                        pnlProxy.BeginInvoke(action);

                        if (pnlProxy.Width <= 393)
                            timer.Change(1, System.Threading.Timeout.Infinite);
                    }, this, 1, System.Threading.Timeout.Infinite);
                }
                else
                {
                    System.Threading.Timer timer = null;
                    timer = new System.Threading.Timer((object state) =>
                    {
                        Action action = () =>
                        {
                            pnlProxy.Width -= 9;
                        };
                        pnlProxy.BeginInvoke(action);

                        if (pnlProxy.Width >= 1)
                            timer.Change(1, System.Threading.Timeout.Infinite);
                    }, this, 1, System.Threading.Timeout.Infinite);
                }
            };

            // 
            // lblMoneyWarning
            // 
            lblMoneyWarning.AutoSize = true;
            lblMoneyWarning.Depth = 0;
            lblMoneyWarning.Font = new System.Drawing.Font("Roboto", 11F);
            lblMoneyWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lblMoneyWarning.Location = new System.Drawing.Point(520, 133);
            lblMoneyWarning.MouseState = MaterialSkin.MouseState.HOVER;
            lblMoneyWarning.Name = $"lblMoneyWarning_{controlName}";
            lblMoneyWarning.Size = new System.Drawing.Size(90, 24);
            lblMoneyWarning.TabIndex = 11;
            lblMoneyWarning.Text = "余额预警";

            // 
            // txtMoneyWarning
            // 
            txtMoneyWarning.Depth = 0;
            txtMoneyWarning.Enabled = false;
            txtMoneyWarning.Hint = "";
            txtMoneyWarning.Location = new System.Drawing.Point(616, 129);
            txtMoneyWarning.MaxLength = 32767;
            txtMoneyWarning.MouseState = MaterialSkin.MouseState.HOVER;
            txtMoneyWarning.Name = $"txtMoneyWarning_{controlName}";
            txtMoneyWarning.PasswordChar = '\0';
            txtMoneyWarning.SelectedText = "";
            txtMoneyWarning.SelectionLength = 0;
            txtMoneyWarning.SelectionStart = 0;
            txtMoneyWarning.Size = new System.Drawing.Size(60, 28);
            txtMoneyWarning.TabIndex = 10;
            txtMoneyWarning.TabStop = false;
            txtMoneyWarning.UseSystemPasswordChar = false;
            txtMoneyWarning.Text = StopMoney == 0 ? string.Empty : StopMoney.ToString();

            // 
            // cbMoneyWarning
            // 
            cbMoneyWarning.AutoSize = true;
            cbMoneyWarning.Checked = true;
            cbMoneyWarning.CheckState = System.Windows.Forms.CheckState.Checked;
            cbMoneyWarning.Depth = 0;
            cbMoneyWarning.Font = new System.Drawing.Font("Roboto", 10F);
            cbMoneyWarning.Location = new System.Drawing.Point(488, 129);
            cbMoneyWarning.Margin = new System.Windows.Forms.Padding(0);
            cbMoneyWarning.MouseLocation = new System.Drawing.Point(-1, -1);
            cbMoneyWarning.MouseState = MaterialSkin.MouseState.HOVER;
            cbMoneyWarning.Name = $"cbMoneyWarning_{controlName}";
            cbMoneyWarning.Ripple = true;
            cbMoneyWarning.Size = new System.Drawing.Size(26, 30);
            cbMoneyWarning.TabIndex = 9;
            cbMoneyWarning.UseVisualStyleBackColor = true;
            cbMoneyWarning.CheckedChanged += delegate (object sender, EventArgs e)
            {
                txtMoneyWarning.Enabled = !cbMoneyWarning.Checked;
            };

            // 
            // lbldescription
            // 
            lbldescription.AutoSize = true;
            lbldescription.Font = new System.Drawing.Font("微软雅黑", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lbldescription.ForeColor = System.Drawing.Color.Blue;
            lbldescription.Location = new System.Drawing.Point(245, 136);
            lbldescription.Name = $"lbldescription_{controlName}";
            lbldescription.Size = new System.Drawing.Size(205, 19);
            lbldescription.TabIndex = 8;
            lbldescription.Text = "输入完成授权身份后会自动同步";

            // 
            // txtMoney
            // 
            txtMoney.Depth = 0;
            txtMoney.Enabled = false;
            txtMoney.Hint = "";
            txtMoney.Location = new System.Drawing.Point(102, 133);
            txtMoney.MaxLength = 32767;
            txtMoney.MouseState = MaterialSkin.MouseState.HOVER;
            txtMoney.Name = $"txtMoney_{controlName}";
            txtMoney.PasswordChar = '\0';
            txtMoney.SelectedText = "";
            txtMoney.SelectionLength = 0;
            txtMoney.SelectionStart = 0;
            txtMoney.Size = new System.Drawing.Size(137, 28);
            txtMoney.TabIndex = 7;
            txtMoney.TabStop = false;
            txtMoney.UseSystemPasswordChar = false;

            // 
            // lblMoney
            // 
            lblMoney.AutoSize = true;
            lblMoney.Depth = 0;
            lblMoney.Font = new System.Drawing.Font("Roboto", 11F);
            lblMoney.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lblMoney.Location = new System.Drawing.Point(6, 131);
            lblMoney.MouseState = MaterialSkin.MouseState.HOVER;
            lblMoney.Name = $"lblMoney_{controlName}";
            lblMoney.Size = new System.Drawing.Size(90, 24);
            lblMoney.TabIndex = 6;
            lblMoney.Text = "账户余额";

            // 
            // btnHow
            // 
            btnHow.AutoSize = true;
            btnHow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            btnHow.CustomFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            btnHow.Depth = 0;
            btnHow.Icon = null;
            btnHow.Location = new System.Drawing.Point(612, 73);
            btnHow.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            btnHow.MouseState = MaterialSkin.MouseState.HOVER;
            btnHow.Name = $"btnHow_{controlName}";
            btnHow.Primary = false;
            btnHow.Size = new System.Drawing.Size(165, 36);
            btnHow.TabIndex = 5;
            btnHow.Text = "How can i get it";
            btnHow.UseVisualStyleBackColor = true;

            // 
            // cbConfigLock
            // 
            cbConfigLock.AutoSize = true;
            cbConfigLock.Checked = true;
            cbConfigLock.CheckState = System.Windows.Forms.CheckState.Checked;
            cbConfigLock.Depth = 0;
            cbConfigLock.Font = new System.Drawing.Font("Roboto", 10F);
            cbConfigLock.Location = new System.Drawing.Point(359, 24);
            cbConfigLock.Margin = new System.Windows.Forms.Padding(0);
            cbConfigLock.MouseLocation = new System.Drawing.Point(-1, -1);
            cbConfigLock.MouseState = MaterialSkin.MouseState.HOVER;
            cbConfigLock.Name = "cbConfigLock_Default";
            cbConfigLock.Ripple = true;
            cbConfigLock.Size = new System.Drawing.Size(26, 30);
            cbConfigLock.TabIndex = 4;
            cbConfigLock.UseVisualStyleBackColor = true;
            cbConfigLock.CheckedChanged += delegate (object sender, EventArgs e)
            {
                txtConfigName.Enabled = !cbConfigLock.Checked;
                txtConfigName.Refresh();
            };

            // 
            // lblConfigName
            // 
            lblConfigName.AutoSize = true;
            lblConfigName.Depth = 0;
            lblConfigName.Font = new System.Drawing.Font("Roboto", 11F);
            lblConfigName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lblConfigName.Location = new System.Drawing.Point(6, 23);
            lblConfigName.MouseState = MaterialSkin.MouseState.HOVER;
            lblConfigName.Name = $"lblConfigName_{controlName}";
            lblConfigName.Size = new System.Drawing.Size(90, 24);
            lblConfigName.TabIndex = 3;
            lblConfigName.Text = "配置名称";

            // 
            // txtConfigName
            // 
            txtConfigName.Depth = 0;
            txtConfigName.Enabled = false;
            txtConfigName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            txtConfigName.Hint = "";
            txtConfigName.Location = new System.Drawing.Point(102, 22);
            txtConfigName.MaxLength = 32767;
            txtConfigName.MouseState = MaterialSkin.MouseState.HOVER;
            txtConfigName.Name = $"txtConfigName_{controlName}";
            txtConfigName.Text = ConfigurationName;
            txtConfigName.PasswordChar = '\0';
            txtConfigName.SelectedText = "";
            txtConfigName.SelectionLength = 0;
            txtConfigName.SelectionStart = 0;
            txtConfigName.Size = new System.Drawing.Size(236, 28);
            txtConfigName.TabIndex = 2;
            txtConfigName.TabStop = false;
            txtConfigName.UseSystemPasswordChar = false;
            txtConfigName.TextChanged += delegate (object sender, EventArgs e)
            {
                lblMoney_Title.Text = txtConfigName.Text;
                tsMaster.Refresh();
            };

            // 
            // txtAuthentication
            // 
            txtAuthentication.Depth = 0;
            txtAuthentication.Hint = "";
            txtAuthentication.Location = new System.Drawing.Point(102, 74);
            txtAuthentication.MaxLength = 32767;
            txtAuthentication.MouseState = MaterialSkin.MouseState.HOVER;
            txtAuthentication.Name = $"txtAuthentication_{controlName}";
            txtAuthentication.PasswordChar = '\0';
            txtAuthentication.SelectedText = "";
            txtAuthentication.SelectionLength = 0;
            txtAuthentication.SelectionStart = 0;
            txtAuthentication.Size = new System.Drawing.Size(499, 28);
            txtAuthentication.TabIndex = 1;
            txtAuthentication.TabStop = false;
            txtAuthentication.Text = Authentication;
            txtAuthentication.UseSystemPasswordChar = false;

            // 
            // lblAuthentication
            // 
            lblAuthentication.AutoSize = true;
            lblAuthentication.Depth = 0;
            lblAuthentication.Font = new System.Drawing.Font("Roboto", 11F);
            lblAuthentication.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lblAuthentication.Location = new System.Drawing.Point(6, 73);
            lblAuthentication.MouseState = MaterialSkin.MouseState.HOVER;
            lblAuthentication.Name = $"lblAuthentication_{controlName}";
            lblAuthentication.Size = new System.Drawing.Size(90, 24);
            lblAuthentication.TabIndex = 0;
            lblAuthentication.Text = "授权身份";

            //
            // txtUrl
            //
            txtUrl.Depth = 0;
            txtUrl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            txtUrl.Hint = "";
            txtUrl.Location = new System.Drawing.Point(508, 22);
            txtUrl.MaxLength = 32767;
            txtUrl.MouseState = MaterialSkin.MouseState.HOVER;
            txtUrl.Name = $"txtUrl_{controlName}";
            txtUrl.PasswordChar = '\0';
            txtUrl.SelectedText = "";
            txtUrl.SelectionLength = 0;
            txtUrl.SelectionStart = 0;
            txtUrl.Size = new System.Drawing.Size(314, 30);
            txtUrl.TabIndex = 19;
            txtUrl.TabStop = false;
            txtUrl.UseSystemPasswordChar = false;
            txtUrl.Text = Url;

            // 
            // lblUrl
            // 
            lblUrl.AutoSize = true;
            lblUrl.Depth = 0;
            lblUrl.Font = new System.Drawing.Font("Roboto", 11F);
            lblUrl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lblUrl.Location = new System.Drawing.Point(451, 23);
            lblUrl.MouseState = MaterialSkin.MouseState.HOVER;
            lblUrl.Name = $"lblUrl_{controlName}";
            lblUrl.Size = new System.Drawing.Size(50, 24);
            lblUrl.TabIndex = 20;
            lblUrl.Text = "网址";

            // 
            // btnOpenBrowser
            // 
            btnOpenBrowser.AutoSize = true;
            btnOpenBrowser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            btnOpenBrowser.CustomFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            btnOpenBrowser.Depth = 0;
            btnOpenBrowser.Icon = null;
            btnOpenBrowser.Location = new System.Drawing.Point(829, 18);
            btnOpenBrowser.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            btnOpenBrowser.MouseState = MaterialSkin.MouseState.HOVER;
            btnOpenBrowser.Name = $"btnOpenBrowser_{controlName}";
            btnOpenBrowser.Primary = false;
            btnOpenBrowser.Size = new System.Drawing.Size(60, 36);
            btnOpenBrowser.TabIndex = 21;
            btnOpenBrowser.Text = "OPEN";
            btnOpenBrowser.UseVisualStyleBackColor = true;

            // Init ListView
            mlvData.AddEditControl(0, new TextBox())
                   .AddEditControl(1, new ComboBox() { DataSource = new List<string>() { "庄", "闲", "和", "停" } })
                   .AddEditControl(2, new TextBox())
                   .AddEditControl(3, new TextBox())
                   .InitializeCustomControl();
            if (source != null)
            {
                //Add display data.
                var data = source.ConvertByJagged();
                foreach (string[] version in data)
                {
                    var item = new ListViewItem(version);
                    mlvData.Items.Add(item);
                }

                // Add click button.
                mlvData.AddEmbeddedButtons(delegate (object sender, EventArgs args)
                {
                    mlvData.RemoveActiveItem(sender as MaterialSkin.Controls.MaterialFlatButton);
                });
            }

            //
            // END
            // 
            lblMoney_Title.ResumeLayout(false);
            lblMoney_Title.PerformLayout();
            pnlProxy.ResumeLayout(false);
            pnlProxy.PerformLayout();
        }

        public event Action OnMainFormClosed;

        #region Event Handler
        private void FormController_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnMainFormClosed?.Invoke();
        } 
        #endregion
    }
}
