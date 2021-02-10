using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for LeftMenuItem table which form LeftMenuItem object used in LeftMenuItem screens
    /// </summary>
    public class LeftMenuItem : ActionInfoVersion
    {
        [Key]
        public int Id { get; set; }
        public string LeftMenuType { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public string Link { get; set; }
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string ImagePath { get; set; }
        
    }
}
