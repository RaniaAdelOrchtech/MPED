using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PhotoArchive table which form PhotoArchive object used in PhotoArchive screens
    /// </summary>
    public class PhotoArchive : PageSeo
    {
        public PhotoArchive()
        {
            PhotosAlbums = new HashSet<PhotosAlbum>();
            PhotoArchiveVersions = new HashSet<PhotoArchiveVersion>();
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
        public ICollection<PhotoArchiveVersion> PhotoArchiveVersions { get; set; }

        public int? PageRouteId { get; set; }
        [ForeignKey("PageRouteId")]
        public PageRoute PageRoute { get; set; }


        public ICollection<PhotosAlbum> PhotosAlbums { get; set; }
    }
}