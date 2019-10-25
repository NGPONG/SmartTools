using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Service
{
    public static class Global
    {
        public static string __KEY;

        static Global()
        {
            __KEY = "SMTDESIV";
        }
    }
}
