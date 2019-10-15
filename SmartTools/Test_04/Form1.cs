using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            Image img = new Bitmap("Resources\\停止.png");
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
            Bitmap bmpOut = new Bitmap(195, 29, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bmpOut);

            Bitmap bmpIn = pictureBox1.Image as Bitmap;
            g.DrawImage(bmpIn, new Rectangle(0, 0, 195, 29), new Rectangle(699, 435, 195, 29), GraphicsUnit.Pixel);


            var grayImage = new Image<Gray, Byte>(bmpOut);
            var threshImge = grayImage.CopyBlank();
            CvInvoke.Threshold(grayImage, threshImge, 0, 255, ThresholdType.Otsu);
            this.pictureBox1.Image = threshImge.ToBitmap();

            //this.pictureBox1.Image = bmpOut;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = $"x:{e.X.ToString()} , y:{e.Y.ToString()}";
        }

        /// <summary>
        /// 調整圖片大小和對比度
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution * 2);//2,3
                                                                                              //image.Save(@"D:\UpWork\OCR_WinForm\Preprocess_HighRes.jpg");

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceOver;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.Clamp);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private Bitmap PreprocesImage(Bitmap image)
        {
            //You can change your new color here. Red,Green,LawnGreen any..
            Color actualColor;
            //make an empty bitmap the same size as scrBitmap
            image = ResizeImage(image, image.Width * 5, image.Height * 5);
            //image.Save(@"D:\UpWork\OCR_WinForm\Preprocess_Resize.jpg");

            Bitmap newBitmap = new Bitmap(image.Width, image.Height);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    //get the pixel from the scrBitmap image
                    actualColor = image.GetPixel(i, j);
                    // > 150 because.. Images edges can be of low pixel colr. if we set all pixel color to new then there will be no smoothness left.
                    if (actualColor.R > 23 || actualColor.G > 23 || actualColor.B > 23)//在這裡設定RGB
                        newBitmap.SetPixel(i, j, Color.White);
                    else
                        newBitmap.SetPixel(i, j, Color.Black);
                }
            }
            return newBitmap;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var engine = new TesseractEngine("./tessdata", "chi_sim", EngineMode.Default);
            //bit.Save($"{AppDomain.CurrentDomain.BaseDirectory}\\1.png", System.Drawing.Imaging.ImageFormat.Png);
            using (var page = engine.Process(this.pictureBox1.Image as Bitmap, PageSegMode.SingleBlock))
            {
                string read = page.GetText();
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            using (var engine = new TesseractEngine("./tessdata", "chi_sim", EngineMode.Default))
            {
                Bitmap bitmap_Out = new Bitmap($"{AppDomain.CurrentDomain.BaseDirectory}\\Resources\\Image_测试_测试.png");
                var grayImage = new Image<Gray, Byte>(bitmap_Out);
                var threshImge = grayImage.CopyBlank();
                CvInvoke.Threshold(grayImage, threshImge, 0, 255, ThresholdType.Otsu);
                using (var page = engine.Process(threshImge.ToBitmap(),PageSegMode.SingleBlock))
                {
                    string strReadPic = strReadPic = page.GetText();
                }

                this.pictureBox1.Image = threshImge.ToBitmap();
            }
        }
    }
}
