using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class MinistriesMapper
    {
        public static MinistryTimeLine MapToMinistrModel(this MinistriesViewModel model)
        {
            return new MinistryTimeLine()
            {
                ApprovalDate = model.ApprovalDate,
                ApprovedById = model.ApprovedById,
                ArDescription = model.ArDescription,
                ArName = model.ArName,
                CreatedById = model.CreatedById,
                CreationDate = model.CreationDate,
                Email = model.Email,
                EnDescription = model.EnDescription,
                EnName = model.EnName,
                Facebook = model.Facebook,
                Id = model.Id,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                PeriodAr = model.PeriodAr,
                PeriodEn = model.PeriodEn,
                Twitter = model.Twitter,
            };
        }

        public static MinistriesViewModel MapToMinistrViewModel(this MinistryTimeLine model)
        {
            return new MinistriesViewModel()
            {
                ApprovalDate = model.ApprovalDate,
                ApprovedById = model.ApprovedById,
                ArDescription = model.ArDescription,
                ArName = model.ArName,
                ChangeActionEnum = ChangeActionEnum.Update,
                CreatedById = model.CreatedById,
                CreationDate = model.CreationDate,
                Email = model.Email,
                EnDescription = model.EnDescription,
                EnName = model.EnName,
                Facebook = model.Facebook,
                Id = model.Id,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                MinistryTimeLineId = model.Id,
                Order = model.Order ?? 0,
                PeriodAr = model.PeriodAr,
                PeriodEn = model.PeriodEn,
                ImageURL = model.ProfileImageUrl,
                VersionStatusEnum =VersionStatusEnum.Draft,
                Twitter = model.Twitter,

            };
        }
    }
}
