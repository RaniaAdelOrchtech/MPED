using Microsoft.EntityFrameworkCore;
using MPMAR.Analytics.Data;
using MPMAR.Business.Interfaces.Analytics;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using MPMAR.Analytics.Data.Enums;
using MPMAR.Analytics.Data.Models;

namespace MPMAR.Business.Services.Analytics
{
    public class ActivityCurrentRepository : IActivityCurrentRepository
    {
        private readonly AnalyticsDbContext _db;
        public ActivityCurrentRepository(AnalyticsDbContext db)
        {
            _db = db;
        }

        public void Add(ActivityCurrent activityCurrent)
        {
            _db.ActivityCurrents.Add(activityCurrent);
            _db.SaveChanges();
        }

        public IEnumerable<ActivityVM> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount, string role = "")
        {

            var notApproval = string.IsNullOrWhiteSpace(role);

            var queryright = (from pm in _db.ActivityCurrents.Where(d => !(d.IsDeleted ?? false)).DefaultIfEmpty()
                              from pmv in _db.ActivityCurrentVersions.Where(d => d.ActivityCurrentId == pm.Id && d.VersionStatusEnum != VersionStatusEIEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new ActivityVM
                              {
                                  ActivityCurrentId = pm.Id,
                                  IsVersion = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)),
                                  Id = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Id : pm.Id,
                                  DFIndicator = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFIndicator.NameEn : pm.DFIndicator.NameEn,
                                  DFQuarter = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFQuarter.NameEn : pm.DFQuarter.NameEn,

                                  DFSource = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFSource.NameEn : pm.DFSource.NameEn,

                                  DFYear = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFYear.NameEn : pm.DFYear.NameEn,

                                  DFUnit = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFUnit.NameEn : pm.DFUnit.NameEn,

                                  AccommodationAndFoodServiceActivities = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.AccommodationAndFoodServiceActivities : pm.AccommodationAndFoodServiceActivities,

                                  AgricultureForestryFishing = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.AgricultureForestryFishing : pm.AgricultureForestryFishing,

                                  BusinessServices = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.BusinessServices : pm.BusinessServices,

                                  Communication = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Communication : pm.Communication,

                                  Construction = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Construction : pm.Construction,

                                  Education = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Education : pm.Education,

                                  Electricity = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Electricity : pm.Electricity,

                                  FinancialIntermediariesAuxiliaryServices = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.FinancialIntermediariesAuxiliaryServices : pm.FinancialIntermediariesAuxiliaryServices,

                                  Gas = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Gas : pm.Gas,

                                  GeneralGovernment = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.GeneralGovernment : pm.GeneralGovernment,

                                  Health = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Health : pm.Health,

                                  WholesaleAndRetailTrade = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.WholesaleAndRetailTrade : pm.WholesaleAndRetailTrade,


                                  WaterSewerageRemediationActivitie = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.WaterSewerageRemediationActivitie : pm.WaterSewerageRemediationActivitie,

                                  TransportationAndStorage = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.TransportationAndStorage : pm.TransportationAndStorage,

                                  TotalGDPAtFactorCost = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.TotalGDPAtFactorCost : pm.TotalGDPAtFactorCost,

                                  Information = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Information : pm.Information,

                                  MiningQuarrying = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.MiningQuarrying : pm.MiningQuarrying,

                                  ManufacturingIndustries = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.ManufacturingIndustries : pm.ManufacturingIndustries,

                                  OtherExtraction = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.OtherExtraction : pm.OtherExtraction,


                                  OtherManufacturing = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.OtherManufacturing : pm.OtherManufacturing,

                                  OtherServices = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.OtherServices : pm.OtherServices,

                                  Petroleum = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Petroleum : pm.Petroleum,

                                  SuezcCanal = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.SuezcCanal : pm.SuezcCanal,

                                  SocialServices = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.SocialServices : pm.SocialServices,

                                  SocialSecurityAndInsurance = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.SocialSecurityAndInsurance : pm.SocialSecurityAndInsurance,

                                  RealEstateOwnership = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.RealEstateOwnership : pm.RealEstateOwnership,

                                  RealEstateActivitie = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.RealEstateActivitie : pm.RealEstateActivitie,

                                  petroleumRefining = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.petroleumRefining : pm.petroleumRefining,

                                  DFSector = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFSector.NameEn : pm.DFSector.NameEn,

                                  IsDeleted = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.IsDeleted ?? false : pm.IsDeleted ?? false,


                                  VersionStatusEnum = pmv.VersionStatusEnum ?? VersionStatusEIEnum.Approved,
                                  ChangeActionEnum = pmv.ChangeActionEnum ?? ChangeActionEIEnum.New,

                                  CreatedById = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.CreatedById : "",

                              });


