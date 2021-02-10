using MPMAR.Business.ViewModels;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewModels
{
    public class ViewPageEventVersions : PageSeoVersion
    {
        public int Id { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public string EnDescription { get; set; }
        public string ArDescription { get; set; }
        //public string Url { get; set; }
        public string EnImageAlt { get; set; }
        public string ArImageAlt { get; set; }
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public string EnUrl { get; set; }
        public string ArUrl { get; set; }

        public string EnAddress { get; set; }
        public string ArAddress { get; set; }
        public string EventLocation { get; set; }

        public string EventLocationUrl { get; set; }

        public string EventDateColor { get; set; }

        public string EventCaption { get; set; }
        public string EventSocialLinks { get; set; }
        public decimal? EventLon { get; set; }


        public decimal? EventLat { get; set; }

        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }

        public string MainEnTitle { get; set; }
        public string MainArTitle { get; set; }
        public string PageEnTitle { get; set; }
        public string PageArTitle { get; set; }
        public string ContollerName { get; set; }

    }

    public class ViewPageEvents
    {
        public List<ViewPageEventVersions> ViewPageEventVersions { get; set; }
        public string MainEnTitle { get; set; }
        public string MainArTitle { get; set; }
        public string PageEnTitle { get; set; }
        public string PageArTitle { get; set; }
        public string ContollerName { get; set; }
    }
    public static class ViewPageEventVersionMapper
    {

        public static List<ViewPageEventVersions> MapToPageEventVersionsViewModel(this IEnumerable<PageEventVersions> pageContact)
        {
            return pageContact.Select(sectionCardVersion => new ViewPageEventVersions
            {
                Id = sectionCardVersion.Id,
                EnTitle = sectionCardVersion.EnTitle,
                ArTitle = sectionCardVersion.ArTitle,
                EnDescription = sectionCardVersion.EnDescription,
                ArDescription = sectionCardVersion.ArDescription,
                EnImageAlt = sectionCardVersion.EnImageAlt,
                ArImageAlt = sectionCardVersion.ArImageAlt,
                Order = sectionCardVersion.Order,
                IsActive = sectionCardVersion.IsActive,
                EnAddress = sectionCardVersion.EnAddress,
                ArAddress = sectionCardVersion.ArAddress,
                EventLocation = sectionCardVersion.EventLocation,
                EventLocationUrl = sectionCardVersion.EventLocationUrl,
                EventDateColor = sectionCardVersion.EventDateColor,

                EventStartDate = Convert.ToDateTime(sectionCardVersion.EventStartDate),
                EventEndDate = Convert.ToDateTime(sectionCardVersion.EventEndDate),
                EventSocialLinks = sectionCardVersion.EventSocialLinks,

                EnUrl = sectionCardVersion.EnUrl,
                ArUrl = sectionCardVersion.ArUrl,
                SeoTitleEN = sectionCardVersion.SeoTitleEN,
                SeoTitleAR = sectionCardVersion.SeoTitleAR,
                SeoDescriptionEN = sectionCardVersion.SeoDescriptionEN,
                SeoDescriptionAR = sectionCardVersion.SeoDescriptionAR,
                SeoOgTitleEN = sectionCardVersion.SeoOgTitleEN,
                SeoOgTitleAR = sectionCardVersion.SeoOgTitleAR,
                SeoTwitterCardEN = sectionCardVersion.SeoTwitterCardEN,
                SeoTwitterCardAR = sectionCardVersion.SeoTwitterCardAR,
                // FileUrl = sectionCardVersion.FileUrl,
                //SectionVersionId = sectionCardVersion.PageSectionVersionId,
                IsDeleted = sectionCardVersion.IsDeleted,
                CreationDate = sectionCardVersion.CreationDate,
                CreatedById = sectionCardVersion.CreatedById
                //PageRouteVersionId = sectionCardVersion.PageRouteVersionId




            }).ToList();
        }

        public static ViewPageEventVersions MapToPageEventVersionViewModel(this PageEventVersionViewModel sectionCardVersion)
        {
            return new ViewPageEventVersions
            {
                Id = sectionCardVersion.Id,
                EnTitle = sectionCardVersion.EnTitle,
                ArTitle = sectionCardVersion.ArTitle,
                EnDescription = sectionCardVersion.EnDescription,
                ArDescription = sectionCardVersion.ArDescription,
                EnImageAlt = sectionCardVersion.EnImageAlt,
                ArImageAlt = sectionCardVersion.ArImageAlt,
                EnUrl = sectionCardVersion.Url,
                ArUrl = sectionCardVersion.Url,
                Order = sectionCardVersion.Order,
                IsActive = sectionCardVersion.IsActive,
                EnAddress = sectionCardVersion.EnAddress,
                ArAddress = sectionCardVersion.ArAddress,
                EventLocation = sectionCardVersion.EventLocation,
                EventLocationUrl = sectionCardVersion.EventLocationUrl,
                EventDateColor = sectionCardVersion.EventDateColor,

                EventStartDate = Convert.ToDateTime(sectionCardVersion.EventStartDate),
                EventEndDate = Convert.ToDateTime(sectionCardVersion.EventEndDate),
                EventSocialLinks = sectionCardVersion.EventSocialLinks,


                SeoTitleEN = sectionCardVersion.SeoTitleEN,
                SeoTitleAR = sectionCardVersion.SeoTitleAR,
                SeoDescriptionEN = sectionCardVersion.SeoDescriptionEN,
                SeoDescriptionAR = sectionCardVersion.SeoDescriptionAR,
                SeoOgTitleEN = sectionCardVersion.SeoOgTitleEN,
                SeoOgTitleAR = sectionCardVersion.SeoOgTitleAR,
                SeoTwitterCardEN = sectionCardVersion.SeoTwitterCardEN,
                SeoTwitterCardAR = sectionCardVersion.SeoTwitterCardAR,
                // FileUrl = sectionCardVersion.FileUrl,
                //SectionVersionId = sectionCardVersion.PageSectionVersionId,
                IsDeleted = sectionCardVersion.IsDeleted,
                CreationDate = sectionCardVersion.CreationDate,
                CreatedById = sectionCardVersion.CreatedById
                //PageRouteVersionId = sectionCardVersion.PageRouteVersionId




            };
        }

    }
}
