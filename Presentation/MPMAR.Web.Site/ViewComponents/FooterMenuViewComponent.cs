using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewComponents
{
    public class FooterMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _dataAccessService;

        public FooterMenuViewComponent(ApplicationDbContext dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }



        public IViewComponentResult Invoke()
        {
            var items = _dataAccessService.FooterMenuItem.Where(i => i.IsActive == true && i.IsDeleted != true).Include(x=>x.FooterMenuTitle).OrderBy(i => i.Order).ToList();
            //GetMenuItemsAsync(HttpContext.User);
            return View(items);
        }
    }
}
