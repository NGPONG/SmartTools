using SmartTools.Service.Module.Entity;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace SmartTools.Service.Contract
{
    [ServiceContract]
    public interface IUserInfo
    {
        [WebInvoke(Method = "POST",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/Login")]
        [OperationContract]
        CustomMessage UserLogin(string userName, string userPwd);

        [WebInvoke(Method = "POST",
                   BodyStyle = WebMessageBodyStyle.WrappedRequest,
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/Register")]
        [OperationContract]
        CustomMessage AddUserInfo(string userName, string userPwd, string emailAddress);

        [WebInvoke(Method = "POST",
                   BodyStyle = WebMessageBodyStyle.WrappedRequest,
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/Activation")]
        [OperationContract]
        CustomMessage ActivationUser(string activationCode);

        [WebInvoke(Method = "GET")]
        [OperationContract]
        Stream Test();
    }
}
