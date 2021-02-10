using MPMAR.Business.ViewModels;
using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class PageEventVersionMapper
    {


        public static PageEventVersions MapToPageEventVersion(this PageEventVersionViewModel sectionCardCreateViewModel)
        {
            PageEventVersions pageSectionVersion = new PageEventVersions();
            pageSectionVersion.EnTitle = sectionCardCreateViewModel.EnTitle;
            pageSectionVersion.ArTitle = sectionCardCreateViewModel.ArTitle;
            pageSectionVersion.EnDescription = sectionCardCreateViewModel.EnDescription;
            pageSectionVersion.ArDescription = sectionCardCreateViewModel.ArDescription;
            pageSectionVersion.EnImageAlt = sectionCardCreateViewModel.EnImageAlt;
            pageSectionVersion.ArImageAlt = sectionCardCreateViewModel.ArImageAlt;
            pageSectionVersion.EnUrl = sectionCardCreateViewModel.Url;
            pageSectionVersion.ArUrl = sectionCardCreateViewModel.DetailUrl;
            pageSectionVersion.Order = sectionCardCreateViewModel.Order;
            pageSectionVersion.IsActive = sectionCardCreateViewModel.IsActive;
            pageSectionVersion.EnAddress = sectionCardCreateViewModel.EnAddress;
            pageSectionVersion.ArAddress = sectionCardCreateViewModel.ArAddress;

            pageSectionVersion.EventLocation = sectionCardCreateViewModel.EventLocation;
            pageSectionVersion.EventLocationUrl = sectionCardCreateViewModel.EventLocationUrl;
            pageSectionVersion.EventDateColor = sectionCardCreateViewModel.EventDateColor;

            //if (sectionCardCreateViewModel.EventDateRange != null)
            //{
            pageSectionVersion.EventStartDate = sectionCardCreateViewModel.EventStartDate;
            pageSectionVersion.EventEndDate = sectionCardCreateViewModel.EventEndDate;
            //}



            pageSectionVersion.EventSocialLinks = sectionCardCreateViewModel.EventSocialLinks;


            pageSectionVersion.SeoTitleEN = sectionCardCreateViewModel.SeoTitleEN;
            pageSectionVersion.SeoTitleAR = sectionCardCreateViewModel.SeoTitleAR;
            pageSectionVersion.SeoDescriptionEN = sectionCardCreateViewModel.SeoDescriptionEN;
            pageSectionVersion.SeoDescriptionAR = sectionCardCreateViewModel.SeoDescriptionAR;
            pageSectionVersion.SeoOgTitleEN = sectionCardCreateViewModel.SeoOgTitleEN;
            pageSectionVersion.SeoOgTitleAR = sectionCardCreateViewModel.SeoOgTitleAR;
            pageSectionVersion.SeoTwitterCardEN = sectionCardCreateViewModel.SeoTwitterCardEN;
            pageSectionVersion.SeoTwitterCardAR = sectionCardCreateViewModel.SeoTwitterCardAR;


            pageSectionVersion.PageRouteVersionId = sectionCardCreateViewModel.PageRouteId;
            if (sectionCardCreateViewModel.Id > 0)
                pageSectionVersion.Id = sectionCardCreateViewModel.Id;
            //else
            //    pageSectionVersion.Id = 5;
            return pageSectionVersion;
        }

        //public static PageEventVersionViewModel MapToEventdViewModel(this PageEventVersions sectionCardVersion)
        //{
        //    PageEventVersionViewModel viewModel = new PageEventVersionViewModel()
        //    {
        //        Id = sectionCardVersion.Id,
        //        EnTitle = sectionCardVersion.EnTitle,
        //        ArTitle = sectionCardVersion.ArTitle,
        //        EnDescription = sectionCardVersion.EnDescription,
        //        ArDescription = sectionCardVersion.ArDescription,
        //        EnImageAlt = sectionCardVersion.EnImageAlt,
        //        ArImageAlt = sectionCardVersion.ArImageAlt,
        //        Order = sectionCardVersion.Order,
        //        IsActive = sectionCardVersion.IsActive,
        //        Url = sectionCardVersion.EnUrl,
        //        DetailUrl = sectionCardVersion.ArUrl,
        //        EnAddress = sectionCardVersion.EnAddress,
        //        ArAddress = sectionCardVersion.ArAddress,


        //        EventLocation = sectionCardVersion.EventLocation,
        //        EventLocationUrl = sectionCardVersion.EventLocationUrl,
        //        EventDateColor = sectionCardVersion.EventDateColor,

        //        EventStartDate = sectionCardVersion.EventStartDate,
        //        EventEndDate = sectionCardVersion.EventEndDate,
        //        EventSocialLinks = sectionCardVersion.EventSocialLinks,


        //        SeoTitleEN = sectionCardVersion.SeoTitleEN,
        //        SeoTitleAR = sectionCardVersion.SeoTitleAR,
        //        SeoDescriptionEN = sectionCardVersion.SeoDescriptionEN,
        //        SeoDescriptionAR = sectionCardVersion.SeoDescriptionAR,
        //        SeoOgTitleEN = sectionCardVersion.SeoOgTitleEN,
        //        SeoOgTitleAR = sectionCardVersion.SeoOgTitleAR,
        //        SeoTwitterCardEN = sectionCardVersion.SeoTwitterCardEN,
        //        SeoTwitterCardAR = sectionCardVersion.SeoTwitterCardAR,

        //        PageRouteId = sectionCardVersion.PageRouteVersionId,
        //        // FileUrl = sectionCardVersion.FileUrl,
        //        //SectionVersionId = sectionCardVersion.PageSectionVersionId,
        //        IsDeleted = sectionCardVersion.IsDeleted,
        //        CreationDate = sectionCardVersion.CreationDate,
        //        CreatedById = sectionCardVersion.CreatedById
        //        //PageRouteVersionId = sectionCardVersion.PageRouteVersionId


        //    };

        //    return viewModel;
        //}


    }
}



