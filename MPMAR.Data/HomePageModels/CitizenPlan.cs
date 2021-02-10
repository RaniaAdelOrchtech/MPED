using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for HomePageCitizenPlan table which form HomePageCitizenPlan model used in HomePageCitizenPlan screen
    /// </summary>
    public class CitizenPlan : ActionInfo
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
    }
}
