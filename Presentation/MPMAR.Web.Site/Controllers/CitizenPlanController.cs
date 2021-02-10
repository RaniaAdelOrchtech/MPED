using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Web.Site.ViewModels;

namespace MPMAR.Web.Site.Controllers
{
    public class CitizenPlanController : BaseController
    {
        private readonly ApplicationDbContext _dataAccessService;
        private readonly IConfiguration _configuration;
        private readonly IPageRouteRepository _pageRouteRepository;
        public CitizenPlanController(ApplicationDbContext dataAccessService, IConfiguration configuration, IPageRouteRepository pageRouteRepository)
        {
            _dataAccessService = dataAccessService;
            _configuration = configuration;
            _pageRouteRepository = pageRouteRepository;
        }
        /// <summary>
        /// get CitizenPlan index page
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public IActionResult Index(string lang)
        {
            var pageRoute = _pageRouteRepository.GetByControllerName(nameof(CitizenPlanController)[0..^10]);
            if (pageRoute == null || !pageRoute.IsActive || pageRoute.IsDeleted)
            {
                return View("Error");
            }

            CityPlanIndexViewModel viewModel = new CityPlanIndexViewModel();
            //get city plan year object which has large gove year
            var cityPlanYearLargeGovYear = _dataAccessService.CityPlanYear.Include(x=>x.DFGov).Where(x => x.IsActive && !x.IsDeleted).OrderByDescending(x => x.GovYear).ThenByDescending(x => x.CreationDate).FirstOrDefault();
            if (cityPlanYearLargeGovYear != null)
            {
                //get all city plan which contain this large year
                var cityPlansObjs = _dataAccessService.CityPlan.Where(x => x.Id == cityPlanYearLargeGovYear.CityPlanId && x.IsActive && !x.IsDeleted).FirstOrDefault();

                //get all city plan year which contain this large year
                var cityPlanYearsWithSameGovYear = _dataAccessService.CityPlanYear.Include(x => x.DFGov).Where(x => x.GovYear == cityPlanYearLargeGovYear.GovYear && x.IsActive && !x.IsDeleted).OrderByDescending(x => x.GovYear).ToList();

                //get all city plan year which not contain this large year
                var cityPlanYearsWithoutGovYear = _dataAccessService.CityPlanYear.Include(x => x.DFGov).Where(x => x.GovYear != cityPlanYearLargeGovYear.GovYear && x.IsActive && !x.IsDeleted).OrderByDescending(x => x.GovYear).ToLookup(x => x.GovYear).ToList();

                var baseaseURL = _configuration.GetValue<string>("BackEndDomain");
                cityPlanYearLargeGovYear.ArFileUrl = baseaseURL + (cityPlanYearLargeGovYear.ArFileUrl != null ? cityPlanYearLargeGovYear.ArFileUrl.Replace(" ", "%20") : "");
                cityPlanYearLargeGovYear.EnFileUrl = baseaseURL + (cityPlanYearLargeGovYear.EnFileUrl != null ? cityPlanYearLargeGovYear.EnFileUrl.Replace(" ", "%20") : "");
                foreach (var item in cityPlanYearsWithSameGovYear)
                {
                    if (!item.ArFileUrl.Contains(baseaseURL))
                    {
                        item.ArFileUrl = baseaseURL + (item.ArFileUrl != null ? item.ArFileUrl.Replace(" ", "%20") : "");
                        item.EnFileUrl = baseaseURL + (item.EnFileUrl != null ? item.EnFileUrl.Replace(" ", "%20") : "");

                    }
                }

                foreach (var group in cityPlanYearsWithoutGovYear)
                {
                    var groupKey = group.Key;
                    foreach (var item in group)
                    {
                        if (!item.ArFileUrl.Contains(baseaseURL))
                        {
                            item.ArFileUrl = baseaseURL + (item.ArFileUrl != null ? item.ArFileUrl.Replace(" ", "%20") : "");
                            item.EnFileUrl = baseaseURL + (item.EnFileUrl != null ? item.EnFileUrl.Replace(" ", "%20") : "");
                        }
                        
                    }
                }

                viewModel = new CityPlanIndexViewModel()
                {
                    CityPlanYearLargeGovYear = cityPlanYearLargeGovYear,
                    CityPlansObj = cityPlansObjs,
                    CityPlanYearsWithSameGovYear = cityPlanYearsWithSameGovYear,
                    CityPlanYearsWithoutSameGovYear = cityPlanYearsWithoutGovYear
                };
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
            return View(viewModel);
        }
        public JsonResult GetCitizenPlan()
        {
            return Json(new
            {
                EGY1530 = new
                {
                    name = "الغربية",
                    zoomable = "no",
                    color = "red",
                    url = "pdf/gharbia.pptx"
                },
                EGY1531 = new
                {
                    name = "الغربية",
                    zoomable = "no",
                    color = "red",
                    url = "pdf/gharbia.pptx"
                }
            });
        }
    }
}