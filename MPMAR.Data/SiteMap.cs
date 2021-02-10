using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for SiteMap table which form SiteMap object used in SiteMap screens
    /// </summary>
    public class SiteMap : PageSeo
    {
        public int Id { get; set; }
        public string EnContent { get; set; }
        public string ArContent { get; set; }
        public int? StatusId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}