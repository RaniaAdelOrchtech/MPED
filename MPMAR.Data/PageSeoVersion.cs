using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for search engine optmization version
    /// </summary>
    public class PageSeoVersion : ActionInfoVersion
    {
        public string SeoTitleEN { get; set; }
        public string SeoTitleAR { get; set; }
        public string SeoDescriptionEN { get; set; }
        public string SeoDescriptionAR { get; set; }
        public string SeoOgTitleEN { get; set; }
        public string SeoOgTitleAR { get; set; }
        public string SeoTwitterCardEN { get; set; }
        public string SeoTwitterCardAR { get; set; }
    }
}
