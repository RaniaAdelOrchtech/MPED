using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class FooterMenuTitleMapper
    {
        public static FooterMenuTitleVersions MapToFooterMenuItemVersionModel(this FooterMenuTitleViewModel LeftMenuItemCreateViewModel)
        {
            FooterMenuTitleVersions viewModel = new FooterMenuTitleVersions()
            {
                Id = LeftMenuItemCreateViewModel.FooterMenuTitleId ?? LeftMenuItemCreateViewModel.Id,
                EnTitle = LeftMenuItemCreateViewModel.EnTitle,
                ArTitle = LeftMenuItemCreateViewModel.ArTitle,
                CreationDate = LeftMenuItemCreateViewModel.CreationDate,
                CreatedById = LeftMenuItemCreateViewModel.CreatedById,
                IsActive = LeftMenuItemCreateViewModel.IsActive,
                Order = LeftMenuItemCreateViewModel.Order,
                IsDeleted = LeftMenuItemCreateViewModel.IsDeleted,
                ApprovalDate = LeftMenuItemCreateViewModel.ApprovalDate,
                ApprovedById = LeftMenuItemCreateViewModel.ApprovedById,
                FooterMenuTitleId = LeftMenuItemCreateViewModel.FooterMenuTitleId,
                VersionStatusEnum = LeftMenuItemCreateViewModel.VersionStatusEnum,
                ChangeActionEnum = LeftMenuItemCreateViewModel.ChangeActionEnum,
            };

            return viewModel;
        }

        //edit get
        public static FooterMenuTitleViewModel MapToLeftMenuItemVersionsViewModel(this FooterMenuTitleVersions LeftMenuItemCreateViewModel)
        {
            FooterMenuTitleViewModel newsTypeViewModel = new FooterMenuTitleViewModel
            {
                Id = LeftMenuItemCreateViewModel.Id,
                EnTitle = LeftMenuItemCreateViewModel.EnTitle,
                ArTitle = LeftMenuItemCreateViewModel.ArTitle,
                CreationDate = LeftMenuItemCreateViewModel.CreationDate,
                CreatedById = LeftMenuItemCreateViewModel.CreatedById,
                IsActive = LeftMenuItemCreateViewModel.IsActive,
                Order = LeftMenuItemCreateViewModel.Order,
                IsDeleted = LeftMenuItemCreateViewModel.IsDeleted,
                ApprovalDate = LeftMenuItemCreateViewModel.ApprovalDate,
                ApprovedById = LeftMenuItemCreateViewModel.ApprovedById,
                FooterMenuTitleId = LeftMenuItemCreateViewModel.FooterMenuTitleId,
                VersionStatusEnum = LeftMenuItemCreateViewModel.VersionStatusEnum,
                ChangeActionEnum = LeftMenuItemCreateViewModel.ChangeActionEnum,
            };

            return newsTypeViewModel;
        }
    }
}
