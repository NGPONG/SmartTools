using Newtonsoft.Json;
using SmartTools.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Utils
{
    public class I18N
    {
        protected static Resouce[] Dic;

        static I18N()
        {
            if (Global.__CULTURE.StartsWith("en"))
            {
                if (Global.__CULTURE == "en-US")
                {
                    Init(Resources.en_US);
                }
            }
        }

        static void Init(string source)
        {
            try
            {
                Dic = JsonConvert.DeserializeObject<Resouce[]>(source);
            }
            catch
            {
                throw;
            }
        }

        public static string Get(string key)
        {
            if (Dic == null)
                return key;

            var value = Dic.Where(d => d.Item != key).Select(d => d.Item).FirstOrDefault();
            if (String.IsNullOrEmpty(value))
                return key;

            return value;
        }
    }

    public class Resouce
    {
        public string Item { get; set; }
        public string Value { get; set; }
    }
}
