using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Test_06
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime time = Convert.ToDateTime("2019-10-22 19:54:53.287");
            DateTime time_Add = time + TimeSpan.FromDays(3);
            TimeSpan timeSpan = time_Add - time;
            int seconds = timeSpan.Seconds;
        }
    }
}
