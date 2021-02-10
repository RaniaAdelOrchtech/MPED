using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PageSectionVersion table which form PageSectionVersion object used in PageSection screens
    /// </summary>
    public class PageSectionVersion : ActionInfoVersion
    {
        public PageSectionVersion()
        {
            PageSectionCardVersions = new HashSet<PageSectionCardVersion>();
        }
        [Key]
        public int Id { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public string EnDescription { get; set; }
        public string ArDescription { get; set; }
        public string Url { get; set; }
        public string EnImageAlt { get; set; }
        public string ArImageAlt { get; set; }
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int PageRouteVersionId { get; set; }
        public int PageSectionTypeId { get; set; }
        public int? PageSectionId { get; set; }
        #region Navigation Properties
        [ForeignKey("PageSectionId")]
        public PageSection PageSection { get; set; }

        [ForeignKey("PageRouteVersionId")]
        public PageRouteVersion PageRouteVersion { get; set; }

        [ForeignKey("PageSectionTypeId")]
        public PageSectionType PageSectionType { get; set; }

        public ICollection<PageSectionCardVersion> PageSectionCardVersions { get; set; }
        #endregion
    }
}
