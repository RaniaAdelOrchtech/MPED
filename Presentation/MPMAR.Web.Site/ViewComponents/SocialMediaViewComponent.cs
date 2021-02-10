using Microsoft.AspNetCore.Mvc;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewComponents
{
    public class SocialMediaViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _dataAccessService;

        public SocialMediaViewComponent(ApplicationDbContext dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }




        public IViewComponentResult Invoke()
        {
            var items = _dataAccessService.SocialMedia.Where(x => !x.IsDeleted).OrderBy(i=>i.Order).ToList();
            //GetMenuItemsAsync(HttpContext.User);
            return View(items);
        }
    }
    public class HeaderSocialMediaViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _dataAccessService;

        public HeaderSocialMediaViewComponent(ApplicationDbContext dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }




        public IViewComponentResult Invoke()
        {
            var items = _dataAccessService.SocialMedia.Where(x=>!x.IsDeleted).OrderBy(i => i.Order).ToList();

            return View(items);
        }
    }
}
