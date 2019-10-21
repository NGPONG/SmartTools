using System.ServiceModel.Description;
using System.ServiceModel.Web;

namespace SmartTools.Service.Module.Configuration
{
    public static class ServiceWebHttpBehaviors
    {
        public static WebHttpBehavior CreateWebHttpBehavior()
        {
            // 查找配置文件中是否定义了Binding，没有再创建默认
            // ..

            return CreateDefaultWebHttpBehavior();
        }

        private static WebHttpBehavior CreateDefaultWebHttpBehavior()
        {
            WebHttpBehavior objWebHttpBehavior = new WebHttpBehavior();
            objWebHttpBehavior.DefaultBodyStyle = WebMessageBodyStyle.WrappedRequest;

            return objWebHttpBehavior;
        }
    }
}
