using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MPMAR.Web.Site.Controllers
{
    public class MinistryTimeLineController : Controller
    {
        /// <summary>
        /// get MinistryTimeLine page index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}