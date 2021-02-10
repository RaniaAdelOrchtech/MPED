using System;
using System.Collections.Generic;
using System.Text;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Data.Mappers
{
    public static class NavItemMapper
    {
        public static NavItemVersion MapToNavItemVersion(this NavItem navItem)
        {
            NavItemVersion navItemVersion = new NavItemVersion()
            {
                //Id = navItem.NavItemVersionId.Value,
                ApprovalDate = navItem.ApprovalDate,
                ApprovedById = navItem.ApprovedById,
                ArName = navItem.ArName,
                EnName = navItem.EnName,
                IsActive = navItem.IsActive,
                //IsApproved = navItem.IsApproved,
                //IsDeleted = navItem.IsDeleted,
                Order = navItem.Order,
                CreationDate = navItem.CreationDate,
                CreatedById = navItem.CreatedById,
                IsDeleted = navItem.IsDeleted,
                ParentNavItemId = navItem.ParentNavItemId,
                //StatusId = navItem.NavItemVersion.StatusId,
                NavItem = navItem
            };

            return navItemVersion;
        }

        public static List<NavItem> MapToNavItems(this List<NavItemVersion> navItemVersions)
        {
            List<NavItem> navItems = new List<NavItem>();
            foreach (NavItemVersion navItemVersion in navItemVersions)
            {
                NavItem navItem = new NavItem()
                {
                    ApprovalDate = navItemVersion.ApprovalDate,
                    ApprovedById = navItemVersion.ApprovedById,
                    ArName = navItemVersion.ArName,
                    CreatedById = navItemVersion.CreatedById,
                    CreationDate = DateTime.Now,
                    EnName = navItemVersion.EnName,
                    IsActive = navItemVersion.IsActive,
                    IsDeleted = navItemVersion.IsDeleted,
                    Order = navItemVersion.Order,
                    ParentNavItemId = navItemVersion.ParentNavItemId,
                    //NavItemVersionId = navItemVersion.Id
                };

                navItems.Add(navItem);
            }

            navItems.Reverse();
            return navItems;
        }
    }
}
