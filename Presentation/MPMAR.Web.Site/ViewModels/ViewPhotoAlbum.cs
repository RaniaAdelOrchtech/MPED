using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewModels
{
    public class ViewPhotoAlbum
    {
       public List<PhotosAlbum> PhotosAlbums { get; set; }
        public string PhotoArchiveArDetails { get; set; }
        public string PhotoArchiveEnDetails { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string PhotoArchiveEnName { get;  set; }
        public string PhotoArchiveArName { get;  set; }
    }
   
}
