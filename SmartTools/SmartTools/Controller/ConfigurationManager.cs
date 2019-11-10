using Newtonsoft.Json;
using SmartTools.Common.Helper;
using SmartTools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Controller
{
    public class ConfigurationManager
    {
        #region member
        private static ConfigurationManager manager = null;
        private static object locker = new object();
        #endregion

        private static List<Configuration> Configs = new List<Configuration>();

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

        public ConfigurationManager Save()
        {
            string strJson = JsonConvert.SerializeObject(Configs);
            IOHelper.SaveToFile("/Temp", Global.__CONFIGFILE, Encoding.ASCII.GetBytes(Convert.ToBase64String(Encoding.ASCII.GetBytes(strJson))));

            return this;
        }

        public ConfigurationManager Load()
        {
            var source = Encoding.ASCII.GetString(Convert.FromBase64String(Encoding.ASCII.GetString(IOHelper.ReadToFile("/Temp", Global.__CONFIGFILE))));
            Configs = JsonConvert.DeserializeObject<List<Configuration>>(source);

            return this;
        }
    }
}
