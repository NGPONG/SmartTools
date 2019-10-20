using SmartTools.Service.Contract;
using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace SmartTools.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UserInfo : IUserInfo
    {
        public string UserLogin(string userName, string userPwd)
        {
            return "Test";
        }
    }
}
