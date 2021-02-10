using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PageEventVersion table which form PageEventVersion object used in PageEvent screens
    /// </summary>
    public class PageEventVersions : PageSeoVersion
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
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public DateTime? Date { get; set; }

        #region Navigation Properties
        public int PageRouteVersionId { get; set; }
        [ForeignKey("PageRouteVersionId")]
        public PageRouteVersion PageRouteVersion { get; set; }

        public int? PageEventId { get; set; }
        [ForeignKey("PageEventId")]
        public PageEvent PageEvent { get; set; }

        #endregion
    }
}
