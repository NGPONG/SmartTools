using Newtonsoft.Json;
using SmartTools.Controller;
using SmartTools.Service.Module.Entity;
using System;
using System.IO;

namespace SmartTools.Model
{
    public class UserInfo : Header
    {
        public bool DefaultModel = true;

        public object Login(string parameter)
        {
            try
            {
                Address = $"http://{Global.__SERVERADDRESS}:{Global.__PORT}/UserInfo/Login";
                Method = Method.POST;
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
                Method = Method.POST;
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
