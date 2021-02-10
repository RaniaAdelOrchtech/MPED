using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for HomePageAffiliates table which form HomePageAffiliates model used in HomePageAffiliates screen
    /// </summary>
    public class HomePageAffiliates
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
    }

    public enum AffiliatesType
    {
        Title = 1,
        Image = 2
    }
}
