using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.ViewModels
{
    public class PhotoArchiveListViewModel : PageSeo
    {
        public int Id { get; set; }
        public string EnPhotoArchiveName { get; set; }
        public string ArPhotoArchiveName { get; set; }
        public string EnPhotoArchiveDesc { get; set; }
        public string ArPhotoArchiveDesc { get; set; }
        public int? StatusId { get; set; }
        public string ImageUrl { get; set; }
        public string EnPhotoArchiveType { get; set; }
        public string ArPhotoArchiveType { get; set; }

        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int VerId { get; set; }
        public string CreatedById { get; set; }
        public VersionStatusEnum VersionStatusEnum { get;  set; }
        public ChangeActionEnum ChangeActionEnum { get; set; }
        public string StatusStr { get { return VersionStatusEnum.ToString(); } }
    }
}
