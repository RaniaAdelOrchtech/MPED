using MPMAR.Analytics.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MPMAR.Analytics.Data.Enums;
using MPMAR.Business.Interfaces;
using MPMAR.Data.Enums;

namespace MPMAR.Business.Services
{
    public class GrossDomesticRepository : IGrossDomesticRepository
    {
        private readonly AnalyticsDbContext _db;
        //private readonly double billion = 1000000000;
        //private readonly double million = 1000000;
        public GrossDomesticRepository(AnalyticsDbContext db)
        {
            _db = db;
        }

        public ChartsViewModel.ReportViewModel GetChartReport(int sheetNum, string lang, int[] years, int[] quarters, string[] prices, int[] sectors, List<string> headers, string chartType = "")
        {
            var repoViewModel = new ChartsViewModel.ReportViewModel();
            var repoViewModelListData = new List<ChartsViewModel.ReportModel>();

            //add total quarter if no quarters exist
            if (quarters.Length < 1)
                quarters = new int[1] { (int)DFQuarterEnum.Total };
            //add total sector if no quarters exist
            if (sectors.Length < 1)
                sectors = new int[1] { (int)DFSectorEnum.Total };

            if (sheetNum == (int)SheetsEnum.Component)
            {
                List<GrossDomesticComponentViewModel> grossDomesticComponents = null;
                //constant
                if (prices.Contains("1"))
                {
                    var componentConst = GetComponentConst(years, quarters).OrderBy(x => x.DFYearFiscal.Order);
                    //multiply values to billion to be as their unit
                    grossDomesticComponents = componentConst.Select(x => new GrossDomesticComponentViewModel()
                    {
                        ExportsOfGoodsAndServices = x.ExportsOfGoodsAndServices,
                        GovernmentConsumption = x.GovernmentConsumption,
                        GrossCapitalFormation = x.GrossCapitalFormation,
                        ImportsOfGoodsAndServices = x.ImportsOfGoodsAndServices,
                        PrivateConsumption = x.PrivateConsumption,
                        TotalGrossDomesticProductAtMarketPrices = x.TotalGrossDomesticProductAtMarketPrices,
                        _Quarter = (lang == null || lang.ToLower() == "ar") ? x.DFQuarter.NameAr : x.DFQuarter.NameEn,
                        FiscalYear = x.DFYearFiscal.Name
                    }).ToList();
                }
                //current
                else if (prices.Contains("2"))
                {
                    var componentCurrent = GetComponentCurrent(years, quarters).OrderBy(x => x.DFYearFiscal.Order);

                    //multiply values to billion to be as their unit
                    grossDomesticComponents = componentCurrent.Select(x => new GrossDomesticComponentViewModel()
                    {
                        ExportsOfGoodsAndServices = x.ExportsOfGoodsAndServices,
                        GovernmentConsumption = x.GovernmentConsumption,
                        GrossCapitalFormation = x.GrossCapitalFormation,
                        ImportsOfGoodsAndServices = x.ImportsOfGoodsAndServices,
                        PrivateConsumption = x.PrivateConsumption,
                        TotalGrossDomesticProductAtMarketPrices = x.TotalGrossDomesticProductAtMarketPrices,
                        _Quarter = (lang == null || lang.ToLower() == "ar") ? x.DFQuarter.NameAr : x.DFQuarter.NameEn,
                        FiscalYear = x.DFYearFiscal.Name
                    }).ToList();
                }


                if (lang == null || lang.ToLower() == "ar")
                {

                    var headersData = _db.DFGDP.Where(x => headers.Contains(x.NameAr) && x.Type == (int)ReportEnum.GDP_Component).OrderBy(x => x.order).ToList();

                    //get values which equal property name(header Name)
                    foreach (var header in headersData)
                    {
                        var viewModelData = new ChartsViewModel.ReportModel();
                        viewModelData.name = header.NameAr;
                        foreach (var item in grossDomesticComponents)
                        {
                            var val = new object();
                            val = item.GetType().GetProperty(header.Name).GetValue(item, null);

                            var numFlag = double.TryParse("" + val, out double num);
                            num = Math.Round(num, 2);

                            viewModelData.data.Add(numFlag ? num : chartType == "line" ? null : (double?)0);
                        }
                        repoViewModelListData.Add(viewModelData);
                    }

                }
                else
                {

                    var headersData = _db.DFGDP.Where(x => headers.Contains(x.NameEn) && x.Type == (int)ReportEnum.GDP_Component).OrderBy(x => x.order).ToList();
                    //get values which equal property name(header Name)
                    foreach (var header in headersData)
                    {
                        var viewModelData = new ChartsViewModel.ReportModel();
                        viewModelData.name = header.NameEn;
                        foreach (var item in grossDomesticComponents)
                        {
                            var val = new object();
                            val = item.GetType().GetProperty(header.Name).GetValue(item, null);
                            var numFlag = double.TryParse("" + val, out double num);
                            num = Math.Round(num, 2);

                            viewModelData.data.Add(numFlag ? num : chartType == "line" ? null : (double?)0);
                        }
                        repoViewModelListData.Add(viewModelData);
                    }

                }
                repoViewModel.Data = repoViewModelListData;
                repoViewModel.Years = grossDomesticComponents.Select(x => $"{x.FiscalYear} {x._Quarter}").ToList();
                return repoViewModel;


            }
            else if (sheetNum == (int)SheetsEnum.Activity)
            {

                //multiply values to million to be as their unit
                var grossDomesticActivities = GetActivity(years, quarters, sectors).OrderBy(x => x.DFYear.Order).Select(x => new GrossDomesticActivity()
                {
                    AccommodationAndFoodServiceActivities = x.AccommodationAndFoodServiceActivities,
                    AgricultureForestryFishing = x.AgricultureForestryFishing,
                    BusinessServices = x.BusinessServices,
                    Communication = x.Communication,
                    Construction = x.Construction,
                    Education = x.Education,
                    Electricity = x.Electricity,
                    FinancialIntermediariesAuxiliaryServices = x.FinancialIntermediariesAuxiliaryServices,
                    Gas = x.Gas,
                    GeneralGovernment = x.GeneralGovernment,
                    Health = x.Health,
                    Information = x.Information,
                    ManufacturingIndustries = x.ManufacturingIndustries,
                    MiningQuarrying = x.MiningQuarrying,
                    OtherExtraction = x.OtherExtraction,
                    OtherManufacturing = x.OtherManufacturing,
                    OtherServices = x.OtherServices,
                    Petroleum = x.Petroleum,
                    petroleumRefining = x.petroleumRefining,
                    RealEstateActivitie = x.RealEstateActivitie,
                    RealEstateOwnership = x.RealEstateOwnership,
                    SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                    WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,
                    SocialServices = x.SocialServices,
                    SuezcCanal = x.SuezcCanal,
                    TotalGDPAtFactorCost = x.TotalGDPAtFactorCost,
                    WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                    TransportationAndStorage = x.TransportationAndStorage,
                    _Quarter = (lang == null || lang.ToLower() == "ar") ? x.DFQuarter.NameAr : x.DFQuarter.NameEn,
                    Sector = (lang == null || lang.ToLower() == "ar") ? x.DFSector.NameAr : x.DFSector.NameEn,
                    _Year = x.DFYear.Name
                }).ToList();

                if (lang == null || lang.ToLower() == "ar")
                {

                    var headersData = _db.DFGDP.Where(x => headers.Contains(x.NameAr) && x.Type == (int)ReportEnum.GDP_Activity).OrderBy(x => x.order).ToList();
                    //get values which equal property name(header Name)
                    foreach (var header in headersData)
                    {
                        var viewModelData = new ChartsViewModel.ReportModel();
                        viewModelData.name = header.NameAr;
                        foreach (var item in grossDomesticActivities)
                        {
                            var val = new object();
                            val = item.GetType().GetProperty(header.Name).GetValue(item, null);
                            var numFlag = double.TryParse("" + val, out double num);
                            num = Math.Round(num, 2);

                            viewModelData.data.Add(numFlag ? num : chartType == "line" ? null : (double?)0);
                        }
                        repoViewModelListData.Add(viewModelData);
                    }
                }
                else
                {
                    var headersData = _db.DFGDP.Where(x => headers.Contains(x.NameEn) && x.Type == (int)ReportEnum.GDP_Activity).OrderBy(x => x.order).ToList();
                    headers = headersData.Select(x => x.NameEn).ToList();
                    //get values which equal property name(header Name)
                    foreach (var header in headersData)
                    {
                        var viewModelData = new ChartsViewModel.ReportModel();
                        viewModelData.name = header.NameEn;
                        foreach (var item in grossDomesticActivities)
                        {
                            var val = new object();
                            val = item.GetType().GetProperty(header.Name).GetValue(item, null);
                            var numFlag = double.TryParse("" + val, out double num);
                            num = Math.Round(num, 2);

                            viewModelData.data.Add(numFlag ? num : chartType == "line" ? null : (double?)0);
                        }
                        repoViewModelListData.Add(viewModelData);
                    }

                }
                repoViewModel.Data = repoViewModelListData;
                repoViewModel.Years = grossDomesticActivities.Select(x => $"{x._Year} {x._Quarter} {x.Sector}").ToList();
                return repoViewModel;
            }
            
            return null;
        }



