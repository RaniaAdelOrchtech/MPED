using MPMAR.Data.HomePageModels;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class homePageBasicInfoMapper
    {
        public static HomePageBasicInfoViewModel MapToViewModel(this HomePageBasicInfo homePageBasicInfo)
        {
            return new HomePageBasicInfoViewModel()
            {
                Id=homePageBasicInfo.Id,
                LogoUrl=homePageBasicInfo.LogoUrl,
                ApprovalDate=homePageBasicInfo.ApprovalDate,
                ApprovedById=homePageBasicInfo.ApprovedById,
                CreatedById=homePageBasicInfo.CreatedById,
                CreationDate=homePageBasicInfo.CreationDate,
                FavIconUrl=homePageBasicInfo.FavIconUrl,
                ModificationDate=homePageBasicInfo.ModificationDate,
                ModifiedById=homePageBasicInfo.ModifiedById,
                SeoDescriptionAR=homePageBasicInfo.SeoDescriptionAR,
                SeoTwitterCardEN=homePageBasicInfo.SeoTwitterCardEN,
                SeoTwitterCardAR=homePageBasicInfo.SeoTwitterCardAR,
                SeoTitleEN=homePageBasicInfo.SeoTitleEN,
                SeoTitleAR=homePageBasicInfo.SeoTitleAR,
                SeoOgTitleEN=homePageBasicInfo.SeoOgTitleEN,
                SeoOgTitleAR=homePageBasicInfo.SeoOgTitleAR,
                SeoDescriptionEN=homePageBasicInfo.SeoDescriptionEN,
               
            };
        }

        public static HomePageBasicInfo MapToModel(this HomePageBasicInfoViewModel homePageBasicInfo)
        {
            return new HomePageBasicInfo()
            {
                Id = homePageBasicInfo.Id,
                LogoUrl = homePageBasicInfo.LogoUrl,
                ApprovalDate = homePageBasicInfo.ApprovalDate,
                ApprovedById = homePageBasicInfo.ApprovedById,
                CreatedById = homePageBasicInfo.CreatedById,
                CreationDate = homePageBasicInfo.CreationDate,
                FavIconUrl = homePageBasicInfo.FavIconUrl,
                ModificationDate = homePageBasicInfo.ModificationDate,
                ModifiedById = homePageBasicInfo.ModifiedById,
                SeoDescriptionAR = homePageBasicInfo.SeoDescriptionAR,
                SeoTwitterCardEN = homePageBasicInfo.SeoTwitterCardEN,
                SeoTwitterCardAR = homePageBasicInfo.SeoTwitterCardAR,
                SeoTitleEN = homePageBasicInfo.SeoTitleEN,
                SeoTitleAR = homePageBasicInfo.SeoTitleAR,
                SeoOgTitleEN = homePageBasicInfo.SeoOgTitleEN,
                SeoOgTitleAR = homePageBasicInfo.SeoOgTitleAR,
                SeoDescriptionEN = homePageBasicInfo.SeoDescriptionEN,

            };
        }
    }
}
