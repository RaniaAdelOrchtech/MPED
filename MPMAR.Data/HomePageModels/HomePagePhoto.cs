using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for HomePagePhoto table which form HomePagePhoto model used in HomePagePhoto screen
    /// </summary>
    public class HomePagePhoto : ActionInfo
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
    }
}
