using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Business.ViewModels;
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
    public class PageSectionVersionRepository : IPageSectionVersionRepository
    {
        private readonly ApplicationDbContext _db;

        public PageSectionVersionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add new page route version to database
        /// </summary>
        /// <param name="pageRouteVersion">page route version model</param>
        /// <returns>Added object</returns>
        public PageRouteVersion AddNewPageRouteVersion(PageRouteVersion pageRouteVersion)
        {
            var newPageRouteVersion = new PageRouteVersion
            {
                VersionStatusEnum = VersionStatusEnum.Draft,
                ArName = pageRouteVersion.ArName,
                ChangeActionEnum = ChangeActionEnum.Update,
                ControllerName = pageRouteVersion.ControllerName,
                CreationDate = DateTime.Now,
                CreatedById = pageRouteVersion.CreatedById,
                EnName = pageRouteVersion.EnName,
                HasNavItem = pageRouteVersion.HasNavItem,
                IsActive = pageRouteVersion.IsActive,
                IsDeleted = false,
                IsDynamicPage = pageRouteVersion.IsDynamicPage,
                NavItemId = pageRouteVersion.NavItemId,
                Order = pageRouteVersion.Order,
                PageFilePathAr = pageRouteVersion.PageFilePathAr,
                PageFilePathEn = pageRouteVersion.PageFilePathEn,
                PageRouteId = pageRouteVersion.PageRouteId,
                PageType = pageRouteVersion.PageType,
                SectionName = pageRouteVersion.SectionName,
                SeoDescriptionAR = pageRouteVersion.SeoDescriptionAR,
                SeoDescriptionEN = pageRouteVersion.SeoDescriptionEN,
                SeoOgTitleAR = pageRouteVersion.SeoOgTitleAR,
                SeoOgTitleEN = pageRouteVersion.SeoOgTitleEN,
                SeoTitleAR = pageRouteVersion.SeoTitleAR,
                SeoTitleEN = pageRouteVersion.SeoTitleEN,
                SeoTwitterCardAR = pageRouteVersion.SeoTwitterCardAR,
                SeoTwitterCardEN = pageRouteVersion.SeoTwitterCardEN,
                StatusId = 1

            };
            _db.PageRouteVersions.Add(newPageRouteVersion);

            _db.SaveChanges();
            return newPageRouteVersion;
        }

        /// <summary>
        /// Coping page section and its cards to page section versions which has the same page route version id 
        /// </summary>
        /// <param name="pageRouteVersion">page route version object which i will take it's id to copy objects</param>
        /// <returns></returns>
        public void CopyPageSectionVersions(PageRouteVersion pageRouteVersion)
        {
            if (pageRouteVersion.PageRouteId != null && !pageRouteVersion.PageSectionVersions.Any())
            {
                var pageSections = _db.PageSections.Include(d => d.PageSectionCards).Where(d => d.PageRouteId == pageRouteVersion.PageRouteId && !d.IsDeleted).ToList();
                foreach (var section in pageSections)
                {
                    var secVer = new PageSectionVersion
                    {
                        ArDescription = section.ArDescription,
                        ArImageAlt = section.ArImageAlt,
                        ArTitle = section.ArTitle,
                        EnDescription = section.EnDescription,
                        EnImageAlt = section.EnImageAlt,
                        EnTitle = section.EnTitle,
                        CreatedById = pageRouteVersion.CreatedById,
                        CreationDate = DateTime.Now,
                        IsActive = section.IsActive,
                        IsDeleted = false,
                        Order = section.Order,
                        PageRouteVersionId = pageRouteVersion.Id,
                        PageSectionId = section.Id,
                        PageSectionTypeId = section.PageSectionTypeId,
                        Url = section.Url
                    };

                    foreach (var card in section.PageSectionCards.Where(d => !d.IsDeleted))
                    {
                        secVer.PageSectionCardVersions.Add(new PageSectionCardVersion
                        {
                            ArDescription = card.ArDescription,
                            ArImageAlt = card.ArImageAlt,
                            ArTitle = card.ArTitle,
                            CreatedById = pageRouteVersion.CreatedById,
                            CreationDate = DateTime.Now,
                            EnDescription = card.EnDescription,
                            EnImageAlt = card.EnImageAlt,
                            EnTitle = card.EnTitle,
                            FileUrl = card.FileUrl,
                            ImageUrl = card.ImageUrl,
                            IsActive = card.IsActive,
                            IsDeleted = false,
                            Order = card.Order,
                            PageSectionCardId = card.Id,
                            PageSectionVersionId = secVer.Id
                        });
                    }
                    _db.PageSectionVersions.Add(secVer);
                }
                _db.SaveChanges();
            }


        }

        /// <summary>
        /// Add a new page section version to database
        /// </summary>
        /// <param name="pageSectionVersion">page section version model</param>
        /// <returns>Added object</returns>
        public PageSectionVersion Add(PageSectionVersion pageSectionVersion)
        {
            try
            {
                var pageRouteVersion = _db.PageRouteVersions.Include(d => d.PageSectionVersions).FirstOrDefault(d => d.Id == pageSectionVersion.PageRouteVersionId);
                if (pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Ignored || pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Approved)
                {
                    pageRouteVersion = AddNewPageRouteVersion(pageRouteVersion);
                }

                pageRouteVersion.ContentVersionStatusEnum = pageRouteVersion.ContentVersionStatusEnum == VersionStatusEnum.Submitted ? VersionStatusEnum.Submitted : VersionStatusEnum.Draft;
                _db.PageRouteVersions.Update(pageRouteVersion);

                CopyPageSectionVersions(pageRouteVersion);
                pageSectionVersion.PageRouteVersionId = pageRouteVersion.Id;
                _db.PageSectionVersions.Add(pageSectionVersion);
                _db.SaveChanges();
                return _db.PageSectionVersions.Include(x => x.PageRouteVersion).FirstOrDefault(s => s.Id == pageSectionVersion.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Update a page section object in database
        /// </summary>
        /// <param name="pageSectionVersion">page section version object</param>
        /// <returns></returns>
        public void NormalUpdate(PageSectionVersion pageSectionVersion)
        {
            try
            {
                _db.PageSectionVersions.Update(pageSectionVersion);
                _db.SaveChanges();
            }
            catch (Exception er) { }
        }

        /// <summary>
        /// Update a page section object in database
        /// </summary>
        /// <param name="pageSectionVersion">page section version object</param>
        /// <returns>Updated object</returns>
        public PageSectionVersion Update(PageSectionVersion pageSectionVersion)
        {
            try
            {
                bool isVersion = true;
                var pageRouteVersion = _db.PageRouteVersions.Include(d => d.PageSectionVersions).FirstOrDefault(d => d.Id == pageSectionVersion.PageRouteVersionId);
                if (pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Ignored || pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Approved)
                {
                    pageRouteVersion = AddNewPageRouteVersion(pageRouteVersion);
                    isVersion = false;
                }

                pageRouteVersion.ContentVersionStatusEnum = pageRouteVersion.ContentVersionStatusEnum == VersionStatusEnum.Submitted ? VersionStatusEnum.Submitted : VersionStatusEnum.Draft;
                _db.PageRouteVersions.Update(pageRouteVersion);

                CopyPageSectionVersions(pageRouteVersion);

                PageSectionVersion entry = null;
                if (isVersion)
                {
                    entry = _db.PageSectionVersions.First(e => e.Id == pageSectionVersion.Id);

                }
                else
                {
                    entry = _db.PageSectionVersions.OrderByDescending(d => d.Id).First(e => e.PageSectionId == pageSectionVersion.Id);
                    pageSectionVersion.Id = entry.Id;
                    pageSectionVersion.PageSectionId = entry.PageSectionId;
                    pageSectionVersion.PageRouteVersionId = entry.PageRouteVersionId;
                }
                _db.Entry(entry).CurrentValues.SetValues(pageSectionVersion);
                _db.SaveChanges();
                return _db.PageSectionVersions.Include(x => x.PageRouteVersion).FirstOrDefault(s => s.Id == pageSectionVersion.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get all page section versions which contain page route version id sent in parameter
        /// </summary>
        /// <param name="pageRouteVerId">page route version id</param>
        /// <returns>List of page section versions view models</returns>
        public List<PageSectionListViewModel> GetPageSectionsByPageRouteId(int pageRouteVerId)
        {
            var lastNotification = _db.ApprovalNotifications.Where(d => d.RelatedVersionId == pageRouteVerId &&
            d.RelatedPageEnum == RelatedPageEnum.PageRouteVersion && d.VersionStatusEnum == VersionStatusEnum.Submitted && d.ChangeType == ChangeType.PageContent).Any();
            var pageRouteVersion = _db.PageRouteVersions.AsNoTracking().Include(d => d.PageSectionVersions).FirstOrDefault(d => d.Id == pageRouteVerId);
            if (pageRouteVersion != null)
            {
                if ((pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Ignored ||
                    pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Approved ||
                    !pageRouteVersion.PageSectionVersions.Any()) && !lastNotification)
                {
                    var data = from ps in _db.PageSections.Where(d => d.PageRouteId == pageRouteVersion.PageRouteId && !d.IsDeleted)
                               from pst in _db.PageSectionTypes.Where(d => d.Id == ps.PageSectionTypeId)
                               select new PageSectionListViewModel
                               {
                                   Id = ps.Id,
                                   SectionType = pst.EnName,
                                   SectionTitle = ps.EnTitle,
                                   SectionDescription = ps.EnDescription,
                                   HasCards = pst.HasCards,
                                   IsActive = ps.IsActive,
                                   PageSectionTypeId = pst.Id,
                                   PageRouteVersionId = pageRouteVerId
                               };
                    return data.ToList();
                }
                else
                {
                    var data = from ps in _db.PageSectionVersions.Where(d => d.PageRouteVersionId == pageRouteVerId && !d.IsDeleted)
                               from pst in _db.PageSectionTypes.Where(d => d.Id == ps.PageSectionTypeId)
                               select new PageSectionListViewModel
                               {
                                   Id = ps.Id,
                                   SectionType = pst.EnName,
                                   SectionTitle = ps.EnTitle,
                                   SectionDescription = ps.EnDescription,
                                   HasCards = pst.HasCards,
                                   IsActive = ps.IsActive,
                                   PageSectionTypeId = pst.Id,
                                   PageRouteVersionId = pageRouteVerId
                               };
                    return data.ToList();
                }

            }
            return null;
        }

        /// <summary>
        /// Delete a page section version from database by id
        /// </summary>
        /// <param name="id">page section version id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns>Deleted page section version</returns>
        public PageSectionVersion Delete(int id, int pageRouteVersionId)
        {
            try
            {
                bool isVersion = true;
                var pageRouteVersion = _db.PageRouteVersions.Include(d => d.PageSectionVersions).FirstOrDefault(d => d.Id == pageRouteVersionId);
                if (pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Ignored || pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Approved)
                {
                    pageRouteVersion = AddNewPageRouteVersion(pageRouteVersion);

                    isVersion = false;
                }

                pageRouteVersion.ContentVersionStatusEnum = pageRouteVersion.ContentVersionStatusEnum == VersionStatusEnum.Submitted ? VersionStatusEnum.Submitted : VersionStatusEnum.Draft;
                _db.PageRouteVersions.Update(pageRouteVersion);

                CopyPageSectionVersions(pageRouteVersion);

                PageSectionVersion entry = null;
                if (isVersion)
                {
                    entry = _db.PageSectionVersions.First(e => e.Id == id);
                }
                else
                {
                    entry = _db.PageSectionVersions.OrderByDescending(d => d.Id).First(e => e.PageSectionId == id);
                }
                entry.IsDeleted = true;
                _db.PageSectionVersions.Attach(entry);
                _db.Entry(entry).State = EntityState.Modified;
                _db.SaveChanges();

                return _db.PageSectionVersions.Include(x => x.PageRouteVersion).FirstOrDefault(s => s.Id == entry.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get page section version from database by id
        /// </summary>
        /// <param name="id">page section version id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns>Page section model mapped to section view model</returns>
        public SectionViewModel Get(int id, int pageRouteVersionId)
        {
            var lastNotification = _db.ApprovalNotifications.Where(d => d.RelatedVersionId == pageRouteVersionId &&
               d.RelatedPageEnum == RelatedPageEnum.PageRouteVersion && d.VersionStatusEnum == VersionStatusEnum.Submitted && d.ChangeType == ChangeType.PageContent).Any();

            var pageRouteVersion = _db.PageRouteVersions.Include(d => d.PageSectionVersions).FirstOrDefault(d => d.Id == pageRouteVersionId);
            if ((pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Ignored ||
                  pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Approved ||
                  !pageRouteVersion.PageSectionVersions.Any()) && !lastNotification)
            {
                var data = from ps in _db.PageSections.Where(d => d.Id == id && !d.IsDeleted)
                           from pst in _db.PageSectionTypes.Where(d => d.Id == ps.PageSectionTypeId)
                           select new SectionViewModel
                           {
                               Id = ps.Id,
                               IsVersion = false,
                               EnTitle = ps.EnTitle,
                               ArTitle = ps.ArTitle,
                               EnDescription = ps.EnDescription,
                               ArDescription = ps.ArDescription,
                               EnImageAlt = ps.EnImageAlt,
                               ArImageAlt = ps.ArImageAlt,
                               Order = ps.Order,
                               IsActive = ps.IsActive,
                               Url = ps.Url,
                               SectionTypeId = ps.PageSectionTypeId,
                               CreationDate = ps.CreationDate,
                               CreatedById = ps.CreatedById,
                               MediaType = pst.MediaType,
                               PageRouteVersionId = pageRouteVersionId
                           };
                return data.FirstOrDefault();
            }
            else
            {
                var data = from ps in _db.PageSectionVersions.Where(d => d.Id == id && !d.IsDeleted)
                           from pst in _db.PageSectionTypes.Where(d => d.Id == ps.PageSectionTypeId)
                           select new SectionViewModel
                           {
                               Id = ps.Id,
                               IsVersion = true,
                               EnTitle = ps.EnTitle,
                               ArTitle = ps.ArTitle,
                               EnDescription = ps.EnDescription,
                               ArDescription = ps.ArDescription,
                               EnImageAlt = ps.EnImageAlt,
                               ArImageAlt = ps.ArImageAlt,
                               Order = ps.Order,
                               IsActive = ps.IsActive,
                               Url = ps.Url,
                               SectionTypeId = ps.PageSectionTypeId,
                               CreationDate = ps.CreationDate,
                               CreatedById = ps.CreatedById,
                               MediaType = pst.MediaType,
                               PageRouteVersionId = pageRouteVersionId
                           };
                return data.FirstOrDefault();
            }
        }

        /// <summary>
        /// Get all page sections which contain page route id sent in parameter
        /// </summary>
        /// <param name="pageRouteId">page route id</param>
        /// <returns>List of page sections</returns>
        public List<PageSection> GetPageSections(int pageRouteId)
        {
            var data = from ps in _db.PageSections.Where(d => d.PageRouteId == pageRouteId && !d.IsDeleted)
                       select ps;
            return data.ToList();
        }

        /// <summary>
        /// Get all page section versions which contain page route version id sent in parameter
        /// </summary>
        /// <param name="pageRouteVerId"></param>
        /// <returns>List of page section versions</returns>
        public List<PageSectionVersion> GetPageSectionVersions(int pageRouteVerId)
        {
            var data = from ps in _db.PageSectionVersions.Include(d => d.PageSectionCardVersions).Where(d => d.PageRouteVersionId == pageRouteVerId && !d.IsDeleted)
                       select ps;
            return data.ToList();
        }
    }
}
