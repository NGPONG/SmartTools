using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Model
{
    public class Configuration
    {
        public string ConfigurationName { get; set; }
        public string Authentication { get; set; }
        public string Url { get; set; }
        public double StopMoney { get; set; }
        public bool IsCycle { get; set; }
        public Proxy Proxy { get; set; }
        public List<CustomAction> Action { get; set; }
    }
}
