using Microsoft.EntityFrameworkCore;
using MPMAR.Analytics.Data.Enums;
using MPMAR.Analytics.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPMAR.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using MPMAR.Analytics.Data.Models;

namespace MPMAR.Business.Services
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly AnalyticsDbContext _db;
        //private readonly double million = 1000000;
        public InvestmentRepository(AnalyticsDbContext db)
        {
            _db = db;
        }

        public void Add(Investments investments)
        {
            _db.Investments.Add(investments);
            _db.SaveChanges();
        }

        public IEnumerable<Investments> GetAll()
        {
            var investmentData = _db.Investments.Where(x => !(x.isDeleted ?? false)).OrderByDescending(x => x.DFYear.Order).ThenBy(x => x.DFQuarterId).Include(x => x.DFIndicator).Include(x => x.DFSource).Include(x => x.DFQuarter).Include(x => x.DFUnit).Include(x => x.DFYear).ToList();

            return investmentData;
        }

        public Investments GetById(int id)
        {
            return _db.Investments.FirstOrDefault(x => x.Id == id);
        }

        public bool Delete(int id)
        {
            try
            {
                var model = GetById(id);
                if (model != null)
                {
                    model.isDeleted = true;

                    Update(model);

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }


        }

        public void Update(Investments investments)
        {
            _db.Investments.Attach(investments);
            _db.Entry(investments).State = EntityState.Modified;
            _db.SaveChanges();
        }



        public ChartsViewModel.ReportViewModel GetChartReport(string lang, int[] years, List<string> headers, string chartType)
        {
            //add total quarter as default
            var quarters = new int[1] { (int)DFQuarterEnum.Total };
            var resultData = GetReportResult(years, quarters);

            var resultView = new List<InvestmentViewModel>();

            var repoViewModel = new ChartsViewModel.ReportViewModel();
            var repoViewModelListData = new List<ChartsViewModel.ReportModel>();

            if (lang == null || lang.ToLower() == "ar")
            {
                //multiply values to million to be as their unit
                resultView = resultData.OrderBy(x => x.DFYear.Order).Select(x => new InvestmentViewModel()
                {
                    _Year = x.DFYear.NameAr,
                    AccommodationAndFoodServiceActivities = x.AccommodationAndFoodServiceActivities,
                    Agriculture = x.Agriculture,
                    Construction = x.Construction,
                    Education = x.Education,
                    Electricity = x.Electricity,
                    FinancialIntermediaryInsuranceAndSocialSecurity = x.FinancialIntermediaryInsuranceAndSocialSecurity,
                    Health = x.Health,
                    InformationAndCommunication = x.InformationAndCommunication,
                    NaturalGas = x.NaturalGas,
                    OtherExtractions = x.OtherExtractions,
                    OtherManufacturing = x.OtherManufacturing,
                    OtherSrvices = x.OtherSrvices,
                    Petroleum = x.Petroleum,
                    PetroleumRefining = x.PetroleumRefining,
                    RealEstateActivities = x.RealEstateActivities,
                    StorageAndTransportation = x.StorageAndTransportation,
                    SuezCanal = x.SuezCanal,
                    TotalInvestments = x.TotalInvestments,
                    WaterAndSewerage = x.WaterAndSewerage,
                    WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,

                }).ToList();

                var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameAr)) && x.Type == (int)ReportEnum.Investment).OrderBy(x => x.order).ToList();

                foreach (var header in headersData)
                {
                    var barViewModelData = new ChartsViewModel.ReportModel();
                    barViewModelData.name = header.NameAr;
                    foreach (var item in resultView)
                    {
                        var val = new object();
                        val = item.GetType().GetProperty(header.Name).GetValue(item, null);
                        var numFlag = double.TryParse("" + val, out double num);
                        num = Math.Round(num, 2);

                        barViewModelData.data.Add(numFlag ? num : chartType == "line" ? null : (double?)0);
                    }
                    repoViewModelListData.Add(barViewModelData);

                }
            }
            else
            {
                resultView = resultData.OrderBy(x => x.DFYear.Order).Select(x => new InvestmentViewModel()
                {
                    _Year = x.DFYear.NameAr,
                    AccommodationAndFoodServiceActivities = x.AccommodationAndFoodServiceActivities,
                    Agriculture = x.Agriculture,
                    Construction = x.Construction,
                    Education = x.Education,
                    Electricity = x.Electricity,
                    FinancialIntermediaryInsuranceAndSocialSecurity = x.FinancialIntermediaryInsuranceAndSocialSecurity,
                    Health = x.Health,
                    InformationAndCommunication = x.InformationAndCommunication,
                    NaturalGas = x.NaturalGas,
                    OtherExtractions = x.OtherExtractions,
                    OtherManufacturing = x.OtherManufacturing,
                    OtherSrvices = x.OtherSrvices,
                    Petroleum = x.Petroleum,
                    PetroleumRefining = x.PetroleumRefining,
                    RealEstateActivities = x.RealEstateActivities,
                    StorageAndTransportation = x.StorageAndTransportation,
                    SuezCanal = x.SuezCanal,
                    TotalInvestments = x.TotalInvestments,
                    WaterAndSewerage = x.WaterAndSewerage,
                    WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,
                }).ToList();

                var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameEn)) && x.Type == (int)ReportEnum.Investment).OrderBy(x => x.order).ToList();

                foreach (var header in headersData)
                {
                    var barViewModelData = new ChartsViewModel.ReportModel();
                    barViewModelData.name = header.NameEn;
                    foreach (var item in resultView)
                    {
                        var val = new object();
                        val = item.GetType().GetProperty(header.Name).GetValue(item, null);
                        var numFlag = double.TryParse("" + val, out double num);
                        num = Math.Round(num, 2);
                        barViewModelData.data.Add(numFlag ? num : chartType == "line" ? null : (double?)0);

                    }
                    repoViewModelListData.Add(barViewModelData);

                }
            }

            repoViewModel.Data = repoViewModelListData;
            repoViewModel.Years = resultView.Select(x => x._Year).ToList();
            return repoViewModel;
        }

        public List<ChartsViewModel.PieViewModel> GetPieReport(string lang, int[] years, int[] quarters, List<string> headers)
        {
            var finalResult = new List<ChartsViewModel.PieViewModel>();

            //add total quarter if no quarters exist
            if (quarters.Length < 1)
                quarters = new int[1] { (int)DFQuarterEnum.Total };

            var resultData = GetReportResult(years, quarters).FirstOrDefault();

            var resultView = new InvestmentViewModel();
            List<DFGDP> headersData;
            List<List<object>> dataRows = new List<List<object>>();


            resultView = new InvestmentViewModel()
            {
                //multiply values to million to be as their unit
                AccommodationAndFoodServiceActivities = resultData.AccommodationAndFoodServiceActivities,
                Agriculture = resultData.Agriculture,
                Construction = resultData.Construction,
                Education = resultData.Education,
                Electricity = resultData.Electricity,
                FinancialIntermediaryInsuranceAndSocialSecurity = resultData.FinancialIntermediaryInsuranceAndSocialSecurity,
                Health = resultData.Health,
                InformationAndCommunication = resultData.InformationAndCommunication,
                NaturalGas = resultData.NaturalGas,
                OtherExtractions = resultData.OtherExtractions,
                OtherManufacturing = resultData.OtherManufacturing,
                OtherSrvices = resultData.OtherSrvices,
                Petroleum = resultData.Petroleum,
                PetroleumRefining = resultData.PetroleumRefining,
                RealEstateActivities = resultData.RealEstateActivities,
                StorageAndTransportation = resultData.StorageAndTransportation,
                SuezCanal = resultData.SuezCanal,
                TotalInvestments = resultData.TotalInvestments,
                WaterAndSewerage = resultData.WaterAndSewerage,
                WholesaleAndRetailTrade = resultData.WholesaleAndRetailTrade,


            };

            if (lang == null || lang.ToLower() == "ar")
            {
                headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameAr)) && x.Type == 3).OrderBy(x => x.order).ToList();
                //if only one selected compare it with the others
                if (headersData.Count == 1)
                {
                    var allOtherHeaders = _db.DFGDP.Where(x => !x.IsBasic && !headers.Contains(x.NameAr) && x.Type == (int)ReportEnum.Investment);

                    double otherHeadersValue = 0;
                    foreach (var otherHeader in allOtherHeaders)
                    {
                        var val = new object();
                        val = resultView.GetType().GetProperty(otherHeader.Name).GetValue(resultView, null);
                        var numFlag = double.TryParse("" + val, out double num);
                        num = Math.Round(num, 2);

                        otherHeadersValue += numFlag ? num : 0;
                    }
                    var resultItem = new ChartsViewModel.PieViewModel("الباقي", otherHeadersValue);
                    finalResult.Add(resultItem);

                    var val2 = new object();
                    val2 = resultView.GetType().GetProperty(headersData[0].Name).GetValue(resultView, null);
                    var numFlag2 = double.TryParse("" + val2, out double num2);
                    num2 = Math.Round(num2, 2);


                    var resultItem2 = new ChartsViewModel.PieViewModel(headersData[0].NameAr, numFlag2 ? num2 : 0);
                    finalResult.Add(resultItem2);
                }

                else
                {
                    foreach (var header in headersData)
                    {
                        var val = new object();
                        val = resultView.GetType().GetProperty(header.Name).GetValue(resultView, null);
                        var numFlag = double.TryParse("" + val, out double num);
                        num = Math.Round(num, 2);

                        var resultItem = new ChartsViewModel.PieViewModel(header.NameAr, numFlag ? num : 0);
                        finalResult.Add(resultItem);
                    }
                }

            }
            else
            {
                headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameEn)) && x.Type == 3).OrderBy(x => x.order).ToList();
                //if only one selected compare it with the others
                if (headersData.Count == 1)
                {
                    var allOtherHeaders = _db.DFGDP.Where(x => !x.IsBasic && !headers.Contains(x.NameEn) && x.Type == (int)ReportEnum.Investment);

                    double otherHeadersValue = 0;
                    foreach (var otherHeader in allOtherHeaders)
                    {
                        var val = new object();
                        val = resultView.GetType().GetProperty(otherHeader.Name).GetValue(resultView, null);
                        var numFlag = double.TryParse("" + val, out double num);
                        num = Math.Round(num, 2);

                        otherHeadersValue += numFlag ? num : 0;
                    }
                    var resultItem = new ChartsViewModel.PieViewModel("Others", otherHeadersValue);
                    finalResult.Add(resultItem);

                    var val2 = new object();
                    val2 = resultView.GetType().GetProperty(headersData[0].Name).GetValue(resultView, null);
                    var numFlag2 = double.TryParse("" + val2, out double num2);
                    num2 = Math.Round(num2, 2);
                    var resultItem2 = new ChartsViewModel.PieViewModel(headersData[0].NameEn, numFlag2 ? num2 : 0);
                    finalResult.Add(resultItem2);
                }

                else
                {
                    foreach (var header in headersData)
                    {
                        var val = new object();
                        val = resultView.GetType().GetProperty(header.Name).GetValue(resultView, null);
                        var numFlag = double.TryParse("" + val, out double num);
                        num = Math.Round(num, 2);
                        var resultItem = new ChartsViewModel.PieViewModel(header.NameEn, numFlag ? num : 0);
                        finalResult.Add(resultItem);
                    }
                }



            }

            return finalResult;
        }

        public ReportViewModel GetReport(string lang, int[] years, int[] quarters, List<string> headers)
        {
            //add total quarter if no quarters exist
            if (quarters.Length < 1)
                quarters = new int[1] { (int)DFQuarterEnum.Total };
            var resultData = GetReportResult(years, quarters);

            var resultView = new List<InvestmentViewModel>();
            List<string> headersNames;
            List<List<object>> dataRows = new List<List<object>>();
            if (lang == null || lang.ToLower() == "ar")
            {
                resultView = resultData.Select(x => new InvestmentViewModel()
                {
                    Indicator = x.DFIndicator.NameAr,
                    _Source = x.DFSource.NameAr,
                    _Quarter = x.DFQuarter.NameAr,
                    Unit = x.DFUnit.NameAr,
                    _Year = x.DFYear.NameAr,
                    AccommodationAndFoodServiceActivities = x.AccommodationAndFoodServiceActivities,
                    Agriculture = x.Agriculture,
                    Construction = x.Construction,
                    Education = x.Education,
                    Electricity = x.Electricity,
                    FinancialIntermediaryInsuranceAndSocialSecurity = x.FinancialIntermediaryInsuranceAndSocialSecurity,
                    Health = x.Health,
                    InformationAndCommunication = x.InformationAndCommunication,
                    NaturalGas = x.NaturalGas,
                    OtherExtractions = x.OtherExtractions,
                    OtherManufacturing = x.OtherManufacturing,
                    OtherSrvices = x.OtherSrvices,
                    Petroleum = x.Petroleum,
                    PetroleumRefining = x.PetroleumRefining,
                    RealEstateActivities = x.RealEstateActivities,
                    StorageAndTransportation = x.StorageAndTransportation,
                    SuezCanal = x.SuezCanal,
                    TotalInvestments = x.TotalInvestments,
                    WaterAndSewerage = x.WaterAndSewerage,
                    WholesaleAndRetailTrade = x.WholesaleAndRetailTrade
                }).ToList();

                var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameAr) || x.IsBasic) && x.Type == 3).OrderBy(x => x.order).ToList();
                headers = headersData.Select(x => x.NameAr).ToList();
                headersNames = headersData.Select(x => x.Name).ToList();
            }
            else
            {
                resultView = resultData.Select(x => new InvestmentViewModel()
                {
                    Indicator = x.DFIndicator.NameEn,
                    _Source = x.DFSource.NameEn,
                    _Quarter = x.DFQuarter.NameEn,
                    Unit = x.DFUnit.NameEn,
                    _Year = x.DFYear.NameEn,
                    AccommodationAndFoodServiceActivities = x.AccommodationAndFoodServiceActivities,
                    Agriculture = x.Agriculture,
                    Construction = x.Construction,
                    Education = x.Education,
                    Electricity = x.Electricity,
                    FinancialIntermediaryInsuranceAndSocialSecurity = x.FinancialIntermediaryInsuranceAndSocialSecurity,
                    Health = x.Health,
                    InformationAndCommunication = x.InformationAndCommunication,
                    NaturalGas = x.NaturalGas,
                    OtherExtractions = x.OtherExtractions,
                    OtherManufacturing = x.OtherManufacturing,
                    OtherSrvices = x.OtherSrvices,
                    Petroleum = x.Petroleum,
                    PetroleumRefining = x.PetroleumRefining,
                    RealEstateActivities = x.RealEstateActivities,
                    StorageAndTransportation = x.StorageAndTransportation,
                    SuezCanal = x.SuezCanal,
                    TotalInvestments = x.TotalInvestments,
                    WaterAndSewerage = x.WaterAndSewerage,
                    WholesaleAndRetailTrade = x.WholesaleAndRetailTrade
                }).ToList();

                var headersData = _db.DFGDP.Where(x => (headers.Contains(x.NameEn) || x.IsBasic) && x.Type == 3).OrderBy(x => x.order).ToList();
                headers = headersData.Select(x => x.NameEn).ToList();
                headersNames = headersData.Select(x => x.Name).ToList();
            }
            //get values which equal property name(header Name)
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
        /// <summary>
        /// get Investments data depend on years and quarters
        /// </summary>
        /// <param name="years">years ids</param>
        /// <param name="quarters">quarters ids</param>
        /// <returns></returns>
        private List<Investments> GetReportResult(int[] years, int[] quarters)
        {
            return _db.Investments.Where(x => years.Contains(x.DFYearId) && quarters.Contains(x.DFQuarterId) && !(x.isDeleted ?? false)).Include(x => x.DFIndicator).Include(x => x.DFSource).Include(x => x.DFQuarter).Include(x => x.DFUnit).Include(x => x.DFYear).OrderByDescending(x=>x.DFYear.Order).ThenByDescending(x=>x.DFQuarterId).ToList();
        }

        public IEnumerable<InvestmentViewModel> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount, string role = "")
        {
            var notApproval = string.IsNullOrWhiteSpace(role);

            var queryright = (from pm in _db.Investments.Where(d => !(d.isDeleted ?? false)).DefaultIfEmpty()
                              from pmv in _db.InvestmentVersions.Where(d => d.InvestmentsId == pm.Id && d.VersionStatusEnum != VersionStatusEIEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new InvestmentViewModel
                              {
                                  Investmentid = pm.Id,
                                  IsVersion = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)),
                                  Id = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Id : pm.Id,
                                  Indicator = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFIndicator.NameEn : pm.DFIndicator.NameEn,

                                  _Quarter = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFQuarter.NameEn : pm.DFQuarter.NameEn,

                                  _Source = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFSource.NameEn : pm.DFSource.NameEn,

                                  _Year = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFYear.NameEn : pm.DFYear.NameEn,

                                  Unit = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFUnit.NameEn : pm.DFUnit.NameEn,

                                  OtherSrvices = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.OtherSrvices : pm.OtherSrvices,

                                  OtherManufacturing = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.OtherManufacturing : pm.OtherManufacturing,

                                  OtherExtractions = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.OtherExtractions : pm.OtherExtractions,

                                  AccommodationAndFoodServiceActivities = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.AccommodationAndFoodServiceActivities : pm.AccommodationAndFoodServiceActivities,

                                  Agriculture = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Agriculture : pm.Agriculture,

                                  Construction = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Construction : pm.Construction,

                                  Education = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Education : pm.Education,

                                  Electricity = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Electricity : pm.Electricity,

                                  FinancialIntermediaryInsuranceAndSocialSecurity = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.FinancialIntermediaryInsuranceAndSocialSecurity : pm.FinancialIntermediaryInsuranceAndSocialSecurity,

                                  Health = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Health : pm.Health,

                                  NaturalGas = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.NaturalGas : pm.NaturalGas,

                                  Petroleum = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Petroleum : pm.Petroleum,

                                  PetroleumRefining = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.PetroleumRefining : pm.PetroleumRefining,

                                  RealEstateActivities = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.RealEstateActivities : pm.RealEstateActivities,

                                  StorageAndTransportation = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.StorageAndTransportation : pm.StorageAndTransportation,

                                  SuezCanal = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.SuezCanal : pm.SuezCanal,

                                  TotalInvestments = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.TotalInvestments : pm.TotalInvestments,

                                  WaterAndSewerage = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.WaterAndSewerage : pm.WaterAndSewerage,

                                  WholesaleAndRetailTrade = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.WholesaleAndRetailTrade : pm.WholesaleAndRetailTrade,

                                  InformationAndCommunication = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.InformationAndCommunication : pm.InformationAndCommunication,

                                  IsDeleted = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.isDeleted ?? false : pm.isDeleted ?? false,


                                  VersionStatusEnum = pmv.VersionStatusEnum ?? VersionStatusEIEnum.Approved,
                                  ChangeActionEnum = pmv.ChangeActionEnum ?? ChangeActionEIEnum.New,

                                  CreatedById = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.CreatedById : "",
                              });


            //get the rest from HomePageAffiliatesVersions that wasn't included in previous join 
            var queryleft = (from prv in _db.InvestmentVersions.Where(d =>  d.VersionStatusEnum != VersionStatusEIEnum.Ignored)
                             where !_db.Investments.Any(d => d.Id == prv.InvestmentsId)
                             select new InvestmentViewModel
                             {
                                 Id = prv.Id,
                                 IsVersion = true,
                                 VersionStatusEnum = prv.VersionStatusEnum,
                                 ChangeActionEnum = prv.ChangeActionEnum,
                                 Investmentid = prv.InvestmentsId,
                                 IsDeleted = prv.isDeleted ?? false,
                                 Indicator = prv.DFIndicator.NameEn,
                                 _Quarter = prv.DFQuarter.NameEn,
                                 _Source = prv.DFSource.NameEn,
                                 Unit = prv.DFUnit.NameEn,
                                 _Year = prv.DFYear.NameEn,
                                 WholesaleAndRetailTrade = prv.WholesaleAndRetailTrade,
                                 WaterAndSewerage = prv.WaterAndSewerage,
                                 TotalInvestments = prv.TotalInvestments,
                                 SuezCanal = prv.SuezCanal,
                                 AccommodationAndFoodServiceActivities = prv.AccommodationAndFoodServiceActivities,
                                 Agriculture = prv.Agriculture,
                                 Construction = prv.Construction,
                                 Education = prv.Education,
                                 Electricity = prv.Electricity,
                                 FinancialIntermediaryInsuranceAndSocialSecurity = prv.FinancialIntermediaryInsuranceAndSocialSecurity,
                                 Health = prv.Health,
                                 InformationAndCommunication = prv.InformationAndCommunication,
                                 NaturalGas = prv.NaturalGas,
                                 OtherExtractions = prv.OtherExtractions,
                                 OtherManufacturing = prv.OtherManufacturing,
                                 OtherSrvices = prv.OtherSrvices,
                                 Petroleum = prv.Petroleum,
                                 PetroleumRefining = prv.PetroleumRefining,
                                 RealEstateActivities = prv.RealEstateActivities,
                                 StorageAndTransportation = prv.StorageAndTransportation,
                                 CreatedById=prv.CreatedById

                             });


            IQueryable<InvestmentViewModel> investmentData;
            if (string.IsNullOrWhiteSpace(role))
                investmentData = queryright.Union(queryleft);
            else
                investmentData = queryright.Union(queryleft).Where(x => x.VersionStatusEnum == VersionStatusEIEnum.Submitted);

            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                investmentData = investmentData.Where(x =>
                    x._Quarter.ToLower().Contains(searchValue.ToLower()) ||
                    x._Year.ToLower().Contains(searchValue.ToLower())
                    );
            }
            totalCount = investmentData.Count();
            if(string.IsNullOrWhiteSpace(sortColumnName))
            {
                investmentData = investmentData.OrderByDescending(x => x._Year).ThenBy(x => x._Quarter);
            }
            else if (sortDirection == "asc")
                investmentData = investmentData.OrderBy($"{sortColumnName} asc");
            else if (sortDirection == "desc")
                investmentData = investmentData
                    .OrderBy($"{sortColumnName} descending");

            //paging
            return investmentData.Skip(start).Take(lenght).ToList();
        }

        public void AddVer(InvestmentVersion investments)
        {
            _db.InvestmentVersions.Add(investments);
            _db.SaveChanges();
        }

        public InvestmentVersion GetVerById(int id, bool disableTracking = true)
        {
            return disableTracking ? _db.InvestmentVersions.AsNoTracking().FirstOrDefault(x => x.Id == id) : _db.InvestmentVersions.FirstOrDefault(x => x.Id == id);
        }

        public InvestmentVersion GetByInvetmentId(int id)
        {
            return _db.InvestmentVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.InvestmentsId == id);
        }

        public void UpdateVer(InvestmentVersion invementVersionModel)
        {
            _db.InvestmentVersions.Update(invementVersionModel);
            _db.SaveChanges();
        }

        public IEnumerable<InvestmentVersion> GetAllSubmited()
        {
            return _db.InvestmentVersions.Where(x => x.VersionStatusEnum == VersionStatusEIEnum.Submitted);
        }
    }
}
