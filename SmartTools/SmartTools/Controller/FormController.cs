using MaterialSkin;
using MaterialSkin.Controls;
using SmartTools.Properties;
using SmartTools.Utils;
using SmartTools.Views;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTools.Controller
{
    public class FormController
    {
        #region member
        private static MaterialSkinManager Themes;
        private static FormController controller = null;
        private static object locker = new object();
        #endregion

        #region private
        private int _colorSchemeIndex = 2;
        private const int _offSizeWidth = 48;
        #endregion

        #region controls
        private NotifyIcon _notifyIcon;
        #endregion

        private FormController() { FormManager.Instance().OnMainFormClosing += this.FormController_OnMainFormClosing; }

        public static FormController Instance()
        {
            if (controller == null)
            {
                lock (locker)
                {
                    if (controller == null)
                    {
                        controller = new FormController();
                    }
                }
            }

            return controller;
        }

        public void Start()
        {
            CreateNewMainForm();
        }

        public FormController InitNotify(bool visible)
        {
            if (!visible)
            {
                if (this._notifyIcon == null)
                {
                    this._notifyIcon = new NotifyIcon();
                    this._notifyIcon.ContextMenu = new ContextMenu(new MenuItem[]
                    {
                        new MenuItem("退出",new EventHandler((object sender,EventArgs args) =>
                        {
                            this._notifyIcon.Visible = false;
                            AutomateController.Instance().CloseAll(()=>
                            {
                                FormManager.Instance().Close();
                            });
                        }))
                    });
                }
                this._notifyIcon.Visible = visible;
                this._notifyIcon.Text = "Smart Tool ✔";
                this._notifyIcon.Icon = Icon.FromHandle(Resources.Sleep.GetHicon());
                this._notifyIcon.MouseClick -= this._notifyIcon_MouseClick;
                this._notifyIcon.MouseClick += this._notifyIcon_MouseClick;
            }
            else
            {
                this._notifyIcon.Visible = visible;
                this._notifyIcon.BalloonTipTitle = "Smart Tool ✔";
                this._notifyIcon.BalloonTipText = "程序正在后台运行";
                this._notifyIcon.ShowBalloonTip(1);
            }

            return this;
        }

        public FormController Init<T>(T frm)
            where T : Form
        {
            frm.Icon = Icon.FromHandle(Resources.Active.GetHicon());
            frm.StartPosition = FormStartPosition.CenterScreen;

            if (Themes == null)
            {
                Themes = MaterialSkinManager.Instance;
                Themes.ROBOTO_MEDIUM_12 = new Font("微软雅黑", 12f);
                Themes.ROBOTO_MEDIUM_10 = new Font("微软雅黑", 11f);
                Themes.ROBOTO_REGULAR_11 = new Font("微软雅黑", 11f);
                Themes.ROBOTO_MEDIUM_11 = new Font("微软雅黑", 11f);
                Themes.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
            }
            Themes.AddFormToManage(frm as MaterialForm);

            frm.Show();
            frm.Activate();
            frm.BringToFront();

            return this;
        }

        public FormController ChangeColorScheme()
        {
            _colorSchemeIndex++;
            if (_colorSchemeIndex > 6) _colorSchemeIndex = 0;

            //These are just example color schemes
            switch (_colorSchemeIndex)
            {
                case 0:
                    Themes.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
                    break;
                case 1:
                    Themes.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
                    break;
                case 2:
                    Themes.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
                    break;
                case 3:
                    Themes.ColorScheme = new ColorScheme(Primary.Amber500, Primary.Amber700, Primary.Amber100, Accent.Red100, TextShade.WHITE);
                    break;
                case 4:
                    Themes.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue200, Accent.Red100, TextShade.WHITE);
                    break;
                case 5:
                    Themes.ColorScheme = new ColorScheme(Primary.Brown600, Primary.Brown700, Primary.Brown200, Accent.Red100, TextShade.WHITE);
                    break;
                case 6:
                    Themes.ColorScheme = new ColorScheme(Primary.Teal600, Primary.Teal700, Primary.Purple200, Accent.Red100, TextShade.WHITE);
                    break;
            }

            return this;
        }

        public FormController SetDynamicSize<T>(T sender, Form frm)
            where T : Control
        {
            if (sender == null) throw new ArgumentNullException();

            frm.Width = sender.Width + _offSizeWidth;

            return this;
        }

        public FormController RePostion(Form frm)
        {
            frm.Left = (Screen.PrimaryScreen.Bounds.Width - frm.Width) / 2;
            frm.Top = (Screen.PrimaryScreen.Bounds.Height - frm.Height) / 2;

            return this;
        }

        private void CreateNewMainForm()
        {
            Init(FormManager.Instance().GetDefaultMainForm())
                .InitNotify(false);
        }

        #region Event Handler
        private void _notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _notifyIcon.Visible = false;
                FormManager.Instance().Show();
            }
        }
        private void FormController_OnMainFormClosing()
        {
            Native.ReleaseMemory();
            InitNotify(true);
        }
        #endregion
    }
}
