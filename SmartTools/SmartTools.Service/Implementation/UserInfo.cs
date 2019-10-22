using Newtonsoft.Json;
using SmartTools.Common.Helper;
using SmartTools.Service.Contract;
using SmartTools.Service.Data;
using SmartTools.Service.Module.Entity;
using System;
using System.IO;
using System.Net.Mime;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Linq;

namespace SmartTools.Service.Implementation
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
            var message = new CustomMessage();

            try
            {
                var dbContext = DbContainer.GetDbContext();

                var query = (from u in dbContext.UserInfo
                             where u.UserName == userName && userPwd == MD5Helper.Encry(userPwd)
                             select u).ToList();

                if (query.Count() == 0)
                {
                    message.Status = HttpStatus.Error;
                    message.Message = "用户名或密码错误！";
                }
                else
                {
                    Data.UserInfo user = query[0];

                    if (user.IsActivation != 0)
                    {
                        message.Status = HttpStatus.Error;
                        message.Message = "当前账户还未激活！";
                    }
                    else
                    {
                        // 计算剩余时间

                    }
                }
            }
            catch (Exception objException)
            {
                LogHelper.Error(objException);
                message.Status = HttpStatus.Error;
                message.Message = objException.Message;
            }

            return JsonConvert.SerializeObject(message);
        }
    }
}
