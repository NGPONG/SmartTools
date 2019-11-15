using MaterialSkin.Controls;
using SmartTools.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartTools.Utils.Extensions;
using SmartTools.Controller;

namespace SmartTools.Views
{
    public partial class Main : MaterialForm
    {
        public Main()
        {
            //InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.FormClosed += handler;

            //this.mlvData_Default.AddEditControl(0, new TextBox())
            //                    .AddEditControl(1, new ComboBox() { DataSource = new List<string>() { "庄", "闲", "和", "停" } })
            //                    .AddEditControl(2, new TextBox())
            //                    .AddEditControl(3, new TextBox())
            //                    .InitializeCustomControl();

            //var data = new List<CustomAction>()
            //{
            //    new CustomAction(){ ActionIndex=1 , BetType=Bet.闲 , Delay=1000 , Money=100  },
            //    new CustomAction(){ ActionIndex=2 , BetType=Bet.和 , Delay=1000 , Money=50   },
            //    new CustomAction(){ ActionIndex=3 , BetType=Bet.闲 , Delay=1000 , Money=10   },
            //    new CustomAction(){ ActionIndex=4 , BetType=Bet.庄 , Delay=1000 , Money=20   },
            //    new CustomAction(){ ActionIndex=5 , BetType=Bet.闲 , Delay=1000 , Money=200  },
            //    new CustomAction(){ ActionIndex=6 , BetType=Bet.闲 , Delay=1000 , Money=200  },
            //    new CustomAction(){ ActionIndex=7 , BetType=Bet.闲 , Delay=1000 , Money=200  },
            //    new CustomAction(){ ActionIndex=8 , BetType=Bet.闲 , Delay=1000 , Money=200  },
            //    new CustomAction(){ ActionIndex=9 , BetType=Bet.闲 , Delay=1000 , Money=200  },
            //    new CustomAction(){ ActionIndex=10 , BetType=Bet.闲 , Delay=1000 , Money=200 },
            //    new CustomAction(){ ActionIndex=11 , BetType=Bet.闲 , Delay=1000 , Money=200 }
            //}
            //.ConvertByJagged();

            ////Add
            //foreach (string[] version in data)
            //{
            //    var item = new ListViewItem(version);
            //    mlvData_Default.Items.Add(item);
            //}

            //this.mlvData_Default.AddEmbeddedButtons(delegate (object sender, EventArgs args)
            //{
            //    this.mlvData_Default.RemoveActiveItem(sender as MaterialFlatButton);
            //});

        }

        private void CbConfigLock_Default_CheckedChanged(object sender, EventArgs e)
        {
            this.txtConfigName_Default.Enabled = !this.cbConfigLock_Default.Checked;
            this.txtConfigName_Default.Invalidate();
        }

        private void TxtConfigName_Default_TextChanged(object sender, EventArgs e)
        {
            this.lblMoney_Title_Default.Text = this.txtConfigName_Default.Text;
            this.tsMaster.Invalidate();
        }

        private void CbUserProxy_Default_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbUserProxy_Default.Checked)
            {
                System.Threading.Timer timer = null;
                timer = new System.Threading.Timer((object state) =>
                {
                    Action action = () =>
                    {
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
                        this.pnlProxy_Default.Width -= 9;
                    };
                    pnlProxy_Default.BeginInvoke(action);

                    if (pnlProxy_Default.Width >= 1)
                        timer.Change(1, System.Threading.Timeout.Infinite);
                }, this, 1, System.Threading.Timeout.Infinite);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            int lastItemIndex;
            if (this.mlvData_Default.Items.Count == 0)
            {
                lastItemIndex = 1;
            }
            else
            {
                var lastListViewItem = this.mlvData_Default.Items[mlvData_Default.Items.Count - 1];
                lastItemIndex = Convert.ToInt32(lastListViewItem.SubItems[0].Text);
            }

            var actionNew = CustomAction.GetDefaultCustomAction();
            actionNew.ActionIndex = this.mlvData_Default.Items.Count == 0 ? lastItemIndex.ToString() : (++lastItemIndex).ToString();

            var listViewItem = new ListViewItem(actionNew.ConvertToArrary());
            this.mlvData_Default.Items.Add(listViewItem);

            this.mlvData_Default.AddEmbeddedButton(delegate (object o, EventArgs args)
            {
                this.mlvData_Default.RemoveActiveItem(o as MaterialFlatButton);
            });
        }

        private void CbMoneyWarning_Default_CheckedChanged(object sender, EventArgs e)
        {
            this.txtMoneyWarning_Default.Enabled = !this.cbMoneyWarning_Default.Checked;
        }
    }
}
