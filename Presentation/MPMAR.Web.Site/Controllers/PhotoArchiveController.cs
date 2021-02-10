using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Web.Site.ViewModels;

namespace MPMAR.Web.Site.Controllers
{
    public class PhotoArchiveController : BaseController
    {
        private readonly ApplicationDbContext _dataAccessService;
        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly IPhotoArchiveElasticSearchService _photoArchiveElasticSearchService;
        private readonly IPhotoArchiveRepository _photoArchiveRepository;
        private readonly IConfiguration _configuration;

        public PhotoArchiveController(ApplicationDbContext dataAccessService, IPageRouteRepository pageRouteRepository, IPhotoArchiveElasticSearchService photoArchiveElasticSearchService, IPhotoArchiveRepository photoArchiveRepository, IConfiguration configuration)
        {
            _dataAccessService = dataAccessService;
            _pageRouteRepository = pageRouteRepository;
            _photoArchiveElasticSearchService = photoArchiveElasticSearchService;
            _photoArchiveRepository = photoArchiveRepository;
            _configuration = configuration;
        }
        /// <summary>
        /// get PhotoArchive page index
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public IActionResult Index(string lang)
        {
            var pageRoute = _pageRouteRepository.GetByControllerName(nameof(PhotoArchiveController)[0..^10]);
            if (pageRoute == null || !pageRoute.IsActive || pageRoute.IsDeleted)
            {
                return View("Error");
            }
            var items = _dataAccessService.PhotoArchive.Where(i => i.IsDeleted != true && i.IsActive == true).OrderBy(i => i.Order).ToList();
            //GetMenuItemsAsync(HttpContext.User);
            SetUpSEO(lang, pageRoute);
            var typeObj = new PhotoArchiveType();
            typeObj.EnName = "all";
            typeObj.ArName = "كل";
            if (lang == null || lang.ToLower() == "ar")
            {
                ViewBag.Title = pageRoute.ArName;
                ViewBag.Nav = pageRoute.NavItem.ArName;
                ViewBag.typeTextValue = typeObj.ArName;
            }
            else
            {
                ViewBag.Title = pageRoute.EnName;
                ViewBag.Nav = pageRoute.NavItem.EnName;
                ViewBag.typeTextValue = typeObj.EnName;
            }
            ViewBag.typeValue = typeObj.ArName;
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            foreach (var item in items)
            {
                if (item.ImageUrl != null)
                    item.ImageUrl = imageBaseURL + item.ImageUrl.Replace(" ", "%20");
            }

            var archiveTypes = _photoArchiveRepository.Get().Select(m => new { ArName = m.ArPhotoArchiveType, EnName = m.EnPhotoArchiveType })
                           .Distinct().Select(x => new PhotoArchiveType { ArName = x.ArName, EnName = x.EnName }).ToList();
            var photoArchive = new PhotoArchiveViewModel()
            {
                PhotoArchives = items,
                PhotoArchiveTypes = archiveTypes

            };
            return View(photoArchive);

        }




        [HttpGet]
        public IActionResult SearchArchive([FromQuery] string searchText, [FromQuery] string type, [FromQuery] string lang)
        {
            var photoArchives = _photoArchiveElasticSearchService.Find(searchText, type);
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            foreach (var item in photoArchives.Result)
            {
                if (item.ImageUrl != null)
                    item.ImageUrl = imageBaseURL + item.ImageUrl.Replace(" ", "%20");
                item.ArPhotoArchiveType = item.ArPhotoArchiveType?.Trim();
                item.EnPhotoArchiveType = item.EnPhotoArchiveType?.Trim();
            }


            var archiveTypes = _photoArchiveRepository.Get().Select(m => new { ArName = m.ArPhotoArchiveType.Trim(), EnName = m.EnPhotoArchiveType.Trim() })
                      .Distinct().Select(x => new PhotoArchiveType { ArName = x.ArName, EnName = x.EnName }).ToList();


            var photoArchive = new PhotoArchiveViewModel()
            {
                PhotoArchives = photoArchives.Result.ToList(),
                PhotoArchiveTypes = archiveTypes
            };

            ViewBag.PhotoArchiveSearchText = searchText;
            var pageRoute = _pageRouteRepository.GetByControllerName(nameof(PhotoArchiveController)[0..^10]);
            var typeObj = new PhotoArchiveType();
            if (string.IsNullOrWhiteSpace(type) || type == "كل")
            {
                typeObj.EnName = "all";
                typeObj.ArName = "كل";

            }
            else
            {

                typeObj = archiveTypes.FirstOrDefault(x => x.ArName == type);
            }
            if (lang == null || lang.ToLower() == "ar")
            {
                ViewBag.Title = pageRoute.ArName;
                ViewBag.Nav = pageRoute.NavItem.ArName;
                ViewBag.typeTextValue = typeObj.ArName;

            }
            else
            {
                ViewBag.Title = pageRoute.EnName;
                ViewBag.Nav = pageRoute.NavItem.EnName;
                ViewBag.typeTextValue = typeObj.EnName;
            }
            ViewBag.typeValue = typeObj.ArName;
            return View("Index", photoArchive);
        }
    }
}