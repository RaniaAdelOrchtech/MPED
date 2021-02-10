using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class PageContactMapper
    {
        public static List<PageContactListViewModel> MapToPageContactViewModel(this IEnumerable<PageContact> pageContact)
        {
            return pageContact.Select(pgContact => new PageContactListViewModel
            {
                Id = pgContact.Id,
                EnMapTitle = pgContact.EnMapTitle,
                ArMapTitle = pgContact.ArMapTitle,
                ArParticipateTitle = pgContact.ArParticipateTitle,
                EnParticipateTitle = pgContact.EnParticipateTitle,
                EnPageName = pgContact.EnPageName,
                ArPageName = pgContact.ArPageName,
                ArAddress = pgContact.ArAddress,
                EnAddress = pgContact.EnAddress,
                MapUrl = pgContact.MapUrl,
                PhoneNumber = pgContact.PhoneNumber,
                FaxNumber = pgContact.FaxNumber,
                EmailParticipateEmail = pgContact.EmailParticipateEmail,
                FormParticipateActive = pgContact.FormParticipateActive,
                IsActive = pgContact.IsActive,
                IsDeleted = pgContact.IsDeleted



            }).ToList();
        }

        public static PageContact MapToPageContact(this PageContactEditViewModel pageContactCreateViewModel)
        {
            PageContact pageContact = new PageContact();
            pageContact.EnMapTitle = pageContactCreateViewModel.EnMapTitle;
            pageContact.ArMapTitle = pageContactCreateViewModel.ArMapTitle;
            pageContact.ArParticipateTitle = pageContactCreateViewModel.ArParticipateTitle;
            pageContact.EnParticipateTitle = pageContactCreateViewModel.EnParticipateTitle;

            pageContact.ArAddress = pageContactCreateViewModel.ArAddress;
            pageContact.EnAddress = pageContactCreateViewModel.EnAddress;
            pageContact.MapUrl = pageContactCreateViewModel.MapUrl;
            pageContact.PhoneNumber = pageContactCreateViewModel.PhoneNumber;
            pageContact.FaxNumber = pageContactCreateViewModel.FaxNumber;
            pageContact.EmailParticipateEmail = pageContactCreateViewModel.EmailParticipateEmail;
            pageContact.FormParticipateActive = pageContactCreateViewModel.FormParticipateActive;
            pageContact.IsActive = pageContactCreateViewModel.IsActive;
            pageContact.IsDeleted = pageContactCreateViewModel.IsDeleted;

            pageContact.Order = pageContactCreateViewModel.Order;
            pageContact.SeoTitleEN = pageContactCreateViewModel.SeoTitleEN;
            pageContact.SeoTitleAR = pageContactCreateViewModel.SeoTitleAR;
            pageContact.SeoDescriptionEN = pageContactCreateViewModel.SeoDescriptionEN;
            pageContact.SeoDescriptionAR = pageContactCreateViewModel.SeoDescriptionAR;
            pageContact.SeoOgTitleEN = pageContactCreateViewModel.SeoOgTitleEN;
            pageContact.SeoOgTitleAR = pageContactCreateViewModel.SeoOgTitleAR;
            pageContact.SeoTwitterCardEN = pageContactCreateViewModel.SeoTwitterCardEN;
            pageContact.SeoTwitterCardAR = pageContactCreateViewModel.SeoTwitterCardAR;

            pageContact.EnPageName = pageContactCreateViewModel.EnPageName;
            pageContact.ArPageName = pageContactCreateViewModel.ArPageName;


            pageContact.PageRouteVersionId = pageContactCreateViewModel.PageRouteVersionId;
            if (pageContactCreateViewModel.Id > 0)
                pageContact.Id = pageContactCreateViewModel.Id;
            return pageContact;
        }

        public static PageContactEditViewModel MapToPageContactViewModel(this PageContact pageContactCreateViewModel)
        {
            PageContactEditViewModel viewModel = new PageContactEditViewModel()
            {
                Id = pageContactCreateViewModel.Id,
                IsActive = pageContactCreateViewModel.IsActive,
                IsDeleted = pageContactCreateViewModel.IsDeleted,
                Order = pageContactCreateViewModel.Order,
                PageRouteVersionId = pageContactCreateViewModel.PageRouteVersionId,
                EnPageName = pageContactCreateViewModel.EnPageName,
                ArPageName = pageContactCreateViewModel.ArPageName,
                EnMapTitle = pageContactCreateViewModel.EnMapTitle,
                ArMapTitle = pageContactCreateViewModel.ArMapTitle,
                ArParticipateTitle = pageContactCreateViewModel.ArParticipateTitle,
                EnParticipateTitle = pageContactCreateViewModel.EnParticipateTitle,
                ArAddress = pageContactCreateViewModel.ArAddress,
                EnAddress = pageContactCreateViewModel.EnAddress,
                MapUrl = pageContactCreateViewModel.MapUrl,
                PhoneNumber = pageContactCreateViewModel.PhoneNumber,
                FaxNumber = pageContactCreateViewModel.FaxNumber,
                EmailParticipateEmail = pageContactCreateViewModel.EmailParticipateEmail,
                FormParticipateActive = pageContactCreateViewModel.FormParticipateActive,

                SeoTitleEN = pageContactCreateViewModel.SeoTitleEN,
                SeoTitleAR = pageContactCreateViewModel.SeoTitleAR,
                SeoDescriptionEN = pageContactCreateViewModel.SeoDescriptionEN,
                SeoDescriptionAR = pageContactCreateViewModel.SeoDescriptionAR,
                SeoOgTitleEN = pageContactCreateViewModel.SeoOgTitleEN,
                SeoOgTitleAR = pageContactCreateViewModel.SeoOgTitleAR,
                SeoTwitterCardEN = pageContactCreateViewModel.SeoTwitterCardEN,
                SeoTwitterCardAR = pageContactCreateViewModel.SeoTwitterCardAR

            };

            return viewModel;
        }

        public static PageContactEditViewModel MapToPageContactVersionViewModel(this PageContactVersions pgMinisty)
        {
            PageContactEditViewModel viewModel = new PageContactEditViewModel()
            {
                Id = pgMinisty.Id,
                IsActive = pgMinisty.IsActive,
                IsDeleted = pgMinisty.IsDeleted,
                VersionStatusEnum = pgMinisty.VersionStatusEnum,
                ChangeActionEnum = pgMinisty.ChangeActionEnum,
                CreationDate = pgMinisty.CreationDate,
                ModificationDate = pgMinisty.ModificationDate,
                ModifiedById = pgMinisty.ModifiedById,
                ApprovalDate = pgMinisty.ApprovalDate,
                ApprovedById = pgMinisty.ApprovedById,
                CreatedById = pgMinisty.CreatedById,
                PageContactId = pgMinisty.PageContactId,
                ArAddress = pgMinisty.ArAddress,
                ArMapTitle = pgMinisty.ArMapTitle,
                PhoneNumber = pgMinisty.PhoneNumber,
                Order = pgMinisty.Order,
                MapUrl = pgMinisty.MapUrl,
                FaxNumber = pgMinisty.FaxNumber,
                ArPageName = pgMinisty.ArPageName,
                ArParticipateTitle = pgMinisty.ArParticipateTitle,
                EmailParticipateEmail = pgMinisty.EmailParticipateEmail,
                EnAddress = pgMinisty.EnAddress,
                EnMapTitle = pgMinisty.EnMapTitle,
                EnPageName = pgMinisty.EnPageName,
                EnParticipateTitle = pgMinisty.EnParticipateTitle,
                FormParticipateActive = pgMinisty.FormParticipateActive,
                SeoDescriptionAR = pgMinisty.SeoDescriptionAR,
                SeoTwitterCardEN = pgMinisty.SeoTwitterCardEN,
                SeoTwitterCardAR = pgMinisty.SeoTwitterCardAR,
                SeoTitleEN = pgMinisty.SeoTitleEN,
                SeoTitleAR = pgMinisty.SeoTitleAR,
                SeoOgTitleEN = pgMinisty.SeoOgTitleEN,
                SeoOgTitleAR = pgMinisty.SeoOgTitleAR,
                SeoDescriptionEN = pgMinisty.SeoDescriptionEN,
                PageRouteVersionId = pgMinisty.PageRouteVersionId ?? 0
            };

            return viewModel;
        }

        public static PageContactVersions MapToPageContactVersionModel(this PageContactEditViewModel pgMinisty)
        {
            PageContactVersions viewModel = new PageContactVersions()
            {
                Id = pgMinisty.PageContactId ?? pgMinisty.Id,
                IsActive = pgMinisty.IsActive,
                IsDeleted = pgMinisty.IsDeleted,
                VersionStatusEnum = pgMinisty.VersionStatusEnum,
                ChangeActionEnum = pgMinisty.ChangeActionEnum,
                CreationDate = pgMinisty.CreationDate,
                ModificationDate = pgMinisty.ModificationDate,
                ModifiedById = pgMinisty.ModifiedById,
                ApprovalDate = pgMinisty.ApprovalDate,
                ApprovedById = pgMinisty.ApprovedById,
                CreatedById = pgMinisty.CreatedById,
                PageContactId = pgMinisty.PageContactId,
                ArAddress = pgMinisty.ArAddress,
                ArMapTitle = pgMinisty.ArMapTitle,
                PhoneNumber = pgMinisty.PhoneNumber,
                Order = pgMinisty.Order,
                MapUrl = pgMinisty.MapUrl,
                FaxNumber = pgMinisty.FaxNumber,
                ArPageName = pgMinisty.ArPageName,
                ArParticipateTitle = pgMinisty.ArParticipateTitle,
                EmailParticipateEmail = pgMinisty.EmailParticipateEmail,
                EnAddress = pgMinisty.EnAddress,
                EnMapTitle = pgMinisty.EnMapTitle,
                EnPageName = pgMinisty.EnPageName,
                EnParticipateTitle = pgMinisty.EnParticipateTitle,
                FormParticipateActive = pgMinisty.FormParticipateActive,
                SeoDescriptionAR = pgMinisty.SeoDescriptionAR,
                SeoTwitterCardEN = pgMinisty.SeoTwitterCardEN,
                SeoTwitterCardAR = pgMinisty.SeoTwitterCardAR,
                SeoTitleEN = pgMinisty.SeoTitleEN,
                SeoTitleAR = pgMinisty.SeoTitleAR,
                SeoOgTitleEN = pgMinisty.SeoOgTitleEN,
                SeoOgTitleAR = pgMinisty.SeoOgTitleAR,
                SeoDescriptionEN = pgMinisty.SeoDescriptionEN,
                PageRouteVersionId = pgMinisty.PageRouteVersionId
            };

            return viewModel;
        }

        //public static PageSectionCardVersion MapToSectionCardVersion(this SectionCardEditViewModel sectionCardEditViewModel)
        //{
        //    PageSectionCardVersion pageContact = new PageSectionCardVersion()
        //    {
        //        Id = sectionCardEditViewModel.Id,
        //        EnTitle = sectionCardEditViewModel.EnTitle,
        //        ArTitle = sectionCardEditViewModel.ArTitle,
        //        EnDescription = sectionCardEditViewModel.EnDescription,
        //        ArDescription = sectionCardEditViewModel.ArDescription,
        //        EnImageAlt = sectionCardEditViewModel.EnImageAlt,
        //        ArImageAlt = sectionCardEditViewModel.ArImageAlt,
        //        Order = sectionCardEditViewModel.Order,
        //        IsActive = sectionCardEditViewModel.IsActive,
        //        ImageUrl = sectionCardEditViewModel.ImageUrl,
        //        FileUrl = sectionCardEditViewModel.FileUrl,
        //        PageSectionVersionId = sectionCardEditViewModel.SectionVersionId,
        //        IsDeleted = sectionCardEditViewModel.IsDeleted,
        //        CreatedById = sectionCardEditViewModel.CreatedById,
        //        CreationDate = sectionCardEditViewModel.CreationDate.Value
        //    };

        //    return pageContact;
        //}
    }
}