            //get the rest from HomePageAffiliatesVersions that wasn't included in previous join 
            var queryleft = (from prv in _db.ActivityCurrentVersions.Where(d => d.VersionStatusEnum != VersionStatusEIEnum.Ignored)
                             where !_db.ActivityCurrents.Any(d => d.Id == prv.ActivityCurrentId)
                             select new ActivityVM
                             {
                                 Id = prv.Id,
                                 IsVersion = true,
                                 VersionStatusEnum = prv.VersionStatusEnum,
                                 ChangeActionEnum = prv.ChangeActionEnum,
                                 AccommodationAndFoodServiceActivities = prv.AccommodationAndFoodServiceActivities,
                                 ActivityCurrentId = prv.ActivityCurrentId,
                                 DFYear = prv.DFYear.NameEn,
                                 WholesaleAndRetailTrade = prv.WholesaleAndRetailTrade,
                                 WaterSewerageRemediationActivitie = prv.WaterSewerageRemediationActivitie,
                                 TransportationAndStorage = prv.TransportationAndStorage,
                                 TotalGDPAtFactorCost = prv.TotalGDPAtFactorCost,
                                 SuezcCanal = prv.SuezcCanal,
                                 SocialServices = prv.SocialServices,
                                 SocialSecurityAndInsurance = prv.SocialSecurityAndInsurance,
                                 RealEstateOwnership = prv.RealEstateOwnership,
                                 RealEstateActivitie = prv.RealEstateActivitie,
                                 petroleumRefining = prv.petroleumRefining,
                                 AgricultureForestryFishing = prv.AgricultureForestryFishing,
                                 BusinessServices = prv.BusinessServices,
                                 Communication = prv.Communication,
                                 Construction = prv.Construction,
                                 DFIndicator = prv.DFIndicator.NameEn,
                                 DFQuarter = prv.DFQuarter.NameEn,
                                 DFSector = prv.DFSector.NameEn,
                                 Petroleum = prv.Petroleum,
                                 OtherServices = prv.OtherServices,
                                 OtherManufacturing = prv.OtherManufacturing,
                                 OtherExtraction = prv.OtherExtraction,
                                 MiningQuarrying = prv.MiningQuarrying,
                                 ManufacturingIndustries = prv.ManufacturingIndustries,
                                 IsDeleted = prv.IsDeleted,
                                 Information = prv.Information,
                                 Health = prv.Health,
                                 GeneralGovernment = prv.GeneralGovernment,
                                 DFSource = prv.DFSource.NameEn,
                                 Gas = prv.Gas,
                                 FinancialIntermediariesAuxiliaryServices = prv.FinancialIntermediariesAuxiliaryServices,
                                 DFUnit = prv.DFUnit.NameEn,
                                 Education = prv.Education,
                                 Electricity = prv.Electricity,
                                 CreatedById = prv.CreatedById
                             }); ;


            IQueryable<ActivityVM> activityCurrentData;
            if (string.IsNullOrWhiteSpace(role))
                activityCurrentData = queryright.Union(queryleft);
            else
                activityCurrentData = queryright.Union(queryleft).Where(x => x.VersionStatusEnum == VersionStatusEIEnum.Submitted);

            //filter depend on search value 
            if (!string.IsNullOrEmpty(searchValue))
            {
                activityCurrentData = activityCurrentData.Where(x =>
                    x.DFYear.ToLower().Contains(searchValue.ToLower()) ||
                    x.DFSector.ToLower().Contains(searchValue.ToLower()) ||
                    x.DFSource.ToLower().Contains(searchValue.ToLower())
                    );
            }
            totalCount = activityCurrentData.Count();

            if (string.IsNullOrWhiteSpace(sortColumnName))
            {
                activityCurrentData = activityCurrentData.OrderByDescending(x => x.DFYear).ThenBy(x => x.DFQuarter).ThenBy(x => x.DFSector);
            }
            else if (sortDirection == "asc")
                activityCurrentData = activityCurrentData.OrderBy($"{sortColumnName} asc");
            else if (sortDirection == "desc")
                activityCurrentData = activityCurrentData
                    .OrderBy($"{sortColumnName} descending");

            //paging
            return activityCurrentData.Skip(start).Take(lenght).ToList();
        }

        public ActivityCurrent GetById(int id)
        {
            return _db.ActivityCurrents.FirstOrDefault(x => x.Id == id);
        }

        public bool Delete(int id)
        {
            try
            {
                var model = GetById(id);
                if (model != null)
                {
                    model.IsDeleted = true;

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

        public void Update(ActivityCurrent activityCurrent)
        {
            _db.ActivityCurrents.Attach(activityCurrent);
            _db.Entry(activityCurrent).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void AddVer(ActivityCurrentVersion activityCurrentVersion)
        {
            _db.ActivityCurrentVersions.Add(activityCurrentVersion);
            _db.SaveChanges();
        }

        public ActivityCurrentVersion GetVerById(int id, bool disableTracking = true)
        {
            return disableTracking ? _db.ActivityCurrentVersions.AsNoTracking().FirstOrDefault(x => x.Id == id) : _db.ActivityCurrentVersions.FirstOrDefault(x => x.Id == id);
        }

        public ActivityCurrentVersion GetByActivityCurrentId(int id)
        {
            return _db.ActivityCurrentVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.ActivityCurrentId == id);
        }

        public void UpdateVer(ActivityCurrentVersion rgdpVersionModel)
        {
            _db.ActivityCurrentVersions.Update(rgdpVersionModel);
            _db.SaveChanges();
        }

        public IEnumerable<ActivityCurrentVersion> GetAllSubmited()
        {
            return _db.ActivityCurrentVersions.Where(x => x.VersionStatusEnum == VersionStatusEIEnum.Submitted);
        }
    }
}
