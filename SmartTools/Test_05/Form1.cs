using OpenCvSharp;
using OpenCvSharp.Cuda;
using OpenCvSharp.Extensions;
using OpenCvSharp.ML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            // DoOCR($"{AppDomain.CurrentDomain.BaseDirectory}\\开局.png");

            Bitmap bitmap = new Bitmap($"{AppDomain.CurrentDomain.BaseDirectory}\\开局.png");
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);

                var source = Mat.FromStream(stream,ImreadModes.AnyColor);
                Cv2.ImShow("source", source);

                var gray = new Mat();
                Cv2.CvtColor(source, gray, ColorConversionCodes.BGRA2GRAY);
                Cv2.ImShow("gray", gray);

                var threshImage = new Mat();
                Cv2.Threshold(gray, threshImage, Thresh, ThresholdMaxVal, ThresholdTypes.BinaryInv); // Threshold to find contour
                Cv2.ImShow("thresh", threshImage);
                
            }
        }

        private const double Thresh = 80;
        private const double ThresholdMaxVal = 255;
        public void DoOCR(string path)
        {
            var src = Cv2.ImRead(path);
            GpuMat gpuMat = new GpuMat(src);

            var gray = new Mat();
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGRA2GRAY);
            Cv2.ImShow("123", gray);

            var threshImage = new Mat();
            Cv2.Threshold(gray, threshImage, Thresh, ThresholdMaxVal, ThresholdTypes.BinaryInv); // Threshold to find contour
            Cv2.ImShow("Binary", threshImage);
             
            Cv2.WaitKey();
        }
    }
}
