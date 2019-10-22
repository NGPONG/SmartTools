using SmartTools.Service.Module.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace SmartTools.Service.Module
{
    public static class ServiceHostBaseExtensions
    {
        public static void LoadBehaviors(this ServiceHostBase host)
        {
            host.Description.Behaviors.Add(ServiceBehavior.CreateThrottlingBehavior());
            host.Description.Behaviors.Add(ServiceBehavior.CreateMetadataBehavior(host.Description.Endpoints.FirstOrDefault().Binding));

            host.Description.Endpoints.FirstOrDefault().Behaviors.Add(ServiceWebHttpBehaviors.CreateWebHttpBehavior());
        }

        public static void InitBehaviors(this ServiceHostBase host)
        {
            for (int j = 1; j < host.Description.Behaviors.Count; j++)
            {
                if (host.Description.Behaviors[j] is ServiceDebugBehavior)
                {
                    ServiceDebugBehavior debugBehavior = host.Description.Behaviors[j] as ServiceDebugBehavior;
                    debugBehavior.IncludeExceptionDetailInFaults = false;
                }
                else if (host.Description.Behaviors[j] is ServiceMetadataBehavior)
                {
                    ServiceMetadataBehavior serviceMetadataBehavior = host.Description.Behaviors[j] as ServiceMetadataBehavior;
                    serviceMetadataBehavior.HttpGetEnabled = false;
                }
            }
        }
    }
}
