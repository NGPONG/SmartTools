using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Model
{
    public class Header
    {
        public string Address { get; set; }
        public Method Method { get; set; }
        public string Parameter { get; set; }
        public string cookies { get; set; }
        public virtual string Accept
        {
            get
            {
                return "application/json, text/plain, */*";
            } 
        }
        public virtual string ContentType
        {
            get
            {
                return "application/json";
            }
        }
        public virtual string UserAgent
        {
            get
            {
                return "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            }
        }

        public string ConvertMethod()
        {
            switch (Method)
            {
                case Method.POST:
                    return "POST";
                case Method.GET:
                    return "GET";
                case Method.HEAD:
                    return "HEAD";
                case Method.DELETE:
                    return "DELETE";
                default:
                    return null;
            }
        }
    }

    public enum Method
    {
        POST = 0,
        GET = 1,
        HEAD = 2,
        DELETE = 3
    }
}
