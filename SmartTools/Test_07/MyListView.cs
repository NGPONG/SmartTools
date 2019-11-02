using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_07
{
    public class MyListView : ListView
    {
        public MyListView()
        {
            GridLines = false;
            FullRowSelect = true;
            HeaderStyle = ColumnHeaderStyle.Nonclickable;
            View = View.Details;
            OwnerDraw = true;
            ResizeRedraw = true;
            BorderStyle = BorderStyle.None;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer , true);
            // SetStyle(ControlStyles.UserPaint, true);

            this.MouseDoubleClick += this.MyListView_MouseDoubleClick;
        }

        private void MyListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Invalidate();
        }
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            //e.DrawDefault = true;
            base.OnDrawItem(e);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }
    }
}
