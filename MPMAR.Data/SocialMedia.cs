using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for SocialMedia table which form SocialMedia object used in SocialMedia screens
    /// </summary>
    public class SocialMedia : ActionInfo
    {
        public int Id { get; set; }
        public string SocialMediaName { get; set; }
        public string Link { get; set; }
      
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}