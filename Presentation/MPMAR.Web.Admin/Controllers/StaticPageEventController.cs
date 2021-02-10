using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Common.Helpers;
using MPMAR.Data;
using MPMAR.Web.Admin.Helpers;
using NToastNotify;
using MPMAR.Business;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class StaticPageEventController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}