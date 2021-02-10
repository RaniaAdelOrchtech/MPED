using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for FormerMinistriesPageInfoVersion table which form FormerMinistriesPageInfoVersion object used in FormerMinistries screens
    /// </summary>
    public class FormerMinistriesPageInfoVersions : ActionInfo
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Title 1 Ar")]
        public string Title1Ar { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Title 1 Ar")]
        public string Title1En { get; set; }
        [Required]
        [MaxLength(1500)]
        [Display(Name = "Description Ar")]
        public string DescriptionAr { get; set; }
        [Required]
        [MaxLength(1500)]
        [Display(Name = "Description En")]
        public string DescriptionEn { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Title 2 Ar")]
        public string Title2Ar { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Title 2 En")]
        public string Title2En { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? FormerMinistriesPageInfoId { get; set; }

        public FormerMinistriesPageInfo FormerMinistriesPageInfo { get; set; }
        public ICollection<MinistryTimeLineVersions> MinistryTimeLineVersions { get; set; }
    }
}
