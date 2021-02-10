using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MPMAR.Data.Enums;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PageNewsVersion table which form PageNewsVersion object used in PageNews screens
    /// </summary>
    public class PageNewsVersion: ActionInfoVersion
    {
        public int Id { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public string EnShortDescription { get; set; }
        public string ArShortDescription { get; set; }
        public string EnDescription { get; set; }
        public string ArDescription { get; set; }
        public string Url { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int PageRouteVersionId { get; set; }
        public int? PageNewsId { get; set; }

        [ForeignKey("PageNewsId")]
        public PageNews PageNews { get; set; }

        public PageRouteVersion PageRouteVersion { get; set; }
        public DateTime? Date { get; set; }

        public ChangeActionEnum? ChangeActionEnum { get; set; }

        public VersionStatusEnum? VersionStatusEnum { get; set; }
        public ICollection<NewsTypesForNewsVersion> NewsTypesForNewsVersions { get; set; }
    }
}
