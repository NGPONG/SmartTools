using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace SmartTools.Service.Module.Configuration
{
    public static class ServiceBehavior
    {
        public static ServiceThrottlingBehavior CreateThrottlingBehavior()
        {
            // 查找配置文件中是否定义了Binding，没有再创建默认
            // ..

            return CreateDefaultThrottlingBehavior();
        }

        private static ServiceThrottlingBehavior CreateDefaultThrottlingBehavior()
        {
            ServiceThrottlingBehavior objServiceThrottlingBehavior = new ServiceThrottlingBehavior();
            objServiceThrottlingBehavior.MaxConcurrentInstances = 3000;
            objServiceThrottlingBehavior.MaxConcurrentCalls = 3000;
            objServiceThrottlingBehavior.MaxConcurrentSessions = 3000;

            return objServiceThrottlingBehavior;
        }

        public static ServiceMetadataBehavior CreateMetadataBehavior(Binding binding)
        {
            // 查找配置文件中是否定义了Binding，没有再创建默认
            // ..

            return CreateDefaultMetadataBehavior(binding);
        }

        private static ServiceMetadataBehavior CreateDefaultMetadataBehavior(Binding binding)
        {
            // 查找配置文件中是否定义了Binding，没有再创建默认
            // ..

            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            behavior.HttpGetEnabled = true;
            behavior.HttpGetBinding = binding;

            return behavior;
        }
    }
}
