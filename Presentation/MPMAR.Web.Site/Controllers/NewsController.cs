using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Web.Site.ViewModels;
using MPMAR.Business.Interfaces;
using Microsoft.AspNetCore.Hosting;
using MPMAR.Data;
using Microsoft.Extensions.Configuration;

namespace MPMAR.Web.Site.Controllers
{
    public class NewsController : BaseController
    {

        private readonly IPageNewsRepository _PageNewsRepository;
        private readonly IWebHostEnvironment _IWebHostEnvironment;
        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly IConfiguration _configuration;
        private readonly IPageNewsElasticSearchService _pageNewsElasticSearchService;

        public NewsController(IPageNewsRepository PageNewsRepository, IWebHostEnvironment WebHostEnvironment, IPageRouteRepository pageRouteRepository, IConfiguration configuration, IPageNewsElasticSearchService pageNewsElasticSearchService)
        {
            _PageNewsRepository = PageNewsRepository;
            _IWebHostEnvironment = WebHostEnvironment;
            _pageRouteRepository = pageRouteRepository;
            _configuration = configuration;
            _pageNewsElasticSearchService = pageNewsElasticSearchService;
        }
        /// <summary>
        /// get  News page index
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public IActionResult Index(string lang)
        {
            var pageRoute = _pageRouteRepository.GetByControllerName(nameof(NewsController)[0..^10]);
            if (pageRoute == null || !pageRoute.IsActive || pageRoute.IsDeleted)
            {
                return View("Error");
            }
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            PageNewsListViewModel PageNewsListVModel = new PageNewsListViewModel();
            //var PageNews = GetPageNews();
            //foreach (var item in PageNews)
            //{
            //    if (item.Url != null)
            //        item.Url = imageBaseURL + item.Url.Replace(" ", "%20");
            //}
            PageNewsListVModel.NewsList = new List<PageNewsListViewModel.News>();

            PageNewsListVModel.PageNewsTypes = _PageNewsRepository.GetPageNewsTypes();
            PageNewsListVModel.WebHostEnvironment = _IWebHostEnvironment.WebRootPath;

            SetUpSEO(lang, pageRoute);
            if (lang == null || lang.ToLower() == "ar")
            {
                ViewBag.Title = pageRoute.ArName;
                ViewBag.Nav = pageRoute.NavItem.ArName;
                ViewBag.NewsSearchTypeText = "كل";
            }
            else
            {
                ViewBag.Title = pageRoute.EnName;
                ViewBag.Nav = pageRoute.NavItem.EnName;
                ViewBag.NewsSearchTypeText = "All";
            }
            ViewBag.NewsSearchTypeValue = 0;
            return View(PageNewsListVModel);
        }
        [HttpGet]
        public ActionResult NewsList(int typeId, string lang, int pageNum)
        {
            int totalCount = 0;
            var PageNews = _PageNewsRepository.GetPageNewsPaginate(pageNum, typeId, out totalCount, lang);
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            foreach (var item in PageNews)
            {
                if (item.Url != null)
                    item.Url = imageBaseURL + item.Url.Replace(" ", "%20");
            }
            ViewBag.TotalCount = totalCount;
            return PartialView("_newsContainer", MapToPageNewsListViewModel(PageNews, lang));
        }
        /// <summary>
        /// get list of PageNewsListViewModel from list of PageNews
        /// </summary>
        /// <param name="pageNews">list PageNews</param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public List<PageNewsListViewModel.News> MapToPageNewsListViewModel(IEnumerable<PageNews> pageNews, string lang)
        {
            if (lang == "en")
            {
                return pageNews.Select(News => new PageNewsListViewModel.News
                {
                    Id = News.Id,
                    EnTitle = News.EnTitle,
                    ArTitle = News.ArTitle,
                    EnDescription = News.EnDescription,
                    ArDescription = News.ArDescription,
                    EnShortDescription = News.EnShortDescription,
                    ArShortDescription = News.ArShortDescription,
                    Url = News.Url,
                    Date = News.Date.Value.ToString("dd MMMM yyyy"),
                    NewsTypes = string.Join(",", News.NewsTypesForNews.Select(a => a.NewsType.EnName).ToList()),
                    NewsTypesClasses = string.Join(" ", News.NewsTypesForNews.Select(a => a.NewsType.EnName.Replace(" ", "_")).ToList())
                }).ToList();
            }
            else
            {
                return pageNews.Select(News => new PageNewsListViewModel.News
                {
                    Id = News.Id,
                    EnTitle = News.EnTitle,
                    ArTitle = News.ArTitle,
                    EnDescription = News.EnDescription,
                    ArDescription = News.ArDescription,
                    EnShortDescription = News.EnShortDescription,
                    ArShortDescription = News.ArShortDescription,
                    Url = News.Url,
                    Date = News.Date.Value.ToString("dd MMMM yyyy", System.Globalization.CultureInfo.GetCultureInfo("ar-eg")),
                    NewsTypes = string.Join(",", News.NewsTypesForNews.Select(a => a.NewsType.ArName).ToList()),
                    NewsTypesClasses = string.Join(" ", News.NewsTypesForNews.Select(a => a.NewsType.ArName.Replace(" ", "_")).ToList())
                }).ToList();
            }

        }

        /// <summary>
        /// get list of PageNews
        /// </summary>
        /// <returns></returns>
        public List<PageNews> GetPageNews()
        {
            return _PageNewsRepository.GetPageNews().ToList();

        }
        /// <summary>
        /// get news matched to the search word
        /// </summary>
        /// <param name="SearchWord"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public List<int> GetMatchedNews(string SearchWord, string lang)
        {
            List<int> MatchedNewsIds = _PageNewsRepository.GetPageNewsBySearchWord(SearchWord, lang);
            return MatchedNewsIds;
        }

        /// <summary>
        /// search for news in elastic search
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="type"></param>
        /// <param name="lang"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SearchNews(string searchText, string type, string lang, int pageNum = 1)
        {
            var news = _pageNewsElasticSearchService.Find(searchText, int.Parse(type), lang, pageNum);
            var totalCount = _pageNewsElasticSearchService.GetCount(searchText, int.Parse(type), lang).Result;
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            foreach (var item in news.Result)
            {
                if (item.Url != null)
                    item.Url = imageBaseURL + item.Url.Replace(" ", "%20");
            }

            ViewBag.TotalCount = totalCount;
            return PartialView("_newsContainer", MapToPageNewsListViewModel(news.Result, lang));
        }

    }
}