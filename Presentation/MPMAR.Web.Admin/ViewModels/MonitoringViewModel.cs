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
    public class MonitoringViewModel : ActionInfo
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        [Display(Name = "Ar Main Title")]
        public string ArMainTitle { get; set; }
        [Required]
        [MaxLength(500)]
        [Display(Name = "En Main Title")]
        public string EnMainTitle { get; set; }
        public string BackGroundImage { get; set; }
        [Display(Name = "BackGroundImage")]
        public IFormFile BackGroundImageFile { get; set; }
        public string Image1 { get; set; }
        [Required]
        [Display(Name = "Image 1")]
        public IFormFile ImageFile1 { get; set; }
        [MaxLength(800)]
        [Required]
        [Display(Name = "Ar Description 1")]
        public string ArDescription1 { get; set; }
        [MaxLength(800)]
        [Required]
        [Display(Name = "En Description 1")]
        public string EnDescription1 { get; set; }
        [Required]
        public string Link1 { get; set; }
        [MaxLength(200)]
        [Required]
        [Display(Name = "Ar Title 2")]
        public string ArTitle2 { get; set; }
        [MaxLength(200)]
        [Required]
        [Display(Name = "En Title 2")]
        public string EnTitle2 { get; set; }
        [MaxLength(800)]
        [Required]
        [Display(Name = "Ar Description 2")]
        public string ArDescription2 { get; set; }
        [MaxLength(800)]
        [Required]
        [Display(Name = "En Description 2")]
        public string EnDescription2 { get; set; }
        [Required]
        public string Link2 { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public int Order { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? MonitringId { get; set; }
    }
}
