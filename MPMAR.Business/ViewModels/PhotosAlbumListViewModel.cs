using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.ViewModels
{
    public class PhotosAlbumListViewModel : PageSeo
    {
        public int Id { get; set; }

        public int PhotoArchiveId { get; set; }
        public string EnPhotosAlbumName { get; set; }
        public string ArPhotosAlbumName { get; set; }
        public string EnPhotosAlbumDesc { get; set; }
        public string ArPhotosAlbumDesc { get; set; }
        public string ImageUrl { get; set; }
        public string EnPhotosAlbumType { get; set; }
        public string ArPhotosAlbumType { get; set; }

        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int VerId { get;  set; }
        public VersionStatusEnum VersionStatusEnum { get; set; }
    }
}
