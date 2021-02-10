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
    public class FooterMenuItemVersionRepository : IFooterMenuItemVersionRepository
    {

        private readonly ApplicationDbContext _db;

        public FooterMenuItemVersionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add footer menu item version object to database
        /// </summary>
        /// <param name="footerMenuItem">footer menu item version data</param>
        /// <returns>Added object</returns>
        public FooterMenuItemVersion Add(FooterMenuItemVersion footerMenuItem)
        {
            try
            {
                _db.FooterMenuItemVersions.Add(footerMenuItem);
                _db.SaveChanges();

                return _db.FooterMenuItemVersions.FirstOrDefault(c => c.Id == footerMenuItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// update footer menu item version object from database
        /// </summary>
        /// <param name="footerMenuItem">footer menu item version data</param>
        /// <returns>updated object</returns>
        public FooterMenuItemVersion Update(FooterMenuItemVersion footerMenuItem)
        {
            try
            {

                _db.FooterMenuItemVersions.Update(footerMenuItem);
                _db.SaveChanges();

                return _db.FooterMenuItemVersions.FirstOrDefault(c => c.Id == footerMenuItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get all footer menu item versions objects
        /// </summary>
        /// <returns>Ienumerable of footer menu item versions objects</returns>
        public IEnumerable<FooterMenuItemVersion> GetFooterMenuItemId()
        {
            //join between version and non version FooterMenuItem take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from pm in _db.FooterMenuItem.Where(d => !d.IsDeleted).DefaultIfEmpty()
                              from pmv in _db.FooterMenuItemVersions.Where(d => d.FooterMenuItemId == pm.Id && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new FooterMenuItemVersion
                              {
                                  FooterMenuItemId = pm.Id,
                                  Id = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.Id : pm.Id,
                                  EnTitle = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.EnTitle : pm.EnTitle,

                                  EnColumnPostion = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.EnColumnPostion : pm.EnColumnPostion,

                                  Link = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.Link : pm.Link,



                                  Order = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.Order : pm.Order,

                                  FooterMenuTitleId = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.FooterMenuTitleId : pm.FooterMenuTitleId,

                                  ArTitle = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ArTitle : pm.ArTitle,

                                  ArColumnPostion = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ArColumnPostion : pm.ArColumnPostion,


                                  IsActive = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsActive : pm.IsActive,

                                  IsDeleted = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsDeleted : pm.IsDeleted,

                                  VersionStatusEnum = pmv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = pmv.ChangeActionEnum ?? ChangeActionEnum.Update
                              });


            //get the rest from FooterMenuItemVersions that wasn't included in previous join 
            var queryleft = (from prv in _db.FooterMenuItemVersions.Where(d => !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                             where !_db.FooterMenuItem.Any(d => d.Id == prv.FooterMenuItemId)
                             select new FooterMenuItemVersion
                             {
                                 Id = prv.Id,
                                 FooterMenuItemId = prv.FooterMenuItemId,
                                 EnTitle = prv.EnTitle,
                                 EnColumnPostion = prv.EnColumnPostion,
                                 Link = prv.Link,
                                 Order = prv.Order,
                                 FooterMenuTitleId = prv.FooterMenuTitleId,
                                 ArTitle = prv.ArTitle,
                                 ArColumnPostion = prv.ArColumnPostion,
                                 IsActive = prv.IsActive,
                                 IsDeleted = prv.IsDeleted,
                                 VersionStatusEnum = prv.VersionStatusEnum,
                                 ChangeActionEnum = prv.ChangeActionEnum,

                             });
            return queryright.Union(queryleft).OrderBy(x => x.Order).Where(x => !x.IsDeleted).ToList();
        }

        /// <summary>
        /// delete footer menu item version
        /// </summary>
        /// <param name="id">footer menu item version id</param>
        /// <returns>True if deleted false otherwise</returns>
        public bool Delete(int id)
        {
            try
            {
                var item = _db.FooterMenuItemVersions.FirstOrDefault(x => x.Id == id);

                item.IsDeleted = true;
                Update(item);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Get footer menu item version object
        /// </summary>
        /// <param name="id">footer menu item version id</param>
        /// <returns>single footer menu item version</returns>
        public FooterMenuItemVersion Get(int id)
        {

            return _db.FooterMenuItemVersions.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Get footer menu item version object
        /// </summary>
        /// <param name="id">footer menu item version id</param>
        /// <returns>single footer menu item version</returns>
        public FooterMenuItemVersion GetDetail(int id)
        {
            //!(p.IsDeleted && p.PageRouteVersion.Status.Id == (int)RequestStatus.Approved) &&

            return _db.FooterMenuItemVersions.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Get all drafts
        /// </summary>
        /// <returns>Ienumarable from drafts footer menu items versions</returns>
        public IEnumerable<FooterMenuItemVersion> GetAllDrafts()
        {
            return _db.FooterMenuItemVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }

        /// <summary>
        /// Get all submitted
        /// </summary>
        /// <returns>Ienumarable from submitted footer menu items versions</returns>
        public IEnumerable<FooterMenuItemVersion> GetAllSubmitted()
        {
            return _db.FooterMenuItemVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }

        /// <summary>
        /// get footer menu item version which contain item id sent in parameter
        /// </summary>
        /// <param name="itemId">item id</param>
        /// <returns>footer menu item version object</returns>
        public FooterMenuItemVersion GetByItemId(int itemId)
        {
            return _db.FooterMenuItemVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.FooterMenuItemId == itemId);
        }
    }
}
