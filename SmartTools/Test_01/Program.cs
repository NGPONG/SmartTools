using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test_01
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory + "\\Resources");
            driver.Url = "http://gci.epda866.com:81/agingame/pcv1/index.jsp?";
            driver.Manage().Window.Position = new Point(0, 0);
            driver.Manage().Window.Size = new Size(1200, 1000);

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
                        Thread.Sleep(2000);

                        for (int i = 0; i < 2; i++)
                        {
                            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 718, 432).Click().Build().Perform();
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 718, 452);
                            Thread.Sleep(5000);
                            actions.Click().Build().Perform();
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 718, 472).Click().Build().Perform();
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
    }
}
