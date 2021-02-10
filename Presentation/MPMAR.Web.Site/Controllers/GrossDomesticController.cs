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
    public class GrossDomesticController : Controller
    {
        private readonly AnalyticsDbContext _db;
        private readonly IEconomicIndicatorRepository _economicIndicatorRepository;
        private readonly IDFUnitRepository _dFUnitRepository;
        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly IGrossDomesticRepository _grossDomesticRepository;
        public GrossDomesticController(IGrossDomesticRepository grossDomesticRepository, AnalyticsDbContext db, IEconomicIndicatorRepository economicIndicatorRepository, IDFUnitRepository dFUnitRepository, IPageRouteRepository pageRouteRepository)
        {
            _grossDomesticRepository = grossDomesticRepository;
            _db = db;
            _economicIndicatorRepository = economicIndicatorRepository;
            _pageRouteRepository = pageRouteRepository;
            _dFUnitRepository = dFUnitRepository;
        }
        /// <summary>
        /// get  GrossDomestic page index
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
        /// get GrossDomestic page index without any headr or footer,
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
        /// initialize the data needed to the GrossDomestic page
        /// </summary>
        /// <param name="lang"></param>
        private void IndexContent(string lang)
        {
            var economicIndicator = _economicIndicatorRepository.GetAll().FirstOrDefault();

            //var yearsActivityConst = _db.ActivityConstants.Where(x => !(x.IsDeleted ?? false)).Select(x => x.DFYearId).ToList();
            var yearsActivityCurrent = _db.ActivityCurrents.Where(x => !(x.IsDeleted ?? false)).Select(x => x.DFYearId).ToList();
            var yearsComponentConst = _db.ComponentConstants.Where(x => !(x.IsDeleted ?? false)).Select(x => x.DFYearFiscalId).ToList();
            var yearsComponentCurrent = _db.ComponentCurrents.Where(x => !(x.IsDeleted ?? false)).Select(x => x.DFYearFiscalId).ToList();
            var yearsSRGDP = _db.SectorGrowthRates.Where(x => !(x.IsDeleted ?? false)).Select(x => x.DFYearId).ToList();

            var yearsRGDP = _db.RGDPGrowthRates.Where(x => !(x.IsDeleted ?? false)).Select(x => x.DFYearId).ToList();


            var years = yearsActivityCurrent.Concat(yearsComponentConst.Concat(yearsSRGDP.Concat(yearsRGDP.Concat(yearsComponentCurrent)))).GroupBy(x => x).Select(grp => grp.First()).ToList();
            var yearsData = _db.DFYears.OrderByDescending(x => x.Order).Where(x => years.Contains(x.Id)).ToList();
            var pageRoute = _pageRouteRepository.GetByControllerName(nameof(AnalyticsController)[0..^10]);



            if (lang == null || lang.ToLower() == "ar")
            {
                ViewBag.Analytics = pageRoute.ArName;
                ViewBag.AnalyticsNav = pageRoute.NavItem.ArName;


                ViewBag.selectFiscalYear = ViewShared.selectFiscalYearAr;
                ViewBag.selectQuarter = ViewShared.selectQuartAr;
                ViewBag.selectPrice = ViewShared.selectPriceAr;
                ViewBag.selectGDP = ViewShared.selectGDPAr;
                ViewBag.selectSector = ViewShared.selectSectorAr;
                ViewBag.selectActivity = ViewShared.selectActivityAr;
                ViewBag.selectComponent = ViewShared.selectComponentAr;
                ViewBag.selectRGDP = ViewShared.selectRGDPAr;
                ViewBag.selectRGDP1617 = ViewShared.selectRGDP1617Ar;
                ViewBag.selectSectorRGDP = ViewShared.selectSectorRGDPAr;
                ViewBag.selectFrequancy = ViewShared.FrequancyAr;
                ViewBag.submitBtn = ViewShared.ApplyBtnAr;
                ViewBag.clearBtn = ViewShared.clearBtnAr;
                ViewBag.ReportSpan = ViewShared.TableSpanAr;
                ViewBag.ChartsSpan = ViewShared.ChartsSpanAr; ;
                ViewBag.Pie = ViewShared.PieAr;
                ViewBag.Bar = ViewShared.BarAr;
                ViewBag.Line = ViewShared.LineAr;
                ViewBag.GDP = economicIndicator.ImageTitleAr1;
                ViewBag.Investment = economicIndicator.ImageTitleAr2;
                ViewBag.Governmate = economicIndicator.ImageTitleAr3;
                ViewBag.LangAr = ViewShared.LangArAr;
                ViewBag.LangEn = ViewShared.LangEnAr;
                ViewBag.LangOpt = ViewShared.LangArAr;
                ViewBag.RepoOpt = economicIndicator.ImageTitleAr1;
                ViewBag.Export = ViewShared.exportAr;



                ViewBag.ChooseAllFilters = ViewSharedErrorMsgs.ChooseAllFiltersAr;
                ViewBag.ConfirmBtn = ViewSharedErrorMsgs.ConfirmBtnAr;
                ViewBag.GDPRepoExcel = ViewSharedErrorMsgs.GDPRepoExcelAr;
                ViewBag.GDPRepoSectorsAndQuarters = ViewSharedErrorMsgs.GDPRepoSectorsAndQuartersAr;
                ViewBag.SwitchTabs = ViewSharedErrorMsgs.SwitchTabsAr;
                ViewBag.SwitchTabsYes = ViewSharedErrorMsgs.SwitchTabsYesAr;
                ViewBag.SwitchTabsNo = ViewSharedErrorMsgs.SwitchTabsNoAr;


                var yearList = yearsData.Select(x => new ColumnView { id = x.Id, title = x.NameAr }).ToList();
                yearList.Insert(0, new ColumnView() { id = -1, title = "جميع السنوات" });
                ViewBag.YearView = yearList;

                var quartersList = _db.DFQuarters.Where(x => x.Id != (int)DFQuarterEnum.Total).Select(x => new ColumnView { id = x.Id, title = x.NameAr }).ToList();
                quartersList.Insert(0, new ColumnView() { id = -1, title = "جميع الإربع" });
                ViewBag.QuarterView = quartersList;


                var sectorList = _db.DFSectors.Where(x => x.Type == 1).Select(x => new ColumnView { id = x.Id, title = x.NameAr }).ToList();
                sectorList.Insert(0, new ColumnView() { id = -1, title = "جميع القطاعات" });
                ViewBag.SectorView = sectorList;

                var componentList = _db.DFGDP.Where(x => x.Type == (int)GDPTypeEnum.Component && !x.IsDeleted && !x.IsBasic && x.DFGDPId == null && x.Id != (int)DFGDPEnum.GDB_Component_RGDP
                && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617
                  && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617_Unit
                    && x.Id != (int)DFGDPEnum.GDB_Component_RGDP_Unit).OrderBy(x => x.order).Select(x => new ColumnViewTree { id = x.Id, title = x.NameAr }).ToList();
                var componentListChild = _db.DFGDP.Where(x => x.Type == (int)GDPTypeEnum.Component && !x.IsDeleted && !x.IsBasic && x.DFGDPId != null && x.Id != (int)DFGDPEnum.GDB_Component_RGDP
                && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617
                    && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617_Unit
                    && x.Id != (int)DFGDPEnum.GDB_Component_RGDP_Unit).ToList();
                componentList.ForEach(x => x.subs = componentListChild.Where(y => y.DFGDPId == x.id).Select(z => new ColumnView { id = z.Id, title = z.NameAr }).ToList());
                componentList.Insert(0, new ColumnViewTree() { id = -1, title = "جميع العناصر", subs = new List<ColumnView>() });
                ViewBag.ComponentView = componentList;




                var activityList = _db.DFGDP.Where(x => x.Type == 2 && !x.IsDeleted && !x.IsBasic && x.DFGDPId == null && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP && x.Id != (int)DFGDPEnum.GDB_Activity_Sector_RGDP
                && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617
                && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617_Unit
                && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP_Unit).OrderBy(x => x.order).Select(x => new ColumnViewTree { id = x.Id, title = x.NameAr }).ToList();
                var activityListChild = _db.DFGDP.Where(x => x.Type == 2 && !x.IsDeleted && !x.IsBasic && x.DFGDPId != null && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP
                && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617 && x.Id != (int)DFGDPEnum.GDB_Activity_Sector_RGDP
                      && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617_Unit
                && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP_Unit).ToList();
                activityList.ForEach(x => x.subs = activityListChild.Where(y => y.DFGDPId == x.id).Select(z => new ColumnView { id = z.Id, title = z.NameAr }).ToList());
                activityList.Insert(0, new ColumnViewTree() { id = -1, title = "جميع الأنشطة", subs = new List<ColumnView>() });
                ViewBag.ActivityView = activityList;



                ViewBag.PriceView = new List<ColumnView>() {
                    new ColumnView(){id=1,title="الأسعار الثابتة"},
                    new ColumnView(){id=2,title="الأسعار الجارية"}
                };


                ViewBag.GDPViewConst = new List<ColumnView>() {
                    new ColumnView(){id=(int)SheetsEnum.Component,title=ViewShared.selectComponentMenueAr},
                    new ColumnView(){id=(int)SheetsEnum.RGDP,title=ViewShared.selectRGDPAr},
                    new ColumnView(){id=(int)SheetsEnum.SectorRGDP,title=ViewShared.selectSectorRGDPAr},
                };
                ViewBag.GDPViewConstChart = new List<ColumnView>() {
                    new ColumnView(){id=(int)SheetsEnum.Component,title=ViewShared.selectComponentMenueAr},

                };
                ViewBag.GDPViewCurrent = new List<ColumnView>() {
                    new ColumnView(){id=(int)SheetsEnum.Component,title=ViewShared.selectComponentMenueAr},
                    new ColumnView(){id=(int)SheetsEnum.Activity,title=ViewShared.selectActivityMenueAr},
                };
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
                ViewBag.selectPrice = ViewShared.selectPriceEn;
                ViewBag.selectGDP = ViewShared.selectGDPEn;
                ViewBag.selectSector = ViewShared.selectSectorEn;
                ViewBag.selectActivity = ViewShared.selectActivityEn;
                ViewBag.selectComponent = ViewShared.selectComponentEn;
                ViewBag.selectRGDP = ViewShared.selectRGDPEn;
                ViewBag.selectRGDP1617 = ViewShared.selectRGDP1617En;
                ViewBag.selectSectorRGDP = ViewShared.selectSectorRGDPEn;
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
                ViewBag.RepoOpt = economicIndicator.ImageTitleEn1;
                ViewBag.Export = ViewShared.exportEn;

                ViewBag.ChooseAllFilters = ViewSharedErrorMsgs.ChooseAllFiltersEn;
                ViewBag.ConfirmBtn = ViewSharedErrorMsgs.ConfirmBtnEn;
                ViewBag.GDPRepoExcel = ViewSharedErrorMsgs.GDPRepoExcelEn;
                ViewBag.GDPRepoSectorsAndQuarters = ViewSharedErrorMsgs.GDPRepoSectorsAndQuartersEn;
                ViewBag.SwitchTabs = ViewSharedErrorMsgs.SwitchTabsEn;
                ViewBag.SwitchTabsYes = ViewSharedErrorMsgs.SwitchTabsYesEn;
                ViewBag.SwitchTabsNo = ViewSharedErrorMsgs.SwitchTabsNoEn;

                var yearList = yearsData.Select(x => new ColumnView { id = x.Id, title = x.NameEn }).ToList();
                yearList.Insert(0, new ColumnView() { id = -1, title = ViewShared.AllOption });
                ViewBag.YearView = yearList;

                var quartersList = _db.DFQuarters.Where(x => x.Id != (int)DFQuarterEnum.Total).Select(x => new ColumnView { id = x.Id, title = x.NameEn }).ToList();
                quartersList.Insert(0, new ColumnView() { id = -1, title = ViewShared.AllOption });
                ViewBag.QuarterView = quartersList;

                var sectorList = _db.DFSectors.Where(x => x.Type == 1).Select(x => new ColumnView { id = x.Id, title = x.NameEn }).ToList();
                sectorList.Insert(0, new ColumnView() { id = -1, title = ViewShared.AllOption });
                ViewBag.SectorView = sectorList;


                var componentList = _db.DFGDP.Where(x => x.Type == (int)GDPTypeEnum.Component && !x.IsDeleted && !x.IsBasic && x.DFGDPId == null && x.Id != (int)DFGDPEnum.GDB_Component_RGDP
                && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617
                   && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617_Unit
                    && x.Id != (int)DFGDPEnum.GDB_Component_RGDP_Unit).OrderBy(x => x.order).Select(x => new ColumnViewTree { id = x.Id, title = x.NameEn }).ToList();
                var componentListChild = _db.DFGDP.Where(x => x.Type == (int)GDPTypeEnum.Component && !x.IsDeleted && !x.IsBasic && x.DFGDPId != null && x.Id != (int)DFGDPEnum.GDB_Component_RGDP
                && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617).ToList();
                componentList.ForEach(x => x.subs = componentListChild.Where(y => y.DFGDPId == x.id).Select(z => new ColumnView { id = z.Id, title = z.NameEn }).ToList());
                componentList.Insert(0, new ColumnViewTree() { id = -1, title = ViewShared.AllOption, subs = new List<ColumnView>() });
                ViewBag.ComponentView = componentList;


                var activityList = _db.DFGDP.Where(x => x.Type == 2 && !x.IsDeleted && !x.IsBasic && x.DFGDPId == null && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP && x.Id != (int)DFGDPEnum.GDB_Activity_Sector_RGDP
                && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617
                      && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617_Unit
                && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP_Unit).OrderBy(x => x.order).Select(x => new ColumnViewTree { id = x.Id, title = x.NameEn }).ToList();
                var activityListChild = _db.DFGDP.Where(x => x.Type == 2 && !x.IsDeleted && !x.IsBasic && x.DFGDPId != null && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP && x.Id != (int)DFGDPEnum.GDB_Activity_Sector_RGDP
                && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617
                      && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617_Unit
                && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP_Unit).ToList();
                activityList.ForEach(x => x.subs = activityListChild.Where(y => y.DFGDPId == x.id).Select(z => new ColumnView { id = z.Id, title = z.NameEn }).ToList());
                activityList.Insert(0, new ColumnViewTree() { id = -1, title = ViewShared.AllOption, subs = new List<ColumnView>() });
                ViewBag.ActivityView = activityList;

                ViewBag.PriceView = new List<ColumnView>() {
                    new ColumnView(){id=1,title="Constant Prices"},
                    new ColumnView(){id=2,title="Current Prices"}
                };

                ViewBag.GDPView = new List<ColumnView>() {
                    new ColumnView(){id=(int)GDPTypeEnum.Component,title=ViewShared.selectComponentEn},
                    new ColumnView(){id=(int)GDPTypeEnum.Activity,title=ViewShared.selectActivityEn}
                };
                ViewBag.GDPViewConst = new List<ColumnView>() {
                    new ColumnView(){id=(int)SheetsEnum.Component,title=ViewShared.selectComponentMenueEn},
                    new ColumnView(){id=(int)SheetsEnum.RGDP,title=ViewShared.selectRGDPEn},
                    new ColumnView(){id=(int)SheetsEnum.SectorRGDP,title=ViewShared.selectSectorRGDPEn},
                };
                ViewBag.GDPViewCurrent = new List<ColumnView>() {
                    new ColumnView(){id=(int)SheetsEnum.Component,title=ViewShared.selectComponentMenueEn},
                    new ColumnView(){id=(int)SheetsEnum.Activity,title=ViewShared.selectActivityMenueEn},
                };

                ViewBag.GDPViewConstChart = new List<ColumnView>() {
                    new ColumnView(){id=(int)SheetsEnum.Component,title=ViewShared.selectComponentMenueEn},

                };

                ViewBag.FrequancyView = new List<ColumnView>() {
                    new ColumnView(){id=1,title="Annual"},
                    new ColumnView(){id=2,title="Quarterly"}
                };

            }
        }
        /// <summary>
        ///  get report data
        /// </summary>
        /// <param name="sheetNum"></param>
        /// <param name="lang"></param>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <param name="prices">prices ids</param>
        /// <param name="sectors">sectors ids</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetReport(int sheetNum, string lang, int[] years, int[] quarters, string[] prices, int[] sectors, List<string> headers)
        {

            var grossDomesticViewModel = _grossDomesticRepository.GetReport(sheetNum, lang, years, quarters, prices, sectors, headers);

            return Json(grossDomesticViewModel);

        }
        /// <summary>
        /// get pie chart data
        /// </summary>
        /// <param name="sheetNum"></param>
        /// <param name="lang"></param>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <param name="prices">prices ids</param>
        /// <param name="sectors">sectors ids</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetPieReport(int sheetNum, string lang, int[] years, int[] quarters, string[] prices
          , int[] sectors, List<string> headers)
        {

            var grossDomesticViewModel = _grossDomesticRepository.GetPieReport(sheetNum, lang, years, quarters, prices, sectors, headers);
            DFUnit unit;
            string unitText = "";
            if (sheetNum == (int)SheetsEnum.Component)
            {
               
                unit = _dFUnitRepository.GetByID((int)DFUnitEnum.BillionEGP);
            }
            else
            {
                
                unit = _dFUnitRepository.GetByID((int)DFUnitEnum.MillionEGP);
            }

            if (lang == "en")
            {
                unitText = " " + unit.NameEn + "\r\n";
            }
            else
            {
                unitText =" "+ unit.NameAr + "\r\n";
            }
            return Json(new { data = grossDomesticViewModel, unit = unitText });

        }
        /// <summary>
        /// get line and bar chart data
        /// </summary>
        /// <param name="sheetNum"></param>
        /// <param name="lang"></param>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <param name="prices">prices ids</param>
        /// <param name="sectors">sectors ids</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetChartReport(int sheetNum, string lang, int[] years, int[] quarters, string[] prices
        , int[] sectors, List<string> headers, string chartType = "")
        {

            var grossDomesticViewModel = _grossDomesticRepository.GetChartReport(sheetNum, lang, years, quarters, prices, sectors, headers);

            DFUnit unit;
            string unitText = "";
            if (sheetNum == (int)SheetsEnum.Component)
            {
                unit = _dFUnitRepository.GetByID((int)DFUnitEnum.BillionEGP);
            }
            else
            {
                unit = _dFUnitRepository.GetByID((int)DFUnitEnum.MillionEGP);
            }

            if (lang == "en")
            {
                unitText = unit.NameEn;
            }
            else
            {
                unitText = unit.NameAr;
            }

            return Json(new { data = grossDomesticViewModel, unit = unitText });

        }

    }
}