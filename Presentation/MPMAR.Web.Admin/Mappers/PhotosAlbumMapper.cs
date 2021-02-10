using MPMAR.Business.ViewModels;
using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class PhotosAlbumMapper
    {
        //public static List<PhotosAlbumListViewModel> MapToPhotosAlbumViewModel(this IEnumerable<PhotosAlbum> PhotosAlbum)
        //{
        //    return PhotosAlbum.Select(pgMinisty => new PhotosAlbumListViewModel
        //    {
        //        Id = pgMinisty.Id,
        //        EnPhotosAlbumName = pgMinisty.EnPhotosAlbumName,
        //        ArPhotosAlbumName = pgMinisty.ArPhotosAlbumName,
        //        EnPhotosAlbumDesc = pgMinisty.EnPhotosAlbumDesc,
        //        ArPhotosAlbumDesc = pgMinisty.ArPhotosAlbumDesc,
        //        IsActive = pgMinisty.IsActive,
        //        IsDeleted = pgMinisty.IsDeleted,
        //        ImageUrl = pgMinisty.ImagePath,
        //        SeoTitleEN = pgMinisty.SeoTitleEN,
        //        SeoTitleAR = pgMinisty.SeoTitleAR,
        //        SeoDescriptionEN = pgMinisty.SeoDescriptionEN,
        //        SeoDescriptionAR = pgMinisty.SeoDescriptionAR,
        //        SeoOgTitleEN = pgMinisty.SeoOgTitleEN,
        //        SeoOgTitleAR = pgMinisty.SeoOgTitleAR,
        //        SeoTwitterCardEN = pgMinisty.SeoTwitterCardEN,
        //        SeoTwitterCardAR = pgMinisty.SeoTwitterCardAR

        //    }).ToList();
        //}

        public static PhotosAlbumVersion MapToPhotosAlbum(this PhotosAlbumEditViewModel sectionCardCreateViewModel)
        {
            PhotosAlbumVersion pageSectionVersion = new PhotosAlbumVersion();
            pageSectionVersion.EnPhotosAlbumName = sectionCardCreateViewModel.EnPhotosAlbumName;
            pageSectionVersion.ArPhotosAlbumName = sectionCardCreateViewModel.ArPhotosAlbumName;
            pageSectionVersion.EnPhotosAlbumDesc = sectionCardCreateViewModel.EnPhotosAlbumDesc;
            pageSectionVersion.ArPhotosAlbumDesc = sectionCardCreateViewModel.ArPhotosAlbumDesc;
            pageSectionVersion.IsActive = sectionCardCreateViewModel.IsActive;
            pageSectionVersion.IsDeleted = sectionCardCreateViewModel.IsDeleted;
            pageSectionVersion.ImagePath = sectionCardCreateViewModel.ImageUrl;
            pageSectionVersion.Order = sectionCardCreateViewModel.Order;
            pageSectionVersion.PhotoArchiveVersionId = sectionCardCreateViewModel.PhotoArchiveVersionId;

            pageSectionVersion.SeoTitleEN = sectionCardCreateViewModel.SeoTitleEN;
            pageSectionVersion.SeoTitleAR = sectionCardCreateViewModel.SeoTitleAR;
            pageSectionVersion.SeoDescriptionEN = sectionCardCreateViewModel.SeoDescriptionEN;
            pageSectionVersion.SeoDescriptionAR = sectionCardCreateViewModel.SeoDescriptionAR;
            pageSectionVersion.SeoOgTitleEN = sectionCardCreateViewModel.SeoOgTitleEN;
            pageSectionVersion.SeoOgTitleAR = sectionCardCreateViewModel.SeoOgTitleAR;
            pageSectionVersion.SeoTwitterCardEN = sectionCardCreateViewModel.SeoTwitterCardEN;
            pageSectionVersion.SeoTwitterCardAR = sectionCardCreateViewModel.SeoTwitterCardAR;

            if (sectionCardCreateViewModel.Id > 0)
                pageSectionVersion.Id = sectionCardCreateViewModel.Id;
            return pageSectionVersion;
        }

        public static PhotosAlbumEditViewModel MapToSctionCardViewModel(this PhotosAlbumVersion sectionCardCreateViewModel)
        {
            PhotosAlbumEditViewModel viewModel = new PhotosAlbumEditViewModel()
            {
                Id = sectionCardCreateViewModel.Id,
                EnPhotosAlbumName = sectionCardCreateViewModel.EnPhotosAlbumName,
                ArPhotosAlbumName = sectionCardCreateViewModel.ArPhotosAlbumName,
                EnPhotosAlbumDesc = sectionCardCreateViewModel.EnPhotosAlbumDesc,
                ArPhotosAlbumDesc = sectionCardCreateViewModel.ArPhotosAlbumDesc,
                IsActive = sectionCardCreateViewModel.IsActive,
                IsDeleted = sectionCardCreateViewModel.IsDeleted,
                ImageUrl = sectionCardCreateViewModel.ImagePath,
                Order = sectionCardCreateViewModel.Order,
                SeoTitleEN = sectionCardCreateViewModel.SeoTitleEN,
                SeoTitleAR = sectionCardCreateViewModel.SeoTitleAR,
                SeoDescriptionEN = sectionCardCreateViewModel.SeoDescriptionEN,
                SeoDescriptionAR = sectionCardCreateViewModel.SeoDescriptionAR,
                SeoOgTitleEN = sectionCardCreateViewModel.SeoOgTitleEN,
                SeoOgTitleAR = sectionCardCreateViewModel.SeoOgTitleAR,
                SeoTwitterCardEN = sectionCardCreateViewModel.SeoTwitterCardEN,
                SeoTwitterCardAR = sectionCardCreateViewModel.SeoTwitterCardAR,
                PhotoArchiveVersionId = sectionCardCreateViewModel.PhotoArchiveVersionId
            };

            return viewModel;
        }

    
    }
}

