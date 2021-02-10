using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Models;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Services.Analytics.Mappers
{
    public static class RGDPMapper
    {
        public static RGDPGrowthRate MapToRGDPModel(this RGDPFormViewModel model)
        {
            return new RGDPGrowthRate()
            {
                Id = model.Id,
                DFIndicatorId = model.Indicator,
                DFSourceId = model.Source,
                DFYearId = model.DFYearFiscalId,
                DFUnitId = model.Unit,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.IsDeleted,
                GrowthRate=model.Value
            };
        }

        public static RGDPGrowthRateVersion MapToRGDPVerModel(this RGDPFormViewModel model)
        {
            return new RGDPGrowthRateVersion()
            {
                Id = model.Id,
                DFIndicatorId = model.Indicator,
                DFSourceId = model.Source,
                DFYearId = model.DFYearFiscalId,
                DFUnitId = model.Unit,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.IsDeleted,
                GrowthRate = model.Value,
                VersionStatusEnum = MPMAR.Analytics.Data.Enums.VersionStatusEIEnum.Draft,
                ChangeActionEnum = MPMAR.Analytics.Data.Enums.ChangeActionEIEnum.New,
                RGDPGrowthRateId = model.RGDPId,
                CreatedById=model.CreatedById
            };
        }
        public static RGDPFormViewModel MapToRGDPViewModel(this RGDPGrowthRate model)
        {
            return new RGDPFormViewModel()
            {
                Id = model.Id,
                Indicator = model.DFIndicatorId,
                Source = model.DFSourceId,
                DFYearFiscalId = model.DFYearId,
                Unit = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.IsDeleted,
                Value = model.GrowthRate,
                RGDPId=model.Id
            };
        }

        public static RGDPFormViewModel MapToRGDPViewModel(this RGDPGrowthRateVersion model)
        {
            return new RGDPFormViewModel()
            {
                Id = model.Id,
                Indicator = model.DFIndicatorId,
                Source = model.DFSourceId,
                DFYearFiscalId = model.DFYearId,
                Unit = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.IsDeleted,
                Value = model.GrowthRate,
                ChangeActionEnum = model.ChangeActionEnum,
                VersionStatusEnum = model.VersionStatusEnum,
                RGDPId = model.RGDPGrowthRateId
            };
        }
        public static RGDPGrowthRate1617 MapToRGDP1617Model(this RGDPFormViewModel model)
        {
            return new RGDPGrowthRate1617()
            {
                Id = model.Id,
                DFIndicatorId = model.Indicator,
                DFSourceId = model.Source,
                DFYearFiscalId = model.DFYearFiscalId,
                DFUnitId = model.Unit,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.IsDeleted,
                Value=model.Value
            };
        }

        public static RGDPFormViewModel MapToRGDP1617ViewModel(this RGDPGrowthRate1617 model)
        {
            return new RGDPFormViewModel()
            {
                Id = model.Id,
                Indicator = model.DFIndicatorId,
                Source = model.DFSourceId,
                DFYearFiscalId = model.DFYearFiscalId,
                Unit = model.DFUnitId,
                DFQuarterId = model.DFQuarterId,
                IsDeleted = model.IsDeleted,
                Value = model.Value
            };
        }
    }
}
