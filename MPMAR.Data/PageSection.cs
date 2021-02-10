using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PageSection table which form PageSection object used in PageSection screens
    /// </summary>
    public class PageSection : ActionInfo
    {
        public int Id { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public string EnDescription { get; set; }
        public string ArDescription { get; set; }
        public string Url { get; set; }
        public string EnImageAlt { get; set; }
        public string ArImageAlt { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        #region Navigation Properties
        public int? PageSectionVersionId { get; set; }
        public PageSectionVersion PageSectionVersion { get; set; }

        public ICollection<PageSectionCard> PageSectionCards { get; set; }

        public int PageRouteId { get; set; }
        public PageRoute PageRoute { get; set; }

        public int PageSectionTypeId { get; set; }
        public PageSectionType PageSectionType { get; set; }
        #endregion
    }
}
