using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Enums;
using MPMAR.Business.Interfaces;

namespace MPMAR.Web.Site.Controllers
{
    public class GovernorateController : Controller
    {
        private readonly AnalyticsDbContext _db;
        private readonly IGovernorateRepository _repo;
        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly IEconomicIndicatorRepository _economicIndicatorRepository;
        public GovernorateController(IGovernorateRepository repo, AnalyticsDbContext db, IEconomicIndicatorRepository economicIndicatorRepository, IPageRouteRepository pageRouteRepository)
        {
            _repo = repo;
            _db = db;
            _economicIndicatorRepository = economicIndicatorRepository;
            _pageRouteRepository = pageRouteRepository;
        }
        /// <summary>
        /// get Governorate page index
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public IActionResult Index(string lang)
        {
            IndexContent(lang);
            ViewBag.Layout = @"~/Views/Shared/_Layout.cshtml";
            ViewBag.IsBlank = "";
            return View();
        }
        /// <summary>
        /// get Governorate page index without any headr or footer,
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
        /// initialize the data needed to the Governorate page
        /// </summary>
        /// <param name="lang"></param>
        private void IndexContent(string lang)
        {
            var economicIndicator = _economicIndicatorRepository.GetAll().FirstOrDefault();
            var pageRoute = _pageRouteRepository.GetByControllerName(nameof(AnalyticsController)[0..^10]);

            var govYears = _db.Governorates.Where(x => !(x.isDeleted ?? false)).AsEnumerable().GroupBy(x => x.DFYearId).Select(grp => grp.First().DFYearId).ToList();
            var govYearsData = _db.DFYears.OrderByDescending(x => x.Order).Where(x => govYears.Contains(x.Id)).ToList();

            if (lang == null || lang.ToLower() == "ar")
            {
                ViewBag.Analytics = pageRoute.ArName;
                ViewBag.AnalyticsNav = pageRoute.NavItem.ArName;

                ViewBag.selectFiscalYear = ViewShared.selectFiscalYearAr;
                ViewBag.selectSector = ViewShared.SelectEconomicActivityAr;
                ViewBag.submitBtn = ViewShared.ApplyBtnAr;
                ViewBag.clearBtn = ViewShared.clearBtnAr;
                ViewBag.ReportSpan = ViewShared.TableSpanAr;
                ViewBag.ChartsSpan = ViewShared.ChartsSpanAr;
                ViewBag.Pie = ViewShared.PieAr;
                ViewBag.Bar = ViewShared.BarAr;
                ViewBag.Line = ViewShared.LineAr;
                ViewBag.Region_gov = ViewShared.Region_govAr;
                ViewBag.Region = ViewShared.RegionAr;
                ViewBag.Gov = ViewShared.GovAr;
                ViewBag.GDP = economicIndicator.ImageTitleAr1;
                ViewBag.Investment = economicIndicator.ImageTitleAr2;
                ViewBag.Governmate = economicIndicator.ImageTitleAr3;
                ViewBag.LangAr = ViewShared.LangArAr;
                ViewBag.LangEn = ViewShared.LangEnAr;
                ViewBag.LangOpt = ViewShared.LangArAr;
                ViewBag.RepoOpt = economicIndicator.ImageTitleAr3;
                ViewBag.Export = ViewShared.exportAr;
                ViewBag.sectorPie = ViewSharedErrorMsgs.sectorPieAr;

                ViewBag.ChooseAllFilters = ViewSharedErrorMsgs.ChooseAllFiltersAr;
                ViewBag.ConfirmBtn = ViewSharedErrorMsgs.ConfirmBtnAr;
                ViewBag.GovReportPieSelection = ViewSharedErrorMsgs.GovReportPieSelectionAr;
                ViewBag.SwitchTabs = ViewSharedErrorMsgs.SwitchTabsAr;
                ViewBag.SwitchTabsYes = ViewSharedErrorMsgs.SwitchTabsYesAr;
                ViewBag.SwitchTabsNo = ViewSharedErrorMsgs.SwitchTabsNoAr;


                var regionandGov = _db.DFRegions.Select(x => new RegionGovView
                {
                    id = x.Id,
                    title = x.NameAr,
                    subs = x.DFGovernorates.Select(y => new RegionGovView { id = y.Id, title = y.NameAr, subs = new List<RegionGovView>() }).ToList()
                }).ToList();
                regionandGov.Insert(0, new RegionGovView() { id = -1, title = "جميع المحافظات", subs = new List<RegionGovView>() });
                ViewBag.RegionandGov = regionandGov;

                var yearList = govYearsData.Select(x => new ColumnView { id = x.Id, title = x.NameAr }).ToList();
                yearList.Insert(0, new ColumnView() { id = -1, title = "جميع السنوات" });
                ViewBag.YearView = yearList;

                var regionList = _db.DFRegions.Select(x => new ColumnView { id = x.Id, title = x.NameAr }).ToList();
                regionList.Insert(0, new ColumnView() { id = -1, title = "جميع المناطق" });
                ViewBag.RegionView = regionList;

                var govList = _db.DFGovernorates.Select(x => new ColumnView { id = x.Id, title = x.NameAr }).ToList();
                govList.Insert(0, new ColumnView() { id = -1, title = "جميع المحافظات" });
                ViewBag.GovernateView = govList;

                var sectorList = _db.DFSectors.Where(x => x.Type == 0).OrderBy(x => x.Order ?? 0).Select(x => new ColumnView { id = x.Id, title = x.NameAr }).ToList();
                sectorList.Insert(0, new ColumnView() { id = -1, title = "جميع الأنشطة" });
                ViewBag.SectorsView = sectorList;

            }
            else
            {
                ViewBag.Analytics = pageRoute.EnName;
                ViewBag.AnalyticsNav = pageRoute.NavItem.EnName;

                ViewBag.selectFiscalYear = ViewShared.selectFiscalYearEn;
                ViewBag.selectSector = ViewShared.SelectEconomicActivityEn;
                ViewBag.submitBtn = ViewShared.ApplyBtnEn;
                ViewBag.clearBtn = ViewShared.clearBtnEn;
                ViewBag.ReportSpan = ViewShared.TableSpanEn;
                ViewBag.ChartsSpan = ViewShared.ChartsSpanEn;
                ViewBag.Pie = ViewShared.PieEn;
                ViewBag.Bar = ViewShared.BarEn;
                ViewBag.Line = ViewShared.LineEn;
                ViewBag.Region_gov = ViewShared.Region_govEn;
                ViewBag.Region = ViewShared.RegionEn;
                ViewBag.Gov = ViewShared.GovEn;
                ViewBag.GDP = economicIndicator.ImageTitleEn1;
                ViewBag.Investment = economicIndicator.ImageTitleEn2;
                ViewBag.Governmate = economicIndicator.ImageTitleEn3;
                ViewBag.LangAr = ViewShared.LangArEn;
                ViewBag.LangEn = ViewShared.LangEnEn;
                ViewBag.LangOpt = ViewShared.LangEnEn;
                ViewBag.RepoOpt = economicIndicator.ImageTitleEn3;
                ViewBag.Export = ViewShared.exportEn;
                ViewBag.sectorPie = ViewSharedErrorMsgs.sectorPieEn;

                ViewBag.ChooseAllFilters = ViewSharedErrorMsgs.ChooseAllFiltersEn;
                ViewBag.ConfirmBtn = ViewSharedErrorMsgs.ConfirmBtnEn;
                ViewBag.GovReportPieSelection = ViewSharedErrorMsgs.GovReportPieSelectionEn;
                ViewBag.SwitchTabs = ViewSharedErrorMsgs.SwitchTabsEn;
                ViewBag.SwitchTabsYes = ViewSharedErrorMsgs.SwitchTabsYesEn;
                ViewBag.SwitchTabsNo = ViewSharedErrorMsgs.SwitchTabsNoEn;


                var regionandGov = _db.DFRegions.Select(x => new RegionGovView { id = x.Id, title = x.NameEn, subs = x.DFGovernorates.Select(y => new RegionGovView { id = y.Id, title = y.NameEn, subs = new List<RegionGovView>() }).ToList() }).ToList();
                regionandGov.Insert(0, new RegionGovView() { id = -1, title = ViewShared.AllOption, subs = new List<RegionGovView>() });
                ViewBag.RegionandGov = regionandGov;

                var yearList = govYearsData.Select(x => new ColumnView { id = x.Id, title = x.NameEn }).ToList();
                yearList.Insert(0, new ColumnView() { id = -1, title = ViewShared.AllOption });
                ViewBag.YearView = yearList;

                var regionList = _db.DFRegions.Select(x => new ColumnView { id = x.Id, title = x.NameEn }).ToList();
                regionList.Insert(0, new ColumnView() { id = -1, title = ViewShared.AllOption });
                ViewBag.RegionView = regionList;

                var govList = _db.DFGovernorates.Select(x => new ColumnView { id = x.Id, title = x.NameEn }).ToList();
                govList.Insert(0, new ColumnView() { id = -1, title = ViewShared.AllOption });
                ViewBag.GovernateView = govList;


                var sectorList = _db.DFSectors.Where(x => x.Type == 0).OrderBy(x => x.Order ?? 0).Select(x => new ColumnView { id = x.Id, title = x.NameEn }).ToList();
                sectorList.Insert(0, new ColumnView() { id = -1, title = ViewShared.AllOption });
                ViewBag.SectorsView = sectorList;

            }
        }
        /// <summary>
        /// get line chart data
        /// </summary>
        /// <param name="regions"></param>
        /// <param name="governates"></param>
        /// <param name="years"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFilterForLineChart(int[] regions, int[] governates, int[] years, int[] activities, string lang)
        {
            var data = _repo.GetFilterForLineChart(regions, governates, years, activities, lang);

            string unit = "";
            if (lang == "en")
            {
                unit = "one thousand";
            }
            else
            {
                unit = "الف جنيه";
            }
            return Json(new { data, unit });
        }
        /// <summary>
        /// get pie chart data
        /// </summary>
        /// <param name="regions"></param>
        /// <param name="governates"></param>
        /// <param name="year"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFilterForPieChart(int[] regions, int[] governates, int year, int[] activities, string lang)
        {
            var data = _repo.GetFilterForPieChart(regions, governates, year, activities, lang);
            string unit = "";
            if (lang == "en")
            {
                unit = " one thousand \r\n";
            }
            else
            {
                unit = " الف جنية \r\n";
            }
            return Json(new { data,  unit });
        }
        /// <summary>
        /// get bar chart data
        /// </summary>
        /// <param name="regions"></param>
        /// <param name="governates"></param>
        /// <param name="years"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFilterForBarChart(int[] regions, int[] governates, int[] years, int[] activities, string lang)
        {
            var data = _repo.GetFilterForBarChart(regions, governates, years, activities, lang);
            string unit = "";
            if (lang == "en")
            {
                unit = "one thousand";
            }
            else
            {
                unit = "الف جنيه";
            }
            return Json(new { data,unit } );
        }
        /// <summary>
        /// get report data
        /// </summary>
        /// <param name="regionsandgov"></param>
        /// <param name="years"></param>
        /// <param name="activities"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFilterdGovernoratesForGrid(int[] regionsandgov, int[] years, int[] activities, string lang)
        {
            GovernorateViewModel result = _repo.GetFilterdGovernoratesForGrid(regionsandgov, years, activities, lang);

            return Json(result);
        }
    }
}