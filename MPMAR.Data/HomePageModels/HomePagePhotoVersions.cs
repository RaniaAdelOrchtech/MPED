using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for HomePagePhotoVersions table which form HomePagePhotoVersions model used in HomePagePhoto screen
    /// </summary>
    public class HomePagePhotoVersions : ActionInfo
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string ArTitle { get; set; }
        public string ArDescription { get; set; }
        public string EnTitle { get; set; }
        public string EnDescription { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? HomePagePhotoId { get; set; }
        public HomePagePhoto HomePagePhoto { get; set; }

    }
}
