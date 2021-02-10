using MPMAR.Business.ViewModels;
using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class PhotoArchiveMapper
    {
        public static List<PhotoArchiveListViewModel> MapToPhotoArchiveViewModel(this IEnumerable<PhotoArchive> PhotoArchive)
        {
            return PhotoArchive.Select(pgMinisty => new PhotoArchiveListViewModel
            {
                Id = pgMinisty.Id,
                EnPhotoArchiveName = pgMinisty.EnPhotoArchiveName,
                ArPhotoArchiveName = pgMinisty.ArPhotoArchiveName,
                EnPhotoArchiveDesc = pgMinisty.EnPhotoArchiveDesc,
                ArPhotoArchiveDesc = pgMinisty.ArPhotoArchiveDesc,
                IsActive = pgMinisty.IsActive,
                IsDeleted = pgMinisty.IsDeleted,
                ImageUrl = pgMinisty.ImageUrl,
                Order = pgMinisty.Order,
                SeoTitleEN = pgMinisty.SeoTitleEN,
                SeoTitleAR = pgMinisty.SeoTitleAR,
                SeoDescriptionEN = pgMinisty.SeoDescriptionEN,
                SeoDescriptionAR = pgMinisty.SeoDescriptionAR,
                SeoOgTitleEN = pgMinisty.SeoOgTitleEN,
                SeoOgTitleAR = pgMinisty.SeoOgTitleAR,
                SeoTwitterCardEN = pgMinisty.SeoTwitterCardEN,
                SeoTwitterCardAR = pgMinisty.SeoTwitterCardAR,

                EnPhotoArchiveType = pgMinisty.EnPhotoArchiveType,
                ArPhotoArchiveType = pgMinisty.ArPhotoArchiveType

            }).ToList();
        }

        public static PhotoArchiveVersion MapToPhotoArchive(this PhotoArchiveEditViewModel photoArchive)
        {
            PhotoArchiveVersion pageSectionVersion = new PhotoArchiveVersion();
            pageSectionVersion.EnPhotoArchiveName = photoArchive.EnPhotoArchiveName;
            pageSectionVersion.ArPhotoArchiveName = photoArchive.ArPhotoArchiveName;
            pageSectionVersion.EnPhotoArchiveDesc = photoArchive.EnPhotoArchiveDesc;
            pageSectionVersion.ArPhotoArchiveDesc = photoArchive.ArPhotoArchiveDesc;
            pageSectionVersion.IsActive = photoArchive.IsActive;
            pageSectionVersion.IsDeleted = photoArchive.IsDeleted;

            if (photoArchive.ImageUrl != null)
                pageSectionVersion.ImageUrl = photoArchive.ImageUrl;
            pageSectionVersion.Order = photoArchive.Order;
            pageSectionVersion.EnPhotoArchiveType = photoArchive.EnPhotoArchiveType;
            pageSectionVersion.ArPhotoArchiveType = photoArchive.ArPhotoArchiveType;

            pageSectionVersion.SeoTitleEN = photoArchive.SeoTitleEN;
            pageSectionVersion.SeoTitleAR = photoArchive.SeoTitleAR;
            pageSectionVersion.SeoDescriptionEN = photoArchive.SeoDescriptionEN;
            pageSectionVersion.SeoDescriptionAR = photoArchive.SeoDescriptionAR;
            pageSectionVersion.SeoOgTitleEN = photoArchive.SeoOgTitleEN;
            pageSectionVersion.SeoOgTitleAR = photoArchive.SeoOgTitleAR;
            pageSectionVersion.SeoTwitterCardEN = photoArchive.SeoTwitterCardEN;
            pageSectionVersion.SeoTwitterCardAR = photoArchive.SeoTwitterCardAR;

            pageSectionVersion.VersionStatusEnum = photoArchive.VersionStatusEnum;
            pageSectionVersion.ChangeActionEnum = photoArchive.ChangeActionEnum;


            if (photoArchive.Id > 0)
                pageSectionVersion.Id = photoArchive.Id;
            return pageSectionVersion;
        }

        public static PhotoArchiveEditViewModel MapToSctionCardViewModel(this PhotoArchive pgMinisty)
        {
            PhotoArchiveEditViewModel viewModel = new PhotoArchiveEditViewModel()
            {
                Id = pgMinisty.Id,
                EnPhotoArchiveName = pgMinisty.EnPhotoArchiveName,
                ArPhotoArchiveName = pgMinisty.ArPhotoArchiveName,
                EnPhotoArchiveDesc = pgMinisty.EnPhotoArchiveDesc,
                ArPhotoArchiveDesc = pgMinisty.ArPhotoArchiveDesc,
                IsActive = pgMinisty.IsActive,
                Order = pgMinisty.Order,
                IsDeleted = pgMinisty.IsDeleted,
                ImageUrl = pgMinisty.ImageUrl,
                SeoTitleEN = pgMinisty.SeoTitleEN,
                SeoTitleAR = pgMinisty.SeoTitleAR,
                SeoDescriptionEN = pgMinisty.SeoDescriptionEN,
                SeoDescriptionAR = pgMinisty.SeoDescriptionAR,
                SeoOgTitleEN = pgMinisty.SeoOgTitleEN,
                SeoOgTitleAR = pgMinisty.SeoOgTitleAR,
                SeoTwitterCardEN = pgMinisty.SeoTwitterCardEN,
                SeoTwitterCardAR = pgMinisty.SeoTwitterCardAR,

                EnPhotoArchiveType = pgMinisty.EnPhotoArchiveType,
                ArPhotoArchiveType = pgMinisty.ArPhotoArchiveType
            };

            return viewModel;
        }


    }
}

