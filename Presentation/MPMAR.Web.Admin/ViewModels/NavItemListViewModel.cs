using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class NavItemListViewModel : ActionInfoVersion
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

        public ChangeActionEnum? ChangeActionEnum { get; set; }
        public VersionStatusEnum? VersionStatusEnum { get; set; }



        public int StatusId { get; set; }

        public int? NavItemId { get; set; }

        public int? ParentNavItemId { get; set; }


    }
}
