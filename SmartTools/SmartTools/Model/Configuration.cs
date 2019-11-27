using Newtonsoft.Json;
using SmartTools.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
                cookies = $"{Global.__AUTHENTICATIONKEY}={value}";
                _authentication = value;
            }
        }
        public string Url { get; set; }
        public string StopMoney { get; set; }
        public bool IsCycle { get; set; }
        public bool IsMoneyWarning { get; set; }
        public Proxy Proxy { get; set; }
        public List<CustomAction> Action { get; set; }

        public Configuration()
        {
            Address = @"https://98613p.com/Account/GetMyBalance";
            Method = Method.POST;
        }
        public static Configuration CreateDefualtConfig(string configName = "默认配置")
        {
            return new Configuration()
            {
                ConfigurationName = configName,
                Authentication = string.Empty,
                Url = string.Empty,
                StopMoney = string.Empty,
                IsCycle = true,
                Action = new List<CustomAction>(),
                Proxy = new Proxy()
                {
                    IP = string.Empty,
                    Port = string.Empty
                }
            };
        }

        public async Task<string> GetBalanceAsync()
        {
            return await Task.Run(()=> 
            {
                HttpController httpController = new HttpController();
                httpController.header = this;
                
                var strReturn = string.Empty;
                var response = httpController.Start();
                using (StreamReader reader = new StreamReader(response))
                {
                    strReturn = reader.ReadLine();
                }

                return strReturn;
            });
        }
    }
}
