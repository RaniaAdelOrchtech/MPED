using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class PageMinistryMapper
    {
        public static List<PageMinistryListViewModel> MapToPageMinistryViewModel(this IEnumerable<PageMinistry> pageMinistry)
        {
            return pageMinistry.Select(pgMinisty => new PageMinistryListViewModel
            {
                Id = pgMinisty.Id,
                EnName = pgMinisty.EnName,
                ArName = pgMinisty.ArName,
                ArContent = pgMinisty.ArContent,
                EnContent = pgMinisty.EnContent,
                ImageUrl = pgMinisty.ImageUrl,
                EnImageUrl = pgMinisty.EnImageUrl,
                IsActive = pgMinisty.IsActive,
                IsDeleted = pgMinisty.IsDeleted
            }).ToList();
        }
        public static List<PageMinistryListViewModel> MapToPageMinistryViewModel(this IEnumerable<PageMinistryVersion> pageMinistry)
        {
            return pageMinistry.Select(pgMinisty => new PageMinistryListViewModel
            {
                Id = pgMinisty.Id,
                EnName = pgMinisty.EnName,
                ArName = pgMinisty.ArName,
                ArContent = pgMinisty.ArContent,
                EnContent = pgMinisty.EnContent,
                ImageUrl = pgMinisty.ImageUrl,
                EnImageUrl = pgMinisty.EnImageUrl,
                IsActive = pgMinisty.IsActive,
                IsDeleted = pgMinisty.IsDeleted
            }).ToList();
        }

        public static PageMinistry MapToPageMinistry(this PageMinistryEditViewModel sectionCardCreateViewModel)
        {
            PageMinistry pageSectionVersion = new PageMinistry();
            pageSectionVersion.EnName = sectionCardCreateViewModel.EnName;
            pageSectionVersion.ArName = sectionCardCreateViewModel.ArName;
            pageSectionVersion.EnContent = sectionCardCreateViewModel.EnContent;
            pageSectionVersion.ArContent = sectionCardCreateViewModel.ArContent;
            pageSectionVersion.ImageUrl = sectionCardCreateViewModel.ImageUrl;
            pageSectionVersion.EnImageUrl = sectionCardCreateViewModel.EnImageUrl;
            pageSectionVersion.IsActive = sectionCardCreateViewModel.IsActive;
            pageSectionVersion.IsDeleted = sectionCardCreateViewModel.IsDeleted;
            pageSectionVersion.IsDobulQuote = sectionCardCreateViewModel.IsDobulQuote;
            pageSectionVersion.Order = sectionCardCreateViewModel.Order;
            pageSectionVersion.IsSection = sectionCardCreateViewModel.IsSection;
            pageSectionVersion.PageRouteId = sectionCardCreateViewModel.PageRouteId;

            pageSectionVersion.SeoTitleEN = sectionCardCreateViewModel.SeoTitleEN;
            pageSectionVersion.SeoTitleAR = sectionCardCreateViewModel.SeoTitleAR;
            pageSectionVersion.SeoDescriptionEN = sectionCardCreateViewModel.SeoDescriptionEN;
            pageSectionVersion.SeoDescriptionAR = sectionCardCreateViewModel.SeoDescriptionAR;
            pageSectionVersion.SeoOgTitleEN = sectionCardCreateViewModel.SeoOgTitleEN;
            pageSectionVersion.SeoOgTitleAR = sectionCardCreateViewModel.SeoOgTitleAR;
            pageSectionVersion.SeoTwitterCardEN = sectionCardCreateViewModel.SeoTwitterCardEN;
            pageSectionVersion.SeoTwitterCardAR = sectionCardCreateViewModel.SeoTwitterCardAR;

            if (sectionCardCreateViewModel.Id > 0)
                pageSectionVersion.Id = sectionCardCreateViewModel.Id;
            return pageSectionVersion;
        }

        public static PageMinistryVersion MapToPageMinistryVersion(this PageMinistryEditViewModel sectionCardCreateViewModel)
        {
            PageMinistryVersion pageSectionVersion = new PageMinistryVersion()
            {
                EnName = sectionCardCreateViewModel.EnName,
                ArName = sectionCardCreateViewModel.ArName,
                EnContent = sectionCardCreateViewModel.EnContent,
                ArContent = sectionCardCreateViewModel.ArContent,
                ImageUrl = sectionCardCreateViewModel.ImageUrl,
                EnImageUrl = sectionCardCreateViewModel.EnImageUrl,
                IsActive = sectionCardCreateViewModel.IsActive,
                IsDeleted = sectionCardCreateViewModel.IsDeleted,
                IsDobulQuote = sectionCardCreateViewModel.IsDobulQuote,
                Order = sectionCardCreateViewModel.Order,
                IsSection = sectionCardCreateViewModel.IsSection,
                PageRouteId = sectionCardCreateViewModel.PageRouteId,
                ApprovalDate=sectionCardCreateViewModel.ApprovalDate,
                ApprovedById=sectionCardCreateViewModel.ApprovedById,
                ChangeActionEnum=sectionCardCreateViewModel.ChangeActionEnum,
                CreatedById=sectionCardCreateViewModel.CreatedById,
                CreationDate=sectionCardCreateViewModel.CreationDate,
                Id=sectionCardCreateViewModel.Id,
                IsHeading=sectionCardCreateViewModel.IsHeading,
                PageMinistryId=sectionCardCreateViewModel.PageMinistryId,
                VersionStatusEnum=sectionCardCreateViewModel.VersionStatusEnum,

                SeoTitleEN = sectionCardCreateViewModel.SeoTitleEN,
                SeoTitleAR = sectionCardCreateViewModel.SeoTitleAR,
                SeoDescriptionEN = sectionCardCreateViewModel.SeoDescriptionEN,
                SeoDescriptionAR = sectionCardCreateViewModel.SeoDescriptionAR,
                SeoOgTitleEN = sectionCardCreateViewModel.SeoOgTitleEN,
                SeoOgTitleAR = sectionCardCreateViewModel.SeoOgTitleAR,
                SeoTwitterCardEN = sectionCardCreateViewModel.SeoTwitterCardEN,
                SeoTwitterCardAR = sectionCardCreateViewModel.SeoTwitterCardAR,
            };





















            if (sectionCardCreateViewModel.Id > 0)
                pageSectionVersion.Id = sectionCardCreateViewModel.Id;
            return pageSectionVersion;
        }

        public static PageMinistryEditViewModel MapToSctionCardViewModel(this PageMinistry sectionCardCreateViewModel)
        {
            PageMinistryEditViewModel viewModel = new PageMinistryEditViewModel()
            {
                Id = sectionCardCreateViewModel.Id,
                EnName = sectionCardCreateViewModel.EnName,
                ArName = sectionCardCreateViewModel.ArName,
                EnContent = sectionCardCreateViewModel.EnContent,
                ArContent = sectionCardCreateViewModel.ArContent,
                IsActive = sectionCardCreateViewModel.IsActive,
                IsDeleted = sectionCardCreateViewModel.IsDeleted,
                IsDobulQuote = sectionCardCreateViewModel.IsDobulQuote,
                ImageUrl = sectionCardCreateViewModel.ImageUrl,
                EnImageUrl = sectionCardCreateViewModel.EnImageUrl,
                Order = sectionCardCreateViewModel.Order,
                IsSection = sectionCardCreateViewModel.IsSection,
                SeoTitleEN = sectionCardCreateViewModel.SeoTitleEN,
                SeoTitleAR = sectionCardCreateViewModel.SeoTitleAR,
                SeoDescriptionEN = sectionCardCreateViewModel.SeoDescriptionEN,
                SeoDescriptionAR = sectionCardCreateViewModel.SeoDescriptionAR,
                SeoOgTitleEN = sectionCardCreateViewModel.SeoOgTitleEN,
                SeoOgTitleAR = sectionCardCreateViewModel.SeoOgTitleAR,
                SeoTwitterCardEN = sectionCardCreateViewModel.SeoTwitterCardEN,
                SeoTwitterCardAR = sectionCardCreateViewModel.SeoTwitterCardAR,
                PageRouteId = sectionCardCreateViewModel.PageRouteId ?? 0,
                VersionStatusEnum = VersionStatusEnum.Draft,
                ChangeActionEnum = ChangeActionEnum.Update,
                PageMinistryId = sectionCardCreateViewModel.Id
            };

            return viewModel;
        }

        public static PageMinistryEditViewModel MapToSctionCardViewModel(this PageMinistryVersion sectionCardCreateViewModel)
        {
            PageMinistryEditViewModel viewModel = new PageMinistryEditViewModel()
            {
                Id = sectionCardCreateViewModel.Id,
                EnName = sectionCardCreateViewModel.EnName,
                ArName = sectionCardCreateViewModel.ArName,
                EnContent = sectionCardCreateViewModel.EnContent,
                ArContent = sectionCardCreateViewModel.ArContent,
                IsActive = sectionCardCreateViewModel.IsActive,
                IsDeleted = sectionCardCreateViewModel.IsDeleted,
                IsDobulQuote = sectionCardCreateViewModel.IsDobulQuote,
                ImageUrl = sectionCardCreateViewModel.ImageUrl,
                EnImageUrl = sectionCardCreateViewModel.EnImageUrl,
                Order = sectionCardCreateViewModel.Order,
                IsSection = sectionCardCreateViewModel.IsSection,
                SeoTitleEN = sectionCardCreateViewModel.SeoTitleEN,
                SeoTitleAR = sectionCardCreateViewModel.SeoTitleAR,
                SeoDescriptionEN = sectionCardCreateViewModel.SeoDescriptionEN,
                SeoDescriptionAR = sectionCardCreateViewModel.SeoDescriptionAR,
                SeoOgTitleEN = sectionCardCreateViewModel.SeoOgTitleEN,
                SeoOgTitleAR = sectionCardCreateViewModel.SeoOgTitleAR,
                SeoTwitterCardEN = sectionCardCreateViewModel.SeoTwitterCardEN,
                SeoTwitterCardAR = sectionCardCreateViewModel.SeoTwitterCardAR,
                PageRouteId = sectionCardCreateViewModel.PageRouteId ?? 0,
                VersionStatusEnum = sectionCardCreateViewModel.VersionStatusEnum,
                ChangeActionEnum = sectionCardCreateViewModel.ChangeActionEnum,
                PageMinistryId = sectionCardCreateViewModel.PageMinistryId
            };

            return viewModel;
        }


    }
}

