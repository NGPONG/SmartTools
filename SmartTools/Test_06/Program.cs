using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_06
{
    class Program
    {
        static void Main(string[] args)
        {
            var serction = ConfigurationManager.GetSection("system.serviceModel/bindings");
        }
    }

    public class Person
    {
        public string Name { get; set; }
    }

    public class Student : Person
    {
        public int StudentAfe { get; set; }
    }
}
