using System;
using System.Collections.Generic;
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

        static Global()
        {
            __KEY = "SMTDESIV";
            __CULTURE = "zh_cn";
            
            //
            __SERVERADDRESS = "127.0.0.1";
            __PORT = "9876";
        }
    }
}
