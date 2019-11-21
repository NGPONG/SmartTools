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
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}
