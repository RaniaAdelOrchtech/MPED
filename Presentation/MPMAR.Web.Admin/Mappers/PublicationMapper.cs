using MPMAR.Data.HomePageModels;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class PublicationMapper
    {
        public static Publication MapToPublicationModel(this PublicationViewModel model)
        {
            return new Publication()
            {
                Id = model.Id,
                ArTitle1 = model.ArTitle1,
                ArTitle2 = model.ArTitle2,
                ArDescription1 = model.ArDescription1,
                ArDescription2 = model.ArDescription2,
                ArDescription3 = model.ArDescription3,
                ArMainTitle = model.ArMainTitle,
                ArTitle3 = model.ArTitle3,
                EnDescription1 = model.EnDescription1,
                EnDescription2 = model.EnDescription2,
                EnDescription3 = model.EnDescription3,
                EnMainTitle = model.EnMainTitle,
                EnTitle1 = model.EnTitle1,
                EnTitle2 = model.EnTitle2,
                EnTitle3 = model.EnTitle3,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                Image1 = model.Image1,
                Image2 = model.Image2,
                Image3 = model.Image3,
                Link1 = model.Link1,
                Link2 = model.Link2,
                Link3 = model.Link3,
            };

        }

        public static PublicationViewModel MapToPublicationViewModel(this Publication model)
        {
            return new PublicationViewModel()
            {
                Id = model.Id,
                ArTitle1 = model.ArTitle1,
                ArTitle2 = model.ArTitle2,
                ArDescription1 = model.ArDescription1,
                ArDescription2 = model.ArDescription2,
                ArDescription3 = model.ArDescription3,
                ArMainTitle = model.ArMainTitle,
                ArTitle3 = model.ArTitle3,
                EnDescription1 = model.EnDescription1,
                EnDescription2 = model.EnDescription2,
                EnDescription3 = model.EnDescription3,
                EnMainTitle = model.EnMainTitle,
                EnTitle1 = model.EnTitle1,
                EnTitle2 = model.EnTitle2,
                EnTitle3 = model.EnTitle3,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                Image1 = model.Image1,
                Image2 = model.Image2,
                Image3 = model.Image3,
                Link1 = model.Link1,
                Link2 = model.Link2,
                Link3 = model.Link3
            };

        }

        public static PublicationViewModel MapToPublicationVersionViewModel(this PublicationVersions pgMinisty)
        {
            PublicationViewModel viewModel = new PublicationViewModel()
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
                PublicationId = pgMinisty.PublicationId,
                ArMainTitle = pgMinisty.ArMainTitle,
                EnMainTitle = pgMinisty.EnMainTitle,
                EnDescription1 = pgMinisty.EnDescription1,
                ArDescription1 = pgMinisty.ArDescription1,
                EnDescription2 = pgMinisty.EnDescription2,
                ArDescription2 = pgMinisty.ArDescription2,
                EnDescription3 = pgMinisty.EnDescription3,
                ArDescription3 = pgMinisty.ArDescription3,
                ArTitle1 = pgMinisty.ArTitle1,
                ArTitle2 = pgMinisty.ArTitle2,
                ArTitle3 = pgMinisty.ArTitle3,
                EnTitle1 = pgMinisty.EnTitle1,
                EnTitle2 = pgMinisty.EnTitle2,
                EnTitle3 = pgMinisty.EnTitle3,
                Link1 = pgMinisty.Link1,
                Link2 = pgMinisty.Link2,
                Link3 = pgMinisty.Link3,
                Image1 = pgMinisty.Image1,
                Image2 = pgMinisty.Image2,
                Image3 = pgMinisty.Image3
            };

            return viewModel;
        }

        public static PublicationVersions MapToPublicationVersionModel(this PublicationViewModel pgMinisty)
        {
            PublicationVersions viewModel = new PublicationVersions()
            {
                Id = pgMinisty.PublicationId ?? pgMinisty.Id,
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
                EnDescription3 = pgMinisty.EnDescription3,
                ArDescription3 = pgMinisty.ArDescription3,
                ArTitle1 = pgMinisty.ArTitle1,
                ArTitle2 = pgMinisty.ArTitle2,
                ArTitle3 = pgMinisty.ArTitle3,
                EnTitle1 = pgMinisty.EnTitle1,
                EnTitle2 = pgMinisty.EnTitle2,
                EnTitle3 = pgMinisty.EnTitle3,
                Link1 = pgMinisty.Link1,
                Link2 = pgMinisty.Link2,
                Link3 = pgMinisty.Link3,
                Image1 = pgMinisty.Image1,
                Image2 = pgMinisty.Image2,
                Image3 = pgMinisty.Image3
            };

            return viewModel;
        }
    }
}
