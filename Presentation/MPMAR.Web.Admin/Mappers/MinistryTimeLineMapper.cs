using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class MinistryTimeLineMapper 
    {


        public static MinistryTimeLine MapToMinistryTimeLine(this MinistryTimeLineViewModel sectionCardCreateViewModel)
        {
            MinistryTimeLine pageSectionVersion = new MinistryTimeLine();
            pageSectionVersion.EnName= sectionCardCreateViewModel.EnName;
            pageSectionVersion.ArName= sectionCardCreateViewModel.ArName;
            pageSectionVersion.EnDescription = sectionCardCreateViewModel.EnDescription;
            pageSectionVersion.ArDescription = sectionCardCreateViewModel.ArDescription;
            pageSectionVersion.ProfileImageUrl = sectionCardCreateViewModel.ProfileImageUrl;
            pageSectionVersion.Order = sectionCardCreateViewModel.Order;
            pageSectionVersion.IsActive = sectionCardCreateViewModel.IsActive;
            pageSectionVersion.StartDate = sectionCardCreateViewModel.StartDate;
            pageSectionVersion.EndDate = sectionCardCreateViewModel.EndDate;
            pageSectionVersion.EventSocialLinks = sectionCardCreateViewModel.EventSocialLinks;
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
            //else
            //    pageSectionVersion.Id = 5;
            return pageSectionVersion;
        }

        public static MinistryTimeLineViewModel MapToEventdViewModel(this MinistryTimeLine sectionCardVersion)
        {
            MinistryTimeLineViewModel viewModel = new MinistryTimeLineViewModel()
            {
                Id = sectionCardVersion.Id,
                EnName = sectionCardVersion.EnName,
                ArName = sectionCardVersion.ArName,
                EnDescription = sectionCardVersion.EnDescription,
                ArDescription = sectionCardVersion.ArDescription,
                Order = sectionCardVersion.Order,
                IsActive = sectionCardVersion.IsActive,
                ProfileImageUrl = sectionCardVersion.ProfileImageUrl,
                StartDate = sectionCardVersion.StartDate,
                EndDate = sectionCardVersion.EndDate,
                EventSocialLinks = sectionCardVersion.EventSocialLinks,


                SeoTitleEN = sectionCardVersion.SeoTitleEN,
                SeoTitleAR = sectionCardVersion.SeoTitleAR,
                SeoDescriptionEN = sectionCardVersion.SeoDescriptionEN,
                SeoDescriptionAR = sectionCardVersion.SeoDescriptionAR,
                SeoOgTitleEN = sectionCardVersion.SeoOgTitleEN,
                SeoOgTitleAR = sectionCardVersion.SeoOgTitleAR,
                SeoTwitterCardEN = sectionCardVersion.SeoTwitterCardEN,
                SeoTwitterCardAR = sectionCardVersion.SeoTwitterCardAR,

                 IsDeleted = sectionCardVersion.IsDeleted,
                CreationDate = sectionCardVersion.CreationDate,
                CreatedById = sectionCardVersion.CreatedById
               


            };

            return viewModel;
        }


    }
}



