using MPMAR.Data.HomePageModels;
using MPMAR.Data.HomePageModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class MinistryVisssionMapper
    {
        public static MinistrtVisionViewModel MapToMinistryVissionVersionViewModel(this MinistryVissionVersion pgMinisty)
        {
            MinistrtVisionViewModel viewModel = new MinistrtVisionViewModel()
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
                MinistrtVisionId = pgMinisty.MinistryVissionId,
                ArDescription = pgMinisty.ArDescription,
                EnDescription = pgMinisty.EnDescription,
                ArTitle = pgMinisty.ArTitle,
                EnTitle = pgMinisty.EnTitle,
                Link = pgMinisty.Link,
                BackGroundImage = pgMinisty.BackGroundImage
            };

            return viewModel;
        }

        public static MinistryVissionVersion MapToMinistryVissionVersionModel(this MinistrtVisionViewModel pgMinisty)
        {
            MinistryVissionVersion viewModel = new MinistryVissionVersion()
            {
                Id = pgMinisty.MinistrtVisionId ?? pgMinisty.Id,
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
                MinistryVissionId = pgMinisty.MinistrtVisionId,
                ArDescription = pgMinisty.ArDescription,
                EnDescription = pgMinisty.EnDescription,
                ArTitle = pgMinisty.ArTitle,
                EnTitle = pgMinisty.EnTitle,
                Link = pgMinisty.Link,
                BackGroundImage = pgMinisty.BackGroundImage
            };

            return viewModel;
        }
    }
}
