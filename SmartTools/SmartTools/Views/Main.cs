﻿using MaterialSkin.Controls;
using SmartTools.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartTools.Utils.Extensions;

namespace SmartTools.Views
{
    public partial class Main : MaterialForm
    {
        public Main(FormClosedEventHandler handler)
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.FormClosed += handler;

            this.mlvData_Default.AddCustomItem(0, new TextBox())
                                  .AddCustomItem(1, new ComboBox() { DataSource = new List<string>() { "庄", "闲", "和", "停" } })
                                  .AddCustomItem(2, new TextBox())
                                  .AddCustomItem(3, new TextBox())
                                  .InitializeCustomControl();

            var data = new List<CustomAction>()
            {
                new CustomAction(){ ActionIndex=1 , BetType=Bet.闲 , Delay=1000 , Money=100 , Action = "点击删除" },
                new CustomAction(){ ActionIndex=2 , BetType=Bet.和 , Delay=1000 , Money=50 , Action = "点击删除"},
                new CustomAction(){ ActionIndex=3 , BetType=Bet.闲 , Delay=1000 , Money=10, Action = "点击删除" },
                new CustomAction(){ ActionIndex=4 , BetType=Bet.庄 , Delay=1000 , Money=20 , Action = "点击删除"},
                new CustomAction(){ ActionIndex=5 , BetType=Bet.闲 , Delay=1000 , Money=200 , Action = "点击删除"},
                new CustomAction(){ ActionIndex=6 , BetType=Bet.闲 , Delay=1000 , Money=200 , Action = "点击删除"},
                new CustomAction(){ ActionIndex=7 , BetType=Bet.闲 , Delay=1000 , Money=200 , Action = "点击删除"},
                new CustomAction(){ ActionIndex=8 , BetType=Bet.闲 , Delay=1000 , Money=200 , Action = "点击删除"},
                new CustomAction(){ ActionIndex=9 , BetType=Bet.闲 , Delay=1000 , Money=200 , Action = "点击删除"},
                new CustomAction(){ ActionIndex=10 , BetType=Bet.闲 , Delay=1000 , Money=200 , Action = "点击删除"},
                new CustomAction(){ ActionIndex=11 , BetType=Bet.闲 , Delay=1000 , Money=200 , Action = "点击删除"}
            }
            .ConvertByJagged();

            //Add
            foreach (string[] version in data)
            {
                var item = new ListViewItem(version);
                mlvData_Default.Items.Add(item);
            }
        }

        private void CbConfigLock_Default_CheckedChanged(object sender, EventArgs e)
        {
            this.txtConfigName_Default.Enabled = !this.cbConfigLock_Default.Checked;
            this.txtConfigName_Default.Refresh();
        }

        private void TxtConfigName_Default_TextChanged(object sender, EventArgs e)
        {
            this.lblMoney_Title_Default.Text = this.txtConfigName_Default.Text;
            this.tsMaster.Refresh();
        }

        private void CbUserProxy_Default_CheckedChanged(object sender, EventArgs e)
        {
            // 66   1

            // 393
            if (this.cbUserProxy_Default.Checked)
            {
                System.Threading.Timer timer = null;
                timer = new System.Threading.Timer((object state) =>
                {
                    Action action = () =>
                    {
                        //if (this.pnlProxy_Default.Height >= 15)
                        //    this.pnlProxy_Default.Visible = true;
                        this.pnlProxy_Default.Width += 9;
                    };
                    pnlProxy_Default.BeginInvoke(action);

                    if (pnlProxy_Default.Width <= 393)
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
                        //if (this.pnlProxy_Default.Height <= 15)
                        //    this.pnlProxy_Default.Visible = false;
                        this.pnlProxy_Default.Width -= 9;
                    };
                    pnlProxy_Default.BeginInvoke(action);

                    if (pnlProxy_Default.Width >= 1)
                        timer.Change(1, System.Threading.Timeout.Infinite);
                }, this, 1, System.Threading.Timeout.Infinite);
            }
        }

        private void MlvData_Default_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = $"X:{e.X.ToString()}  Y:{e.Y.ToString()}";
            this.Refresh();
        }
    }
}
