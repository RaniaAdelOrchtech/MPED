using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class SocialMediaMapper
    {
        public static SocialMediaVersion MapToSocialVersionModel(this SocialMediaViewModel viewModel)
        {
            return new SocialMediaVersion()
            {
                Id = viewModel.Id,
                Link = viewModel.Link,
                SocialMediaName = viewModel.SocialMediaName,
                Order = viewModel.Order,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                SocialMediaId = viewModel.SocialMediaId,
                ModificationDate = viewModel.ModificationDate,
                ModifiedById = viewModel.ModifiedById,
                ApprovalDate = viewModel.ApprovalDate,
                ApprovedById = viewModel.ApprovedById,
                ChangeActionEnum = viewModel.ChangeActionEnum,
                CreatedById = viewModel.CreatedById,
                CreationDate = viewModel.CreationDate,
                VersionStatusEnum = viewModel.VersionStatusEnum,
            };
        }

        public static SocialMediaViewModel MapToSocialViewModel(this SocialMedia viewModel)
        {
            return new SocialMediaViewModel()
            {
                Id = 0,
                Link = viewModel.Link,
                SocialMediaName = viewModel.SocialMediaName,
                Order = viewModel.Order,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                SocialMediaId = viewModel.Id,
                ChangeActionEnum = ChangeActionEnum.New,
                VersionStatusEnum = VersionStatusEnum.Draft,
                ApprovedById=viewModel.ApprovedById,
                CreatedById=viewModel.CreatedById,
                CreationDate=viewModel.CreationDate,
                ModificationDate=viewModel.ModificationDate,
                ModifiedById=viewModel.ModifiedById,
                ApprovalDate=viewModel.ApprovalDate
            };
        }

        public static SocialMediaViewModel MapToSocialViewModel(this SocialMediaVersion viewModel)
        {
            return new SocialMediaViewModel()
            {
                Id = viewModel.Id,
                Link = viewModel.Link,
                SocialMediaName = viewModel.SocialMediaName,
                Order = viewModel.Order,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                SocialMediaId = viewModel.SocialMediaId,
                ChangeActionEnum = viewModel.ChangeActionEnum,
                VersionStatusEnum = viewModel.VersionStatusEnum,
                ApprovalDate = viewModel.ApprovalDate,
                ApprovedById = viewModel.ApprovedById,
                CreatedById = viewModel.CreatedById,
                CreationDate = viewModel.CreationDate,
                ModificationDate = viewModel.ModificationDate,
                ModifiedById = viewModel.ModifiedById,
                
            };
        }
    }
}
