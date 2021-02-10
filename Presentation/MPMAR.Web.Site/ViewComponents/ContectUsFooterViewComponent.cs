using Microsoft.AspNetCore.Mvc;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewComponents
{
    public class ContectUsFooterViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _dataAccessService;

        public ContectUsFooterViewComponent(ApplicationDbContext dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }



        public IViewComponentResult Invoke()
        {
            var items = _dataAccessService.PageContact.Where(i => i.IsDeleted != true && i.IsActive == true).OrderBy(i => i.Id).First();
            //GetMenuItemsAsync(HttpContext.User);
            return View(items);
        }
    }
}
