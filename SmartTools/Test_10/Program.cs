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
            var a = 1;
            if (a % 1 > 0)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("NO");
            }

            Console.ReadKey();
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
