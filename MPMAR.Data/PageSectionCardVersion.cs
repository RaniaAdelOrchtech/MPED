using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PageSectionCardVersion table which form PageSectionCardVersion object used in PageSection screens
    /// </summary>
    public class PageSectionCardVersion : ActionInfoVersion
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "English Title")]
        public string EnTitle { get; set; }

        [Display(Name = "Arabic Title")]
        public string ArTitle { get; set; }

        [Display(Name = "English Description")]
        public string EnDescription { get; set; }

        [Display(Name = "Arabic Description")]
        public string ArDescription { get; set; }

        [Display(Name = "English Image Alt")]
        public string EnImageAlt { get; set; }

        [Display(Name = "English Image Alt")]
        public string ArImageAlt { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "File")]
        public string FileUrl { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? PageSectionCardId { get; set; }
        #region Navigation Properties
        [ForeignKey("PageSectionCardId")]
        public PageSectionCard PageSectionCard { get; set; }

        public int PageSectionVersionId { get; set; }
        [ForeignKey("PageSectionVersionId")]
        public PageSectionVersion PageSectionVersion { get; set; }
        #endregion
    }
}
