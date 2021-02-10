using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Models;
using MPMAR.Business.Services.Analytics.ViewModels;
using MPMAR.Web.Admin.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MPMAR.Web.Admin.Mappers
{
    public static class GovenorateMapper
    {
        public static Governorate MapToGovernorate(this GovernorateVM governorateVM)
        {
            Governorate governorate = new Governorate()
            {
                Id = governorateVM.Id,
                DFIndicatorId = governorateVM.DFIndicatorId,
                DFYearId = governorateVM.DFYearId,
                DFGovernorateId = governorateVM.DFGovernorateId,
                Unit = governorateVM.Unit,
                DomesticWorkers = governorateVM.DomesticWorkers,
                CustomFees = governorateVM.CustomFees,
                Communication = governorateVM.Communication,
                BusinessServices = governorateVM.BusinessServices,
                Agriculture = governorateVM.Agriculture,
                AccommodationandFoodServiceActivities = governorateVM.AccommodationandFoodServiceActivities,
                Construction = governorateVM.Construction,
                CrudePetroleumExtraction = governorateVM.CrudePetroleumExtraction,
                Education = governorateVM.Education,
                ElectricityandGas = governorateVM.ElectricityandGas,
                FinancialCorporations = governorateVM.FinancialCorporations,
                GeneralGovernment = governorateVM.GeneralGovernment,
                Information = governorateVM.Information,
                Health = governorateVM.Health,
                isDeleted = governorateVM.isDeleted,
                ManufacturingIndustries = governorateVM.ManufacturingIndustries,
                NonFinancialCorporations = governorateVM.NonFinancialCorporations,
                NonProfitInstitutionsServingHouseholdSector = governorateVM.NonProfitInstitutionsServingHouseholdSector,
                OtherExtractions = governorateVM.OtherExtractions,
                OtherServices = governorateVM.OtherServices,
                PetroleumRefinement = governorateVM.PetroleumRefinement,
                RealEstateOwnership = governorateVM.RealEstateOwnership,
                Sewerage = governorateVM.Sewerage,
                TotalGDPEgyptWithCustomFees = governorateVM.TotalGDPEgyptWithCustomFees,
                TotalGovernorateGDP = governorateVM.TotalGovernorateGDP,
                Water = governorateVM.Water,
                TransportationandStorage = governorateVM.TransportationandStorage,
                WasteRecycling = governorateVM.WasteRecycling,
                WholesaleandRetailTrade = governorateVM.WholesaleandRetailTrade
            };
            return governorate;
        }

        public static GovernorateVersion MapToGovernorateVer(this GovernorateVM governorateVM)
        {
            GovernorateVersion governorate = new GovernorateVersion()
            {
                Id = governorateVM.Id,
                DFIndicatorId = governorateVM.DFIndicatorId,
                DFYearId = governorateVM.DFYearId,
                DFGovernorateId = governorateVM.DFGovernorateId,
                Unit = governorateVM.Unit,
                DomesticWorkers = governorateVM.DomesticWorkers,
                CustomFees = governorateVM.CustomFees,
                Communication = governorateVM.Communication,
                BusinessServices = governorateVM.BusinessServices,
                Agriculture = governorateVM.Agriculture,
                AccommodationandFoodServiceActivities = governorateVM.AccommodationandFoodServiceActivities,
                Construction = governorateVM.Construction,
                CrudePetroleumExtraction = governorateVM.CrudePetroleumExtraction,
                Education = governorateVM.Education,
                ElectricityandGas = governorateVM.ElectricityandGas,
                FinancialCorporations = governorateVM.FinancialCorporations,
                GeneralGovernment = governorateVM.GeneralGovernment,
                Information = governorateVM.Information,
                Health = governorateVM.Health,
                isDeleted = governorateVM.isDeleted,
                ManufacturingIndustries = governorateVM.ManufacturingIndustries,
                NonFinancialCorporations = governorateVM.NonFinancialCorporations,
                NonProfitInstitutionsServingHouseholdSector = governorateVM.NonProfitInstitutionsServingHouseholdSector,
                OtherExtractions = governorateVM.OtherExtractions,
                OtherServices = governorateVM.OtherServices,
                PetroleumRefinement = governorateVM.PetroleumRefinement,
                RealEstateOwnership = governorateVM.RealEstateOwnership,
                Sewerage = governorateVM.Sewerage,
                TotalGDPEgyptWithCustomFees = governorateVM.TotalGDPEgyptWithCustomFees,
                TotalGovernorateGDP = governorateVM.TotalGovernorateGDP,
                Water = governorateVM.Water,
                TransportationandStorage = governorateVM.TransportationandStorage,
                WasteRecycling = governorateVM.WasteRecycling,
                WholesaleandRetailTrade = governorateVM.WholesaleandRetailTrade,
                VersionStatusEnum = MPMAR.Analytics.Data.Enums.VersionStatusEIEnum.Draft,
                ChangeActionEnum = MPMAR.Analytics.Data.Enums.ChangeActionEIEnum.New,
                GovernorateId = governorateVM.GovernorateId,
                CreatedById=governorateVM.CreatedById
            };
            return governorate;
        }
        public static GovernorateVM MapToGovernorateVM(this Governorate governorate)
        {
            GovernorateVM governorateVM = new GovernorateVM()
            {
                Id = governorate.Id,
                DFIndicatorId = governorate.DFIndicatorId,
                DFYearId = governorate.DFYearId,
                DFGovernorateId = governorate.DFGovernorateId,
                Unit = governorate.Unit,
                DomesticWorkers = governorate.DomesticWorkers,
                CustomFees = governorate.CustomFees,
                Communication = governorate.Communication,
                BusinessServices = governorate.BusinessServices,
                Agriculture = governorate.Agriculture,
                AccommodationandFoodServiceActivities = governorate.AccommodationandFoodServiceActivities,
                Construction = governorate.Construction,
                CrudePetroleumExtraction = governorate.CrudePetroleumExtraction,
                Education = governorate.Education,
                ElectricityandGas = governorate.ElectricityandGas,
                FinancialCorporations = governorate.FinancialCorporations,
                GeneralGovernment = governorate.GeneralGovernment,
                Information = governorate.Information,
                Health = governorate.Health,
                isDeleted = governorate.isDeleted,
                ManufacturingIndustries = governorate.ManufacturingIndustries,
                NonFinancialCorporations = governorate.NonFinancialCorporations,
                NonProfitInstitutionsServingHouseholdSector = governorate.NonProfitInstitutionsServingHouseholdSector,
                OtherExtractions = governorate.OtherExtractions,
                OtherServices = governorate.OtherServices,
                PetroleumRefinement = governorate.PetroleumRefinement,
                RealEstateOwnership = governorate.RealEstateOwnership,
                Sewerage = governorate.Sewerage,
                TotalGDPEgyptWithCustomFees = governorate.TotalGDPEgyptWithCustomFees,
                TotalGovernorateGDP = governorate.TotalGovernorateGDP,
                Water = governorate.Water,
                TransportationandStorage = governorate.TransportationandStorage,
                WasteRecycling = governorate.WasteRecycling,
                WholesaleandRetailTrade = governorate.WholesaleandRetailTrade,
                GovernorateId = governorate.Id,
            };
            return governorateVM;
        }
        public static GovernorateVM MapToGovernorateVM(this GovernorateVersion governorate)
        {
            GovernorateVM governorateVM = new GovernorateVM()
            {
                Id = governorate.Id,
                DFIndicatorId = governorate.DFIndicatorId,
                DFYearId = governorate.DFYearId,
                DFGovernorateId = governorate.DFGovernorateId,
                Unit = governorate.Unit,
                DomesticWorkers = governorate.DomesticWorkers,
                CustomFees = governorate.CustomFees,
                Communication = governorate.Communication,
                BusinessServices = governorate.BusinessServices,
                Agriculture = governorate.Agriculture,
                AccommodationandFoodServiceActivities = governorate.AccommodationandFoodServiceActivities,
                Construction = governorate.Construction,
                CrudePetroleumExtraction = governorate.CrudePetroleumExtraction,
                Education = governorate.Education,
                ElectricityandGas = governorate.ElectricityandGas,
                FinancialCorporations = governorate.FinancialCorporations,
                GeneralGovernment = governorate.GeneralGovernment,
                Information = governorate.Information,
                Health = governorate.Health,
                isDeleted = governorate.isDeleted,
                ManufacturingIndustries = governorate.ManufacturingIndustries,
                NonFinancialCorporations = governorate.NonFinancialCorporations,
                NonProfitInstitutionsServingHouseholdSector = governorate.NonProfitInstitutionsServingHouseholdSector,
                OtherExtractions = governorate.OtherExtractions,
                OtherServices = governorate.OtherServices,
                PetroleumRefinement = governorate.PetroleumRefinement,
                RealEstateOwnership = governorate.RealEstateOwnership,
                Sewerage = governorate.Sewerage,
                TotalGDPEgyptWithCustomFees = governorate.TotalGDPEgyptWithCustomFees,
                TotalGovernorateGDP = governorate.TotalGovernorateGDP,
                Water = governorate.Water,
                TransportationandStorage = governorate.TransportationandStorage,
                WasteRecycling = governorate.WasteRecycling,
                WholesaleandRetailTrade = governorate.WholesaleandRetailTrade,
                ChangeActionEnum = governorate.ChangeActionEnum,
                VersionStatusEnum = governorate.VersionStatusEnum,
                GovernorateId = governorate.GovernorateId
            };
            return governorateVM;
        }


    }

}
