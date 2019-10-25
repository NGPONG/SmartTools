using MaterialSkin;
using MaterialSkin.Controls;
using SmartTools.Controller;
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
    public partial class Login : MaterialForm
    {
        MaterialSkinManager Themes = MaterialSkinManager.Instance;

        FormController controller = new FormController();
        public Login()
        {
            InitializeComponent();
            controller.Init(this);
        }

        private int _colorSchemeIndex;
        private void MaterialRaisedButton1_Click(object sender, EventArgs e)
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
    }
}
