using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for HomePagePhotoSlider table which form HomePagePhotoSlider model used in HomePagePhotoSlider screen
    /// </summary>
    public class HomePagePhotoSlider
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
    }
}
