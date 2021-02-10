using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for NavItem table which form NavItem object used in NavItem screens
    /// </summary>
    public class NavItem : ActionInfo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Arabic Name")]
        public string ArName { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "English Name")]
        public string EnName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer number")]
        public int? Order { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        #region Navigation Properties


        public int? ParentNavItemId { get; set; }
        public NavItem ParentNavItem { get; set; }
        public ICollection<NavItem> NavItemList { get; set; }
        public ICollection<PageRoute> PageRoutes { get; set; }
        public List<PageRouteVersion> PageRoutesList { get; set; }
        #endregion
    }
}
