using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for HomePageEconomicDevelopment table which form HomePageEconomicDevelopment model used in HomePageEconomicDevelopment screen
    /// </summary>
    public class EconomicDevelopment : ActionInfo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string ArMainTitle { get; set; }
        [Required]
        [MaxLength(500)]
        public string EnMainTitle { get; set; }

        [Required]
        [MaxLength(200)]
        public string ArTitle1 { get; set; }
        [Required]
        [MaxLength(200)]
        public string EnTitle1 { get; set; }

        [Required]
        [MaxLength(800)]
        public string ArDescription1 { get; set; }
        [Required]
        [MaxLength(800)]
        public string EnDescription1 { get; set; }

        public string BackGroundImage { get; set; }

        [Required]
        [Url]

        public string Url1 { get; set; }

        [Required]
        [MaxLength(200)]
        public string ArTitle2 { get; set; }
        [Required]
        [MaxLength(200)]
        public string EnTitle2 { get; set; }

        [Required]
        [MaxLength(800)]
        public string ArDescription2 { get; set; }
        [Required]
        [MaxLength(800)]
        public string EnDescription2 { get; set; }
        [Required]
        [Url]

        public string Url2 { get; set; }

        [Required]
        [MaxLength(200)]
        public string ArTitle3 { get; set; }
        [Required]
        [MaxLength(200)]
        public string EnTitle3 { get; set; }

        [Required]
        [MaxLength(800)]
        public string ArDescription3 { get; set; }
        [Required]
        [MaxLength(800)]
        public string EnDescription3 { get; set; }
        [Required]
        [Url]

        public string Url3 { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
