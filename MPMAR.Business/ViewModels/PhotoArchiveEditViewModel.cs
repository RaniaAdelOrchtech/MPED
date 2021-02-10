using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Business.ViewModels
{
    public class PhotoArchiveEditViewModel : PageSeo
    {
        public int Id { get; set; }
        public string EnPhotoArchiveName { get; set; }
        public string ArPhotoArchiveName { get; set; }
        public string EnPhotoArchiveDesc { get; set; }
        public string ArPhotoArchiveDesc { get; set; }
        public int PageRouteId { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }
        [Required]
        public string EnPhotoArchiveType { get; set; }
        [Required]
        public string ArPhotoArchiveType { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }


        public IEnumerable<SelectListItem> PhotoArchiveType { get; set; }
        public int PhotoArchiveId { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }

        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int PageRouteVersionId { get; set; }
    }

    public class PhotoArchiveType
    {
        public string EnPhotoArchiveType { get; set; }
        public string ArPhotoArchiveType { get; set; }
    }

   
}
