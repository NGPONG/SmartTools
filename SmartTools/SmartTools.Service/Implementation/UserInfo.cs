using Newtonsoft.Json;
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
using SmartTools.Common.Helper;
using SmartTools.Service.Utils;
using SmartTools.Common.Enum;

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

                userPwd = MD5Helper.Encry(userPwd);
                var query = (from u in dbContext.UserInfo
                             where u.UserName == userName && u.UserPwd == userPwd
                             select u).ToList();

                if (query.Count() == 0)
                {
                    message.Status = HttpStatus.Error;
                    message.Message = "用户名或密码错误！";
                }
                else
                {
                    Data.UserInfo user = query[0];

                    if (!(bool)user.IsActivation)
                    {
                        message.Status = HttpStatus.Error;
                        message.Message = "当前账户还未激活！";
                    }
                    else
                    {
                        // 计算剩余时间
                        var remainingSeconds = (DateTime.Now - ConvertExtensions.ToTimeSpan((int)user.ActivationLevel, (DateTime)user.ActivationDate)).TotalSeconds;

                        message.Status = HttpStatus.OK;
                        message.Message = JsonConvert.SerializeObject(new
                        {
                            RemainingSeconds = remainingSeconds,
                            Message = "登陆成功！"
                        });
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

        public string AddUserInfo(string userName, string userPwd)
        {
            var message = new CustomMessage();

            try
            {
                var dbContext = DbContainer.GetDbContext();

                var query = from u in dbContext.UserInfo
                            where u.UserName == userName
                            select u;

                if (query.Count() > 0)
                {
                    message.Status = HttpStatus.Error;
                    message.Message = "用户名已存在!";
                }
                else
                {
                    var user = new Data.UserInfo()
                    {
                        UserName = userName,
                        UserPwd = MD5Helper.Encry(userPwd),
                        CreateDate = DateTime.Now
                    };

                    dbContext.UserInfo.Add(user);
                    dbContext.SaveChanges();

                    message.Status = HttpStatus.OK;
                    message.Message = "用户注册成功!请及时激活";
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

        public string Activation(string userName, int activationLevel)
        {
            var message = new CustomMessage();

            try
            {
                var dbContext = DbContainer.GetDbContext();
                dbContext.Configuration.ValidateOnSaveEnabled = false;

                var user = new Data.UserInfo()
                {
                    UserId = 2
                };

                var userItem = (from u in dbContext.UserInfo
                                where u.UserName == userName
                                select u).FirstOrDefault();

                userItem.IsActivation = true;
                userItem.ActivationLevel = activationLevel;
                userItem.ActivationDate = DateTime.Now;

                if (dbContext.SaveChanges() <= 0)
                {
                    message.Status = HttpStatus.Error;
                    message.Message = "用户名错误！";
                }

                message.Status = HttpStatus.OK;
                message.Message = $"账号激活成功!到期日期为{ConvertExtensions.ToActivationDate(activationLevel)}后";
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
