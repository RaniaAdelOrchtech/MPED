using MPMAR.Data.HomePageModels;
using MPMAR.Data.HomePageModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class LogoLinkMapper
    {
        public static HomePageLogoLink MapToLogoLinkModel(this HP_LogoLinkViewModel viewModel)
        {
            return new HomePageLogoLink()
            {
                Id = viewModel.Id,
                EnTitle = viewModel.EnTitle,
                ArTitle = viewModel.ArTitle,
                ImageUrl = viewModel.ImageUrl,
                Url = viewModel.Url
            };
        }

        public static HP_LogoLinkViewModel MapToLogoLinkViewModel(this HomePageLogoLink viewModel)
        {
            return new HP_LogoLinkViewModel()
            {
                Id = viewModel.Id,
                EnTitle = viewModel.EnTitle,
                ArTitle = viewModel.ArTitle,
                ImageUrl = viewModel.ImageUrl,
                Url = viewModel.Url
            };
        }

        public static HP_LogoLinkViewModel MapToLogoLinkVersionViewModel(this HomePageLogoLinkVersions pgMinisty)
        {
            HP_LogoLinkViewModel viewModel = new HP_LogoLinkViewModel()
            {
                Id = pgMinisty.Id,
                VersionStatusEnum = pgMinisty.VersionStatusEnum,
                ChangeActionEnum = pgMinisty.ChangeActionEnum,
                CreationDate = pgMinisty.CreationDate,
                ModificationDate = pgMinisty.ModificationDate,
                ModifiedById = pgMinisty.ModifiedById,
                ApprovalDate = pgMinisty.ApprovalDate,
                ApprovedById = pgMinisty.ApprovedById,
                CreatedById = pgMinisty.CreatedById,
                ArTitle = pgMinisty.ArTitle,
                EnTitle = pgMinisty.EnTitle,
                ImageUrl = pgMinisty.ImageUrl,
                LogoLinkId = pgMinisty.HomePageLogoLinkId,
                Url = pgMinisty.Url
            };

            return viewModel;
        }

        public static HomePageLogoLinkVersions MapToLogoLinkVersionModel(this HP_LogoLinkViewModel pgMinisty)
        {
            HomePageLogoLinkVersions viewModel = new HomePageLogoLinkVersions()
            {
                Id = pgMinisty.LogoLinkId ?? pgMinisty.Id,
                VersionStatusEnum = pgMinisty.VersionStatusEnum,
                ChangeActionEnum = pgMinisty.ChangeActionEnum,
                CreationDate = pgMinisty.CreationDate,
                ModificationDate = pgMinisty.ModificationDate,
                ModifiedById = pgMinisty.ModifiedById,
                ApprovalDate = pgMinisty.ApprovalDate,
                ApprovedById = pgMinisty.ApprovedById,
                CreatedById = pgMinisty.CreatedById,
                ArTitle = pgMinisty.ArTitle,
                EnTitle = pgMinisty.EnTitle,
                ImageUrl = pgMinisty.ImageUrl,
                HomePageLogoLinkId = pgMinisty.LogoLinkId,
                Url = pgMinisty.Url
            };

            return viewModel;
        }
    }
}
