using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Common;
using MPMAR.Web.Admin.Helpers;

namespace MPMAR.Web.Admin.Controllers
{
    public class HP_EventsController : Controller
    {
        [Auth2FactorFilter]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}