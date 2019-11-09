using SmartTools.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;
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
        public ListViewItem HoveredItem { get; set; }

        #region Member
        private const int ITEM_PADDING = 12;
        private const int CONTROL_PADDING = ITEM_PADDING + 1;

        private bool IsShowing = false;

        private Dictionary<int, Control> _dynamicControls = new Dictionary<int, Control>();
        private int _dynamicRowIndex = -1;
        private int _dynamicColumnIndex = -1;

        private Dictionary<SubItem, Control> _embeddedControls = new Dictionary<SubItem, Control>();
        #endregion

        public class SubItem
        {
            public int Column { get; set; }
            public int Row { get; set; }
        }

        public MaterialListView()
        {
            // Initialize textbox & drowdownlist
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
            MouseDown += delegate { MouseState = MouseState.DOWN; };
            MouseUp += delegate { MouseState = MouseState.HOVER; };
            #region Deprecated
            //MouseEnter += delegate
            //{
            //    MouseState = MouseState.HOVER;
            //};
            //MouseLeave += delegate
            //{
            //    MouseState = MouseState.OUT;
            //    MouseLocation = new Point(-1, -1);
            //    HoveredItem = null;
            //    Invalidate();
            //}; 
            #endregion
            MouseMove += delegate (object sender, MouseEventArgs args)
            {
                var currentHoveredItem = this.GetItemAt(args.Location.X, args.Location.Y);
                if (HoveredItem != currentHoveredItem)
                {
                    HoveredItem = currentHoveredItem;
                }
            };

            // Showning Component When Mouse DoubleClick Item
            MouseDoubleClick += delegate (object sender, MouseEventArgs e)
            {
                RECT subItem_RECT = this.GetSubItemRECT(e.Location);

                try
                {
                    if (_dynamicColumnIndex == 0)
                        throw new Exception("default");

                    ShowEditControl(_dynamicControls[_dynamicColumnIndex], subItem_RECT);
                }
                catch
                {
                    _dynamicRowIndex = -1;
                    _dynamicColumnIndex = -1;
                }
            };
        }

        public MaterialListView InitializeCustomControl()
        {
            foreach (KeyValuePair<int, Control> item in this._dynamicControls)
            {
                var control = item.Value;
                control.Font = new Font("微软雅黑", 9f);
                control.Hide();
                if (control is TextBox)
                {
                    TextBox textBox = control as TextBox;
                    textBox.KeyDown += delegate (object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            // Save
                            this.Items[_dynamicRowIndex].SubItems[_dynamicColumnIndex].Text = textBox.Text;
                            HideEditControl(textBox);
                        }
                        else if (e.KeyCode == Keys.Escape)
                        {
                            HideEditControl(textBox);
                        }
                    };
                    textBox.Leave += delegate (object sender, EventArgs e)
                    {
                        HideEditControl(textBox);
                    };
                    this.Controls.Add(textBox);
                }
                else if (control is ComboBox)
                {
                    ComboBox combo = control as ComboBox;
                    combo.DropDownStyle = ComboBoxStyle.DropDownList;
                    combo.SelectedIndexChanged += delegate (object sender, EventArgs e)
                    {
                        if (combo.SelectedIndex > -1)
                            this.Items[_dynamicRowIndex].SubItems[_dynamicColumnIndex].Text = combo.Text; // Save
                    };
                    combo.KeyDown += delegate (object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Escape)
                        {
                            HideEditControl(combo);
                        }
                    };
                    combo.Leave += delegate (object sender, EventArgs e)
                    {
                        HideEditControl(combo);
                    };
                    this.Controls.Add(combo);
                }
            }

            return this;
        }

        protected override void WndProc(ref Message m)
        {
            if (IsShowing
                          && (m.Msg == Native.WM_MOUSEWHEEL ||
                              m.Msg == Native.WM_HSCROLL ||
                              m.Msg == Native.WM_VSCROLL ||
                             (m.Msg == Native.WM_KEYDOWN && (m.WParam == (IntPtr)40 || m.WParam == (IntPtr)35))))
            {
                // Tracking Scroll ball Active
            }
            else
            {
                // Change control postion when windows painting
                if (m.Msg == Native.WM_PAINT)
                {
                    if (View == View.Details)
                    {
                        foreach (KeyValuePair<SubItem, Control> item in _embeddedControls)
                        {
                            Rectangle rc = Rectangle.Empty;
                            try
                            {
                                rc = this.GetSubItemBounds(this.Items[item.Key.Row], item.Key.Column);
                            }
                            catch
                            {
                                base.WndProc(ref m);
                                return;
                            }

                            if ((this.HeaderStyle != ColumnHeaderStyle.None) &&
                                (rc.Top < this.Font.Height)) // Control overlaps ColumnHeader
                            {
                                item.Value.Visible = false;
                                continue;
                            }
                            else
                            {
                                item.Value.Visible = true;
                            }

                            item.Value.Bounds = rc;
                        }
                    }
                }
                base.WndProc(ref m);
            }
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), new Rectangle(e.Bounds.X, e.Bounds.Y, Width, e.Bounds.Height));
            e.Graphics.DrawString(e.Header.Text,
                new Font("微软雅黑", 9f),
                SkinManager.GetPrimaryTextBrush(),
                new Rectangle(e.Header.Text == "操作"? e.Bounds.X + ITEM_PADDING + 25 : e.Bounds.X + ITEM_PADDING, e.Bounds.Y + ITEM_PADDING, e.Bounds.Width - ITEM_PADDING * 2, e.Bounds.Height - ITEM_PADDING * 2),
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
                //Draw default text
                g.DrawString(subItem.Text,
                             SkinManager.ROBOTO_MEDIUM_10,
                             SkinManager.GetPrimaryTextBrush(),
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

        private RECT GetSubItemRECT(Point mouse_Point)
        {
            RECT subItem_RECT = new RECT();
            var activeItem = this.GetItemAt(mouse_Point.X, mouse_Point.Y);

            if (activeItem != null)
            {
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    // We need to pass the 1 based index of the subitem.
                    subItem_RECT.top = i + 1;
                    subItem_RECT.left = 0;

                    var success = Win32.SendMessage(this.Handle, (0x1000) + 56, activeItem.Index, ref subItem_RECT);
                    if (success > 0)
                    {
                        // 仅针对第一列的内容
                        if (mouse_Point.X < subItem_RECT.left)
                        {
                            _dynamicRowIndex = activeItem.Index;
                            _dynamicColumnIndex = i;
                            break;
                        }
                        else if (mouse_Point.X > subItem_RECT.left && mouse_Point.X < subItem_RECT.right)
                        {
                            _dynamicRowIndex = activeItem.Index;
                            _dynamicColumnIndex = i + 1;
                            break;
                        }
                    }
                }
            }

            return subItem_RECT;
        }

        private void ShowEditControl(Control control, RECT rect)
        {
            if (this._dynamicRowIndex != -1 && this._dynamicColumnIndex != -1)
            {
                control.Location = _dynamicColumnIndex == 0 ? new Point(0 + CONTROL_PADDING, rect.top + CONTROL_PADDING) : new Point(rect.left + CONTROL_PADDING, rect.top + CONTROL_PADDING);
                control.Text = this.Items[_dynamicRowIndex].SubItems[_dynamicColumnIndex].Text;
                control.Show();
                ChangeEditControlWidth(control);
                // lock scroll ball first time
                this.IsShowing = true;
                Win32.SetFocus(control.Handle);
            }
        }

        private void ChangeEditControlWidth(Control control)
        {
            if (control is TextBox)
            {
                control.Width = TextRenderer.MeasureText(control.Text,
                                                control.Font,
                                                Size.Empty,
                                                TextFormatFlags.TextBoxControl).Width + 8;
            }
            else if (control is ComboBox)
            {
                ComboBox combo = control as ComboBox;
                // Get the max length in combobox datasouce item
                var data = combo.DataSource as List<string>;
                string maxValue = string.Empty;
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        int result = Math.Max(item.Length, maxValue.Length);
                        if (result << 2 == item.Length << 2)
                        {
                            maxValue = item;
                        }
                    }
                }
                control.Width = TextRenderer.MeasureText(data == null ? control.Text : maxValue,
                                                         control.Font,
                                                         Size.Empty,
                                                         TextFormatFlags.TextBoxControl).Width + 25;
            }
        }
        private void HideEditControl(Control control)
        {
            this.IsShowing = false;
            control.Text = string.Empty;
            control.Hide();
            _dynamicRowIndex = -1;
            _dynamicColumnIndex = -1;
        }

        public MaterialListView AddEmbeddedButton(EventHandler handler, string columnName = "chAction_Default")
        {
            // Add Column header.
            ColumnHeader header = new ColumnHeader();
            header.Name = columnName;
            header.Text = "操作";
            header.Width = 105;
            header.TextAlign = HorizontalAlignment.Center;
            this.Columns.Add(header);

            for (int i = 0; i < this.Items.Count; i++)
            {
                MaterialFlatButton button = new MaterialFlatButton();
                button.CustomFont = new Font("微软雅黑", 9f);
                button.Text = "点击删除";
                button.Primary = true;
                button.Click += handler;
                button.Tag = i;

                SubItem subItem = new SubItem() { Column = header.Index, Row = i };
                _embeddedControls[subItem] = button;

                this.Controls.Add(button);
            }

            return this;
        }

        /// <summary>
        /// Retrieve the order in which columns dispaly
        /// </summary>
        /// <returns></returns>
        private int[] GetColumnOrder()
        {
            IntPtr lPar = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * Columns.Count);

            IntPtr res = Win32.SendMessage(Handle, Native.LVM_GETCOLUMNORDERARRAY, new IntPtr(Columns.Count), lPar);
            if (res.ToInt32() == 0)
            {
                // Something went wrong
                Marshal.FreeHGlobal(lPar);
                return null;
            }

            int[] order = new int[Columns.Count];
            Marshal.Copy(lPar, order, 0, Columns.Count);

            Marshal.FreeHGlobal(lPar);

            return order;
        }

        /// <summary>
        /// Retrieve the bounds of a ListViewSubItem
        /// </summary>
        /// <param name="Item">The Item containing the SubItem</param>
        /// <param name="SubItem">Index of the SubItem</param>
        /// <returns>Subitem's bounds</returns>
        protected Rectangle GetSubItemBounds(ListViewItem Item, int SubItem)
        {
            Rectangle subItemRect = Rectangle.Empty;

            int[] order = GetColumnOrder();
            if (order == null) // No Columns
                return subItemRect;

            // Retrieve the bounds of the entire ListViewItem (all subitems)
            Rectangle lviBounds = Item.GetBounds(ItemBoundsPortion.Entire);
            int subItemX = lviBounds.Left;

            // Calculate the X position of the SubItem.
            // Because the columns can be reordered we have to use Columns[order[i]] instead of Columns[i] !
            int i;
            for (i = 0; i < order.Length; i++)
            {
                ColumnHeader col = this.Columns[order[i]];
                if (col.Index == SubItem)
                    break;
                subItemX += col.Width;
            }

            subItemRect = new Rectangle(subItemX + (CONTROL_PADDING - 5), lviBounds.Top + (CONTROL_PADDING - 4), this.Columns[order[i]].Width, lviBounds.Height);

            return subItemRect;
        }

        public void RemoveActiveItem(MaterialFlatButton button)
        {
            int activeButtonIndex = Convert.ToInt32(button.Tag);

            var subItems = _embeddedControls.Select(kvp => kvp.Key).ToList();
            var buttons = _embeddedControls.Select(kvp => kvp.Value).ToList();
            var resetSubItem = subItems.Where(s => s.Row == activeButtonIndex).FirstOrDefault();

            this._embeddedControls.Remove(resetSubItem);

            for (int i = 0; i < subItems.Count; i++)
            {
                if (subItems[i].Row == activeButtonIndex)
                    this.Controls.Remove(buttons[i]);
                if (subItems[i].Row > activeButtonIndex)
                {
                    subItems[i].Row--;
                    buttons[i].Tag = Convert.ToInt32(buttons[i].Tag) - 1;
                }
            }

            this.Items.Remove(this.Items[activeButtonIndex]);
        }

        public MaterialListView AddEditControl(int columnIndex, Control control)
        {
            this._dynamicControls[columnIndex] = control;
            return this;
        }
    }
}
