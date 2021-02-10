using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PageRouteVersion table which form PageRouteVersion object used in PageRoute screens
    /// </summary>
    public class PageRouteVersion : PageSeoVersion
    {
        public PageRouteVersion()
        {
            PageSectionVersions = new HashSet<PageSectionVersion>();
        }
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
        public string ControllerName { get; set; }
        public string PageType { get; set; }
        public string SectionName { get; set; }
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool HasNavItem { get; set; }
        public bool IsDynamicPage { get; set; }
        public string PageFilePathAr { get; set; }
        public string PageFilePathEn { get; set; }
        public int? StatusId { get; set; }
        public int? NavItemId { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public VersionStatusEnum? ContentVersionStatusEnum { get; set; }
        public int? PageRouteId { get; set; }

        #region Navigation Properties

        public Status Status { get; set; }
        [ForeignKey("PageRouteId")]
        public PageRoute PageRoute { get; set; }

      
        public NavItem NavItem { get; set; }

        public ICollection<PageSectionVersion> PageSectionVersions { get; set; }

        public ICollection<PageEventVersions> PageEventVersions { get; set; }

        public ICollection<PageNewsVersion> PageNewsVersions { get; set; }

        #endregion
    }
}
