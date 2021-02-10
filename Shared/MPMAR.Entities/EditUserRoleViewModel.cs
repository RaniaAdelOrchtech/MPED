using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MPMAR.Entities
{
    public class EditUserRoleViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public bool IsFirstLogin { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool EmailConfirmed { get; set; }
        [Display(Name = "Is Super Admin")]
        public bool IsSuperAdmin { get; set; }


    }
}
