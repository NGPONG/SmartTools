using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace SmartTools.Views
{
    public partial class Main : MaterialForm
    {
        public Main(FormClosedEventHandler handler)
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.FormClosed += handler;
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
    }
}
