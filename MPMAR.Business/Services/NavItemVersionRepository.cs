using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Business.Services
{
    public class NavItemVersionRepository : INavItemVersionRepository
    {
        private readonly ApplicationDbContext _db;

        public NavItemVersionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public NavItemVersion Add(NavItemVersion navItemVersion)
        {
            try
            {
                _db.NavItemVersions.Add(navItemVersion);
                _db.SaveChanges();
                return navItemVersion;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public NavItemVersion Update(NavItemVersion navItemVersion)
        {
            try
            {
                _db.NavItemVersions.Update(navItemVersion);
                _db.SaveChanges();
                return navItemVersion;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public NavItemVersion Delete(int id)
        {
            try
            {
                var item = _db.NavItemVersions.FirstOrDefault(x => x.Id == id);
                item.IsDeleted = true;
                Update(item);

                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public NavItemVersion ApplyEditRequest(int id)
        {
            try
            {
                var item = _db.NavItemVersions.FirstOrDefault(x => x.Id == id);

                _db.NavItemVersions.Attach(item);
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public NavItemVersion Get(int id)
        {
            return _db.NavItemVersions.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<NavItemVersion> Get()
        {
            //join between version and non version NavItems take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from pm in _db.NavItems.Where(d => !d.IsDeleted).DefaultIfEmpty()
                              from pmv in _db.NavItemVersions.Where(d => d.NavItemId == pm.Id && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new NavItemVersion
                              {
                                  NavItemId = pm.Id,
                                  Id = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.Id : pm.Id,
                                  ArName = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ArName : pm.ArName,

                                  EnName = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.EnName : pm.EnName,

                                  Order = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.Order : pm.Order,



                                  ParentNavItemId = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ParentNavItemId : pm.ParentNavItemId,


                                  IsActive = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsActive : pm.IsActive,

                                  IsDeleted = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsDeleted : pm.IsDeleted,

                                  VersionStatusEnum = pmv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = pmv.ChangeActionEnum ?? ChangeActionEnum.Update
                              });


            //get the rest from NavItemVersions that wasn't included in previous join 
            var queryleft = (from prv in _db.NavItemVersions.Where(d => !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                             where !_db.NavItems.Any(d => d.Id == prv.NavItemId)
                             select new NavItemVersion
                             {
                                 Id = prv.Id,
                                 NavItemId = prv.NavItemId,
                                 ParentNavItemId = prv.ParentNavItemId,
                                 Order = prv.Order,
                                 EnName = prv.EnName,
                                 ArName = prv.ArName,
                                 IsActive = prv.IsActive,
                                 IsDeleted = prv.IsDeleted,
                                 VersionStatusEnum = prv.VersionStatusEnum,
                                 ChangeActionEnum = prv.ChangeActionEnum,

                             });
            return queryright.Union(queryleft).OrderBy(x => x.Order).Where(x => !x.IsDeleted).ToList();

        }

        public IEnumerable<NavItemVersion> GetAllDrafts()
        {
            return _db.NavItemVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }
        public IEnumerable<NavItemVersion> GetAllSubmitted()
        {
            return _db.NavItemVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }

        public NavItemVersion GetByNavId(int navId)
        {
            return _db.NavItemVersions.Include(x=>x.NavItem.PageRoutes).Include(x=>x.NavItem.NavItemList).OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.NavItemId == navId);
        }
    }
}
