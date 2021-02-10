using Microsoft.AspNetCore.Http;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels.ViewModels
{
    public class MinistrtVisionViewModel : ActionInfo
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Ar Title")]
        public string ArTitle { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "En Title")]
        public string EnTitle { get; set; }
        public string BackGroundImage { get; set; }
        [Display(Name = "BackGroundImage")]
        public IFormFile BackGroundImageFile { get; set; }

        [Required]
        [MaxLength(1000)]
        [Display(Name = "Ar Description")]
        public string ArDescription { get; set; }
        [Required]
        [MaxLength(1000)]
        [Display(Name = "En Description")]
        public string EnDescription { get; set; }
        public string Link { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? MinistrtVisionId { get; set; }
    }
}
