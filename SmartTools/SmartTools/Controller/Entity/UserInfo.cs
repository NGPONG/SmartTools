using Newtonsoft.Json;
using SmartTools.Service.Module.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Controller.Entity
{
    public class UserInfo : Header
    {
        public bool DefaultModel = true;
        public override string ContentType { get => DefaultModel ? base.ContentType : this.ContentType; set => this.ContentType = value; }
        public override string UserAgent { get => DefaultModel ? base.UserAgent : this.UserAgent; set => this.UserAgent = value; }

        public object Login(string parameter)
        {
            try
            {
                Address = $"http://{Global.__SERVERADDRESS}:{Global.__PORT}/UserInfo/Login";
                Method = "POST";
                Parameter = parameter;

                HttpController controller = new HttpController() { header = this };

                CustomMessage message;
                using (Stream stream = controller.Start())
                {
                    StreamReader reader = new StreamReader(stream);
                    message = JsonConvert.DeserializeObject<CustomMessage>(reader.ReadToEnd());
                }
                controller.Close();

                return message;
            }
            catch (Exception objException)
            {
                if (objException.InnerException != null)
                {
                    throw new Exception(objException.InnerException.Message);
                }
                throw new Exception(objException.Message);
            }
        }

        public object Register(string parameter)
        {
            try
            {
                Address = $"http://{Global.__SERVERADDRESS}:{Global.__PORT}/UserInfo/Register";
                Method = "POST";
                Parameter = parameter;

                HttpController controller = new HttpController() { header = this };

                CustomMessage message;
                using (Stream stream = controller.Start())
                {
                    StreamReader reader = new StreamReader(stream);
                    message = JsonConvert.DeserializeObject<CustomMessage>(reader.ReadToEnd());
                }
                controller.Close();

                return message;
            }
            catch (Exception objException)
            {
                if (objException.InnerException != null)
                {
                    throw new Exception(objException.InnerException.Message);
                }
                throw new Exception(objException.Message);
            }
        }
    }
}
