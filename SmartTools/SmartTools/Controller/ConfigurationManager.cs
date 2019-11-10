using Newtonsoft.Json;
using SmartTools.Common.Helper;
using SmartTools.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms;

namespace SmartTools.Controller
{
    public class ConfigurationManager
    {
        #region member
        private static ConfigurationManager manager = null;
        private static object locker = new object();

        public Task _saveConfig;
        public Task _loadConfig;
        #endregion

        private List<Configuration> Configs = new List<Configuration>();

        private ConfigurationManager()
        {
            if (_saveConfig == null)
                _saveConfig = new Task((object state) =>
                {
                    var configActive = state as Dictionary<string, System.Windows.Forms.TabPage>;
                    if (configActive != null)
                    {
                        Save(configActive);
                    }
                }, FormManager.Instance()._userConfigs, TaskCreationOptions.LongRunning);

            if (_loadConfig == null)
                _loadConfig = new Task(() =>
                {
                    this.Load();
                }, TaskCreationOptions.LongRunning);
        }

        public static ConfigurationManager Instance()
        {
            if (manager == null)
            {
                lock (locker)
                {
                    if (manager == null)
                    {
                        manager = new ConfigurationManager();
                    }
                }
            }

            return manager;
        }

        public List<Configuration> GetUserConfigs()
        {
            return Configs;
        }

        public ConfigurationManager Save(Dictionary<string, System.Windows.Forms.TabPage> userConfig)
        {
            var configs = new List<Configuration>();

            foreach (KeyValuePair<string, System.Windows.Forms.TabPage> config in userConfig)
            {
                var controls = config.Value.Controls.OfType<Control>();

                Configuration configuration = new Configuration()
                {
                    ConfigurationName = controls.Where(
                        c => 
                        c.Name == $"txtConfigName_{config.Key}")
                    .Select(
                        c => 
                        c.Text)
                    .FirstOrDefault(),
                    Authentication = controls.Where(c => c.Name == $"txtAuthentication_{config.Key}").Select(c => c.Text).FirstOrDefault(),
                    Url = controls.Where(c => c.Name == $"txtUrl_{config.Key}").Select(c => c.Text).FirstOrDefault(),
                    StopMoney = controls.Where(c => c.Name == $"txtMoneyWarning_{config.Key}").Select(c => c.Text).FirstOrDefault(),
                    IsCycle = (controls.Where(c => c.Name == $"cblblIsCycle_{config.Key}").FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox).Checked,
                    Proxy = new Proxy()
                    {
                        IP = controls.Where(c => c.Name == $"pnlProxy_{config.Key}")
                             .Select(c => c.Controls)
                             .FirstOrDefault()
                             .OfType<System.Windows.Forms.Control>()
                             .Where(c => c.Name == $"txtIP_{config.Key}")
                             .Select(c => c.Text).FirstOrDefault(),
                        Port = controls.Where(c => c.Name == $"pnlProxy_{config.Key}")
                               .Select(c => c.Controls)
                               .FirstOrDefault()
                               .OfType<System.Windows.Forms.Control>()
                               .Where(c => c.Name == $"txtPort_{config.Key}")
                               .Select(c => c.Text).FirstOrDefault()
                    },
                    Action = new List<CustomAction>()
                };

                var customActions = new List<CustomAction>();
                var listView = controls.Where(c => c.Name == $"mlvData_{config.Key}").FirstOrDefault() as MaterialSkin.Controls.MaterialListView;
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
                        configuration.Action.Add(action);
                    }
                }


                configs.Add(configuration);
            }

            string strJson = JsonConvert.SerializeObject(configs);
            IOHelper.SaveToFile("\\Temp", Global.__CONFIGFILE, Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(strJson))));

            return this;
        }

        public ConfigurationManager Load()
        {
            var source = Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.UTF8.GetString(IOHelper.ReadToFile("/Temp", Global.__CONFIGFILE))));
            Configs = JsonConvert.DeserializeObject<List<Configuration>>(source);

            return this;
        }
    }
}
