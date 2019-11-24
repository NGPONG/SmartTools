using SmartTools.Common.Helper;
using SmartTools.Model;
using SmartTools.Utils;
using SmartTools.Utils.Extensions;
using SmartTools.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace SmartTools.Controller
{
    public class FormManager
    {
        #region member
        private static FormManager manager = null;
        private static object locker = new object();

        private Main mainForm;
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

        private void InitializeComponent()
        {
            //HttpController http = new HttpController() { header=new Header() { Method = Method.POST,Address= @"https://98613p.com/Account/GetMyBalance",ContentType= } }
            #region Init control
            var tcMaster = new MaterialSkin.Controls.MaterialTabControl();
            var tsMaster = new MaterialSkin.Controls.MaterialTabSelector();
            var materialDivider = new MaterialSkin.Controls.MaterialDivider();
            var btnStart = new MaterialSkin.Controls.MaterialRaisedButton();
            var btnAddConfig = new MaterialSkin.Controls.MaterialRaisedButton();
            var btnDeleteConfig = new MaterialSkin.Controls.MaterialRaisedButton();

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

            //
            // Tab controler
            //
            tcMaster.Depth = 0;
            tcMaster.Location = new System.Drawing.Point(12, 134);
            tcMaster.MouseState = MaterialSkin.MouseState.HOVER;
            tcMaster.Name = "tcMaster";
            tcMaster.SelectedIndex = 0;
            tcMaster.Size = new System.Drawing.Size(906, 559);
            tcMaster.TabIndex = 0;
            tcMaster.SelectedIndexChanged += delegate (object sender, EventArgs e)
            {
                if ((Boolean)tcMaster.SelectedTab.Tag)
                {
                    btnStart.Text = "停止";
                }
                else
                {
                    btnStart.Text = "开始";
                }
            };

            //
            // Tab selecteor
            //
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
            foreach (KeyValuePair<string,Configuration> config in configs)
            {
                CreateConfigControls(config.Key,
                                     tsMaster,
                                     tcMaster,
                                     btnStart,
                                     config.Value.Authentication,
                                     config.Value.Url,
                                     config.Value.StopMoney,
                                     config.Value.IsCycle,
                                     config.Value.IsMoneyWarning,
                                     config.Value.Proxy,
                                     config.Value.Action);
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
            btnStart.Click += delegate (object sender, EventArgs e)
            {
                var configName = tcMaster.SelectedTab.Text;

                if (btnStart.Text == "开始")
                {
                    AutomateController.Instance().StartAction(configName);
                }
                else
                {
                    AutomateController.Instance().StopAction(configName);
                }
            };


            // 
            // btnAddConfig
            // 
            btnAddConfig.AutoSize = true;
            btnAddConfig.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            btnAddConfig.Depth = 0;
            btnAddConfig.Icon = null;
            btnAddConfig.Location = new System.Drawing.Point(680, 731);
            btnAddConfig.MouseState = MaterialSkin.MouseState.HOVER;
            btnAddConfig.Name = "btnAddConfig";
            btnAddConfig.Primary = true;
            btnAddConfig.Size = new System.Drawing.Size(173, 36);
            btnAddConfig.TabIndex = 6;
            btnAddConfig.Text = "添加一页新的配置";
            btnAddConfig.UseVisualStyleBackColor = true;
            btnAddConfig.Click += delegate (object sender, EventArgs e)
            {
                CreateConfigControls($"Config_{tcMaster.TabPages.Count + 1}",
                                     tsMaster,
                                     tcMaster,
                                     btnStart);
                tcMaster.SelectedIndex = tcMaster.TabPages.Count - 1;
                tsMaster.Invalidate();
            };

            // 
            // btnDeleteConfig
            // 
            btnDeleteConfig.AutoSize = true;
            btnDeleteConfig.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            btnDeleteConfig.Depth = 0;
            btnDeleteConfig.Icon = null;
            btnDeleteConfig.Location = new System.Drawing.Point(482, 731);
            btnDeleteConfig.MouseState = MaterialSkin.MouseState.HOVER;
            btnDeleteConfig.Name = "materialRaisedButton1";
            btnDeleteConfig.Primary = true;
            btnDeleteConfig.Size = new System.Drawing.Size(192, 36);
            btnDeleteConfig.TabIndex = 7;
            btnDeleteConfig.Text = "删除当前选中的配置";
            btnDeleteConfig.UseVisualStyleBackColor = true;
            btnDeleteConfig.Click += delegate (object sender, EventArgs e)
            {
                btnDeleteConfig.Enabled = false;

                var activePage = tcMaster.TabPages[tcMaster.SelectedIndex];
                if (activePage.Text == "默认配置")
                {
                    btnDeleteConfig.Enabled = true;
                    MessageBoxExt.Show("无法删除默认配置", MessageboxType.Info);
                    return;
                }

                tcMaster.SelectedIndex -= 1;
                tsMaster.Invalidate();
                Application.DoEvents();

                Task.Run(() =>
                {
                    Thread.Sleep(250);
                    mainForm.Invoke(new Action(() =>
                    {
                        tcMaster.TabPages.Remove(activePage);
                        ConfigurationManager.Instance().RemoveConfig(activePage.Text);
                        btnDeleteConfig.Enabled = true;
                    }));
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
            mainForm.Controls.Add(btnStart);
            mainForm.Controls.Add(btnAddConfig);
            mainForm.Controls.Add(btnDeleteConfig);
            mainForm.Controls.Add(materialDivider);
            mainForm.Controls.Add(tsMaster);
            mainForm.Controls.Add(tcMaster);

            //
            // END
            //
            tcMaster.ResumeLayout(false);
            mainForm.ResumeLayout(false);
            mainForm.PerformLayout();
            #endregion
        }

        private void CreateConfigControls(string ConfigurationName,
                                          MaterialSkin.Controls.MaterialTabSelector tsMaster,
                                          MaterialSkin.Controls.MaterialTabControl tcMaster,
                                          MaterialSkin.Controls.MaterialRaisedButton btnStart,
                                          string Authentication = "",
                                          string Url = "",
                                          string StopMoney = "",
                                          bool IsCycle = false,
                                          bool IsMoneyWarning = false,
                                          Proxy proxy = null,
                                          List<CustomAction> source = null)
        {
            var mlvData = new MaterialSkin.Controls.MaterialListView();
            var lblMoney_Title = new TabPage();
            var chActionIndex = new ColumnHeader();
            var chBetType = new ColumnHeader();
            var chDelay = new ColumnHeader();
            var chMoney = new ColumnHeader();
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
            var btnAdd = new MaterialSkin.Controls.MaterialFlatButton();
            var psWait_Open = new MaterialSkin.Controls.MaterialProgressSpinner();
            var psWait_Start = new MaterialSkin.Controls.MaterialProgressSpinner();

            //
            // START
            // 
            lblMoney_Title.SuspendLayout();
            pnlProxy.SuspendLayout();
            tcMaster.TabPages.Add(lblMoney_Title);

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
            lblMoney_Title.Controls.Add(btnAdd);
            lblMoney_Title.Controls.Add(psWait_Open);
            lblMoney_Title.Controls.Add(psWait_Start);
            lblMoney_Title.Location = new System.Drawing.Point(4, 25);
            lblMoney_Title.Name = $"lblMoney_Title_{ConfigurationName}";
            lblMoney_Title.Tag = false; // Action status.
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
            lblIsCycle.Name = $"lblIsCycle{ConfigurationName}";
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
            cbIsCycle.Name = $"cblblIsCycle_{ConfigurationName}";
            cbIsCycle.Ripple = true;
            cbIsCycle.Size = new System.Drawing.Size(26, 30);
            cbIsCycle.TabIndex = 17;
            cbIsCycle.Checked = IsCycle;
            cbIsCycle.UseVisualStyleBackColor = true;
            cbIsCycle.Checked = true;
            cbIsCycle.CheckedChanged += delegate (object sender, EventArgs e)
            {
                ConfigurationManager.Instance().AddConfig(lblMoney_Title);
            };

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
            mlvData.Name = $"mlvData_{ConfigurationName}";
            mlvData.OwnerDraw = true;
            mlvData.Size = new System.Drawing.Size(892, 345);
            mlvData.TabIndex = 16;
            mlvData.UseCompatibleStateImageBehavior = false;
            mlvData.View = System.Windows.Forms.View.Details;
            mlvData.OnCustomContolValueChanged += delegate (object sender, EventArgs args)
            {
                ConfigurationManager.Instance().AddConfig(lblMoney_Title);
            };

            // 
            // txtPort
            // 
            txtPort.Depth = 0;
            txtPort.Hint = "";
            txtPort.Location = new System.Drawing.Point(296, 20);
            txtPort.MaxLength = 32767;
            txtPort.MouseState = MaterialSkin.MouseState.HOVER;
            txtPort.Name = $"txtPort_{ConfigurationName}";
            txtPort.PasswordChar = '\0';
            txtPort.SelectedText = "";
            txtPort.SelectionLength = 0;
            txtPort.SelectionStart = 0;
            txtPort.Size = new System.Drawing.Size(70, 28);
            txtPort.TabIndex = 11;
            txtPort.TabStop = false;
            txtPort.Text = proxy == null || (string.IsNullOrEmpty(proxy.IP) && string.IsNullOrEmpty(proxy.Port)) ? string.Empty : proxy.Port.ToString();
            txtPort.UseSystemPasswordChar = false;
            txtPort.Leave += delegate (object sender, EventArgs e)
            {
                ConfigurationManager.Instance().AddConfig(lblMoney_Title);
            };

            // 
            // txtIP
            // 
            txtIP.Depth = 0;
            txtIP.Hint = "";
            txtIP.Location = new System.Drawing.Point(39, 20);
            txtIP.MaxLength = 32767;
            txtIP.MouseState = MaterialSkin.MouseState.HOVER;
            txtIP.Name = $"txtIP_{ConfigurationName}";
            txtIP.PasswordChar = '\0';
            txtIP.SelectedText = "";
            txtIP.SelectionLength = 0;
            txtIP.SelectionStart = 0;
            txtIP.Size = new System.Drawing.Size(175, 28);
            txtIP.TabIndex = 9;
            txtIP.TabStop = false;
            txtIP.Text = proxy == null || (string.IsNullOrEmpty(proxy.IP) && string.IsNullOrEmpty(proxy.Port)) ? string.Empty : proxy.IP.ToString();
            txtIP.UseSystemPasswordChar = false;
            txtIP.Leave += delegate (object sender, EventArgs e)
            {
                ConfigurationManager.Instance().AddConfig(lblMoney_Title);
            };

            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Depth = 0;
            lblPort.Font = new System.Drawing.Font("Roboto", 11F);
            lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lblPort.Location = new System.Drawing.Point(243, 21);
            lblPort.MouseState = MaterialSkin.MouseState.HOVER;
            lblPort.Name = $"lblPort_{ConfigurationName}";
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
            lblIP.Name = $"lblIP_{ConfigurationName}";
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
            pnlProxy.Name = $"pnlProxy_{ConfigurationName}";
            pnlProxy.Size = new System.Drawing.Size(proxy == null || (string.IsNullOrEmpty(proxy.IP) && string.IsNullOrEmpty(proxy.Port)) ? 1 : 393, 66);
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
            lblUserProxy.Name = $"lblUserProxy_{ConfigurationName}";
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
            cbUserProxy.Checked = proxy == null || (string.IsNullOrEmpty(proxy.IP) && string.IsNullOrEmpty(proxy.Port)) ? false : true;
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
            lblMoneyWarning.Name = $"lblMoneyWarning_{ConfigurationName}";
            lblMoneyWarning.Size = new System.Drawing.Size(90, 24);
            lblMoneyWarning.TabIndex = 11;
            lblMoneyWarning.Text = "余额预警";

            // 
            // txtMoneyWarning
            // 
            txtMoneyWarning.Depth = 0;
            txtMoneyWarning.Enabled = true;
            txtMoneyWarning.Hint = "";
            txtMoneyWarning.Location = new System.Drawing.Point(616, 129);
            txtMoneyWarning.MaxLength = 32767;
            txtMoneyWarning.MouseState = MaterialSkin.MouseState.HOVER;
            txtMoneyWarning.Name = $"txtMoneyWarning_{ConfigurationName}";
            txtMoneyWarning.PasswordChar = '\0';
            txtMoneyWarning.SelectedText = "";
            txtMoneyWarning.SelectionLength = 0;
            txtMoneyWarning.SelectionStart = 0;
            txtMoneyWarning.Size = new System.Drawing.Size(60, 28);
            txtMoneyWarning.TabIndex = 10;
            txtMoneyWarning.TabStop = false;
            txtMoneyWarning.UseSystemPasswordChar = false;
            txtMoneyWarning.Text = StopMoney;
            txtMoneyWarning.Leave += delegate (object sender, EventArgs e)
            {
                ConfigurationManager.Instance().AddConfig(lblMoney_Title);
            };

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
            cbMoneyWarning.Name = $"cbMoneyWarning_{ConfigurationName}";
            cbMoneyWarning.Ripple = true;
            cbMoneyWarning.Size = new System.Drawing.Size(26, 30);
            cbMoneyWarning.TabIndex = 9;
            cbMoneyWarning.Checked = IsMoneyWarning;
            cbMoneyWarning.UseVisualStyleBackColor = true;

            cbMoneyWarning.CheckedChanged += delegate (object sender, EventArgs e)
            {
                ConfigurationManager.Instance().AddConfig(lblMoney_Title);
            };

            // 
            // lbldescription
            // 
            lbldescription.AutoSize = true;
            lbldescription.Font = new System.Drawing.Font("微软雅黑", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lbldescription.ForeColor = System.Drawing.Color.Blue;
            lbldescription.Location = new System.Drawing.Point(245, 136);
            lbldescription.Name = $"lbldescription_{ConfigurationName}";
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
            txtMoney.Name = $"txtMoney_{ConfigurationName}";
            txtMoney.PasswordChar = '\0';
            txtMoney.SelectedText = "";
            txtMoney.SelectionLength = 0;
            txtMoney.SelectionStart = 0;
            txtMoney.Size = new System.Drawing.Size(137, 28);
            txtMoney.TabIndex = 7;
            txtMoney.TabStop = false;
            txtMoney.UseSystemPasswordChar = false;
            txtMoney.Leave += delegate (object sender, EventArgs e)
            {
                ConfigurationManager.Instance().AddConfig(lblMoney_Title);
            };

            // 
            // lblMoney
            // 
            lblMoney.AutoSize = true;
            lblMoney.Depth = 0;
            lblMoney.Font = new System.Drawing.Font("Roboto", 11F);
            lblMoney.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lblMoney.Location = new System.Drawing.Point(6, 131);
            lblMoney.MouseState = MaterialSkin.MouseState.HOVER;
            lblMoney.Name = $"lblMoney_{ConfigurationName}";
            lblMoney.Size = new System.Drawing.Size(90, 24);
            lblMoney.TabIndex = 6;
            lblMoney.Text = "账户余额";

            // 
            // btnHow
            // 
            btnHow.AutoSize = true;
            btnHow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            btnHow.CustomFont = new System.Drawing.Font("微软雅黑", 10F);
            btnHow.Depth = 0;
            btnHow.Icon = null;
            btnHow.Location = new System.Drawing.Point(612, 73);
            btnHow.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            btnHow.MouseState = MaterialSkin.MouseState.HOVER;
            btnHow.Name = $"btnHow_{ConfigurationName}";
            btnHow.Primary = false;
            btnHow.Size = new System.Drawing.Size(165, 36);
            btnHow.TabIndex = 5;
            btnHow.Text = "HOW CAN I GET IT";
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
            cbConfigLock.Name = $"cbConfigLock_{ConfigurationName}";
            cbConfigLock.Ripple = true;
            cbConfigLock.Size = new System.Drawing.Size(26, 30);
            cbConfigLock.TabIndex = 4;
            cbConfigLock.UseVisualStyleBackColor = true;
            cbConfigLock.CheckedChanged += delegate (object sender, EventArgs e)
            {
                txtConfigName.Enabled = !cbConfigLock.Checked;
                txtConfigName.Invalidate();

                ConfigurationManager.Instance().AddConfig(lblMoney_Title);
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
            lblConfigName.Name = $"lblConfigName_{ConfigurationName}";
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
            txtConfigName.Name = $"txtConfigName_{ConfigurationName}";
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
                tsMaster.Invalidate();
            };
            txtConfigName.Leave += delegate (object sender, EventArgs e)
            {
                ConfigurationManager.Instance().AddConfig(lblMoney_Title);
            };

            // 
            // txtAuthentication
            // 
            txtAuthentication.Depth = 0;
            txtAuthentication.Hint = "";
            txtAuthentication.Location = new System.Drawing.Point(102, 74);
            txtAuthentication.MaxLength = 32767;
            txtAuthentication.MouseState = MaterialSkin.MouseState.HOVER;
            txtAuthentication.Name = $"txtAuthentication_{ConfigurationName}";
            txtAuthentication.PasswordChar = '\0';
            txtAuthentication.SelectedText = "";
            txtAuthentication.SelectionLength = 0;
            txtAuthentication.SelectionStart = 0;
            txtAuthentication.Size = new System.Drawing.Size(499, 28);
            txtAuthentication.TabIndex = 2;
            txtAuthentication.TabStop = false;
            txtAuthentication.Text = Authentication;
            txtAuthentication.UseSystemPasswordChar = false;
            txtAuthentication.Leave += async delegate (object sender, EventArgs e)
            {
                var response = await ConfigurationManager.Instance().AddConfig(lblMoney_Title)
                                                                    .Configs[ConfigurationName]
                                                                    .GetBalanceAsync();
                double balance;
                var isMatch = double.TryParse(response, out balance);
                if (isMatch)
                {
                    txtMoney.Text = balance.ToString();
                }
            };

            // 
            // lblAuthentication
            // 
            lblAuthentication.AutoSize = true;
            lblAuthentication.Depth = 0;
            lblAuthentication.Font = new System.Drawing.Font("Roboto", 11F);
            lblAuthentication.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lblAuthentication.Location = new System.Drawing.Point(6, 73);
            lblAuthentication.MouseState = MaterialSkin.MouseState.HOVER;
            lblAuthentication.Name = $"lblAuthentication_{ConfigurationName}";
            lblAuthentication.Size = new System.Drawing.Size(90, 24);
            lblAuthentication.TabIndex = 0;
            lblAuthentication.Text = "授权身份";

            //
            // txtUrl
            //
            txtUrl.Depth = 0;
            txtUrl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            txtUrl.Hint = "";
            txtUrl.Location = new System.Drawing.Point(498, 22);
            txtUrl.MaxLength = 32767;
            txtUrl.MouseState = MaterialSkin.MouseState.HOVER;
            txtUrl.Name = $"txtUrl_{ConfigurationName}";
            txtUrl.PasswordChar = '\0';
            txtUrl.SelectedText = "";
            txtUrl.SelectionLength = 0;
            txtUrl.SelectionStart = 0;
            txtUrl.Size = new System.Drawing.Size(314, 30);
            txtUrl.TabIndex = 1;
            txtUrl.TabStop = false;
            txtUrl.UseSystemPasswordChar = false;
            txtUrl.Text = Url;
            txtUrl.Leave += delegate (object sender, EventArgs e)
            {
                ConfigurationManager.Instance().AddConfig(lblMoney_Title);
            };

            // 
            // lblUrl
            // 
            lblUrl.AutoSize = true;
            lblUrl.Depth = 0;
            lblUrl.Font = new System.Drawing.Font("Roboto", 11F);
            lblUrl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lblUrl.Location = new System.Drawing.Point(441, 23);
            lblUrl.MouseState = MaterialSkin.MouseState.HOVER;
            lblUrl.Name = $"lblUrl_{ConfigurationName}";
            lblUrl.Size = new System.Drawing.Size(50, 24);
            lblUrl.TabIndex = 20;
            lblUrl.Text = "网址";

            // 
            // btnOpenBrowser
            // 
            btnOpenBrowser.AutoSize = true;
            btnOpenBrowser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            btnOpenBrowser.CustomFont = new System.Drawing.Font("微软雅黑", 10F);
            btnOpenBrowser.Depth = 0;
            btnOpenBrowser.Icon = null;
            btnOpenBrowser.Location = new System.Drawing.Point(814, 20);
            btnOpenBrowser.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            btnOpenBrowser.MouseState = MaterialSkin.MouseState.HOVER;
            btnOpenBrowser.Name = $"btnOpenBrowser_{ConfigurationName}";
            btnOpenBrowser.Primary = false;
            btnOpenBrowser.TabIndex = 21;
            btnOpenBrowser.Text = "OPEN";
            btnOpenBrowser.Tag = false; // Browser open state.
            btnOpenBrowser.UseVisualStyleBackColor = true;
            btnOpenBrowser.Click += delegate (object sender, EventArgs e)
            {
                if (!VerifyURLValidity(txtUrl.Text))
                {
                    MessageBoxExt.Show("请输入正确的地址", MessageboxType.Info);
                    return;
                }

                btnOpenBrowser.Enabled = false;
                psWait_Open.Start();
                if (!(bool)btnOpenBrowser.Tag)
                {
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            AutomateController.Instance().Open(ConfigurationName, txtUrl.Text,
                            () => // Opened
                            {
                                psWait_Open.Invoke(new Action(() =>
                                {
                                    //Win32.SetActiveWindow(mainForm.Handle);
                                    psWait_Open.Stop();
                                    btnOpenBrowser.Text = "CLOS";
                                    btnOpenBrowser.Tag = true;
                                    btnOpenBrowser.Enabled = true;
                                }));
                            },
                            () => // Closed
                            {
                                psWait_Open.Invoke(new Action(() =>
                                {
                                    psWait_Open.Stop();
                                    btnOpenBrowser.Text = "OPEN";
                                    btnOpenBrowser.Tag = false;
                                    btnOpenBrowser.Enabled = true;
                                }));
                            },
                            ()=> // Start
                            {
                                psWait_Start.Invoke(new Action(() =>
                                {
                                    psWait_Start.Start();
                                    btnStart.Text = "停止";
                                    btnAdd.Enabled = false;
                                    lblMoney_Title.Tag = true;
                                }));
                            },
                            ()=> // Stop
                            {
                                psWait_Start.Invoke(new Action(() =>
                                {
                                    psWait_Start.Stop();
                                    btnStart.Text = "开始";
                                    btnAdd.Enabled = true;
                                    lblMoney_Title.Tag = false;
                                }));
                            },
                            (string balance)=> 
                            {
                                if (Convert.ToDouble(balance) > 0)
                                    txtMoney.Invoke(new Action(() =>
                                    {
                                        txtMoney.Text = balance;
                                    }));
                            });
                        }
                        catch (Exception ex)
                        {
                            mainForm.Invoke(new Action(() =>
                            {
                                MessageBoxExt.Show(ex.Message, MessageboxType.Error);
                            }));
                        }
                    }, TaskCreationOptions.LongRunning);
                }
                else
                {
                    Task.Run(()=> 
                    {
                        AutomateController.Instance().Close(ConfigurationName);
                    });
                }
            };

            // 
            // btnAdd
            // 
            btnAdd.AutoSize = true;
            btnAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            btnAdd.CustomFont = new System.Drawing.Font("微软雅黑", 10F);
            btnAdd.Depth = 0;
            btnAdd.Icon = null;
            btnAdd.Location = new System.Drawing.Point(876, 225);
            btnAdd.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            btnAdd.MouseState = MaterialSkin.MouseState.HOVER;
            btnAdd.Name = $"btnAdd_{ConfigurationName}";
            btnAdd.Primary = false;
            btnAdd.Size = new System.Drawing.Size(32, 36);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "+";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.BringToFront();
            btnAdd.Click += delegate (object sender, EventArgs e)
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
                actionNew.ActionIndex = mlvData.Items.Count == 0 ? lastItemIndex.ToString() : (++lastItemIndex).ToString();

                var listViewItem = new ListViewItem(actionNew.ConvertToArrary());
                mlvData.Items.Add(listViewItem);

                mlvData.AddEmbeddedButton(delegate (object o, EventArgs args)
                {
                    mlvData.RemoveActiveItem(o as MaterialSkin.Controls.MaterialFlatButton);
                    ConfigurationManager.Instance().AddConfig(lblMoney_Title);
                });

                ConfigurationManager.Instance().AddConfig(lblMoney_Title);
            };

            // 
            // psWait_Open
            // 
            psWait_Open.CustomBackground = false;
            psWait_Open.Location = new System.Drawing.Point(890, 30);
            psWait_Open.Maximum = 100;
            psWait_Open.Name = $"psWait_Open_{ConfigurationName}";
            psWait_Open.Size = new System.Drawing.Size(16, 16);
            psWait_Open.TabIndex = 22;
            psWait_Open.Value = 50;
            psWait_Open.Visible = false;

            // 
            // psWait_Start
            // 
            psWait_Start.CustomBackground = false;
            psWait_Start.Location = new System.Drawing.Point(880, 185);
            psWait_Start.Maximum = 100;
            psWait_Start.Name = $"psWait_Start_{ConfigurationName}";
            psWait_Start.Size = new System.Drawing.Size(23, 23);
            psWait_Start.TabIndex = 22;
            psWait_Start.Value = 50;
            psWait_Start.Speed = 2F;
            psWait_Start.Visible = false;

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

                if (data.Cast<string[]>().Count() > 0)
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

            if (!ConfigurationManager.Instance().Configs.ContainsKey(ConfigurationName))
            {
                ConfigurationManager.Instance().Configs[ConfigurationName] = Configuration.CreateDefualtConfig(ConfigurationName);
            }
        }
        private bool VerifyURLValidity(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            if (!Regex.IsMatch(url, @"(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]"))
                return false;

            return true;
        }

        public void Show()
        {
            this.mainForm.Show();
            this.mainForm.Visible = true;
        }

        public void Close()
        {
            mainForm.FormClosing -= MainForm_FormClosing;
            mainForm.Invoke(new Action(() =>
            {
                mainForm.Close();
            }));
        }

        public Main GetDefaultMainForm()
        {
            mainForm = new Main();
            mainForm.FormClosing += this.MainForm_FormClosing;
            InitializeComponent();
#if !DEBUG
            Win32.SetWindowPos(mainForm.Handle, Native.HWND_TOPMOST, 0, 0, 0, 0, Native.SWP_NOMOVE | Native.SWP_NOSIZE | Native.SWP_SHOWWINDOW);
#endif
            return mainForm;
        }

#region Event Handler
        public event Action OnMainFormClosing;
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.Visible = false;
            e.Cancel = true;
            ConfigurationManager.Instance().SaveConfig();
            OnMainFormClosing?.Invoke();
        }
#endregion
    }
}
