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
        IWebDriver CreateDrvier();
    }
}
