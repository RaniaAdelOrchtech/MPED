using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Enums;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Interfaces.Analytics;

namespace MPMAR.Web.Site.Controllers
{
    public class InvestmentController : Controller
    {

        private readonly AnalyticsDbContext _db;
        private readonly IInvestmentRepository _investmentRepository;
        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly IDFUnitRepository _dFUnitRepository;
        private readonly IEconomicIndicatorRepository _economicIndicatorRepository;
        public InvestmentController(IInvestmentRepository investmentRepository, AnalyticsDbContext db, IEconomicIndicatorRepository economicIndicatorRepository, IPageRouteRepository pageRouteRepository, IDFUnitRepository dFUnitRepository)
        {
            _investmentRepository = investmentRepository;
            _db = db;
            _economicIndicatorRepository = economicIndicatorRepository;
            _pageRouteRepository = pageRouteRepository;
            _dFUnitRepository = dFUnitRepository;
        }
        /// <summary>
        /// get Investment page index
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
        /// get Investment page index without any headr or footer,
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
        /// initialize the data needed to the Investment page
        /// </summary>
        /// <param name="lang"></param>
        private void IndexContent(string lang)
        {
            var economicIndicator = _economicIndicatorRepository.GetAll().FirstOrDefault();
            var pageRoute = _pageRouteRepository.GetByControllerName(nameof(AnalyticsController)[0..^10]);

            var years = _db.Investments.AsEnumerable().Where(x => !(x.isDeleted ?? false)).GroupBy(x => x.DFYearId).Select(grp => grp.First().DFYearId).ToList();
            var yearsData = _db.DFYears.OrderByDescending(x => x.Order).Where(x => years.Contains(x.Id)).ToList();

            if (lang == null || lang.ToLower() == "ar")
            {
                ViewBag.Analytics = pageRoute.ArName;
                ViewBag.AnalyticsNav = pageRoute.NavItem.ArName;

                ViewBag.selectFiscalYear = ViewShared.selectFiscalYearAr;
                ViewBag.selectQuarter = ViewShared.selectQuartAr;
                ViewBag.selectSector = ViewShared.SelectEconomicActivityAr;
                ViewBag.selectFrequancy = ViewShared.FrequancyAr;
                ViewBag.submitBtn = ViewShared.ApplyBtnAr;
                ViewBag.clearBtn = ViewShared.clearBtnAr;
                ViewBag.ReportSpan = ViewShared.TableSpanAr;
                ViewBag.ChartsSpan = ViewShared.ChartsSpanAr;
                ViewBag.Pie = ViewShared.PieAr;
                ViewBag.Bar = ViewShared.BarAr;
                ViewBag.Line = ViewShared.LineAr;
                ViewBag.GDP = economicIndicator.ImageTitleAr1;
                ViewBag.Investment = economicIndicator.ImageTitleAr2;
                ViewBag.Governmate = economicIndicator.ImageTitleAr3;
                ViewBag.LangAr = ViewShared.LangArAr;
                ViewBag.LangEn = ViewShared.LangEnAr;
                ViewBag.LangOpt = ViewShared.LangArAr;
                ViewBag.RepoOpt = economicIndicator.ImageTitleAr2;
                ViewBag.Export = ViewShared.exportAr;

                ViewBag.ChooseAllFilters = ViewSharedErrorMsgs.ChooseAllFiltersAr;
                ViewBag.ConfirmBtn = ViewSharedErrorMsgs.ConfirmBtnAr;
                ViewBag.SwitchTabs = ViewSharedErrorMsgs.SwitchTabsAr;
                ViewBag.SwitchTabsYes = ViewSharedErrorMsgs.SwitchTabsYesAr;
                ViewBag.SwitchTabsNo = ViewSharedErrorMsgs.SwitchTabsNoAr;


                var yearList = yearsData.Select(x => new ColumnView { id = x.Id, title = x.NameAr }).ToList();
                yearList.Insert(0, new ColumnView() { id = -1, title = "جميع السنوات" });
                ViewBag.YearView = yearList;

                var quartersList = _db.DFQuarters.Where(x => x.Id != (int)DFQuarterEnum.Total).Select(x => new ColumnView { id = x.Id, title = x.NameAr }).ToList();
                quartersList.Insert(0, new ColumnView() { id = -1, title = "جميع الإربع" });
                ViewBag.QuarterView = quartersList;


                var investmentList = _db.DFGDP.Where(x => x.Type == 3 && !x.IsDeleted && !x.IsBasic && x.DFGDPId == null).OrderBy(x => x.order).Select(x => new ColumnViewTree { id = x.Id, title = x.NameAr }).ToList();
                var investmentListChild = _db.DFGDP.Where(x => x.Type == 3 && !x.IsDeleted && !x.IsBasic && x.DFGDPId != null).ToList();
                investmentList.ForEach(x => x.subs = investmentListChild.Where(y => y.DFGDPId == x.id).Select(z => new ColumnView { id = z.Id, title = z.NameAr }).ToList());
                investmentList.Insert(0, new ColumnViewTree() { id = -1, title = "جميع الأنشطة", subs = new List<ColumnView>() });
                ViewBag.InvestmentView = investmentList;

                ViewBag.FrequancyView = new List<ColumnView>() {
                    new ColumnView(){id=1,title="سنوي"},
                    new ColumnView(){id=2,title="ربع سنوي"}
                };
            }
            else
            {
                ViewBag.Analytics = pageRoute.EnName;
                ViewBag.AnalyticsNav = pageRoute.NavItem.EnName;

                ViewBag.selectFiscalYear = ViewShared.selectFiscalYearEn;
                ViewBag.selectQuarter = ViewShared.selectQuartEn;
                ViewBag.selectSector = ViewShared.SelectEconomicActivityEn;
                ViewBag.selectFrequancy = ViewShared.FrequancyEn;
                ViewBag.submitBtn = ViewShared.ApplyBtnEn;
                ViewBag.clearBtn = ViewShared.clearBtnEn;
                ViewBag.ReportSpan = ViewShared.TableSpanEn;
                ViewBag.ChartsSpan = ViewShared.ChartsSpanEn;
                ViewBag.Pie = ViewShared.PieEn;
                ViewBag.Bar = ViewShared.BarEn;
                ViewBag.Line = ViewShared.LineEn;
                ViewBag.GDP = economicIndicator.ImageTitleEn1;
                ViewBag.Investment = economicIndicator.ImageTitleEn2;
                ViewBag.Governmate = economicIndicator.ImageTitleEn3;
                ViewBag.LangAr = ViewShared.LangArEn;
                ViewBag.LangEn = ViewShared.LangEnEn;
                ViewBag.LangOpt = ViewShared.LangEnEn;
                ViewBag.RepoOpt = economicIndicator.ImageTitleEn2;
                ViewBag.Export = ViewShared.exportEn;

                ViewBag.ChooseAllFilters = ViewSharedErrorMsgs.ChooseAllFiltersEn;
                ViewBag.ConfirmBtn = ViewSharedErrorMsgs.ConfirmBtnEn;
                ViewBag.SwitchTabs = ViewSharedErrorMsgs.SwitchTabsEn;
                ViewBag.SwitchTabsYes = ViewSharedErrorMsgs.SwitchTabsYesEn;
                ViewBag.SwitchTabsNo = ViewSharedErrorMsgs.SwitchTabsNoEn;

                var yearList = yearsData.Where(x => !x.IsDeleted).Select(x => new ColumnView { id = x.Id, title = x.NameEn }).ToList();
                yearList.Insert(0, new ColumnView() { id = -1, title = ViewShared.AllOption });
                ViewBag.YearView = yearList;

                var quartersList = _db.DFQuarters.Where(x => x.Id != (int)DFQuarterEnum.Total).Select(x => new ColumnView { id = x.Id, title = x.NameEn }).ToList();
                quartersList.Insert(0, new ColumnView() { id = -1, title = ViewShared.AllOption });
                ViewBag.QuarterView = quartersList;

                var investmentList = _db.DFGDP.Where(x => x.Type == 3 && !x.IsDeleted && !x.IsBasic && x.DFGDPId == null).OrderBy(x => x.order).Select(x => new ColumnViewTree { id = x.Id, title = x.NameEn }).ToList();
                var investmentListChild = _db.DFGDP.Where(x => x.Type == 3 && !x.IsDeleted && !x.IsBasic && x.DFGDPId != null).ToList();
                investmentList.ForEach(x => x.subs = investmentListChild.Where(y => y.DFGDPId == x.id).Select(z => new ColumnView { id = z.Id, title = z.NameEn }).ToList());
                investmentList.Insert(0, new ColumnViewTree() { id = -1, title = ViewShared.AllOption, subs = new List<ColumnView>() });
                ViewBag.InvestmentView = investmentList;

                ViewBag.FrequancyView = new List<ColumnView>() {
                    new ColumnView(){id=1,title="Annual"},
                    new ColumnView(){id=2,title="Quarterly"}
                };
            }
        }

