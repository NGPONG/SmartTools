using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTools.Controller
{
    public class FormController
    {
        public static MaterialSkinManager Themes;

        public void Init(Form frm)
        {
            if (Themes == null)
            {
                Themes = MaterialSkinManager.Instance;
                Themes.ROBOTO_MEDIUM_12 = new Font("微软雅黑", 12f);
                Themes.ROBOTO_MEDIUM_10 = new Font("微软雅黑", 10f);
                Themes.ROBOTO_REGULAR_11 = new Font("微软雅黑", 11f);
                Themes.ROBOTO_MEDIUM_11 = new Font("微软雅黑", 11f);
                Themes.AddFormToManage(frm as MaterialForm);
            }
        }



        public void ChangeColorScheme()
        {

        }
    }
}
