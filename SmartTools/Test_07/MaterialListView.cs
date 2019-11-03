using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MaterialSkin.Controls
{
    public class MaterialListView : ListView, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        [Browsable(false)]
        public Point MouseLocation { get; set; }
        [Browsable(false)]
        private ListViewItem HoveredItem { get; set; }

        public MaterialListView()
        {
            //GridLines = false;
            //FullRowSelect = true;
            //HeaderStyle = ColumnHeaderStyle.Nonclickable;
            //View = View.Details;
            OwnerDraw = true;
            //ResizeRedraw = true;
            BorderStyle = BorderStyle.None;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);

            //Fix for hovers, by default it doesn't redraw
            //TODO: should only redraw when the hovered line changed, this to reduce unnecessary redraws
            MouseLocation = new Point(-1, -1);
            MouseState = MouseState.OUT;
            MouseEnter += delegate
            {
                MouseState = MouseState.HOVER;
            };
            MouseLeave += delegate
            {
                MouseState = MouseState.OUT;
                MouseLocation = new Point(-1, -1);
                HoveredItem = null;
                Invalidate();
            };
            MouseDown += delegate { MouseState = MouseState.DOWN; };
            MouseUp += delegate { MouseState = MouseState.HOVER; };
            MouseMove += delegate (object sender, MouseEventArgs args)
            {
                MouseLocation = args.Location;
                var currentHoveredItem = this.GetItemAt(MouseLocation.X, MouseLocation.Y);
                if (HoveredItem != currentHoveredItem)
                {
                    HoveredItem = currentHoveredItem;
                    Refresh();
                }
            };
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255, 255)), new Rectangle(e.Bounds.X, e.Bounds.Y, Width, e.Bounds.Height));
            e.Graphics.DrawString(e.Header.Text,
                new Font("微软雅黑", 9),
                new SolidBrush(Color.FromArgb(138, 0, 0, 0)),
                new Rectangle(e.Bounds.X + ITEM_PADDING, e.Bounds.Y + ITEM_PADDING, e.Bounds.Width - ITEM_PADDING * 2, e.Bounds.Height - ITEM_PADDING * 2),
                getStringFormat());
        }

        private const int ITEM_PADDING = 12;
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            //We draw the current line of items (= item with subitems) on a temp bitmap, then draw the bitmap at once. This is to reduce flickering.
            var b = new Bitmap(e.Item.Bounds.Width, e.Item.Bounds.Height);
            var g = Graphics.FromImage(b);

            //always draw default background
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255, 255)), new Rectangle(new Point(e.Bounds.X, 0), e.Bounds.Size));

            if (e.State.HasFlag(ListViewItemStates.Selected))
            {
                //selected background
                g.FillRectangle(new SolidBrush(Color.FromArgb((int)((40 / 100d) * 255d), Color.FromArgb(
                (0x999999C & 0xff0000) >> 16,
                (0x999999C & 0xff00) >> 8,
                 0x999999C & 0xff))), new Rectangle(new Point(e.Bounds.X, 0), e.Bounds.Size));
            }
            else if (e.Bounds.Contains(MouseLocation) && MouseState == MouseState.HOVER)
            {
                //hover background
                g.FillRectangle(new SolidBrush(Color.FromArgb((int)((20 / 100d) * 255d), Color.FromArgb(
                (0x999999 & 0xff0000) >> 16,
                (0x999999 & 0xff00) >> 8,
                 0x999999 & 0xff))), new Rectangle(new Point(e.Bounds.X, 0), e.Bounds.Size));
            }


            //Draw separator
            g.DrawLine(new Pen(Color.FromArgb(31, 0, 0, 0)), e.Bounds.Left, 0, e.Bounds.Right, 0);

            foreach (ListViewItem.ListViewSubItem subItem in e.Item.SubItems)
            {
                //Draw text
                g.DrawString(subItem.Text, new Font("微软雅黑", 9), new SolidBrush(Color.FromArgb(222, 0, 0, 0)),
                                 new Rectangle(subItem.Bounds.X + ITEM_PADDING, ITEM_PADDING, subItem.Bounds.Width - 2 * ITEM_PADDING, subItem.Bounds.Height - 2 * ITEM_PADDING),
                                 getStringFormat());
            }

            e.Graphics.DrawImage((Image)b.Clone(), new Point(0, e.Item.Bounds.Location.Y));
            g.Dispose();
            b.Dispose();
        }

        private StringFormat getStringFormat()
        {
            return new StringFormat
            {
                FormatFlags = StringFormatFlags.LineLimit,
                Trimming = StringTrimming.EllipsisCharacter,
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LogFont
        {
            public int lfHeight = 0;
            public int lfWidth = 0;
            public int lfEscapement = 0;
            public int lfOrientation = 0;
            public int lfWeight = 0;
            public byte lfItalic = 0;
            public byte lfUnderline = 0;
            public byte lfStrikeOut = 0;
            public byte lfCharSet = 0;
            public byte lfOutPrecision = 0;
            public byte lfClipPrecision = 0;
            public byte lfQuality = 0;
            public byte lfPitchAndFamily = 0;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string lfFaceName = string.Empty;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // This hack tries to apply the Roboto (24) font to all ListViewItems in this ListView
            // It only succeeds if the font is installed on the system.
            // Otherwise, a default sans serif font is used.
            var roboto24 = new Font("微软雅黑", 24);
            var roboto24Logfont = new LogFont();
            roboto24.ToLogFont(roboto24Logfont);

            try
            {
                // Font.FromLogFont is the method used when drawing ListViewItems. I 'test' it in this safer context to avoid unhandled exceptions later.
                Font = Font.FromLogFont(roboto24Logfont);
            }
            catch (ArgumentException)
            {
                Font = new Font(FontFamily.GenericSansSerif, 24);
            }
        }
        static string GetStackTraceModelName()
        {
            //当前堆栈信息
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame[] sfs = st.GetFrames();
            //过虑的方法名称,以下方法将不会出现在返回的方法调用列表中
            string _filterdName = "ResponseWrite,ResponseWriteError,";
            string _fullName = string.Empty, _methodName = string.Empty;
            for (int i = 1; i < sfs.Length; ++i)
            {
                //非用户代码,系统方法及后面的都是系统调用，不获取用户代码调用结束
                if (System.Diagnostics.StackFrame.OFFSET_UNKNOWN == sfs[i].GetILOffset()) break;
                _methodName = sfs[i].GetMethod().Name;//方法名称
                                                      //sfs[i].GetFileLineNumber();//没有PDB文件的情况下将始终返回0
                if (_filterdName.Contains(_methodName)) continue;
                _fullName = _methodName + "()->" + _fullName;
            }
            st = null;
            sfs = null;
            _filterdName = _methodName = null;
            return _fullName.TrimEnd('-', '>');
        }

    }
}
