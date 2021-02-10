using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Business.ViewModels
{
    public class SectionViewModel
    {
        public int Id { get; set; }

 
        [Display(Name = "English Title")]
        public string EnTitle { get; set; }

 
        [Display(Name = "Arabic Title")]
        public string ArTitle { get; set; }

        [Display(Name = "English Description")]
        public string EnDescription { get; set; }

        [Display(Name = "Arabic Description")]
        public string ArDescription { get; set; }

        [Required]
        [Display(Name = "English Image Alt")]
        public string EnImageAlt { get; set; }

        [Required]
        [Display(Name = "Arabic Image Alt")]
        public string ArImageAlt { get; set; }

        [Required]
        [Data.CustomDataAnnotation.Url]
        public string Url { get; set; }

        public IFormFile Photo { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter an integer number greater than ZERO")]
        public int? Order { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public int? SectionTypeId { get; set; }
        public string MediaType { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreatedById { get; set; }
        public int PageSectionId { get; internal set; }
        public bool IsVersion { get; internal set; }
        public int PageRouteVersionId { get; internal set; }
    }
}
