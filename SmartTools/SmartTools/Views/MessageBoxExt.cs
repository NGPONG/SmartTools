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
    public partial class MessageBoxExt : MaterialForm
    {
        FormController control = new FormController();
        public MessageBoxExt()
        {
            InitializeComponent();
            control.Init(this);
        }
    }
}
