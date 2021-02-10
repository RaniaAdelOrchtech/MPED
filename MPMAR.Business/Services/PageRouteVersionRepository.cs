using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Business.ViewModels;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Business.Services
{
    public class PageRouteVersionRepository : IPageRouteVersionRepository
    {
        private readonly ApplicationDbContext _db;
        IApprovalNotificationsRepository _approvalNotificationsRepository;
        public PageRouteVersionRepository(ApplicationDbContext db, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _db = db;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }

        /// <summary>
        /// Adding a new page route version
        /// </summary>
        /// <param name="PageRouteVersion">page route version model</param>
        /// <returns>the added object</returns>
        public PageRouteVersion Add(PageRouteVersion PageRouteVersion)
        {
            if (PageRouteVersion.StatusId <= 0)
                PageRouteVersion.StatusId = (int)RequestStatus.Draft;
            PageRouteVersion.HasNavItem = true;
            PageRouteVersion.IsDynamicPage = true;
            PageRouteVersion.ControllerName = "DynamicPage";

            try
            {
                _db.PageRouteVersions.Add(PageRouteVersion);
                _db.SaveChanges();
                return PageRouteVersion;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Adding a new static page 
        /// </summary>
        /// <param name="PageRouteVersion">page route version model</param>
        /// <returns></returns>
        public void AddStaticPage(PageRouteVersion PageRouteVersion)
        {
            _db.PageRouteVersions.Add(PageRouteVersion);
            _db.SaveChanges();
        }

        /// <summary>
        /// upgate a page route version model
        /// </summary>
        /// <param name="pageRouteVersion">page route version model</param>
        /// <returns>updated object</returns>
        public PageRouteVersion Update(PageRouteVersion pageRouteVersion)
        {
            try
            {
                if (pageRouteVersion.StatusId <= 1)
                    pageRouteVersion.StatusId = (int)RequestStatus.Draft;

                _db.PageRouteVersions.Update(pageRouteVersion);
                _db.SaveChanges();
                return pageRouteVersion;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// delete single page route version object
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <returns>deleted object</returns>
        public PageRouteVersion SoftDelete(int id)
        {
            try
            {
                var item = _db.PageRouteVersions.FirstOrDefault(x => x.Id == id);
                item.IsDeleted = true;
                item.StatusId = (int)RequestStatus.Draft;

                _db.PageRouteVersions.Attach(item);
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// delete single page route version object
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <returns>true ifdeleted false  otherwise</returns>
        public bool Delete(int id)
        {
            try
            {
                var item = _db.PageRouteVersions.FirstOrDefault(x => x.Id == id);

                _db.PageRouteVersions.Remove(item);

                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// apply editing request method
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <param name="pageFilePathAr">arabic page file path</param>
        /// <param name="pageFilePathEn">english page file path</param>
        /// <returns>editing pae route version</returns>
        public PageRouteVersion ApplyEditRequest(int id, string pageFilePathAr, string pageFilePathEn)
        {
            try
            {
                var item = _db.PageRouteVersions.FirstOrDefault(x => x.Id == id);
                //item.StatusId = (int)RequestStatus.Submitted;
                item.PageFilePathAr = pageFilePathAr;
                item.PageFilePathEn = pageFilePathEn;

                _db.PageRouteVersions.Attach(item);
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// get a single page route version
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <returns>a single page route version </returns>
        public PageRouteVersion Get(int id)
        {
            try
            {
                return _db.PageRouteVersions.Include(p => p.NavItem).Include(p => p.PageSectionVersions).ThenInclude(s => s.PageSectionCardVersions).Include(p => p.PageSectionVersions).ThenInclude(p => p.PageSectionType).FirstOrDefault(p => !(p.IsDeleted && p.StatusId == (int)RequestStatus.Approved) && p.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PageRouteVersion GetWithNoTracking(int id)
        {
            try
            {
                return _db.PageRouteVersions.AsNoTracking().Include(p => p.NavItem).Include(p => p.PageSectionVersions).ThenInclude(s => s.PageSectionCardVersions).Include(p => p.PageSectionVersions).ThenInclude(p => p.PageSectionType).FirstOrDefault(p => !(p.IsDeleted && p.StatusId == (int)RequestStatus.Approved) && p.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// get page route versions objects
        /// </summary>
        /// <returns>page route versions objects</returns>
        public IEnumerable<PageRouteVersion> Get()
        {
            return _db.PageRouteVersions.Where(n => !n.IsDeleted).OrderBy(nv => nv.Id);
        }

        /// <summary>
        /// get all dynamic pages objects
        /// </summary>
        /// <returns>all dynamic objects</returns>
        public List<PageRouteListViewModel> GetDynamicPages()
        {
            return GetPages(true);

        }

        private List<PageRouteListViewModel> GetPages(bool isDynamic)
        {
            //join between version and non version PageRoutes take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from pr in _db.PageRoutes.Where(d => !d.IsDeleted && d.IsDynamicPage == isDynamic)
                              from prv in _db.PageRouteVersions.Where(d => d.PageRouteId == pr.Id && !d.IsDeleted && d.IsDynamicPage == isDynamic && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).Take(1)
                              from navpr in _db.NavItems.Where(d => d.Id == pr.NavItemId && !d.IsDeleted)
                              from navprv in _db.NavItems.Where(d => d.Id == prv.NavItemId && !d.IsDeleted)
                              select new PageRouteListViewModel
                              {
                                  Id = pr.Id,
                                  PageRouteVersionId = prv.Id,
                                  ArName = (pr == null || prv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || prv.VersionStatusEnum == VersionStatusEnum.Submitted) ? prv.ArName : pr.ArName,
                                  EnName = (pr == null || prv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || prv.VersionStatusEnum == VersionStatusEnum.Submitted) ? prv.EnName : pr.EnName,
                                  IsActive = (pr == null || prv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || prv.VersionStatusEnum == VersionStatusEnum.Submitted) ? prv.IsActive : pr.IsActive,
                                  NavItemEnName = (pr == null || prv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || prv.VersionStatusEnum == VersionStatusEnum.Submitted) ? navprv.EnName : navpr.EnName,
                                  Order = (pr == null || prv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || prv.VersionStatusEnum == VersionStatusEnum.Submitted) ? prv.Order : pr.Order,
                                  VersionStatusEnum = prv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = prv.ChangeActionEnum ?? ChangeActionEnum.New,
                                  ContentStatusEnum = prv.ContentVersionStatusEnum ?? VersionStatusEnum.Draft,
                                  PageType = prv.PageType,
                                  HasSections = (prv.PageSectionVersions.Any() && (prv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || prv.VersionStatusEnum == VersionStatusEnum.Submitted)),
                                  ControllerName = (pr == null || prv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || prv.VersionStatusEnum == VersionStatusEnum.Submitted) ? prv.ControllerName : pr.ControllerName,
                                  CreatedById = (pr == null || prv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || prv.VersionStatusEnum == VersionStatusEnum.Submitted) ? prv.CreatedById : pr.CreatedById
                              });
            //get the rest from PageRouteVersions that wasn't included in previous join 
            var queryleft = (from prv in _db.PageRouteVersions.Where(d => !d.IsDeleted && d.IsDynamicPage == isDynamic && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                             from navprv in _db.NavItems.Where(d => d.Id == prv.NavItemId && !d.IsDeleted)
                             where !_db.PageRoutes.Any(d => d.Id == prv.PageRouteId)
                             select new PageRouteListViewModel
                             {
                                 Id = (int?)null,
                                 PageRouteVersionId = prv.Id,
                                 ArName = prv.ArName,
                                 EnName = prv.EnName,
                                 VersionStatusEnum = prv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                 ContentStatusEnum = prv.ContentVersionStatusEnum ?? VersionStatusEnum.Draft,
                                 ChangeActionEnum = prv.ChangeActionEnum,
                                 HasSections = prv.PageSectionVersions.Any(),
                                 IsActive = prv.IsActive,
                                 NavItemEnName = navprv.EnName,
                                 Order = prv.Order,
                                 PageType = prv.PageType,
                                 ControllerName = prv.ControllerName,
                                 CreatedById = prv.CreatedById
                             });
            return queryright.Union(queryleft).OrderByDescending(d => d.PageRouteVersionId).ToList();
        }

        /// <summary>
        /// changing page route version status
        /// </summary>
        /// <param name="pageVersionId">page route version id</param>
        /// <param name="requestStatus">changed status</param>
        /// <returns></returns>
        public void ChangeStatus(int pageVersionId, RequestStatus requestStatus)
        {
            try
            {
                var item = _db.PageRouteVersions.FirstOrDefault(x => x.Id == pageVersionId);
                item.StatusId = (int)requestStatus;

                _db.PageRouteVersions.Attach(item);
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// get all static pages objects
        /// </summary>
        /// <returns>all static pages objects</returns>
        public List<PageRouteListViewModel> GetStaticPages()
        {
            return GetPages(false);



        }

        /// <summary>
        /// Applying submit request
        /// </summary>
        /// <param name="pageRouteVersion">page route version model</param>
        /// <param name="userId">logged in user</param>
        /// <param name="urlParent">parent route version url</param>
        /// <param name="urlSection">section url</param>
        /// <returns>true if applying false otherwise</returns>
        public bool ApplySubmitRequest(PageRouteVersion pageRouteVersion, string userId, string urlParent, string urlSection)
        {
            var existPageRouteVer = _db.PageRouteVersions.FirstOrDefault(x => x.Id == pageRouteVersion.Id);
            //existPageRouteVer.VersionStatusEnum = VersionStatusEnum.Submitted;

            existPageRouteVer.VersionStatusEnum = VersionStatusEnum.Submitted;

            var existingNotifications = _db.ApprovalNotifications.Where(d => d.RelatedVersionId == pageRouteVersion.Id
            && d.RelatedPageEnum == RelatedPageEnum.PageRouteVersion && d.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
            ///check if no notification added for the basic info then add new one
            if ((pageRouteVersion.PageRouteId == null && !existingNotifications.Any(d => d.ChangeType == ChangeType.BasicInfo)))
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = pageRouteVersion.ChangeActionEnum ?? ChangeActionEnum.New,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.BasicInfo,
                    PageLink = $"/{urlParent}/{pageRouteVersion.Id}",
                    PageName = pageRouteVersion.EnName,
                    PageType = PageType.Dynamic,
                    ContentManagerId = userId,
                    RelatedVersionId = pageRouteVersion.Id,
                    RelatedPageEnum = RelatedPageEnum.PageRouteVersion
                };
                _approvalNotificationsRepository.Add(approval);
            }
            if (!existingNotifications.Any(d => d.ChangeType == ChangeType.PageContent) || pageRouteVersion.ContentVersionStatusEnum == VersionStatusEnum.Draft)
            {
                existPageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Submitted;
                ApprovalNotification approvalSections = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{urlSection}?pageRouteVersionId={pageRouteVersion.Id}",
                    PageName = pageRouteVersion.EnName,
                    PageType = PageType.Dynamic,
                    ContentManagerId = userId,
                    RelatedVersionId = pageRouteVersion.Id,
                    RelatedPageEnum = RelatedPageEnum.PageRouteVersion
                };
                _approvalNotificationsRepository.Add(approvalSections);
            }
            _db.PageRouteVersions.Attach(existPageRouteVer);
            _db.Entry(existPageRouteVer).State = EntityState.Modified;
            _db.SaveChanges();
            return true;
        }

        /// <summary>
        /// get page route version by page type
        /// </summary>
        /// <param name="pageType">page type</param>
        /// <returns>single page route version</returns>
        public PageRouteVersion GetPageRouteVersionByPageType(string pageType)
        {
            var pageRouteVersion = _db.PageRouteVersions.Where(x => x.PageType == pageType && x.VersionStatusEnum != VersionStatusEnum.Ignored).OrderByDescending(x => x.Id).FirstOrDefault();
            return pageRouteVersion;
        }

        /// <summary>
        /// Approve page route version
        /// </summary>
        /// <param name="pageRouteVersion">page route version model</param>
        /// <param name="changeType">change type</param>
        /// <returns></returns>
        public void ApprovePageRoute(PageRouteVersion pageRouteVersion, ChangeType changeType)
        {
            var lastNotification = _db.ApprovalNotifications.Where(d => d.RelatedVersionId == pageRouteVersion.Id &&
             d.RelatedPageEnum == RelatedPageEnum.PageRouteVersion && d.VersionStatusEnum == VersionStatusEnum.Submitted
             && d.ChangeType != changeType).Any();
            if (!lastNotification)
            {
                pageRouteVersion.VersionStatusEnum = VersionStatusEnum.Approved;
            }
        }

        /// <summary>
        /// get a single page route version
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <returns>single page route version</returns>
        public PageRouteVersion GetById(int id)
        {
            return _db.PageRouteVersions.Include(x => x.PageRoute).FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// get page route version without model includes
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <returns>single page route version</returns>
        public PageRouteVersion GetByIdWithoutIncludes(int id)
        {
            return _db.PageRouteVersions.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// get single page route version by page route id
        /// </summary>
        /// <param name="id">page route id</param>
        /// <returns>page route version object</returns>
        public PageRouteVersion GetByPageRoute(int id)
        {
            return _db.PageRouteVersions.Include(x => x.PageRoute).OrderByDescending(x => x.Id).FirstOrDefault(x => x.PageRouteId == id && x.VersionStatusEnum != VersionStatusEnum.Ignored);
        }

        /// <summary>
        /// Adding or updating page route version
        /// </summary>
        /// <param name="pageRouteId">page route id</param>
        /// <returns>added or updated object</returns>
        public PageRouteVersion AddOrUpdatePageRouteVersion(int pageRouteId)
        {
            var pageRouteVersion = GetByPageRoute(pageRouteId);
            if ((pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Approved || pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            && (pageRouteVersion.ContentVersionStatusEnum == VersionStatusEnum.Approved || pageRouteVersion.ContentVersionStatusEnum == VersionStatusEnum.Ignored))
            {
                if (pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    var pageRoute = _db.PageRoutes.Find(pageRouteId);
                    var newVer = new PageRouteVersion
                    {
                        VersionStatusEnum = VersionStatusEnum.Approved,
                        ArName = pageRoute.ArName,
                        ChangeActionEnum = ChangeActionEnum.Update,
                        ControllerName = pageRoute.ControllerName,
                        CreationDate = DateTime.Now,
                        CreatedById = pageRoute.CreatedById,
                        EnName = pageRoute.EnName,
                        HasNavItem = pageRoute.HasNavItem,
                        IsActive = pageRoute.IsActive,
                        IsDeleted = false,
                        IsDynamicPage = pageRoute.IsDynamicPage,
                        NavItemId = pageRoute.NavItemId,
                        Order = pageRoute.Order,
                        PageFilePathAr = pageRoute.PageFilePathAr,
                        PageFilePathEn = pageRoute.PageFilePathEn,
                        PageRouteId = pageRoute.Id,
                        PageType = pageRoute.PageType,
                        SectionName = pageRoute.SectionName,
                        SeoDescriptionAR = pageRoute.SeoDescriptionAR,
                        SeoDescriptionEN = pageRoute.SeoDescriptionEN,
                        SeoOgTitleAR = pageRoute.SeoOgTitleAR,
                        SeoOgTitleEN = pageRoute.SeoOgTitleEN,
                        SeoTitleAR = pageRoute.SeoTitleAR,
                        SeoTitleEN = pageRoute.SeoTitleEN,
                        SeoTwitterCardAR = pageRoute.SeoTwitterCardAR,
                        SeoTwitterCardEN = pageRoute.SeoTwitterCardEN,
                        StatusId = 1,
                        ContentVersionStatusEnum = VersionStatusEnum.Draft
                    };
                    _db.PageRouteVersions.Add(newVer);
                    _db.SaveChanges();
                    return newVer;
                }
                else
                {
                    var newVer = new PageRouteVersion
                    {
                        VersionStatusEnum = pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Ignored ? VersionStatusEnum.Approved : pageRouteVersion.VersionStatusEnum,
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
                        StatusId = 1,
                        ContentVersionStatusEnum = VersionStatusEnum.Draft
                    };
                    _db.PageRouteVersions.Add(newVer);
                    _db.SaveChanges();
                    return newVer;
                }
            }
            else
            {
                pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                _db.PageRouteVersions.Update(pageRouteVersion);
                _db.SaveChanges();
                return pageRouteVersion;
            }

        }

        public PageRouteVersion GetByPageEnName(string pageEnName)
        {
            return _db.PageRouteVersions.Where(x => pageEnName == x.EnName).OrderByDescending(x => x.Id).FirstOrDefault();
        }

        public List<string> GetMediaPagesNames()
        {
            List<string> mediaListController = new List<string>() { "News", "PhotoArchive", "EventCalendar" };
            return _db.PageRoutes.Where(x => !x.IsDeleted && x.IsActive && mediaListController.Contains(x.ControllerName)).Select(x => x.EnName).Distinct().ToList();
        }

        public PageRouteVersion GetCurrentPageRouteVersionByPageRouteId(int? pageRouteId)
        {
            return _db.PageRouteVersions.Include(x => x.PageSectionVersions).Where(x => x.PageRouteId == pageRouteId && x.VersionStatusEnum == VersionStatusEnum.Approved).OrderByDescending(x => x.Id).FirstOrDefault();
        }
    }
}
