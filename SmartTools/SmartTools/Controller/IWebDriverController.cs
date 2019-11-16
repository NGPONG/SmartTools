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

    public class WebDriver
    {
        public static IWebDriverController GetDriver(string browser)
        {
            IWebDriverController webDriver = null;
            switch (browser.ToLower())
            {
                case "chrome":
                    webDriver = new ChromeController();
                    break;
            }

            return webDriver;
        }
    }
}
