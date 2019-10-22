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
                   ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string UserLogin(string userName, string userPwd);

        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string AddUserInfo(string userName, string userPwd);

        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string Activation(string userName, int activationLevel);

        [WebInvoke(Method = "GET")]
        [OperationContract]
        Stream Test();
    }
}
