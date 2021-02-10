using Microsoft.AspNetCore.Http;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class PublicationViewModel : ActionInfo
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Ar Main Title")]
        public string ArMainTitle { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "En Main Title")]
        public string EnMainTitle { get; set; }
        [MaxLength(100)]
        [Display(Name = "Ar Title 1")]
        public string ArTitle1 { get; set; }
        [MaxLength(100)]
        [Display(Name = "En Title 1")]
        public string EnTitle1 { get; set; }
        [MaxLength(100)]
        [Display(Name = "Ar Title 2")]
        public string ArTitle2 { get; set; }
        [MaxLength(100)]
        [Display(Name = "En Title 2")]
        public string EnTitle2 { get; set; }
        [MaxLength(100)]
        [Display(Name = "Ar Title 3")]
        public string ArTitle3 { get; set; }
        [MaxLength(100)]
        [Display(Name = "En Title 3")]
        public string EnTitle3 { get; set; }
        [MaxLength(500)]
        [Display(Name = "Ar Description 1")]
        public string ArDescription1 { get; set; }
        [MaxLength(500)]
        [Display(Name = "En Description 1")]
        public string EnDescription1 { get; set; }
        [MaxLength(500)]
        [Display(Name = "Ar Description 2")]
        public string ArDescription2 { get; set; }
        [MaxLength(500)]
        [Display(Name = "En Description 2")]
        public string EnDescription2 { get; set; }
        [MaxLength(500)]
        [Display(Name = "Ar Description 3")]
        public string ArDescription3 { get; set; }
        [MaxLength(500)]
        [Display(Name = "En Description 3")]
        public string EnDescription3 { get; set; }
        public string Image1 { get; set; }
        [Required]
        [Display(Name = "Image 1")]
        public IFormFile ImageFile1 { get; set; }
        public string Image2 { get; set; }
        [Required]
        [Display(Name = "Image 2")]
        public IFormFile ImageFile2 { get; set; }
        public string Image3 { get; set; }
        [Required]
        [Display(Name = "Image 3")]
        public IFormFile ImageFile3 { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        [Required]
        public string Link1 { get; set; }
        [Required]
        public string Link2 { get; set; }
        [Required]
        public string Link3 { get; set; }
        public int Order { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? PublicationId { get; set; }
    }
}
