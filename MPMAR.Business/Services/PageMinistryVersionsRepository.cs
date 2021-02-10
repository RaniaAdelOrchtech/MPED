using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Business.Services
{
    public class PageMinistryVersionsRepository : IPageMinistryVersionsRepository
    {
        private readonly ApplicationDbContext _db;

        public PageMinistryVersionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public PageMinistryVersion Add(PageMinistryVersion pageMinistry)
        {
            try
            {
                pageMinistry.StatusId = (int)RequestStatus.Approved;
                _db.PageMinistryVersions.Add(pageMinistry);
                _db.SaveChanges();
                return _db.PageMinistryVersions.FirstOrDefault(c => c.Id == pageMinistry.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PageMinistryVersion Update(PageMinistryVersion pageMinistry)
        {

            try
            {
                _db.PageMinistryVersions.Update(pageMinistry);
                _db.SaveChanges();
                return pageMinistry;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<PageMinistryVersion> GetPageMinistryByPageId(int pageRouteId)
        {

            var pageMinistrys = _db.PageMinistryVersions.Where(s => s.PageRouteId == pageRouteId && (s.VersionStatusEnum == VersionStatusEnum.Submitted || s.VersionStatusEnum == VersionStatusEnum.Draft) && !s.IsDeleted).OrderBy(s => s.Id).ToList();

            return pageMinistrys;
        }

  

        public bool Delete(int id)
        {
            try
            {
                var item = _db.PageMinistry.FirstOrDefault(x => x.Id == id);
                _db.PageMinistry.Remove(item);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public PageMinistryVersion GetByPageRouteId(int id)
        {

            return _db.PageMinistryVersions.Include(x => x.PageRoute).FirstOrDefault(p => p.PageRouteId == id);
        }
        public PageMinistryVersion GetDetail(int id)
        {

            return _db.PageMinistryVersions.Include(x => x.PageRoute).FirstOrDefault(p => p.Id == id);
        }

        public List<PageMinistryVersion> GetMinistries(int pageRoteId)
        {

            //join between version and non version PageMinistry take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from pm in _db.PageMinistry.Where(d => !d.IsDeleted && d.PageRouteId == pageRoteId).DefaultIfEmpty()
                              from pmv in _db.PageMinistryVersions.Where(d => d.PageMinistryId == pm.Id && !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored && d.PageRouteId == pageRoteId)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new PageMinistryVersion
                              {
                                  Id = pm.Id,
                                  PageMinistryId = pm.Id,
                                  PageRouteId = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.PageRouteId : pm.PageRouteId,
                                  ArName = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ArName : pm.ArName,

                                  EnName = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.EnName : pm.EnName,

                                  EnContent = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.EnContent : pm.EnContent,

                                  ArContent = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ArContent : pm.ArContent,

                                  ImageUrl = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ImageUrl : pm.ImageUrl,

                                  EnImageUrl = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.EnImageUrl : pm.EnImageUrl,

                                  IsActive = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsActive : pm.IsActive,

                                  IsDeleted = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsDeleted : pm.IsDeleted,

                                  IsDobulQuote = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsDobulQuote : pm.IsDobulQuote,

                                  Order = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.Order : pm.Order,

                                  IsSection = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsSection : pm.IsSection,

                                  ApprovalDate = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ApprovalDate : pm.ApprovalDate,

                                  ApprovedById = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ApprovedById : pm.ApprovedById,


                                  VersionStatusEnum = pmv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = pmv.ChangeActionEnum ?? ChangeActionEnum.Update
                              });

            return queryright.ToList();
        }

        public PageMinistryVersion GetByPageMinistryId(int id)
        {
            return _db.PageMinistryVersions.OrderByDescending(i => i.CreationDate).AsNoTracking().FirstOrDefault(i => i.PageMinistryId == id);
        }

        public IEnumerable<PageMinistryVersion> GetAllDrafts(int pageRouteId)
        {
            return _db.PageMinistryVersions.Where(e => !e.IsDeleted && e.VersionStatusEnum == VersionStatusEnum.Draft && e.PageRouteId == pageRouteId).ToList();
        }

        public IEnumerable<PageMinistryVersion> GetAllSubmitted(int pageRouteId)
        {
            return _db.PageMinistryVersions.Where(e => !e.IsDeleted && e.VersionStatusEnum == VersionStatusEnum.Submitted && e.PageRouteId == pageRouteId).ToList();
        }
    }
}
