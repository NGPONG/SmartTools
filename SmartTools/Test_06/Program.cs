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
            Timer timer = new Timer((object state) =>
            {
                Console.WriteLine("哈哈哈");
            }, null, 0, 10);

            

            Console.ReadLine();
        }
    }
}
