using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Models;
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
    public class SectionCardVersionRepository : ISectionCardVersionRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IPageSectionVersionRepository _sectionVersionRepository;
        public SectionCardVersionRepository(ApplicationDbContext db, IPageSectionVersionRepository sectionVersionRepository)
        {
            _db = db;
            _sectionVersionRepository = sectionVersionRepository;
        }

        /// <summary>
        /// Add a new page section card version to database
        /// </summary>
        /// <param name="sectionCardVersion">page section card object</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns>Added object</returns>
        public PageSectionCardVersion Add(PageSectionCardVersion sectionCardVersion, int pageRouteVersionId)
        {
            try
            {
                bool isVersion = true;
                var pageRouteVersion = _db.PageRouteVersions.Include(d => d.PageSectionVersions).FirstOrDefault(d => d.Id == pageRouteVersionId);
                if (pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Ignored || pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Approved)
                {
                    pageRouteVersion = _sectionVersionRepository.AddNewPageRouteVersion(pageRouteVersion);
                    isVersion = false;
                }

                pageRouteVersion.ContentVersionStatusEnum = pageRouteVersion.ContentVersionStatusEnum == VersionStatusEnum.Submitted ? VersionStatusEnum.Submitted : VersionStatusEnum.Draft;
                _db.PageRouteVersions.Update(pageRouteVersion);

                _sectionVersionRepository.CopyPageSectionVersions(pageRouteVersion);
                if (!isVersion)
                {
                    var sectionVer = pageRouteVersion.PageSectionVersions.FirstOrDefault(d => d.PageSectionId == sectionCardVersion.PageSectionVersionId);
                    sectionCardVersion.PageSectionVersionId = sectionVer.Id;
                }
                _db.PageSectionCardVersions.Add(sectionCardVersion);
                _db.SaveChanges();
                return _db.PageSectionCardVersions.Include(x => x.PageSectionVersion).ThenInclude(x => x.PageRouteVersion).FirstOrDefault(c => c.Id == sectionCardVersion.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Update a page section card version from database
        /// </summary>
        /// <param name="sectionCardVersion">page section card object</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns>Updated object</returns>
        public PageSectionCardVersion Update(PageSectionCardVersion sectionCardVersion, int pageRouteVersionId)
        {
            try
            {
                bool isVersion = true;
                var pageRouteVersion = _db.PageRouteVersions.Include(d => d.PageSectionVersions).FirstOrDefault(d => d.Id == pageRouteVersionId);
                if (pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Ignored || pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Approved)
                {
                    pageRouteVersion = _sectionVersionRepository.AddNewPageRouteVersion(pageRouteVersion);
                    isVersion = false;
                }

                pageRouteVersion.ContentVersionStatusEnum = pageRouteVersion.ContentVersionStatusEnum == VersionStatusEnum.Submitted ? VersionStatusEnum.Submitted : VersionStatusEnum.Draft;
                _db.PageRouteVersions.Update(pageRouteVersion);

                _sectionVersionRepository.CopyPageSectionVersions(pageRouteVersion);
                PageSectionCardVersion entry = null;
                if (isVersion)
                {
                    entry = _db.PageSectionCardVersions.First(e => e.Id == sectionCardVersion.Id);
                }
                else
                {
                    entry = _db.PageSectionCardVersions.OrderByDescending(d => d.Id).First(e => e.PageSectionCardId == sectionCardVersion.Id);
                    sectionCardVersion.Id = entry.Id;
                    sectionCardVersion.PageSectionVersionId = entry.PageSectionVersionId;
                }
                _db.Entry(entry).CurrentValues.SetValues(sectionCardVersion);
                _db.SaveChanges();
                return _db.PageSectionCardVersions.Include(x => x.PageSectionVersion).ThenInclude(x => x.PageRouteVersion).FirstOrDefault(c => c.Id == sectionCardVersion.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get all section cards which contain the same section version id sent in paramater
        /// </summary>
        /// <param name="sectionVersionId">page section version id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns>List of page section cards mapped to section card view model</returns>
        public List<SectionCardListViewModel> GetCardsBySectionId(int sectionVersionId, int pageRouteVersionId)
        {
            var lastNotification = _db.ApprovalNotifications.Where(d => d.RelatedVersionId == pageRouteVersionId &&
              d.RelatedPageEnum == RelatedPageEnum.PageRouteVersion && d.VersionStatusEnum == VersionStatusEnum.Submitted && d.ChangeType == ChangeType.PageContent).Any();

            var pageRouteVersion = _db.PageRouteVersions.Include(d => d.PageSectionVersions).FirstOrDefault(d => d.Id == pageRouteVersionId);
            if ((pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Ignored ||
                  pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Approved ||
                  !pageRouteVersion.PageSectionVersions.Any()) && !lastNotification)
            {
                var data = from sc in _db.PageSectionCards.Where(d => d.PageSectionId == sectionVersionId && !d.IsDeleted)
                           select new SectionCardListViewModel
                           {
                               Id = sc.Id,
                               EnTitle = sc.EnTitle,
                               ArTitle = sc.ArTitle,
                               CardDescription = sc.EnDescription,
                               IsActive = sc.IsActive,
                               IsDeleted = sc.IsDeleted,
                               PageSectionId = sectionVersionId,
                               PageRouteVersionId = pageRouteVersionId
                           };
                return data.ToList();
            }
            else
            {
                var data = from sc in _db.PageSectionCardVersions.Where(d => d.PageSectionVersionId == sectionVersionId && !d.IsDeleted)
                           select new SectionCardListViewModel
                           {
                               Id = sc.Id,
                               EnTitle = sc.EnTitle,
                               ArTitle = sc.ArTitle,
                               CardDescription = sc.EnDescription,
                               IsActive = sc.IsActive,
                               IsDeleted = sc.IsDeleted,
                               PageSectionId = sectionVersionId,
                               PageRouteVersionId = pageRouteVersionId
                           };
                return data.ToList();
            }
        }

        /// <summary>
        /// Delete a page section card version object by id
        /// </summary>
        /// <param name="id">page section card version id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns>Deleted object</returns>
        public DeleteCardsViewModel Delete(int id, int pageRouteVersionId)
        {
            try
            {
                bool isVersion = true;
                var pageRouteVersion = _db.PageRouteVersions.Include(d => d.PageSectionVersions).FirstOrDefault(d => d.Id == pageRouteVersionId);
                if (pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Ignored || pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Approved)
                {
                    pageRouteVersion = _sectionVersionRepository.AddNewPageRouteVersion(pageRouteVersion);
                    isVersion = false;
                }
                pageRouteVersion.ContentVersionStatusEnum = pageRouteVersion.ContentVersionStatusEnum == VersionStatusEnum.Submitted ? VersionStatusEnum.Submitted : VersionStatusEnum.Draft;
                _db.PageRouteVersions.Update(pageRouteVersion);

                _sectionVersionRepository.CopyPageSectionVersions(pageRouteVersion);
                PageSectionCardVersion entry = null;
                if (isVersion)
                {
                    entry = _db.PageSectionCardVersions.First(e => e.Id == id);
                }
                else
                {
                    entry = _db.PageSectionCardVersions.OrderByDescending(d => d.Id).First(e => e.PageSectionCardId == id);
                }
                entry.IsDeleted = true;

                _db.PageSectionCardVersions.Attach(entry);
                _db.Entry(entry).State = EntityState.Modified;
                _db.SaveChanges();

                return new DeleteCardsViewModel()
                {

                    pageSectionCardVersion = _db.PageSectionCardVersions.Include(x => x.PageSectionVersion).ThenInclude(x => x.PageRouteVersion).FirstOrDefault(c => c.Id == id),
                    Link = $"{entry.PageSectionVersionId}?pageRouteVersionId={pageRouteVersion.Id}"
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get page section card version object 
        /// </summary>
        /// <param name="id">page section card version id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns>Section card version object mapped to section card version view model</returns>
        public SectionCardEditViewModel Get(int id, int pageRouteVersionId)
        {
            var lastNotification = _db.ApprovalNotifications.Where(d => d.RelatedVersionId == pageRouteVersionId &&
                         d.RelatedPageEnum == RelatedPageEnum.PageRouteVersion && d.VersionStatusEnum == VersionStatusEnum.Submitted && d.ChangeType == ChangeType.PageContent).Any();

            var pageRouteVersion = _db.PageRouteVersions.Include(d => d.PageSectionVersions).FirstOrDefault(d => d.Id == pageRouteVersionId);
            if ((pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Ignored ||
                  pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Approved ||
                  !pageRouteVersion.PageSectionVersions.Any()) && !lastNotification)
            {
                var data = from sectionCardVersion in _db.PageSectionCards.Where(d => d.Id == id && !d.IsDeleted)
                           select new SectionCardEditViewModel
                           {
                               Id = sectionCardVersion.Id,
                               EnTitle = sectionCardVersion.EnTitle,
                               ArTitle = sectionCardVersion.ArTitle,
                               EnDescription = sectionCardVersion.EnDescription,
                               ArDescription = sectionCardVersion.ArDescription,
                               EnImageAlt = sectionCardVersion.EnImageAlt,
                               ArImageAlt = sectionCardVersion.ArImageAlt,
                               Order = sectionCardVersion.Order,
                               IsActive = sectionCardVersion.IsActive,
                               ImageUrl = sectionCardVersion.ImageUrl,
                               FileUrl = sectionCardVersion.FileUrl,
                               SectionVersionId = sectionCardVersion.PageSectionId,
                               IsDeleted = sectionCardVersion.IsDeleted,
                               CreationDate = sectionCardVersion.CreationDate,
                               CreatedById = sectionCardVersion.CreatedById,
                               PageRouteVersionId = pageRouteVersionId
                           };
                return data.FirstOrDefault();
            }
            else
            {
                var data = from sectionCardVersion in _db.PageSectionCardVersions.Where(d => d.Id == id && !d.IsDeleted)
                           select new SectionCardEditViewModel
                           {
                               Id = sectionCardVersion.Id,
                               EnTitle = sectionCardVersion.EnTitle,
                               ArTitle = sectionCardVersion.ArTitle,
                               EnDescription = sectionCardVersion.EnDescription,
                               ArDescription = sectionCardVersion.ArDescription,
                               EnImageAlt = sectionCardVersion.EnImageAlt,
                               ArImageAlt = sectionCardVersion.ArImageAlt,
                               Order = sectionCardVersion.Order,
                               IsActive = sectionCardVersion.IsActive,
                               ImageUrl = sectionCardVersion.ImageUrl,
                               FileUrl = sectionCardVersion.FileUrl,
                               SectionVersionId = sectionCardVersion.PageSectionVersionId,
                               IsDeleted = sectionCardVersion.IsDeleted,
                               CreationDate = sectionCardVersion.CreationDate,
                               CreatedById = sectionCardVersion.CreatedById,
                               PageRouteVersionId = pageRouteVersionId
                           };
                return data.FirstOrDefault();
            }
        }

        /// <summary>
        /// Get all section cards by section id
        /// </summary>
        /// <param name="sectionId">page section id</param>
        /// <returns>IEnumerable of page section cards</returns>
        public IEnumerable<PageSectionCard> GetSectionCards(int sectionId)
        {
            var sectionCards = _db.PageSectionCards.Where(s => s.IsActive && !(s.IsDeleted) && s.PageSectionId == sectionId).OrderBy(s => s.Id).ToList();
            return sectionCards;
        }
    }
}
