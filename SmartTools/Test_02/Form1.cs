using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace Test_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        IWebDriver driver;
        private void Button1_Click(object sender, EventArgs e)
        {
            var options = new ChromeOptions();
            options.AddExcludedArguments(new string[] { "enable-automation" });
            driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory + "\\Resources", options);
            driver.Url = "http://gci.epda866.com:81/agingame/pcv1/index.jsp?";
            driver.Manage().Window.Position = new System.Drawing.Point(-7, 0);

            // 根据当前屏幕尺寸 分辨率 ddi 点距 计算出窗口的具体尺寸
            driver.Manage().Window.Size = new System.Drawing.Size(800, 600);
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 573, 543).Click().Build().Perform(); // 573,543
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                driver.SwitchTo().Window(driver.WindowHandles.First()).Close();

                driver.SwitchTo().Window(driver.WindowHandles.First());
                driver.Manage().Window.Position = new System.Drawing.Point(0, 0);
                driver.Manage().Window.Size = new System.Drawing.Size(1200, 1000);
            }
            catch (Exception)
            {
                this.Button3_Click(this.button3, new EventArgs());
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 985, 512).Click().Build().Perform(); // 1139,663

            // 907, 534 
            // 967, 534  maxleft:985 minleft：937  maxtop:547 mintop：512
            // 997, 534 


            //Thread.Sleep(1000);
            //actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 783, 555).Click().Build().Perform();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 718, 432).Click().Build().Perform(); // 1139,663

            // 和：718, 432
            // 庄：718, 452
            // 闲：718, 472
        }


        private void Button5_Click(object sender, EventArgs e)
        {
            // 闲：509, 320
            // 和：509, 300
            // 庄：509, 280

            // 1：602, 360
            // 2：632, 360
            // 3：662, 360
            // 4：692, 360
            // 5：732, 360


            Actions actions = new Actions(driver);
            //var picBuffer = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            //var source = Mat.FromImageData(picBuffer, ImreadModes.AnyColor);
            //var roi = new OpenCvSharp.Rect(509, 280, 200, 100);

            //var text = new Mat(source, roi);
            //Cv2.ImShow("text", text);
            //913, 534
            try
            {
                actions.MoveToElement(driver.FindElement(By.TagName("canvas")), Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text)).Click().Perform(); // 1139,663
            }
            catch (Exception)
            {

                throw;
            }

            //new Point(509, 320)
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 961, 534).Click().Build().Perform(); // 1139,663
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 1009, 534).Click().Build().Perform(); // 1139,663
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 1057, 534).Click().Build().Perform(); // 1139,663
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 1105, 534).Click().Build().Perform(); // 1139,663
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 718, 432).Click().Build().Perform();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 718, 452);
            actions.Click().Build().Perform();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 718, 472).Click().Build().Perform();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            //byte[] bytes = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            //MemoryStream stream = new MemoryStream(bytes);
            //Bitmap bit = new Bitmap(stream);

            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("Test.png", ScreenshotImageFormat.Png);
            MessageBox.Show("保存成功!");
        }
        int pageindex = 0;
        private void Button15_Click(object sender, EventArgs e)
        {
            using (var engine = new TesseractEngine("./tessdata", "chi_sim", EngineMode.Default))
            {
                Stopwatch sw = new Stopwatch();
                while (true)
                {
                    sw.Restart();
                    sw.Start();
                    var picBuffer = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;

                    string strReadPic;
                    using (MemoryStream stream = new MemoryStream(picBuffer))
                    {
                        Bitmap bitmap_In = new Bitmap(stream);
                        Bitmap bitmap_Out = new Bitmap(371, 110, PixelFormat.Format24bppRgb);

                        Graphics g = Graphics.FromImage(bitmap_Out);
                        g.DrawImage(bitmap_In, new Rectangle(0, 0, 371, 110), new Rectangle(609, 392, 371, 100), GraphicsUnit.Pixel);

                        if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\Image"))
                        {
                            Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}\\Image");
                        }

                        bitmap_In.Save($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\Image_{pageindex.ToString()}.png", System.Drawing.Imaging.ImageFormat.Png);
                        bitmap_Out.Save($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\Image_{pageindex.ToString()}_{pageindex.ToString()}.png", System.Drawing.Imaging.ImageFormat.Png);

                        using (var page = engine.Process(bitmap_Out, PageSegMode.SingleBlock))
                        {
                            strReadPic = page.GetText();
                            if (Regex.IsMatch(strReadPic, "开局"))
                            {
                                using (FileStream writer = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}\\log.txt", FileMode.Append, FileAccess.Write))
                                {
                                    byte[] bytes = Encoding.Default.GetBytes($"time：{sw.Elapsed.TotalSeconds.ToString()} content：{strReadPic} pageIndex：{pageindex} 吴鹏 \r\n");
                                    writer.Write(bytes, 0, bytes.Length);
                                }
                                break;
                            }
                        }
                    }
                    sw.Stop();
                    using (FileStream writer = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}\\log.txt", FileMode.Append, FileAccess.Write))
                    {
                        byte[] bytes = Encoding.Default.GetBytes($"time：{sw.Elapsed.TotalSeconds.ToString()} content：{strReadPic} pageIndex：{pageindex} \r\n");
                        writer.Write(bytes, 0, bytes.Length);
                    }

                    pageindex++;
                }

                Console.WriteLine($"start pageIndex{pageindex.ToString()}");

                Thread.Sleep(5000);

                Actions action1 = new Actions(driver);
                action1.MoveToElement(driver.FindElement(By.TagName("canvas")), 718, 432);
                for (int i = 0; i < 2; i++)
                {
                    action1.Click();
                }
                action1.Perform();

                Actions action2 = new Actions(driver);
                action2.MoveToElement(driver.FindElement(By.TagName("canvas")), 718, 452);
                for (int i = 0; i < 2; i++)
                {
                    action2.Click();
                }
                action2.Perform();

                Actions action3 = new Actions(driver);
                action3.MoveToElement(driver.FindElement(By.TagName("canvas")), 718, 472);
                for (int i = 0; i < 2; i++)
                {
                    action3.Click();
                }
                action3.Perform();
            }
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            using (var engine = new TesseractEngine("./tessdata", "chi_sim", EngineMode.Default))
            {
                Stopwatch sw = new Stopwatch();
                sw.Restart();
                sw.Start();
                string strReadPic;

                Bitmap bitmap_Out = new Bitmap($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\Image_测试_测试.png");

                using (var page = engine.Process(bitmap_Out, PageSegMode.SingleBlock))
                {
                    strReadPic = page.GetText();
                    if (Regex.IsMatch(strReadPic, "开局"))
                    {
                        using (FileStream writer = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}\\log.txt", FileMode.Append, FileAccess.Write))
                        {
                            byte[] bytes = Encoding.Default.GetBytes($"time：{sw.Elapsed.TotalSeconds.ToString()} content：{strReadPic} pageIndex：测试 吴鹏 \r\n");
                            writer.Write(bytes, 0, bytes.Length);
                        }
                    }
                }
                sw.Stop();
                using (FileStream writer = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}\\log.txt", FileMode.Append, FileAccess.Write))
                {
                    byte[] bytes = Encoding.Default.GetBytes($"time：{sw.Elapsed.TotalSeconds.ToString()} content：{strReadPic} pageIndex：测试 \r\n");
                    writer.Write(bytes, 0, bytes.Length);
                }
            }
        }
        TesseractEngine engine2 = new TesseractEngine("./tessdata", "chi_sim", EngineMode.Default);
        private void Button17_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var picBuffer = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            // 940,1482
            // 1088,1483
            // 869,1184
            // 宽：1482 / 2560
            // 高：1240 / 1440
            var source = Mat.FromImageData(picBuffer, ImreadModes.AnyColor);
            Cv2.ImShow("source", source);
            //Bitmap map = BitmapConverter.ToBitmap(source);
            //map.Save($"{AppDomain.CurrentDomain.BaseDirectory}\\Image\\1.png", System.Drawing.Imaging.ImageFormat.Png);
            //Cv2.ImShow("source2", source);
            //Cv2.Resize(source, source, new OpenCvSharp.Size(1482, 940),0,0,InterpolationFlags.Linear);
            //Cv2.ImShow("source", source);
            var roi = new OpenCvSharp.Rect(465, 225, 128, 18); // 699, 435, 195, 29
            // 465, 223, 128, 18 屏占比？
            
            var text = new Mat(source, roi);

            Cv2.ImShow("text", text);

            var gray = new Mat();
            Cv2.CvtColor(text, gray, ColorConversionCodes.BGRA2GRAY);

            var threshImage = new Mat();
            Cv2.Threshold(gray, threshImage, 80, 255, ThresholdTypes.BinaryInv);
            Cv2.ImShow("Threshold", threshImage);

            using (var page = engine2.Process(BitmapConverter.ToBitmap(threshImage), PageSegMode.SingleBlock))
            {
                string str = page.GetText();
                //MessageBox.Show($"content:{str} \r\n time:{stopwatch.Elapsed.TotalSeconds.ToString()}");
            }
        }

        private void calc_dpi(double x, double y, double diag)
        {
            if (y == 0 || x == 0)
                return;

            double ratio = y / x;
            double xd = Math.Sqrt(Math.Pow(diag, 2) / (1 + Math.Pow(ratio, 2)));
            double dotPitch = 25.4 / (x / xd);

            double dopi = Math.Round(dotPitch * 10000) / 10000;
            double metricDiag = diag * 2.54;
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            int SH = Screen.PrimaryScreen.Bounds.Height;
            int SW = Screen.PrimaryScreen.Bounds.Width;
            calc_dpi(2048, 1152, 27);

            // 233.52
            // 175.14
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 3));
                wait.Until<bool>((IWebDriver driver) =>
            {
               
                    var count = driver.WindowHandles.Count;
                    return false;
                

            });

            //OpenQA.Selenium.WebDriverTimeoutException
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = (Convert.ToInt32(textBox1.Text) + 5).ToString();
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = (Convert.ToInt32(textBox2.Text) + 5).ToString();
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = (Convert.ToInt32(textBox1.Text) - 5).ToString();
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = (Convert.ToInt32(textBox2.Text) - 5).ToString();
        }
    }
}
