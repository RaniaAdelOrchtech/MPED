using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Models;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Services.Analytics.Mappers
{
    public static class ComponentMapper
    {
        public static ComponentConstant MapToComponentConstantModel(this ComponentFormViewModel model)
        {
            return new ComponentConstant()
            {
                Id = model.Id,
                DFIndicatorId = model.DFIndicatorId,
                DFSourceId = model.DFSourceId,
                DFYearFiscalId = model.DFYearFiscalId,
                DFUnitId = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.IsDeleted,
                ExportsOfGoodsAndServices = model.ExportsOfGoodsAndServices,
                GovernmentConsumption = model.GovernmentConsumption,
                GrossCapitalFormation = model.GrossCapitalFormation,
                ImportsOfGoodsAndServices = model.ImportsOfGoodsAndServices,
                PrivateConsumption = model.PrivateConsumption,
                TotalGrossDomesticProductAtMarketPrices = model.TotalGrossDomesticProductAtMarketPrices
            };
        }

        public static ComponentConstantVersion MapToComponentConstantVerModel(this ComponentFormViewModel model)
        {
            return new ComponentConstantVersion()
            {
                Id = model.Id,
                DFIndicatorId = model.DFIndicatorId,
                DFSourceId = model.DFSourceId,
                DFYearFiscalId = model.DFYearFiscalId,
                DFUnitId = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.IsDeleted,
                ExportsOfGoodsAndServices = model.ExportsOfGoodsAndServices,
                GovernmentConsumption = model.GovernmentConsumption,
                GrossCapitalFormation = model.GrossCapitalFormation,
                ImportsOfGoodsAndServices = model.ImportsOfGoodsAndServices,
                PrivateConsumption = model.PrivateConsumption,
                TotalGrossDomesticProductAtMarketPrices = model.TotalGrossDomesticProductAtMarketPrices,
                VersionStatusEnum = MPMAR.Analytics.Data.Enums.VersionStatusEIEnum.Draft,
                ChangeActionEnum = MPMAR.Analytics.Data.Enums.ChangeActionEIEnum.New,
                ComponentConstantId = model.ComponentId,
                CreatedById = model.CreatedById
            };
        }

        public static ComponentFormViewModel MapToComponentConstantViewModel(this ComponentConstant model)
        {
            return new ComponentFormViewModel()
            {
                Id = model.Id,
                DFIndicatorId = model.DFIndicatorId,
                DFSourceId = model.DFSourceId,
                DFYearFiscalId = model.DFYearFiscalId,
                DFUnitId = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.IsDeleted,
                ExportsOfGoodsAndServices = model.ExportsOfGoodsAndServices,
                GovernmentConsumption = model.GovernmentConsumption,
                GrossCapitalFormation = model.GrossCapitalFormation,
                ImportsOfGoodsAndServices = model.ImportsOfGoodsAndServices,
                PrivateConsumption = model.PrivateConsumption,
                TotalGrossDomesticProductAtMarketPrices = model.TotalGrossDomesticProductAtMarketPrices,
                ComponentId = model.Id,

            };
        }

        public static ComponentFormViewModel MapToComponentConstantViewModel(this ComponentConstantVersion model)
        {
            return new ComponentFormViewModel()
            {
                Id = model.Id,
                DFIndicatorId = model.DFIndicatorId,
                DFSourceId = model.DFSourceId,
                DFYearFiscalId = model.DFYearFiscalId,
                DFUnitId = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.IsDeleted,
                ExportsOfGoodsAndServices = model.ExportsOfGoodsAndServices,
                GovernmentConsumption = model.GovernmentConsumption,
                GrossCapitalFormation = model.GrossCapitalFormation,
                ImportsOfGoodsAndServices = model.ImportsOfGoodsAndServices,
                PrivateConsumption = model.PrivateConsumption,
                TotalGrossDomesticProductAtMarketPrices = model.TotalGrossDomesticProductAtMarketPrices,
                ChangeActionEnum = model.ChangeActionEnum,
                VersionStatusEnum = model.VersionStatusEnum,
                ComponentId = model.ComponentConstantId
            };
        }
        public static ComponentCurrent MapToComponentCurrentModel(this ComponentFormViewModel model)
        {
            return new ComponentCurrent()
            {
                Id = model.Id,
                DFIndicatorId = model.DFIndicatorId,
                DFSourceId = model.DFSourceId,
                DFYearFiscalId = model.DFYearFiscalId,
                DFUnitId = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.IsDeleted,
                ExportsOfGoodsAndServices = model.ExportsOfGoodsAndServices,
                GovernmentConsumption = model.GovernmentConsumption,
                GrossCapitalFormation = model.GrossCapitalFormation,
                ImportsOfGoodsAndServices = model.ImportsOfGoodsAndServices,
                PrivateConsumption = model.PrivateConsumption,
                TotalGrossDomesticProductAtMarketPrices = model.TotalGrossDomesticProductAtMarketPrices
            };
        }

        public static ComponentCurrentVersion MapToComponentCurrentVerModel(this ComponentFormViewModel model)
        {
            return new ComponentCurrentVersion()
            {
                Id = model.Id,
                DFIndicatorId = model.DFIndicatorId,
                DFSourceId = model.DFSourceId,
                DFYearFiscalId = model.DFYearFiscalId,
                DFUnitId = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.IsDeleted,
                ExportsOfGoodsAndServices = model.ExportsOfGoodsAndServices,
                GovernmentConsumption = model.GovernmentConsumption,
                GrossCapitalFormation = model.GrossCapitalFormation,
                ImportsOfGoodsAndServices = model.ImportsOfGoodsAndServices,
                PrivateConsumption = model.PrivateConsumption,
                TotalGrossDomesticProductAtMarketPrices = model.TotalGrossDomesticProductAtMarketPrices,
                VersionStatusEnum = MPMAR.Analytics.Data.Enums.VersionStatusEIEnum.Draft,
                ChangeActionEnum = MPMAR.Analytics.Data.Enums.ChangeActionEIEnum.New,
                ComponentCurrentId = model.ComponentId,
                CreatedById=model.CreatedById
            };
        }

        public static ComponentFormViewModel MapToComponentCurrentViewModel(this ComponentCurrent model)
        {
            return new ComponentFormViewModel()
            {
                Id = model.Id,
                DFIndicatorId = model.DFIndicatorId,
                DFSourceId = model.DFSourceId,
                DFYearFiscalId = model.DFYearFiscalId,
                DFUnitId = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.IsDeleted,
                ExportsOfGoodsAndServices = model.ExportsOfGoodsAndServices,
                GovernmentConsumption = model.GovernmentConsumption,
                GrossCapitalFormation = model.GrossCapitalFormation,
                ImportsOfGoodsAndServices = model.ImportsOfGoodsAndServices,
                PrivateConsumption = model.PrivateConsumption,
                TotalGrossDomesticProductAtMarketPrices = model.TotalGrossDomesticProductAtMarketPrices,
                ComponentId = model.Id
            };
        }

        public static ComponentFormViewModel MapToComponentCurrentViewModel(this ComponentCurrentVersion model)
        {
            return new ComponentFormViewModel()
            {
                Id = model.Id,
                DFIndicatorId = model.DFIndicatorId,
                DFSourceId = model.DFSourceId,
                DFYearFiscalId = model.DFYearFiscalId,
                DFUnitId = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.IsDeleted,
                ExportsOfGoodsAndServices = model.ExportsOfGoodsAndServices,
                GovernmentConsumption = model.GovernmentConsumption,
                GrossCapitalFormation = model.GrossCapitalFormation,
                ImportsOfGoodsAndServices = model.ImportsOfGoodsAndServices,
                PrivateConsumption = model.PrivateConsumption,
                TotalGrossDomesticProductAtMarketPrices = model.TotalGrossDomesticProductAtMarketPrices,
                ChangeActionEnum = model.ChangeActionEnum,
                ComponentId = model.ComponentCurrentId,
                VersionStatusEnum = model.VersionStatusEnum
            };
        }
    }
}
