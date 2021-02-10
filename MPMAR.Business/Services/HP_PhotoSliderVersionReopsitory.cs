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
    public class HP_PhotoSliderVersionReopsitory : IHP_PhotoSliderVersionReopsitory
    {
        private readonly ApplicationDbContext _db;
        public HP_PhotoSliderVersionReopsitory(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<HomePagePhotoSliderVersion> GetAll()
        {
            //join between version and non version homePagePhotoSlider take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from pm in _db.homePagePhotoSlider.Where(d => !d.IsDeleted).DefaultIfEmpty()
                              from pmv in _db.HomePagePhotoSliderVersions.Where(d => d.HomePagePhotoSliderId == pm.Id && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new HomePagePhotoSliderVersion
                              {
                                  HomePagePhotoSliderId = pm.Id,
                                  Id = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.Id : pm.Id,
                                  ArDescription = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ArDescription : pm.ArDescription,
                                  ArTitle = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ArTitle : pm.ArTitle,

                                  EnDescription = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.EnDescription : pm.EnDescription,

                                  EnTitle = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.EnTitle : pm.EnTitle,

                                  Url = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.Url : pm.Url,

                                  ImageUrl = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ImageUrl : pm.ImageUrl,

                                  IsActive = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsActive : pm.IsActive,

                                  IsDeleted = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsDeleted : pm.IsDeleted,

                                  VersionStatusEnum = pmv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = pmv.ChangeActionEnum ?? ChangeActionEnum.Update
                              });

            //return queryright.ToList();


            //get the rest from HomePagePhotoSliderVersions that wasn't included in previous join 
            var queryleft = (from prv in _db.HomePagePhotoSliderVersions.Where(d => !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                             where !_db.homePagePhotoSlider.Any(d => d.Id == prv.HomePagePhotoSliderId)
                             select new HomePagePhotoSliderVersion
                             {
                                 Id = prv.Id,
                                 HomePagePhotoSliderId = prv.HomePagePhotoSliderId,
                                 ArDescription = prv.ArDescription,
                                 EnDescription = prv.EnDescription,
                                 ArTitle = prv.ArTitle,
                                 EnTitle = prv.EnTitle,
                                 Url = prv.Url,
                                 ImageUrl = prv.ImageUrl,
                                 IsActive = prv.IsActive,
                                 IsDeleted = prv.IsDeleted,
                                 VersionStatusEnum = prv.VersionStatusEnum,
                                 ChangeActionEnum = prv.ChangeActionEnum,

                             });
            return queryright.Union(queryleft).Where(x => !x.IsDeleted).ToList();

        }

        public bool SoftDelete(int id)
        {
            try
            {
                var model = _db.HomePagePhotoSliderVersions.FirstOrDefault(x => x.Id == id);
                if (model != null)
                {
                    model.IsDeleted = true;

                    Update(model);

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public void Add(HomePagePhotoSliderVersion photoSlider)
        {
            _db.HomePagePhotoSliderVersions.Add(photoSlider);
            _db.SaveChanges();
            //_db.Entry(photoSlider).State = EntityState.Detached;
        }


        public HomePagePhotoSliderVersion GetById(int id)
        {
            return _db.HomePagePhotoSliderVersions.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Update(HomePagePhotoSliderVersion homePagePhotoSlider)
        {
            _db.HomePagePhotoSliderVersions.Update(homePagePhotoSlider);
            _db.SaveChanges();
        }

        public HomePagePhotoSliderVersion GetBySliderId(int id)
        {
            return _db.HomePagePhotoSliderVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.HomePagePhotoSliderId == id);
        }

        public IEnumerable<HomePagePhotoSliderVersion> GetAllDrafts()
        {
            return _db.HomePagePhotoSliderVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }
        public IEnumerable<HomePagePhotoSliderVersion> GetAllSubmitted()
        {
            return _db.HomePagePhotoSliderVersions.Where(e =>  e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
