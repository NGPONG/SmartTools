using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Controller
{
    public class Header
    {
        public string Address { get; set; }
        public string Method { get; set; }
        public string Parameter { get; set; }
        public virtual string ContentType { get => "application/json"; set => this.ContentType = value; }
        public virtual string UserAgent { get => "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)"; set => this.ContentType = value; }
    }
}
