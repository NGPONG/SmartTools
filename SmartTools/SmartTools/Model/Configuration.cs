using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Model
{
    public class Configuration
    {
        public Proxy Proxy { get; set; }
        public CustomAction Action { get; set; }
        public string Authentication { get; set; }
    }
}
