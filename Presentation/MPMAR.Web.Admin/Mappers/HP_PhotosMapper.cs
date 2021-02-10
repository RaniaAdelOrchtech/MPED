using MPMAR.Data.HomePageModels;
using MPMAR.Data.HomePageModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class HP_PhotosMapper
    {
        public static HomePagePhoto MapToPhotosModel(this HP_PhotoViewModel viewModel)
        {
            return new HomePagePhoto()
            {
                Id = viewModel.Id,
                EnTitle = viewModel.EnTitle,
                ArDescription = viewModel.ArDescription,
                ArTitle = viewModel.ArTitle,
                EnDescription = viewModel.EnDescription,
                ImageUrl = viewModel.ImageUrl,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                Url = viewModel.Url
            };
        }

        public static HP_PhotoViewModel MapToPhotosViewModel(this HomePagePhoto viewModel)
        {
            return new HP_PhotoViewModel()
            {
                Id = viewModel.Id,
                EnTitle = viewModel.EnTitle,
                ArDescription = viewModel.ArDescription,
                ArTitle = viewModel.ArTitle,
                EnDescription = viewModel.EnDescription,
                ImageUrl = viewModel.ImageUrl,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                Url = viewModel.Url
            };
        }

        public static HP_PhotoViewModel MapToPhotoVersionViewModel(this HomePagePhotoVersions pgMinisty)
        {
            HP_PhotoViewModel viewModel = new HP_PhotoViewModel()
            {
                Id = pgMinisty.Id,
                IsActive = pgMinisty.IsActive,
                IsDeleted = pgMinisty.IsDeleted,
                VersionStatusEnum = pgMinisty.VersionStatusEnum,
                ChangeActionEnum = pgMinisty.ChangeActionEnum,
                CreationDate = pgMinisty.CreationDate,
                ModificationDate = pgMinisty.ModificationDate,
                ModifiedById = pgMinisty.ModifiedById,
                ApprovalDate = pgMinisty.ApprovalDate,
                ApprovedById = pgMinisty.ApprovedById,
                CreatedById = pgMinisty.CreatedById,
                ArDescription = pgMinisty.ArDescription,
                ArTitle = pgMinisty.ArTitle,
                EnDescription = pgMinisty.EnDescription,
                EnTitle = pgMinisty.EnTitle,
                ImageUrl = pgMinisty.ImageUrl,
                PhotoId = pgMinisty.HomePagePhotoId,
                Url = pgMinisty.Url
            };

            return viewModel;
        }

        public static HomePagePhotoVersions MapToPhotoVersionModel(this HP_PhotoViewModel pgMinisty)
        {
            HomePagePhotoVersions viewModel = new HomePagePhotoVersions()
            {
                Id = pgMinisty.PhotoId ?? pgMinisty.Id,
                IsActive = pgMinisty.IsActive,
                IsDeleted = pgMinisty.IsDeleted,
                VersionStatusEnum = pgMinisty.VersionStatusEnum,
                ChangeActionEnum = pgMinisty.ChangeActionEnum,
                CreationDate = pgMinisty.CreationDate,
                ModificationDate = pgMinisty.ModificationDate,
                ModifiedById = pgMinisty.ModifiedById,
                ApprovalDate = pgMinisty.ApprovalDate,
                ApprovedById = pgMinisty.ApprovedById,
                CreatedById = pgMinisty.CreatedById,
                ArDescription = pgMinisty.ArDescription,
                ArTitle = pgMinisty.ArTitle,
                EnDescription = pgMinisty.EnDescription,
                EnTitle = pgMinisty.EnTitle,
                ImageUrl = pgMinisty.ImageUrl,
                HomePagePhotoId = pgMinisty.PhotoId,
                Url = pgMinisty.Url
            };

            return viewModel;
        }
    }
}
