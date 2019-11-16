using System;
using System.IO;

namespace SmartTools.Common.Helper
{
    public class IOHelper
    {
        public static string SearchFile(string fileName)
        {
            string filePath = string.Empty;
            foreach (var disk in DriveInfo.GetDrives())
            {
                filePath = SeachFile(disk.Name, fileName);
                if (!string.IsNullOrEmpty(filePath))
                    break;
            }

            return filePath;
        }

        private static string SeachFile(string path,string name)
        {
            try
            {
                string fileName = string.Empty;

                // Check recycle bin and other unsafe places.
                var dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Root.FullName.Equals(dirInfo.FullName) &&
                    dirInfo.Attributes.HasFlag(FileAttributes.System))
                    return fileName;

                var directorys = Directory.GetDirectories(path);
                foreach (var directory in directorys)
                {
                    fileName = SeachFile(directory, name);
                    if (!string.IsNullOrEmpty(fileName))
                        break;
                }

                // Exit if the upper function captures the file name
                if (!string.IsNullOrEmpty(fileName))
                    return fileName;

                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    if (Path.GetFileName(file).Equals(name))
                    {
                        fileName = file;
                        break;
                    }
                }

                return fileName;
            }
            catch (UnauthorizedAccessException)
            {
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static void SaveToFile(string path, string name, byte[] data)
        {
            string complete = AppDomain.CurrentDomain.BaseDirectory + Path.Combine(path, name);
            CreateIfNotExists(AppDomain.CurrentDomain.BaseDirectory + path);

            try
            {
                using (FileStream streamWriter = new FileStream(complete, FileMode.Create, FileAccess.Write))
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
                return new byte[0];
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
