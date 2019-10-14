using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tesseract;

namespace Test_01
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                IWebDriver driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory + "\\Resources");
                driver.Url = "http://gci.epda866.com:81/agingame/pcv1/index.jsp?";
                driver.Manage().Window.Position = new Point(0, 0);
                driver.Manage().Window.Size = new Size(1200, 1000); // 1200 , 1000

                while (true)
                {
                    string strRead = Console.ReadLine();
                    if (strRead == "start postion")
                    {
                        var canvas = driver.FindElement(By.TagName("canvas"));
                        Actions actions = new Actions(driver);
                        actions.MoveToElement(canvas, 573, 543).Click().Build().Perform();

                        Thread.Sleep(2000);

                        string strRead2 = Console.ReadLine();
                        if (strRead2 == "start")
                        {
                            using (var engine = new TesseractEngine("./tessdata", "chi_sim", EngineMode.Default))
                            {
                                while (true)
                                {
                                    var picBuffer = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
                                    if (picBuffer.Length == 0)
                                    {
                                        Console.WriteLine("获取图片失败");
                                        Thread.Sleep(1000);
                                        continue;
                                    }

                                    using (MemoryStream stream = new MemoryStream(picBuffer))
                                    {
                                        Bitmap bitmap_In = new Bitmap(stream);
                                        Bitmap bitmap_Out = new Bitmap(371, 110, PixelFormat.Format24bppRgb);

                                        Graphics g = Graphics.FromImage(bitmap_Out);
                                        g.DrawImage(bitmap_In, new Rectangle(0, 0, 371, 110), new Rectangle(609, 362, 371, 110), GraphicsUnit.Pixel);

                                        using (var page = engine.Process(bitmap_Out))
                                        {
                                            string strReadPic = page.GetText();
                                            if (strReadPic.Contains("已开局"))
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }

                                Console.WriteLine("开始下注");

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
                    else if (strRead == "exit")
                    {
                        driver.SwitchTo().Window(driver.WindowHandles.First()).Close();
                        driver.Quit();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
