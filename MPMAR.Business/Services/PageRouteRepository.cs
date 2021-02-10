using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Models;
using MPMAR.Data;
using MPMAR.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Text;

namespace MPMAR.Business.Services
{
    public class PageRouteRepository : IPageRouteRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;
        private readonly ILogger<PageRouteRepository> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string userName;
        private readonly string _userId;

        public PageRouteRepository(ApplicationDbContext db, IPageRouteVersionRepository pageRouteVersionRepository, ILogger<PageRouteRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _pageRouteVersionRepository = pageRouteVersionRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            _userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }


        /// <summary>
        /// Update a page route object values
        /// </summary>
        /// <param name="pageRoute">page route model</param>
        /// <returns>updated object</returns>
        public PageRoute Update(PageRoute pageRoute)
        {
            try
            {
                _db.PageRoutes.Add(pageRoute);
                int updated = _db.SaveChanges();

                if (updated > 0)
                {
                    _logger.LogInformation($"User: {userName} has updated nav item with name: {pageRoute.EnName}");
                }

                return pageRoute;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Delete a page route object from database by id
        /// </summary>
        /// <param name="id">page route id</param>
        /// <returns>Deleted object</returns>
        public PageRoute Delete(int id)
        {
            PageRoute pageRoute = _db.PageRoutes.Find(id);
            try
            {
                PageRouteVersion pageRouteVersion = pageRoute.MapToPageRouteVersion();
                pageRouteVersion.IsDeleted = true;
                pageRouteVersion.CreatedById = _userId;
                pageRouteVersion.CreationDate = DateTime.Now;
                _db.PageRouteVersions.Add(pageRouteVersion);
                int deleted = _db.SaveChanges();
                if (deleted > 0)
                {
                    _logger.LogInformation($"User: {userName} has deleted nav item with name: {pageRoute.EnName}");
                }

                return pageRoute;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get a page route object from database by id
        /// </summary>
        /// <param name="id">page route id</param>
        /// <returns>a single object from page route with the same id</returns>
        public PageRoute Get(int id)
        {
            return _db.PageRoutes.Include(x => x.NavItem).FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Get all not deleted dynamic pages from database
        /// </summary>
        /// <returns>IEnumerable of dynamic page route objects</returns>
        public IEnumerable<PageRoute> GetDynamicPages()
        {
            return _db.PageRoutes.Include(x => x.NavItem).Where(p => !p.IsDeleted && p.IsDynamicPage).OrderBy(n => n.Id);
        }

        /// <summary>
        /// Get all not deleted static pages from database
        /// </summary>
        /// <returns>IEnumerable of static page route objects</returns>
        public IEnumerable<PageRoute> GetStaticPages()
        {
            return _db.PageRoutes.Include(x => x.NavItem).Where(p => !p.IsDeleted && !p.IsDynamicPage).OrderBy(n => n.Id);
        }

        /// <summary>
        /// Add a new page route to database
        /// </summary>
        /// <param name="pageRoute">page route model</param>
        /// <returns></returns>
        public void Add(PageRoute pageRoute)
        {
            _db.Add(pageRoute);
            _db.SaveChanges();
        }

        /// <summary>
        /// Update a page route object values
        /// </summary>
        /// <param name="pageRoute">page route model</param>
        /// <returns></returns>
        public void UpdatePageRoute(PageRoute pageRoute)
        {
            _db.PageRoutes.Update(pageRoute);
            _db.SaveChanges();
        }

        /// <summary>
        /// Get all page routes which contains the controller name sent in parameter
        /// </summary>
        /// <param name="controllerName">controller name value</param>
        /// <returns>Single page route object</returns>
        public PageRoute GetByControllerName(string controllerName)
        {
            return _db.PageRoutes.Include(x => x.NavItem).FirstOrDefault(x => x.ControllerName == controllerName && x.IsActive && !x.IsDeleted);
        }

        public List<int> GetAllId()
        {
            return _db.PageRoutes.Include(x => x.NavItem).Where(x => x.IsActive && !x.IsDeleted && x.NavItem.IsActive && !x.NavItem.IsDeleted).Select(x => x.Id).ToList();
        }

        public GlobalSearchModel GetPageData(int pageRouteId)
        {
            var pageNameEnumPerController = new PageNameEnumPerController();
            var pageRoute = Get(pageRouteId);
            var data = new GlobalSearchModel()
            {
                Id = pageRoute.Id,
                ArTitle = pageRoute.ArName.Trim(),
                EnTitle = pageRoute.EnName.Trim(),
                URL = @$"/{pageRoute.ControllerName}?id={pageRoute.Id}",
                PageEnum = pageNameEnumPerController.PageNameEnumPerControllerDictionary.GetValueOrDefault(pageRoute.ControllerName)

            };
            switch (data.PageEnum)
            {
                case PageNameEnum.DynamicPage:
                    data.GlobalSearchContentModels = GetDynamicPageConten(pageRoute.Id);
                    break;
                case PageNameEnum.FormerMinistries:
                    data.GlobalSearchContentModels = GetFormerMinistriesContent();
                    break;
                case PageNameEnum.MinistryVision:
                    data.GlobalSearchContentModels = GetPageMinistryContent(pageRoute.Id);
                    break;
                case PageNameEnum.MinistrySpeech:
                    data.GlobalSearchContentModels = GetPageMinistryContent(pageRoute.Id);
                    break;
                case PageNameEnum.News:
                    data.GlobalSearchContentModels = GetNewsContent();
                    break;
                case PageNameEnum.EgyptVision:
                    data.GlobalSearchContentModels = GetEgyptVisionContent();
                    break;
                case PageNameEnum.Analytics:
                    data.GlobalSearchContentModels = GetAnalyticsContent();
                    break;
                case PageNameEnum.PhotoArchive:
                    data.GlobalSearchContentModels = GetPhotoArchiveContent();
                    break;
            }
            return data;
        }
        private List<GlobalSearchContentModel> GetDynamicPageConten(int pageRouteId)
        {
            var data = new List<GlobalSearchContentModel>();
            var sections = _db.PageSections.Where(x => x.PageRouteId == pageRouteId && x.IsActive && !x.IsDeleted).OrderBy(x => x.Order).ToList();

            var sectionsCards = _db.PageSectionCards.Where(x => x.IsActive && !x.IsDeleted && sections.Select(x => x.Id).Contains(x.PageSectionId)).ToList();
            int i = 0;
            foreach (var item in sections)
            {
                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArTitle?.Trim(),
                    EnContent = item.EnTitle?.Trim(),
                    ShowInResult = false
                });
                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArDescription,
                    EnContent = item.EnDescription,
                    ShowInResult = (i == 0)
                });
                i++;
            }
            foreach (var item in sectionsCards)
            {
                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArTitle?.Trim(),
                    EnContent = item.EnTitle?.Trim(),
                    ShowInResult = false
                });
                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArDescription,
                    EnContent = item.EnDescription,
                    ShowInResult = false
                });
            }

            return data;
        }
        private List<GlobalSearchContentModel> GetFormerMinistriesContent()
        {
            var data = new List<GlobalSearchContentModel>();
            var formerMinistryPageInfo = _db.FormerMinistriesPageInfos.FirstOrDefault();
            data.Add(new GlobalSearchContentModel()
            {
                ArContent = formerMinistryPageInfo.Title1Ar?.Trim(),
                EnContent = formerMinistryPageInfo.Title1En?.Trim(),
                ShowInResult = false
            });
            data.Add(new GlobalSearchContentModel()
            {
                ArContent = formerMinistryPageInfo.Title2Ar?.Trim(),
                EnContent = formerMinistryPageInfo.Title2En?.Trim(),
                ShowInResult = false
            });
            data.Add(new GlobalSearchContentModel()
            {
                ArContent = formerMinistryPageInfo.DescriptionAr,
                EnContent = formerMinistryPageInfo.DescriptionEn,
                ShowInResult = true
            });
            var minstries = _db.MinistryTimeLine.Where(x => x.IsActive && !x.IsDeleted).ToList();
            foreach (var item in minstries)
            {
                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArName?.Trim(),
                    EnContent = item.EnName?.Trim(),
                    ShowInResult = false
                });
                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArDescription,
                    EnContent = item.EnDescription,
                    ShowInResult = false
                });
            }
            return data;
        }
        private List<GlobalSearchContentModel> GetPageMinistryContent(int pageRouteId)
        {
            var data = new List<GlobalSearchContentModel>();
            var pageMinistryData = _db.PageMinistry.Where(x => x.IsActive && !x.IsDeleted && x.PageRouteId == pageRouteId).ToList();
            int i = 0;
            foreach (var item in pageMinistryData)
            {
                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArName?.Trim(),
                    EnContent = item.EnName?.Trim(),
                    ShowInResult = false
                });

                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArContent,
                    EnContent = item.EnContent,
                    ShowInResult = (i == 0)
                });

                i++;
            }

            return data;
        }
        private List<GlobalSearchContentModel> GetNewsContent()
        {
            var data = new List<GlobalSearchContentModel>();
            var news = _db.PageNews.Where(x => x.IsActive && !x.IsDeleted).ToList();

            data.Add(new GlobalSearchContentModel()
            {
                ArContent = "بيانات صحفية صفحة إخبارية لنشر الأخبار المتعلقة بوزارة التخطيط والتنمية الاقتصادية المصرية",
                EnContent = "News is a page designed for publishing the press releases of the Ministry of Planning and Economic Development",
                ShowInResult = true
            });

            foreach (var item in news)
            {

                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArTitle?.Trim(),
                    EnContent = item.EnTitle?.Trim(),
                    ShowInResult = false
                });

                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArShortDescription,
                    EnContent = item.EnShortDescription,
                    ShowInResult = false
                });

                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArDescription,
                    EnContent = item.EnDescription,
                    ShowInResult = false
                });


            }
            return data;

        }
        private List<GlobalSearchContentModel> GetEgyptVisionContent()
        {
            var data = new List<GlobalSearchContentModel>();
            var egyptVisionData = _db.EgyptVision.Where(x => x.IsActive && !x.IsDeleted).ToList();
            int i = 0;
            foreach (var item in egyptVisionData)
            {
                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArEgyptVisionName?.Trim(),
                    EnContent = item.EnEgyptVisionName?.Trim(),
                    ShowInResult = false
                });

                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArEgyptVisionSmallDesc,
                    EnContent = item.EnEgyptVisionSmallDesc,
                    ShowInResult = (i == 0)
                });

                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArEgyptVisionDesc,
                    EnContent = item.ArEgyptVisionDesc,
                    ShowInResult = false
                });

                i++;
            }
            return data;

        }
        private List<GlobalSearchContentModel> GetAnalyticsContent()
        {
            var data = new List<GlobalSearchContentModel>();
            var analyticsData = _db.EconomicIndicators.FirstOrDefault();

            data.Add(new GlobalSearchContentModel()
            {
                ArContent = analyticsData.ImageTitleAr1?.Trim(),
                EnContent = analyticsData.ImageTitleEn1?.Trim(),
                ShowInResult = false
            });
            data.Add(new GlobalSearchContentModel()
            {
                ArContent = analyticsData.ImageTitleAr2?.Trim(),
                EnContent = analyticsData.ImageTitleEn2?.Trim(),
                ShowInResult = false
            });
            data.Add(new GlobalSearchContentModel()
            {
                ArContent = analyticsData.ImageTitleAr3?.Trim(),
                EnContent = analyticsData.ImageTitleEn3?.Trim(),
                ShowInResult = false
            });
            data.Add(new GlobalSearchContentModel()
            {
                ArContent = analyticsData.ImageDiscriptionAr1,
                EnContent = analyticsData.ImageDiscriptionEn1,
                ShowInResult = true
            });
            data.Add(new GlobalSearchContentModel()
            {
                ArContent = analyticsData.ImageDiscriptionAr2,
                EnContent = analyticsData.ImageDiscriptionEn2,
                ShowInResult = false
            });
            data.Add(new GlobalSearchContentModel()
            {
                ArContent = analyticsData.ImageDiscriptionAr3,
                EnContent = analyticsData.ImageDiscriptionEn3,
                ShowInResult = false
            });
            return data;

        }

        private List<GlobalSearchContentModel> GetPhotoArchiveContent()
        {
            var data = new List<GlobalSearchContentModel>();
            var photoArchive = _db.PhotoArchive.Where(x => x.IsActive && !x.IsDeleted).ToList();

            data.Add(new GlobalSearchContentModel()
            {
                ArContent = "مكتبة الصور صفحة مصممة لنشر الصور الخاصة بوزارة التخطيط والتنمية الاقتصادية المصرية",
                EnContent = "Pictures Library is a page designed for publishing the pictures of the Ministry of Planning and Economic Development",
                ShowInResult = true
            });

            foreach (var item in photoArchive)
            {

                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArPhotoArchiveDesc,
                    EnContent = item.EnPhotoArchiveDesc,
                    ShowInResult = false
                });

                data.Add(new GlobalSearchContentModel()
                {
                    ArContent = item.ArPhotoArchiveName.Trim(),
                    EnContent = item.EnPhotoArchiveName.Trim(),
                    ShowInResult = false
                });

            }
            return data;

        }


    }
}
