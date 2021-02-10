using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.ViewModels
{
    public class PageEventViewModel:PageSeo
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
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public int VerId { get; set; }
        public VersionStatusEnum VersionStatusEnum { get; set; }
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
        public bool ShowInHome { get; set; }
        public object PageEventId { get; set; }
        public string StatusName { get { return VersionStatusEnum.ToString(); } }
    }
}
