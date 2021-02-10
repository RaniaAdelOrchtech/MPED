using MPMAR.Data.HomePageModels;
using MPMAR.Data.HomePageModels.ViewModels;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class CitizenPlanHPMapper
    {
        public static CitizenPlanViewModel MapToCitizenPlanVersionViewModel(this CitizenPlanVersions pgMinisty)
        {
            CitizenPlanViewModel viewModel = new CitizenPlanViewModel()
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
                CitizenPlanId = pgMinisty.CitizenPlanId,
                ArDescription = pgMinisty.ArDescription,
                EnDescription = pgMinisty.EnDescription,
                ArTitle = pgMinisty.ArTitle,
                EnTitle = pgMinisty.EnTitle,
                Link = pgMinisty.Link,
                Image = pgMinisty.Image,
                EnImage = pgMinisty.EnImage,
                ArMainTitle = pgMinisty.ArMainTitle,
                EnMainTitle = pgMinisty.EnMainTitle,
            };

            return viewModel;
        }

        public static CitizenPlanVersions MapToCitizenPlanVersionModel(this CitizenPlanViewModel pgMinisty)
        {
            CitizenPlanVersions viewModel = new CitizenPlanVersions()
            {
                Id = pgMinisty.CitizenPlanId ?? pgMinisty.Id,
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
                CitizenPlanId = pgMinisty.CitizenPlanId,
                ArDescription = pgMinisty.ArDescription,
                EnDescription = pgMinisty.EnDescription,
                ArTitle = pgMinisty.ArTitle,
                EnTitle = pgMinisty.EnTitle,
                Link = pgMinisty.Link,
                Image = pgMinisty.Image,
                EnImage = pgMinisty.EnImage,
                ArMainTitle = pgMinisty.ArMainTitle,
                EnMainTitle = pgMinisty.EnMainTitle
            };

            return viewModel;
        }
    }
}
