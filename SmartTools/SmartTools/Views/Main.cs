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
        public Main(FormClosedEventHandler handler)
        {
            InitializeComponent();
            this.FormClosed += handler;
        }
    }
}
