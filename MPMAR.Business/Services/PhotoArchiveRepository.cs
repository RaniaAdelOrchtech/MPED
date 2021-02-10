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
    public class PhotoArchiveRepository : IPhotoArchiveRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        private readonly IPhotoArchiveElasticSearchService _photoArchiveElasticSearchService;
        private readonly IGlobalElasticSearchService _globalElasticSearchService;
        private readonly IPageRouteRepository _pageRouteRepository;

        public PhotoArchiveRepository(ApplicationDbContext db, IPageRouteVersionRepository pageRouteVersionRepository, IApprovalNotificationsRepository approvalNotificationsRepository, IPhotoArchiveElasticSearchService photoArchiveElasticSearchService, IGlobalElasticSearchService globalElasticSearchService, IPageRouteRepository pageRouteRepository)
        {
            _db = db;
            _pageRouteVersionRepository = pageRouteVersionRepository;
            _approvalNotificationsRepository = approvalNotificationsRepository;
            _photoArchiveElasticSearchService = photoArchiveElasticSearchService;
            _globalElasticSearchService = globalElasticSearchService;
            _pageRouteRepository = pageRouteRepository;
        }

        public PhotoArchiveVersion Add(PhotoArchiveVersion PhotoArchiveItem, int pageRouteId)
        {
            try
            {
                var pageRouteVersion = _pageRouteVersionRepository.AddOrUpdatePageRouteVersion(pageRouteId);
                PhotoArchiveItem.PageRouteVersionId = pageRouteVersion.Id;
                PhotoArchiveItem.VersionStatusEnum = VersionStatusEnum.Draft;
                PhotoArchiveItem.ChangeActionEnum = ChangeActionEnum.New;
                PhotoArchiveItem.Date = DateTime.Now;
                foreach (var photo in PhotoArchiveItem.PhotosAlbumVersions)
                {
                    photo.VersionStatusEnum = VersionStatusEnum.Draft;
                    photo.ChangeActionEnum = ChangeActionEnum.New;
                    photo.Date = DateTime.Now;
                }
                _db.PhotoArchiveVersions.Add(PhotoArchiveItem);
                _db.SaveChanges();
                return _db.PhotoArchiveVersions.FirstOrDefault(c => c.Id == PhotoArchiveItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PhotoArchiveVersion Update(PhotoArchiveVersion PhotoArchiveItem, int pageRouteId)
        {
            try
            {
                var pageRouteVersion = _pageRouteVersionRepository.AddOrUpdatePageRouteVersion(pageRouteId);
                var existingPhotoVer = _db.PhotoArchiveVersions.Find(PhotoArchiveItem.Id);
                if (existingPhotoVer.VersionStatusEnum == VersionStatusEnum.Approved || existingPhotoVer.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    PhotoArchiveItem.Id = 0;
                    PhotoArchiveItem.VersionStatusEnum = VersionStatusEnum.Draft;
                    PhotoArchiveItem.ChangeActionEnum = ChangeActionEnum.Update;
                    PhotoArchiveItem.PageRouteVersionId = pageRouteVersion.Id;
                    PhotoArchiveItem.PhotoArchiveId = existingPhotoVer.PhotoArchiveId;
                    _db.PhotoArchiveVersions.Add(PhotoArchiveItem);
                }
                else
                {
                    PhotoArchiveItem.VersionStatusEnum = VersionStatusEnum.Draft;
                    PhotoArchiveItem.PageRouteVersionId = pageRouteVersion.Id;
                    PhotoArchiveItem.ChangeActionEnum = PhotoArchiveItem.ChangeActionEnum == ChangeActionEnum.New ? ChangeActionEnum.New : ChangeActionEnum.Update;
                    _db.Entry(existingPhotoVer).CurrentValues.SetValues(PhotoArchiveItem);
                }
                _db.SaveChanges();
                return _db.PhotoArchiveVersions.FirstOrDefault(s => s.Id == PhotoArchiveItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<PhotoArchive> GetPhotoArchiveId(int PhotoArchiveItemId)
        {
            var PhotoArchiveItem = _db.PhotoArchive.Where(i => i.IsDeleted != true).OrderBy(s => s.Id).ToList();
            return PhotoArchiveItem;
        }
        public bool Delete(int id)
        {
            try
            {
                var item = _db.PhotoArchive.FirstOrDefault(x => x.Id == id);
                _db.PhotoArchive.Remove(item);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public PhotoArchive Get(int id)
        {
            return _db.PhotoArchive.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
        }
        public PhotoArchiveVersion GetVersion(int id, bool isDeleted = false)
        {
            return _db.PhotoArchiveVersions.FirstOrDefault(p => p.Id == id && (isDeleted || !p.IsDeleted));
        }

        public IEnumerable<PhotoArchive> Get()
        {

            return _db.PhotoArchive.Where(i => i.IsDeleted != true);
        }
        public IEnumerable<PhotoArchiveVersion> GetVersion()
        {
            return _db.PhotoArchiveVersions.Where(i => i.IsDeleted != true);
        }
        public PhotoArchiveEditViewModel GetDetail(int id)
        {
            var photoArchiveVersion = GetVersion(id);
            if (photoArchiveVersion != null)
            {
                if (photoArchiveVersion.VersionStatusEnum == VersionStatusEnum.Approved || photoArchiveVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    var photoArchive = Get(photoArchiveVersion.PhotoArchiveId.Value);
                    var model = new PhotoArchiveEditViewModel
                    {
                        Id = id,
                        EnPhotoArchiveName = photoArchive.EnPhotoArchiveName,
                        ArPhotoArchiveName = photoArchive.ArPhotoArchiveName,
                        EnPhotoArchiveDesc = photoArchive.EnPhotoArchiveDesc,
                        ArPhotoArchiveDesc = photoArchive.ArPhotoArchiveDesc,
                        IsActive = photoArchive.IsActive,
                        Order = photoArchive.Order,
                        IsDeleted = photoArchive.IsDeleted,
                        ImageUrl = photoArchive.ImageUrl,
                        SeoTitleEN = photoArchive.SeoTitleEN,
                        SeoTitleAR = photoArchive.SeoTitleAR,
                        SeoDescriptionEN = photoArchive.SeoDescriptionEN,
                        SeoDescriptionAR = photoArchive.SeoDescriptionAR,
                        SeoOgTitleEN = photoArchive.SeoOgTitleEN,
                        SeoOgTitleAR = photoArchive.SeoOgTitleAR,
                        SeoTwitterCardEN = photoArchive.SeoTwitterCardEN,
                        SeoTwitterCardAR = photoArchive.SeoTwitterCardAR,
                        EnPhotoArchiveType = photoArchive.EnPhotoArchiveType,
                        ArPhotoArchiveType = photoArchive.ArPhotoArchiveType,
                        PhotoArchiveId = photoArchive.Id,

                    };
                    return model;
                }
                else
                {
                    var model = new PhotoArchiveEditViewModel
                    {
                        Id = id,
                        EnPhotoArchiveName = photoArchiveVersion.EnPhotoArchiveName,
                        ArPhotoArchiveName = photoArchiveVersion.ArPhotoArchiveName,
                        EnPhotoArchiveDesc = photoArchiveVersion.EnPhotoArchiveDesc,
                        ArPhotoArchiveDesc = photoArchiveVersion.ArPhotoArchiveDesc,
                        IsActive = photoArchiveVersion.IsActive,
                        Order = photoArchiveVersion.Order,
                        IsDeleted = photoArchiveVersion.IsDeleted,
                        ImageUrl = photoArchiveVersion.ImageUrl,
                        SeoTitleEN = photoArchiveVersion.SeoTitleEN,
                        SeoTitleAR = photoArchiveVersion.SeoTitleAR,
                        SeoDescriptionEN = photoArchiveVersion.SeoDescriptionEN,
                        SeoDescriptionAR = photoArchiveVersion.SeoDescriptionAR,
                        SeoOgTitleEN = photoArchiveVersion.SeoOgTitleEN,
                        SeoOgTitleAR = photoArchiveVersion.SeoOgTitleAR,
                        SeoTwitterCardEN = photoArchiveVersion.SeoTwitterCardEN,
                        SeoTwitterCardAR = photoArchiveVersion.SeoTwitterCardAR,
                        EnPhotoArchiveType = photoArchiveVersion.EnPhotoArchiveType,
                        ArPhotoArchiveType = photoArchiveVersion.ArPhotoArchiveType,
                        PhotoArchiveId = photoArchiveVersion.Id,
                        ChangeActionEnum = photoArchiveVersion.ChangeActionEnum,
                        VersionStatusEnum = photoArchiveVersion.VersionStatusEnum,
                    };
                    return model;

                }
            }

            return null;
        }
        private void CopyToVersion()
        {
            var photArchives = _db.PhotoArchive.Include(x => x.PhotosAlbums).ToList();
            foreach (var item in photArchives)
            {
                var photoArchiveVersion = new PhotoArchiveVersion()
                {
                    PageRouteVersionId = 2,
                    ArPhotoArchiveDesc = item.ArPhotoArchiveDesc,
                    ArPhotoArchiveName = item.ArPhotoArchiveName,
                    ArPhotoArchiveType = item.ArPhotoArchiveType,
                    ChangeActionEnum = ChangeActionEnum.Update,
                    CreationDate = item.CreationDate,
                    CreatedById = item.CreatedById,
                    EnPhotoArchiveDesc = item.EnPhotoArchiveDesc,
                    EnPhotoArchiveName = item.EnPhotoArchiveName,
                    VersionStatusEnum = VersionStatusEnum.Approved,
                    SeoTwitterCardEN = item.SeoTwitterCardEN,
                    SeoTwitterCardAR = item.SeoTwitterCardAR,
                    SeoTitleEN = item.SeoTitleEN,
                    SeoTitleAR = item.SeoTitleAR,
                    SeoOgTitleEN = item.SeoOgTitleEN,
                    SeoOgTitleAR = item.SeoOgTitleAR,
                    SeoDescriptionEN = item.SeoDescriptionEN,
                    SeoDescriptionAR = item.SeoDescriptionAR,
                    PhotoArchiveId = item.Id,
                    Order = item.Order,
                    IsDeleted = item.IsDeleted,
                    IsActive = item.IsActive,
                    ImageUrl = item.ImageUrl,
                    EnPhotoArchiveType = item.EnPhotoArchiveType,

                };
                _db.PhotoArchiveVersions.Add(photoArchiveVersion);
                _db.SaveChanges();
                foreach (var innerItem in item.PhotosAlbums)
                {
                    _db.PhotosAlbumVersions.Add(new PhotosAlbumVersion()
                    {
                        ChangeActionEnum = ChangeActionEnum.Update,
                        CreationDate = innerItem.CreationDate,
                        CreatedById = innerItem.CreatedById,
                        VersionStatusEnum = VersionStatusEnum.Approved,
                        SeoTwitterCardEN = innerItem.SeoTwitterCardEN,
                        SeoTwitterCardAR = innerItem.SeoTwitterCardAR,
                        SeoTitleEN = innerItem.SeoTitleEN,
                        SeoTitleAR = innerItem.SeoTitleAR,
                        SeoOgTitleEN = innerItem.SeoOgTitleEN,
                        SeoOgTitleAR = innerItem.SeoOgTitleAR,
                        SeoDescriptionEN = innerItem.SeoDescriptionEN,
                        SeoDescriptionAR = innerItem.SeoDescriptionAR,
                        PhotosAlbumId = innerItem.Id,
                        Order = innerItem.Order,
                        IsDeleted = innerItem.IsDeleted,
                        IsActive = innerItem.IsActive,
                        PhotoArchiveVersionId = photoArchiveVersion.Id,
                        ImagePath = innerItem.ImagePath,
                        ImageName = innerItem.ImageName,
                        ArPhotosAlbumDesc = innerItem.ArPhotosAlbumDesc,
                        ArPhotosAlbumName = innerItem.ArPhotosAlbumName,
                        EnPhotosAlbumDesc = innerItem.EnPhotosAlbumDesc,
                        EnPhotosAlbumName = innerItem.EnPhotosAlbumName,
                    });
                }
            }
            _db.SaveChanges();
        }
        public IEnumerable<PhotoArchiveListViewModel> GetPhotoArchiveByPageRouteId(int pageRouteId)
        {


            var pageRouteVersionId = 0;

            var pageRouteVersion = _pageRouteVersionRepository.GetByPageRoute(pageRouteId);

            if (pageRouteVersion != null) pageRouteVersionId = pageRouteVersion.Id;

            var queryright = (from nw in _db.PhotoArchive.Where(d => !d.IsDeleted && d.PageRouteId == pageRouteId)
                              from nwv in _db.PhotoArchiveVersions.Where(d => d.PhotoArchiveId == nw.Id
                              && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).Take(1).Where(x => !x.IsDeleted)
                              select new PhotoArchiveListViewModel
                              {
                                  Id = nw.Id,
                                  VerId = nwv.Id,
                                  EnPhotoArchiveName = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.EnPhotoArchiveName : nw.EnPhotoArchiveName,
                                  ArPhotoArchiveName = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.ArPhotoArchiveName : nw.ArPhotoArchiveName,
                                  EnPhotoArchiveDesc = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.EnPhotoArchiveDesc : nw.EnPhotoArchiveDesc,
                                  ArPhotoArchiveDesc = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.ArPhotoArchiveDesc : nw.ArPhotoArchiveDesc,
                                  IsActive = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.IsActive : nw.IsActive,
                                  ImageUrl = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.ImageUrl : nw.ImageUrl,
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
                                  EnPhotoArchiveType = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.EnPhotoArchiveType : nw.EnPhotoArchiveType,
                                  ArPhotoArchiveType = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.ArPhotoArchiveType : nw.ArPhotoArchiveType,
                                  CreatedById = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.CreatedById : nw.CreatedById,
                                  VersionStatusEnum = nwv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = nwv.ChangeActionEnum ?? ChangeActionEnum.New,
                              });
            var queryleft = (from nwv in _db.PhotoArchiveVersions.Where(d => !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored
                             && d.PageRouteVersionId == pageRouteVersionId)
                             where !_db.PhotoArchive.Any(d => d.Id == nwv.PhotoArchiveId)
                             select new PhotoArchiveListViewModel
                             {
                                 Id = 0,
                                 VerId = nwv.Id,
                                 EnPhotoArchiveName = nwv.EnPhotoArchiveName,
                                 ArPhotoArchiveName = nwv.ArPhotoArchiveName,
                                 EnPhotoArchiveDesc = nwv.EnPhotoArchiveDesc,
                                 ArPhotoArchiveDesc = nwv.ArPhotoArchiveDesc,
                                 IsActive = nwv.IsActive,
                                 ImageUrl = nwv.ImageUrl,
                                 Order = nwv.Order,
                                 SeoTitleEN = nwv.SeoTitleEN,
                                 SeoTitleAR = nwv.SeoTitleAR,
                                 SeoDescriptionEN = nwv.SeoDescriptionEN,
                                 SeoDescriptionAR = nwv.SeoDescriptionAR,
                                 SeoOgTitleEN = nwv.SeoOgTitleEN,
                                 SeoOgTitleAR = nwv.SeoOgTitleAR,
                                 SeoTwitterCardEN = nwv.SeoTwitterCardEN,
                                 SeoTwitterCardAR = nwv.SeoTwitterCardAR,
                                 EnPhotoArchiveType = nwv.EnPhotoArchiveType,
                                 ArPhotoArchiveType = nwv.ArPhotoArchiveType,
                                 VersionStatusEnum = nwv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                 ChangeActionEnum = nwv.ChangeActionEnum ?? ChangeActionEnum.New,
                                 CreatedById = nwv.CreatedById
                             });
            return queryright.Union(queryleft).OrderByDescending(d => d.VerId).ToList();

        }

        public PhotoArchiveVersion AddOrUpdatePhotoArchiveVersion(int pageRouteId, int photoArchiveVersionId)
        {
            var pageRouteVersion = _pageRouteVersionRepository.AddOrUpdatePageRouteVersion(pageRouteId);
            var photoArchiveVersion = GetVersion(photoArchiveVersionId);
            if (photoArchiveVersion.VersionStatusEnum == VersionStatusEnum.Approved || photoArchiveVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                var newVer = new PhotoArchiveVersion
                {
                    VersionStatusEnum = VersionStatusEnum.Draft,
                    ChangeActionEnum = ChangeActionEnum.Update,
                    CreationDate = DateTime.Now,
                    CreatedById = photoArchiveVersion.CreatedById,
                    IsActive = photoArchiveVersion.IsActive,
                    IsDeleted = false,
                    Date = DateTime.Now,
                    ArPhotoArchiveDesc = photoArchiveVersion.ArPhotoArchiveDesc,
                    ArPhotoArchiveName = photoArchiveVersion.ArPhotoArchiveName,
                    EnPhotoArchiveDesc = photoArchiveVersion.EnPhotoArchiveDesc,
                    EnPhotoArchiveName = photoArchiveVersion.EnPhotoArchiveName,
                    ImageUrl = photoArchiveVersion.ImageUrl,
                    PageRouteVersionId = pageRouteVersion.Id,
                    PhotoArchiveId = photoArchiveVersion.PhotoArchiveId,
                    Order = photoArchiveVersion.Order,
                    SeoDescriptionAR = photoArchiveVersion.SeoDescriptionAR,
                    SeoDescriptionEN = photoArchiveVersion.SeoDescriptionEN,
                    SeoOgTitleAR = photoArchiveVersion.SeoOgTitleAR,
                    SeoOgTitleEN = photoArchiveVersion.SeoOgTitleEN,
                    SeoTitleAR = photoArchiveVersion.SeoTitleAR,
                    SeoTitleEN = photoArchiveVersion.SeoTitleEN,
                    SeoTwitterCardAR = photoArchiveVersion.SeoTwitterCardAR,
                    SeoTwitterCardEN = photoArchiveVersion.SeoTwitterCardEN,
                    ArPhotoArchiveType = photoArchiveVersion.ArPhotoArchiveType,
                    EnPhotoArchiveType = photoArchiveVersion.EnPhotoArchiveType
                };
                _db.PhotoArchiveVersions.Add(newVer);
                _db.SaveChanges();
                return newVer;
            }
            else
            {
                photoArchiveVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                _db.PhotoArchiveVersions.Update(photoArchiveVersion);
                _db.SaveChanges();
                return photoArchiveVersion;
            }

        }

        public bool ApplySubmitRequest(int id, string userId, string pageLink)
        {
            try
            {
                var ifApprovalExist = _approvalNotificationsRepository.GetByPageNameAndRelatedId("Photo Archive", id);
                var photoArchiveVersion = _db.PhotoArchiveVersions.Find(id);
                var pageVer = _db.PageRouteVersions.Find(photoArchiveVersion.PageRouteVersionId);
                photoArchiveVersion.VersionStatusEnum = VersionStatusEnum.Submitted;
                _db.PhotoArchiveVersions.Update(photoArchiveVersion);
                if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    _db.ApprovalNotifications.Add(new ApprovalNotification
                    {
                        ChangeAction = photoArchiveVersion.ChangeActionEnum.Value,
                        ChangesDateTime = DateTime.Now,
                        ChangeType = ChangeType.PageContent,
                        ContentManagerId = userId,
                        PageLink = $"{pageLink}/{id}?pageRouteId={pageVer.PageRouteId}",
                        PageName = "Photo Archive",
                        PageType = PageType.Static,
                        RelatedVersionId = id,
                        VersionStatusEnum = VersionStatusEnum.Submitted,

                    });
                }

                var draftPhotoarchive = _db.PhotoArchiveVersions.Where(d => d.PageRouteVersionId == photoArchiveVersion.PageRouteVersionId && d.Id != id &
                d.VersionStatusEnum == VersionStatusEnum.Draft).Any();
                if (!draftPhotoarchive)
                {
                    pageVer.ContentVersionStatusEnum = VersionStatusEnum.Submitted;
                    _db.PageRouteVersions.Update(pageVer);
                }
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Approve(int id, int approvalId, string userId)
        {
            var photoarchiveVer = _db.PhotoArchiveVersions.Include(x => x.PhotosAlbumVersions).FirstOrDefault(d => d.Id == id);
            if (photoarchiveVer != null)
            {
                var pageRouteId = _db.PageRouteVersions.Find(photoarchiveVer.PageRouteVersionId).PageRouteId;
                PhotoArchive photoArchive;
                var photosAlbumsVersion = photoarchiveVer.PhotosAlbumVersions;

                if (photoarchiveVer.ChangeActionEnum == ChangeActionEnum.New)
                {
                    photoArchive = new PhotoArchive
                    {
                        ApprovalDate = DateTime.Now,
                        ApprovedById = userId,
                        CreatedById = photoarchiveVer.CreatedById,
                        CreationDate = photoarchiveVer.CreationDate,
                        IsActive = photoarchiveVer.IsActive,
                        SeoTwitterCardEN = photoarchiveVer.SeoTwitterCardEN,
                        SeoTwitterCardAR = photoarchiveVer.SeoTwitterCardAR,
                        SeoTitleEN = photoarchiveVer.SeoTitleEN,
                        SeoTitleAR = photoarchiveVer.SeoTitleAR,
                        SeoOgTitleEN = photoarchiveVer.SeoOgTitleEN,
                        SeoOgTitleAR = photoarchiveVer.SeoOgTitleAR,
                        SeoDescriptionEN = photoarchiveVer.SeoDescriptionEN,
                        SeoDescriptionAR = photoarchiveVer.SeoDescriptionAR,
                        Order = photoarchiveVer.Order,
                        ArPhotoArchiveDesc = photoarchiveVer.ArPhotoArchiveDesc,
                        ArPhotoArchiveName = photoarchiveVer.ArPhotoArchiveName,
                        ArPhotoArchiveType = photoarchiveVer.ArPhotoArchiveType,
                        EnPhotoArchiveDesc = photoarchiveVer.EnPhotoArchiveDesc,
                        EnPhotoArchiveName = photoarchiveVer.EnPhotoArchiveName,
                        EnPhotoArchiveType = photoarchiveVer.EnPhotoArchiveType,
                        ImageUrl = photoarchiveVer.ImageUrl,
                        IsDeleted = false,
                        PageRouteId = pageRouteId,


                    };
                    _db.PhotoArchive.Add(photoArchive);
                    _db.SaveChanges();

                    photoArchive = _db.PhotoArchive.FirstOrDefault(x => x.Id == photoArchive.Id);

                    AddPhotosAlbum(photoArchive, photoarchiveVer.PhotosAlbumVersions.ToList());

                    if (photoArchive.IsActive)
                        _photoArchiveElasticSearchService.AddAsync(photoArchive);
                }
                else if (photoarchiveVer.ChangeActionEnum == ChangeActionEnum.Update)
                {
                    photoArchive = _db.PhotoArchive.FirstOrDefault(x => x.Id == photoarchiveVer.PhotoArchiveId);
                    photoArchive.ApprovalDate = DateTime.Now;
                    photoArchive.ApprovedById = userId;

                    photoArchive.ApprovalDate = DateTime.Now;
                    photoArchive.ApprovedById = photoArchive.ApprovedById;
                    photoArchive.CreatedById = photoarchiveVer.CreatedById;
                    photoArchive.CreationDate = photoarchiveVer.CreationDate;
                    photoArchive.IsActive = photoarchiveVer.IsActive;
                    photoArchive.IsDeleted = photoarchiveVer.IsDeleted;
                    photoArchive.SeoTwitterCardEN = photoarchiveVer.SeoTwitterCardEN;
                    photoArchive.SeoTwitterCardAR = photoarchiveVer.SeoTwitterCardAR;
                    photoArchive.SeoTitleEN = photoarchiveVer.SeoTitleEN;
                    photoArchive.SeoTitleAR = photoarchiveVer.SeoTitleAR;
                    photoArchive.SeoOgTitleEN = photoarchiveVer.SeoOgTitleEN;
                    photoArchive.SeoOgTitleAR = photoarchiveVer.SeoOgTitleAR;
                    photoArchive.SeoDescriptionEN = photoarchiveVer.SeoDescriptionEN;
                    photoArchive.SeoDescriptionAR = photoarchiveVer.SeoDescriptionAR;
                    photoArchive.Order = photoarchiveVer.Order;
                    photoArchive.ArPhotoArchiveDesc = photoarchiveVer.ArPhotoArchiveDesc;
                    photoArchive.ArPhotoArchiveName = photoarchiveVer.ArPhotoArchiveName;
                    photoArchive.ArPhotoArchiveType = photoarchiveVer.ArPhotoArchiveType;
                    photoArchive.EnPhotoArchiveDesc = photoarchiveVer.EnPhotoArchiveDesc;
                    photoArchive.EnPhotoArchiveName = photoarchiveVer.EnPhotoArchiveName;
                    photoArchive.EnPhotoArchiveType = photoarchiveVer.EnPhotoArchiveType;


                    _db.PhotoArchive.Update(photoArchive);


                    _db.SaveChanges();

                    photoArchive = _db.PhotoArchive.Include(x => x.PhotosAlbums).FirstOrDefault(x => x.Id == photoArchive.Id);

                    DeletePhotosAlbum(photoArchive);

                    AddPhotosAlbum(photoArchive);

                    if (photoArchive.IsActive)
                    {
                        _photoArchiveElasticSearchService.DeleteAsync(photoArchive);
                        _photoArchiveElasticSearchService.AddAsync(photoArchive);
                    }
                    else
                    {
                        _photoArchiveElasticSearchService.DeleteAsync(photoArchive);
                    }

                }
                else
                {
                    photoArchive = _db.PhotoArchive.Find(photoarchiveVer.PhotoArchiveId);
                    photoArchive.IsDeleted = true;
                    _db.PhotoArchive.Update(photoArchive);
                    DeletePhotosAlbum(photoArchive);

                    _photoArchiveElasticSearchService.DeleteAsync(photoArchive);
                }




                _db.SaveChanges();

                photoarchiveVer.VersionStatusEnum = VersionStatusEnum.Approved;
                photoarchiveVer.PhotoArchiveId = photoArchive.Id;
                _db.PhotoArchiveVersions.Update(photoarchiveVer);

                var notification = _db.ApprovalNotifications.Find(approvalId);
                notification.VersionStatusEnum = VersionStatusEnum.Approved;
                _db.ApprovalNotifications.Update(notification);

                var submitedPhotoArchive = _db.PhotoArchiveVersions.Where(d => d.PageRouteVersionId == photoarchiveVer.PageRouteVersionId && d.Id != id &
               d.VersionStatusEnum == VersionStatusEnum.Submitted).Any();
                if (!submitedPhotoArchive)
                {
                    var pageVer = _db.PageRouteVersions.Find(photoarchiveVer.PageRouteVersionId);
                    pageVer.ContentVersionStatusEnum = VersionStatusEnum.Approved;
                    _db.PageRouteVersions.Update(pageVer);
                }
                _db.SaveChanges();

                try
                {
                    _globalElasticSearchService.DeleteAsync(pageRouteId ?? 0);
                    _globalElasticSearchService.AddAsync(_pageRouteRepository.GetPageData(pageRouteId ?? 0));
                }
                catch { }

                return true;
            }
            return false;
        }

        private void DeletePhotosAlbum(PhotoArchive photoArchive)
        {
            photoArchive.PhotosAlbums.ToList().ForEach(x => x.IsDeleted = true);
            _db.SaveChanges();
        }

        private void AddPhotosAlbum(PhotoArchive photoArchive, List<PhotosAlbumVersion> photosAlbumVersion = null)
        {
            if (photosAlbumVersion == null)
            {
                var photoArchiveVersions = _db.PhotoArchiveVersions.Where(x => x.PhotoArchiveId == photoArchive.Id && !x.IsDeleted && x.IsActive && x.VersionStatusEnum != VersionStatusEnum.Ignored).Select(x => x.Id).ToList();

                var photosAlbumsVersion = _db.PhotosAlbumVersions.Where(x => photoArchiveVersions.Contains(x.PhotoArchiveVersionId)).ToList();

                var notMapedToAlbum = photosAlbumsVersion.Where(x => x.PhotosAlbumId == null);

                var mappedToAlpum = photosAlbumsVersion.Where(x => x.PhotosAlbumId != null).GroupBy(x => x.PhotosAlbumId).Select(g => g.OrderByDescending(c => c.Id).FirstOrDefault()).ToList();

                mappedToAlpum.AddRange(notMapedToAlbum);

                foreach (var item in mappedToAlpum)
                {
                    var newPhotoAlbum = new PhotosAlbum()
                    {
                        PhotoArchiveId = photoArchive.Id,
                        Order = item.Order,
                        ImageName = item.ImageName,
                        ImagePath = item.ImagePath,
                        SeoTwitterCardEN = item.SeoTwitterCardEN,
                        SeoTwitterCardAR = item.SeoTwitterCardAR,
                        SeoTitleEN = item.SeoTitleEN,
                        SeoTitleAR = item.SeoTitleAR,
                        SeoOgTitleEN = item.SeoOgTitleEN,
                        SeoOgTitleAR = item.SeoOgTitleAR,
                        SeoDescriptionEN = item.SeoDescriptionEN,
                        SeoDescriptionAR = item.SeoDescriptionAR,
                        IsActive = item.IsActive,
                        ArPhotosAlbumDesc = item.ArPhotosAlbumDesc,
                        ArPhotosAlbumName = item.ArPhotosAlbumName,
                        CreationDate = item.CreationDate,
                        CreatedById = item.CreatedById,
                        IsDeleted = item.IsDeleted,
                        EnPhotosAlbumDesc = item.EnPhotosAlbumDesc,
                        EnPhotosAlbumName = item.EnPhotosAlbumName,
                    };
                    _db.PhotosAlbum.Add(newPhotoAlbum);
                    _db.SaveChanges();
                    item.PhotosAlbumId = newPhotoAlbum.Id;
                }
            }
            else
            {
                foreach (var item in photosAlbumVersion)
                {
                    var newPhotoAlbum = new PhotosAlbum()
                    {
                        PhotoArchiveId = photoArchive.Id,
                        Order = item.Order,
                        ImageName = item.ImageName,
                        ImagePath = item.ImagePath,
                        SeoTwitterCardEN = item.SeoTwitterCardEN,
                        SeoTwitterCardAR = item.SeoTwitterCardAR,
                        SeoTitleEN = item.SeoTitleEN,
                        SeoTitleAR = item.SeoTitleAR,
                        SeoOgTitleEN = item.SeoOgTitleEN,
                        SeoOgTitleAR = item.SeoOgTitleAR,
                        SeoDescriptionEN = item.SeoDescriptionEN,
                        SeoDescriptionAR = item.SeoDescriptionAR,
                        IsActive = item.IsActive,
                        ArPhotosAlbumDesc = item.ArPhotosAlbumDesc,
                        ArPhotosAlbumName = item.ArPhotosAlbumName,
                        CreationDate = item.CreationDate,
                        CreatedById = item.CreatedById,
                        IsDeleted = item.IsDeleted,
                        EnPhotosAlbumDesc = item.EnPhotosAlbumDesc,
                        EnPhotosAlbumName = item.EnPhotosAlbumName,
                    };
                    _db.PhotosAlbum.Add(newPhotoAlbum);
                    _db.SaveChanges();
                    item.PhotosAlbumId = newPhotoAlbum.Id;
                }
            }

            _db.SaveChanges();
        }

        public bool Ignore(int id, int approvalId, string userId)
        {
            var photoArchiveVer = _db.PhotoArchiveVersions.Include(x => x.PhotosAlbumVersions).First(d => d.Id == id);
            if (photoArchiveVer != null)
            {
                if (photoArchiveVer.ChangeActionEnum == ChangeActionEnum.Delete)
                {
                    photoArchiveVer.ChangeActionEnum = ChangeActionEnum.Update;
                    photoArchiveVer.VersionStatusEnum = VersionStatusEnum.Approved;
                    photoArchiveVer.IsDeleted = false;

                }
                else
                {
                    photoArchiveVer.VersionStatusEnum = VersionStatusEnum.Ignored;

                    if (photoArchiveVer.PhotoArchiveId != null)
                    {
                        var photoAlbums = _db.PhotosAlbum.Where(x => x.PhotoArchiveId == photoArchiveVer.PhotoArchiveId);

                        photoArchiveVer.PhotosAlbumVersions.ToList().ForEach(x => x.IsDeleted = true);
                        foreach (var item in photoAlbums)
                        {
                            _db.PhotosAlbumVersions.Add(new PhotosAlbumVersion()
                            {
                                VersionStatusEnum = VersionStatusEnum.Draft,
                                ChangeActionEnum = ChangeActionEnum.New,
                                SeoTwitterCardEN = item.SeoTwitterCardEN,
                                SeoTwitterCardAR = item.SeoTwitterCardAR,
                                SeoTitleEN = item.SeoTitleEN,
                                SeoTitleAR = item.SeoTitleAR,
                                SeoOgTitleEN = item.SeoOgTitleEN,
                                SeoOgTitleAR = item.SeoOgTitleAR,
                                SeoDescriptionEN = item.SeoDescriptionEN,
                                SeoDescriptionAR = item.SeoDescriptionAR,
                                Order = item.Order,
                                IsDeleted = item.IsDeleted,
                                IsActive = item.IsActive,
                                ImagePath = item.ImagePath,
                                EnPhotosAlbumDesc = item.EnPhotosAlbumDesc,
                                PhotosAlbumId = item.Id,
                                PhotoArchiveVersionId = photoArchiveVer.Id,
                                ImageName = item.ImageName,
                                EnPhotosAlbumName = item.EnPhotosAlbumName,
                                CreationDate = item.CreationDate,
                                CreatedById = item.CreatedById,
                                ApprovalDate = item.ApprovalDate,
                                ApprovedById = item.ApprovedById,
                                ArPhotosAlbumDesc = item.ArPhotosAlbumDesc,
                                ArPhotosAlbumName = item.ArPhotosAlbumName,

                            });
                        }
                    }



                }
                _db.PhotoArchiveVersions.Update(photoArchiveVer);
                var notification = _db.ApprovalNotifications.Find(approvalId);
                notification.VersionStatusEnum = VersionStatusEnum.Ignored;
                _db.ApprovalNotifications.Update(notification);

                _db.SaveChanges();

                return true;
            }
            return false;
        }

        public bool DeleteVer(int id, string userId, string pageLink)
        {
            try
            {
                var photoArchiveVer = _db.PhotoArchiveVersions.FirstOrDefault(x => x.Id == id);
                photoArchiveVer.IsDeleted = true;
                photoArchiveVer.ChangeActionEnum = ChangeActionEnum.Delete;
                photoArchiveVer.VersionStatusEnum = VersionStatusEnum.Submitted;
                _db.PhotoArchiveVersions.Update(photoArchiveVer);
                if (photoArchiveVer.PhotoArchiveId != null)
                {
                    var pageVer = _db.PageRouteVersions.Find(photoArchiveVer.PageRouteVersionId);
                    _db.ApprovalNotifications.Add(new ApprovalNotification
                    {
                        ChangeAction = photoArchiveVer.ChangeActionEnum.Value,
                        ChangesDateTime = DateTime.Now,
                        ChangeType = ChangeType.PageContent,
                        ContentManagerId = userId,
                        PageLink = $"{pageLink}/{id}?pageRouteId={pageVer.PageRouteId}",
                        PageName = "Photo Archive",
                        PageType = PageType.Static,
                        RelatedVersionId = id,
                        VersionStatusEnum = VersionStatusEnum.Submitted,

                    });
                }
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
