using SmartTools.Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SmartTools.Controller
{
    public class HttpController
    {
        public Header header;

        private HttpWebRequest requestContext;
        private HttpWebResponse responseContext;

        public Stream Start()
        {
            GC.Collect();

            try
            {
                if (string.IsNullOrEmpty(header.Address))
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
            if (header.Address.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => { return true; };
                requestContext = WebRequest.Create(header.Address) as HttpWebRequest;
                requestContext.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                requestContext = (HttpWebRequest)WebRequest.Create(header.Address);
            }
            requestContext.KeepAlive = false;
            requestContext.Timeout = 60000;
            requestContext.ReadWriteTimeout = 60000;
            requestContext.Method = header.Method;
            requestContext.ContentType = header.ContentType;
            requestContext.UserAgent = header.UserAgent;
            if (header.Method == "POST")
            {
                byte[] buffer = ASCIIEncoding.UTF8.GetBytes(header.Parameter);
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
