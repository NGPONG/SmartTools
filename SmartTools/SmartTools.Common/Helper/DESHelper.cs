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
        private const string _KEY = "SMARTTOOLS_G";

        public static string Encrypt(string sourceStr)
        {
            MemoryStream memStream;
            CryptoStream crypStream;
            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(_KEY);
                byte[] keyIV = keyBytes;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(sourceStr);
                DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();

                // java 默认的是ECB模式，PKCS5padding；c#默认的CBC模式，PKCS7padding 所以这里我们默认使用ECB方式
                desProvider.Mode = CipherMode.ECB;
                memStream = new MemoryStream();
                crypStream = new CryptoStream(memStream, desProvider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                crypStream.Write(inputByteArray, 0, inputByteArray.Length);
                crypStream.FlushFinalBlock();
                return Convert.ToBase64String(memStream.ToArray(), Base64FormattingOptions.InsertLineBreaks);
            }
            catch
            {
                return sourceStr;
            }
        }
    }
}
