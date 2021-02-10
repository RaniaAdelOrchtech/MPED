using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for HomePageAffiliatesVersions table which form HomePageAffiliatesVersions model used in HomePageAffiliates screen
    /// </summary>
    public class HomePageAffiliatesVersions : ActionInfo
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string ArDescription { get; set; }
        [Required]
        public string EnDescription { get; set; }
        [Required]
        public string Url { get; set; }
        public AffiliatesType Type { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? HomePageAffiliatesId { get; set; }
        public HomePageAffiliates HomePageAffiliates { get; set; }
    }
}
