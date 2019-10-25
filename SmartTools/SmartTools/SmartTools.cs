using System;
using System.Collections.Generic;
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
                    Console.WriteLine("HelloWorld");
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                // Application.Run(new Form1());
            }
        }
    }
}
