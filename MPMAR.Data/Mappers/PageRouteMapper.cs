using System;
using System.Collections.Generic;
using System.Text;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Data.Mappers
{
    public static class PageRouteMapper
    {
        public static PageRouteVersion MapToPageRouteVersion(this PageRoute pageRoute)
        {
            PageRouteVersion pageRouteVersion = new PageRouteVersion()
            {
                ApprovalDate = pageRoute.ApprovalDate,
                ApprovedById = pageRoute.ApprovedById,
                ArName = pageRoute.ArName,
                EnName = pageRoute.EnName,
                IsActive = pageRoute.IsActive,
                IsDeleted = pageRoute.IsDeleted,
                Order = pageRoute.Order,
                NavItemId = pageRoute.NavItemId,
                ControllerName = pageRoute.ControllerName,
                SectionName = pageRoute.SectionName,
                CreationDate = pageRoute.CreationDate,
                CreatedById = pageRoute.CreatedById,
                IsDynamicPage = pageRoute.IsDynamicPage,

                SeoTitleEN = pageRoute.SeoTitleEN,
                SeoTitleAR = pageRoute.SeoTitleAR,
                SeoDescriptionEN = pageRoute.SeoDescriptionEN,
                SeoDescriptionAR = pageRoute.SeoDescriptionAR,
                SeoOgTitleEN = pageRoute.SeoOgTitleEN,
                SeoOgTitleAR = pageRoute.SeoOgTitleAR,
                SeoTwitterCardEN = pageRoute.SeoTwitterCardEN,
                SeoTwitterCardAR = pageRoute.SeoTwitterCardAR,

            };

            return pageRouteVersion;
        }

        public static List<PageRouteVersion> MapToPageRouteVersions(this List<PageRoute> pageRoutes)
        {
            List<PageRouteVersion> pageRouteVersions = new List<PageRouteVersion>();
            foreach (PageRoute pageRoute in pageRoutes)
            {
                PageRouteVersion pageRouteVersion = new PageRouteVersion()
                {
                    ApprovalDate = pageRoute.ApprovalDate,
                    ApprovedById = pageRoute.ApprovedById,
                    ArName = pageRoute.ArName,
                    CreatedById = pageRoute.CreatedById,
                    CreationDate = DateTime.Now,
                    EnName = pageRoute.EnName,
                    IsActive = pageRoute.IsActive,
                    IsDeleted = pageRoute.IsDeleted,
                    Order = pageRoute.Order,
                    NavItemId = pageRoute.NavItemId,
                    ControllerName = pageRoute.ControllerName,
                    SectionName = pageRoute.SectionName,
                    StatusId = (int)RequestStatus.Approved
                };

                pageRouteVersions.Add(pageRouteVersion);
            }

            pageRouteVersions.Reverse();
            return pageRouteVersions;
        }
    }
}
