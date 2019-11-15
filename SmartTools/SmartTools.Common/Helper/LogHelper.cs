using log4net;
using System;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace SmartTools.Common.Helper
{
    public static class LogHelper
    {
        private static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Error(Exception objException)
        {
            string message = objException.InnerException == null ? objException.Message : objException.InnerException.Message;
            Logger.Error(message);
        }

        public static void Info(string message)
        {
            Logger.Info(message);
        }

        public static void Warn(Exception objException)
        {
            string message = objException.InnerException == null ? objException.Message : objException.InnerException.Message;
            Logger.Warn(message, objException);
        }
    }
}
