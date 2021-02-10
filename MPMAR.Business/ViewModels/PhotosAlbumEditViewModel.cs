using Microsoft.AspNetCore.Http;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Business.ViewModels
{
    public class PhotosAlbumEditViewModel : PageSeo
    {
        public int Id { get; set; }

        public int PhotoArchiveId { get; set; }
        public int PhotoArchiveVersionId { get; set; }
        public int PageRouteId { get; set; }
        public string EnPhotosAlbumName { get; set; }
        public string ArPhotosAlbumName { get; set; }
        public string EnPhotosAlbumDesc { get; set; }
        public string ArPhotosAlbumDesc { get; set; }
        public int? StatusId { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }
        public string EnPhotosAlbumType { get; set; }
        public string ArPhotosAlbumType { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int PhotoAlbumId { get;  set; }
    }


   
}
