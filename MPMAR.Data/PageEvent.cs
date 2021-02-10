using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PageEvent table which form PageEvent object used in PageEvent screens
    /// </summary>
    public class PageEvent : PageSeo
    {
        public int Id { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public string EnDescription { get; set; }
        public string ArDescription { get; set; }
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

        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public bool ShowInHome { get; set; }

        #region Navigation Properties
        public int PageRouteId { get; set; }


        [ForeignKey("PageRouteId")]
        public PageRoute PageRoute { get; set; }
        public ICollection<PageEventVersions> PageEventVersions { get; set; }
        #endregion
    }
}
