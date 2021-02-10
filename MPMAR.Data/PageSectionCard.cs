using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PageSectionCard table which form PageSectionCard object used in PageSection screens
    /// </summary>
    public class PageSectionCard : ActionInfo
    {
        public int Id { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public string EnDescription { get; set; }
        public string ArDescription { get; set; }
        public string EnImageAlt { get; set; }
        public string ArImageAlt { get; set; }
        public string ImageUrl { get; set; }
        public string FileUrl { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        #region Navigation Properties     

        public int PageSectionId { get; set; }
        public PageSection PageSection { get; set; }
        #endregion
    }
}