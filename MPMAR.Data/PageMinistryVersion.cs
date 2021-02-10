using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PageMinistryVersion table which form PageMinistryVersion object used in PageMinistry screens
    /// </summary>
    public class PageMinistryVersion : PageSeoVersion
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public Nullable<int> Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> NavItemId { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public Nullable<int> StatusId { get; set; }
        public string ArContent { get; set; }
        public string EnContent { get; set; }
        public bool IsHeading { get; set; }
        public bool IsSection { get; set; }
        public bool IsDobulQuote { get; set; }
        public int? PageRouteId { get; set; }

        public string ImageUrl { get; set; }
        public string EnImageUrl { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public int? PageMinistryId { get; set; }

        public virtual PageRoute PageRoute { get; set; }
        public virtual PageMinistry PageMinistry { get; set; }
    }
}
