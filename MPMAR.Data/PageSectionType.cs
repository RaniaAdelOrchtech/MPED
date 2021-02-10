using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PageSectionType table which form PageSectionType object used in PageSection screens
    /// </summary>
    public class PageSectionType
    {
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
        public string MediaType { get; set; }
        public string ThemeImage { get; set; }
        public bool HasCards { get; set; }

        #region Navigation Properties
        public List<PageSection> PageSections { get; set; }
        public List<PageSectionVersion> PageSectionVersions { get; set; }
        #endregion
    }
}
