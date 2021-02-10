using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MPMAR.Business.Interfaces;
using MPMAR.Business.ViewModels;
using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class PageNewsRepository : IPageNewsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        private readonly IPageNewsElasticSearchService _pageNewsElasticSearchService;
        private readonly IGlobalElasticSearchService _globalElasticSearchService;
        private readonly IPageRouteRepository _pageRouteRepository;

        public PageNewsRepository(ApplicationDbContext db, IPageRouteVersionRepository pageRouteVersionRepository, IApprovalNotificationsRepository approvalNotificationsRepository, IPageNewsElasticSearchService pageNewsElasticSearchService, IGlobalElasticSearchService globalElasticSearchService, IPageRouteRepository pageRouteRepository)
        {
            _db = db;
            _pageRouteVersionRepository = pageRouteVersionRepository;
            _approvalNotificationsRepository = approvalNotificationsRepository;
            _pageNewsElasticSearchService = pageNewsElasticSearchService;
            _globalElasticSearchService = globalElasticSearchService;
            _pageRouteRepository = pageRouteRepository;
        }

        public List<PageNewsType> GetPageNewsTypes()
        {
            return _db.PageNewsType.Where(x => !(x.IsDeleted)).ToList();
        }

        public List<PageNewsType> GetPageNewsType(List<int> ids)
        {
            return _db.PageNewsType.Where(st => !(st.IsDeleted) && ids.Contains(st.Id)).ToList();
        }

        public PageNewsVersion Add(PageNewsVersion pagenews, int pageRouteId)
        {
            try
            {
                var pageRouteVersion = _pageRouteVersionRepository.AddOrUpdatePageRouteVersion(pageRouteId);
                pagenews.PageRouteVersionId = pageRouteVersion.Id;
                pagenews.VersionStatusEnum = VersionStatusEnum.Draft;
                pagenews.ChangeActionEnum = ChangeActionEnum.New;
                _db.PageNewsVersions.Add(pagenews);
                _db.SaveChanges();

                return _db.PageNewsVersions.FirstOrDefault(c => c.Id == pagenews.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PageNewsVersion Update(PageNewsVersion pagenews, int pageRouteId)
        {
            try
            {
                var pageRouteVersion = _pageRouteVersionRepository.AddOrUpdatePageRouteVersion(pageRouteId);
                var existingNewsVer = _db.PageNewsVersions.Find(pagenews.Id);
                if (existingNewsVer.VersionStatusEnum == VersionStatusEnum.Approved || existingNewsVer.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    pagenews.Id = 0;
                    pagenews.VersionStatusEnum = VersionStatusEnum.Draft;
                    pagenews.ChangeActionEnum = ChangeActionEnum.Update;
                    pagenews.PageRouteVersionId = pageRouteVersion.Id;
                    _db.PageNewsVersions.Add(pagenews);
                }
                else
                {
                    pagenews.VersionStatusEnum = VersionStatusEnum.Draft;
                    pagenews.PageRouteVersionId = pageRouteVersion.Id;
                    pagenews.ChangeActionEnum = pagenews.ChangeActionEnum == ChangeActionEnum.New ? ChangeActionEnum.New : ChangeActionEnum.Update;
                    _db.Entry(existingNewsVer).CurrentValues.SetValues(pagenews);
                    var newsTypes = _db.NewsTypesForNewsVersions.Where(d => d.PageNewsVersionId == pagenews.Id).ToList();
                    _db.NewsTypesForNewsVersions.RemoveRange(newsTypes);
                    foreach (var type in pagenews.NewsTypesForNewsVersions)
                    {
                        type.PageNewsVersionId = pagenews.Id;
                    }
                    _db.NewsTypesForNewsVersions.AddRange(pagenews.NewsTypesForNewsVersions);
                }
                _db.SaveChanges();
                return _db.PageNewsVersions.FirstOrDefault(s => s.Id == pagenews.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var existingNewsVer = _db.PageNewsVersions.Find(id);
                if (existingNewsVer.VersionStatusEnum == VersionStatusEnum.Draft && existingNewsVer.PageNewsId == null)
                {
                    existingNewsVer.IsDeleted = true;
                    existingNewsVer.ChangeActionEnum = ChangeActionEnum.Delete;
                    _db.PageNewsVersions.Update(existingNewsVer);
                    return true;
                }
                else
                {
                    var pageRouteVer = _db.PageRouteVersions.Find(existingNewsVer.PageRouteVersionId);
                    var pageRouteVersion = _pageRouteVersionRepository.AddOrUpdatePageRouteVersion(pageRouteVer.PageRouteId.Value);

                    if (existingNewsVer.VersionStatusEnum == VersionStatusEnum.Approved || existingNewsVer.VersionStatusEnum == VersionStatusEnum.Ignored)
                    {
                        existingNewsVer.Id = 0;
                        existingNewsVer.VersionStatusEnum = VersionStatusEnum.Draft;
                        //existingNewsVer.IsDeleted = true;
                        existingNewsVer.ChangeActionEnum = ChangeActionEnum.Delete;
                        existingNewsVer.PageRouteVersionId = pageRouteVersion.Id;
                        _db.PageNewsVersions.Add(existingNewsVer);
                    }
                    else
                    {
                        existingNewsVer.VersionStatusEnum = VersionStatusEnum.Draft;
                        //existingNewsVer.IsDeleted = true;
                        existingNewsVer.ChangeActionEnum = ChangeActionEnum.Delete;
                        existingNewsVer.PageRouteVersionId = pageRouteVersion.Id;
                        _db.PageNewsVersions.Update(existingNewsVer);
                    }
                    _db.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public NewsViewModel Get(int id)
        {
            var newsVer = _db.PageNewsVersions.Include(d => d.NewsTypesForNewsVersions).FirstOrDefault(p => p.Id == id);
            List<PageNewsType> NewsType = GetPageNewsTypes();
            if (newsVer != null)
            {
                if (newsVer.VersionStatusEnum == VersionStatusEnum.Approved || newsVer.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    var news = _db.PageNews.Include(d => d.NewsTypesForNews).FirstOrDefault(p => p.Id == newsVer.PageNewsId);
                    NewsViewModel NewsViewModel = new NewsViewModel
                    {
                        Id = newsVer.Id,
                        EnTitle = news.EnTitle,
                        ArTitle = news.ArTitle,
                        EnDescription = news.EnDescription,
                        ArDescription = news.ArDescription,
                        EnShortDescription = news.EnShortDescription,
                        ArShortDescription = news.ArShortDescription,
                        IsActive = news.IsActive,
                        url = news.Url,
                        CreationDate = news.CreationDate,
                        CreatedById = news.CreatedById,
                        Date = news.Date,
                        NewsTypeIds = string.Join(",", news.NewsTypesForNews.Select(a => a.NewsTypeId)),
                        PageNewsId = news.Id
                    };

                    return NewsViewModel;
                }
                else
                {
                    NewsViewModel NewsViewModel = new NewsViewModel
                    {
                        Id = newsVer.Id,
                        EnTitle = newsVer.EnTitle,
                        ArTitle = newsVer.ArTitle,
                        EnDescription = newsVer.EnDescription,
                        ArDescription = newsVer.ArDescription,
                        EnShortDescription = newsVer.EnShortDescription,
                        ArShortDescription = newsVer.ArShortDescription,
                        IsActive = newsVer.IsActive,
                        url = newsVer.Url,
                        CreationDate = newsVer.CreationDate,
                        CreatedById = newsVer.CreatedById,
                        Date = newsVer.Date,
                        NewsTypeIds = string.Join(",", newsVer.NewsTypesForNewsVersions.Select(a => a.NewsTypeId)),
                        PageNewsId = newsVer.PageNewsId,
                        VersionStatusEnum = newsVer.VersionStatusEnum,
                        ChangeActionEnum = newsVer.ChangeActionEnum
                    };

                    return NewsViewModel;
                }
            }
            return null;
        }

        public IEnumerable<PageNewsListViewModel> GetPageNewsByPageRouteId(int pageRouteId)
        {
            var pageRouteVersionId = 0;

            var pageRouteVersion = _pageRouteVersionRepository.GetByPageRoute(pageRouteId);

            if (pageRouteVersion != null) pageRouteVersionId = pageRouteVersion.Id;

            //join betwen version and non version pageNews tables
            //takes the value from version if non version is not exsit, version status is draft or submited
            var queryright = (from nw in _db.PageNews.Where(d => !d.IsDeleted && d.PageRouteId == pageRouteId)
                              from nwv in _db.PageNewsVersions.Where(d => d.PageNewsId == nw.Id && !d.IsDeleted
                              && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).Take(1)
                              select new PageNewsListViewModel
                              {
                                  Id = nw.Id,
                                  VerId = nwv.Id,
                                  EnTitle = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.EnTitle : nw.EnTitle,
                                  EnglishDescription = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.EnDescription : nw.EnDescription,
                                  EnglishShortDescription = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.EnShortDescription : nw.EnShortDescription,
                                  ArTitle = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.ArTitle : nw.ArTitle,
                                  arabicShortDescription = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.ArShortDescription : nw.ArShortDescription,
                                  IsActive = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.IsActive : nw.IsActive,
                                  VersionStatusEnum = nwv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  Date = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.Date : nw.Date,
                                      CreatedById = (nw == null || nwv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || nwv.VersionStatusEnum == VersionStatusEnum.Submitted) ? nwv.CreatedById : nw.CreatedById,
                                  ChangeActionEnum = nwv.ChangeActionEnum ?? ChangeActionEnum.New,
                              });
            //get the rest of version from pageNews which not included in previous join
            var queryleft = (from nwv in _db.PageNewsVersions.Where(d => !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored
                             && d.PageRouteVersionId == pageRouteVersionId)
                             where !_db.PageNews.Any(d => d.Id == nwv.PageNewsId)
                             select new PageNewsListViewModel
                             {
                                 Id = 0,
                                 VerId = nwv.Id,
                                 EnTitle = nwv.EnTitle,
                                 EnglishDescription = nwv.EnDescription,
                                 EnglishShortDescription = nwv.EnShortDescription,
                                 ArTitle = nwv.ArTitle,
                                 arabicShortDescription = nwv.ArShortDescription,
                                 IsActive = nwv.IsActive,
                                 VersionStatusEnum = nwv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                 ChangeActionEnum = nwv.ChangeActionEnum ?? ChangeActionEnum.New,
                                 CreatedById = nwv.CreatedById,
                                 Date = nwv.Date
                             });
            //join the right and the left queries 
            return queryright.Union(queryleft).OrderByDescending(d => d.Date).ToList();

        }
        public List<int> GetPageNewsBySearchWord(string SearchWord, string lang)
        {
            var pageNewsIds = _db.PageNews.Where(s => !(s.IsDeleted) && lang == "en" ? ((s.EnTitle.Contains(SearchWord) || s.EnDescription.Contains(SearchWord) || s.EnShortDescription.Contains(SearchWord))) : (s.ArTitle.Contains(SearchWord) || s.ArDescription.Contains(SearchWord) || s.ArShortDescription.Contains(SearchWord))).Select(a => a.Id).ToList();
            return pageNewsIds;
        }


        public PageNews GetSinglePageNewsByPageNewsId(int PageNewsId)
        {
            var SinglepageNews = _db.PageNews.Where(s => s.Id == PageNewsId).FirstOrDefault();
            return SinglepageNews;
        }

        public IEnumerable<PageNews> GetPageNews()
        {
            return _db.PageNews.Include(x => x.NewsTypesForNews).ThenInclude(x => x.NewsType).Where(d => !d.IsDeleted && d.IsActive).OrderByDescending(a => a.Date).ToList();
        }

        public bool ApplySubmitRequest(int id, string userId, string pageLink)
        {
            try
            {
                var ifApprovalExist = _approvalNotificationsRepository.GetByPageNameAndRelatedId("News", id);
                var newsVersion = _db.PageNewsVersions.Find(id);
                var pageVer = _db.PageRouteVersions.Find(newsVersion.PageRouteVersionId);
                newsVersion.VersionStatusEnum = VersionStatusEnum.Submitted;
                _db.PageNewsVersions.Update(newsVersion);
                if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    _db.ApprovalNotifications.Add(new ApprovalNotification
                    {
                        ChangeAction = newsVersion.ChangeActionEnum.Value,
                        ChangesDateTime = DateTime.Now,
                        ChangeType = ChangeType.PageContent,
                        ContentManagerId = userId,
                        PageLink = $"{pageLink}/{id}?pageRouteId={pageVer.PageRouteId}",
                        PageName = "News",
                        PageType = PageType.Static,
                        RelatedVersionId = id,
                        VersionStatusEnum = VersionStatusEnum.Submitted,

                    });
                }

                var draftNews = _db.PageNewsVersions.Where(d => d.PageRouteVersionId == newsVersion.PageRouteVersionId && d.Id != id &
                d.VersionStatusEnum == VersionStatusEnum.Draft).Any();
                if (!draftNews)
                {
                    pageVer.ContentVersionStatusEnum = VersionStatusEnum.Submitted;
                    _db.PageRouteVersions.Update(pageVer);
                }
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Approve(int id, int approvalId, string userId)
        {
            var newsVer = _db.PageNewsVersions.Include(d => d.NewsTypesForNewsVersions).First(d => d.Id == id);
            if (newsVer != null)
            {
                var pageRouteId = _db.PageRouteVersions.Find(newsVer.PageRouteVersionId).PageRouteId;
                PageNews news;
                if (newsVer.ChangeActionEnum == ChangeActionEnum.New)
                {
                    news = new PageNews
                    {
                        ApprovalDate = DateTime.Now,
                        ApprovedById = userId,
                        ArDescription = newsVer.ArDescription,
                        ArShortDescription = newsVer.ArShortDescription,
                        CreatedById = newsVer.CreatedById,
                        ArTitle = newsVer.ArTitle,
                        CreationDate = newsVer.CreationDate,
                        Date = newsVer.Date,
                        EnDescription = newsVer.EnDescription,
                        EnShortDescription = newsVer.EnShortDescription,
                        EnTitle = newsVer.EnTitle,
                        IsActive = newsVer.IsActive,
                        IsDeleted = false,
                        PageRouteId = pageRouteId,
                        Url = newsVer.Url,
                        NewsTypesForNews = newsVer.NewsTypesForNewsVersions.Select(d => new NewsTypesForNews
                        {
                            NewsTypeId = d.NewsTypeId
                        }).ToList()
                    };
                    _db.PageNews.Add(news);
                    _db.SaveChanges();

                    news = _db.PageNews.Include(x => x.NewsTypesForNews).ThenInclude(x => x.NewsType).FirstOrDefault(x => x.Id == news.Id);

                    if (news.IsActive)
                        _pageNewsElasticSearchService.AddAsync(news);
                }
                else if (newsVer.ChangeActionEnum == ChangeActionEnum.Update)
                {
                    news = _db.PageNews.FirstOrDefault(x => x.Id == newsVer.PageNewsId);
                    news.ApprovalDate = DateTime.Now;
                    news.ApprovedById = userId;
                    news.ArDescription = newsVer.ArDescription;
                    news.ArShortDescription = newsVer.ArShortDescription;
                    news.ArTitle = newsVer.ArTitle;
                    news.Date = newsVer.Date;
                    news.EnDescription = newsVer.EnDescription;
                    news.EnShortDescription = newsVer.EnShortDescription;
                    news.EnTitle = newsVer.EnTitle;
                    news.IsActive = newsVer.IsActive;
                    news.Url = newsVer.Url;
                    _db.PageNews.Update(news);
                    var types = _db.NewsTypesForNews.Where(d => d.PageNewsId == news.Id).ToList();
                    _db.NewsTypesForNews.RemoveRange(types);
                    _db.NewsTypesForNews.AddRange(newsVer.NewsTypesForNewsVersions.Select(d => new NewsTypesForNews
                    {
                        NewsTypeId = d.NewsTypeId,
                        PageNewsId = news.Id
                    }).ToList());

                    _db.SaveChanges();

                    news = _db.PageNews.Include(x => x.NewsTypesForNews).ThenInclude(x => x.NewsType).FirstOrDefault(x => x.Id == news.Id);

                    if (news.IsActive)
                    {
                        _pageNewsElasticSearchService.DeleteAsync(news);
                        _pageNewsElasticSearchService.AddAsync(news);
                    }
                    else
                    {
                        _pageNewsElasticSearchService.DeleteAsync(news);
                    }

                }
                else
                {
                    news = _db.PageNews.Find(newsVer.PageNewsId);
                    news.IsDeleted = true;
                    _db.PageNews.Update(news);

                    _pageNewsElasticSearchService.DeleteAsync(news);
                }
                _db.SaveChanges();
                newsVer.VersionStatusEnum = VersionStatusEnum.Approved;
                newsVer.PageNewsId = news.Id;
                _db.PageNewsVersions.Update(newsVer);

                var notification = _db.ApprovalNotifications.Find(approvalId);
                notification.VersionStatusEnum = VersionStatusEnum.Approved;
                _db.ApprovalNotifications.Update(notification);

                var submittedNews = _db.PageNewsVersions.Where(d => d.PageRouteVersionId == newsVer.PageRouteVersionId && d.Id != id &
               d.VersionStatusEnum == VersionStatusEnum.Submitted).Any();
                if (!submittedNews)
                {
                    var pageVer = _db.PageRouteVersions.Find(newsVer.PageRouteVersionId);
                    pageVer.ContentVersionStatusEnum = VersionStatusEnum.Approved;
                    _db.PageRouteVersions.Update(pageVer);
                }
                _db.SaveChanges();

                try
                {
                    _globalElasticSearchService.DeleteAsync(pageRouteId ?? 0);
                    _globalElasticSearchService.AddAsync(_pageRouteRepository.GetPageData(pageRouteId ?? 0));
                }
                catch { }

                return true;
            }
            return false;
        }

        public bool Ignore(int id, int approvalId, string userId)
        {
            var newsVer = _db.PageNewsVersions.Include(d => d.NewsTypesForNewsVersions).First(d => d.Id == id);
            if (newsVer != null)
            {
                var pageRouteId = _db.PageRouteVersions.Find(newsVer.PageRouteVersionId).PageRouteId;

                newsVer.VersionStatusEnum = VersionStatusEnum.Ignored;
                _db.PageNewsVersions.Update(newsVer);

                var notification = _db.ApprovalNotifications.Find(approvalId);
                notification.VersionStatusEnum = VersionStatusEnum.Ignored;
                _db.ApprovalNotifications.Update(notification);

                var submittedNews = _db.PageNewsVersions.Where(d => d.PageRouteVersionId == newsVer.PageRouteVersionId && d.Id != id &
              d.VersionStatusEnum == VersionStatusEnum.Submitted).Any();
                if (!submittedNews)
                {
                    var pageVer = _db.PageRouteVersions.Find(newsVer.PageRouteVersionId);
                    pageVer.ContentVersionStatusEnum = VersionStatusEnum.Approved;
                    _db.PageRouteVersions.Update(pageVer);
                }
                _db.SaveChanges();

                return true;
            }
            return false;
        }

        public IEnumerable<PageNews> GetPageNewsPaginate(int pageNum, int typeId, out int totalCount, string lang)
        {
            if (lang == "en")
            {
                var data = _db.PageNews.Include(x => x.NewsTypesForNews).ThenInclude(x => x.NewsType).Where(d => !string.IsNullOrWhiteSpace(d.EnShortDescription) && !string.IsNullOrWhiteSpace(d.EnTitle) && !d.IsDeleted && d.IsActive && (d.NewsTypesForNews.Any(t => t.NewsTypeId == typeId) || typeId == 0)).OrderByDescending(a => a.Date);
                totalCount = data.Count();
                return data.Skip(10 * (pageNum - 1)).Take(10).ToList();
            }
            else
            {

                var data = _db.PageNews.Include(x => x.NewsTypesForNews).ThenInclude(x => x.NewsType).Where(d => !d.IsDeleted && d.IsActive && (d.NewsTypesForNews.Any(t => t.NewsTypeId == typeId) || typeId == 0)).OrderByDescending(a => a.Date);
                totalCount = data.Count();
                return data.Skip(10 * (pageNum - 1)).Take(10).ToList();
            }
        }
    }
}
