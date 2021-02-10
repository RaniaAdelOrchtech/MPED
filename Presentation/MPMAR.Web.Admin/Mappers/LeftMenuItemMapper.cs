using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class LeftMenuItemMapper
    {
        //index
        //public static List<LeftMenuItemListViewModel> MapToLeftMenuItemListViewModel(this IEnumerable<LeftMenuItem> pageNewsType)
        //{
        //    return pageNewsType.Select(NewsType => new LeftMenuItemListViewModel
        //    {
        //        Id= NewsType.Id,
        //        EnName = NewsType.EnName,
        //        ArName= NewsType.ArName,
        //        IsDeleted=NewsType.IsDeleted
        //    }).ToList();
        //}

        //create

        //edit post
        //public static LeftMenuItem MapToLeftMenuItemVersion(this LeftMenuItemEditViewModel LeftMenuItemViewModel)
        //{
        //    LeftMenuItem LeftMenuItem = new LeftMenuItem();
        //    LeftMenuItem.Id = LeftMenuItemViewModel.NewsType.Id.Value;
        //    LeftMenuItem.EnName = LeftMenuItemViewModel.NewsType.EnName;
        //    LeftMenuItem.ArName = LeftMenuItemViewModel.NewsType.ArName;
        //    return LeftMenuItem;
        //}

        public static LeftMenuItem MapToLeftMenuItemViewModel(this LeftMenuItemViewModel LeftMenuItemCreateViewModel)
        {
            LeftMenuItem viewModel = new LeftMenuItem()
            {

                LeftMenuType = LeftMenuItemCreateViewModel.LeftMenuType,
                EnTitle = LeftMenuItemCreateViewModel.EnTitle,
                ArTitle = LeftMenuItemCreateViewModel.ArTitle,
                Link = LeftMenuItemCreateViewModel.Link,
                ImagePath = LeftMenuItemCreateViewModel.ImagePath,
                CreationDate = LeftMenuItemCreateViewModel.CreationDate,
                CreatedById = LeftMenuItemCreateViewModel.CreatedById,
                IsActive = LeftMenuItemCreateViewModel.IsActive
            };

            return viewModel;
        }

        //edit get
        public static LeftMenuItemViewModel MapToLeftMenuItemViewModelInEdit(this LeftMenuItem LeftMenuItem)
        {
            LeftMenuItemViewModel newsTypeViewModel = new LeftMenuItemViewModel
            {
                Id = LeftMenuItem.Id,
                LeftMenuType = LeftMenuItem.LeftMenuType,
                EnTitle = LeftMenuItem.EnTitle,
                ArTitle = LeftMenuItem.ArTitle,
                Link = LeftMenuItem.Link,
                ImagePath = LeftMenuItem.ImagePath,
                CreationDate = LeftMenuItem.CreationDate,
                CreatedById = LeftMenuItem.CreatedById,
                IsActive = LeftMenuItem.IsActive
            };

            return newsTypeViewModel;
        }

        public static LeftMenuItemVersions MapToLeftMenuItemVersionsModel(this LeftMenuItemViewModel LeftMenuItemCreateViewModel)
        {
            LeftMenuItemVersions viewModel = new LeftMenuItemVersions()
            {
                Id = LeftMenuItemCreateViewModel.LeftMenuItemId ?? LeftMenuItemCreateViewModel.Id,
                LeftMenuType = LeftMenuItemCreateViewModel.LeftMenuType,
                EnTitle = LeftMenuItemCreateViewModel.EnTitle,
                ArTitle = LeftMenuItemCreateViewModel.ArTitle,
                Link = LeftMenuItemCreateViewModel.Link,
                ImagePath = LeftMenuItemCreateViewModel.ImagePath,
                CreationDate = LeftMenuItemCreateViewModel.CreationDate,
                CreatedById = LeftMenuItemCreateViewModel.CreatedById,
                IsActive = LeftMenuItemCreateViewModel.IsActive,
                Order = LeftMenuItemCreateViewModel.Order,
                IsDeleted = LeftMenuItemCreateViewModel.IsDeleted,
                ApprovalDate = LeftMenuItemCreateViewModel.ApprovalDate,
                ApprovedById = LeftMenuItemCreateViewModel.ApprovedById,
                LeftMenuItemId = LeftMenuItemCreateViewModel.LeftMenuItemId,
                VersionStatusEnum = LeftMenuItemCreateViewModel.VersionStatusEnum,
                ChangeActionEnum = LeftMenuItemCreateViewModel.ChangeActionEnum
            };

            return viewModel;
        }

        //edit get
        public static LeftMenuItemViewModel MapToLeftMenuItemVersionsViewModel(this LeftMenuItemVersions LeftMenuItemCreateViewModel)
        {
            LeftMenuItemViewModel newsTypeViewModel = new LeftMenuItemViewModel
            {
                LeftMenuType = LeftMenuItemCreateViewModel.LeftMenuType,
                EnTitle = LeftMenuItemCreateViewModel.EnTitle,
                ArTitle = LeftMenuItemCreateViewModel.ArTitle,
                Link = LeftMenuItemCreateViewModel.Link,
                ImagePath = LeftMenuItemCreateViewModel.ImagePath,
                CreationDate = LeftMenuItemCreateViewModel.CreationDate,
                CreatedById = LeftMenuItemCreateViewModel.CreatedById,
                IsActive = LeftMenuItemCreateViewModel.IsActive,
                Id = LeftMenuItemCreateViewModel.Id,
                Order = LeftMenuItemCreateViewModel.Order,
                IsDeleted = LeftMenuItemCreateViewModel.IsDeleted,
                ApprovalDate = LeftMenuItemCreateViewModel.ApprovalDate,
                ApprovedById = LeftMenuItemCreateViewModel.ApprovedById,
                LeftMenuItemId = LeftMenuItemCreateViewModel.LeftMenuItemId,
                VersionStatusEnum = LeftMenuItemCreateViewModel.VersionStatusEnum,
                ChangeActionEnum = LeftMenuItemCreateViewModel.ChangeActionEnum
            };

            return newsTypeViewModel;
        }
    }
}


