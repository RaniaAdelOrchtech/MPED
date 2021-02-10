using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Models;
using MPMAR.Business.Services.Analytics.ViewModels;
using MPMAR.Web.Admin.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MPMAR.Web.Admin.Mappers
{
    public static class ActivityMapper
    {
        public static ActivityConstant MapToActivityConst(this ActivityVMForCreateEdit x)
        {
            ActivityConstant activityConstant = new ActivityConstant()
            {
                Id = x.Id,
                DFIndicatorId = x.DFIndicatorId,
                DFQuarterId = x.DFQuarterId,
                DFSectorId = x.DFSectorId,
                DFSourceId = x.DFSourceId,
                DFUnitId = x.DFUnitId,
                DFYearId = x.DFYearId,
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
                MiningQuarrying = x.MiningQuarrying,
                OtherExtraction = x.OtherExtraction,
                OtherManufacturing = x.OtherManufacturing,
                Petroleum = x.Petroleum,
                petroleumRefining = x.petroleumRefining,
                RealEstateActivitie = x.RealEstateActivitie,
                SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                SocialServices = x.SocialServices,
                SuezcCanal = x.SuezcCanal,
                TotalGDPAtFactorCost = x.TotalGDPAtFactorCost,
                TransportationAndStorage = x.TransportationAndStorage,
                WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,

            };
            return activityConstant;
        }
        public static ActivityCurrent MapToActivityCurrent(this ActivityVMForCreateEdit x)
        {
            ActivityCurrent activityCurrent = new ActivityCurrent()
            {
                Id = x.Id,
                DFIndicatorId = x.DFIndicatorId,
                DFQuarterId = x.DFQuarterId,
                DFSectorId = x.DFSectorId,
                DFSourceId = x.DFSourceId,
                DFUnitId = x.DFUnitId,
                DFYearId = x.DFYearId,
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
                MiningQuarrying = x.MiningQuarrying,
                OtherExtraction = x.OtherExtraction,
                OtherManufacturing = x.OtherManufacturing,
                Petroleum = x.Petroleum,
                petroleumRefining = x.petroleumRefining,
                RealEstateActivitie = x.RealEstateActivitie,
                SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                SocialServices = x.SocialServices,
                SuezcCanal = x.SuezcCanal,
                TotalGDPAtFactorCost = x.TotalGDPAtFactorCost,
                TransportationAndStorage = x.TransportationAndStorage,
                WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,

            };
            return activityCurrent;
        }

        public static ActivityCurrentVersion MapToActivityCurrentVer(this ActivityVMForCreateEdit x)
        {
            ActivityCurrentVersion activityCurrent = new ActivityCurrentVersion()
            {
                Id = x.Id,
                DFIndicatorId = x.DFIndicatorId,
                DFQuarterId = x.DFQuarterId,
                DFSectorId = x.DFSectorId,
                DFSourceId = x.DFSourceId,
                DFUnitId = x.DFUnitId,
                DFYearId = x.DFYearId,
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
                MiningQuarrying = x.MiningQuarrying,
                OtherExtraction = x.OtherExtraction,
                OtherManufacturing = x.OtherManufacturing,
                Petroleum = x.Petroleum,
                petroleumRefining = x.petroleumRefining,
                RealEstateActivitie = x.RealEstateActivitie,
                SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                SocialServices = x.SocialServices,
                SuezcCanal = x.SuezcCanal,
                TotalGDPAtFactorCost = x.TotalGDPAtFactorCost,
                TransportationAndStorage = x.TransportationAndStorage,
                WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,
                ActivityCurrentId = x.ActivityCurrentId,
                VersionStatusEnum = MPMAR.Analytics.Data.Enums.VersionStatusEIEnum.Draft,
                ChangeActionEnum = MPMAR.Analytics.Data.Enums.ChangeActionEIEnum.New,
                CreatedById=x.CreatedById
            };
            return activityCurrent;
        }

        public static SectorGrowthRate MapToSectorGrowth(this ActivityVMForCreateEdit x)
        {
            SectorGrowthRate sectorGrowthRate = new SectorGrowthRate()
            {
                Id = x.Id,
                DFIndicatorId = x.DFIndicatorId,
                DFQuarterId = x.DFQuarterId,
                DFSectorId = x.DFSectorId,
                DFSourceId = x.DFSourceId,
                DFUnitId = x.DFUnitId,
                DFYearId = x.DFYearId,
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
                MiningQuarrying = x.MiningQuarrying,
                OtherExtraction = x.OtherExtraction,
                OtherManufacturing = x.OtherManufacturing,
                Petroleum = x.Petroleum,
                petroleumRefining = x.petroleumRefining,
                RealEstateActivitie = x.RealEstateActivitie,
                SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                SocialServices = x.SocialServices,
                SuezcCanal = x.SuezcCanal,
                TotalGDPAtFactorCost = x.TotalGDPAtFactorCost,
                TransportationAndStorage = x.TransportationAndStorage,
                WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                WholesaleAndRetailTrade = x.WholesaleAndRetailTrade
            };
            return sectorGrowthRate;
        }
        public static SectorGrowthRateVersion MapToSectorGrowthVer(this ActivityVMForCreateEdit x)
        {
            SectorGrowthRateVersion sectorGrowthRate = new SectorGrowthRateVersion()
            {
                Id = x.Id,
                DFIndicatorId = x.DFIndicatorId,
                DFQuarterId = x.DFQuarterId,
                DFSectorId = x.DFSectorId,
                DFSourceId = x.DFSourceId,
                DFUnitId = x.DFUnitId,
                DFYearId = x.DFYearId,
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
                MiningQuarrying = x.MiningQuarrying,
                OtherExtraction = x.OtherExtraction,
                OtherManufacturing = x.OtherManufacturing,
                Petroleum = x.Petroleum,
                petroleumRefining = x.petroleumRefining,
                RealEstateActivitie = x.RealEstateActivitie,
                SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                SocialServices = x.SocialServices,
                SuezcCanal = x.SuezcCanal,
                TotalGDPAtFactorCost = x.TotalGDPAtFactorCost,
                TransportationAndStorage = x.TransportationAndStorage,
                WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,
                SectorGrowthRateId = x.ActivityCurrentId,
                VersionStatusEnum = MPMAR.Analytics.Data.Enums.VersionStatusEIEnum.Draft,
                ChangeActionEnum = MPMAR.Analytics.Data.Enums.ChangeActionEIEnum.New,
                CreatedById=x.CreatedById
            };
            return sectorGrowthRate;
        }

        public static ActivityVMForCreateEdit MapToActivityConstVM(this ActivityConstant x)
        {
            ActivityVMForCreateEdit activityVMForCreateEdit = new ActivityVMForCreateEdit()
            {
                Id = x.Id,
                DFIndicatorId = x.DFIndicatorId,
                DFQuarterId = x.DFQuarterId,
                DFSectorId = x.DFSectorId,
                DFSourceId = x.DFSourceId,
                DFUnitId = x.DFUnitId,
                DFYearId = x.DFYearId,
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
                MiningQuarrying = x.MiningQuarrying,
                OtherExtraction = x.OtherExtraction,
                OtherManufacturing = x.OtherManufacturing,
                Petroleum = x.Petroleum,
                petroleumRefining = x.petroleumRefining,
                RealEstateActivitie = x.RealEstateActivitie,
                SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                SocialServices = x.SocialServices,
                SuezcCanal = x.SuezcCanal,
                TotalGDPAtFactorCost = x.TotalGDPAtFactorCost,
                TransportationAndStorage = x.TransportationAndStorage,
                WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                WholesaleAndRetailTrade = x.WholesaleAndRetailTrade
            };
            return activityVMForCreateEdit;
        }

        public static ActivityVMForCreateEdit MapToActivityCurrentVM(this ActivityCurrent x)
        {
            ActivityVMForCreateEdit activityVMForCreateEdit = new ActivityVMForCreateEdit()
            {
                Id = x.Id,
                DFIndicatorId = x.DFIndicatorId,
                DFQuarterId = x.DFQuarterId,
                DFSectorId = x.DFSectorId,
                DFSourceId = x.DFSourceId,
                DFUnitId = x.DFUnitId,
                DFYearId = x.DFYearId,
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
                MiningQuarrying = x.MiningQuarrying,
                OtherExtraction = x.OtherExtraction,
                OtherManufacturing = x.OtherManufacturing,
                Petroleum = x.Petroleum,
                petroleumRefining = x.petroleumRefining,
                RealEstateActivitie = x.RealEstateActivitie,
                SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                SocialServices = x.SocialServices,
                SuezcCanal = x.SuezcCanal,
                TotalGDPAtFactorCost = x.TotalGDPAtFactorCost,
                TransportationAndStorage = x.TransportationAndStorage,
                WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,
                ActivityCurrentId = x.Id,

            };
            return activityVMForCreateEdit;
        }

        public static ActivityVMForCreateEdit MapToActivityCurrentVMVer(this ActivityCurrentVersion x)
        {
            ActivityVMForCreateEdit activityVMForCreateEdit = new ActivityVMForCreateEdit()
            {
                Id = x.Id,
                DFIndicatorId = x.DFIndicatorId,
                DFQuarterId = x.DFQuarterId,
                DFSectorId = x.DFSectorId,
                DFSourceId = x.DFSourceId,
                DFUnitId = x.DFUnitId,
                DFYearId = x.DFYearId,
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
                MiningQuarrying = x.MiningQuarrying,
                OtherExtraction = x.OtherExtraction,
                OtherManufacturing = x.OtherManufacturing,
                Petroleum = x.Petroleum,
                petroleumRefining = x.petroleumRefining,
                RealEstateActivitie = x.RealEstateActivitie,
                SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                SocialServices = x.SocialServices,
                SuezcCanal = x.SuezcCanal,
                TotalGDPAtFactorCost = x.TotalGDPAtFactorCost,
                TransportationAndStorage = x.TransportationAndStorage,
                WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,
                ChangeActionEnum = x.ChangeActionEnum,
                ActivityCurrentId = x.ActivityCurrentId,
                VersionStatusEnum = x.VersionStatusEnum
            };
            return activityVMForCreateEdit;
        }

        public static ActivityVMForCreateEdit MapToSectorGrowthVM(this SectorGrowthRate x)
        {
            ActivityVMForCreateEdit activityVMForCreateEdit = new ActivityVMForCreateEdit()
            {
                Id = x.Id,
                DFIndicatorId = x.DFIndicatorId,
                DFQuarterId = x.DFQuarterId,
                DFSectorId = x.DFSectorId,
                DFSourceId = x.DFSourceId,
                DFUnitId = x.DFUnitId,
                DFYearId = x.DFYearId,
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
                MiningQuarrying = x.MiningQuarrying,
                OtherExtraction = x.OtherExtraction,
                OtherManufacturing = x.OtherManufacturing,
                Petroleum = x.Petroleum,
                petroleumRefining = x.petroleumRefining,
                RealEstateActivitie = x.RealEstateActivitie,
                SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                SocialServices = x.SocialServices,
                SuezcCanal = x.SuezcCanal,
                TotalGDPAtFactorCost = x.TotalGDPAtFactorCost,
                TransportationAndStorage = x.TransportationAndStorage,
                WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,
                ActivityCurrentId = x.Id
            };
            return activityVMForCreateEdit;
        }

        public static ActivityVMForCreateEdit MapToSectorGrowthVMVer(this SectorGrowthRateVersion x)
        {
            ActivityVMForCreateEdit activityVMForCreateEdit = new ActivityVMForCreateEdit()
            {
                Id = x.Id,
                DFIndicatorId = x.DFIndicatorId,
                DFQuarterId = x.DFQuarterId,
                DFSectorId = x.DFSectorId,
                DFSourceId = x.DFSourceId,
                DFUnitId = x.DFUnitId,
                DFYearId = x.DFYearId,
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
                MiningQuarrying = x.MiningQuarrying,
                OtherExtraction = x.OtherExtraction,
                OtherManufacturing = x.OtherManufacturing,
                Petroleum = x.Petroleum,
                petroleumRefining = x.petroleumRefining,
                RealEstateActivitie = x.RealEstateActivitie,
                SocialSecurityAndInsurance = x.SocialSecurityAndInsurance,
                SocialServices = x.SocialServices,
                SuezcCanal = x.SuezcCanal,
                TotalGDPAtFactorCost = x.TotalGDPAtFactorCost,
                TransportationAndStorage = x.TransportationAndStorage,
                WaterSewerageRemediationActivitie = x.WaterSewerageRemediationActivitie,
                WholesaleAndRetailTrade = x.WholesaleAndRetailTrade,
                ChangeActionEnum = x.ChangeActionEnum,
                ActivityCurrentId = x.SectorGrowthRateId,
                VersionStatusEnum = x.VersionStatusEnum
            };
            return activityVMForCreateEdit;
        }

    }

}
