using SmartTools.Service.Contract;
using SmartTools.Service.Module.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            //WebHttpBinding objWebHttpBinding = new WebHttpBinding();
            //objWebHttpBinding.Name = "WebHttpBindingConfig";

            //objWebHttpBinding.TransferMode = TransferMode.Buffered;
            //objWebHttpBinding.MaxBufferSize = 2147483647;
            //objWebHttpBinding.UseDefaultWebProxy = false;
            //objWebHttpBinding.MaxReceivedMessageSize = 2147483647;
            //objWebHttpBinding.MaxBufferPoolSize = 2147483647;
            //objWebHttpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            //objWebHttpBinding.ReaderQuotas.MaxDepth = 1024;
            //objWebHttpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
            //objWebHttpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            //objWebHttpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            //objWebHttpBinding.WriteEncoding = Encoding.UTF8;
            //objWebHttpBinding.AllowCookies = true;

            //ServiceHost objServiceHost = new ServiceHost(typeof(UserInfo), new Uri("http://127.0.0.1:9876" + "/" + "UserInfo/"));
            //ServiceEndpoint objServiceEndpoint = objServiceHost.AddServiceEndpoint(typeof(IUserInfo), objWebHttpBinding, new Uri("http://127.0.0.1:9876"+"/" + "UserInfo/"));
            //ServiceThrottlingBehavior objServiceThrottlingBehavior = new ServiceThrottlingBehavior();
            //objServiceThrottlingBehavior.MaxConcurrentInstances = 3000;
            //objServiceThrottlingBehavior.MaxConcurrentCalls = 3000;
            //objServiceThrottlingBehavior.MaxConcurrentSessions = 3000;
            ////开放元数据
            //ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            //behavior.HttpGetEnabled = true;
            //behavior.HttpGetBinding = objWebHttpBinding;
            //objServiceHost.Description.Behaviors.Add(objServiceThrottlingBehavior);
            //WebHttpBehavior objWebHttpBehavior = new WebHttpBehavior();
            //objWebHttpBehavior.DefaultBodyStyle = WebMessageBodyStyle.WrappedRequest;
            //objServiceEndpoint.Behaviors.Add(objWebHttpBehavior);
            //objServiceHost.Description.Behaviors.Add(behavior);

            //for (int j = 1; j < objServiceHost.Description.Behaviors.Count; j++)
            //{
            //    if (objServiceHost.Description.Behaviors[j] is ServiceDebugBehavior)
            //    {
            //        ServiceDebugBehavior debugBehavior = objServiceHost.Description.Behaviors[j] as ServiceDebugBehavior;
            //        debugBehavior.IncludeExceptionDetailInFaults = false;
            //    }
            //    else if (objServiceHost.Description.Behaviors[j] is ServiceMetadataBehavior)
            //    {
            //        ServiceMetadataBehavior serviceMetadataBehavior = objServiceHost.Description.Behaviors[j] as ServiceMetadataBehavior;
            //        serviceMetadataBehavior.HttpGetEnabled = false;
            //    }
            //}

            //objServiceHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
            ////启动服务
            //((ICommunicationObject)objServiceHost).Open();

            //Console.ReadKey();

            var contracts = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "SmartTools.Service.Contract");
            var services = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "SmartTools.Service.Imple");

            foreach (var service in services)
            {
                Uri url = new Uri($"http://127.0.0.1:{ConfigurationManager.AppSettings["Port"]}/{service.Name}");

                ServiceHost host = new ServiceHost(service, url);
                host.Opened += (object sender, EventArgs e) => 
                {
                    Console.WriteLine("[SmartTools] Opened!");
                };

                var serviceBinding = ServiceBinding.Create<WebHttpBinding>();

                var endPoint = host.AddServiceEndpoint(contracts.Where(c => c.Name.Contains(service.Name)).FirstOrDefault(), serviceBinding, url);
                host.Description.Behaviors.Add(ServiceBehavior.CreateThrottlingBehavior());
                host.Description.Behaviors.Add(ServiceBehavior.CreateMetadataBehavior(serviceBinding));

                //WebHttpBehavior objWebHttpBehavior = new WebHttpBehavior();
                //objWebHttpBehavior.DefaultBodyStyle = WebMessageBodyStyle.WrappedRequest;
                //objServiceEndpoint.Behaviors.Add(objWebHttpBehavior);
            }
        }
    }
}
