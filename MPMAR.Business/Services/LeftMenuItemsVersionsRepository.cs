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
    public class LeftMenuItemsVersionsRepository : ILeftMenuItemsVersionsRepository
    {
        private readonly ApplicationDbContext _db;
        public LeftMenuItemsVersionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(LeftMenuItemVersions model)
        {
            _db.LeftMenuItemVersions.Add(model);
            _db.SaveChanges();
        }

        public bool Update(LeftMenuItemVersions model)
        {
            try
            {
                _db.LeftMenuItemVersions.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<LeftMenuItemVersions> GetLeftMenuItemVersions()
        {
            //join between version and non version LeftMenuItem take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from lmi in _db.LeftMenuItem.Where(d => !d.IsDeleted)
                              from lmiv in _db.LeftMenuItemVersions.Where(d => d.LeftMenuItemId == lmi.Id && !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new LeftMenuItemVersions
                              {
                                  Id = lmi.Id,
                                  LeftMenuType = (lmiv != null && (lmiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || lmiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? lmiv.LeftMenuType : lmi.LeftMenuType,
                                  ImagePath = (lmiv != null && (lmiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || lmiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? lmiv.ImagePath : lmi.ImagePath,
                                  Link = (lmiv != null && (lmiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || lmiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? lmiv.Link : lmi.Link,
                                  EnTitle = (lmiv != null && (lmiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || lmiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? lmiv.EnTitle : lmi.EnTitle,
                                  ArTitle = (lmiv != null && (lmiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || lmiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? lmiv.ArTitle : lmi.ArTitle,
                                  Order = (lmiv != null && (lmiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || lmiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? lmiv.Order : lmi.Order,
                                  IsActive = (lmiv != null && (lmiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || lmiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? lmiv.IsActive : lmi.IsActive,
                                  IsDeleted = (lmiv != null && (lmiv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || lmiv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? lmiv.IsDeleted : lmi.IsDeleted,
                                  LeftMenuItemId = lmi.Id,
                                  VersionStatusEnum = lmiv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = lmiv.ChangeActionEnum ?? ChangeActionEnum.New,
                              });

            return queryright.ToList();
        }

        public LeftMenuItemVersions Get(int id)
        {
            return _db.LeftMenuItemVersions.FirstOrDefault(i => i.Id == id);
        }

        public LeftMenuItemVersions GetByLeftMenuItemId(int id)
        {
            return _db.LeftMenuItemVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.LeftMenuItemId == id);
        }

        public IEnumerable<LeftMenuItemVersions> GetAllDrafts()
        {
            return _db.LeftMenuItemVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }

        public IEnumerable<LeftMenuItemVersions> GetAllSubmitted()
        {
            return _db.LeftMenuItemVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
