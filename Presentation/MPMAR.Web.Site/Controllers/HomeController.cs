using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Models;
using MPMAR.Web.Site.Models;

namespace MPMAR.Web.Site.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IHP_BasicInfoReopsitory _hP_BasicInfoReopsitory;
        private readonly IConfiguration _configuration;
        private readonly IGlobalElasticSearchService _globalElasticSearchService;

        public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache, IHP_BasicInfoReopsitory hP_BasicInfoReopsitory, IConfiguration configuration, IGlobalElasticSearchService globalElasticSearchService)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _hP_BasicInfoReopsitory = hP_BasicInfoReopsitory;
            _configuration = configuration;
            _globalElasticSearchService = globalElasticSearchService;
        }
        /// <summary>
        /// get home page index
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public IActionResult Index(string lang)
        {
            //get current domain to use it in SEO link in image
            var CurrentDomain = _configuration.GetValue<string>("CurrentDomain");
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            ViewBag.PageName = "HomePage";
            var homePageBasicInfo = _hP_BasicInfoReopsitory.GetAll().FirstOrDefault();
            ViewBag.FavIconUrl = imageBaseURL + homePageBasicInfo.FavIconUrl;
            SetUpSEO(lang, new Data.PageRoute()
            {
                SeoDescriptionAR = homePageBasicInfo.SeoDescriptionAR,
                SeoDescriptionEN = homePageBasicInfo.SeoDescriptionEN,
                SeoOgTitleAR = homePageBasicInfo.SeoOgTitleAR,
                SeoOgTitleEN = homePageBasicInfo.SeoOgTitleEN,
                SeoTwitterCardAR = homePageBasicInfo.SeoTwitterCardAR,
                SeoTwitterCardEN = homePageBasicInfo.SeoTwitterCardEN

            }, CurrentDomain, CurrentDomain + "/EgyptFlag.jpg");
            if (lang == null || lang.Equals("ar"))
            {
                ViewData["Title"] = "الرئيسية";
            }
            else
            {
                ViewData["Title"] = "Home";
            }


            return View();
        }

        public IActionResult Privacy()
        {
            DateTime currentTime;
            DateTime cachedcurrentTime = DateTime.Now;
            bool isExist = _memoryCache.TryGetValue("CacheTime", out currentTime);
            if (!isExist)
            {
                currentTime = DateTime.Now;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.NeverRemove);


                _memoryCache.Set("CacheTime", currentTime, cacheEntryOptions);
            }
            else
            {
                cachedcurrentTime = _memoryCache.Get<DateTime>("CacheTime");
            }
            return Json(cachedcurrentTime);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> GlobalSearch([FromQuery]string lang, [FromQuery]string searchText, [FromQuery] int pageNum = 1)
        {
            var data = await _globalElasticSearchService.FindAsync(searchText, pageNum);

            ViewBag.SearchText = searchText;
            ViewBag.TotalCount = data.Count;
            ViewBag.pageNum = pageNum;

            return View(data.GlobalSearchModels.ToList());
        }
        [HttpGet]
        public async Task<IActionResult> GlobalSearchPaginate(string searchText, int pageNum = 1)
        {
            var data = await _globalElasticSearchService.FindAsync(searchText, pageNum);
            ViewBag.TotalCount = data.Count;
            return PartialView("_GlobalSearchContainer", data.GlobalSearchModels.ToList());
        }


    }
}
