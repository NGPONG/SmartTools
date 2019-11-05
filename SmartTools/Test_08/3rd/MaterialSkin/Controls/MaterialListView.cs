﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static MaterialSkin.Controls.MaterialForm;

namespace MaterialSkin.Controls
{
    public class MaterialListView : ListView, IMaterialControl
    {
        [Browsable(true)]
        public new bool HideSelection { get { return base.HideSelection; } set { base.HideSelection = true; } }

        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        [Browsable(false)]
        public Point MouseLocation { get; set; }
        [Browsable(false)]
        private ListViewItem HoveredItem { get; set; }

        #region Member
        private const int ITEM_PADDING = 12;
        private bool IsShowing = false;
        #endregion

        #region SubControls
        private TextBox textBox;
        #endregion

        public MaterialListView()
        {
            // Initialize textbox & drowdownlist
            InitializeComponent();

            HideSelection = true;
            GridLines = false;
            FullRowSelect = true;
            HeaderStyle = ColumnHeaderStyle.Nonclickable;
            View = View.Details;
            OwnerDraw = true;
            ResizeRedraw = true;
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
                    Invalidate();
                }
            };
            // Showning Component When Mouse DoubleClick Item
            MouseDoubleClick += delegate (object sender, MouseEventArgs e)
            {
                int rowIndex, columnIndex;
                RECT subItem_RECT = this.GetSubItemRECT(e.Location, out rowIndex, out columnIndex);

                this.textBox.Location = columnIndex == 0 ? new Point(0 + ITEM_PADDING - 2, subItem_RECT.top + ITEM_PADDING - 2) : new Point(subItem_RECT.left + ITEM_PADDING - 2, subItem_RECT.top + ITEM_PADDING - 2);
                this.textBox.Text = this.Items[rowIndex].SubItems[columnIndex].Text;
                this.textBox.Visible = true;
                this.IsShowing = true;
                this.textBox.Focus();
            };
        }

        private void InitializeComponent()
        {
            textBox = new TextBox();
            textBox.Font = new Font("微软雅黑", 9f);
            textBox.Visible = false;
            textBox.KeyDown += delegate (object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {


                    this.IsShowing = false;
                    ((TextBox)sender).Visible = false;
                }
            };
            this.Controls.Add(textBox);
        }

        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_KEYDOWN = 0x0100;
        protected override void WndProc(ref Message m)
        {
            if (IsShowing
                          && (m.Msg == WM_MOUSEWHEEL ||
                              m.Msg == WM_HSCROLL ||
                              m.Msg == WM_VSCROLL ||
                             (m.Msg == WM_KEYDOWN && (m.WParam == (IntPtr)40 || m.WParam == (IntPtr)35))))
            {
                // Tracking Scroll ball Active
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), new Rectangle(e.Bounds.X, e.Bounds.Y, Width, e.Bounds.Height));
            e.Graphics.DrawString(e.Header.Text,
                new Font("微软雅黑", 9f),
                SkinManager.GetPrimaryTextBrush(),
                new Rectangle(e.Bounds.X + ITEM_PADDING, e.Bounds.Y + ITEM_PADDING, e.Bounds.Width - ITEM_PADDING * 2, e.Bounds.Height - ITEM_PADDING * 2),
                getStringFormat());
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            //We draw the current line of items (= item with subitems) on a temp bitmap, then draw the bitmap at once. This is to reduce flickering.
            var b = new Bitmap(e.Item.Bounds.Width, e.Item.Bounds.Height);
            var g = Graphics.FromImage(b);

            //always draw default background
            g.FillRectangle(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), new Rectangle(new Point(e.Bounds.X, 0), e.Bounds.Size));

            if (e.State.HasFlag(ListViewItemStates.Selected))
            {
                //selected background
                g.FillRectangle(SkinManager.GetFlatButtonPressedBackgroundBrush(), new Rectangle(new Point(e.Bounds.X, 0), e.Bounds.Size));
            }
            else if (e.Bounds.Contains(MouseLocation) && MouseState == MouseState.HOVER)
            {
                //hover background
                g.FillRectangle(SkinManager.GetFlatButtonHoverBackgroundBrush(), new Rectangle(new Point(e.Bounds.X, 0), e.Bounds.Size));
            }


            //Draw separator
            g.DrawLine(new Pen(SkinManager.GetDividersColor()), e.Bounds.Left, 0, e.Bounds.Right, 0);

            foreach (ListViewItem.ListViewSubItem subItem in e.Item.SubItems)
            {
                //Draw text
                g.DrawString(subItem.Text, SkinManager.ROBOTO_MEDIUM_10, SkinManager.GetPrimaryTextBrush(),
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
            var roboto24 = new Font(SkinManager.ROBOTO_MEDIUM_12.FontFamily, 24);
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

        private RECT GetSubItemRECT(Point mouse_Point, out int rowIndex, out int columnIndex)
        {
            rowIndex = -1;
            columnIndex = -1;

            RECT subItem_RECT = new RECT();
            var activeItem = this.GetItemAt(mouse_Point.X, mouse_Point.Y);
            if (activeItem != null)
            {
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    // We need to pass the 1 based index of the subitem.
                    subItem_RECT.top = i + 1;
                    subItem_RECT.left = 0;

                    var success = SendMessage(this.Handle, (0x1000) + 56, activeItem.Index, ref subItem_RECT);
                    if (success > 0)
                    {
                        // 仅针对第一列的内容
                        if (mouse_Point.X < subItem_RECT.left)
                        {
                            rowIndex = activeItem.Index;
                            columnIndex = i;
                            break;
                        }
                        else if (mouse_Point.X > subItem_RECT.left && mouse_Point.X < subItem_RECT.right)
                        {
                            rowIndex = activeItem.Index;
                            columnIndex = i + 1;
                            break;
                        }
                    }
                }
            }

            return subItem_RECT;
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, int messageID, int wParam, ref RECT lParam);
    }
}