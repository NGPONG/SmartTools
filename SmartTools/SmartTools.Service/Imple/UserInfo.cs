using SmartTools.Service.Contract;
using System;
using System.IO;
using System.Net.Mime;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace SmartTools.Service.Imple
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UserInfo : IUserInfo
    {
        public Stream Test()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "attachment; filename=qwd.html");
            WebOperationContext.Current.OutgoingResponse.ContentType = MediaTypeNames.Application.Octet;

            var stream = new FileStream(@"C:\Users\acer\Desktop\qwd.html", FileMode.Open, FileAccess.Read);
            return stream;
        }

        public string UserLogin(string userName, string userPwd)
        {
            return "Test";
        }
    }
}
