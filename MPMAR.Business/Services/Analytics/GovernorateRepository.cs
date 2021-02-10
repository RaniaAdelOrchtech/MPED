using Microsoft.EntityFrameworkCore;
using MPMAR.Analytics.Data;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using MPMAR.Analytics.Data.Enums;
using MPMAR.Analytics.Data.Models;

namespace MPMAR.Business.Services
{
    public class GovernorateRepository : IGovernorateRepository
    {
        private readonly AnalyticsDbContext _db;
        public GovernorateRepository(AnalyticsDbContext db)
        {
            _db = db;
        }

        public Governorate findById(int id)
        {
            var governorates = _db.Governorates.Where(g => g.Id == id).Include(g => g.DFIndicator).Include(g => g.DFYear).Include(g => g.DFGovernorate).FirstOrDefault();

            return governorates;
        }

        public bool SoftDelete(int id)
        {
            try
            {
                Governorate model = _db.Governorates.FirstOrDefault(x => x.Id == id);
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


        public void Update(Governorate governorate)
        {
            _db.Governorates.Attach(governorate);
            _db.Entry(governorate).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Add(Governorate governorate)
        {
            _db.Governorates.Add(governorate);
            _db.SaveChanges();
        }

        public List<GovernoratesViewModel> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount, string role = "")
        {
            var notApproval = string.IsNullOrWhiteSpace(role);

            var queryright = (from pm in _db.Governorates.Where(d => !(d.isDeleted ?? false)).DefaultIfEmpty()
                              from pmv in _db.GovernorateVersions.Where(d => d.GovernorateId == pm.Id && d.VersionStatusEnum != VersionStatusEIEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new GovernoratesViewModel
                              {
                                  GovernorateId = pm.Id,
                                  IsVersion = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)),
                                  Id = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Id : pm.Id,
                                  Indicator = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFIndicator.NameEn : pm.DFIndicator.NameEn,

                                  NonProfitInstitutionsServingHouseholdSector = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.NonProfitInstitutionsServingHouseholdSector : pm.NonProfitInstitutionsServingHouseholdSector,

                                  Sewerage = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Sewerage : pm.Sewerage,

                                  Year = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFYear.NameEn : pm.DFYear.NameEn,

                                  Unit = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Unit : pm.Unit,

                                  AccommodationandFoodServiceActivities = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.AccommodationandFoodServiceActivities : pm.AccommodationandFoodServiceActivities,

                                  BusinessServices = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.BusinessServices : pm.BusinessServices,

                                  OtherExtractions = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.OtherExtractions : pm.OtherExtractions,

                                  Communication = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Communication : pm.Communication,

                                  Agriculture = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Agriculture : pm.Agriculture,

                                  Construction = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Construction : pm.Construction,

                                  Education = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Education : pm.Education,

                                  CrudePetroleumExtraction = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.CrudePetroleumExtraction : pm.CrudePetroleumExtraction,

                                  CustomFees = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.CustomFees : pm.CustomFees,

                                  Health = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Health : pm.Health,

                                  DomesticWorkers = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DomesticWorkers : pm.DomesticWorkers,

                                  ElectricityandGas = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.ElectricityandGas : pm.ElectricityandGas,

                                  FinancialCorporations = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.FinancialCorporations : pm.FinancialCorporations,

                                  GeneralGovernment = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.GeneralGovernment : pm.GeneralGovernment,

                                  Information = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Information : pm.Information,

                                  WholesaleandRetailTrade = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.WholesaleandRetailTrade : pm.WholesaleandRetailTrade,

                                  Water = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Water : pm.Water,

                                  WasteRecycling = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.WasteRecycling : pm.WasteRecycling,

                                  ManufacturingIndustries = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.ManufacturingIndustries : pm.ManufacturingIndustries,

                                  NonFinancialCorporations = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.NonFinancialCorporations : pm.NonFinancialCorporations,

                                  Governorate = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFGovernorate.NameEn : pm.DFGovernorate.NameEn,

                                  OtherServices = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.OtherServices : pm.OtherServices,

                                  PetroleumRefinement = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.PetroleumRefinement : pm.PetroleumRefinement,

                                  RealEstateOwnership = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.RealEstateOwnership : pm.RealEstateOwnership,

                                  TotalGDPEgyptWithCustomFees = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.TotalGDPEgyptWithCustomFees : pm.TotalGDPEgyptWithCustomFees,

                                  TotalGovernorateGDP = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.TotalGovernorateGDP : pm.TotalGovernorateGDP,

                                  TransportationandStorage = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.TransportationandStorage : pm.TransportationandStorage,


                                  IsDeleted = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.isDeleted ?? false : pm.isDeleted ?? false,


                                  VersionStatusEnum = pmv.VersionStatusEnum ?? VersionStatusEIEnum.Approved,
                                  ChangeActionEnum = pmv.ChangeActionEnum ?? ChangeActionEIEnum.New,

                                  CreatedById = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.CreatedById : "",
                              });


            //get the rest from HomePageAffiliatesVersions that wasn't included in previous join 
            var queryleft = (from prv in _db.GovernorateVersions.Where(d => d.VersionStatusEnum != VersionStatusEIEnum.Ignored)
                             where !_db.Governorates.Any(d => d.Id == prv.GovernorateId)
                             select new GovernoratesViewModel
                             {
                                 Id = prv.Id,
                                 IsVersion = true,
                                 VersionStatusEnum = prv.VersionStatusEnum,
                                 ChangeActionEnum = prv.ChangeActionEnum,
                                 GovernorateId = prv.GovernorateId,
                                 IsDeleted = prv.isDeleted ?? false,
                                 Indicator = prv.DFIndicator.NameEn,
                                 Sewerage = prv.Sewerage,
                                 RealEstateOwnership = prv.RealEstateOwnership,
                                 Unit = prv.Unit,
                                 Year = prv.DFYear.NameEn,
                                 WholesaleandRetailTrade = prv.WholesaleandRetailTrade,
                                 Water = prv.Water,
                                 WasteRecycling = prv.WasteRecycling,
                                 TransportationandStorage = prv.TransportationandStorage,
                                 TotalGovernorateGDP = prv.TotalGovernorateGDP,
                                 Agriculture = prv.Agriculture,
                                 Construction = prv.Construction,
                                 Education = prv.Education,
                                 TotalGDPEgyptWithCustomFees = prv.TotalGDPEgyptWithCustomFees,
                                 AccommodationandFoodServiceActivities = prv.AccommodationandFoodServiceActivities,
                                 Health = prv.Health,
                                 BusinessServices = prv.BusinessServices,
                                 Communication = prv.Communication,
                                 OtherExtractions = prv.OtherExtractions,
                                 CrudePetroleumExtraction = prv.CrudePetroleumExtraction,
                                 CustomFees = prv.CustomFees,
                                 DomesticWorkers = prv.DomesticWorkers,
                                 ElectricityandGas = prv.ElectricityandGas,
                                 FinancialCorporations = prv.FinancialCorporations,
                                 GeneralGovernment = prv.GeneralGovernment,
                                 Governorate = prv.DFGovernorate.NameEn,
                                 Information = prv.Information,
                                 PetroleumRefinement = prv.PetroleumRefinement,
                                 OtherServices = prv.OtherServices,
                                 NonProfitInstitutionsServingHouseholdSector = prv.NonProfitInstitutionsServingHouseholdSector,
                                 ManufacturingIndustries = prv.ManufacturingIndustries,
                                 NonFinancialCorporations = prv.NonFinancialCorporations,
                                 CreatedById = prv.CreatedById
                             });


            IQueryable<GovernoratesViewModel> governorates;
            if (string.IsNullOrWhiteSpace(role))
                governorates = queryright.Union(queryleft);
            else
                governorates = queryright.Union(queryleft).Where(x => x.VersionStatusEnum == VersionStatusEIEnum.Submitted);

            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                governorates = governorates.Where(x =>
                    x.Governorate.ToLower().Contains(searchValue.ToLower()) ||
                    x.Year.ToLower().Contains(searchValue.ToLower())
                    );
            }
            totalCount = governorates.Count();
            if (sortDirection == "asc")
                governorates = governorates.OrderBy($"{sortColumnName} asc");
            else if (sortDirection == "desc")
                governorates = governorates
                    .OrderBy($"{sortColumnName} descending");

            //paging
            return governorates.Skip(start).Take(lenght).ToList();
        }

