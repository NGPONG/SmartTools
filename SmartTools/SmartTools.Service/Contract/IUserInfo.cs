using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace SmartTools.Service.Contract
{
    [ServiceContract]
    public interface IUserInfo
    {
        [WebInvoke(Method = "GET",
                   BodyStyle = WebMessageBodyStyle.Wrapped,
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string UserLogin(string userName, string userPwd);

        [WebInvoke(Method = "GET")]
        [OperationContract]
        Stream Test();
    }
}
