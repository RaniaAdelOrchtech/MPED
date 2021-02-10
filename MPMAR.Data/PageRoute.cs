using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PageRoute table which form PageRoute object used in PageRoute screens
    /// </summary>
    public class PageRoute : PageSeo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "English Name")]
        public string EnName { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Arabic Name")]
        public string ArName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }

        public string ControllerName { get; set; }
        public string SectionName { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public bool HasNavItem { get; set; }

        public bool IsDynamicPage { get; set; }

       
        public ICollection<PageSection> PageSections { get; set; }
        public ICollection<PageRouteVersion> PageRouteVersions { get; set; }

        public int? NavItemId { get; set; }
        public NavItem NavItem { get; set; }
        public string PageFilePathAr { get; set; }
        public string PageFilePathEn { get; set; }
        public string PageType { get; set; }

        public ICollection<PageMinistry> PageMinistry { get; set; }
        public ICollection<PageMinistryVersion> PageMinistryVersions { get; set; }
        public ICollection<PageNews> PageNews { get; set; }
        public ICollection<BEUsersPrivileges> BEUsersPrivileges { get; set; }
    }
}
