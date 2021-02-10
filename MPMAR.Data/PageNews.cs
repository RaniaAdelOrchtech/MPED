using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nest;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PageNews table which form PageNews object used in PageNews screens
    /// </summary>
    public class PageNews: ActionInfo
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
        public int? PageRouteId { get; set; }
        [Nested(IncludeInParent = true)]
        public ICollection<NewsTypesForNews> NewsTypesForNews { get; set; }
        public ICollection<PageNewsVersion> PageNewsVersions { get; set; }
        [ForeignKey("PageRouteId")]
        public PageRoute PageRoute { get; set; }
        public DateTime? Date { get; set; }



    }
}
