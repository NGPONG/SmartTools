using System;
using System.Configuration;

namespace SmartTools.Service.Utils
{
    public static class AppConfiger
    {
        public static string Port = ConfigurationManager.AppSettings["Port"];

        public static string Email = ConfigurationManager.AppSettings["Email"];
    }
}
