using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Web.Site.Models;

namespace MPMAR.Web.Site.Controllers
{
    public class MinistrySpeechController : BaseController
    {
        private readonly ApplicationDbContext _dataAccessService;
        private readonly IConfiguration _configuration;
        private readonly IPageRouteRepository _pageRouteRepository;

        public MinistrySpeechController(ApplicationDbContext dataAccessService, IConfiguration configuration, IPageRouteRepository pageRouteRepository)
        {
            _dataAccessService = dataAccessService;
            _configuration = configuration;
            _pageRouteRepository = pageRouteRepository;
        }
        /// <summary>
        /// get MinistrySpeech page index
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public IActionResult Index(string lang)
        {
            var pageRoute = _pageRouteRepository.GetByControllerName(nameof(MinistrySpeechController)[0..^10]);
            if (pageRoute == null || !pageRoute.IsActive || pageRoute.IsDeleted)
            {
                return View("Error");
            }
            MinistryVisionViewModel objView = new MinistryVisionViewModel();
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            var items = _dataAccessService.PageMinistry.Where(i => i.IsDeleted != true && i.IsActive == true).OrderBy(i => i.Id).ToList();
            foreach (var item in items)
            {
                if (item.ImageUrl != null)
                    item.ImageUrl = imageBaseURL + item.ImageUrl.Replace(" ", "%20");

                if (item.EnImageUrl != null)
                    item.EnImageUrl = imageBaseURL + item.EnImageUrl.Replace(" ", "%20");
            }
            var IdAndTitle = _dataAccessService.PageMinistry.Where(x => x.PageRoute.SectionName == "Minister's Speech").Select(i => i.PageRouteId).Distinct().ToList();
            objView.PageMinistry = items;
            objView.PageRoute = _dataAccessService.PageRoutes.Where(i => IdAndTitle.Contains(Convert.ToInt32(i.Id))).ToList();
         
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
            objView = RemoveTagsFromDescription(objView);
            return View(objView);
        }

        /// <summary>
        /// remove <p> and </p> from description
        /// </summary>
        /// <param name="viewModel">MinistryVisionViewModel</param>
        /// <returns></returns>
        private MinistryVisionViewModel RemoveTagsFromDescription(MinistryVisionViewModel viewModel)
        {
            foreach (var item in viewModel.PageMinistry)
            {
                if (item.EnContent != null)
                {
                    item.EnContent = item.EnContent.Replace("<p>", "");
                    item.EnContent = item.EnContent.Replace("</p>", "");
                }
                if (item.ArContent != null)
                {
                    item.ArContent = item.ArContent.Replace("<p>", "");
                    item.ArContent = item.ArContent.Replace("</p>", "");
                }
            }



            return viewModel;
        }
    }
}