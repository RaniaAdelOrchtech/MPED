using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for HomePageLogoLinkVersions table which form HomePageLogoLinkVersions model used in HomePageLogoLink screen
    /// </summary>
    public class HomePageLogoLinkVersions : ActionInfo
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string ArTitle { get; set; }
        public string EnTitle { get; set; }
        public string Url { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? HomePageLogoLinkId { get; set; }
        public HomePageLogoLink HomePageLogoLink { get; set; }

    }
}
