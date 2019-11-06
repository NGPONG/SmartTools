using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_08
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.materialListView1.AddCustomItem(0, new TextBox())
                                  .AddCustomItem(1, new ComboBox() { DataSource = new List<string>() { "392", "518", "237", "375", "408" } })
                                  .AddCustomItem(2, new TextBox())
                                  .AddCustomItem(3, new TextBox())
                                  .InitializeCustomControl();

            var data = new[]
            {
                new []{"Lollipop", "392", "0.2", "0"},
                new []{"KitKat", "518", "26.0", "7"},
                new []{"Ice cream sandwich", "237", "9.0", "4.3"},
                new []{"Jelly Bean", "375", "0.0", "0.0"},
                new []{"Honeycomb", "408", "3.2", "6.5"}
            };

            //Add
            foreach (string[] version in data)
            {
                var item = new ListViewItem(version);
                materialListView1.Items.Add(item);
            }
        }

        private void ComboBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
