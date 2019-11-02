using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTools.Common.Helper
{
    public class IOHelper
    {
        public static void SaveToFile(string path, string name, byte[] data)
        {
            CreateIfNotExists(path, name);
            string complete = Path.Combine(path, name);

            try
            {
                using (FileStream streamWriter = new FileStream(complete, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    streamWriter.Write(data, 0, data.Length);
                }
            }
            catch
            {
                throw;
            }
        }

        public static byte[] ReadToFile(string path, string name)
        {
            try
            {
                byte[] destination;
                int offset_Dst = 0;

                string complete = Path.Combine(path, name);
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
            catch
            {
                throw;
            }
        }

        public static void CreateIfNotExists(string path, string name)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(Path.Combine(path, name));
            }
        }
    }
}
