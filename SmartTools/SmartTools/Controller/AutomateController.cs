using OpenQA.Selenium;
using SmartTools.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartTools.Utils.Extensions;
using Tesseract;

namespace SmartTools.Controller
{
    public class AutomateController
    {
        #region member
        private static object locker = new object();
        private static AutomateController manager = null;
        #endregion

        private Dictionary<string, IWebDriverController> _driverHandler = new Dictionary<string, IWebDriverController>();

        public CancellationToken EngineCancelToken = new CancellationToken();

        private AutomateController() { }

        public static AutomateController Instance()
        {
            if (manager == null)
            {
                lock (locker)
                {
                    if (manager == null)
                    {
                        manager = new AutomateController();
                    }
                }
            }

            return manager;
        }

        public void Open(string configName, string url, Action OnWebDriverOpened = null, Action OnWebDriverClosed = null, Action OnWebDriverStarted = null, Action OnWebDriverStopped = null)
        {
            try
            {
                var driverController = WebDriverFactory.Get(Global.__BROWSERSUPPORT);
                driverController.OnWebDriverOpened += OnWebDriverOpened;
                driverController.OnWebDriverClosed += OnWebDriverClosed;
                driverController.OnWebDriverStarted += OnWebDriverStarted;
                driverController.OnWebDriverStopped += OnWebDriverStopped;

                var webDriver = driverController.CreateDrvier();
                if (webDriver == null)
                    throw new Exception("驱动启动失败!检查日志");
                webDriver.Manage().Window.Size = Global.__BROWSER_WINDOWSIZE;
                webDriver.Url = url;
                SetDriverPostion(webDriver);
                driverController.Status = DriverState.Open;

                _driverHandler[configName] = driverController;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Close(string configName)
        {
            _driverHandler[configName].Close();
            _driverHandler.Remove(configName);
        }

        public void StartAction(string configName, List<CustomAction> actions)
        {
            var driverController = _driverHandler.TryGet(configName);
            // This is caused by the fact that WebDriver has not been opened yet.
            if (driverController == null)
                return;

            Task.Factory.StartNew(() =>
            {
                driverController.Start(actions);
            }, driverController.ControllerCancelToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void SetDriverPostion(IWebDriver webDriver)
        {
            if (_driverHandler.Count == 0)
            {
                webDriver.Manage().Window.Position = new Point(-7, 0);
            }
            else
            {
                int windows_Height = Screen.PrimaryScreen.Bounds.Height;
                int windows_Width = Screen.PrimaryScreen.Bounds.Width;

                int x = -7;
                int y = 0;
                for (int i = 1; i <= _driverHandler.Count; i++)
                {
                    if (i % (int)Math.Round((double)(windows_Width / Global.__BROWSER_WINDOWSIZE.Width)) == 0)
                    {
                        x = -7;
                        y += Global.__BROWSER_WINDOWSIZE.Height;
                        continue;
                    }

                    x += (Global.__BROWSER_WINDOWSIZE.Width - 9);
                }

                webDriver.Manage().Window.Position = new Point(x, y);
            }
        }
    }
}
