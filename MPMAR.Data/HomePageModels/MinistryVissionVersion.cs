﻿using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for MinistryVissionVersion table which form MinistryVissionVersion model used in MinistryVission screen
    /// </summary>
    public class MinistryVissionVersion : ActionInfo
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Ar Title")]
        public string ArTitle { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "En Title")]
        public string EnTitle { get; set; }

        public string BackGroundImage { get; set; }

        [Required]
        [MaxLength(1000)]
        [Display(Name = "Ar Description")]
        public string ArDescription { get; set; }
        [Required]
        [MaxLength(1000)]
        [Display(Name = "En Description")]
        public string EnDescription { get; set; }
        [Url]
        public string Link { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? MinistryVissionId { get; set; }
        public MinistryVission MinistryVission { get; set; }
    }
}
