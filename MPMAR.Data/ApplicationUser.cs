using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for users table which form user object used in accounts screen
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// detect if user logged in for first time or not (it helps 2FA functionalit)
        /// </summary>
        public bool isFirstLogin { get; set; } = true;

        #region NavItem navigation properties
        public ICollection<NavItem> CreatedNavItems { get; set; }
        public ICollection<NavItem> ModifiedNavItems { get; set; }
        public ICollection<NavItem> ApprovedNavItems { get; set; }
        #endregion

        #region NavItemVersion navigation properties
        public ICollection<NavItemVersion> CreatedNavItemVersions { get; set; }
        public ICollection<NavItemVersion> ApprovedNavItemVersions { get; set; }
        #endregion

        #region PageRoute navigation properties
        public ICollection<PageRoute> CreatedPageRoutes { get; set; }
        public ICollection<PageRoute> ModifiedPageRoutes { get; set; }
        public ICollection<PageRoute> ApprovedPageRoutes { get; set; }
        #endregion

        #region PageRouteVersion navigation properties
        public ICollection<PageRouteVersion> CreatedPageRouteVersions { get; set; }
        public ICollection<PageRouteVersion> ApprovedPageRouteVersions { get; set; }
        #endregion

        #region PageSection navigation properties
        public ICollection<PageSection> CreatedPageSections { get; set; }
        public ICollection<PageSection> ModifiedPageSections { get; set; }
        public ICollection<PageSection> ApprovedPageSections { get; set; }
        #endregion

        #region PageSectionVersion navigation properties
        public ICollection<PageSectionVersion> CreatedPageSectionVersions { get; set; }
        public ICollection<PageSectionVersion> ApprovedPageSectionVersions { get; set; }
        public ICollection<BEUsersPrivileges> BEUsersPrivileges { get; set; }
        #endregion
    }
}
