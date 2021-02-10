using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Data
{
    public class BEUsersPrivileges
    {
        public int Id { get; set; }
        public int? PageRouteId { get; set; }
        public PrivilegesPageType PageTypeId { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        public bool CanView { get; set; } = false;
        public bool CanAdd { get; set; } = false;
        public bool CanEdit { get; set; } = false;
        public bool CanDelete { get; set; } = false;
        public bool CanApprove { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public string PageName { get; set; }
        public PageRoute PageRoute { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
