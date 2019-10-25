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

        static Global()
        {
            __KEY = "SMTDESIV";
            __CULTURE = "zh_cn";
        }
    }
}
