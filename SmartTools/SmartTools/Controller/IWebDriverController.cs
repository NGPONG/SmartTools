﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SmartTools.Model;
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
        Actions CustomActions { get; }
        ActionPoint Postion { get; }
        IWebDriver Instance { get; set; }
        IWebDriver CreateDrvier();
        void Close();
        void Start();
        void Stop();

        event Action OnWebDriverOpened;
        event Action OnWebDriverClosed;
        event Action OnWebDriverStarted;
        event Action OnWebDriverStopped;
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
