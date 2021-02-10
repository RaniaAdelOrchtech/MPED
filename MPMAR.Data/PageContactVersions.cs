using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PageContactVersion table which form PageContactVersion object used in PageContact screens
    /// </summary>
    public class PageContactVersions : PageSeo
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string ArParticipateTitle { get; set; }
        public string EnParticipateTitle { get; set; }

        public string EnPageName { get; set; }
        public string ArPageName { get; set; }
        public string ArMapTitle { get; set; }
        public string EnMapTitle { get; set; }
        public string ArAddress { get; set; }
        public string EnAddress { get; set; }
        public bool FormParticipateActive { get; set; }
        public string MapUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailParticipateEmail { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? PageContactId { get; set; }

        public PageContact PageContact { get; set; }

        public int? PageRouteVersionId { get; set; }
        public virtual PageRouteVersion PageRouteVersions { get; set; }
    }
}
