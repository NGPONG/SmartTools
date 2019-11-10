using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Model
{
    public class Proxy
    {
        public string IP { get; set; }
        public string Port { get; set; }

        public static Proxy Empty()
        {
            return new Proxy() { IP = string.Empty, Port = string.Empty };
        }
    }
}
