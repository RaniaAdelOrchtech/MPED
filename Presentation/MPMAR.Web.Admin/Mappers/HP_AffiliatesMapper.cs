using MPMAR.Data.Enums;
using MPMAR.Data.HomePageModels;
using MPMAR.Data.HomePageModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class HP_AffiliatesMapper
    {
        public static HomePageAffiliates MapToAffiliatesModel(this HP_AffiliatesViewModel viewModel)
        {
            return new HomePageAffiliates()
            {
                Id = viewModel.Id,
                ArDescription = viewModel.ArDescription,
                EnDescription = viewModel.EnDescription,
                ImageUrl = viewModel.ImageUrl,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                Url = viewModel.Url,
                Type = viewModel.Type
            };
        }

        public static HomePageAffiliatesVersions MapToAffiliatesVersionModel(this HP_AffiliatesViewModel viewModel)
        {
            return new HomePageAffiliatesVersions()
            {
                Id = viewModel.Id,
                ArDescription = viewModel.ArDescription,
                EnDescription = viewModel.EnDescription,
                ImageUrl = viewModel.ImageUrl,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                Url = viewModel.Url,
                Type = viewModel.Type,
                HomePageAffiliatesId = viewModel.HomePageAffiliatesId,
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

        public static HP_AffiliatesViewModel MapToAffiliatesViewModel(this HomePageAffiliates viewModel)
        {
            return new HP_AffiliatesViewModel()
            {
                Id = 0,
                ArDescription = viewModel.ArDescription,
                EnDescription = viewModel.EnDescription,
                ImageUrl = viewModel.ImageUrl,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                Url = viewModel.Url,
                Type = viewModel.Type,
                HomePageAffiliatesId = viewModel.Id,
                ChangeActionEnum = ChangeActionEnum.New,
                VersionStatusEnum = VersionStatusEnum.Draft,
            };
        }

        public static HP_AffiliatesViewModel MapToAffiliatesViewModel(this HomePageAffiliatesVersions viewModel)
        {
            return new HP_AffiliatesViewModel()
            {
                Id = viewModel.Id,
                ArDescription = viewModel.ArDescription,
                EnDescription = viewModel.EnDescription,
                ImageUrl = viewModel.ImageUrl,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                Url = viewModel.Url,
                Type = viewModel.Type,
                HomePageAffiliatesId = viewModel.HomePageAffiliatesId,
                ChangeActionEnum = viewModel.ChangeActionEnum,
                VersionStatusEnum = viewModel.VersionStatusEnum,
                ApprovalDate=viewModel.ApprovalDate,
                ApprovedById= viewModel.ApprovedById,
                CreatedById= viewModel.CreatedById,
                CreationDate= viewModel.CreationDate,
                ModificationDate= viewModel.ModificationDate,
                ModifiedById= viewModel.ModifiedById,
                
            };
        }
    }
}
