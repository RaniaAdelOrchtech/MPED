using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class NavItemVersionMapper
    {
        public static NavItemVersion MapToNavVersionModel(this NavItemVersionViewModel viewModel)
        {
            return new NavItemVersion()
            {
                Id = viewModel.Id,
                NavItemId = viewModel.NavItemId,
                ArName = viewModel.ArName,
                Order = viewModel.Order,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                EnName = viewModel.EnName,
                ParentNavItemId = viewModel.ParentNavItemId,
                ApprovalDate = viewModel.ApprovalDate,
                ApprovedById = viewModel.ApprovedById,
                ChangeActionEnum = viewModel.ChangeActionEnum,
                CreatedById = viewModel.CreatedById,
                CreationDate = viewModel.CreationDate,
                VersionStatusEnum = viewModel.VersionStatusEnum,
            };
        }

        public static NavItemVersionViewModel MapToNavViewModel(this NavItem viewModel)
        {
            return new NavItemVersionViewModel()
            {
                Id = 0,
                ParentNavItemId = viewModel.ParentNavItemId,
                Order = viewModel.Order,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                NavItemId = viewModel.Id,
                ChangeActionEnum = ChangeActionEnum.New,
                VersionStatusEnum = VersionStatusEnum.Draft,
                ApprovedById = viewModel.ApprovedById,
                CreatedById = viewModel.CreatedById,
                CreationDate = viewModel.CreationDate,
                EnName = viewModel.EnName,
                ArName = viewModel.ArName,
                ApprovalDate = viewModel.ApprovalDate,
            };
        }

        public static NavItemVersionViewModel MapToNavViewModel(this NavItemVersion viewModel)
        {
            return new NavItemVersionViewModel()
            {
                Id = viewModel.Id,
                ArName = viewModel.ArName,
                ParentNavItemId = viewModel.ParentNavItemId,
                Order = viewModel.Order,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                NavItemId = viewModel.NavItemId,
                ChangeActionEnum = viewModel.ChangeActionEnum,
                VersionStatusEnum = viewModel.VersionStatusEnum,
                ApprovalDate = viewModel.ApprovalDate,
                ApprovedById = viewModel.ApprovedById,
                CreatedById = viewModel.CreatedById,
                CreationDate = viewModel.CreationDate,
                EnName = viewModel.EnName,
                
            };
        }
    }
}
