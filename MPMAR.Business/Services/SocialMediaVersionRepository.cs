using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class SocialMediaVersionRepository : ISocialMediaVersionRepository
    {
        private readonly ApplicationDbContext _db;

        public SocialMediaVersionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public SocialMediaVersion Add(SocialMediaVersion socialMedia)
        {
            try
            {
                _db.SocialMediaVersions.Add(socialMedia);
                _db.SaveChanges();

                return _db.SocialMediaVersions.FirstOrDefault(c => c.Id == socialMedia.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SocialMediaVersion Update(SocialMediaVersion socialMedia)
        {
            try
            {

                _db.SocialMediaVersions.Update(socialMedia);
                _db.SaveChanges();

                return _db.SocialMediaVersions.FirstOrDefault(c => c.Id == socialMedia.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public IEnumerable<SocialMediaVersion> GetFooterSocialMediaLink()
        {
            var socialMedias = _db.SocialMediaVersions.OrderBy(s => s.Id).ToList();
            return socialMedias;
        }


        public bool Delete(int id)
        {
            try
            {
                var item = _db.SocialMediaVersions.FirstOrDefault(x => x.Id == id);

                item.IsDeleted = true;
                Update(item);


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public SocialMediaVersion GetById(int id)
        {

            return _db.SocialMediaVersions.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }
        public SocialMediaVersion GetDetail(int id)
        {

            return _db.SocialMediaVersions.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<SocialMediaVersion> GetAll()
        {
            //join between version and non version SocialMedia take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from pm in _db.SocialMedia.Where(d => !d.IsDeleted).DefaultIfEmpty()
                              from pmv in _db.SocialMediaVersions.Where(d => d.SocialMediaId == pm.Id && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new SocialMediaVersion
                              {
                                  SocialMediaId = pm.Id,
                                  Id = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.Id : pm.Id,
                                  Link = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.Link : pm.Link,

                                  Order = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.Order : pm.Order,

                                  SocialMediaName = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.SocialMediaName : pm.SocialMediaName,


                                  IsActive = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsActive : pm.IsActive,

                                  IsDeleted = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsDeleted : pm.IsDeleted,

                                  VersionStatusEnum = pmv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = pmv.ChangeActionEnum ?? ChangeActionEnum.Update
                              });

            //get the rest from SocialMediaVersions that wasn't included in previous join 
            var queryleft = (from prv in _db.SocialMediaVersions.Where(d => !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                             where !_db.SocialMedia.Any(d => d.Id == prv.SocialMediaId)
                             select new SocialMediaVersion
                             {
                                 Id = prv.Id,
                                 Link = prv.Link,
                                 SocialMediaName = prv.SocialMediaName,
                                 Order = prv.Order,
                                 SocialMediaId = prv.SocialMediaId,
                                 IsActive = prv.IsActive,
                                 IsDeleted = prv.IsDeleted,
                                 VersionStatusEnum = prv.VersionStatusEnum,
                                 ChangeActionEnum = prv.ChangeActionEnum,

                             });
            return queryright.Union(queryleft).OrderBy(x => x.Order).Where(x => !x.IsDeleted).ToList();
        }

        public IEnumerable<SocialMediaVersion> GetAllDrafts()
        {
            return _db.SocialMediaVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }
        public IEnumerable<SocialMediaVersion> GetAllSubmitted()
        {
            return _db.SocialMediaVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }

        public SocialMediaVersion GetBySocialId(int socialId)
        {
            return _db.SocialMediaVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.SocialMediaId == socialId);
        }
    }
}
