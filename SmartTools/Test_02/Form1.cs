using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
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
            driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory + "\\Resources");
            driver.Url = "https://98613p.com/";
            driver.Manage().Window.Position = new Point(0, 0);
            driver.Manage().Window.Size = new Size(1200, 1000);
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
                driver.Manage().Window.Position = new Point(0, 0);
                driver.Manage().Window.Size = new Size(1200, 1000);
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
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 913, 534).Click().Build().Perform(); // 1139,663
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

        private void Button15_Click(object sender, EventArgs e)
        {
            using (var engine = new TesseractEngine("./tessdata", "chi_sim", EngineMode.Default))
            {
                while (true)
                {
                    var picBuffer = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
                    if (picBuffer.Length == 0)
                    {
                        MessageBox.Show("获取图片失败");
                        Thread.Sleep(1000);
                        continue;
                    }

                    using (MemoryStream stream = new MemoryStream(picBuffer))
                    {
                        Bitmap bitmap_In = new Bitmap(stream);
                        Bitmap bitmap_Out = new Bitmap(371, 110, PixelFormat.Format24bppRgb);

                        Graphics g = Graphics.FromImage(bitmap_Out);
                        g.DrawImage(bitmap_In, new Rectangle(0, 0, 371, 110), new Rectangle(609, 362, 371, 110), GraphicsUnit.Pixel);


                        using (var page = engine.Process(bitmap_Out, PageSegMode.Auto))
                        {
                            string strReadPic = page.GetText();
                            if (strReadPic.Contains("已开局"))
                            {
                                break;
                            }
                        }
                    }
                }

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
    }
}
