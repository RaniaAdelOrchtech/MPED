using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PhotoArchiveVersion table which form PhotoArchiveVersion object used in PhotoArchive screens
    /// </summary>
    public class PhotoArchiveVersion : PageSeoVersion
    {
        public PhotoArchiveVersion()
        {
            PhotosAlbumVersions = new HashSet<PhotosAlbumVersion>();
        }
        public int Id { get; set; }
        public string EnPhotoArchiveName { get; set; }
        public string ArPhotoArchiveName { get; set; }
        public string EnPhotoArchiveDesc { get; set; }
        public string ArPhotoArchiveDesc { get; set; }
        public string ImageUrl { get; set; }
        public string EnPhotoArchiveType { get; set; }
        public string ArPhotoArchiveType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Date { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }

        public VersionStatusEnum? VersionStatusEnum { get; set; }

        public int? PhotoArchiveId { get; set; }

        [ForeignKey("PhotoArchiveId")]
        public PhotoArchive PhotoArchive { get; set; }
        public int PageRouteVersionId { get; set; }
        [ForeignKey("PageRouteVersionId")]
        public PageRouteVersion PageRouteVersion { get; set; }

        public ICollection<PhotosAlbumVersion> PhotosAlbumVersions { get; set; }
    }
}