using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SmartTools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tesseract;

namespace SmartTools.Controller
{
    public interface IWebDriverController
    {
        string DriverDownloadURL { get; }
        string DriverDownloadFile { get; }
        string DriverPath { get; }
        DriverState Status { get; set; }
        Actions Actions { get; }
        ActionPoint Postion { get; }
        TesseractEngine Tesseract { get; set; }
        CancellationTokenSource ControllerCancelToken { get; set; }
        IWebDriver Instance { get; set; }
        IWebDriver CreateDrvier();
        void SetEnumeratorQueue(IEnumerable<CustomAction> actions);
        void Close();
        void Start();
        void Stop();

        event Action OnWebDriverOpened;
        event Action OnWebDriverClosed;
        event Action OnWebDriverStarted;
        event Action OnWebDriverStopped;
    }

    public enum DriverState
    {
        Open = 0,
        Close = 1,
        Start = 2,
        Stop = 3
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