        public GovernorateViewModel GetFilterdGovernoratesForGrid(int[] regionsandgov, int[] years, int[] activities, string lang)
        {
            GovernorateViewModel dataTable = GetGridAsDataTable(regionsandgov, years, activities, lang);
            return dataTable;
        }

        public LineGovModelParent GetFilterForLineChart(int[] regions, int[] governorates, int[] years, int[] activities, string lang)
        {
            List<LineGovModel> result = new List<LineGovModel>();
            LineGovModelParent lastResult = new LineGovModelParent();
            List<string> lineYears = new List<string>();
            List<Governorate> query;
            if (activities.Length == 0)
            {
                if (regions.Length != 0)
                {
                    //get all Governorate filterd by regions and years
                    query = _db.Governorates.Include(x => x.DFIndicator).Include(x => x.DFYear).Include(x => x.DFGovernorate.DFRegion).Include(x => x.DFGovernorate)
                       .Where(x => !(x.isDeleted ?? false) &&
                   (regions.Length == 0 || regions.Contains(x.DFGovernorate.DFRegion.Id)) &&
                   (years.Length == 0 || years.Contains(x.DFYear.Id)) &&
                   (x.DFGovernorate.Name.StartsWith("Total"))
                   ).OrderBy(x => x.DFYear.Order).ToList();
                    //group by region name and get there years
                    if (lang == null || lang.ToLower() == "ar")
                    {
                        result = query.GroupBy(item => new { gov = item.DFGovernorate.DFRegion.NameAr },
                         (key, group) =>
                         {
                             lineYears.AddRange(group.Select(x => x.DFYear.Name).ToList());
                             return new LineGovModel(key.gov, group.Select(x => x.TotalGovernorateGDP != null ? (object)Math.Round((decimal)x.TotalGovernorateGDP, 2) : null).ToList());
                         }).ToList();
                    }
                    else
                    {
                        result = query.GroupBy(item => new { gov = item.DFGovernorate.DFRegion.NameEn },
                         (key, group) =>
                         {
                             lineYears.AddRange(group.Select(x => x.DFYear.Name).ToList());
                             return new LineGovModel(key.gov, group.Select(x => x.TotalGovernorateGDP != null ? (object)Math.Round((decimal)x.TotalGovernorateGDP, 2) : null).ToList());
                         }).ToList();
                    }
                }
                else
                {
                    //get all Governorate filterd by governorates and years
                    query = _db.Governorates.OrderBy(x => x.DFYear.Order).Include(x => x.DFIndicator).Include(x => x.DFYear).Include(x => x.DFGovernorate).Include(x => x.DFGovernorate.DFRegion).Where(x => !(x.isDeleted ?? false) &&
                   ((governorates.Length == 0 || governorates.Contains(x.DFGovernorate.Id)) &&
                   ((years.Length == 0 || years.Contains(x.DFYearId))
                   ))).ToList();
                    //group by region name and get there years
                    if (lang == null || lang.ToLower() == "ar")
                    {
                        result = query.GroupBy(item => new { gov = item.DFGovernorate.NameAr },
                        (key, group) =>
                        {
                            lineYears.AddRange(group.Select(x => x.DFYear.NameAr).ToList());
                            return new LineGovModel(key.gov, group.Select(x => x.TotalGovernorateGDP != null ? (object)Math.Round((decimal)x.TotalGovernorateGDP, 2) : null).ToList());
                        }).ToList();
                    }
                    else
                    {
                        result = query.GroupBy(item => new { gov = item.DFGovernorate.NameEn },
                        (key, group) =>
                        {
                            lineYears.AddRange(group.Select(x => x.DFYear.NameEn).ToList());
                            return new LineGovModel(key.gov, group.Select(x => x.TotalGovernorateGDP != null ? (object)Math.Round((decimal)x.TotalGovernorateGDP, 2) : null).ToList());
                        }).ToList();
                    }
                }

                lineYears = lineYears.Distinct().ToList();

                lastResult.Data = result;
                lastResult.Years = lineYears;
            }
            else
            {
                var sectors = _db.DFSectors.Where(x => activities.Contains(x.Id));
                if (regions.Length != 0)
                {
                    query = _db.Governorates.Include(x => x.DFYear).Include(x => x.DFIndicator).Include(x => x.DFGovernorate).Include(x => x.DFGovernorate.DFRegion)
                    .Where(x => !(x.isDeleted ?? false) && (x.DFGovernorate.isTotal ?? false) && regions.Contains(x.DFGovernorate.DFRegionId ?? 0) && years.Contains(x.DFYear.Id)).ToList();
                }
                else
                {

                    query = _db.Governorates.Include(x => x.DFYear).Include(x => x.DFIndicator).Include(x => x.DFGovernorate).Include(x => x.DFGovernorate.DFRegion)
                    .Where(x => !(x.isDeleted ?? false) && governorates.Contains(x.DFGovernorateId) && years.Contains(x.DFYear.Id)).ToList();
                }
                lineYears = query.Select(x => lang == "en" ? $"{x.DFYear.NameEn} {x.DFGovernorate.NameEn}" : $"{x.DFYear.NameAr} {x.DFGovernorate.NameAr}").ToList();

                foreach (var sector in sectors)
                {
                    var vieModelData = new LineGovModel();
                    vieModelData.data = new List<object>();
                    vieModelData.name = lang == "en" ? sector.NameEn : sector.NameAr;
                    foreach (var item in query)
                    {
                        var val = new object();
                        val = item.GetType().GetProperty(sector.Name).GetValue(item, null);
                        var numFlag = double.TryParse("" + val, out double num);
                        num = Math.Round(num, 2);
                        vieModelData.data.Add(numFlag ? Math.Round(num, 2) : (double?)null);
                    }
                    result.Add(vieModelData);
                }

                lastResult.Data = result;
                lastResult.Years = lineYears;
            }

            return lastResult;
        }
        public BarGovModelParent GetFilterForBarChart(int[] regions, int[] governorates, int[] years, int[] activities, string lang)
        {
            List<int> allGovernorates = new List<int>();
            List<Governorate> query = new List<Governorate>();
            BarGovModelParent lastResult = new BarGovModelParent();
            List<LatestGovModel> result = new List<LatestGovModel>();

            List<string> barYears = new List<string>();
            if (activities.Length == 0)
            {
                if (regions.Length != 0)
                {
                    //get all Governorate filterd by regions and years
                    allGovernorates = _db.Governorates.Include(x => x.DFGovernorate).Include(x => x.DFIndicator).Include(x => x.DFYear)
                   .Where(g => !(g.isDeleted ?? false) && regions.Contains(g.DFGovernorate.DFRegion.Id)).OrderBy(x => x.DFYear.Order).Select(x => x.DFGovernorate.DFRegion.Id).Distinct().ToList();

                    query = _db.Governorates.OrderBy(x => x.DFYear.Order).Include(x => x.DFGovernorate).Include(x => x.DFIndicator).Include(x => x.DFYear).Where(x => !(x.isDeleted ?? false) &&
                    (allGovernorates.Count == 0 || allGovernorates.Contains(x.DFGovernorate.DFRegion.Id)) &&
                    (years.Length == 0 || years.Contains(x.DFYear.Id))
                   ).ToList();
                }
                else if (governorates.Length != 0)
                {
                    //get all Governorate filterd by governorates and years
                    var allRegion = _db.Governorates.OrderBy(x => x.DFYear.Order).Include(x => x.DFGovernorate).Include(x => x.DFIndicator).Include(x => x.DFYear)
                        .Where(g => !(g.isDeleted ?? false) && governorates.Contains(g.DFGovernorate.Id)).Select(x => x.DFGovernorate.Id).Distinct().ToList();

                    allGovernorates.AddRange(allRegion);
                    query = _db.Governorates.OrderBy(x => x.DFYear.Order).Include(x => x.DFGovernorate).Include(x => x.DFIndicator).Include(x => x.DFYear).Where(x => !(x.isDeleted ?? false) &&
                (allGovernorates.Count == 0 || allGovernorates.Contains(x.DFGovernorate.Id)) &&
                 (years.Length == 0 || years.Contains(x.DFYear.Id))
                 ).ToList();
                }
                else
                {
                    query = _db.Governorates.OrderBy(x => x.DFYear.Order).Include(x => x.DFGovernorate).Include(x => x.DFIndicator).Include(x => x.DFYear).Where(x => !(x.isDeleted ?? false) &&
                    (allGovernorates.Count == 0 || allGovernorates.Contains(x.DFGovernorate.Id)) &&
                     (allGovernorates.Count == 0 || allGovernorates.Contains(x.DFGovernorate.DFRegion.Id)) &&
                    (years.Length == 0 || years.Contains(x.DFYear.Id))
                    ).ToList();
                }


                //group by Governorate name and get there years
                if (lang == null || lang.ToLower() == "ar")
                {
                    result = query.GroupBy(item => new { gov = item.DFGovernorate.NameAr },
                    (key, group) =>
                    {
                        barYears.AddRange(group.Select(x => x.DFYear.NameAr).ToList());
                        return new LatestGovModel(key.gov, group.Select(x => x.TotalGovernorateGDP != null ? (object)Math.Round((decimal)x.TotalGovernorateGDP, 2) : 0).ToList());
                    }).ToList();
                }
                else
                {
                    result = query.GroupBy(item => new { gov = item.DFGovernorate.NameEn },
                    (key, group) =>
                    {
                        barYears.AddRange(group.Select(x => x.DFYear.NameEn).ToList());
                        return new LatestGovModel(key.gov, group.Select(x => x.TotalGovernorateGDP != null ? (object)Math.Round((decimal)x.TotalGovernorateGDP, 2) : 0).ToList());
                    }).ToList();
                }
                barYears = barYears.Distinct().ToList();


                lastResult.Data = result;
                lastResult.Years = barYears;
            }
            else
            {
                var sectors = _db.DFSectors.Where(x => activities.Contains(x.Id));
                if (regions.Length != 0)
                {
                    query = _db.Governorates.Include(x => x.DFYear).Include(x => x.DFIndicator).Include(x => x.DFGovernorate).Include(x => x.DFGovernorate.DFRegion)
                    .Where(x => !(x.isDeleted ?? false) && (x.DFGovernorate.isTotal ?? false) && regions.Contains(x.DFGovernorate.DFRegionId ?? 0) && years.Contains(x.DFYear.Id)).ToList();
                }
                else
                {

                    query = _db.Governorates.Include(x => x.DFYear).Include(x => x.DFIndicator).Include(x => x.DFGovernorate).Include(x => x.DFGovernorate.DFRegion)
                    .Where(x => !(x.isDeleted ?? false) && governorates.Contains(x.DFGovernorateId) && years.Contains(x.DFYear.Id)).ToList();
                }
                barYears = query.Select(x => lang == "en" ? $"{x.DFYear.NameEn} {x.DFGovernorate.NameEn}" : $"{x.DFYear.NameAr} {x.DFGovernorate.NameAr}").ToList();

                foreach (var sector in sectors)
                {
                    var vieModelData = new LatestGovModel();
                    vieModelData.data = new List<object>();
                    vieModelData.name = lang == "en" ? sector.NameEn : sector.NameAr;
                    foreach (var item in query)
                    {
                        var val = new object();
                        val = item.GetType().GetProperty(sector.Name).GetValue(item, null);
                        var numFlag = double.TryParse("" + val, out double num);
                        num = Math.Round(num, 2);
                        vieModelData.data.Add(numFlag ? Math.Round(num, 2) : (double?)0);
                    }
                    result.Add(vieModelData);
                }

                lastResult.Data = result;
                lastResult.Years = barYears;
            }
            return lastResult;
        }
        public List<GovPieModel> GetFilterForPieChart(int[] regions, int[] governorates, int year, int[] activities, string lang)
        {
            List<GovPieModel> result = new List<GovPieModel>();
            if (activities.Length == 0)
            {
                if (regions.Length != 0)
                {
                    //get all Governorate filterd by regions and years
                    var query = _db.Governorates.Include(x => x.DFYear).Include(x => x.DFIndicator).Include(x => x.DFGovernorate).Include(x => x.DFGovernorate.DFRegion)
                        .Where(x => !(x.isDeleted ?? false) && regions.Contains(x.DFGovernorate.DFRegion.Id) && year.Equals(x.DFYear.Id)).ToList().GroupBy(x => x.DFGovernorate.DFRegionId, (key, group) => group.Where(x => !(x.isDeleted ?? false) && x.DFGovernorateId == group.Max(y => y.DFGovernorateId)).FirstOrDefault()).ToList();
                    //group by Governorate name and get there years
                    if (lang == null || lang.ToLower() == "ar")
                    {

                        result = query.Select(x => new GovPieModel(x.DFGovernorate.NameAr, (decimal)(x.TotalGovernorateGDP ?? 0))).ToList();
                        //if only one selected compare it with the others
                        if (result.Count == 1)
                        {

                            var query2 = _db.Governorates.Include(x => x.DFYear).Include(x => x.DFIndicator).Include(x => x.DFGovernorate).Include(x => x.DFGovernorate.DFRegion)
                       .Where(x => !(x.isDeleted ?? false) && !regions.Contains(x.DFGovernorate.DFRegion.Id) && year.Equals(x.DFYear.Id)).ToList().GroupBy(x => x.DFGovernorate.DFRegionId, (key, group) => group.Where(x => !(x.isDeleted ?? false) && x.DFGovernorateId == group.Max(y => y.DFGovernorateId)).FirstOrDefault()).ToList();

                            var result2 = query2.Select(x => new GovPieModel(x.DFGovernorate.NameAr, (decimal)(x.TotalGovernorateGDP ?? 0))).ToList();

                            decimal othersValue = 0;
                            foreach (var q in result2)
                            {
                                othersValue += q.Y;
                            }
                            var numFlag = double.TryParse("" + othersValue, out double num);
                            num = Math.Round(num, 2);

                            result.Add(new GovPieModel("الباقي", numFlag ? (decimal)num : 0));
                        }
                    }
                    else
                    {
                        result = query.Select(x => new GovPieModel(x.DFGovernorate.NameEn, (decimal)(x.TotalGovernorateGDP ?? 0))).ToList();
                        //if only one selected compare it with the others
                        if (result.Count == 1)
                        {

                            var query2 = _db.Governorates.Include(x => x.DFYear).Include(x => x.DFIndicator).Include(x => x.DFGovernorate).Include(x => x.DFGovernorate.DFRegion)
                       .Where(x => !regions.Contains(x.DFGovernorate.DFRegion.Id) && year.Equals(x.DFYear.Id)).ToList().GroupBy(x => x.DFGovernorate.DFRegionId, (key, group) => group.Where(x => !(x.isDeleted ?? false) && x.DFGovernorateId == group.Max(y => y.DFGovernorateId)).FirstOrDefault()).ToList();

                            var result2 = query2.Select(x => new GovPieModel(x.DFGovernorate.NameEn, (decimal)(x.TotalGovernorateGDP ?? 0))).ToList();

                            decimal othersValue = 0;
                            foreach (var q in result2)
                            {
                                othersValue += q.Y;
                            }
                            var numFlag = double.TryParse("" + othersValue, out double num);
                            num = Math.Round(num, 2);

                            result.Add(new GovPieModel("Others", numFlag ? (decimal)num : 0));


                        }
                    }
                }
                else
                {
                    //get all Governorate filterd by governorates and years
                    var query = _db.Governorates.Include(x => x.DFYear).Include(x => x.DFIndicator).Include(x => x.DFGovernorate).Where(x => !(x.isDeleted ?? false) &&
                      ((governorates.Length == 0 || governorates.Contains(x.DFGovernorate.Id)) &&
                      ((year == 0 || year.Equals(x.DFYear.Id))
                      ))).ToList();
                    //group by Governorate name and get there years
                    if (lang == null || lang.ToLower() == "ar")
                    {
                        result = query.GroupBy(item => new { govRegion = item.DFGovernorate.NameAr },
                        (key, group) => new GovPieModel(key.govRegion, (decimal)group.Select(x => x.TotalGovernorateGDP).FirstOrDefault().Value)).ToList();
                        //if only one selected compare it with the others
                        if (result.Count == 1)
                        {

                            var query2 = _db.Governorates.Include(x => x.DFYear).Include(x => x.DFIndicator).Include(x => x.DFGovernorate).Include(x => x.DFGovernorate.DFRegion)
                     .Where(x => !(x.isDeleted ?? false) &&
                 (!governorates.Contains(x.DFGovernorate.Id)) && year.Equals(x.DFYear.Id)).ToList();

                            var result2 = query2.GroupBy(item => new { govRegion = item.DFGovernorate.NameAr },
                        (key, group) => new GovPieModel(key.govRegion, (decimal)group.Select(x => x.TotalGovernorateGDP).FirstOrDefault().Value)).ToList();

                            decimal othersValue = 0;
                            foreach (var q in result2)
                            {
                                othersValue += q.Y;
                            }
                            var numFlag = double.TryParse("" + othersValue, out double num);
                            num = Math.Round(num, 2);

                            result.Add(new GovPieModel("الباقي", numFlag ? (decimal)num : 0));
                        }
                    }
                    else
                    {
                        result = query.GroupBy(item => new { govRegion = item.DFGovernorate.NameEn },
                        (key, group) => new GovPieModel(key.govRegion, (decimal)group.Select(x => x.TotalGovernorateGDP).FirstOrDefault().Value)).ToList();
                        //if only one selected compare it with the others
                        if (result.Count == 1)
                        {
                            var query2 = _db.Governorates.Include(x => x.DFYear).Include(x => x.DFIndicator).Include(x => x.DFGovernorate).Include(x => x.DFGovernorate.DFRegion)
                    .Where(x => !(x.isDeleted ?? false) &&
                (!governorates.Contains(x.DFGovernorate.Id)) && year.Equals(x.DFYear.Id)).ToList();

                            var result2 = query2.GroupBy(item => new { govRegion = item.DFGovernorate.NameEn },
                        (key, group) => new GovPieModel(key.govRegion, (decimal)group.Select(x => x.TotalGovernorateGDP).FirstOrDefault().Value)).ToList();


                            decimal othersValue = 0;
                            foreach (var q in result2)
                            {
                                othersValue += q.Y;
                            }
                            var numFlag = double.TryParse("" + othersValue, out double num);
                            num = Math.Round(num, 2);

                            result.Add(new GovPieModel("Others", numFlag ? (decimal)num : 0));

                        }
                    }
                }
            }
            else
            {
                Governorate query;
                var sectors = _db.DFSectors.Where(x => activities.Contains(x.Id)).ToList();
                if (regions.Length != 0)
                {
                    var regionId = regions.FirstOrDefault();
                    query = _db.Governorates.Include(x => x.DFYear).Include(x => x.DFIndicator).Include(x => x.DFGovernorate).Include(x => x.DFGovernorate.DFRegion)
                   .Where(x => !(x.isDeleted ?? false) && (x.DFGovernorate.isTotal ?? false) && x.DFGovernorate.DFRegionId == regionId && x.DFYear.Id == year).FirstOrDefault();


                }
                else
                {
                    var govId = governorates.FirstOrDefault();
                    query = _db.Governorates.Include(x => x.DFYear).Include(x => x.DFIndicator).Include(x => x.DFGovernorate).Include(x => x.DFGovernorate.DFRegion)
                .Where(x => !(x.isDeleted ?? false) && x.DFGovernorateId == govId && x.DFYear.Id == year).FirstOrDefault();

                }

                foreach (var sector in sectors)
                {
                    var val = new object();
                    val = query.GetType().GetProperty(sector.Name).GetValue(query, null);
                    var numFlag = double.TryParse("" + val, out double num);
                    num = Math.Round(num, 2);

                    result.Add(new GovPieModel(lang == "en" ? sector.NameEn : sector.NameAr, numFlag ? (decimal)num > 0 ? (decimal)num : 0 : 0));
                }
                if (sectors.Count == 1)
                {
                    var changeSectors = _db.DFSectors.Where(x => !activities.Contains(x.Id)).ToList();
                    decimal changeSectorsSum = 0;
                    foreach (var sector in changeSectors)
                    {
                        var val = new object();
                        var type = query.GetType().GetProperty(sector.Name);
                        if (type != null)
                        {
                            val = type.GetValue(query, null);
                            var numFlag = double.TryParse("" + val, out double num);
                            num = Math.Round(num, 2);
                            changeSectorsSum += numFlag ? (decimal)num : 0;
                        }
                    }

                    result.Add(new GovPieModel(lang == "en" ? "Others" : "الباقي", changeSectorsSum > 0 ? changeSectorsSum : 0));
                }

            }
            return result;
        }

