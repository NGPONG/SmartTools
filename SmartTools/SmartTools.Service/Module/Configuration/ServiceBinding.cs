using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SmartTools.Service.Module.Configuration
{
    public static class ServiceBinding
    {
        public static T Create<T>()
            where T : new()
        {
            // 查找配置文件中是否定义了Binding，没有再创建默认
            // ..

            return (T)(Object)CreateDefaultBinding();
        }

        public static WebHttpBinding CreateDefaultBinding()
        {
            WebHttpBinding binding = new WebHttpBinding();
            binding.Name = "WebHttpBindingConfig";
            binding.TransferMode = TransferMode.Buffered;
            binding.MaxBufferSize = 2147483647;
            binding.UseDefaultWebProxy = false;
            binding.MaxReceivedMessageSize = 2147483647;
            binding.MaxBufferPoolSize = 2147483647;
            binding.ReaderQuotas.MaxStringContentLength = 2147483647;
            binding.ReaderQuotas.MaxDepth = 1024;
            binding.ReaderQuotas.MaxArrayLength = 2147483647;
            binding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            binding.WriteEncoding = Encoding.UTF8;
            binding.AllowCookies = true;

            return binding;
        }
    }
}
