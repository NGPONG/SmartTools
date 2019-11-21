using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Common.Helper
{
    public class PictureHelper
    {
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution * 2);

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

        public static Bitmap ProcessImage(Rectangle rec_Cut, byte[] picBuffer)
        {
            var source = Mat.FromImageData(picBuffer, ImreadModes.AnyColor);
            var roi = new Rect(rec_Cut.X, rec_Cut.Y, rec_Cut.Width, rec_Cut.Height);

            var textRegion = new Mat(source, roi);

            var gray = new Mat();
            Cv2.CvtColor(textRegion, gray, ColorConversionCodes.BGRA2GRAY);

            var threshImage = new Mat();
            Cv2.Threshold(gray, threshImage, 80, 255, ThresholdTypes.BinaryInv);

            return BitmapConverter.ToBitmap(threshImage);
        }
    }
}
