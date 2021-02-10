using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for FooterMenuTitle table which form FooterMenuTitle object used in FooterMenuTitle screens
    /// </summary>
    public class FooterMenuTitle : ActionInfoVersion
    {
        [Key]
        public int Id { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<FooterMenuItem> FooterMenuItems { get; set; }


    }
}
