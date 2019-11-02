using SmartTools.Controller;
using SmartTools.Utils;
using SmartTools.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTools
{
    static class SmartTools
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, $"Global\\SmartTools__{Application.StartupPath.GetHashCode()}"))
            {
                if (!mutex.WaitOne(0, false))
                {
                    Console.WriteLine(I18N.Get("初始化失败，Smart Tool已运行!"), I18N.Get("警告"));
                }

                Directory.SetCurrentDirectory(Application.StartupPath);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ApplicationExit += Application_ApplicationExit;
                Application.ThreadException += Application_ThreadException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                FormController.Instance().Start();

                Application.Run();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
        }
    }
}
