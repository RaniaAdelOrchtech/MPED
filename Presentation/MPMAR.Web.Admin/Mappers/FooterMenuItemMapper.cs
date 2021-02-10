using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class FooterMenuItemMapper
    {
        public static FooterMenuItemVersion MapToFooterItemVersionModel(this FooterMenuItemViewModel viewModel)
        {
            return new FooterMenuItemVersion()
            {
                Id = viewModel.Id,
                Link = viewModel.Link,
                EnTitle = viewModel.EnTitle,
                Order = viewModel.Order,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                EnColumnPostion = viewModel.EnColumnPostion,
                FooterMenuTitleId = viewModel.FooterMenuTitleId,
                FooterMenuItemId = viewModel.FooterMenuItemId,
                ApprovalDate = viewModel.ApprovalDate,
                ApprovedById = viewModel.ApprovedById,
                ChangeActionEnum = viewModel.ChangeActionEnum,
                CreatedById = viewModel.CreatedById,
                CreationDate = viewModel.CreationDate,
                VersionStatusEnum = viewModel.VersionStatusEnum,
                ArColumnPostion=viewModel.ArColumnPostion,
                ArTitle=viewModel.ArTitle,
                
            };
        }

        public static FooterMenuItemViewModel MapToFooterItemViewModel(this FooterMenuItem viewModel)
        {
            return new FooterMenuItemViewModel()
            {
                Id = 0,
                Link = viewModel.Link,
                ArTitle = viewModel.ArTitle,
                Order = viewModel.Order,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                FooterMenuItemId = viewModel.Id,
                ChangeActionEnum = ChangeActionEnum.New,
                VersionStatusEnum = VersionStatusEnum.Draft,
                ApprovedById = viewModel.ApprovedById,
                CreatedById = viewModel.CreatedById,
                CreationDate = viewModel.CreationDate,
                ArColumnPostion = viewModel.ArColumnPostion,
                FooterMenuTitleId = viewModel.FooterMenuTitleId,
                ApprovalDate = viewModel.ApprovalDate,
                EnTitle=viewModel.EnTitle,
                EnColumnPostion=viewModel.EnColumnPostion,
                
            };
        }

        public static FooterMenuItemViewModel MapToFooterItemViewModel(this FooterMenuItemVersion viewModel)
        {
            return new FooterMenuItemViewModel()
            {
                Id = viewModel.Id,
                Link = viewModel.Link,
                EnColumnPostion = viewModel.EnColumnPostion,
                Order = viewModel.Order,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                FooterMenuItemId = viewModel.FooterMenuItemId,
                ChangeActionEnum = viewModel.ChangeActionEnum,
                VersionStatusEnum = viewModel.VersionStatusEnum,
                ApprovalDate = viewModel.ApprovalDate,
                ApprovedById = viewModel.ApprovedById,
                CreatedById = viewModel.CreatedById,
                CreationDate = viewModel.CreationDate,
                EnTitle = viewModel.EnTitle,
                FooterMenuTitleId = viewModel.FooterMenuTitleId,
                ArTitle=viewModel.ArTitle,
                ArColumnPostion=viewModel.ArColumnPostion,
            };
        }
    }
}
