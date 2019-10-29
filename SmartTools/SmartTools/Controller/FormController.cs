using MaterialSkin;
using MaterialSkin.Controls;
using SmartTools.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartTools.Controller
{
    public class FormController
    {
        #region member
        public static MaterialSkinManager Themes;
        #endregion

        #region private
        private int _colorSchemeIndex = 2;
        private const int _offSizeWidth = 48;
        #endregion

        public void Init(Form frm)
        {
            frm.Icon = Icon.FromHandle(Resources.Active.GetHicon());
            frm.StartPosition = FormStartPosition.CenterScreen;
            if (Themes == null)
            {
                Themes = MaterialSkinManager.Instance;
                Themes.ROBOTO_MEDIUM_12 = new Font("微软雅黑", 12f);
                Themes.ROBOTO_MEDIUM_10 = new Font("微软雅黑", 10f);
                Themes.ROBOTO_REGULAR_11 = new Font("微软雅黑", 11f);
                Themes.ROBOTO_MEDIUM_11 = new Font("微软雅黑", 11f);
                Themes.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
                Themes.AddFormToManage(frm as MaterialForm);
            }
        }

        public void ChangeColorScheme()
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
        }

        public void SetDynamicSize<T>(T sender, Form frm)
            where T : Control
        {
            if (sender == null) throw new ArgumentNullException();

            frm.Width = sender.Width + _offSizeWidth;
        }
    }
}
