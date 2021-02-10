using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class MinistryTimeLineVersionMapper
    {
        public static MinistryTimeLineVersions MapToMinistryTimeLineVersionModel(this MinistriesViewModel model)
        {


            return new MinistryTimeLineVersions()
            {
                ApprovalDate = model.ApprovalDate,
                ApprovedById = model.ApprovedById,
                ArDescription = model.ArDescription,
                ArName = model.ArName,
                ChangeActionEnum = model.ChangeActionEnum,
                CreatedById = model.CreatedById,
                CreationDate = model.CreationDate,
                Email = model.Email,
                EnDescription = model.EnDescription,
                EnName = model.EnName,
                Facebook = model.Facebook,
                Id = model.Id,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                MinistryTimeLineId = model.MinistryTimeLineId,
                Order = model.Order,
                PeriodAr = model.PeriodAr,
                PeriodEn = model.PeriodEn,
                ProfileImageUrl = model.ImageURL,
                VersionStatusEnum = model.VersionStatusEnum,
                Twitter = model.Twitter,
                FormerMinistriesPageInfoVersionsId = model.FormerMinistriesPageInfoVersionsId
            };
        }

        public static MinistriesViewModel MapToMinistrViewModel(this MinistryTimeLineVersions model)
        {
            return new MinistriesViewModel()
            {
                ApprovalDate = model.ApprovalDate,
                ApprovedById = model.ApprovedById,
                ArDescription = model.ArDescription,
                ArName = model.ArName,
                ChangeActionEnum = model.ChangeActionEnum,
                CreatedById = model.CreatedById,
                CreationDate = model.CreationDate,
                Email = model.Email,
                EnDescription = model.EnDescription,
                EnName = model.EnName,
                Facebook = model.Facebook,
                Id = model.Id,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                MinistryTimeLineId = model.MinistryTimeLineId,
                Order = model.Order ?? 0,
                PeriodAr = model.PeriodAr,
                PeriodEn = model.PeriodEn,
                ImageURL = model.ProfileImageUrl,
                VersionStatusEnum = model.VersionStatusEnum,
                Twitter = model.Twitter,
                FormerMinistriesPageInfoVersionsId=model.FormerMinistriesPageInfoVersionsId
            };
        }
    }
}
