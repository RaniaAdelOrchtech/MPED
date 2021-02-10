using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for SocialMediaVersion table which form SocialMediaVersion object used in SocialMedia screens
    /// </summary>
    public class SocialMediaVersion : ActionInfo
    {
        public int Id { get; set; }
        public string SocialMediaName { get; set; }
        public string Link { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? SocialMediaId { get; set; }
        public SocialMedia SocialMedia { get; set; }

    }
}
