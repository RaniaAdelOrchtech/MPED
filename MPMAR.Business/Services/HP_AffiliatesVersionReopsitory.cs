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
    public class HP_AffiliatesVersionReopsitory : IHP_AffiliatesVersionReopsitory
    {
        private readonly ApplicationDbContext _db;
        public HP_AffiliatesVersionReopsitory(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<HomePageAffiliatesVersions> GetAll()
        {
            //join between version and non version HomePageAffiliates take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from pm in _db.HomePageAffiliates.Where(d => !d.IsDeleted).DefaultIfEmpty()
                              from pmv in _db.HomePageAffiliatesVersions.Where(d => d.HomePageAffiliatesId == pm.Id && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new HomePageAffiliatesVersions
                              {
                                  HomePageAffiliatesId = pm.Id,
                                  Id = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.Id : pm.Id,
                                  ArDescription = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ArDescription : pm.ArDescription,
                                  Type = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.Type : pm.Type,

                                  EnDescription = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.EnDescription : pm.EnDescription,



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


            //get the rest from HomePageAffiliatesVersions that wasn't included in previous join 
            var queryleft = (from prv in _db.HomePageAffiliatesVersions.Where(d => !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                             where !_db.HomePageAffiliates.Any(d => d.Id == prv.HomePageAffiliatesId)
                             select new HomePageAffiliatesVersions
                             {
                                 Id = prv.Id,
                                 HomePageAffiliatesId = prv.HomePageAffiliatesId,
                                 ArDescription = prv.ArDescription,
                                 EnDescription = prv.EnDescription,
                                 Type = prv.Type,
                                 Url = prv.Url,
                                 ImageUrl = prv.ImageUrl,
                                 IsActive = prv.IsActive,
                                 IsDeleted = prv.IsDeleted,
                                 VersionStatusEnum = prv.VersionStatusEnum,
                                 ChangeActionEnum = prv.ChangeActionEnum,

                             });
            return queryright.Union(queryleft).OrderBy(x => x.Type).Where(x => !x.IsDeleted).ToList();
        }

        public HomePageAffiliatesVersions GetById(int id)
        {
            return _db.HomePageAffiliatesVersions.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Update(HomePageAffiliatesVersions homePageAffiliates)
        {
            _db.HomePageAffiliatesVersions.Update(homePageAffiliates);
            _db.SaveChanges();
        }

        public void Add(HomePageAffiliatesVersions homePageAffiliates)
        {
            _db.HomePageAffiliatesVersions.Add(homePageAffiliates);
            _db.SaveChanges();
        }

        public bool SoftDelete(int id)
        {
            try
            {
                var model = _db.HomePageAffiliatesVersions.FirstOrDefault(x => x.Id == id);
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

        public HomePageAffiliatesVersions GetByAffilitId(int id)
        {
            return _db.HomePageAffiliatesVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.HomePageAffiliatesId == id);
        }

        public IEnumerable<HomePageAffiliatesVersions> GetAllDrafts()
        {
            return _db.HomePageAffiliatesVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }
        public IEnumerable<HomePageAffiliatesVersions> GetAllSubmitted()
        {
            return _db.HomePageAffiliatesVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
