using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for HomePageLogoLink table which form HomePageLogoLink model used in HomePageLogoLink screen
    /// </summary>
    public class HomePageLogoLink : ActionInfo
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string ArTitle { get; set; }
        public string EnTitle { get; set; }
        public string Url { get; set; }
    }
}
