using MPMAR.Data.HomePageModels;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class MonitoringMapper
    {
        public static Monitoring MapToMonitoringModel(this MonitoringViewModel model)
        {
            return new Monitoring()
            {
                Id = model.Id,
                ArTitle2 = model.ArTitle2,
                ArDescription1 = model.ArDescription1,
                ArDescription2 = model.ArDescription2,
                ArMainTitle = model.ArMainTitle,
                EnDescription1 = model.EnDescription1,
                EnDescription2 = model.EnDescription2,
                EnMainTitle = model.EnMainTitle,
                EnTitle2 = model.EnTitle2,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                Image1 = model.Image1,
                Link1 = model.Link1,
                Link2 = model.Link2,
                BackGroundImage=model.BackGroundImage
              
            };

        }

        public static MonitoringViewModel MapToMonitoringViewModel(this Monitoring model)
        {
            return new MonitoringViewModel()
            {
                Id = model.Id,
                ArTitle2 = model.ArTitle2,
                ArDescription1 = model.ArDescription1,
                ArDescription2 = model.ArDescription2,
                ArMainTitle = model.ArMainTitle,
                EnDescription1 = model.EnDescription1,
                EnDescription2 = model.EnDescription2,
                EnMainTitle = model.EnMainTitle,
                EnTitle2 = model.EnTitle2,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                Image1 = model.Image1,
                Link1 = model.Link1,
                Link2 = model.Link2,
                BackGroundImage = model.BackGroundImage

            };

        }

        public static MonitoringViewModel MapToMonitringVersionViewModel(this MonitoringVersions pgMinisty)
        {
            MonitoringViewModel viewModel = new MonitoringViewModel()
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
                MonitringId = pgMinisty.MonitoringId,
                ArMainTitle = pgMinisty.ArMainTitle,
                EnMainTitle = pgMinisty.EnMainTitle,
                EnDescription1 = pgMinisty.EnDescription1,
                ArDescription1 = pgMinisty.ArDescription1,
                EnDescription2 = pgMinisty.EnDescription2,
                ArDescription2 = pgMinisty.ArDescription2,
                BackGroundImage = pgMinisty.BackGroundImage,
                ArTitle2 = pgMinisty.ArTitle2,
                EnTitle2 = pgMinisty.EnTitle2,
                Link1 = pgMinisty.Link1,
                Link2 = pgMinisty.Link2,
                Image1 = pgMinisty.Image1,
            };

            return viewModel;
        }

        public static MonitoringVersions MapToMonitringVersionModel(this MonitoringViewModel pgMinisty)
        {
            MonitoringVersions viewModel = new MonitoringVersions()
            {
                Id = pgMinisty.MonitringId ?? pgMinisty.Id,
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
                ArMainTitle = pgMinisty.ArMainTitle,
                EnMainTitle = pgMinisty.EnMainTitle,
                EnDescription1 = pgMinisty.EnDescription1,
                ArDescription1 = pgMinisty.ArDescription1,
                EnDescription2 = pgMinisty.EnDescription2,
                ArDescription2 = pgMinisty.ArDescription2,
                ArTitle2 = pgMinisty.ArTitle2,
                EnTitle2 = pgMinisty.EnTitle2,
                Link1 = pgMinisty.Link1,
                Link2 = pgMinisty.Link2,
                Image1 = pgMinisty.Image1,
                BackGroundImage = pgMinisty.BackGroundImage
            };

            return viewModel;
        }
    }
}
