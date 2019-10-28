using MaterialSkin.Controls;
using Newtonsoft.Json;
using SmartTools.Common.Enum;
using SmartTools.Common.Helper;
using SmartTools.Controller;
using SmartTools.Controller.Entity;
using SmartTools.Service.Module.Entity;
using SmartTools.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartTools.Views
{
    public partial class Login : MaterialForm
    {
        FormController control = new FormController();
        public Login()
        {
            InitializeComponent();
            control.Init(this);
        }

        private void MaterialRaisedButton1_Click_1(object sender, EventArgs e)
        {
            control.ChangeColorScheme();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = this.txtUserName.Text;
                if (string.IsNullOrEmpty(userName)) throw new Exception(I18N.Get("请输入用户名"));

                string userPwd = this.txtUserPwd.Text;
                if (string.IsNullOrEmpty(userPwd)) throw new Exception(I18N.Get("请输入密码"));

                UserInfo userInfoService = new UserInfo();
                CustomMessage resouce = userInfoService.Login(JsonConvert.SerializeObject(new
                {
                    userName,
                    userPwd
                })) as CustomMessage;

                if (resouce.Status != HttpStatus.OK)
                {
                    MessageBoxExt.Show(resouce.Message.ToString(), MessageboxType.Error);
                }
            }
            catch (Exception objException)
            {
                MessageBoxExt.Show(objException.Message, MessageboxType.Error);
            }
        }

        private void BtnRegistered_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = this.txtRegistered_UserName.Text;
                if (string.IsNullOrEmpty(userName)) throw new Exception(I18N.Get("请输入用户名"));

                string userPwd = this.txtRegistered_UserPwd.Text;
                if (string.IsNullOrEmpty(userPwd)) throw new Exception(I18N.Get("请输入密码"));

                string userEmail = this.txtRegistered_Email.Text;
                if (string.IsNullOrEmpty(userPwd)) throw new Exception(I18N.Get("请输入邮箱"));
                if (!Regex.IsMatch(userEmail, @"^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$")) throw new Exception(I18N.Get("请输入正确的邮箱号"));

                UserInfo userInfoService = new UserInfo();
                CustomMessage resouce = userInfoService.Register(JsonConvert.SerializeObject(new
                {
                    userName,
                    userPwd,
                    emailAddress = userEmail
                })) as CustomMessage;

                if (resouce.Status != HttpStatus.OK)
                {
                    MessageBoxExt.Show(resouce.Message.ToString(), MessageboxType.Error);
                }
                else
                {
                    MessageBoxExt.Show(resouce.Message.ToString(), MessageboxType.Info);
                }
            }
            catch (Exception objException)
            {
                MessageBoxExt.Show(objException.Message, MessageboxType.Error);
            }
        }
    }
}
