using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SmartTools.Common.Helper;

namespace SmartTools.Controller
{
    public class ChromeController : IWebDriverController
    {
        public string DriverDownloadURL => "http://chromedriver.storage.googleapis.com/";

        public string DriverDownloadFile => "chromedriver_win32.zip";

        public IWebDriver Instance { get => this.Instance; set => this.Instance = value; }

        public IWebDriver CreateDrvier()
        {
            if (!File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\chromedriver.exe"))
            {
                string filePath = IOHelper.SearchFile("chrome.exe");
                DownLoadFile(filePath);
            }
        }

        public void DownLoadFile(string filePath)
        {
            var version = FileVersionInfo.GetVersionInfo(filePath)?.FileVersion;
            var versionSplit = version.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (versionSplit.Length > 3)
            {
                var masterVersion = versionSplit[0];     
                var subVersion1 = versionSplit[1];       
                var subVersion2 = versionSplit[2];       
                var subVersion3 = versionSplit[3];
                var threeVersionNumber = $"{masterVersion}.{subVersion1}.{subVersion2}";

            }
        }
    }
}
