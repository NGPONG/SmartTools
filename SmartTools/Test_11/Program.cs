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
        static void Main(string[] args)
        {
            Action action = new Action(DoSomeThing);
            var result = action.BeginInvoke(new AsyncCallback(CallBack), null);

            result.AsyncWaitHandle.WaitOne();
            string str = "321";

            Console.ReadLine();
        }

        static void DoSomeThing()
        {
            Thread.Sleep(4000);
        }

        static void CallBack(IAsyncResult result)
        {
            ((((AsyncResult)result).AsyncDelegate) as Action).EndInvoke(result);

            for (int i = 0; i < 5; i++)
            {
                string str = "123";
            }
        }
    }
}
