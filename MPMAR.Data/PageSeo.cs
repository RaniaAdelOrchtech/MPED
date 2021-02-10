using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for search engine optmization which help classes to inhirit from this class
    /// </summary>
   public class PageSeo : ActionInfo
    {
        [Display(Name = "Seo English Page Title")]
        public string SeoTitleEN { get; set; }

        [Display(Name = "Seo Arabic Page Title")]
        public string SeoTitleAR { get; set; }

        [Display(Name = "Seo English Page Description")]
        public string SeoDescriptionEN { get; set; }

        [Display(Name = "Seo Arabic Page Description")]
        public string SeoDescriptionAR { get; set; }

        [Display(Name = "Seo English Facebook Og Title")]
        public string SeoOgTitleEN { get; set; }

        [Display(Name = "Seo Arabic Facebook Og Title")]
        public string SeoOgTitleAR { get; set; }

        [Display(Name = "Seo English Twitter Card")]
        public string SeoTwitterCardEN { get; set; }

        [Display(Name = "Seo Arabic Twitter Card")]
        public string SeoTwitterCardAR { get; set; }
    }
}
