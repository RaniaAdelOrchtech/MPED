using MPMAR.Data.HomePageModels;
using MPMAR.Data.HomePageModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class HP_VideoMapper
    {
        public static HP_VideoViewModel MapToVideoVersionViewModel(this HomePageVideoVersions pgMinisty)
        {
            HP_VideoViewModel viewModel = new HP_VideoViewModel()
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
                VideoUrl = pgMinisty.VideoUrl,
                VideoId = pgMinisty.HomePageVideoId
            };

            return viewModel;
        }

        public static HomePageVideoVersions MapToVideoVersionModel(this HP_VideoViewModel pgMinisty)
        {
            HomePageVideoVersions viewModel = new HomePageVideoVersions()
            {
                Id = pgMinisty.VideoId ?? pgMinisty.Id,
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
                VideoUrl = pgMinisty.VideoUrl,
                HomePageVideoId = pgMinisty.VideoId
            };

            return viewModel;
        }
    }
}
