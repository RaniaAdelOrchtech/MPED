using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Web.Admin.Mappers
{
    public static class NavItemMapper
    {
        //public static List<NavItemListViewModel> MapToNavItemViewModels(this IEnumerable<NavItemVersion> navItemVersions)
        //{
        //    return navItemVersions.Select(navItemVersion => new NavItemListViewModel
        //    {
        //        Id = navItemVersion.Id,
        //        EnName = navItemVersion.EnName,
        //        ArName = navItemVersion.ArName,
        //        Order = navItemVersion.Order,
        //        IsActive = navItemVersion.IsActive,
        //        StatusId = navItemVersion.StatusId,
        //    }).ToList();
        //}

        public static NavItemVersion MapToNavItemVersion(this NavItem navItem)
        {
            NavItemVersion navItemVersion = new NavItemVersion
            {
                //Id = navItem.NavItemVersionId.Value,
                EnName = navItem.EnName,
                ArName = navItem.ArName,
                Order = navItem.Order,
                IsActive = navItem.IsActive
            };

            return navItemVersion;
        }
    }
}
