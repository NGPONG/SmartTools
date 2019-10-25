using System;
using System.Linq;
using System.Configuration;

namespace SmartTools.Service.Utils
{
    public static class Configuration
    {
        #region CustomSetting
        public static string Port;
        public static string Email; 
        #endregion

        static Configuration()
        {
            var setting = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.Settings;

            #region init
            Port = setting["Port"].Value.ToString();
            Email = setting["Email"].Value.ToString(); 
            #endregion
        }
    }
}
