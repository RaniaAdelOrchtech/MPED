using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPMAR.Business.Interfaces;
using MPMAR.Data;

namespace MPMAR.Web.Site.Controllers
{
    public class EgyptVisionController : BaseController
    {
        private readonly ApplicationDbContext _dataAccessService;
        private readonly IConfiguration _configuration;
        private readonly IPageRouteRepository _pageRouteRepository;
        public EgyptVisionController(ApplicationDbContext dataAccessService, IConfiguration configuration, IPageRouteRepository pageRouteRepository)
        {
            _dataAccessService = dataAccessService;
            _configuration = configuration;
            _pageRouteRepository = pageRouteRepository;
        }
        /// <summary>
        /// get EgyptVision index page
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public IActionResult Index(string lang)
        {
            var pageRoute = _pageRouteRepository.GetByControllerName(nameof(EgyptVisionController)[0..^10]);
            if (pageRoute == null || !pageRoute.IsActive || pageRoute.IsDeleted)
            {
                return View("Error");
            }
            var items = _dataAccessService.EgyptVision.Where(i => i.IsDeleted != true && i.IsActive == true).OrderBy(i => i.Order).ToList();
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            foreach (var item in items)
            {
                item.ArImagePath = imageBaseURL + item.ArImagePath.Replace(" ", "%20");
                item.EnImagePath = imageBaseURL + item.EnImagePath.Replace(" ", "%20");
            }

            SetUpSEO(lang, pageRoute);
            if (lang == null || lang.ToLower() == "ar")
            {
                ViewBag.Title = pageRoute.ArName;
                ViewBag.Nav = pageRoute.NavItem.ArName;
            }
            else
            {
                ViewBag.Title = pageRoute.EnName;
                ViewBag.Nav = pageRoute.NavItem.EnName;
            }
            return View(items);
        }
    }
}