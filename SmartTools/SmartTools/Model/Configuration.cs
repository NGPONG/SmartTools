using Newtonsoft.Json;
using SmartTools.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Configuration : Header
    {
        private string _authentication;

        public string ConfigurationName { get; set; }
        public string Authentication
        {
            get => this._authentication;
            set
            {
                base.cookies = $"{Global.__AUTHENTICATIONKEY}={value}";
                _authentication = value;
            }
        }
        public string Url { get; set; }
        public string StopMoney { get; set; }
        public bool IsCycle { get; set; }
        public Proxy Proxy { get; set; }
        public List<CustomAction> Action { get; set; }

        [JsonIgnore]
        public IWebDriverController DriverHandler { get; set; }

        public static Configuration CreateDefualtConfig(string configName = "默认配置")
        {
            return new Configuration()
            {
                ConfigurationName = configName,
                Authentication = string.Empty,
                Url = string.Empty,
                StopMoney = string.Empty,
                IsCycle = false,
                Action = new List<CustomAction>(),
                Proxy = new Proxy()
                {
                    IP = string.Empty,
                    Port = string.Empty
                }
            };
        }

        public void GetBalance()
        {

        }
    }
}
