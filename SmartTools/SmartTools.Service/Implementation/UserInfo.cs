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

        public string AddUserInfo(string userName, string userPwd, string emailAddress)
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
                        EmailAddress = emailAddress,
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

        public string ActivationUser(string activationCode)
        {
            var message = new CustomMessage();

            try
            {
                var dbContext = DbContainer.GetDbContext();
                dbContext.Configuration.ValidateOnSaveEnabled = false;

                // 查询激活码的有效性
                var activationInfo = dbContext.Sys_Activation.Where(c => c.ActivationCode == activationCode).FirstOrDefault();
                if (activationInfo == null)
                {
                    throw new Exception("激活码不存在或已过期!");
                }

                string source = DESHelper.Decrypt(activationCode, Licensing.__KEY);
                if (string.IsNullOrEmpty(source))
                {
                    throw new Exception("激活码有误！请重新输入");
                }

                // 解码后的信息，包含用户名还有激活等级
                dynamic info = JsonConvert.DeserializeObject<dynamic>(source);
                string userName = info.UserName;
                int activationLevel = info.ActivationLevel;

                // 修改用户激活数据
                var userItem = (from u in dbContext.UserInfo
                                where u.UserName == userName
                                select u).FirstOrDefault();
                userItem.IsActivation = true;
                userItem.ActivationLevel = activationLevel;
                userItem.ActivationDate = DateTime.Now;

                // 删除掉临时保存的激活码
                dbContext.Sys_Activation.Remove(activationInfo);

                if (dbContext.SaveChanges() == 0)
                {
                    message.Status = HttpStatus.Error;
                    message.Message = "修改错误";
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
