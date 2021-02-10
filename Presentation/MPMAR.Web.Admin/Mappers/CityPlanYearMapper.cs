using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class CityPlanYearMapper
    {
        public static List<CityPlanYearListViewModel> MapToCityPlanYearViewModel(this IEnumerable<CityPlanYear> CityPlanYear)
        {
            return CityPlanYear.Select(cityPlanYear => new CityPlanYearListViewModel
            {

                Id = cityPlanYear.Id,
                CityPlanId = cityPlanYear.CityPlanId,
                GovName = cityPlanYear.GovName,
                GovYear = cityPlanYear.GovYear,
                IsMapActive = cityPlanYear.IsMapActive,
                EnFileUrl = cityPlanYear.EnFileUrl,
                ArFileUrl = cityPlanYear.ArFileUrl,
                CreationDate = cityPlanYear.CreationDate,
                CreatedById = cityPlanYear.CreatedById,
                ApprovalDate = cityPlanYear.ApprovalDate,
                ApprovedById = cityPlanYear.ApprovedById,
                IsActive = cityPlanYear.IsActive,
                StatusId = cityPlanYear.StatusId,
                IsDeleted = cityPlanYear.IsDeleted,
             
            }).ToList();
        }

        public static CityPlanYearVersion MapToCityPlanYearVersion(this CityPlanYearEditViewModel cityPlanYear)
        {
            var ctPlanYear = new CityPlanYearVersion
            {
                Id = cityPlanYear.Id,
                CityPlanVersionId = cityPlanYear.CityPlanId,
                GovName = cityPlanYear.GovName,
                GovYear = cityPlanYear.GovYear,
                IsMapActive = cityPlanYear.IsMapActive,
                EnFileUrl = cityPlanYear.EnFileUrl,
                ArFileUrl = cityPlanYear.ArFileUrl,
                CreationDate = cityPlanYear.CreationDate,
                CreatedById = cityPlanYear.CreatedById,
                ApprovalDate = cityPlanYear.ApprovalDate,
                ApprovedById = cityPlanYear.ApprovedById,
                IsActive = cityPlanYear.IsActive,
                StatusId = cityPlanYear.StatusId,
                IsDeleted = cityPlanYear.IsDeleted,
                CityPlanYearId=cityPlanYear.CityPlanYearId,
                ChangeActionEnum=cityPlanYear.ChangeActionEnum,
                VersionStatusEnum=cityPlanYear.VersionStatusEnum,
                ModifiedById=cityPlanYear.ModifiedById,
                ModificationDate=cityPlanYear.ModificationDate,
                DFGovId=cityPlanYear.DFGovId
            };

            return ctPlanYear;
        }


        public static CityPlanYearEditViewModel MapToCityPlanYearViewModel(this CityPlanYear cityPlanYear)
        {
            CityPlanYearEditViewModel viewModel = new CityPlanYearEditViewModel()
            {
                Id = 0,
                CityPlanId = cityPlanYear.CityPlanId,
                GovName = cityPlanYear.GovName,
                GovYear = cityPlanYear.GovYear,
                IsMapActive = cityPlanYear.IsMapActive,
                EnFileUrl = cityPlanYear.EnFileUrl,
                ArFileUrl = cityPlanYear.ArFileUrl,
                CreationDate = cityPlanYear.CreationDate,
                CreatedById = cityPlanYear.CreatedById,
                ApprovalDate = cityPlanYear.ApprovalDate,
                ApprovedById = cityPlanYear.ApprovedById,
                IsActive = cityPlanYear.IsActive,
                StatusId = cityPlanYear.StatusId,
                IsDeleted = cityPlanYear.IsDeleted,
                VersionStatusEnum = VersionStatusEnum.Draft,
                ChangeActionEnum = ChangeActionEnum.New,
                CityPlanYearId=cityPlanYear.Id,
                ModificationDate=cityPlanYear.ModificationDate,
                ModifiedById=cityPlanYear.ModifiedById,
                DFGovId = cityPlanYear.DFGovId

            };

            return viewModel;
        }

        public static CityPlanYearEditViewModel MapToCityPlanYearViewModel(this CityPlanYearVersion cityPlanYear)
        {
            CityPlanYearEditViewModel viewModel = new CityPlanYearEditViewModel()
            {
                Id = cityPlanYear.Id,
                CityPlanId = cityPlanYear.CityPlanVersionId,
                GovName = cityPlanYear.GovName,
                GovYear = cityPlanYear.GovYear,
                IsMapActive = cityPlanYear.IsMapActive,
                EnFileUrl = cityPlanYear.EnFileUrl,
                ArFileUrl = cityPlanYear.ArFileUrl,
                CreationDate = cityPlanYear.CreationDate,
                CreatedById = cityPlanYear.CreatedById,
                ApprovalDate = cityPlanYear.ApprovalDate,
                ApprovedById = cityPlanYear.ApprovedById,
                IsActive = cityPlanYear.IsActive,
                StatusId = cityPlanYear.StatusId,
                IsDeleted = cityPlanYear.IsDeleted,
                VersionStatusEnum = cityPlanYear.VersionStatusEnum,
                ChangeActionEnum = cityPlanYear.ChangeActionEnum,
                CityPlanYearId = cityPlanYear.CityPlanYearId,
                ModificationDate = cityPlanYear.ModificationDate,
                ModifiedById = cityPlanYear.ModifiedById,
                DFGovId = cityPlanYear.DFGovId

            };

            return viewModel;
        }


    }
}

