using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MPMAR.Web.Admin.ViewModels
{
    public class SectionCardCreateViewModel
    {
        public int SectionVersionId { get; set; }

        [Required]
        public string EnTitle { get; set; }

        [Required]
        public string ArTitle { get; set; }

        [Required]
        [AllowHtml]
        public string EnDescription { get; set; }

        [Required]
        [AllowHtml]
        public string ArDescription { get; set; }

        public string EnImageAlt { get; set; }

        [Required]
        public string ArImageAlt { get; set; }

        [Required]
        public IFormFile Photo { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public IFormFile File { get; set; }
        public string FileUrl { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter an integer number greater than ZERO")]
        public int? Order { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int PageRouteVersionId { get; set; }
    }
}
