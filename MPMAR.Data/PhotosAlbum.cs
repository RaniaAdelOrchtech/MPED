using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PhotosAlbum table which form PhotosAlbum object used in PhotosAlbum screens
    /// </summary>
    public class PhotosAlbum  : PageSeo
    {
        public PhotosAlbum()
        {
            PhotosAlbumVersions = new HashSet<PhotosAlbumVersion>();
        }
        public int Id { get; set; }
        public int PhotoArchiveId { get; set; }
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
        [ForeignKey("PhotoArchiveId")]
        public PhotoArchive PhotoArchive { get; set; }
        public ICollection<PhotosAlbumVersion> PhotosAlbumVersions { get; set; }
    }
}