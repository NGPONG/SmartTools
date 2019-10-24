using log4net;
using Newtonsoft.Json;
using SmartTools.Common.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Test_06
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = JsonConvert.SerializeObject(new
            {
                Name = "Admin",
                ActivationLevel = 4
            });

            string str =  DESHelper.Encrypt(source, "SMTDESIV");
            //202265E69C3D36A4EE65FC125D6EBEBA69E71541B5BF66728E1A4FD9800E4C9FF4EADADEE9EC7C44
            string strD = DESHelper.Decrypt(str, "SMARTOOL");

            Console.WriteLine(strD);

            Console.ReadKey();
        }
    }
}
