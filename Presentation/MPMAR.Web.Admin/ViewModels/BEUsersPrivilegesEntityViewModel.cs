using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class BEUsersPrivilegesEntityViewModel
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
        public bool OldCanView { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public string PageName { get; set; }
        public PageRoute PageRoute { get; set; }

    }
}
