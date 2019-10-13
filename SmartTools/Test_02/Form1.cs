using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            driver.Url = "http://gci.epda866.com:81/agingame/pcv1/index.jsp?";
            driver.Manage().Window.Position = new Point(0, 0);
            driver.Manage().Window.Size = new Size(1200, 1000);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(By.TagName("canvas")), 563, 480).Click().Build().Perform(); // 573,543
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
    }
}
