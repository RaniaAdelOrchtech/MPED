using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for MinistryTimeLine table which form MinistryTimeLine object used in MinistryTimeLine screens
    /// </summary>
    public class MinistryTimeLine : PageSeoVersion
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
        public string EnDescription { get; set; }
        public string ArDescription { get; set; }
        public string ProfileImageUrl { get; set; }
       
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string EventSocialLinks { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate  { get; set; }
        [MaxLength(30)]
        public string PeriodAr { get; set; }
        [MaxLength(30)]
        public string PeriodEn { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Email { get; set; }
        public int? FormerMinistriesPageInfoId { get; set; }
        #region Navigation Properties
        public int StatusId { get; set; }

        public Status Status { get; set; }
        public FormerMinistriesPageInfo FormerMinistriesPageInfo { get; set; }

        #endregion
    }
}
