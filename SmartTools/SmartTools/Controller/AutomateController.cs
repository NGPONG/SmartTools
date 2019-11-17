using OpenQA.Selenium;
using SmartTools.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTools.Controller
{
    public class AutomateController
    {
        #region member
        private static object locker = new object();
        private static AutomateController manager = null;
        #endregion

        private Dictionary<string, IWebDriver> _driverHandler = new Dictionary<string, IWebDriver>();

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

        public void Open(string configName, string url)
        {
            try
            {
                var driverController = WebDriverFactory.Get(Global.__BROWSERSUPPORT);
                if (driverController == null)
                    throw new Exception("驱动启动失败!检查日志");

                var webDriver = driverController.CreateDrvier();

                webDriver.Manage().Window.Size = Global.__BROWSER_WINDOWSIZE;
                webDriver.Url = url;
                SetDriverPostion(webDriver);

                _driverHandler[configName] = webDriver;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
                bool isCycle = false;
                for (int i = 1; i <= _driverHandler.Count; i++)
                {
                    if (isCycle)
                    {
                        x = -7;
                        y += Global.__BROWSER_WINDOWSIZE.Height;
                    }

                    if (i % (int)Math.Round((double)(windows_Height / Global.__BROWSER_WINDOWSIZE.Width)) == 0)
                        isCycle = true;
                    else
                        isCycle = false;

                    x += Global.__BROWSER_WINDOWSIZE.Width;
                }

                webDriver.Manage().Window.Position = new Point(x, y);
            }
        }

        public void Close()
        {
            foreach (KeyValuePair<string, IWebDriver> driver in _driverHandler)
            {
                driver.Value.Quit();
                driver.Value.Close();
                driver.Value.Dispose();
            }
        }
    }
}
