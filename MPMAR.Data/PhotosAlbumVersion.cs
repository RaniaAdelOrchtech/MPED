using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PhotosAlbumVersion table which form PhotosAlbumVersion object used in PhotosAlbum screens
    /// </summary>
    public class PhotosAlbumVersion : PageSeoVersion
    {
       
        public int Id { get; set; }
        public int PhotoArchiveVersionId { get; set; }
        public int? PhotosAlbumId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string EnPhotosAlbumName { get; set; }
        public string ArPhotosAlbumName { get; set; }
        public string EnPhotosAlbumDesc { get; set; }
        public string ArPhotosAlbumDesc { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Date { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }

        public VersionStatusEnum? VersionStatusEnum { get; set; }
        [ForeignKey("PhotoArchiveVersionId")]
        public PhotoArchiveVersion PhotoArchiveVersion { get; set; }
        [ForeignKey("PhotosAlbumId")]
        public PhotosAlbum PhotosAlbum { get; set; }
    }
}