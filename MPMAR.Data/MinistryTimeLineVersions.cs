using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for MinistryTimeLineVersion table which form MinistryTimeLineVersion object used in MinistryTimeLine screens
    /// </summary>
    public class MinistryTimeLineVersions : PageSeoVersion
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
        public DateTime? EndDate { get; set; }
        [MaxLength(30)]
        public string PeriodAr { get; set; }
        [MaxLength(30)]
        public string PeriodEn { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Email { get; set; }
        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? MinistryTimeLineId { get; set; }

        public int? FormerMinistriesPageInfoVersionsId { get; set; }


        #region Navigation Properties
        public int StatusId { get; set; }

        public Status Status { get; set; }
        public MinistryTimeLine MinistryTimeLine { get; set; }
        public FormerMinistriesPageInfoVersions FormerMinistriesPageInfoVersions { get; set; }

        #endregion
    }
}
