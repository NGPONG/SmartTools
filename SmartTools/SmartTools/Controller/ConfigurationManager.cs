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

namespace SmartTools.Controller
{
    public class ConfigurationManager
    {
        #region member
        private static ConfigurationManager manager = null;
        private static object locker = new object();
        #endregion

        public List<Configuration> Configs = new List<Configuration>();

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
                    }
                }
            }

            return manager;
        }

        public List<Configuration> GetUserConfigs()
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
                Configs = JsonConvert.DeserializeObject<List<Configuration>>(source);

            return this;
        }
    }
}
