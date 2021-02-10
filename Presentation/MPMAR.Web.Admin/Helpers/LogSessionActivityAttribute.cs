using Microsoft.AspNetCore.Mvc.Filters;
using MPMAR.Business;
using MPMAR.Common;
using MPMAR.Common.Interfaces;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Helpers
{
    public class LogSessionActivityAttribute : ActionFilterAttribute
    {
        public static IEventLogger<LogSessionActivityAttribute> _eventLogger;
        
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
               
                _eventLogger.LogInfoEvent(filterContext.HttpContext.User.Identity.Name, Common.ActivityEnum.Add, filterContext.Controller.ToString(), filterContext.ActionDescriptor.DisplayName.ToString());

            }
            catch (Exception)
            {

                throw;
            }

            base.OnActionExecuted(filterContext);
        }
       
    }
}
