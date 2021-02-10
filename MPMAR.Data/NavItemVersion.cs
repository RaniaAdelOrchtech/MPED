using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for NavItemVersion table which form NavItemVersion object used in NavItem screens
    /// </summary>
    public class NavItemVersion : ActionInfoVersion
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

        public int? StatusId { get; set; }
        public Status Status { get; set; }


        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }


        #region Navigation Properties
       
        public NavItem NavItem { get; set; }

        public int? NavItemId { get; set; }

        public int? ParentNavItemId { get; set; }
        public NavItem ParentNavItem { get; set; }
        #endregion
    }
}
