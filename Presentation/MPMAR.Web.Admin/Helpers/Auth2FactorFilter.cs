using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace MPMAR.Web.Admin.Helpers
{
    public class Auth2FactorFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Authorize 2-factor authentication
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userName = context.HttpContext.User.Identity.Name;
            var userManager = context.HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();
            var user = userManager.FindByNameAsync(userName).Result;
            if(user.isFirstLogin && !user.TwoFactorEnabled)
            {
                context.Result = new RedirectResult("~/Identity/Account/Login");
            }
        }
    }
}
