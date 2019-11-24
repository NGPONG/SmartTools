using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test_11
{
    class Program
    {
        private static object _locker = new object();
        public static event Action Event1;
        public static event Action Event2;
        static void Main(string[] args)
        {
            Event1 += Program_Event1;
            Event1 += Program_Event11;
            Event2 += Program_Event2;
            //Event2 = Event1;
            Event2();

            Console.ReadKey();
        }

        private static void Program_Event2()
        {
            Console.WriteLine("Event2");
        }

        private static void Program_Event11()
        {
            Console.WriteLine("Event11");
        }

        private static void Program_Event1()
        {
            Console.WriteLine("Event1");
        }

        

        static async Task<string> GetResultAsync()
        {
            return await Task.Run(()=> 
            {
                Thread.Sleep(5000);
                return "123";
            });
        }
    }
}
