using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class HP_PhotosVersionRepository : IHP_PhotosVersionRepository
    {
        private readonly ApplicationDbContext _db;

        public HP_PhotosVersionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(HomePagePhotoVersions model)
        {
            _db.HomePagePhotoVersions.Add(model);
            _db.SaveChanges();
        }

        public bool Update(HomePagePhotoVersions model)
        {
            try
            {
                _db.HomePagePhotoVersions.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<HomePagePhotoVersions> GetPhotosVersions()
        {
            //join between version and non version homePagePhotos take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from ph in _db.homePagePhotos.Where(d => !d.IsDeleted)
                              from phv in _db.HomePagePhotoVersions.Where(d => d.HomePagePhotoId == ph.Id && !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new HomePagePhotoVersions
                              {
                                  Id = ph.Id,
                                  EnDescription = (phv != null && (phv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || phv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? phv.EnDescription : ph.EnDescription,
                                  ArDescription = (phv != null && (phv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || phv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? phv.ArDescription : ph.ArDescription,
                                  EnTitle = (phv != null && (phv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || phv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? phv.EnTitle : ph.EnTitle,
                                  ArTitle = (phv != null && (phv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || phv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? phv.ArTitle : ph.ArTitle,
                                  ImageUrl = (phv != null && (phv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || phv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? phv.ImageUrl : ph.ImageUrl,
                                  Url = (phv != null && (phv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || phv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? phv.Url : ph.Url,
                                  IsActive = (phv != null && (phv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || phv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? phv.IsActive : ph.IsActive,
                                  IsDeleted = (phv != null && (phv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || phv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? phv.IsDeleted : ph.IsDeleted,
                                  HomePagePhotoId = ph.Id,
                                  VersionStatusEnum = phv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = phv.ChangeActionEnum ?? ChangeActionEnum.New,
                              });

            return queryright.ToList();
        }

        public HomePagePhotoVersions Get(int id)
        {
            return _db.HomePagePhotoVersions.FirstOrDefault(i => i.Id == id);
        }

        public HomePagePhotoVersions GetByPhotoId(int id)
        {
            return _db.HomePagePhotoVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.HomePagePhotoId == id);
        }

        public IEnumerable<HomePagePhotoVersions> GetAllDrafts()
        {
            return _db.HomePagePhotoVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }

        public IEnumerable<HomePagePhotoVersions> GetAllSubmitted()
        {
            return _db.HomePagePhotoVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
