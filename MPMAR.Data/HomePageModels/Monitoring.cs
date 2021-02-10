using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for Monitoring table which form Monitoring model used in Monitoring screen
    /// </summary>
    public class Monitoring : ActionInfo
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
        [Required]
        public string Image1 { get; set; }
        [MaxLength(800)]
        [Required]
        [Display(Name = "Ar Description 1")]
        public string ArDescription1 { get; set; }
        [MaxLength(800)]
        [Required]
        [Display(Name = "En Description 1")]
        public string EnDescription1 { get; set; }
        [Required]
        [Url]
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
        [Url]
        public string Link2 { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

    }
}
