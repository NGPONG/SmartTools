using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_09
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            IEnumerable<string> list = new List<string>() { "哈哈", "傻逼", "脑残" };
            var enumerator = list.GetEnumerator();
            while (true)
            {
                enumerator.MoveNext();
                var str = enumerator.Current;
                if (str == null)
                    enumerator.Reset();
            }


            InitializeComponent(); // you need to add a listView named listView1 with the designer
            listView1.FullRowSelect = true;
            ListViewExtender extender = new ListViewExtender(listView1);
            // extend 2nd column
            ListViewButtonColumn buttonAction = new ListViewButtonColumn(1);
            buttonAction.Click += OnButtonActionClick;
            buttonAction.FixedWidth = true;

            extender.AddColumn(buttonAction);

            for (int i = 0; i < 200; i++)
            {
                ListViewItem item = listView1.Items.Add("item" + i);
                item.SubItems.Add("button " + i);
            }
        }

        private void OnButtonActionClick(object sender, ListViewColumnMouseEventArgs e)
        {
            MessageBox.Show(this, @"you clicked " + e.SubItem.Text);
        }
    }
}
