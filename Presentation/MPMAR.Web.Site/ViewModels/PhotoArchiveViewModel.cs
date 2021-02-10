using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewModels
{
    public class PhotoArchiveViewModel
    {
        public List<PhotoArchive> PhotoArchives { get; set; }
        public List<PhotoArchiveType> PhotoArchiveTypes { get; set; }

    }
    public class PhotoArchiveType
    {
        public string EnName { get; set; }
        public string ArName { get; set; }
    } 
}
