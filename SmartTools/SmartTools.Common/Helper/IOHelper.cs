using System;
using System.IO;

namespace SmartTools.Common.Helper
{
    public class IOHelper
    {
        public static void SaveToFile(string path, string name, byte[] data)
        {
            string complete = AppDomain.CurrentDomain.BaseDirectory + Path.Combine(path, name);
            CreateIfNotExists(AppDomain.CurrentDomain.BaseDirectory + path);

            try
            {
                using (FileStream streamWriter = new FileStream(complete, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    streamWriter.Write(data, 0, data.Length);
                }
            }
            catch (Exception objException)
            {
                LogHelper.Error(objException);
            }
        }

        public static byte[] ReadToFile(string path, string name)
        {
            try
            {
                byte[] destination;
                int offset_Dst = 0;

                string complete = AppDomain.CurrentDomain.BaseDirectory + Path.Combine(path, name);
                using (FileStream streamReader = new FileStream(complete, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    destination = new byte[streamReader.Length];

                    byte[] source = new byte[1024];
                    while (true)
                    {
                        int offset_Source = streamReader.Read(source, 0, source.Length);
                        if (offset_Source == 0)
                            break;

                        Buffer.BlockCopy(source, 0, destination, offset_Dst, offset_Source);
                        Array.Clear(source, 0, offset_Source);
                        offset_Dst += offset_Source;
                    }
                }

                return destination;
            }
            catch(Exception objException)
            {
                LogHelper.Error(objException);
                return null;
            }
        }

        public static void CreateIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
