using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Web.Site.Common;
using MPMAR.Web.Site.ViewModels;

namespace MPMAR.Web.Site.Controllers
{
    public class FormerMinistriesController : BaseController
    {
        private readonly IFormerMinistriesPageInfoRepository _formerMinistriesPageInfoRepository;
        private readonly IMinistryTimeLineRepository _ministryTimeLineRepository;
        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly IConfiguration _configuration;

        public FormerMinistriesController(IFormerMinistriesPageInfoRepository formerMinistriesPageInfoRepository,
            IMinistryTimeLineRepository ministryTimeLineRepository,
            IPageRouteRepository pageRouteRepository,IConfiguration configuration)
        {
            _formerMinistriesPageInfoRepository = formerMinistriesPageInfoRepository;
            _ministryTimeLineRepository = ministryTimeLineRepository;
            _pageRouteRepository = pageRouteRepository;
            _configuration = configuration;
        }
        /// <summary>
        /// get FormerMinistries index page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public IActionResult Index(int id, string lang)
        {
            var pageMetaData = _pageRouteRepository.GetByControllerName(nameof(FormerMinistriesController)[0..^10]);
            if (pageMetaData == null || !pageMetaData.IsActive || pageMetaData.IsDeleted)
            {
                return View("Error");
            }
            //get current domain to use it in sharing links
            var CurrentDomain = _configuration.GetValue<string>("CurrentDomain");
            ViewBag.CurrentDomain = CurrentDomain;
            var pagInfo = _formerMinistriesPageInfoRepository.Get();
            var ministries = _ministryTimeLineRepository.GetMinistryTimeLine().Where(x => x.IsActive && !x.IsDeleted).OrderBy(x => x.Order).ToList();
            FormerMinistriesViewModel formerMinistriesViewModel;
           
            SetUpSEO(lang, pageMetaData);
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            if (lang == null || lang.Equals("ar"))
            {
                formerMinistriesViewModel = LoadFormerMinistriesAr(pagInfo, ministries, imageBaseURL);
                if (pageMetaData != null)
                {
                    ViewBag.PageTitle = pageMetaData.ArName;
                    ViewBag.PagePath = $"{pageMetaData.NavItem.ArName} / {pageMetaData.ArName}";
                    ViewBag.ForShare = ViewStringsCommon.ForShareAr;
                }
            }
            else
            {
                formerMinistriesViewModel = LoadFormerMinistriesEn(pagInfo, ministries, imageBaseURL);
                if (pageMetaData != null)
                {
                    ViewBag.PageTitle = pageMetaData.EnName;
                    ViewBag.PagePath = $"{pageMetaData.NavItem.EnName} / {pageMetaData.EnName}";
                    ViewBag.ForShare = ViewStringsCommon.ForShareEn;
                }

            }

            return View(formerMinistriesViewModel);
        }
        /// <summary>
        /// get FormerMinistriesViewModel in english
        /// </summary>
        /// <param name="pagInfo"></param>
        /// <param name="ministries">list of ministries</param>
        /// <param name="imageBaseURL"></param>
        /// <returns></returns>
        private static FormerMinistriesViewModel LoadFormerMinistriesEn(FormerMinistriesPageInfo pagInfo, List<MinistryTimeLine> ministries,string imageBaseURL)
        {
            var formerMinistriesViewModel = new FormerMinistriesViewModel();

            formerMinistriesViewModel.Title1 = pagInfo.Title1En;
            formerMinistriesViewModel.Title2 = pagInfo.Title2En;
            formerMinistriesViewModel.Description = pagInfo.DescriptionEn;
            foreach (var ministr in ministries)
            {
                formerMinistriesViewModel.MinistryTimeLine.Add(new MinistryViewModel()
                {
                    Id=ministr.Id,
                    Name = ministr.EnName,
                    Description = ministr.EnDescription,
                    Order = ministr.Order??0,
                    ProfileImageUrl = imageBaseURL + (ministr.ProfileImageUrl != null ? ministr.ProfileImageUrl.Replace(" ", "%20") : ""),
                    Period = ministr.PeriodEn,
                     Facebook = ministr.Facebook,
                    Twitter = ministr.Twitter,
                    Email = ministr.Email

                });
            }

            return formerMinistriesViewModel;
        }
        /// <summary>
        /// get FormerMinistriesViewModel in arabic
        /// </summary>
        /// <param name="pagInfo"></param>
        /// <param name="ministries">list of ministries</param>
        /// <param name="imageBaseURL"></param>
        /// <returns></returns>
        private static FormerMinistriesViewModel LoadFormerMinistriesAr(FormerMinistriesPageInfo pagInfo, List<MinistryTimeLine> ministries,string imageBaseURL)
        {
            var formerMinistriesViewModel = new FormerMinistriesViewModel();

            formerMinistriesViewModel.Title1 = pagInfo.Title1Ar;
            formerMinistriesViewModel.Title2 = pagInfo.Title2Ar;
            formerMinistriesViewModel.Description = pagInfo.DescriptionAr;
            foreach (var ministr in ministries)
            {
                formerMinistriesViewModel.MinistryTimeLine.Add(new MinistryViewModel()
                {
                    Id = ministr.Id,
                    Name = ministr.ArName,
                    Description = ministr.ArDescription,
                    Order = ministr.Order??0,
                    ProfileImageUrl = imageBaseURL + (ministr.ProfileImageUrl != null ? ministr.ProfileImageUrl.Replace(" ", "%20") : ""),
                    Period = ministr.PeriodAr,
                    Facebook = ministr.Facebook,
                    Twitter = ministr.Twitter,
                    Email = ministr.Email

                });
            }

            return formerMinistriesViewModel;
        }
    }
}