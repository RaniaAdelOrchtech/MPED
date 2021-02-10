using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Data;
using MPMAR.Web.Admin.Models;
using MPMAR.Business.Interfaces;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBEUsersPrivilegesRepository _bEUsersPrivilegesRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IBEUsersPrivilegesRepository bEUsersPrivilegesRepository, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _bEUsersPrivilegesRepository = bEUsersPrivilegesRepository;
            _userManager = userManager;
        }
        /// <summary>
        /// get home page index
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Index()
        {
            //var userId = _userManager.GetUserId(HttpContext.User);
            //ViewBag.Userprivileges = _bEUsersPrivilegesRepository.GetUserPrivileges(userId).ToList();
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
        [Route("/admin")]
        [Authorize]
        public IActionResult Admin()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
