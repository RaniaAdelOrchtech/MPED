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
    public class FooterMenuTitleVersionsRepository : IFooterMenuTitleVersionsRepository
    {
        private readonly ApplicationDbContext _db;
        public FooterMenuTitleVersionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add footer menu title version to database
        /// </summary>
        /// <param name="model">footer menu title version</param>
        /// <returns></returns>
        public void Add(FooterMenuTitleVersions model)
        {
            _db.FooterMenuTitleVersions.Add(model);
            _db.SaveChanges();
        }

        /// <summary>
        /// Update fotter menu title version from database
        /// </summary>
        /// <param name="model">footer menu title version new data</param>
        /// <returns>true if updated false otherwise</returns>
        public bool Update(FooterMenuTitleVersions model)
        {
            try
            {
                _db.FooterMenuTitleVersions.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Get all footer menu title versions
        /// </summary>
        /// <returns>List of foooter menu title versions objects</returns>
        public List<FooterMenuTitleVersions> GetFoorterMenuTitleVersions()
        {
            //join between version and non version FooterMenuTitles take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from fmt in _db.FooterMenuTitles.Where(d => !d.IsDeleted)
                              from fmtv in _db.FooterMenuTitleVersions.Where(d => d.FooterMenuTitleId == fmt.Id && !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new FooterMenuTitleVersions
                              {
                                  Id = fmt.Id,

                                  EnTitle = (fmtv != null && (fmtv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || fmtv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? fmtv.EnTitle : fmt.EnTitle,
                                  ArTitle = (fmtv != null && (fmtv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || fmtv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? fmtv.ArTitle : fmt.ArTitle,
                                  Order = (fmtv != null && (fmtv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || fmtv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? fmtv.Order : fmt.Order,
                                  IsActive = (fmtv != null && (fmtv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || fmtv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? fmtv.IsActive : fmt.IsActive,
                                  IsDeleted = (fmtv != null && (fmtv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || fmtv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? fmtv.IsDeleted : fmt.IsDeleted,
                                  FooterMenuTitleId = fmt.Id,
                                  VersionStatusEnum = fmtv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = fmtv.ChangeActionEnum ?? ChangeActionEnum.New,
                              });

            return queryright.ToList();
        }

        /// <summary>
        /// Get footer menu title version object by id
        /// </summary>
        /// <param name="id">footer menu title version id</param>
        /// <returns>footer menu item version object</returns>
        public FooterMenuTitleVersions Get(int id)
        {
            return _db.FooterMenuTitleVersions.FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// get footer menu title version by footer menu title id
        /// </summary>
        /// <param name="id">footer menu title id</param>
        /// <returns>footer menu title version object</returns>
        public FooterMenuTitleVersions GetByFooterMenuTitleId(int id)
        {
            return _db.FooterMenuTitleVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.FooterMenuTitleId == id);
        }

        /// <summary>
        /// Get all drafts objects
        /// </summary>
        /// <returns>Ienumerable of footer menu title versions objects</returns>
        public IEnumerable<FooterMenuTitleVersions> GetAllDrafts()
        {
            return _db.FooterMenuTitleVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }

        /// <summary>
        /// Get all submitted objects
        /// </summary>
        /// <returns>Ienumerable of footer menu title versions objects</returns>
        public IEnumerable<FooterMenuTitleVersions> GetAllSubmitted()
        {
            return _db.FooterMenuTitleVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
