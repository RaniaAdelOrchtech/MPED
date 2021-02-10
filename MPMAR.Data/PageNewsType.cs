using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for PageNewsType table which form PageNewsType object used in PageNews screens
    /// </summary>
    public class PageNewsType
    { 
        public int Id { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreationDate { get; set; }

        [MaxLength(450)]
        public string CreatedById { get; set; }
    }
}
