using Microsoft.EntityFrameworkCore;
using MPMAR.Analytics.Data;
using MPMAR.Business.Interfaces.Analytics;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;

namespace MPMAR.Business.Services.Analytics
{
    public class ActivityConstantRepository : IActivityConstantRepository
    {
        private readonly AnalyticsDbContext _db;
        public ActivityConstantRepository(AnalyticsDbContext db)
        {
            _db = db;
        }

        public void Add(ActivityConstant activityConstant)
        {
            _db.ActivityConstants.Add(activityConstant);
            _db.SaveChanges();
        }

        public IEnumerable<ActivityVM> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount)
        {
            var activityConstantData = _db.ActivityConstants
                .Where(x => !(x.IsDeleted ?? false))
                .OrderByDescending(x => x.DFYear.Order).ThenBy(x => x.DFQuarterId)
                .Include(x => x.DFIndicator).Include(x => x.DFSource).Include(x => x.DFQuarter)
                .Include(x => x.DFUnit).Include(x => x.DFYear).Include(x=>x.DFSector)
                .Select(x => new ActivityVM()
                {
                    Id = x.Id,
                    DFIndicator = x.DFIndicator.NameEn,
                    DFQuarter = x.DFQuarter.NameEn,
                    DFSector = x.DFSector.NameEn,
                    DFSource = x.DFSource.NameEn,
                    DFUnit = x.DFUnit.NameEn,
                    DFYear = x.DFYear.NameEn,
                    BusinessServices = x.BusinessServices,
                    Communication = x.Communication,
                    Construction = x.Construction,
                    Education = x.Education,
                    GeneralGovernment = x.GeneralGovernment,
                    Health = x.Health,
                    Information = x.Information,
                    ManufacturingIndustries = x.ManufacturingIndustries,
                    OtherServices = x.OtherServices,
                    RealEstateOwnership = x.RealEstateOwnership,
                    AccommodationAndFoodServiceActivities = x.AccommodationAndFoodServiceActivities,
                    AgricultureForestryFishing = x.AgricultureForestryFishing,
                    Electricity = x.Electricity,
                    FinancialIntermediariesAuxiliaryServices = x.FinancialIntermediariesAuxiliaryServices,
                    Gas = x.Gas,
                    IsDeleted = x.IsDeleted,
                    MiningQuarrying=x.MiningQuarrying,
                    OtherExtraction=x.OtherExtraction,
                    OtherManufacturing=x.OtherManufacturing,
                    Petroleum=x.Petroleum,
                    petroleumRefining=x.petroleumRefining,
                    RealEstateActivitie=x.RealEstateActivitie,
                    SocialSecurityAndInsurance=x.SocialSecurityAndInsurance,
                    SocialServices=x.SocialServices,
                    SuezcCanal=x.SuezcCanal,
                    TotalGDPAtFactorCost=x.TotalGDPAtFactorCost,
                    TransportationAndStorage=x.TransportationAndStorage,
                    WaterSewerageRemediationActivitie=x.WaterSewerageRemediationActivitie,
                    WholesaleAndRetailTrade=x.WholesaleAndRetailTrade
                });

            //filter depend on search value 
            if (!string.IsNullOrEmpty(searchValue))
            {
                activityConstantData = activityConstantData.Where(x =>
                    x.DFYear.ToLower().Contains(searchValue.ToLower())||
                    x.DFYearBase.ToLower().Contains(searchValue.ToLower())||
                    x.DFSector.ToLower().Contains(searchValue.ToLower())||
                    x.DFSource.ToLower().Contains(searchValue.ToLower())
                    );
            }

            totalCount = activityConstantData.Count();
            if (sortDirection == "asc")
                activityConstantData = activityConstantData.OrderBy($"{sortColumnName} asc");
            else if (sortDirection == "desc")
                activityConstantData = activityConstantData
                    .OrderBy($"{sortColumnName} descending");

            //paging
            return activityConstantData.Skip(start).Take(lenght).ToList();
        }

        public ActivityConstant GetById(int id)
        {
            return _db.ActivityConstants.FirstOrDefault(x => x.Id == id);
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

        public void Update(ActivityConstant activityConstant)
        {
            _db.ActivityConstants.Attach(activityConstant);
            _db.Entry(activityConstant).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
