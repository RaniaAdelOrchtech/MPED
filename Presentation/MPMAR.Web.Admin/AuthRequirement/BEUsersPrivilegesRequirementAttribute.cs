using Microsoft.AspNetCore.Mvc;
using MPMAR.Data.Consts;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.AuthHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.AuthRequirement
{
    public class BEUsersPrivilegesRequirementAttribute : TypeFilterAttribute
    {
        public BEUsersPrivilegesRequirementAttribute(PrivilegesPageType pageType, PrivilegesActions[] pageActions, int pageId = -1) : base(typeof(BEUsersPrivilegesRequirementFilter))
        {
            Arguments = new object[] { new BEUsersPrivilegesRequirementModel(pageType, pageActions, pageId == -1 ? (int?)null : pageId) };
        }

        public class BEUsersPrivilegesRequirementModel
        {
            public PrivilegesPageType PageType { get; set; }
            public ICollection<PrivilegesActions>  PageActions { get; set; }
            public int? PageId { get; set; }

            public BEUsersPrivilegesRequirementModel(PrivilegesPageType pageType, ICollection<PrivilegesActions> pageActions, int? pageId = null)
            {
                PageType = pageType;
                PageActions = pageActions;
                PageId = pageId;
            }
        }

  
    }
}