        /// <summary>
        /// get report data
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetReport(string lang, int[] years, int[] quarters, List<string> headers)
        {

            var investmentViewModel = _investmentRepository.GetReport(lang, years, quarters, headers);

            return Json(investmentViewModel);

        }

        /// <summary>
        /// get pie chart data
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetPieReport(string lang, int[] years, int[] quarters, List<string> headers)
        {

            var pieInvestmentViewModel = _investmentRepository.GetPieReport(lang, years, quarters, headers);
            DFUnit unit;
            string unitText = "";
            unit = _dFUnitRepository.GetByID((int)DFUnitEnum.MillionEGP);


            if (lang == "en")
            {
                unitText = " " + unit.NameEn + "\r\n";
            }
            else
            {
                unitText = " " + unit.NameAr + "\r\n";
            }
            return Json(new { data = pieInvestmentViewModel, unit = unitText });

        }
        /// <summary>
        /// get line and bar chart data
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="years">years ids</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetChartReport(string lang, int[] years, List<string> headers, string chartType="")
        {
            var pieInvestmentViewModel = _investmentRepository.GetChartReport(lang, years, headers, chartType);

            DFUnit unit;
            string unitText = "";

            unit = _dFUnitRepository.GetByID((int)DFUnitEnum.MillionEGP);

            if (lang == "en")
            {
                unitText = unit.NameEn;
            }
            else
            {
                unitText = unit.NameAr;
            }

            return Json(new { data = pieInvestmentViewModel, unit = unitText });

        }
    }
}