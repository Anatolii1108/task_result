using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Browsing.Models
{
    public class DirectoryEntries
    {
        public string [] DirectoriesPath { get; set; }
        public string[] DirectoriesName{ get; set; }
        public string[] FilesName { get; set; }
        public string ParentDirectory { get; set; }
        public bool ShowParentDirectory { get; set; }
        public bool ShowLogicalDrives { get; set; }
        public int FilesCountLess10Mb { get; set; }
        public int FilesCount10_50Mb { get; set; }
        public int FilesCountMore50Mb { get; set; }

    }
}