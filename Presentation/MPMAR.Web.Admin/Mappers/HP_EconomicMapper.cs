using MPMAR.Data.HomePageModels;
using MPMAR.Data.HomePageModels.ViewModels;
using NToastNotify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class HP_EconomicMapper
    {
        public static EconomicDevelopment MapToEconomicModel(this HP_EconomicDevViewModel viewModel)
        {
            return new EconomicDevelopment()
            {
                Id = viewModel.Id,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                ArDescription1 = viewModel.ArDescription1,
                ArDescription2 = viewModel.ArDescription2,
                ArDescription3 = viewModel.ArDescription3,
                ArMainTitle = viewModel.ArMainTitle,
                ArTitle1 = viewModel.ArTitle1,
                ArTitle2 = viewModel.ArTitle2,
                ArTitle3 = viewModel.ArTitle3,
                EnDescription1 = viewModel.EnDescription1,
                EnDescription2 = viewModel.EnDescription2,
                EnDescription3 = viewModel.EnDescription3,
                EnMainTitle = viewModel.EnMainTitle,
                EnTitle1 = viewModel.EnTitle1,
                EnTitle2 = viewModel.EnTitle2,
                EnTitle3 = viewModel.EnTitle3,
                Url1 = viewModel.Url1,
                Url2 = viewModel.Url2,
                Url3 = viewModel.Url3
            };
        }

        public static HP_EconomicDevViewModel MapToEconomicViewModel(this EconomicDevelopment viewModel)
        {
            return new HP_EconomicDevViewModel()
            {
                Id = viewModel.Id,
                IsActive = viewModel.IsActive,
                IsDeleted = viewModel.IsDeleted,
                ArDescription1 = viewModel.ArDescription1,
                ArDescription2 = viewModel.ArDescription2,
                ArDescription3 = viewModel.ArDescription3,
                ArMainTitle = viewModel.ArMainTitle,
                ArTitle1 = viewModel.ArTitle1,
                ArTitle2 = viewModel.ArTitle2,
                ArTitle3 = viewModel.ArTitle3,
                EnDescription1 = viewModel.EnDescription1,
                EnDescription2 = viewModel.EnDescription2,
                EnDescription3 = viewModel.EnDescription3,
                EnMainTitle = viewModel.EnMainTitle,
                EnTitle1 = viewModel.EnTitle1,
                EnTitle2 = viewModel.EnTitle2,
                EnTitle3 = viewModel.EnTitle3,
                Url1 = viewModel.Url1,
                Url2 = viewModel.Url2,
                Url3 = viewModel.Url3
            };
        }


        public static HP_EconomicDevViewModel MapToEcoDevVersionViewModel(this EconomicDevelopmentVersions pgMinisty)
        {
            HP_EconomicDevViewModel viewModel = new HP_EconomicDevViewModel()
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
                EconomicDevelopmentId = pgMinisty.EconomicDevelopmentId,
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
                Url1 = pgMinisty.Url1,
                Url2 = pgMinisty.Url2,
                Url3 = pgMinisty.Url3,
                BackGroundImage = pgMinisty.BackGroundImage
            };

            return viewModel;
        }

        public static EconomicDevelopmentVersions MapToEcoDevVersionModel(this HP_EconomicDevViewModel pgMinisty)
        {
            EconomicDevelopmentVersions viewModel = new EconomicDevelopmentVersions()
            {
                Id = pgMinisty.EconomicDevelopmentId ?? pgMinisty.Id,
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
                Url1 = pgMinisty.Url1,
                Url2 = pgMinisty.Url2,
                Url3 = pgMinisty.Url3,
                BackGroundImage = pgMinisty.BackGroundImage
            };

            return viewModel;
        }
    }
}
