using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SmartTools.Common.Helper;

namespace SmartTools.Controller
{
    public class ChromeController : IWebDriverController
    {
        public string DriverDownloadURL => "http://chromedriver.storage.googleapis.com/";

        public string DriverDownloadFile => "chromedriver_win32.zip";

        private IWebDriver _instance;
        public IWebDriver Instance
        {
            get
            {
                return this._instance;
            }
            set
            {
                this._instance = value;
            }
        }

        public event Action OnWebDriverOpened;

        public event Action OnWebDriverClosed;

        public IWebDriver CreateDrvier()
        {
            if (!File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\Driver\\chromedriver.exe"))
            {
                string filePath = IOHelper.SearchFile("chrome.exe");
                if (string.IsNullOrEmpty(filePath))
                    return null;

                DownLoadFile(filePath);
            }

            try
            {
                Instance = new ChromeDriver($"{AppDomain.CurrentDomain.BaseDirectory}\\Driver");
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                return null;
            }
            OnWebDriverOpened?.Invoke();

            return Instance;
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

                using (HttpClient httpClient = new HttpClient())
                {
                    // XHR version address.
                    var versionContent = httpClient.GetAsync($"{DriverDownloadURL}?delimiter=/&prefix=").Result.Content.ReadAsStreamAsync().Result;
                    XmlDocument doc = new XmlDocument();
                    doc.Load(versionContent);

                    var version_Nodes = doc.DocumentElement.ChildNodes.Cast<XmlNode>()
                    .Where(x => x.Name == "CommonPrefixes"
                                                           && x.ChildNodes[0].InnerText != "icons/"
                                                           && x.ChildNodes[0].InnerText.Remove(x.ChildNodes[0].InnerText.LastIndexOf(".")) == threeVersionNumber) // Remove unuse elements
                    .Select(n => n.InnerText.Remove(n.InnerText.Length - 1, 1))
                    .OrderBy(o => o.Substring(o.LastIndexOf(".")))
                    .ToList();

                    string userfulVersion = string.Empty;
                    for (int i = 0; i < version_Nodes.Count; i++)
                    {
                        int subVersionCompare = Convert.ToInt32(version_Nodes[i].Substring(version_Nodes[i].LastIndexOf(".") + 1));
                        if (i == 0)
                        {
                            // 0 ~ anyversion
                            if (Convert.ToInt32(subVersion3) < subVersionCompare)
                            {
                                userfulVersion = version_Nodes[i];
                                break;
                            }
                        }
                        else
                        {
                            int subVersionCompareLast = Convert.ToInt32(version_Nodes[i - 1].Substring(version_Nodes[i - 1].LastIndexOf(".") + 1));

                            if (i == version_Nodes.Count - 1)
                            {
                                userfulVersion = version_Nodes[i];
                                break;
                            }

                            // anyversion1 ~ anyversion2
                            if (Convert.ToInt32(subVersion3) > subVersionCompareLast && Convert.ToInt32(subVersion3) < subVersionCompare)
                            {
                                userfulVersion = version_Nodes[i];
                                break;
                            }
                        }
                    }

                    using (var chromeDriver_Stream = httpClient.GetAsync($"{DriverDownloadURL}{userfulVersion}/{DriverDownloadFile}").Result.Content.ReadAsStreamAsync().Result)
                    {
                        chromeDriver_Stream.Seek(0, SeekOrigin.Begin);

                        byte[] buffer = new byte[1024 * 5];

                        using (ZipArchive archive = new ZipArchive(chromeDriver_Stream, ZipArchiveMode.Read))
                        {
                            ZipArchiveEntry entry = archive.GetEntry("chromedriver.exe");
                            using (var entry_Stream = entry.Open())
                            {
                                if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\Driver"))
                                    Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}\\Driver");

                                var output = new FileStream($"{AppDomain.CurrentDomain.BaseDirectory}\\Driver\\chromedriver.exe", FileMode.CreateNew, FileAccess.Write);

                                int stream_Available;
                                while (true)
                                {
                                    stream_Available = entry_Stream.Read(buffer, 0, buffer.Length);
                                    if (stream_Available == 0)
                                        break;
                                    output.Write(buffer, 0, stream_Available);

                                    Array.Clear(buffer, 0, buffer.Length);
                                }
                                output.Close();
                                output.Dispose();
                            }
                        }
                    }
                }
            }

            GC.Collect();
        }

        public void Close()
        {
            Instance.Quit();
            OnWebDriverClosed?.Invoke();
        }
    }
}
