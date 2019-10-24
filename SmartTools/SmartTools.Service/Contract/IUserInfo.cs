using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace SmartTools.Service.Contract
{
    [ServiceContract]
    public interface IUserInfo
    {
        [WebInvoke(Method = "POST",
                   BodyStyle = WebMessageBodyStyle.WrappedRequest,
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/Login")]
        [OperationContract]
        string UserLogin(string userName, string userPwd);

        [WebInvoke(Method = "POST",
                   BodyStyle = WebMessageBodyStyle.WrappedRequest,
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/Register")]
        [OperationContract]
        string AddUserInfo(string userName, string userPwd, string emailAddress);

        [WebInvoke(Method = "POST",
                   BodyStyle = WebMessageBodyStyle.WrappedRequest,
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json,
                   UriTemplate = "/Activation")]
        [OperationContract]
        string ActivationUser(string activationCode);

        [WebInvoke(Method = "GET")]
        [OperationContract]
        Stream Test();
    }
}
