
using MPMAR.Business.ViewModels;
using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Web.Admin.Mappers
{
    public static class PageRouteMapper
    {
        public static List<PageRouteListViewModel> MapToPageRouteViewModels(this IEnumerable<PageRouteVersion> pageRouteVersions)
        {
            return pageRouteVersions.Select(pageRouteVersion => new PageRouteListViewModel
            {
                Id = pageRouteVersion.Id,
                EnName = pageRouteVersion.EnName,
                ArName = pageRouteVersion.ArName,
                Order = pageRouteVersion.Order,
                PageType = pageRouteVersion.PageType,
                IsActive = pageRouteVersion.IsActive,
                NavItemArName = (pageRouteVersion.PageRoute != null) ? ((pageRouteVersion.PageRoute.NavItem != null) ? pageRouteVersion.PageRoute.NavItem.ArName : "") : ((pageRouteVersion.NavItem != null) ? pageRouteVersion.NavItem.ArName : ""),
                NavItemEnName = (pageRouteVersion.PageRoute != null) ? ((pageRouteVersion.PageRoute.NavItem != null) ? pageRouteVersion.PageRoute.NavItem.EnName : "") : ((pageRouteVersion.NavItem != null) ? pageRouteVersion.NavItem.EnName : ""),
                StatusId = pageRouteVersion.StatusId,
               // StatusName = ((RequestStatus)pageRouteVersion.StatusId).ToString(),
                HasSections = pageRouteVersion.PageSectionVersions.Any(),
                IsDeleted = pageRouteVersion.IsDeleted,
            }).ToList();
        }

        //public static List<PageRouteListViewModel> MapToPageRouteViewModels(this IEnumerable<PageRoute> pageRouteVersions)
        //{
        //    return pageRouteVersions.Select(pageRouteVersion => new PageRouteListViewModel
        //    {
        //        Id = pageRouteVersion.Id,
        //        EnName = pageRouteVersion.EnName,
        //        ArName = pageRouteVersion.ArName,
        //        Order = pageRouteVersion.Order,
        //        IsActive = pageRouteVersion.IsActive,
        //        NavItemArName = (pageRouteVersion.PageRouteVersion != null) ? ((pageRouteVersion.NavItem != null) ? pageRouteVersion.PageRoute.NavItem.ArName : "") : ((pageRouteVersion.NavItem != null) ? pageRouteVersion.NavItem.ArName : ""),
        //        NavItemEnName = (pageRouteVersion.PageRouteVersion != null) ? ((pageRouteVersion.NavItem != null) ? pageRouteVersion.PageRoute.NavItem.EnName : "") : ((pageRouteVersion.NavItem != null) ? pageRouteVersion.NavItem.EnName : ""),
        //        StatusId = pageRouteVersion.StatusId,
        //        StatusName = ((RequestStatus)pageRouteVersion.StatusId).ToString(),
        //        HasSections = pageRouteVersion.PageSectionVersions.Any(),
        //        IsDeleted = pageRouteVersion.IsDeleted
        //    }).ToList();
        //}

        public static PageRouteVersion MapToPageRouteVersion(this PageRouteCreateViewModel pageRouteViewModel, PageRouteVersion pageRouteVersion)
        {
            pageRouteVersion.EnName = pageRouteViewModel.EnName;
            pageRouteVersion.ArName = pageRouteViewModel.ArName;
            pageRouteVersion.Order = pageRouteViewModel.Order;
            pageRouteVersion.IsActive = pageRouteViewModel.IsActive;
            pageRouteVersion.NavItemId = pageRouteViewModel.NavItemId;

            pageRouteVersion.SeoTitleEN = pageRouteViewModel.SeoTitleEN;
            pageRouteVersion.SeoTitleAR = pageRouteViewModel.SeoTitleAR;
            pageRouteVersion.SeoDescriptionEN = pageRouteViewModel.SeoDescriptionEN;
            pageRouteVersion.SeoDescriptionAR = pageRouteViewModel.SeoDescriptionAR;
            pageRouteVersion.SeoOgTitleEN = pageRouteViewModel.SeoOgTitleEN;
            pageRouteVersion.SeoOgTitleAR = pageRouteViewModel.SeoOgTitleAR;
            pageRouteVersion.SeoTwitterCardEN = pageRouteViewModel.SeoTwitterCardEN;
            pageRouteVersion.SeoTwitterCardAR = pageRouteViewModel.SeoTwitterCardAR;
            pageRouteVersion.PageType = pageRouteVersion.PageType;
            return pageRouteVersion;
        }

        public static PageRouteEditViewModel MapToPageRouteViewModel(this PageRouteVersion pageRouteVersion, PageRouteEditViewModel pageRouteEditViewModel)
        {
            pageRouteEditViewModel.Id = pageRouteVersion.Id;
            pageRouteEditViewModel.EnName = pageRouteVersion.EnName;
            pageRouteEditViewModel.ArName = pageRouteVersion.ArName;
            pageRouteEditViewModel.Order = pageRouteVersion.Order;
            pageRouteEditViewModel.IsActive = pageRouteVersion.IsActive;
            pageRouteEditViewModel.IsDynamicPage = pageRouteVersion.IsDynamicPage;
            pageRouteEditViewModel.HasNavItem = pageRouteVersion.HasNavItem;
            pageRouteEditViewModel.NavItemId = pageRouteVersion.NavItemId;
            pageRouteEditViewModel.CreatedById = pageRouteVersion.CreatedById;
            pageRouteEditViewModel.CreationDate = pageRouteVersion.CreationDate;
            pageRouteEditViewModel.ControllerName = pageRouteVersion.ControllerName;
            pageRouteEditViewModel.SectionName = pageRouteVersion.SectionName;

            pageRouteEditViewModel.SeoTitleEN = pageRouteVersion.SeoTitleEN;
            pageRouteEditViewModel.SeoTitleAR = pageRouteVersion.SeoTitleAR;
            pageRouteEditViewModel.SeoDescriptionEN = pageRouteVersion.SeoDescriptionEN;
            pageRouteEditViewModel.SeoDescriptionAR = pageRouteVersion.SeoDescriptionAR;
            pageRouteEditViewModel.SeoOgTitleEN = pageRouteVersion.SeoOgTitleEN;
            pageRouteEditViewModel.SeoOgTitleAR = pageRouteVersion.SeoOgTitleAR;
            pageRouteEditViewModel.SeoTwitterCardEN = pageRouteVersion.SeoTwitterCardEN;
            pageRouteEditViewModel.SeoTwitterCardAR = pageRouteVersion.SeoTwitterCardAR;
            pageRouteEditViewModel.PageType = pageRouteVersion.PageType;
            pageRouteEditViewModel.ChangeActionEnum = pageRouteVersion.ChangeActionEnum;
            pageRouteEditViewModel.VersionStatusEnum = pageRouteVersion.VersionStatusEnum;
            pageRouteEditViewModel.PageRouteId = pageRouteVersion.PageRouteId;
            pageRouteEditViewModel.ContentVersionStatusEnum = pageRouteVersion.ContentVersionStatusEnum;
            return pageRouteEditViewModel;
        }

        public static PageRouteVersion MapToPageRouteVersion(this PageRouteEditViewModel pageRouteViewModel)
        {
            PageRouteVersion pageRouteVersion = new PageRouteVersion();

            pageRouteVersion.Id = pageRouteViewModel.Id;
            pageRouteVersion.EnName = pageRouteViewModel.EnName;
            pageRouteVersion.ArName = pageRouteViewModel.ArName;
            pageRouteVersion.Order = pageRouteViewModel.Order;
            pageRouteVersion.IsActive = pageRouteViewModel.IsActive;
            pageRouteVersion.IsDynamicPage = pageRouteViewModel.IsDynamicPage;
            pageRouteVersion.HasNavItem = pageRouteViewModel.HasNavItem;
            pageRouteVersion.NavItemId = pageRouteViewModel.NavItemId;
            pageRouteVersion.CreatedById = pageRouteViewModel.CreatedById;
            pageRouteVersion.CreationDate = pageRouteViewModel.CreationDate;
            pageRouteVersion.ControllerName = pageRouteViewModel.ControllerName;
            pageRouteVersion.SectionName = pageRouteViewModel.SectionName;


            pageRouteVersion.PageType = pageRouteViewModel.PageType;

            pageRouteVersion.SeoTitleEN = pageRouteViewModel.SeoTitleEN;
            pageRouteVersion.SeoTitleAR = pageRouteViewModel.SeoTitleAR;
            pageRouteVersion.SeoDescriptionEN = pageRouteViewModel.SeoDescriptionEN;
            pageRouteVersion.SeoDescriptionAR = pageRouteViewModel.SeoDescriptionAR;
            pageRouteVersion.SeoOgTitleEN = pageRouteViewModel.SeoOgTitleEN;
            pageRouteVersion.SeoOgTitleAR = pageRouteViewModel.SeoOgTitleAR;
            pageRouteVersion.SeoTwitterCardEN = pageRouteViewModel.SeoTwitterCardEN;
            pageRouteVersion.SeoTwitterCardAR = pageRouteViewModel.SeoTwitterCardAR;

            pageRouteVersion.ChangeActionEnum = pageRouteViewModel.ChangeActionEnum;
            pageRouteVersion.VersionStatusEnum = pageRouteViewModel.VersionStatusEnum;
            pageRouteVersion.ContentVersionStatusEnum = pageRouteViewModel.ContentVersionStatusEnum;
            pageRouteVersion.PageRouteId = pageRouteViewModel.PageRouteId;

            return pageRouteVersion;
        }

        public static PageRouteVersion MapToPageRouteVersion(this PageRoute pageRoute)
        {
            PageRouteVersion pageRouteVersion = new PageRouteVersion
            {
                EnName = pageRoute.EnName,
                ArName = pageRoute.ArName,
                Order = pageRoute.Order,
                IsActive = pageRoute.IsActive,
                NavItemId = pageRoute.NavItemId,
                SeoTitleEN = pageRoute.SeoTitleEN,
                SeoTitleAR = pageRoute.SeoTitleAR,
                SeoDescriptionEN = pageRoute.SeoDescriptionEN,
                SeoDescriptionAR = pageRoute.SeoDescriptionAR,
                SeoOgTitleEN = pageRoute.SeoOgTitleEN,
                SeoOgTitleAR = pageRoute.SeoOgTitleAR,
                SeoTwitterCardEN = pageRoute.SeoTwitterCardEN,
                SeoTwitterCardAR = pageRoute.SeoTwitterCardAR
            };

            return pageRouteVersion;
        }
    }
}