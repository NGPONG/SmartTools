using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using SmartTools.Common.Helper;
using SmartTools.Model;
using SmartTools.Utils;
using SmartTools.Utils.Extensions;
using Tesseract;

namespace SmartTools.Controller
{
    public class ChromeController : IWebDriverController
    {
        #region Fields
        private static object _locker = new object();
        private IWebDriver _instance;
        private IWebElement _actionElement;
        private ActionPoint _postion;
        private TesseractEngine _tesseract;
        private string _configName;
        private CancellationTokenSource _controllerCancelToken;
        private const double _thresh = 80;
        private const double _thresholdMaxVal = 255;
        private DriverState _status;
        private IEnumerator<CustomAction> _actionsQueue;
        private Rectangle _trackingArea = new Rectangle(465, 225, 128, 18);
        private AutoResetEvent _isComplete = new AutoResetEvent(false);
        #endregion

        #region Property
        public string DriverDownloadURL => "http://chromedriver.storage.googleapis.com/";
        public string DriverDownloadFile => "chromedriver_win32.zip";
        public string DriverPath => $"{AppDomain.CurrentDomain.BaseDirectory}Driver\\chromedriver.exe";
        public Actions Actions => new Actions(Instance);
        public IWebElement ActionElement
        {
            get
            {
                if (this._actionElement == null)
                {
                    lock (_locker)
                    {
                        if (this._actionElement == null)
                        {
                            _actionElement = Instance.FindElement(By.TagName("canvas"));
                        }
                    }
                }

                return this._actionElement;
            }
        }
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
        public ActionPoint Postion
        {
            get
            {
                if (this._postion == null)
                    this._postion = new ActionPoint();
                return this._postion;
            }
        }
        public TesseractEngine Tesseract
        {
            get
            {
                return this._tesseract;
            }
            set
            {
                this._tesseract = value;
            }
        }
        public CancellationTokenSource ControllerCancelToken
        {
            get
            {
                return this._controllerCancelToken;
            }
            set
            {
                this._controllerCancelToken = value;
            }
        }
        public DriverState Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
                switch (this._status)
                {
                    case DriverState.Open:
                        OnWebDriverOpened?.Invoke();
                        break;
                    case DriverState.Close:
                        OnWebDriverClosed?.Invoke();
                        break;
                    case DriverState.Start:
                        OnWebDriverStarted?.Invoke();
                        break;
                    case DriverState.Stop:
                        OnWebDriverStopped?.Invoke();
                        break;
                    default:
                        break;
                }
            }
        }
        public string ConfigName
        {
            get
            {
                return _configName;
            }
            set
            {
                _configName = value;
            }
        }
        public Configuration Config
        {
            get
            {
                return ConfigurationManager.Instance().Configs[ConfigName];
            }
        }
        public Func<IEnumerator<CustomAction>, CustomAction> StartCallerAsync { get; set; }
        #endregion

        public event Action OnWebDriverOpened;
        public event Action OnWebDriverClosed;
        public event Action OnWebDriverStarted;
        public event Action OnWebDriverStopped;
        public event Action<string> OnProcessing;

        public ChromeController()
        {
            this.Tesseract = new TesseractEngine("./tessdata", "chi_sim", EngineMode.Default);
            this.ControllerCancelToken = new CancellationTokenSource();
            this.StartCallerAsync = WaitNewGambling;
        }

        public void Close()
        {
            if (Status == DriverState.Start)
            {
                Stop();
                _isComplete.WaitOne();
                Thread.Sleep(2000);
            }

            Instance.Quit();
            Status = DriverState.Close;
        }

        public void Stop()
        {
            ControllerCancelToken.Cancel();
            // Clear action queue
            this._actionsQueue = null;
        }

        public void Start()
        {
            Status = DriverState.Start;
            StartCallerAsync.BeginInvoke(_actionsQueue, new AsyncCallback(Do), "Async:OK!");
        }

        public IWebDriver CreateDrvier()
        {
            try
            {
                if (!File.Exists(DriverPath))
                {
                    string filePath = IOHelper.SearchFile("chrome.exe");
                    if (string.IsNullOrEmpty(filePath))
                        return null;

                    DownLoadFile(filePath);
                }


                Instance = new ChromeDriver($"{AppDomain.CurrentDomain.BaseDirectory}Driver");
#if DEBUG
                IntPtr driverHandler = Win32.FindWindow(null, DriverPath);
                if (driverHandler != IntPtr.Zero)
                    Win32.ShowWindow(driverHandler, Native.SW_HIDE); 
#endif

            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                Status = DriverState.Close;
                return null;
            }

            return Instance;
        }

        public CustomAction WaitNewGambling(IEnumerator<CustomAction> customActions)
        {
            try
            {
                while (true)
                {
                    if (ControllerCancelToken.IsCancellationRequested)
                    {
                        ControllerCancelToken = ControllerCancelToken.Reset();
                        return null;
                    }

                    var picBuffer = ((ITakesScreenshot)Instance).GetScreenshot().AsByteArray;

                    var bitmap_Cut = PictureHelper.ProcessImage(_trackingArea, picBuffer);

                    var page = Tesseract.Process(bitmap_Cut, PageSegMode.SingleBlock);
                    var readByPic = page.GetText();

                    // Dispose Unmanaged variable because of the calling recursive functions
                    Array.Clear(picBuffer, 0, picBuffer.Length);
                    page.Dispose();

                    if (Regex.IsMatch(readByPic, "开局"))
                    {
                        double balance;
                        var response = Config.GetBalanceAsync();
                        var isMatch = double.TryParse(response.Result, out balance); // Return HTML code maybe.                        
                        if (isMatch)
                        {
                            OnProcessing.Invoke(balance.ToString()); // Update GUI balance display.
                        }
                        // To call back function.
                        break;
                    }

                    GC.Collect();
                }

                return customActions.MoveNext<CustomAction>();
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                return null;
            }
        }

        public void Do(IAsyncResult result)
        {
            var action = ((((AsyncResult)result).AsyncDelegate) as Func<IEnumerator<CustomAction>, CustomAction>).EndInvoke(result);
            if (action == null)
            {
                _isComplete.Set();
                Status = DriverState.Stop;
                return;
            }

            try
            {
                // Move and Click the first chip.
                this.Actions.MoveToElement(ActionElement, Postion.One.X, Postion.One.Y).Click().Perform();

                if (action.BetType != Bet.停)
                {
                    double balance = -1;
                    // Check if the user's balance has reached the maximum threshold.
                    if (Config.IsMoneyWarning && !string.IsNullOrEmpty(Config.StopMoney))
                    {
                        var response = Config.GetBalanceAsync();

                        var isMatch = double.TryParse(response.Result, out balance); // Return HTML code maybe.                        
                        if (isMatch)
                        {
                            if (balance - Convert.ToDouble(action.Money) < Convert.ToDouble(Config.StopMoney))
                            {
                                _isComplete.Set();
                                Status = DriverState.Stop;
                                return;
                            }
                        }
                    }

                    var action_ChipCount = Math.Round(Convert.ToDouble(action.Money) / 10);

                    var action_Bet = this.Actions;
                    for (int i = 0; i < action_ChipCount; i++)
                    {
                        var postion = Postion.BetPoint(action.BetType);
                        action_Bet.MoveToElement(ActionElement, postion.X, postion.Y)
                                  .Click();
                    }
                    action_Bet.Perform();

                    this.Actions.MoveToElement(ActionElement, Postion.Confirm.X, Postion.Confirm.Y)
                                .Click()
                                .Perform();
                }

                // Custom delay.
                Thread.Sleep(Convert.ToInt32(action.Delay));

                // Call back.
                StartCallerAsync.BeginInvoke(_actionsQueue, new AsyncCallback(Do), "Async:Callback");
            }
            catch (Exception e)
            {
                Status = DriverState.Stop;
                LogHelper.Error(e);
                return;
            }
        }

        public void DownLoadFile(string filePath)
        {
            try
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SetEnumeratorQueue(IEnumerable<CustomAction> actions)
        {
            this._actionsQueue = actions.GetEnumerator();
        }
    }
}
