using Newtonsoft.Json;
using SmartTools.Common.Helper;
using SmartTools.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.Control;

namespace SmartTools.Controller
{
    public class ConfigurationManager
    {
        #region member
        private static ConfigurationManager manager = null;
        private static object locker = new object();
        #endregion

        public Dictionary<string, Configuration> Configs = new Dictionary<string, Configuration>();

        private ConfigurationManager() { }

        public static ConfigurationManager Instance()
        {
            if (manager == null)
            {
                lock (locker)
                {
                    if (manager == null)
                    {
                        manager = new ConfigurationManager();
                        // Add a defualt value to configs
                        manager.Configs["默认配置"] = Configuration.CreateDefualtConfig();
                    }
                }
            }

            return manager;
        }

        public Dictionary<string, Configuration> GetUserConfigs()
        {
            return Configs;
        }

        public async void SaveConfig()
        {
            await Task.Factory.StartNew(() =>
            {
                Save();
            }, TaskCreationOptions.LongRunning);
        }

        public async Task LoadConfig()
        {
            await Task.Factory.StartNew(() =>
            {
                Load();
            }, TaskCreationOptions.LongRunning);
        }

        private ConfigurationManager Save()
        {
            string strJson = JsonConvert.SerializeObject(Configs);
            IOHelper.SaveToFile("\\Temp", Global.__CONFIGFILE, Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(strJson))));

            return this;
        }

        public ConfigurationManager Load()
        {
            var source = Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.UTF8.GetString(IOHelper.ReadToFile("/Temp", Global.__CONFIGFILE))));
            if (!string.IsNullOrEmpty(source))
                Configs = JsonConvert.DeserializeObject<Dictionary<string, Configuration>>(source);

            return this;
        }

        public ConfigurationManager AddConfig(TabPage page)
        {
            var subControls = page.Controls.OfType<Control>();

            var config = new Configuration()
            {
                ConfigurationName = page.Text,
                Authentication = subControls.Where(c => c.Name == $"txtAuthentication_{page.Text}")
                                            .Select(
                    c => 
                    c.Text)
                                            .FirstOrDefault(),
                Url = subControls.Where(c => c.Name == $"txtUrl_{page.Text}")
                                 .Select(c => c.Text)
                                 .FirstOrDefault(),
                StopMoney = subControls.Where(c => c.Name == $"txtMoneyWarning_{page.Text}")
                                       .Select(c => c.Text)
                                       .FirstOrDefault(),
                IsCycle = (subControls.Where(c => c.Name == $"cblblIsCycle_{page.Text}")
                                      .FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox).Checked,
                Proxy = new Proxy()
                {
                    IP = subControls.Where(c => c.Name == $"pnlProxy_{page.Text}")
                                    .Select(c => c.Controls)
                                    .FirstOrDefault()
                                    .OfType<System.Windows.Forms.Control>()
                                    .Where(c => c.Name == $"txtIP_{page.Text}")
                                    .Select(c => c.Text).FirstOrDefault(),
                    Port = subControls.Where(c => c.Name == $"pnlProxy_{page.Text}")
                                      .Select(c => c.Controls)
                                      .FirstOrDefault()
                                      .OfType<System.Windows.Forms.Control>()
                                      .Where(c => c.Name == $"txtPort_{page.Text}")
                                      .Select(c => c.Text).FirstOrDefault()
                },
                Action = new List<CustomAction>()
            };

            var listView = subControls.Where(c => c.Name == $"mlvData_{page.Text}")
                                      .FirstOrDefault() as MaterialSkin.Controls.MaterialListView;
            if (listView != null)
            {
                foreach (ListViewItem item in listView.Items)
                {
                    CustomAction action = new CustomAction()
                    {
                        ActionIndex = item.SubItems[0].Text,
                        BetType = CustomAction.ToBet(item.SubItems[1].Text),
                        Delay = item.SubItems[2].Text,
                        Money = item.SubItems[3].Text
                    };
                    config.Action.Add(action);
                }
            }

            this.Configs[page.Text] = config;

            return this;
        }

        public ConfigurationManager RemoveConfig(string configName)
        {
            this.Configs.Remove(configName);

            return this;
        }

        public ConfigurationManager ConvertAllControlToConfigs(ControlCollection controls)
        {
            var tabControl = controls["tcMaster"] as MaterialSkin.Controls.MaterialTabControl;
            if (tabControl == null)
                return this;

            foreach (TabPage page in tabControl.TabPages)
            {
                AddConfig(page);
            }

            return this;
        }
    }
}
