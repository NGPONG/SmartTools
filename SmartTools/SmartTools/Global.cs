using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools
{
    public class Global
    {
        public static string __KEY;
        public static string __CULTURE;
        public static string __SERVERADDRESS;
        public static string __PORT;
        public static string __CONFIGFILE;
        public static string __BROWSERSUPPORT;
        public static Size __BROWSER_WINDOWSIZE;

        static Global()
        {
            __KEY = "SMTDESIV";
            __CULTURE = "zh_cn";
            __CONFIGFILE = "smt-config.json";
            __BROWSERSUPPORT = "chrome";
            __BROWSER_WINDOWSIZE = new Size(800, 600);

            // 配置文件中配置
            __SERVERADDRESS = "127.0.0.1";
            __PORT = "9876";
        }
    }
}
