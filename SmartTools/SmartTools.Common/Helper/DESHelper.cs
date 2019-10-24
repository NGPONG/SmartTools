using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Common.Helper
{
    public static class DESHelper
    {
        public static string Encrypt(string source, string _key)
        {
            MemoryStream output = null;
            CryptoStream crypStream = null;
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                provider.Key = ASCIIEncoding.ASCII.GetBytes(_key);
                provider.IV = ASCIIEncoding.ASCII.GetBytes(_key);

                output = new MemoryStream();
                crypStream = new CryptoStream(output, provider.CreateEncryptor(), CryptoStreamMode.Write);
                byte[] input = Encoding.GetEncoding("UTF-8").GetBytes(source);
                crypStream.Write(input, 0, input.Length);
                crypStream.FlushFinalBlock();

                StringBuilder ret = new StringBuilder();
                foreach (byte b in output.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }

                return ret.ToString();
            }
            catch (Exception objException)
            {
                LogHelper.Error(objException);
                return string.Empty;
            }
            finally
            {
                output.Close(); output.Dispose();
                crypStream.Close(); crypStream.Dispose();
            }
        }

        public static string Decrypt(string toDecrypt, string _key)
        {
            MemoryStream output = null;
            CryptoStream crypStream = null;

            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                provider.Key = ASCIIEncoding.ASCII.GetBytes(_key);
                provider.IV = ASCIIEncoding.ASCII.GetBytes(_key);

                output = new MemoryStream();
                crypStream = new CryptoStream(output, provider.CreateDecryptor(), CryptoStreamMode.Write);
                byte[] input = new byte[toDecrypt.Length / 2];
                for (int x = 0; x < toDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(toDecrypt.Substring(x * 2, 2), 16));
                    input[x] = (byte)i;
                }
                crypStream.Write(input, 0, input.Length);
                crypStream.FlushFinalBlock();

                return System.Text.Encoding.UTF8.GetString(output.ToArray());
            }
            catch (Exception objException)
            {
                LogHelper.Error(objException);
                return string.Empty;
            }
            finally
            {
                output.Close(); output.Dispose();
                crypStream.Close(); crypStream.Dispose();
            }
        }
    }
}
