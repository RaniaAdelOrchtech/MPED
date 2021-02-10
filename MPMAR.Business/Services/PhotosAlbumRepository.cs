using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Business.ViewModels;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Business.Services
{
    public class PhotosAlbumRepository : IPhotosAlbumRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IPhotoArchiveRepository _photoArchiveRepository;
        public PhotosAlbumRepository(ApplicationDbContext db, IPhotoArchiveRepository photoArchiveRepository)
        {
            _db = db;
            _photoArchiveRepository = photoArchiveRepository;
        }

        public PhotosAlbumVersion Add(PhotosAlbumVersion photosAlbumVersion, int pageRouteId)
        {
            try
            {
                var photoArchiveVersion = _photoArchiveRepository.AddOrUpdatePhotoArchiveVersion(pageRouteId, photosAlbumVersion.PhotoArchiveVersionId);
                photosAlbumVersion.PhotoArchiveVersionId = photoArchiveVersion.Id;
                photosAlbumVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                photosAlbumVersion.ChangeActionEnum = ChangeActionEnum.New;
                photosAlbumVersion.Date = DateTime.Now;
                _db.PhotosAlbumVersions.Add(photosAlbumVersion);
                _db.SaveChanges();
                return _db.PhotosAlbumVersions.FirstOrDefault(c => c.Id == photosAlbumVersion.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PhotosAlbumVersion Update(PhotosAlbumVersion photosAlbumVersion, int pageRouteId)
        {
            try
            {
                var photoArchiveVersion = _photoArchiveRepository.AddOrUpdatePhotoArchiveVersion(pageRouteId, photosAlbumVersion.PhotoArchiveVersionId);
                var existingPhotoVer = _db.PhotosAlbumVersions.Find(photosAlbumVersion.Id);
                if (existingPhotoVer.VersionStatusEnum == VersionStatusEnum.Approved || existingPhotoVer.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    photosAlbumVersion.Id = 0;
                    photosAlbumVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                    photosAlbumVersion.ChangeActionEnum = ChangeActionEnum.Update;
                    photosAlbumVersion.PhotoArchiveVersionId = photoArchiveVersion.Id;
                    _db.PhotosAlbumVersions.Add(photosAlbumVersion);
                }
                else
                {
                    photosAlbumVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                    photosAlbumVersion.PhotoArchiveVersionId = photoArchiveVersion.Id;
                    photosAlbumVersion.ChangeActionEnum = photosAlbumVersion.ChangeActionEnum == ChangeActionEnum.New ? ChangeActionEnum.New : ChangeActionEnum.Update;
                    _db.Entry(existingPhotoVer).CurrentValues.SetValues(photosAlbumVersion);
                }
                _db.SaveChanges();
                return _db.PhotosAlbumVersions.FirstOrDefault(s => s.Id == photosAlbumVersion.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public IEnumerable<PhotosAlbum> GetPhotosAlbumId(int PhotosAlbumItemId)
        {
            var PhotosAlbumItem = _db.PhotosAlbum.OrderBy(s => s.Id).ToList();
            return PhotosAlbumItem;
        }


        public bool Delete(int id,int pageRouteId)
        {
            try
            {

                var item = _db.PhotosAlbumVersions.Include(x=>x.PhotoArchiveVersion).FirstOrDefault(x => x.Id == id);

                var photoArchiveVersion = _photoArchiveRepository.AddOrUpdatePhotoArchiveVersion(pageRouteId, item.PhotoArchiveVersionId);

                item.IsDeleted = true;

                _db.PhotosAlbumVersions.Update(item);
                if (item.PhotoArchiveVersion != null)
                {

                    var photoArchId = item.PhotoArchiveVersion.PhotoArchiveId;
                   
                    if (photoArchiveVersion != null)
                        photoArchiveVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                }
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool DeleteByPhotoArchiveId(List<PhotosAlbum> list)
        {
            try
            {
                _db.PhotosAlbum.RemoveRange(list);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<PhotosAlbumListViewModel> GetPhotoAlbums(int id)
        {
            var photoArchiveId = 0;
            var photoArchiveVersion = _photoArchiveRepository.GetVersion(id);
            if (photoArchiveVersion.PhotoArchiveId != null)
            {
                photoArchiveId = _photoArchiveRepository.Get(photoArchiveVersion.PhotoArchiveId.Value).Id;
            }
            var queryright = (from nw in _db.PhotosAlbum.Where(d => !d.IsDeleted && d.PhotoArchiveId == photoArchiveId)
                              from nwv in _db.PhotosAlbumVersions.Where(d => d.PhotosAlbumId == nw.Id && !d.IsDeleted
                              && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).Take(1)
                              select new PhotosAlbumListViewModel
                              {
                                  Id = nw.Id,
                                  VerId = nwv.Id,
                                  EnPhotosAlbumDesc = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.EnPhotosAlbumDesc : nw.EnPhotosAlbumDesc,
                                  ArPhotosAlbumDesc = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.ArPhotosAlbumDesc : nw.ArPhotosAlbumDesc,
                                  EnPhotosAlbumName = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.EnPhotosAlbumName : nw.EnPhotosAlbumName,
                                  ArPhotosAlbumName = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.ArPhotosAlbumName : nw.ArPhotosAlbumName,
                                  IsActive = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.IsActive : nw.IsActive,
                                  ImageUrl = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.ImagePath : nw.ImagePath,
                                  Order = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.Order : nw.Order,
                                  SeoTitleEN = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.SeoTitleEN : nw.SeoTitleEN,
                                  SeoTitleAR = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.SeoTitleAR : nw.SeoTitleAR,
                                  SeoDescriptionEN = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.SeoDescriptionEN : nw.SeoDescriptionEN,
                                  SeoDescriptionAR = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.SeoDescriptionAR : nw.SeoDescriptionAR,
                                  SeoOgTitleEN = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.SeoOgTitleEN : nw.SeoOgTitleEN,
                                  SeoOgTitleAR = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.SeoOgTitleAR : nw.SeoOgTitleAR,
                                  SeoTwitterCardEN = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.SeoTwitterCardEN : nw.SeoTwitterCardEN,
                                  SeoTwitterCardAR = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.SeoTwitterCardAR : nw.SeoTwitterCardAR,
                                  //EnPhotosAlbumType = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  //|| nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.en : nw.EnPhotosAlbumType,
                                  //ArPhotosAlbumType = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  //|| nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.ArPhotosAlbumType : nw.ArPhotosAlbumType,
                                  VersionStatusEnum = nwv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  PhotoArchiveId = photoArchiveId
                              });
            var queryleft = (from nwv in _db.PhotosAlbumVersions.Where(d => !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored
                             && d.PhotoArchiveVersionId == id)
                             where !_db.PhotosAlbum.Any(d => d.Id == nwv.PhotosAlbumId)
                             select new PhotosAlbumListViewModel
                             {
                                 Id = 0,
                                 VerId = nwv.Id,
                                 EnPhotosAlbumDesc = nwv.EnPhotosAlbumDesc,
                                 ArPhotosAlbumDesc = nwv.ArPhotosAlbumDesc,
                                 EnPhotosAlbumName = nwv.EnPhotosAlbumName,
                                 ArPhotosAlbumName = nwv.ArPhotosAlbumName,
                                 IsActive = nwv.IsActive,
                                 ImageUrl = nwv.ImagePath,
                                 Order = nwv.Order,
                                 SeoTitleEN = nwv.SeoTitleEN,
                                 SeoTitleAR = nwv.SeoTitleAR,
                                 SeoDescriptionEN = nwv.SeoDescriptionEN,
                                 SeoDescriptionAR = nwv.SeoDescriptionAR,
                                 SeoOgTitleEN = nwv.SeoOgTitleEN,
                                 SeoOgTitleAR = nwv.SeoOgTitleAR,
                                 SeoTwitterCardEN = nwv.SeoTwitterCardEN,
                                 SeoTwitterCardAR = nwv.SeoTwitterCardAR,
                                 //EnPhotoArchiveType = nwv.EnPhotoArchiveType,
                                 //ArPhotoArchiveType = nwv.ArPhotoArchiveType,
                                 VersionStatusEnum = nwv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                 PhotoArchiveId = photoArchiveId
                             });
            return queryright.Union(queryleft).OrderByDescending(d => d.VerId).ToList();
        }

        public IEnumerable<PhotosAlbum> Get()
        {
            return _db.PhotosAlbum;
        }
        public PhotosAlbumVersion GetVersion(int id)
        {
            return _db.PhotosAlbumVersions.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
        }
        public PhotosAlbum Get(int id)
        {
            return _db.PhotosAlbum.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
        }
        public PhotosAlbumEditViewModel GetDetail(int id)
        {
            var photoAlbumVersion = GetVersion(id);
            if (photoAlbumVersion != null)
            {
                if (photoAlbumVersion.VersionStatusEnum == VersionStatusEnum.Approved || photoAlbumVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    var photoAlbum = Get(photoAlbumVersion.PhotosAlbumId.Value);
                    var model = new PhotosAlbumEditViewModel
                    {
                        Id = id,
                        EnPhotosAlbumName = photoAlbum.EnPhotosAlbumName,
                        ArPhotosAlbumName = photoAlbum.ArPhotosAlbumName,
                        EnPhotosAlbumDesc = photoAlbum.EnPhotosAlbumDesc,
                        ArPhotosAlbumDesc = photoAlbum.ArPhotosAlbumDesc,
                        IsActive = photoAlbum.IsActive,
                        IsDeleted = photoAlbum.IsDeleted,
                        ImageUrl = photoAlbum.ImagePath,
                        Order = photoAlbum.Order,
                        SeoTitleEN = photoAlbum.SeoTitleEN,
                        SeoTitleAR = photoAlbum.SeoTitleAR,
                        SeoDescriptionEN = photoAlbum.SeoDescriptionEN,
                        SeoDescriptionAR = photoAlbum.SeoDescriptionAR,
                        SeoOgTitleEN = photoAlbum.SeoOgTitleEN,
                        SeoOgTitleAR = photoAlbum.SeoOgTitleAR,
                        SeoTwitterCardEN = photoAlbum.SeoTwitterCardEN,
                        SeoTwitterCardAR = photoAlbum.SeoTwitterCardAR,
                        PhotoArchiveVersionId = photoAlbumVersion.PhotoArchiveVersionId,
                        PhotoArchiveId = photoAlbum.PhotoArchiveId,
                        PhotoAlbumId = photoAlbum.Id
                    };
                    return model;
                }
                else
                {
                    var model = new PhotosAlbumEditViewModel
                    {
                        Id = id,
                        EnPhotosAlbumName = photoAlbumVersion.EnPhotosAlbumName,
                        ArPhotosAlbumName = photoAlbumVersion.ArPhotosAlbumName,
                        EnPhotosAlbumDesc = photoAlbumVersion.EnPhotosAlbumDesc,
                        ArPhotosAlbumDesc = photoAlbumVersion.ArPhotosAlbumDesc,
                        IsActive = photoAlbumVersion.IsActive,
                        IsDeleted = photoAlbumVersion.IsDeleted,
                        ImageUrl = photoAlbumVersion.ImagePath,
                        Order = photoAlbumVersion.Order,
                        SeoTitleEN = photoAlbumVersion.SeoTitleEN,
                        SeoTitleAR = photoAlbumVersion.SeoTitleAR,
                        SeoDescriptionEN = photoAlbumVersion.SeoDescriptionEN,
                        SeoDescriptionAR = photoAlbumVersion.SeoDescriptionAR,
                        SeoOgTitleEN = photoAlbumVersion.SeoOgTitleEN,
                        SeoOgTitleAR = photoAlbumVersion.SeoOgTitleAR,
                        SeoTwitterCardEN = photoAlbumVersion.SeoTwitterCardEN,
                        SeoTwitterCardAR = photoAlbumVersion.SeoTwitterCardAR,
                        PhotoArchiveVersionId = photoAlbumVersion.PhotoArchiveVersionId,
                        //PhotoArchiveId = photoAlbumVersion.PhotoArchiveId,
                        PhotoAlbumId = photoAlbumVersion.Id
                    };
                    return model;

                }
            }

            return null;
        }


    }
}
