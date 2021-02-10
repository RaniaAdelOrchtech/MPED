using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for HomePageCitizenPlanVersions table which form HomePageCitizenPlanVersions model used in HomePageCitizenPlan screen
    /// </summary>
    public class CitizenPlanVersions : ActionInfo
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
        [Display(Name = "Ar Title")]
        public string ArTitle { get; set; }
        [MaxLength(100)]
        [Display(Name = "En Title")]
        public string EnTitle { get; set; }
        [Required]
        [MaxLength(1000)]
        [Display(Name = "Ar Description")]
        public string ArDescription { get; set; }
        [Required]
        [MaxLength(1000)]
        [Display(Name = "En Description")]
        public string EnDescription { get; set; }
        [Url]
        public string Link { get; set; }
        public string Image { get; set; }
        public string EnImage { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? CitizenPlanId { get; set; }
        public CitizenPlan CitizenPlan { get; set; }
    }
}
