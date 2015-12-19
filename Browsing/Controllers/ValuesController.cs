using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Browsing.Models;

namespace Browsing.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        DirectoryEntries de = new DirectoryEntries();
        public DirectoryEntries Get()
        {
            de.DirectoriesPath = Directory.GetLogicalDrives();
            de.DirectoriesName = Directory.GetLogicalDrives();
            de.FilesCountLess10Mb = 0;
            de.FilesCount10_50Mb = 0;
            de.FilesCountMore50Mb = 0;
            
            return de;
        }

        public DirectoryEntries Post(string path)
        {
            try
            {
                de.DirectoriesPath = Directory.GetDirectories(path);
                de.DirectoriesName = Directory.GetDirectories(path).Select(x => Path.GetFileName(x)).ToArray();
                de.FilesName = Directory.GetFiles(path).Select(x => Path.GetFileName(x)).ToArray();
                if (Directory.GetParent(path) != null)
                {
                    de.ParentDirectory = Directory.GetParent(path).FullName;
                    de.ShowParentDirectory = true;
                    de.ShowLogicalDrives = false;
                }
                else
                {
                    de.ShowLogicalDrives = true;
                    de.ShowParentDirectory = false;
                    de.FilesCountLess10Mb = 0;
                    de.FilesCount10_50Mb = 0;
                    de.FilesCountMore50Mb = 0;
                }


                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                WalkDirectoryTree(directoryInfo);

                return de;
            }
            catch (UnauthorizedAccessException e)
            {
                return null;

            }
            catch (DirectoryNotFoundException e)
            {
                return null;
            }
            catch (IOException e)
            {
                return null;
            }


        }

        public void WalkDirectoryTree(DirectoryInfo root)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            try
            {
                subDirs = root.GetDirectories();
                files = root.GetFiles("*.*");
               
            }
            catch(UnauthorizedAccessException e)
            {
            
            }
            catch (System.IO.DirectoryNotFoundException e)
            {

            }

            if (files != null)
            {
                    foreach (FileInfo file in files)
                    {
                        if (file.Length <= 1048576 * 10)
                        { de.FilesCountLess10Mb++; }
                        else if (file.Length > 1048576 * 10 && file.Length <= 1048576 * 50)
                        { de.FilesCount10_50Mb++; }
                        else if (file.Length >= 1048576 * 100)
                        { de.FilesCountMore50Mb++; }
                    }
            }
            if (subDirs != null)
            {
                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    WalkDirectoryTree(dirInfo);
                }
            }
        }
        
    }
}