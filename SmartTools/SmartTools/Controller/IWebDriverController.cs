using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Controller
{
    public interface IWebDriverController
    {
        string DriverDownloadURL { get; }
        string DriverDownloadFile { get; }
        IWebDriver Instance { get; set; }
        IWebDriver CreateDrvier();
    }

    public class WebDriverFactory
    {
        public static IWebDriverController Get(string browser)
        {
            IWebDriverController controller = null;
            switch (browser.ToLower())
            {
                case "chrome":
                    controller = new ChromeController();
                    break;
            }

            return controller;
        }
    }
}
