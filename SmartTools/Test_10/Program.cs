using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Test_10
{
    class Program
    {
        static void Main(string[] args)
        {
            var disks = System.IO.DriveInfo.GetDrives();
            string filePath = string.Empty;
            foreach (var disk in disks)
            {
                filePath = ScanFile(disk.Name);
                if (!string.IsNullOrEmpty(filePath))
                    break;
            }
            // "77.0.3865.120"
            var version = FileVersionInfo.GetVersionInfo(filePath)?.FileVersion;
            var versionSplit = version.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (versionSplit.Length > 3)
            {
                string masterVersion = versionSplit[0];     // 77.
                string subVersion1 = versionSplit[1];       // 0.
                string subVersion2 = versionSplit[2];       // 3865.
                string subVersion3 = versionSplit[3];       // 120.

                var availableVersions = new List<string>();
                var threeVersionNumber = $"{masterVersion}.{subVersion1}.{subVersion2}";

                HttpClient httpClient = new HttpClient();
                var versionContent = httpClient.GetAsync("http://chromedriver.storage.googleapis.com/?delimiter=/&prefix=").Result.Content.ReadAsStreamAsync().Result;
                XmlDocument doc = new XmlDocument();
                doc.Load(versionContent);

                var lodes = doc.DocumentElement.ChildNodes.Cast<XmlNode>()
                    .Where(x => x.Name == "CommonPrefixes" 
                                                           && x.ChildNodes[0].InnerText != "icons/" 
                                                           && x.ChildNodes[0].InnerText.Remove(x.ChildNodes[0].InnerText.LastIndexOf(".")) == threeVersionNumber) // Remove unuse elements
                    .Select(n => n.InnerText.Remove(n.InnerText.Length - 1, 1))
                    .OrderBy(o => o.Substring(o.LastIndexOf(".")))
                    .ToList();

                string activeVersion = string.Empty;
                for (int i = 0; i < lodes.Count; i++)
                {
                    int subVersionCompare = Convert.ToInt32(lodes[i].Substring(lodes[i].LastIndexOf(".") + 1));
                    if (i == 0)
                    {
                        // 0 ~ anyversion
                        if (Convert.ToInt32(subVersion3) < subVersionCompare)
                        {
                            activeVersion = lodes[i];
                            break;
                        }
                    }
                    else
                    {
                        int subVersionCompareLast = Convert.ToInt32(lodes[i - 1].Substring(lodes[i - 1].LastIndexOf(".") + 1));

                        if (i == lodes.Count - 1)
                        {
                            activeVersion = lodes[i];
                            break;
                        }

                        // anyversion1 ~ anyversion2
                        if (Convert.ToInt32(subVersion3) > subVersionCompareLast && Convert.ToInt32(subVersion3) < subVersionCompare)
                        {
                            activeVersion = lodes[i];
                            break;
                        }
                    }
                }

                // http://chromedriver.storage.googleapis.com/77.0.3865.40/chromedriver_win32.zip
                var download = httpClient.GetAsync("http://chromedriver.storage.googleapis.com/" + activeVersion + "/chromedriver_win32.zip").Result;

                var stream = download.Content.ReadAsStreamAsync().Result;
                stream.Seek(0, SeekOrigin.Begin);

                using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    // 解压某个文件
                    ZipArchiveEntry entry = archive.GetEntry("chromedriver.exe");
                    using (System.IO.Stream stream1 = entry.Open())
                    {
                        var output = new FileStream(@"C:\Users\NGPONG\Desktop\chromedriver2.exe", FileMode.Create);
                        int b = -1;
                        while ((b = stream1.ReadByte()) != -1)
                        {
                            output.WriteByte((byte)b);
                        }
                        output.Close();
                    }
                }
            }
        }

        static string ScanFile(string path)
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
                    fileName = ScanFile(directory);
                    if (!string.IsNullOrEmpty(fileName))
                        break;
                }

                // Exit if the upper function captures the file name
                if (!string.IsNullOrEmpty(fileName))
                    return fileName;

                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    if (Path.GetFileName(file).Equals("chrome.exe"))
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

        static void scan_dir(string path)
        {
            // Exclude some directories according to their attributes
            string[] files = null;
            string skipReason = null;
            var dirInfo = new DirectoryInfo(path);
            var isroot = dirInfo.Root.FullName.Equals(dirInfo.FullName);
            if (    // as root dirs (e.g. "C:\") apparently have the system + hidden flags set, we must check whether it's a root dir, if it is, we do NOT skip it even though those attributes are present
                    (dirInfo.Attributes.HasFlag(FileAttributes.System) && !isroot)    // We must not access such folders/files, or this crashes with UnauthorizedAccessException on folders like $RECYCLE.BIN
                )
            {
                skipReason = "system file/folder, no access";
            }

            if (null == skipReason)
            {
                try
                {
                    files = Directory.GetFiles(path);
                }
                catch (UnauthorizedAccessException ex)
                {
                    skipReason = ex.Message;
                }
                catch (PathTooLongException ex)
                {
                    skipReason = ex.Message;
                }
            }

            if (null != skipReason)
            {   // perhaps do some error logging, stating skipReason
                return; // we skip this directory
            }

            foreach (var f in files)
            {
                var fileAttribs = new FileInfo(f).Attributes;
                // do stuff with file if the attributes are to your liking
            }

            try
            {
                var dirs = Directory.GetDirectories(path);
                foreach (var d in dirs)
                {
                    scan_dir(d); // recursive call
                }
            }
            catch (PathTooLongException ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
    }
}
