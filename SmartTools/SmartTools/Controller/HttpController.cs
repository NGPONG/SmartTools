using SmartTools.Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SmartTools.Controller
{
    public class HttpController
    {
        public string Address { get; set; }
        public string Method { get; set; }
        public string ContentType { get; set; }
        public string Parameter { get; set; }

        public string UserAgent => _userAgent ?? (_userAgent = _defaultUserAgent);

        private string _defaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        private string _userAgent;

        private HttpWebRequest requestContext;

        private HttpWebResponse responseContext;

        public Stream Start()
        {
            GC.Collect();

            try
            {
                if (string.IsNullOrEmpty(Address))
                {
                    throw new ArgumentNullException("empty address");
                }

                ServicePointManager.DefaultConnectionLimit = 512;
                requestContext = Init();

                responseContext = (HttpWebResponse)requestContext.GetResponse();
                return responseContext.GetResponseStream();
            }
            catch (Exception objException)
            {
                if (objException.InnerException != null)
                {
                    LogHelper.Error(objException.InnerException);
                    throw new Exception(objException.InnerException.Message);
                }
                LogHelper.Error(objException);
                throw new Exception(objException.Message);
            }
        }

        public void Close() { this.requestContext?.Abort(); this.responseContext?.Close(); this.responseContext?.Dispose(); }

        public HttpWebRequest Init()
        {
            if (Address.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => { return true; };
                requestContext = WebRequest.Create(Address) as HttpWebRequest;
                requestContext.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                requestContext = (HttpWebRequest)WebRequest.Create(Address);
            }
            requestContext.KeepAlive = false;
            requestContext.Timeout = 60000;
            requestContext.ReadWriteTimeout = 60000;
            requestContext.Method = Method;
            requestContext.ContentType = ContentType;
            requestContext.UserAgent = UserAgent;
            if (Method == "POST")
            {
                byte[] buffer = ASCIIEncoding.UTF8.GetBytes(Parameter);
                requestContext.ContentLength = buffer.Length;
                using (Stream stream = requestContext.GetRequestStream())
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }

            return requestContext;
        }

    }
}
