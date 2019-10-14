using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace Test_04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = $"x:{e.X.ToString()} , y:{e.Y.ToString()}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Image img = new Bitmap("Resources\\开局.png");
            this.pictureBox1.Image = img;
            this.pictureBox1.Width = img.Width;
            this.pictureBox1.Height = img.Height;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Image img = new Bitmap("Resources\\开局.png");
            this.pictureBox1.Image = img;
            this.pictureBox1.Width = img.Width;
            this.pictureBox1.Height = img.Height;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Bitmap bmpOut = new Bitmap(371, 110, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bmpOut);

            Bitmap bmpIn = pictureBox1.Image as Bitmap;
            g.DrawImage(bmpIn, new Rectangle(0, 0, 371, 110), new Rectangle(609, 362, 371, 110), GraphicsUnit.Pixel);

            this.pictureBox1.Image = bmpOut;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = $"x:{e.X.ToString()} , y:{e.Y.ToString()}";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var engine = new TesseractEngine("./tessdata", "chi_sim", EngineMode.TesseractOnly);
            var page = engine.Process(this.pictureBox1.Image as Bitmap,PageSegMode.SingleLine);
            string str = page.GetText();
        }
    }
}
