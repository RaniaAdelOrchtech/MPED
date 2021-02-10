using MPMAR.Data;
using MPMAR.Data.HomePageModels.ViewModels;
using NToastNotify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class EconomicIndicatorsMapper
    {
        public static EconomicIndicators MapToEconomicIndiModel(this EconomicIndicatorViewModel viewModel)
        {
            return new EconomicIndicators()
            {
                Id = viewModel.Id,
                ImageDiscriptionAr1 = viewModel.ImageDiscriptionAr1,
                ImageDiscriptionAr2 = viewModel.ImageDiscriptionAr2,
                ImageDiscriptionAr3 = viewModel.ImageDiscriptionAr3,
                ImageTitleAr1 = viewModel.ImageTitleAr1,
                ImageTitleAr2 = viewModel.ImageTitleAr2,
                ImageTitleAr3 = viewModel.ImageTitleAr3,
                MainDiscriptionAr = viewModel.MainDiscriptionAr,
                ImageDiscriptionEn1 = viewModel.ImageDiscriptionEn1,
                ImageDiscriptionEn2 = viewModel.ImageDiscriptionEn2,
                ImageDiscriptionEn3 = viewModel.ImageDiscriptionEn3,
                ImageTitleEn1 = viewModel.ImageTitleEn1,
                ImageTitleEn2 = viewModel.ImageTitleEn2,
                ImageTitleEn3 = viewModel.ImageTitleEn3,
                MainDiscriptionEn = viewModel.MainDiscriptionEn,
                Link1 = viewModel.Link1,
                Link2 = viewModel.Link2,
                Link3 = viewModel.Link3,
                ImageUrl1 = viewModel.ImageUrl1,
                ImageUrl2 = viewModel.ImageUrl2,
                ImageUrl3 = viewModel.ImageUrl3
            };
        }

        public static EconomicIndicatorViewModel MapToEconomicIndiViewModel(this EconomicIndicators viewModel)
        {
            return new EconomicIndicatorViewModel()
            {
                Id = viewModel.Id,
                ImageDiscriptionAr1 = viewModel.ImageDiscriptionAr1,
                ImageDiscriptionAr2 = viewModel.ImageDiscriptionAr2,
                ImageDiscriptionAr3 = viewModel.ImageDiscriptionAr3,
                ImageTitleAr1 = viewModel.ImageTitleAr1,
                ImageTitleAr2 = viewModel.ImageTitleAr2,
                ImageTitleAr3 = viewModel.ImageTitleAr3,
                MainDiscriptionAr = viewModel.MainDiscriptionAr,
                ImageDiscriptionEn1 = viewModel.ImageDiscriptionEn1,
                ImageDiscriptionEn2 = viewModel.ImageDiscriptionEn2,
                ImageDiscriptionEn3 = viewModel.ImageDiscriptionEn3,
                ImageTitleEn1 = viewModel.ImageTitleEn1,
                ImageTitleEn2 = viewModel.ImageTitleEn2,
                ImageTitleEn3 = viewModel.ImageTitleEn3,
                MainDiscriptionEn = viewModel.MainDiscriptionEn,
                Link1 = viewModel.Link1,
                Link2 = viewModel.Link2,
                Link3 = viewModel.Link3,
                ImageUrl1 = viewModel.ImageUrl1,
                ImageUrl2 = viewModel.ImageUrl2,
                ImageUrl3 = viewModel.ImageUrl3
            };
        }


        public static EconomicIndicatorViewModel MapToEcoIndiVersionViewModel(this EconomicIndicatorsVersion pgMinisty)
        {
            EconomicIndicatorViewModel viewModel = new EconomicIndicatorViewModel()
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
                EconomicIndicatorId = pgMinisty.EconomicIndicatorsId,
                ImageDiscriptionAr1 = pgMinisty.ImageDiscriptionAr1,
                ImageDiscriptionAr2 = pgMinisty.ImageDiscriptionAr2,
                ImageDiscriptionAr3 = pgMinisty.ImageDiscriptionAr3,
                ImageTitleAr1 = pgMinisty.ImageTitleAr1,
                ImageTitleAr2 = pgMinisty.ImageTitleAr2,
                ImageTitleAr3 = pgMinisty.ImageTitleAr3,
                MainDiscriptionAr = pgMinisty.MainDiscriptionAr,
                ImageDiscriptionEn1 = pgMinisty.ImageDiscriptionEn1,
                ImageDiscriptionEn2 = pgMinisty.ImageDiscriptionEn2,
                ImageDiscriptionEn3 = pgMinisty.ImageDiscriptionEn3,
                ImageTitleEn1 = pgMinisty.ImageTitleEn1,
                ImageTitleEn2 = pgMinisty.ImageTitleEn2,
                ImageTitleEn3 = pgMinisty.ImageTitleEn3,
                MainDiscriptionEn = pgMinisty.MainDiscriptionEn,
                Link1 = pgMinisty.Link1,
                Link2 = pgMinisty.Link2,
                Link3 = pgMinisty.Link3,
                ImageUrl1 = pgMinisty.ImageUrl1,
                ImageUrl2 = pgMinisty.ImageUrl2,
                ImageUrl3 = pgMinisty.ImageUrl3
            };

            return viewModel;
        }

        public static EconomicIndicatorsVersion MapToEcoIndiVersionModel(this EconomicIndicatorViewModel pgMinisty)
        {
            EconomicIndicatorsVersion viewModel = new EconomicIndicatorsVersion()
            {
                Id = pgMinisty.EconomicIndicatorId ?? pgMinisty.Id,
                VersionStatusEnum = pgMinisty.VersionStatusEnum,
                ChangeActionEnum = pgMinisty.ChangeActionEnum,
                CreationDate = pgMinisty.CreationDate,
                ModificationDate = pgMinisty.ModificationDate,
                ModifiedById = pgMinisty.ModifiedById,
                ApprovalDate = pgMinisty.ApprovalDate,
                ApprovedById = pgMinisty.ApprovedById,
                CreatedById = pgMinisty.CreatedById,
                ImageDiscriptionAr1 = pgMinisty.ImageDiscriptionAr1,
                ImageDiscriptionAr2 = pgMinisty.ImageDiscriptionAr2,
                ImageDiscriptionAr3 = pgMinisty.ImageDiscriptionAr3,
                ImageTitleAr1 = pgMinisty.ImageTitleAr1,
                ImageTitleAr2 = pgMinisty.ImageTitleAr2,
                ImageTitleAr3 = pgMinisty.ImageTitleAr3,
                MainDiscriptionAr = pgMinisty.MainDiscriptionAr,
                ImageDiscriptionEn1 = pgMinisty.ImageDiscriptionEn1,
                ImageDiscriptionEn2 = pgMinisty.ImageDiscriptionEn2,
                ImageDiscriptionEn3 = pgMinisty.ImageDiscriptionEn3,
                ImageTitleEn1 = pgMinisty.ImageTitleEn1,
                ImageTitleEn2 = pgMinisty.ImageTitleEn2,
                ImageTitleEn3 = pgMinisty.ImageTitleEn3,
                MainDiscriptionEn = pgMinisty.MainDiscriptionEn,
                Link1 = pgMinisty.Link1,
                Link2 = pgMinisty.Link2,
                Link3 = pgMinisty.Link3,
                ImageUrl1 = pgMinisty.ImageUrl1,
                ImageUrl2 = pgMinisty.ImageUrl2,
                ImageUrl3 = pgMinisty.ImageUrl3            
            };

            return viewModel;
        }
    }
}
