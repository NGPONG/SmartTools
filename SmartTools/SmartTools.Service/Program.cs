using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Configuration;
using SmartTools.Service.Module;
using SmartTools.Service.Module.Configuration;
using System.ServiceModel.Description;
using System.Collections.Concurrent;
using SmartTools.Service.Utils;

namespace SmartTools.Service
{
    class Program
    {
        public static ConcurrentQueue<ICommunicationObject> _ServiceQueue = new ConcurrentQueue<ICommunicationObject>();

        static void Main(string[] args)
        {
            var contracts = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "SmartTools.Service.Contract" && t.IsPublic);
            var services = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "SmartTools.Service.Implementation" && t.IsPublic);

            foreach (var service in services)
            {
                Uri url = new Uri($"http://127.0.0.1:{AppConfiger.Port}/{service.Name}");

                ServiceHost host = new ServiceHost(service, url);
                host.Opened += (object sender, EventArgs e) =>
                {
                    Console.WriteLine("[SmartTools Service] Opened!");
                };
                host.Closed += (object sender, EventArgs e) =>
                {
                    Console.WriteLine("[SmartTools Service] closed!");
                };

                host.AddServiceEndpoint(contracts.Where(c => c.Name.Contains(service.Name)).FirstOrDefault(), ServiceBinding.Create<WebHttpBinding>(), url);

                host.LoadBehaviors();
                host.InitBehaviors();

                host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                var communicationObject = host as ICommunicationObject;
                communicationObject.Open();

                _ServiceQueue.Enqueue(communicationObject);
            }

            while (true)
            {
                var input = Console.ReadLine();
                if (input.Equals("Close Service", StringComparison.OrdinalIgnoreCase))
                {
                    while (true)
                    {
                        ICommunicationObject serviceHost;
                        bool isPeekSuccess = _ServiceQueue.TryPeek(out serviceHost);
                        if (!isPeekSuccess)
                            break;

                        serviceHost.Close();
                    }
                    break;
                }
            }
        }
    }
}
