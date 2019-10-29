using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTools.Utils
{
    public static class FormInitializationExtensions
    {
        public static void RePostion(this Form form)
        {
            form.Left = (Screen.PrimaryScreen.Bounds.Width - form.Width) / 2;
            form.Top = (Screen.PrimaryScreen.Bounds.Height - form.Height) / 2;
        }
    }
}
