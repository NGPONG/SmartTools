using MaterialSkin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace MaterialSkin.Controls
{
    [Designer(typeof(MaterialProgressSpinnerDesigner))]
    [ToolboxBitmap(typeof(ProgressBar))]
    public partial class MaterialProgressSpinner : Control
    {
        #region Fields

        private Timer timer;
        private int progress;
        private float angle = 270;

        [DefaultValue(0)]
        [Category("Material Appearance")]
        public int Value
        {
            get { return progress; }
            set
            {
                if (value != -1 && (value < minimum || value > maximum))
                    throw new ArgumentOutOfRangeException("Progress value must be -1 or between Minimum and Maximum.", (Exception)null);
                progress = value;
                Refresh();
            }
        }

        private int minimum = 0;
        [DefaultValue(0)]
        [Category("Material Appearance")]
        public int Minimum
        {
            get { return minimum; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Minimum value must be >= 0.", (Exception)null);
                if (value >= maximum)
                    throw new ArgumentOutOfRangeException("Minimum value must be < Maximum.", (Exception)null);
                minimum = value;
                if (progress != -1 && progress < minimum)
                    progress = minimum;
                Refresh();
            }
        }

        private int maximum = 100;
        [DefaultValue(0)]
        [Category("Material Appearance")]
        public int Maximum
        {
            get { return maximum; }
            set
            {
                if (value <= minimum)
                    throw new ArgumentOutOfRangeException("Maximum value must be > Minimum.", (Exception)null);
                maximum = value;
                if (progress > maximum)
                    progress = maximum;
                Refresh();
            }
        }

        private bool ensureVisible = true;
        [DefaultValue(true)]
        [Category("Material Appearance")]
        public bool EnsureVisible
        {
            get { return ensureVisible; }
            set { ensureVisible = value; Refresh(); }
        }

        private float speed;
        [DefaultValue(1f)]
        [Category("Material Behaviour")]
        public float Speed
        {
            get { return speed; }
            set
            {
                if (value <= 0 || value > 10)
                    throw new ArgumentOutOfRangeException("Speed value must be > 0 and <= 10.", (Exception)null);

                speed = value;
            }
        }

        private bool backwards;
        [DefaultValue(false)]
        [Category("Material Behaviour")]
        public bool Backwards
        {
            get { return backwards; }
            set { backwards = value; Refresh(); }
        }

        private bool useCustomBackground = false;
        [Category("Material Appearance")]
        public bool CustomBackground
        {
            get { return useCustomBackground; }
            set { useCustomBackground = value; }
        }

        #endregion

        #region Constructor

        public MaterialProgressSpinner()
        {
            timer = new Timer();
            timer.Interval = 20;
            timer.Tick += delegate (object sender, EventArgs e)
            {
                if (!DesignMode)
                {
                    angle += 6f * speed * (backwards ? -1 : 1);
                    Refresh();
                }
            };

            this.speed = 1;
            base.Width = 16;
            base.Height = 16;
            base.DoubleBuffered = true;

            if (!DesignMode)
                this.Visible = false;
        }

        #endregion

        #region Public Methods

        public void Reset()
        {
            progress = 50;
            angle = 270;
            Refresh();
        }

        public void Start()
        {
            timer.Start();
            this.Visible = true;
        }

        public void Stop()
        {
            timer.Stop();
            angle = 270;
            this.Visible = false;
        }

        #endregion

        #region Paint Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            Color backColor = MaterialSkinManager.Instance.GetApplicationBackgroundColor();
            Color foreColor = MaterialSkinManager.Instance.ColorScheme.PrimaryColor;

            e.Graphics.Clear(backColor);

            using (Pen forePen = new Pen(foreColor, (float)Width / 5))
            {
                int padding = (int)Math.Ceiling((float)Width / 10);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                if (progress != -1)
                {
                    float sweepAngle;
                    float progFrac = (float)(progress - minimum) / (float)(maximum - minimum);

                    if (ensureVisible)
                    {
                        sweepAngle = 30 + 300f * progFrac;
                    }
                    else
                    {
                        sweepAngle = 360f * progFrac;
                    }

                    if (backwards)
                    {
                        sweepAngle = -sweepAngle;
                    }

                    e.Graphics.DrawArc(forePen, padding, padding, Width - 2 * padding - 1, Height - 2 * padding - 1, angle, sweepAngle);
                }
                else
                {
                    const int maxOffset = 180;
                    for (int offset = 0; offset <= maxOffset; offset += 15)
                    {
                        int alpha = 290 - (offset * 290 / maxOffset);

                        if (alpha > 255)
                        {
                            alpha = 255;
                        }
                        if (alpha < 0)
                        {
                            alpha = 0;
                        }

                        Color col = Color.FromArgb(alpha, forePen.Color);
                        using (Pen gradPen = new Pen(col, forePen.Width))
                        {
                            float startAngle = angle + (offset - (ensureVisible ? 30 : 0)) * (backwards ? 1 : -1);
                            float sweepAngle = 15 * (backwards ? 1 : -1);
                            e.Graphics.DrawArc(gradPen, padding, padding, Width - 2 * padding - 1, Height - 2 * padding - 1, startAngle, sweepAngle);
                        }
                    }
                }
            }
        }
        #endregion
    }

    internal class MaterialProgressSpinnerDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules
        {
            get
            {
                return base.SelectionRules;
            }
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            properties.Remove("ImeMode");
            properties.Remove("Padding");
            properties.Remove("FlatAppearance");
            properties.Remove("FlatStyle");
            properties.Remove("AutoEllipsis");
            properties.Remove("UseCompatibleTextRendering");

            properties.Remove("Image");
            properties.Remove("ImageAlign");
            properties.Remove("ImageIndex");
            properties.Remove("ImageKey");
            properties.Remove("ImageList");
            properties.Remove("TextImageRelation");

            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("UseVisualStyleBackColor");

            properties.Remove("Font");
            properties.Remove("ForeColor");
            properties.Remove("RightToLeft");
            properties.Remove("Text");

            base.PreFilterProperties(properties);
        }
    }
}
