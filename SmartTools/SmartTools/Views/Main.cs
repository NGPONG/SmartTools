using MaterialSkin.Controls;
using SmartTools.Controller;
using SmartTools.Properties;
using SmartTools.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTools.Views
{
    public partial class Main : MaterialForm
    {
        FormController control = new FormController();
        public Main()
        {
            InitializeComponent();
            InitNotifyIcon();
            control.Init(this);
        }

        private void InitNotifyIcon()
        {
            this.notifyIcon.Text = "Smart Tool ✔";
            this.notifyIcon.Visible = false;
            this.notifyIcon.Icon = Icon.FromHandle(Resources.Sleep.GetHicon());
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
            this.notifyIcon.Visible = true;
            this.notifyIcon.BalloonTipTitle = "Smart Tool ✔";
            this.notifyIcon.BalloonTipText = "程序正在后台运行";
            this.notifyIcon.ShowBalloonTip(1);
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.RePostion();
                this.Visible = true;
                this.notifyIcon.Visible = false;
            }
        }
    }
}
