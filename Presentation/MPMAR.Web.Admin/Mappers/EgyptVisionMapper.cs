using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class EgyptVisionMapper
    {
        public static List<EgyptVisionListViewModel> MapToEgyptVisionViewModel(this IEnumerable<EgyptVision> EgyptVision)
        {
            return EgyptVision.Select(pgMinisty => new EgyptVisionListViewModel
            {
                Id = pgMinisty.Id,
                EnEgyptVisionName = pgMinisty.EnEgyptVisionName,
                ArEgyptVisionName = pgMinisty.ArEgyptVisionName,
                EnEgyptVisionSmallDesc = pgMinisty.EnEgyptVisionSmallDesc,
                ArEgyptVisionSmallDesc = pgMinisty.ArEgyptVisionSmallDesc,
                BgColor = pgMinisty.BgColor,
                LineColor = pgMinisty.LineColor,
                IsActive = pgMinisty.IsActive,
                IsDeleted = pgMinisty.IsDeleted,
                EnImagePath = pgMinisty.EnImagePath,
                ArImagePath = pgMinisty.ArImagePath,
                ImagePositionIsRight = pgMinisty.ImagePositionIsRight,
                SeoTitleEN = pgMinisty.SeoTitleEN,
                SeoTitleAR = pgMinisty.SeoTitleAR,
                SeoDescriptionEN = pgMinisty.SeoDescriptionEN,
                SeoDescriptionAR = pgMinisty.SeoDescriptionAR,
                SeoOgTitleEN = pgMinisty.SeoOgTitleEN,
                SeoOgTitleAR = pgMinisty.SeoOgTitleAR,
                SeoTwitterCardEN = pgMinisty.SeoTwitterCardEN,
                SeoTwitterCardAR = pgMinisty.SeoTwitterCardAR,



            }).ToList();
        }

        public static EgyptVision MapToEgyptVision(this EgyptVisionEditViewModel pgMinisty)
        {
            EgyptVision pageSectionVersion = new EgyptVision();
            pageSectionVersion.EnEgyptVisionName = pgMinisty.EnEgyptVisionName;
            pageSectionVersion.ArEgyptVisionName = pgMinisty.ArEgyptVisionName;
            pageSectionVersion.EnEgyptVisionSmallDesc = pgMinisty.EnEgyptVisionSmallDesc;
            pageSectionVersion.ArEgyptVisionSmallDesc = pgMinisty.ArEgyptVisionSmallDesc;
            pageSectionVersion.EnEgyptVisionDesc = pgMinisty.EnEgyptVisionDesc;
            pageSectionVersion.ArEgyptVisionDesc = pgMinisty.ArEgyptVisionDesc;
            pageSectionVersion.IsActive = pgMinisty.IsActive;
            pageSectionVersion.IsDeleted = pgMinisty.IsDeleted;
            pageSectionVersion.EnImagePath = pgMinisty.EnImagePath;
            pageSectionVersion.ArImagePath = pgMinisty.ArImagePath;
            pageSectionVersion.Order = pgMinisty.Order;
            pageSectionVersion.ImagePositionIsRight = pgMinisty.ImagePositionIsRight;
            pageSectionVersion.BgColor = pgMinisty.BgColor;
            pageSectionVersion.LineColor = pgMinisty.LineColor;

            pageSectionVersion.SeoTitleEN = pgMinisty.SeoTitleEN;
            pageSectionVersion.SeoTitleAR = pgMinisty.SeoTitleAR;
            pageSectionVersion.SeoDescriptionEN = pgMinisty.SeoDescriptionEN;
            pageSectionVersion.SeoDescriptionAR = pgMinisty.SeoDescriptionAR;
            pageSectionVersion.SeoOgTitleEN = pgMinisty.SeoOgTitleEN;
            pageSectionVersion.SeoOgTitleAR = pgMinisty.SeoOgTitleAR;
            pageSectionVersion.SeoTwitterCardEN = pgMinisty.SeoTwitterCardEN;
            pageSectionVersion.SeoTwitterCardAR = pgMinisty.SeoTwitterCardAR;


            if (pgMinisty.Id > 0)
                pageSectionVersion.Id = pgMinisty.Id;
            return pageSectionVersion;
        }

        public static EgyptVisionEditViewModel MapToSctionCardViewModel(this EgyptVision pgMinisty)
        {
            EgyptVisionEditViewModel viewModel = new EgyptVisionEditViewModel()
            {
                Id = pgMinisty.Id,
                EnEgyptVisionName = pgMinisty.EnEgyptVisionName,
                ArEgyptVisionName = pgMinisty.ArEgyptVisionName,
                EnEgyptVisionSmallDesc = pgMinisty.EnEgyptVisionSmallDesc,
                ArEgyptVisionSmallDesc = pgMinisty.ArEgyptVisionSmallDesc,
                EnEgyptVisionDesc = pgMinisty.EnEgyptVisionDesc,
                ArEgyptVisionDesc = pgMinisty.ArEgyptVisionDesc,
                IsActive = pgMinisty.IsActive,
                IsDeleted = pgMinisty.IsDeleted,
                EnImagePath = pgMinisty.EnImagePath,
                ArImagePath = pgMinisty.ArImagePath,
                SeoTitleEN = pgMinisty.SeoTitleEN,
                SeoTitleAR = pgMinisty.SeoTitleAR,
                SeoDescriptionEN = pgMinisty.SeoDescriptionEN,
                SeoDescriptionAR = pgMinisty.SeoDescriptionAR,
                SeoOgTitleEN = pgMinisty.SeoOgTitleEN,
                SeoOgTitleAR = pgMinisty.SeoOgTitleAR,
                SeoTwitterCardEN = pgMinisty.SeoTwitterCardEN,
                SeoTwitterCardAR = pgMinisty.SeoTwitterCardAR,
                BgColor = pgMinisty.BgColor,
                LineColor = pgMinisty.LineColor,
                Order = pgMinisty.Order,
                ImagePositionIsRight = pgMinisty.ImagePositionIsRight,
                ApprovalDate = pgMinisty.ApprovalDate,
                ApprovedById=pgMinisty.ApprovedById,
                CreatedById=pgMinisty.CreatedById,
                CreationDate = pgMinisty.CreationDate,
                ModificationDate=pgMinisty.ModificationDate,
                ModifiedById = pgMinisty.ModifiedById,
                PageRouteVersionId = pgMinisty.PageRouteVersionId,
                StatusId = pgMinisty.StatusId
            };

            return viewModel;
        }

        public static EgyptVisionVersionEditViewModel MapToEgyptVisionVersionViewModel(this EgyptVisionVersion pgMinisty)
        {
            EgyptVisionVersionEditViewModel viewModel = new EgyptVisionVersionEditViewModel()
            {
                Id = pgMinisty.Id,
                EnEgyptVisionName = pgMinisty.EnEgyptVisionName,
                ArEgyptVisionName = pgMinisty.ArEgyptVisionName,
                EnEgyptVisionSmallDesc = pgMinisty.EnEgyptVisionSmallDesc,
                ArEgyptVisionSmallDesc = pgMinisty.ArEgyptVisionSmallDesc,
                EnEgyptVisionDesc = pgMinisty.EnEgyptVisionDesc,
                ArEgyptVisionDesc = pgMinisty.ArEgyptVisionDesc,
                IsActive = pgMinisty.IsActive,
                IsDeleted = pgMinisty.IsDeleted,
                EnImagePath = pgMinisty.EnImagePath,
                ArImagePath = pgMinisty.ArImagePath,
                SeoTitleEN = pgMinisty.SeoTitleEN,
                SeoTitleAR = pgMinisty.SeoTitleAR,
                SeoDescriptionEN = pgMinisty.SeoDescriptionEN,
                SeoDescriptionAR = pgMinisty.SeoDescriptionAR,
                SeoOgTitleEN = pgMinisty.SeoOgTitleEN,
                SeoOgTitleAR = pgMinisty.SeoOgTitleAR,
                SeoTwitterCardEN = pgMinisty.SeoTwitterCardEN,
                SeoTwitterCardAR = pgMinisty.SeoTwitterCardAR,
                BgColor = pgMinisty.BgColor,
                LineColor = pgMinisty.LineColor,
                Order = pgMinisty.Order,
                ImagePositionIsRight = pgMinisty.ImagePositionIsRight,
                VersionStatusEnum = pgMinisty.VersionStatusEnum,
                ChangeActionEnum = pgMinisty.ChangeActionEnum,
                EgyptVisionId = pgMinisty.EgyptVisionId,
                CreationDate = pgMinisty.CreationDate,
                StatusId = pgMinisty.StatusId,
                ModificationDate = pgMinisty.ModificationDate,
                ModifiedById = pgMinisty.ModifiedById,
                ApprovalDate = pgMinisty.ApprovalDate,
                ApprovedById = pgMinisty.ApprovedById,
                CreatedById = pgMinisty.CreatedById,
                PageRouteVersionId = pgMinisty.PageRouteVersionId
            };

            return viewModel;
        }

        public static EgyptVisionVersion MapToEgyptVisionVersionModel(this EgyptVisionVersionEditViewModel pgMinisty)
        {
            EgyptVisionVersion viewModel = new EgyptVisionVersion()
            {
                Id = pgMinisty.EgyptVisionId??pgMinisty.Id,
                EnEgyptVisionName = pgMinisty.EnEgyptVisionName,
                ArEgyptVisionName = pgMinisty.ArEgyptVisionName,
                EnEgyptVisionSmallDesc = pgMinisty.EnEgyptVisionSmallDesc,
                ArEgyptVisionSmallDesc = pgMinisty.ArEgyptVisionSmallDesc,
                EnEgyptVisionDesc = pgMinisty.EnEgyptVisionDesc,
                ArEgyptVisionDesc = pgMinisty.ArEgyptVisionDesc,
                IsActive = pgMinisty.IsActive,
                IsDeleted = pgMinisty.IsDeleted,
                EnImagePath = pgMinisty.EnImagePath,
                ArImagePath = pgMinisty.ArImagePath,
                SeoTitleEN = pgMinisty.SeoTitleEN,
                SeoTitleAR = pgMinisty.SeoTitleAR,
                SeoDescriptionEN = pgMinisty.SeoDescriptionEN,
                SeoDescriptionAR = pgMinisty.SeoDescriptionAR,
                SeoOgTitleEN = pgMinisty.SeoOgTitleEN,
                SeoOgTitleAR = pgMinisty.SeoOgTitleAR,
                SeoTwitterCardEN = pgMinisty.SeoTwitterCardEN,
                SeoTwitterCardAR = pgMinisty.SeoTwitterCardAR,
                BgColor = pgMinisty.BgColor,
                LineColor = pgMinisty.LineColor,
                Order = pgMinisty.Order,
                ImagePositionIsRight = pgMinisty.ImagePositionIsRight,
                VersionStatusEnum = pgMinisty.VersionStatusEnum,
                ChangeActionEnum = pgMinisty.ChangeActionEnum,
                EgyptVisionId = pgMinisty.EgyptVisionId,
                StatusId = pgMinisty.StatusId,
                ApprovalDate = pgMinisty.ApprovalDate,
                ApprovedById = pgMinisty.ApprovedById,
                CreatedById = pgMinisty.CreatedById,
                ModificationDate = pgMinisty.ModificationDate,
                CreationDate = pgMinisty.CreationDate,
                ModifiedById = pgMinisty.ModifiedById,
                PageRouteVersionId = pgMinisty.PageRouteVersionId
            };

            return viewModel;
        }
    }
}

