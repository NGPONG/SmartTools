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
        private static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            Logger.Info("这是 Info() 方法，用于记录【消息】。");

            Logger.Debug("这是 Debug() 方法，用于记录【调试】消息。");

            Logger.Warn("这是 Warn() 方法，用于记录【警告】消息。");

            Logger.Error("这是 Error() 方法，用于记录【异常】消息。");

            Logger.Fatal("这是 Fatal() 方法，用于记录【严重错误】消息。");

            Console.ReadKey();
        }
    }

    public class Person
    {
        public string Name { get; set; }
    }

    public class Student : Person
    {
        public int StudentAfe { get; set; }
    }
}
