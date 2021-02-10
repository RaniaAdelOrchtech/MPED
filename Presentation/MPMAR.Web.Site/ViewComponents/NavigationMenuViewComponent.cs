using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Web.Site.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewComponents
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _dataAccessService;
        private readonly IHP_BasicInfoReopsitory _hP_BasicInfoReopsitory;
        private readonly IConfiguration _configuration;

        public NavigationMenuViewComponent(ApplicationDbContext dataAccessService, IHP_BasicInfoReopsitory hP_BasicInfoReopsitory, IConfiguration configuration)
        {
            _dataAccessService = dataAccessService;
            _hP_BasicInfoReopsitory = hP_BasicInfoReopsitory;
            _configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            var homePageBasicInfo = _hP_BasicInfoReopsitory.GetAll().FirstOrDefault();
            if (homePageBasicInfo.FavIconUrl != null)
                homePageBasicInfo.FavIconUrl = imageBaseURL + homePageBasicInfo.FavIconUrl.Replace(" ", "%20");
            if (homePageBasicInfo.LogoUrl != null)
                homePageBasicInfo.LogoUrl = imageBaseURL + homePageBasicInfo.LogoUrl.Replace(" ", "%20");

            ViewBag.FavIconUrl = homePageBasicInfo.FavIconUrl;
            ViewBag.LogoUrl = homePageBasicInfo.LogoUrl;
            FavIcon.FavIconLink = homePageBasicInfo.FavIconUrl;
            var contactUs = _dataAccessService.PageContact.FirstOrDefault();
            var items1 = _dataAccessService.NavItems.Where(x => !x.IsDeleted && x.IsActive).OrderBy(i => i.Order).ThenByDescending(i => i.CreationDate).ToList();
            foreach (var item1 in items1)
            {
                item1.PageRoutes = _dataAccessService.PageRoutes.Where(i => i.NavItemId == item1.Id && !i.IsDeleted && i.IsActive && (!i.IsDynamicPage || i.PageSections.Any(x => !x.IsDeleted && x.IsActive))).OrderBy(x => x.Order).ThenByDescending(x => x.CreationDate).ToList();

            }
            ViewBag.ContactUsAr = contactUs.ArPageName;
            ViewBag.ContactUsEn = contactUs.EnPageName;

            return View(items1);
        }
    }
}
