using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Data;
using MPMAR.Web.Site.ViewModels;

namespace MPMAR.Web.Site.Controllers
{
    public class SitemapController : Controller
    {
        private readonly ApplicationDbContext _dataAccessService;

        public SitemapController(ApplicationDbContext dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }
        /// <summary>
        /// get Sitemap page index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewSitemap objView = new ViewSitemap();
            objView.PageRoutesList = new List<PageRoute>();
            objView.PageRoutes = new List<PageRoute>();
            objView.SiteMap = _dataAccessService.SiteMap.SingleOrDefault(i => i.Id == 1);
            var items1 = _dataAccessService.NavItems.Where(x => x.IsActive && !x.IsDeleted).OrderBy(i => i.Id).ToList();
            foreach (var item1 in items1)
            {
                List<PageRoute> objPage = new List<PageRoute>();
                objPage = _dataAccessService.PageRoutes.Where(i => i.NavItemId == item1.Id && i.IsActive && !i.IsDeleted).ToList();
                if (objPage.Count > 0)
                {
                    objView.PageRoutesList.AddRange(objPage);
                }
                List<PageRoute> objPageRoute = new List<PageRoute>();
                objPageRoute = _dataAccessService.PageRoutes.Where(i => i.NavItemId == item1.Id && i.IsActive && !i.IsDeleted).ToList();
                if (objPage.Count > 0)
                {
                    objView.PageRoutes.AddRange(objPageRoute);
                }
            }
            objView.NavItemList = items1;
            return View(objView);
        }
    }
}