using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Configuration;
using SmartTools.Service.Module;
using SmartTools.Service.Module.Configuration;
using System.ServiceModel.Description;

namespace SmartTools.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            // 服务契约
            var contracts = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "SmartTools.Service.Contract");
            // 服务实现
            var services = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "SmartTools.Service.Implementation");

            foreach (var service in services)
            {
                Uri url = new Uri($"http://127.0.0.1:{ConfigurationManager.AppSettings["Port"]}/{service.Name}");

                ServiceHost host = new ServiceHost(service, url);
                host.Opened += (object sender, EventArgs e) =>
                {
                    Console.WriteLine("[SmartTools] Opened!");
                };

                host.AddServiceEndpoint(contracts.Where(c => c.Name.Contains(service.Name)).FirstOrDefault(), ServiceBinding.Create<WebHttpBinding>(), url);

                host.LoadBehaviors();
                host.InitBehaviors();

                host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                // Start
                ((ICommunicationObject)host).Open();
            }

            Console.ReadKey();
        }
    }
}
