using MaterialSkin.Controls;
using Newtonsoft.Json;
using SmartTools.Common.Enum;
using SmartTools.Common.Helper;
using SmartTools.Controller;
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

                #region login
                HttpController webContext = new HttpController()
                {
                    Address = "http://127.0.0.1:9876/UserInfo/Login",
                    Method = "POST",
                    ContentType = "application/json",
                    Parameter = JsonConvert.SerializeObject(new
                    {
                        userName = userName,
                        userPwd = userPwd
                    })
                };
                CustomMessage resouce;
                using (Stream stream = webContext.Start())
                {
                    StreamReader reader = new StreamReader(stream);
                    resouce = JsonConvert.DeserializeObject<CustomMessage>(reader.ReadToEnd());
                }
                #endregion

                if (resouce.Status != HttpStatus.OK)
                {
                    MessageBox.Show(resouce.Message.ToString());
                }
            }
            catch (Exception objException)
            {
                MessageBox.Show(objException.Message);
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

                #region Registered
                HttpController webContext = new HttpController()
                {
                    Address = "http://127.0.0.1:9876/UserInfo/Register",
                    Method = "POST",
                    ContentType = "application/json",
                    Parameter = JsonConvert.SerializeObject(new
                    {
                        userName = userName,
                        userPwd = userPwd,
                        emailAddress = userEmail
                    })
                };
                CustomMessage resouce;
                using (Stream stream = webContext.Start())
                {
                    StreamReader reader = new StreamReader(stream);
                    resouce = JsonConvert.DeserializeObject<CustomMessage>(reader.ReadToEnd());
                }
                webContext.Close();
                #endregion

                if (resouce.Status != HttpStatus.OK)
                {
                    MessageBox.Show(resouce.Message.ToString());
                }
                else
                {
                    MessageBox.Show(resouce.Message.ToString());
                }
            }
            catch (Exception objException)
            {
                MessageBox.Show(objException.Message);
            }
        }
    }
}
