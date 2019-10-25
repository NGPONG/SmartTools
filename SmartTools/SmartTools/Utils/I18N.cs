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
            string culture = System.Globalization.CultureInfo.CurrentCulture.Name;
            if (culture.StartsWith("en"))
            {
                if (culture == "en-US")
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
