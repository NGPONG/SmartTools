using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Controller
{
    public class AutomateController
    {
        #region member
        private static object locker = new object();
        #endregion

        private Dictionary<string, IWebDriver> _driverHandler = new Dictionary<string, IWebDriver>();

        public AutomateController() { }

        public void AddDriver(string configName)
        {

        }
    }
}
