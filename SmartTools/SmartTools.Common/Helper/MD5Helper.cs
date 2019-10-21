using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Common.Helper
{
    public static class MD5Helper
    {
        public static string Encry(string str)
        {
            try
            {
                StringBuilder sbResult = new StringBuilder();
                byte[] buffer = Encoding.Default.GetBytes(str);
                MD5 md5 = MD5.Create();
                byte[] buffer_MD5 = md5.ComputeHash(buffer);

                for (int i = 0; i < buffer_MD5.Length; i++)
                {
                    sbResult.Append(buffer_MD5[i].ToString("X2"));
                }
                return sbResult.ToString();
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
