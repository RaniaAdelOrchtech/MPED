using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Models;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class InvestmentFormMapper
    {
        public static Investments MapToInvestmentModel(this InvestmentFormViewModel model)
        {
            return new Investments()
            {
                Id = model.Id,
                DFIndicatorId = model.DFIndicatorId,
                DFSourceId = model.DFSourceId,
                DFYearId = model.DFYearId,
                DFUnitId = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                isDeleted = model.IsDeleted,
                Agriculture = model.Agriculture,
                AccommodationAndFoodServiceActivities = model.AccommodationAndFoodServiceActivities,
                Construction = model.Construction,
                Education = model.Education,
                Electricity = model.Electricity,
                FinancialIntermediaryInsuranceAndSocialSecurity = model.FinancialIntermediaryInsuranceAndSocialSecurity,
                Health = model.Health,
                InformationAndCommunication = model.InformationAndCommunication,
                NaturalGas = model.NaturalGas,
                OtherExtractions = model.OtherExtractions,
                OtherManufacturing = model.OtherManufacturing,
                OtherSrvices = model.OtherSrvices,
                Petroleum = model.Petroleum,
                PetroleumRefining = model.PetroleumRefining,
                RealEstateActivities = model.RealEstateActivities,
                StorageAndTransportation = model.StorageAndTransportation,
                SuezCanal = model.SuezCanal,
                TotalInvestments = model.TotalInvestments,
                WaterAndSewerage = model.WaterAndSewerage,
                WholesaleAndRetailTrade = model.WholesaleAndRetailTrade,



            };
        }

        public static InvestmentVersion MapToInvestmentModelVer(this InvestmentFormViewModel model)
        {
            return new InvestmentVersion()
            {
                Id = model.Id,
                DFIndicatorId = model.DFIndicatorId,
                DFSourceId = model.DFSourceId,
                DFYearId = model.DFYearId,
                DFUnitId = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                isDeleted = model.IsDeleted,
                Agriculture = model.Agriculture,
                AccommodationAndFoodServiceActivities = model.AccommodationAndFoodServiceActivities,
                Construction = model.Construction,
                Education = model.Education,
                Electricity = model.Electricity,
                FinancialIntermediaryInsuranceAndSocialSecurity = model.FinancialIntermediaryInsuranceAndSocialSecurity,
                Health = model.Health,
                InformationAndCommunication = model.InformationAndCommunication,
                NaturalGas = model.NaturalGas,
                OtherExtractions = model.OtherExtractions,
                OtherManufacturing = model.OtherManufacturing,
                OtherSrvices = model.OtherSrvices,
                Petroleum = model.Petroleum,
                PetroleumRefining = model.PetroleumRefining,
                RealEstateActivities = model.RealEstateActivities,
                StorageAndTransportation = model.StorageAndTransportation,
                SuezCanal = model.SuezCanal,
                TotalInvestments = model.TotalInvestments,
                WaterAndSewerage = model.WaterAndSewerage,
                WholesaleAndRetailTrade = model.WholesaleAndRetailTrade,
                VersionStatusEnum = MPMAR.Analytics.Data.Enums.VersionStatusEIEnum.Draft,
                ChangeActionEnum = MPMAR.Analytics.Data.Enums.ChangeActionEIEnum.New,
                InvestmentsId = model.Investmentid,
                CreatedById = model.CreatedById
            };
        }

        public static InvestmentFormViewModel MapToInvestmentFormViewModel(this Investments model)
        {
            return new InvestmentFormViewModel()
            {
                Id = model.Id,
                DFIndicatorId = model.DFIndicatorId,
                DFSourceId = model.DFSourceId,
                DFYearId = model.DFYearId,
                DFUnitId = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.isDeleted,
                Agriculture = model.Agriculture,
                AccommodationAndFoodServiceActivities = model.AccommodationAndFoodServiceActivities,
                Construction = model.Construction,
                Education = model.Education,
                Electricity = model.Electricity,
                FinancialIntermediaryInsuranceAndSocialSecurity = model.FinancialIntermediaryInsuranceAndSocialSecurity,
                Health = model.Health,
                InformationAndCommunication = model.InformationAndCommunication,
                NaturalGas = model.NaturalGas,
                OtherExtractions = model.OtherExtractions,
                OtherManufacturing = model.OtherManufacturing,
                OtherSrvices = model.OtherSrvices,
                Petroleum = model.Petroleum,
                PetroleumRefining = model.PetroleumRefining,
                RealEstateActivities = model.RealEstateActivities,
                StorageAndTransportation = model.StorageAndTransportation,
                SuezCanal = model.SuezCanal,
                TotalInvestments = model.TotalInvestments,
                WaterAndSewerage = model.WaterAndSewerage,
                WholesaleAndRetailTrade = model.WholesaleAndRetailTrade,
                Investmentid = model.Id
            };
        }

        public static InvestmentFormViewModel MapToInvestmentFormViewModelVer(this InvestmentVersion model)
        {
            return new InvestmentFormViewModel()
            {
                Id = model.Id,
                DFIndicatorId = model.DFIndicatorId,
                DFSourceId = model.DFSourceId,
                DFYearId = model.DFYearId,
                DFUnitId = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.isDeleted,
                Agriculture = model.Agriculture,
                AccommodationAndFoodServiceActivities = model.AccommodationAndFoodServiceActivities,
                Construction = model.Construction,
                Education = model.Education,
                Electricity = model.Electricity,
                FinancialIntermediaryInsuranceAndSocialSecurity = model.FinancialIntermediaryInsuranceAndSocialSecurity,
                Health = model.Health,
                InformationAndCommunication = model.InformationAndCommunication,
                NaturalGas = model.NaturalGas,
                OtherExtractions = model.OtherExtractions,
                OtherManufacturing = model.OtherManufacturing,
                OtherSrvices = model.OtherSrvices,
                Petroleum = model.Petroleum,
                PetroleumRefining = model.PetroleumRefining,
                RealEstateActivities = model.RealEstateActivities,
                StorageAndTransportation = model.StorageAndTransportation,
                SuezCanal = model.SuezCanal,
                TotalInvestments = model.TotalInvestments,
                WaterAndSewerage = model.WaterAndSewerage,
                WholesaleAndRetailTrade = model.WholesaleAndRetailTrade,
                ChangeActionEnum = model.ChangeActionEnum,
                VersionStatusEnum = model.VersionStatusEnum,
                Investmentid = model.InvestmentsId


            };
        }

    }
}
