using MPMAR.Business.ViewModels;
using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class SectionCardMapper
    {
       

        public static PageSectionCardVersion MapToSectionCardVersion(this SectionCardCreateViewModel sectionCardCreateViewModel)
        {
            PageSectionCardVersion pageSectionVersion = new PageSectionCardVersion();
            pageSectionVersion.EnTitle = sectionCardCreateViewModel.EnTitle;
            pageSectionVersion.ArTitle = sectionCardCreateViewModel.ArTitle;
            pageSectionVersion.EnDescription = sectionCardCreateViewModel.EnDescription;
            pageSectionVersion.ArDescription = sectionCardCreateViewModel.ArDescription;
            pageSectionVersion.EnImageAlt = sectionCardCreateViewModel.EnImageAlt;
            pageSectionVersion.ArImageAlt = sectionCardCreateViewModel.ArImageAlt;
            pageSectionVersion.ImageUrl = sectionCardCreateViewModel.ImageUrl;
            pageSectionVersion.Order = sectionCardCreateViewModel.Order;
            pageSectionVersion.IsActive = sectionCardCreateViewModel.IsActive;
            pageSectionVersion.PageSectionVersionId = sectionCardCreateViewModel.SectionVersionId;
            return pageSectionVersion;
        }

     
        public static PageSectionCardVersion MapToSectionCardVersion(this SectionCardEditViewModel sectionCardEditViewModel)
        {
            PageSectionCardVersion pageSectionVersion = new PageSectionCardVersion()
            {
                Id = sectionCardEditViewModel.Id,
                EnTitle = sectionCardEditViewModel.EnTitle,
                ArTitle = sectionCardEditViewModel.ArTitle,
                EnDescription = sectionCardEditViewModel.EnDescription,
                ArDescription = sectionCardEditViewModel.ArDescription,
                EnImageAlt = sectionCardEditViewModel.EnImageAlt,
                ArImageAlt = sectionCardEditViewModel.ArImageAlt,
                Order = sectionCardEditViewModel.Order,
                IsActive = sectionCardEditViewModel.IsActive,
                ImageUrl = sectionCardEditViewModel.ImageUrl,
                FileUrl = sectionCardEditViewModel.FileUrl,
                PageSectionVersionId = sectionCardEditViewModel.SectionVersionId,
                IsDeleted = sectionCardEditViewModel.IsDeleted,
                CreatedById = sectionCardEditViewModel.CreatedById,
                CreationDate = sectionCardEditViewModel.CreationDate.Value
            };

            return pageSectionVersion;
        }
    }
}
