using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for HomePagePhotoSliderVersions table which form HomePagePhotoSliderVersions model used in HomePagePhotoSlider screen
    /// </summary>
    public class HomePagePhotoSliderVersion : ActionInfo
    {
        public int Id { get; set; }
        public string ArTitle { get; set; }
        public string EnTitle { get; set; }
        public string ArDescription { get; set; }
        public string EnDescription { get; set; }
        public string Url { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? HomePagePhotoSliderId { get; set; }
        public HomePagePhotoSlider HomePagePhotoSlider { get; set; }
    }
}
