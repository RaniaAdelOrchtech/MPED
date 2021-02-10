using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Analytics.Data.Enums;
using MPMAR.Business.Interfaces;
using MPMAR.Data;

namespace MPMAR.Web.Site.Controllers
{
    public class AnalyticsController : BaseController
    {
        private readonly IPageRouteRepository _pageRouteRepository;

        public AnalyticsController(IPageRouteRepository pageRouteRepository)
        {
            _pageRouteRepository = pageRouteRepository;
        }
        /// <summary>
        /// get Analytics index page
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public IActionResult Index(string lang)
        {
            var pageRoute = _pageRouteRepository.GetByControllerName(nameof(AnalyticsController)[0..^10]);
            if (pageRoute == null || !pageRoute.IsActive || pageRoute.IsDeleted)
            {
                return View("Error");
            }

            IndexContent(lang);
            ViewBag.Layout = @"~/Views/Shared/_Layout.cshtml";
            ViewBag.IsBlank = "";
            return View();
        }
        /// <summary>
        /// get Analytics page index without any headr or footer,
        /// to put in iframe
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public IActionResult IndexBlank(string lang)
        {

            IndexContent(lang);
            ViewBag.Layout = @"~/Views/Shared/Analytics/_AnalyticsLayout.cshtml";
            ViewBag.IsBlank = "Blank";
            return View("Index");
        }
        /// <summary>
        /// initialize the data needed to the Analytics page
        /// </summary>
        /// <param name="lang"></param>
        private void IndexContent(string lang)
        {
            var pageRoute = _pageRouteRepository.GetByControllerName(nameof(AnalyticsController)[0..^10]);
            SetUpSEO(lang, pageRoute);
            if (lang == null || lang.ToLower() == "ar")
            {
                ViewBag.GDP = ViewShared.GDPAr;
                ViewBag.Investment = ViewShared.InvestmentAr;
                ViewBag.Governmate = ViewShared.GovernmateAr;
                ViewBag.LangAr = ViewShared.LangArAr;
                ViewBag.LangEn = ViewShared.LangEnAr;
                ViewBag.LangOpt = "اختر اللغة";
                ViewBag.RepoOpt = "اختر البيان";
                ViewBag.Title = pageRoute.ArName;
                ViewBag.Nav = pageRoute.NavItem.ArName;
            }
            else
            {
                ViewBag.GDP = ViewShared.GDPEn;
                ViewBag.Investment = ViewShared.InvestmentEn;
                ViewBag.Governmate = ViewShared.GovernmateEn;
                ViewBag.LangAr = ViewShared.LangArEn;
                ViewBag.LangEn = ViewShared.LangEnEn;
                ViewBag.LangOpt = "Choose Language";
                ViewBag.RepoOpt = "Choose Report";
                ViewBag.Title = pageRoute.EnName;
                ViewBag.Nav = pageRoute.NavItem.EnName;
            }

        }
       
    }
}