        private GovernorateViewModel GetGridAsDataTable(int[] regionsandgov, int[] years, int[] activities, string lang)
        {
            List<List<object>> dataRows = new List<List<object>>();
            DataTable dataTable = new DataTable();

            //default Columns
            dataTable.Columns.Add("DFIndicatorId");
            dataTable.Columns.Add("Unit");
            dataTable.Columns.Add("DFGovernorateId");
            dataTable.Columns.Add("DFRegions");
            dataTable.Columns.Add("DFYearId");

            List<string> headers = new List<string>();
            //default headers
            headers.Add("DFIndicatorId");
            headers.Add("Unit");
            headers.Add("DFGovernorateId");
            headers.Add("DFRegions");
            headers.Add("DFYearId");

            //add Columns and headers sent by activities ids
            _db.DFSectors.OrderBy(x => x.Order ?? 0).Where(x => activities.Contains(x.Id)).ToList().ForEach(item =>
            {
                dataTable.Columns.Add(item.Name);
                headers.Add(item.Name);
            });

            //get Governorates filterd by regionsandgov and years
            var query = _db.Governorates.Include(x => x.DFGovernorate.DFRegion).Include(x => x.DFGovernorate).Include(x => x.DFYear).Include(x => x.DFIndicator)
            .Where(x => !(x.isDeleted ?? false) &&
            (years.Length == 0 || years.Contains(x.DFYear.Id)) &&
            (regionsandgov.Length == 0 || regionsandgov.Contains(x.DFGovernorate.Id))
            ).OrderByDescending(x => x.DFYearId).ToList();

            if (lang == null || lang.ToLower() == "ar")
            {
                query.ForEach(x =>
                {
                    List<object> row = new List<object>();
                    //put values to corresponding headers 
                    foreach (var item in headers)
                    {
                        switch (item)
                        {
                            case "DFIndicatorId":
                                row.Add(x.DFIndicator.GetType().GetProperty("NameAr").GetValue(x.DFIndicator, null).ToString());
                                break;
                            //todo:unit forigen key
                            case "Unit":
                                row.Add("الف جنيه");
                                break;

                            case "DFGovernorateId":
                                row.Add(x.DFGovernorate.GetType().GetProperty("NameAr").GetValue(x.DFGovernorate, null).ToString());
                                break;

                            case "DFRegions":
                                row.Add(x.DFGovernorate.DFRegion.GetType().GetProperty("NameAr").GetValue(x.DFGovernorate.DFRegion, null).ToString());
                                break;

                            case "DFYearId":
                                row.Add(x.DFYear.GetType().GetProperty("NameAr").GetValue(x.DFYear, null).ToString());
                                break;


                            default:
                                var obj = x.GetType().GetProperty(item).GetValue(x, null);
                                //row.Add(obj != null ? obj.ToString() : "N/A");
                                double num;
                                var numFlag = double.TryParse("" + obj, out num);
                                if (numFlag)
                                {
                                    num = Math.Round(num, 2);
                                    row.Add(num);
                                }
                                else
                                {
                                    row.Add(obj != null ? obj : "N/A");
                                }
                                break;
                        }
                    }

                    dataRows.Add(row);
                });
            }
            else
            {
                query.ForEach(x =>
                {
                    List<object> row = new List<object>();
                    foreach (var item in headers)
                    {
                        switch (item)
                        {
                            case "DFIndicatorId":
                                row.Add(x.DFIndicator.GetType().GetProperty("NameEn").GetValue(x.DFIndicator, null).ToString());
                                break;

                            case "Unit":
                                row.Add(x.GetType().GetProperty("Unit").GetValue(x, null).ToString());
                                break;

                            case "DFGovernorateId":
                                row.Add(x.DFGovernorate.GetType().GetProperty("NameEn").GetValue(x.DFGovernorate, null).ToString());
                                break;

                            case "DFRegions":
                                row.Add(x.DFGovernorate.DFRegion.GetType().GetProperty("NameEn").GetValue(x.DFGovernorate.DFRegion, null).ToString());
                                break;

                            case "DFYearId":
                                row.Add(x.DFYear.GetType().GetProperty("NameEn").GetValue(x.DFYear, null).ToString());
                                break;


                            default:
                                var obj = x.GetType().GetProperty(item).GetValue(x, null);
                                double num;
                                var numFlag = double.TryParse("" + obj, out num);
                                if (numFlag)
                                {
                                    num = Math.Round(num, 2);
                                    row.Add(num);
                                }
                                else
                                {
                                    row.Add(obj != null ? obj : "N/A");
                                }
                                break;
                        }
                    }

                    dataRows.Add(row);
                });
            }
            //get localize columns names
            headers = LocalizeColumnNames(headers, lang);

            GovernorateViewModel tableResult = new GovernorateViewModel(headers, dataRows);

            return tableResult;
        }
        /// <summary>
        /// get localize columns names
        /// </summary>
        /// <param name="headers">headers names</param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public List<string> LocalizeColumnNames(List<string> headers, string lang)
        {
            List<LocalizedColumnName> localizedNames = _db.LocalizedColumnNames.ToList();

            for (int i = 0; i < headers.Count; i++)
            {
                if (lang == null || lang.ToLower() == "ar")
                {
                    string localName = localizedNames.FirstOrDefault(x => x.Key == headers[i]).NameAr;
                    headers[i] = localName;
                }
                else
                {
                    string localName = localizedNames.FirstOrDefault(x => x.Key == headers[i]).NameEn;
                    headers[i] = localName;
                }
            }

            return headers;
        }
        public int GetCount()
        {
            return _db.Governorates.Count();
        }
        public IEnumerable<Governorate> GetByCondition(Expression<Func<Governorate, bool>> expression)
        {
            return _db.Governorates.Where(expression).ToList();
        }

        public void AddVer(GovernorateVersion governorate)
        {
            _db.GovernorateVersions.Add(governorate);
            _db.SaveChanges();
        }

        public GovernorateVersion GetVerById(int id, bool disableTracking = true)
        {
            return disableTracking ? _db.GovernorateVersions.AsNoTracking().Include(g => g.DFIndicator).FirstOrDefault(x => x.Id == id) : _db.GovernorateVersions.FirstOrDefault(x => x.Id == id);
        }

        public GovernorateVersion GetByGovId(int id)
        {
            return _db.GovernorateVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.GovernorateId == id);
        }

        public void UpdateVer(GovernorateVersion govVersionModel)
        {
            _db.GovernorateVersions.Update(govVersionModel);
            _db.SaveChanges();
        }

        public IEnumerable<GovernorateVersion> GetAllSubmited()
        {
            return _db.GovernorateVersions.Where(x => x.VersionStatusEnum == VersionStatusEIEnum.Submitted);
        }
    }
}
