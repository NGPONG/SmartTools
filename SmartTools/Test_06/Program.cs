using log4net;
using Newtonsoft.Json;
using SmartTools.Common.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test_06.Properties;
using static System.Net.Mime.MediaTypeNames;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Test_06
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = JsonConvert.DeserializeObject<dynamic>(Encoding.UTF8.GetString(Resources._1123));
            var name = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures);
        }
    }

    public interface Test
    {
    }
}