        public List<ChartsViewModel.PieViewModel> GetPieReport(int sheetNum, string lang, int[] years, int[] quarters, string[] prices, int[] sectors, List<string> headers)
        {
            //add total quarter if no quarters exist
            if (quarters.Length < 1)
                quarters = new int[1] { (int)DFQuarterEnum.Total };
            //add total sector if no quarters exist
            if (sectors.Length < 1)
                sectors = new int[1] { (int)DFSectorEnum.Total };
            var finalResult = new List<ChartsViewModel.PieViewModel>();

            if (sheetNum == (int)SheetsEnum.Component)
            {
                GrossDomesticComponentViewModel grossDomesticComponents = null;
                //constant
                if (prices.Contains("1"))
                {
                    var componentConst = GetComponentConst(years, quarters);
                    //multiply values to billion to be as their unit
                    grossDomesticComponents = componentConst.Select(x => new GrossDomesticComponentViewModel()
                    {
                        ExportsOfGoodsAndServices = x.ExportsOfGoodsAndServices,
                        GovernmentConsumption = x.GovernmentConsumption,
                        GrossCapitalFormation = x.GrossCapitalFormation,
                        ImportsOfGoodsAndServices = x.ImportsOfGoodsAndServices,
                        PrivateConsumption = x.PrivateConsumption,
                        TotalGrossDomesticProductAtMarketPrices = x.TotalGrossDomesticProductAtMarketPrices,
                    }).FirstOrDefault();
                }
                //current
                else if (prices.Contains("2"))
                {
                    var componentCurrent = GetComponentCurrent(years, quarters);
                    //multiply values to billion to be as their unit
                    grossDomesticComponents = componentCurrent.Select(x => new GrossDomesticComponentViewModel()
                    {
                        ExportsOfGoodsAndServices = x.ExportsOfGoodsAndServices,
                        GovernmentConsumption = x.GovernmentConsumption,
                        GrossCapitalFormation = x.GrossCapitalFormation,
                        ImportsOfGoodsAndServices = x.ImportsOfGoodsAndServices,
                        PrivateConsumption = x.PrivateConsumption,
                        TotalGrossDomesticProductAtMarketPrices = x.TotalGrossDomesticProductAtMarketPrices,
                    }).FirstOrDefault();
                }

                if (grossDomesticComponents == null)
                    return finalResult;

                if (lang == null || lang.ToLower() == "ar")
                {

                    var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameAr)) && x.Type == (int)ReportEnum.GDP_Component).OrderBy(x => x.order).ToList();
                    //if only one selected compare it with the others
                    if (headersData.Count == 1)
                    {
                        var allOtherHeaders = _db.DFGDP.Where(x =>
                        x.Id != (int)DFGDPEnum.GDB_Component_RGDP_Unit
                        && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617_Unit
                        && x.Id != (int)DFGDPEnum.GDB_Component_RGDP
                        && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617
                         && !x.IsBasic && !headers.Contains(x.NameAr)
                         && x.Type == (int)ReportEnum.GDP_Component);

                        double otherHeadersValue = 0;
                        foreach (var otherHeader in allOtherHeaders)
                        {
                            var val = new object();

                            val = grossDomesticComponents.GetType().GetProperty(otherHeader.Name).GetValue(grossDomesticComponents, null);

                            var numFlag = double.TryParse("" + val, out double num);
                            num = Math.Round(num, 2);

                            otherHeadersValue += numFlag ? num : 0;
                        }
                        var resultItem = new ChartsViewModel.PieViewModel("الباقي", otherHeadersValue);
                        finalResult.Add(resultItem);

                        var val2 = new object();
                        val2 = grossDomesticComponents.GetType().GetProperty(headersData[0].Name).GetValue(grossDomesticComponents, null);

                        var numFlag2 = double.TryParse("" + val2, out double num2);
                        num2 = Math.Round(num2, 2);

                        var resultItem2 = new ChartsViewModel.PieViewModel(headersData[0].NameAr, numFlag2 ? num2 : 0);

                        finalResult.Add(resultItem2);
                    }
                    else
                    {
                        //get values which equal property name(header Name)
                        foreach (var header in headersData)
                        {
                            var val = new object();
                            val = grossDomesticComponents.GetType().GetProperty(header.Name).GetValue(grossDomesticComponents, null);

                            var numFlag = double.TryParse("" + val, out double num);
                            num = Math.Round(num, 2);

                            var resultItem = new ChartsViewModel.PieViewModel(header.NameAr, numFlag ? num : 0);
                            finalResult.Add(resultItem);
                        }
                    }

                }
                else
                {

                    var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameEn)) && x.Type == (int)ReportEnum.GDP_Component).OrderBy(x => x.order).ToList();
                    //if only one selected compare it with the others
                    if (headersData.Count == 1)
                    {
                        var allOtherHeaders = _db.DFGDP.Where(x =>
                        x.Id != (int)DFGDPEnum.GDB_Component_RGDP_Unit
                        && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617_Unit
                        && x.Id != (int)DFGDPEnum.GDB_Component_RGDP
                        && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617
                         && !x.IsBasic && !headers.Contains(x.NameEn)
                         && x.Type == (int)ReportEnum.GDP_Component);

                        double otherHeadersValue = 0;
                        foreach (var otherHeader in allOtherHeaders)
                        {
                            var val = new object();
                            val = grossDomesticComponents.GetType().GetProperty(otherHeader.Name).GetValue(grossDomesticComponents, null);

                            var numFlag = double.TryParse("" + val, out double num);
                            num = Math.Round(num, 2);

                            otherHeadersValue += numFlag ? num : 0;
                        }
                        var resultItem = new ChartsViewModel.PieViewModel("Others", otherHeadersValue);
                        finalResult.Add(resultItem);

                        var val2 = new object();
                        val2 = grossDomesticComponents.GetType().GetProperty(headersData[0].Name).GetValue(grossDomesticComponents, null);
                        var numFlag2 = double.TryParse("" + val2, out double num2);
                        num2 = Math.Round(num2, 2);
                        var resultItem2 = new ChartsViewModel.PieViewModel(headersData[0].NameEn, numFlag2 ? num2 : 0);
                        finalResult.Add(resultItem2);
                    }

                    else
                    {
                        //get values which equal property name(header Name)
                        foreach (var header in headersData)
                        {
                            var val = new object();
                            val = grossDomesticComponents.GetType().GetProperty(header.Name).GetValue(grossDomesticComponents, null);
                            var numFlag = double.TryParse("" + val, out double num);
                            num = Math.Round(num, 2);

                            var resultItem = new ChartsViewModel.PieViewModel(header.NameEn, numFlag ? num : 0);
                            finalResult.Add(resultItem);
                        }
                    }
                }

            }
            else if (sheetNum == (int)SheetsEnum.Activity)
            {
                //multiply values to million to be as their unit
                var grossDomesticActivities = GetActivity(years, quarters, sectors).Select(x => new GrossDomesticActivity()
                {
                    AccommodationAndFoodServiceActivities = x.AccommodationAndFoodServiceActivities,
                    AgricultureForestryFishing = x.AgricultureForestryFishing,
                    BusinessServices = x.BusinessServices,
                    Communication = x.Communication,
                    Construction = x.Construction,
                    Education = x.Education,
                    Electricity = x.Electricity,
                    FinancialIntermediariesAuxiliaryServices = x.FinancialIntermediariesAuxiliaryServices,
                    Gas = x.Gas,
                    GeneralGovernment = x.GeneralGovernment,
                    Health = x.Health,
                    Information = x.Information,
                    OtherExtraction = x.OtherExtraction,
                    OtherManufacturing = x.OtherManufacturing,
                    OtherServices = x.OtherServices,
                    Petroleum = x.Petroleum,
                    petroleumRefining = x.petroleumRefining,
                    RealEstateActivitie = x.RealEstateActivitie,
                    RealEstateOwnership = x.RealEstateOwnership,
                    SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                    WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,
                    SocialServices = x.SocialServices,
                    SuezcCanal = x.SuezcCanal,
                    TotalGDPAtFactorCost = x.TotalGDPAtFactorCost,
                    WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                    TransportationAndStorage = x.TransportationAndStorage,
                    MiningQuarrying = x.MiningQuarrying,
                    ManufacturingIndustries = x.ManufacturingIndustries
                }).FirstOrDefault();

                if (grossDomesticActivities == null)
                    return finalResult;
                if (lang == null || lang.ToLower() == "ar")
                {

                    var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameAr)) && x.Type == (int)ReportEnum.GDP_Activity).OrderBy(x => x.order).ToList();

                    var headersDataFilter = headersData.Where(x =>
                       (x.DFGDPId == null && x.DFGDPs == null)
                    || (x.DFGDPId == null && x.DFGDPs != null && headersData.Where(z => z.DFGDPId == x.Id).Count() == 0)
                    || (x.DFGDPId != null && (headersData.FirstOrDefault(z => z.Id == x.DFGDPId) == null || headersData.FirstOrDefault(z => z.Id == x.DFGDPId).DFGDPs.Count() != 0))
                    ).ToList();



                    headers = headersData.Select(x => x.NameAr).ToList();
                    //if only one selected compare it with the others
                    if (headersDataFilter.Count == 1)
                    {
                        var allOtherHeaders = _db.DFGDP.Where(x => !x.IsBasic && !headers.Contains(x.NameAr) && x.Type == (int)ReportEnum.GDP_Activity
                        && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP
                        && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617
                        && x.Id != (int)DFGDPEnum.GDB_Activity_Sector_RGDP
                        && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP_Unit
                        && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617_Unit
                        && x.DFGDPId == null).ToList();

                        double otherHeadersValue = 0;
                        foreach (var otherHeader in allOtherHeaders)
                        {
                            var val = new object();
                            val = grossDomesticActivities.GetType().GetProperty(otherHeader.Name).GetValue(grossDomesticActivities, null);

                            var numFlag = double.TryParse("" + val, out double num);
                            num = Math.Round(num, 2);

                            otherHeadersValue += numFlag ? num : 0;
                        }
                        var resultItem = new ChartsViewModel.PieViewModel("الباقي", otherHeadersValue);
                        finalResult.Add(resultItem);

                        var val2 = new object();
                        val2 = grossDomesticActivities.GetType().GetProperty(headersDataFilter[0].Name).GetValue(grossDomesticActivities, null);

                        var numFlag2 = double.TryParse("" + val2, out double num2);
                        num2 = Math.Round(num2, 2);

                        var resultItem2 = new ChartsViewModel.PieViewModel(headersDataFilter[0].NameAr, numFlag2 ? num2 : 0);
                        finalResult.Add(resultItem2);
                    }
                    else
                    {
                        //get values which equal property name(header Name)
                        foreach (var header in headersDataFilter)
                        {
                            var val = new object();
                            val = grossDomesticActivities.GetType().GetProperty(header.Name).GetValue(grossDomesticActivities, null);
                            var numFlag = double.TryParse("" + val, out double num);
                            num = Math.Round(num, 2);
                            var resultItem = new ChartsViewModel.PieViewModel(header.NameAr, numFlag ? num : 0);
                            finalResult.Add(resultItem);
                        }
                    }

                }
                else
                {


                    var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameEn)) && x.Type == (int)ReportEnum.GDP_Activity).OrderBy(x => x.order).ToList();

                    var headersDataFilter = headersData.Where(x => x.DFGDPId == null || !(headersData.Contains(headersData.FirstOrDefault(y => y.Id == x.DFGDPId)))).ToList();

                    headers = headersData.Select(x => x.NameEn).ToList();
                    //if only one selected compare it with the others
                    if (headersDataFilter.Count == 1)
                    {
                        var allOtherHeaders = _db.DFGDP.Where(x => !x.IsBasic && !headers.Contains(x.NameEn) &&
                        x.Type == (int)ReportEnum.GDP_Activity
                       && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP
                        && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617
                        && x.Id != (int)DFGDPEnum.GDB_Activity_Sector_RGDP
                        && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP_Unit
                        && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617_Unit
                         && x.DFGDPId == null).ToList();

                        double otherHeadersValue = 0;
                        foreach (var otherHeader in allOtherHeaders)
                        {
                            var val = new object();
                            val = grossDomesticActivities.GetType().GetProperty(otherHeader.Name).GetValue(grossDomesticActivities, null);
                            var numFlag = double.TryParse("" + val, out double num);
                            num = Math.Round(num, 2);

                            otherHeadersValue += numFlag ? num : 0;
                        }
                        var resultItem = new ChartsViewModel.PieViewModel("Others", otherHeadersValue);
                        finalResult.Add(resultItem);

                        var val2 = new object();
                        val2 = grossDomesticActivities.GetType().GetProperty(headersDataFilter[0].Name).GetValue(grossDomesticActivities, null);
                        var numFlag2 = double.TryParse("" + val2, out double num2);
                        num2 = Math.Round(num2, 2);
                        var resultItem2 = new ChartsViewModel.PieViewModel(headersDataFilter[0].NameEn, numFlag2 ? num2 : 0);
                        finalResult.Add(resultItem2);
                    }
                    else
                    {
                        //get values which equal property name(header Name)
                        foreach (var header in headersDataFilter)
                        {
                            var val = new object();
                            val = grossDomesticActivities.GetType().GetProperty(header.Name).GetValue(grossDomesticActivities, null);
                            var numFlag = double.TryParse("" + val, out double num);
                            num = Math.Round(num, 2);
                            var resultItem = new ChartsViewModel.PieViewModel(header.NameEn, numFlag ? num : 0);
                            finalResult.Add(resultItem);
                        }
                    }
                }
            }
           
            return finalResult;
        }

        public ReportViewModel GetReport(int sheetNum, string lang, int[] years, int[] quarters, string[] prices, int[] sectors, List<string> headers)
        {
            if (quarters.Length < 1)
                quarters = new int[1] { (int)DFQuarterEnum.Total };
            if (sectors.Length < 1)
                sectors = new int[1] { (int)DFSectorEnum.Total };
            List<string> headersNames;
            if (sheetNum == (int)SheetsEnum.Component)
            {

                var resultView = new List<GrossDomesticComponentViewModel>();
                if (prices.Contains("1"))
                {
                    var componentConst = GetComponentConst(years, quarters);

                    if (lang == null || lang.ToLower() == "ar")
                    {
                        resultView = componentConst.Select(x => new GrossDomesticComponentViewModel()
                        {
                            Indicator = x.DFIndicator.NameAr,
                            _Source = x.DFSource.NameAr,
                            _Quarter = x.DFQuarter.NameAr,
                            Unit = x.DFUnit.NameAr,
                            FiscalYear = x.DFYearFiscal.NameAr,
                            ExportsOfGoodsAndServices = x.ExportsOfGoodsAndServices,
                            GovernmentConsumption = x.GovernmentConsumption,
                            GrossCapitalFormation = x.GrossCapitalFormation,
                            ImportsOfGoodsAndServices = x.ImportsOfGoodsAndServices,
                            PrivateConsumption = x.PrivateConsumption,
                            TotalGrossDomesticProductAtMarketPrices = x.TotalGrossDomesticProductAtMarketPrices,
                        }).ToList();

                        var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameAr) || x.IsBasic) && x.Type == 1 && x.Id != (int)DFGDPEnum.GDB_Component_RGDP && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617 && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617_Unit && x.Id != (int)DFGDPEnum.GDB_Component_RGDP_Unit
                        ).OrderBy(x => x.order).ToList();
                        headers = headersData.Select(x => x.NameAr).ToList();
                        headersNames = headersData.Select(x => x.Name).ToList();
                    }
                    else
                    {
                        resultView = componentConst.Select(x => new GrossDomesticComponentViewModel()
                        {
                            Indicator = x.DFIndicator.NameEn,
                            _Source = x.DFSource.NameEn,
                            _Quarter = x.DFQuarter.NameEn,
                            Unit = x.DFUnit.NameEn,
                            FiscalYear = x.DFYearFiscal.NameEn,
                            ExportsOfGoodsAndServices = x.ExportsOfGoodsAndServices,
                            GovernmentConsumption = x.GovernmentConsumption,
                            GrossCapitalFormation = x.GrossCapitalFormation,
                            ImportsOfGoodsAndServices = x.ImportsOfGoodsAndServices,
                            PrivateConsumption = x.PrivateConsumption,
                            TotalGrossDomesticProductAtMarketPrices = x.TotalGrossDomesticProductAtMarketPrices,
                        }).ToList();

                        var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameEn) || x.IsBasic) && x.Type == 1 && x.Id != (int)DFGDPEnum.GDB_Component_RGDP && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617 && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617_Unit && x.Id != (int)DFGDPEnum.GDB_Component_RGDP_Unit
                        ).OrderBy(x => x.order).ToList();
                        headers = headersData.Select(x => x.NameEn).ToList();
                        headersNames = headersData.Select(x => x.Name).ToList();
                    }
                    //remove quarter column from headers if tota quarter exist
                    if (quarters.Contains((int)DFQuarterEnum.Total))
                    {
                        headersNames.Remove("_Quarter");
                        headers.Remove("Quarter");
                        headers.Remove("الربع");
                    }
                    return GenerateReportData(headers, resultView, headersNames);

                }
                else if (prices.Contains("2"))
                {
                    var componentCurrent = GetComponentCurrent(years, quarters);

                    if (lang == null || lang.ToLower() == "ar")
                    {
                        resultView = componentCurrent.Select(x => new GrossDomesticComponentViewModel()
                        {
                            Indicator = x.DFIndicator.NameAr,
                            _Source = x.DFSource.NameAr,
                            _Quarter = x.DFQuarter.NameAr,
                            Unit = x.DFUnit.NameAr,
                            FiscalYear = x.DFYearFiscal.NameAr,
                            ExportsOfGoodsAndServices = x.ExportsOfGoodsAndServices,
                            GovernmentConsumption = x.GovernmentConsumption,
                            GrossCapitalFormation = x.GrossCapitalFormation,
                            ImportsOfGoodsAndServices = x.ImportsOfGoodsAndServices,
                            PrivateConsumption = x.PrivateConsumption,
                            TotalGrossDomesticProductAtMarketPrices = x.TotalGrossDomesticProductAtMarketPrices,
                        }).ToList();

                        var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameAr) || x.IsBasic) && x.Type == 1 && x.Id != (int)DFGDPEnum.GDB_Component_RGDP && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617 && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617_Unit && x.Id != (int)DFGDPEnum.GDB_Component_RGDP_Unit
                        ).OrderBy(x => x.order).ToList();
                        headers = headersData.Select(x => x.NameAr).ToList();
                        headersNames = headersData.Select(x => x.Name).ToList();
                    }
                    else
                    {
                        resultView = componentCurrent.Select(x => new GrossDomesticComponentViewModel()
                        {
                            Indicator = x.DFIndicator.NameEn,
                            _Source = x.DFSource.NameEn,
                            _Quarter = x.DFQuarter.NameEn,
                            Unit = x.DFUnit.NameEn,
                            FiscalYear = x.DFYearFiscal.NameEn,
                            ExportsOfGoodsAndServices = x.ExportsOfGoodsAndServices,
                            GovernmentConsumption = x.GovernmentConsumption,
                            GrossCapitalFormation = x.GrossCapitalFormation,
                            ImportsOfGoodsAndServices = x.ImportsOfGoodsAndServices,
                            PrivateConsumption = x.PrivateConsumption,
                            TotalGrossDomesticProductAtMarketPrices = x.TotalGrossDomesticProductAtMarketPrices,
                        }).ToList();

                        var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameEn) || x.IsBasic) && x.Type == 1 && x.Id != (int)DFGDPEnum.GDB_Component_RGDP && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617 && x.Id != (int)DFGDPEnum.GDB_Component_RGDP1617_Unit && x.Id != (int)DFGDPEnum.GDB_Component_RGDP_Unit
                        ).OrderBy(x => x.order).ToList();
                        headers = headersData.Select(x => x.NameEn).ToList();
                        headersNames = headersData.Select(x => x.Name).ToList();
                    }
                    //remove quarter column from headers if tota quarter exist
                    if (quarters.Contains((int)DFQuarterEnum.Total))
                    {
                        headersNames.Remove("_Quarter");
                        headers.Remove("Quarter");
                        headers.Remove("الربع");
                    }
                    return GenerateReportData(headers, resultView, headersNames);
                }
            }
            else if (sheetNum == (int)SheetsEnum.Activity)
            {
                var resultView = new List<GrossDomesticActivity>();

                var activityCurrent = GetActivity(years, quarters, sectors);

                if (lang == null || lang.ToLower() == "ar")
                {
                    resultView = activityCurrent.Select(x => new GrossDomesticActivity()
                    {
                        Indicator = x.DFIndicator.NameAr,
                        _Source = x.DFSource.NameAr,
                        _Quarter = x.DFQuarter.NameAr,
                        Unit = x.DFUnit.NameAr,
                        _Year = x.DFYear.NameAr,
                        Sector = x.DFSector.NameAr,
                        AccommodationAndFoodServiceActivities = x.AccommodationAndFoodServiceActivities,
                        AgricultureForestryFishing = x.AgricultureForestryFishing,
                        BusinessServices = x.BusinessServices,
                        Communication = x.Communication,
                        Construction = x.Construction,
                        Education = x.Education,
                        Electricity = x.Electricity,
                        FinancialIntermediariesAuxiliaryServices = x.FinancialIntermediariesAuxiliaryServices,
                        Gas = x.Gas,
                        GeneralGovernment = x.GeneralGovernment,
                        Health = x.Health,
                        Information = x.Information,
                        ManufacturingIndustries = x.ManufacturingIndustries,
                        MiningQuarrying = x.MiningQuarrying,
                        OtherExtraction = x.OtherExtraction,
                        OtherManufacturing = x.OtherManufacturing,
                        OtherServices = x.OtherServices,
                        Petroleum = x.Petroleum,
                        petroleumRefining = x.petroleumRefining,
                        RealEstateActivitie = x.RealEstateActivitie,
                        RealEstateOwnership = x.RealEstateOwnership,
                        SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                        WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,
                        SocialServices = x.SocialServices,
                        SuezcCanal = x.SuezcCanal,
                        TotalGDPAtFactorCost = x.TotalGDPAtFactorCost,
                        WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                        TransportationAndStorage = x.TransportationAndStorage,
                    }).ToList();

                    var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameAr) || x.IsBasic) && x.Type == 2 && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP_Unit && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617_Unit && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617 && x.Id != (int)DFGDPEnum.GDB_Activity_Total_GDP_GrowthRate_At_Factor_Cost
                    ).OrderBy(x => x.order).ToList();
                    headers = headersData.Select(x => x.NameAr).ToList();
                    headersNames = headersData.Select(x => x.Name).ToList();
                }
                else
                {
                    resultView = activityCurrent.Select(x => new GrossDomesticActivity()
                    {
                        Indicator = x.DFIndicator.NameEn,
                        _Source = x.DFSource.NameEn,
                        _Quarter = x.DFQuarter.NameEn,
                        Unit = x.DFUnit.NameEn,
                        _Year = x.DFYear.NameEn,
                        Sector = x.DFSector.NameEn,
                        AccommodationAndFoodServiceActivities = x.AccommodationAndFoodServiceActivities,
                        AgricultureForestryFishing = x.AgricultureForestryFishing,
                        BusinessServices = x.BusinessServices,
                        Communication = x.Communication,
                        Construction = x.Construction,
                        Education = x.Education,
                        Electricity = x.Electricity,
                        FinancialIntermediariesAuxiliaryServices = x.FinancialIntermediariesAuxiliaryServices,
                        Gas = x.Gas,
                        GeneralGovernment = x.GeneralGovernment,
                        Health = x.Health,
                        Information = x.Information,
                        ManufacturingIndustries = x.ManufacturingIndustries,
                        MiningQuarrying = x.MiningQuarrying,
                        OtherExtraction = x.OtherExtraction,
                        OtherManufacturing = x.OtherManufacturing,
                        OtherServices = x.OtherServices,
                        Petroleum = x.Petroleum,
                        petroleumRefining = x.petroleumRefining,
                        RealEstateActivitie = x.RealEstateActivitie,
                        RealEstateOwnership = x.RealEstateOwnership,
                        SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                        WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,
                        SocialServices = x.SocialServices,
                        SuezcCanal = x.SuezcCanal,
                        TotalGDPAtFactorCost = x.TotalGDPAtFactorCost,
                        WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                        TransportationAndStorage = x.TransportationAndStorage,
                    }).ToList();

                    var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameEn) || x.IsBasic) && x.Type == 2 && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP_Unit && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617_Unit && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617 && x.Id != (int)DFGDPEnum.GDB_Activity_Total_GDP_GrowthRate_At_Factor_Cost
                                      ).OrderBy(x => x.order).ToList();

                    headers = headersData.Select(x => x.NameEn).ToList();
                    headersNames = headersData.Select(x => x.Name).ToList();
                }
                //remove quarter column from headers if tota quarter exist
                if (quarters.Contains((int)DFQuarterEnum.Total))
                {
                    headersNames.Remove("_Quarter");
                    headers.Remove("Quarter");
                    headers.Remove("الربع");
                }
                return GenerateReportData(headers, resultView, headersNames);
            }
            else if (sheetNum == (int)SheetsEnum.RGDP)
            {
                var resultView = new List<GrossDomesticComponentViewModel>();

                var rgdp = GetRGDP(years, quarters);

                if (lang == null || lang.ToLower() == "ar")
                {
                    resultView = rgdp.Select(x => new GrossDomesticComponentViewModel()
                    {
                        Indicator = x.DFIndicator.NameAr,
                        _Source = x.DFSource.NameAr,
                        _Quarter = x.DFQuarter.NameAr,
                        Unit = x.DFUnit.NameAr,
                        FiscalYear = x.DFYear.NameAr,
                        RealGrowthRate = x.GrowthRate,
                    }).ToList();

                    var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameAr) || x.IsBasic || x.Id == (int)DFGDPEnum.GDB_Component_RGDP) && x.Type == 1).ToList();
                    headers = headersData.Select(x => x.NameAr).ToList();
                    headersNames = headersData.Select(x => x.Name).ToList();
                }
                else
                {
                    resultView = rgdp.Select(x => new GrossDomesticComponentViewModel()
                    {
                        Indicator = x.DFIndicator.NameEn,
                        _Source = x.DFSource.NameEn,
                        _Quarter = x.DFQuarter.NameEn,
                        Unit = x.DFUnit.NameEn,
                        FiscalYear = x.DFYear.NameEn,
                        RealGrowthRate = x.GrowthRate,
                    }).ToList();

                    var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameEn) || x.IsBasic || x.Id == (int)DFGDPEnum.GDB_Component_RGDP) && x.Type == 1).ToList();
                    headers = headersData.Select(x => x.NameEn).ToList();
                    headersNames = headersData.Select(x => x.Name).ToList();
                }
                //remove quarter column from headers if tota quarter exist
                if (quarters.Contains((int)DFQuarterEnum.Total))
                {
                    headersNames.Remove("_Quarter");
                    headers.Remove("Quarter");
                    headers.Remove("الربع");
                }
                return GenerateReportData(headers, resultView, headersNames);
            }
            else if (sheetNum == (int)SheetsEnum.SectorRGDP)
            {
                var GDB_Activity_Total_GDP_At_Factor_Cost = _db.DFGDP.Find((int)DFGDPEnum.GDB_Activity_Total_GDP_At_Factor_Cost);
                var GDB_Activity_Total_GDP_GrowthRate_At_Factor_Cost = _db.DFGDP.Find((int)DFGDPEnum.GDB_Activity_Total_GDP_GrowthRate_At_Factor_Cost);

                var resultView = new List<GrossDomesticActivity>();

                var sectorrgdp = GetSectorRGDP(years, quarters, sectors);

                if (lang == null || lang.ToLower() == "ar")
                {
                    resultView = sectorrgdp.Select(x => new GrossDomesticActivity()
                    {
                        Indicator = x.DFIndicator.NameAr,
                        _Source = x.DFSource.NameAr,
                        _Quarter = x.DFQuarter.NameAr,
                        Unit = x.DFUnit.NameAr,
                        _Year = x.DFYear.NameAr,
                        Sector = x.DFSector.NameAr,
                        AccommodationAndFoodServiceActivities = x.AccommodationAndFoodServiceActivities,
                        AgricultureForestryFishing = x.AgricultureForestryFishing,
                        BusinessServices = x.BusinessServices,
                        Communication = x.Communication,
                        Construction = x.Construction,
                        Education = x.Education,
                        Electricity = x.Electricity,
                        FinancialIntermediariesAuxiliaryServices = x.FinancialIntermediariesAuxiliaryServices,
                        Gas = x.Gas,
                        GeneralGovernment = x.GeneralGovernment,
                        Health = x.Health,
                        Information = x.Information,
                        ManufacturingIndustries = x.ManufacturingIndustries,
                        MiningQuarrying = x.MiningQuarrying,
                        OtherExtraction = x.OtherExtraction,
                        OtherManufacturing = x.OtherManufacturing,
                        OtherServices = x.OtherServices,
                        Petroleum = x.Petroleum,
                        petroleumRefining = x.petroleumRefining,
                        RealEstateActivitie = x.RealEstateActivitie,
                        RealEstateOwnership = x.RealEstateOwnership,
                        SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                        WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,
                        SocialServices = x.SocialServices,
                        SuezcCanal = x.SuezcCanal,
                        TotalGDPGrowthRateAtFactorCost = x.TotalGDPAtFactorCost,
                        WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                        TransportationAndStorage = x.TransportationAndStorage,
                    }).ToList();


                    if (headers.Contains(GDB_Activity_Total_GDP_At_Factor_Cost.NameAr))
                    {
                        var indexHeader = headers.IndexOf(GDB_Activity_Total_GDP_At_Factor_Cost.NameAr);
                        headers[indexHeader] = GDB_Activity_Total_GDP_GrowthRate_At_Factor_Cost.NameAr;
                    }



                    var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameAr) || x.IsBasic) && x.Type == 2 && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP_Unit && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617_Unit && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617 && x.Id != (int)DFGDPEnum.GDB_Activity_Total_GDP_At_Factor_Cost
                    ).OrderBy(x => x.order).ToList();

                    headers = headersData.Select(x => x.NameAr).ToList();
                    headersNames = headersData.Select(x => x.Name).ToList();
                }
                else
                {
                    resultView = sectorrgdp.Select(x => new GrossDomesticActivity()
                    {
                        Indicator = x.DFIndicator.NameEn,
                        _Source = x.DFSource.NameEn,
                        _Quarter = x.DFQuarter.NameEn,
                        Unit = x.DFUnit.NameEn,
                        _Year = x.DFYear.NameEn,
                        Sector = x.DFSector.NameEn,
                        AccommodationAndFoodServiceActivities = x.AccommodationAndFoodServiceActivities,
                        AgricultureForestryFishing = x.AgricultureForestryFishing,
                        BusinessServices = x.BusinessServices,
                        Communication = x.Communication,
                        Construction = x.Construction,
                        Education = x.Education,
                        Electricity = x.Electricity,
                        FinancialIntermediariesAuxiliaryServices = x.FinancialIntermediariesAuxiliaryServices,
                        Gas = x.Gas,
                        GeneralGovernment = x.GeneralGovernment,
                        Health = x.Health,
                        Information = x.Information,
                        ManufacturingIndustries = x.ManufacturingIndustries,
                        MiningQuarrying = x.MiningQuarrying,
                        OtherExtraction = x.OtherExtraction,
                        OtherManufacturing = x.OtherManufacturing,
                        OtherServices = x.OtherServices,
                        Petroleum = x.Petroleum,
                        petroleumRefining = x.petroleumRefining,
                        RealEstateActivitie = x.RealEstateActivitie,
                        RealEstateOwnership = x.RealEstateOwnership,
                        SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                        WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,
                        SocialServices = x.SocialServices,
                        SuezcCanal = x.SuezcCanal,
                        TotalGDPGrowthRateAtFactorCost = x.TotalGDPAtFactorCost,
                        WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                        TransportationAndStorage = x.TransportationAndStorage,
                    }).ToList();



                    if (headers.Contains(GDB_Activity_Total_GDP_At_Factor_Cost.NameEn))
                    {
                        var indexHeader = headers.IndexOf(GDB_Activity_Total_GDP_At_Factor_Cost.NameEn);
                        headers[indexHeader] = GDB_Activity_Total_GDP_GrowthRate_At_Factor_Cost.NameEn;
                    }

                    var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameEn) || x.IsBasic) && x.Type == 2 && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP_Unit && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617_Unit && x.Id != (int)DFGDPEnum.GDB_Activity_RGDP1617 && x.Id != (int)DFGDPEnum.GDB_Activity_Total_GDP_At_Factor_Cost
                                      ).OrderBy(x => x.order).ToList();
                    headers = headersData.Select(x => x.NameEn).ToList();
                    headersNames = headersData.Select(x => x.Name).ToList();
                }
                //remove quarter column from headers if tota quarter exist
                if (quarters.Contains((int)DFQuarterEnum.Total))
                {
                    headersNames.Remove("_Quarter");
                    headers.Remove("Quarter");
                    headers.Remove("الربع");
                }
                return GenerateReportData(headers, resultView, headersNames);
            }

            return null;


        }
        /// <summary>
        /// get SectorGrowthRate depend on years, quarters and sectors
        /// </summary>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <param name="sectors">sectors ids</param>
        /// <returns></returns>
        private List<SectorGrowthRate> GetSectorRGDP(int[] years, int[] quarters, int[] sectors)
        {
            return _db.SectorGrowthRates.Where(x => years.Contains(x.DFYearId) && quarters.Contains(x.DFQuarterId) && sectors.Contains(x.DFSectorId) && !(x.IsDeleted ?? false)).Include(x => x.DFIndicator).Include(x => x.DFSource).Include(x => x.DFQuarter).Include(x => x.DFUnit).Include(x => x.DFYear).Include(x => x.DFSector).OrderByDescending(x => x.DFYear.Order).ThenByDescending(x => x.DFQuarterId).ThenBy(x => x.DFSector.Order).ToList();
        }
        /// <summary>
        /// get RGDPGrowthRate depend on years, quarters
        /// </summary>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <returns></returns>
        private List<RGDPGrowthRate> GetRGDP(int[] years, int[] quarters)
        {
            return _db.RGDPGrowthRates.Where(x => years.Contains(x.DFYearId) && quarters.Contains(x.DFQuarterId) && !(x.IsDeleted ?? false)).Include(x => x.DFIndicator).Include(x => x.DFSource).Include(x => x.DFQuarter).Include(x => x.DFUnit).Include(x => x.DFYear).OrderByDescending(x => x.DFYear.Order).ThenByDescending(x => x.DFQuarterId).ToList();
        }
        /// <summary>
        /// get ActivityCurrent depend on years, quarters and sectors
        /// </summary>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <param name="sectors">sectors ids</param>
        /// <returns></returns>
        private List<ActivityCurrent> GetActivity(int[] years, int[] quarters, int[] sectors)
        {
            return _db.ActivityCurrents.Where(x => years.Contains(x.DFYearId) && quarters.Contains(x.DFQuarterId) && sectors.Contains(x.DFSectorId) && !(x.IsDeleted ?? false)).Include(x => x.DFIndicator).Include(x => x.DFSource).Include(x => x.DFQuarter).Include(x => x.DFUnit).Include(x => x.DFYear).Include(x => x.DFSector).OrderByDescending(x => x.DFYear.Order).ThenByDescending(x => x.DFQuarterId).ThenBy(x=>x.DFSector.Order).ToList();
        }
        /// <summary>
        /// get ComponentCurrent depend on years, quarters
        /// </summary>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <returns></returns>
        private List<ComponentCurrent> GetComponentCurrent(int[] years, int[] quarters)
        {
            return _db.ComponentCurrents.Where(x => years.Contains(x.DFYearFiscalId) && quarters.Contains(x.DFQuarterId) && !(x.IsDeleted ?? false)).Include(x => x.DFIndicator).Include(x => x.DFSource).Include(x => x.DFQuarter).Include(x => x.DFUnit).Include(x => x.DFYearFiscal).OrderByDescending(x => x.DFYearFiscal.Order).ThenByDescending(x => x.DFQuarterId).ToList();
        }
        /// <summary>
        /// get ComponentConstant depend on years, quarters
        /// </summary>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <returns></returns>
        private List<ComponentConstant> GetComponentConst(int[] years, int[] quarters)
        {
            return _db.ComponentConstants.Where(x => years.Contains(x.DFYearFiscalId) && quarters.Contains(x.DFQuarterId) && !(x.IsDeleted ?? false)).Include(x => x.DFIndicator).Include(x => x.DFSource).Include(x => x.DFQuarter).Include(x => x.DFUnit).Include(x => x.DFYearFiscal).OrderByDescending(x => x.DFYearFiscal.Order).ThenByDescending(x => x.DFQuarterId).ToList();
        }
        /// <summary>
        /// get report data for T resultView
        /// </summary>
        /// <typeparam name="T">any report class type</typeparam>
        /// <param name="headers">headrs names in view</param>
        /// <param name="resultView">list of result data </param>
        /// <param name="headersNames">headers that equal property name</param>
        /// <returns></returns>
        private static ReportViewModel GenerateReportData<T>(List<string> headers, List<T> resultView, List<string> headersNames)
        {
            List<List<object>> dataRows = new List<List<object>>();
            foreach (var item in resultView)
            {
                List<object> row = new List<object>();

                foreach (var headerName in headersNames)
                {
                    var val = new object();
                    val = item.GetType().GetProperty(headerName).GetValue(item, null);
                    double num;
                    var numFlag = double.TryParse("" + val, out num);
                    if (numFlag)
                    {
                        num = Math.Round(num, 2);
                        row.Add(num);
                    }
                    else
                    {
                        row.Add(val != null ? val : "N/A");
                    }
                }
                dataRows.Add(row);

            }
            ReportViewModel grossDomesticViewModel = new ReportViewModel(headers, dataRows);
            return grossDomesticViewModel;
        }
    }
}
