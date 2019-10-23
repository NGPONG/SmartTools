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
        public static string Encrypt(string source,string _key)
        {
            MemoryStream output = null;
            CryptoStream crypStream = null;
            try
            {
                output = new MemoryStream();

                byte[] rbgKey = Encoding.UTF8.GetBytes(_key), rbgIv = rbgKey;
                byte[] input = Encoding.UTF8.GetBytes(source);

                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                provider.Mode = CipherMode.ECB; // java 默认的是ECB模式 -> PKCS5padding  c#默认的CBC模式 -> PKCS7padding

                crypStream = new CryptoStream(output, provider.CreateEncryptor(rbgKey, rbgIv), CryptoStreamMode.Write);
                crypStream.Write(input, 0, input.Length);
                crypStream.FlushFinalBlock();

                return Convert.ToBase64String(output.ToArray(), Base64FormattingOptions.InsertLineBreaks);
            }
            catch(Exception objException)
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
                byte[] input = new byte[toDecrypt.Length / 2];
                for (int x = 0; x < toDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(toDecrypt.Substring(x * 2, 2), 16));
                    input[x] = (byte)i;
                }

                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                provider.Key = ASCIIEncoding.ASCII.GetBytes(_key);
                provider.IV = provider.Key;

                output = new MemoryStream();
                crypStream = new CryptoStream(output, provider.CreateDecryptor(), CryptoStreamMode.Write);
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
