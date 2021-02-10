using MPMAR.Data.Enums;
using MPMAR.Data.HomePageModels;
using MPMAR.Data.HomePageModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class HP_PhotoSliderMapper
    {
        public static HomePagePhotoSlider MapToPhotoSliderModel(this HP_PhotoSliderViewModel viewModel)
        {
            return new HomePagePhotoSlider()
            {
                Id = viewModel.Id,
                ArDescription = viewModel.ArDescription,
                EnDescription = viewModel.EnDescription,
                ImageUrl = viewModel.ImageUrl,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                Url = viewModel.Url,
                ArTitle = viewModel.ArTitle,
                EnTitle = viewModel.EnTitle
            };
        }

        public static HomePagePhotoSliderVersion MapToPhotoSliderVersionModel(this HP_PhotoSliderViewModel viewModel)
        {
            return new HomePagePhotoSliderVersion()
            {
                Id = viewModel.Id,
                ArDescription = viewModel.ArDescription,
                EnDescription = viewModel.EnDescription,
                ImageUrl = viewModel.ImageUrl,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                Url = viewModel.Url,
                ArTitle = viewModel.ArTitle,
                EnTitle = viewModel.EnTitle,
                CreatedById = viewModel.CreatedById,
                ModificationDate = viewModel.ModificationDate,
                VersionStatusEnum = viewModel.VersionStatusEnum,
                ModifiedById = viewModel.ModifiedById,
                ApprovedById = viewModel.ApprovedById,
                ChangeActionEnum = viewModel.ChangeActionEnum,
                HomePagePhotoSliderId = viewModel.HomePagePhotoSliderId,
                ApprovalDate = viewModel.ApprovalDate,
                CreationDate = viewModel.CreationDate,


            };
        }

        public static HP_PhotoSliderViewModel MapToPhotoSliderViewModel(this HomePagePhotoSlider viewModel)
        {
            return new HP_PhotoSliderViewModel()
            {
                Id = 0,
                ArDescription = viewModel.ArDescription,
                EnDescription = viewModel.EnDescription,
                ImageUrl = viewModel.ImageUrl,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                Url = viewModel.Url,
                ArTitle = viewModel.ArTitle,
                EnTitle = viewModel.EnTitle,
                HomePagePhotoSliderId = viewModel.Id,
                ChangeActionEnum = ChangeActionEnum.New,
                VersionStatusEnum = VersionStatusEnum.Draft,
            };
        }

        public static HP_PhotoSliderViewModel MapToPhotoSliderViewModel(this HomePagePhotoSliderVersion viewModel)
        {
            return new HP_PhotoSliderViewModel()
            {
                Id = viewModel.Id,
                ArDescription = viewModel.ArDescription,
                EnDescription = viewModel.EnDescription,
                ImageUrl = viewModel.ImageUrl,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                Url = viewModel.Url,
                ArTitle = viewModel.ArTitle,
                EnTitle = viewModel.EnTitle,
                ChangeActionEnum = viewModel.ChangeActionEnum,
                VersionStatusEnum = viewModel.VersionStatusEnum,
                HomePagePhotoSliderId = viewModel.HomePagePhotoSliderId,
                ApprovedById = viewModel.ApprovedById,
                CreatedById = viewModel.CreatedById,
                ApprovalDate = viewModel.ApprovalDate,
                CreationDate = viewModel.CreationDate,
                ModifiedById = viewModel.ModifiedById,
                ModificationDate = viewModel.ModificationDate,

            };
        }
    }
}
