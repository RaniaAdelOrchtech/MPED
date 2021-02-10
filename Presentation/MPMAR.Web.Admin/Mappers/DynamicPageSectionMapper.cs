using MPMAR.Business.ViewModels;
using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class DynamicPageSectionMapper
    {
       

        public static PageSectionVersion MapToPageSectionVersion(this PageSectionCreateViewModel dynamicPageSectionViewModel)
        {
            PageSectionVersion pageSectionVersion = new PageSectionVersion();
            pageSectionVersion.EnTitle = dynamicPageSectionViewModel.Section.EnTitle;
            pageSectionVersion.ArTitle = dynamicPageSectionViewModel.Section.ArTitle;
            pageSectionVersion.EnDescription = dynamicPageSectionViewModel.Section.EnDescription;
            pageSectionVersion.ArDescription = dynamicPageSectionViewModel.Section.ArDescription;
            pageSectionVersion.EnImageAlt = dynamicPageSectionViewModel.Section.EnImageAlt;
            pageSectionVersion.ArImageAlt = dynamicPageSectionViewModel.Section.ArImageAlt;
            pageSectionVersion.Url = dynamicPageSectionViewModel.Section.Url;
            pageSectionVersion.Order = dynamicPageSectionViewModel.Section.Order;
            pageSectionVersion.IsActive = dynamicPageSectionViewModel.Section.IsActive;
            pageSectionVersion.PageSectionTypeId = dynamicPageSectionViewModel.SectionTypeId.Value;
            pageSectionVersion.PageRouteVersionId = dynamicPageSectionViewModel.pageRouteVersionId;
            return pageSectionVersion;
        }

        public static PageSectionVersion MapToPageSectionVersion(this PageSectionEditViewModel dynamicPageSectionViewModel)
        {
            PageSectionVersion pageSectionVersion = new PageSectionVersion();
            pageSectionVersion.Id = dynamicPageSectionViewModel.Id;
            pageSectionVersion.EnTitle = dynamicPageSectionViewModel.Section.EnTitle;
            pageSectionVersion.ArTitle = dynamicPageSectionViewModel.Section.ArTitle;
            pageSectionVersion.EnDescription = dynamicPageSectionViewModel.Section.EnDescription;
            pageSectionVersion.ArDescription = dynamicPageSectionViewModel.Section.ArDescription;
            pageSectionVersion.EnImageAlt = dynamicPageSectionViewModel.Section.EnImageAlt;
            pageSectionVersion.ArImageAlt = dynamicPageSectionViewModel.Section.ArImageAlt;
            pageSectionVersion.Url = dynamicPageSectionViewModel.Section.Url;
            pageSectionVersion.Order = dynamicPageSectionViewModel.Section.Order;
            pageSectionVersion.IsActive = dynamicPageSectionViewModel.Section.IsActive;
            pageSectionVersion.CreationDate = dynamicPageSectionViewModel.Section.CreationDate.Value;
            pageSectionVersion.CreatedById = dynamicPageSectionViewModel.Section.CreatedById;
            pageSectionVersion.PageSectionTypeId = dynamicPageSectionViewModel.SectionTypeId.Value;
            pageSectionVersion.PageRouteVersionId = dynamicPageSectionViewModel.pageRouteVersionId;

            return pageSectionVersion;
        }

        public static SectionViewModel MapToSectionViewModel(this PageSectionVersion pageSectionVersion)
        {
            SectionViewModel sectionViewModel = new SectionViewModel
            {
                Id = pageSectionVersion.Id,
                EnTitle = pageSectionVersion.EnTitle,
                ArTitle = pageSectionVersion.ArTitle,
                EnDescription = pageSectionVersion.EnDescription,
                ArDescription = pageSectionVersion.ArDescription,
                EnImageAlt = pageSectionVersion.EnImageAlt,
                ArImageAlt = pageSectionVersion.ArImageAlt,
                Order = pageSectionVersion.Order,
                IsActive = pageSectionVersion.IsActive,
                Url = pageSectionVersion.Url,
                SectionTypeId = pageSectionVersion.PageSectionTypeId,
                CreationDate = pageSectionVersion.CreationDate,
                CreatedById = pageSectionVersion.CreatedById,
                MediaType = pageSectionVersion.PageSectionType.MediaType
            };

            return sectionViewModel;
        }

        public static PageSectionVersion MapToPageSectionVersion(this PageSection pageSection)
        {
            PageSectionVersion pageRouteVersion = new PageSectionVersion
            {
                Id = pageSection.PageSectionVersionId.Value,
                EnTitle = pageSection.EnTitle,
                ArTitle = pageSection.ArTitle,
                EnDescription = pageSection.EnDescription,
                ArDescription = pageSection.ArDescription,
                Order = pageSection.Order,
                IsActive = pageSection.IsActive,
                Url = pageSection.Url,
                EnImageAlt = pageSection.EnImageAlt,
                ArImageAlt = pageSection.ArImageAlt,
                PageSectionType = pageSection.PageSectionType,
                PageSectionTypeId = pageSection.PageSectionTypeId,
                PageRouteVersion = pageSection.PageSectionVersion.PageRouteVersion,
                PageRouteVersionId = pageSection.PageSectionVersion.PageRouteVersionId
            };

            return pageRouteVersion;
        }
    }
}
