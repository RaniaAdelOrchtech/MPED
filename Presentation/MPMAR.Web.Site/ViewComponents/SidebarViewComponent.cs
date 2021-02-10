using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPMAR.Data;
using MPMAR.Web.Site.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _dataAccessService;
        private readonly IConfiguration _configuration;

        public SidebarViewComponent(IConfiguration configuration, ApplicationDbContext dataAccessService)
        {
            _dataAccessService = dataAccessService;
            _configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            var items = _dataAccessService.LeftMenuItem.OrderBy(i => i.Id).ToList();
            //get image base url to add it to the relative url
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            foreach (var model in items)
            {
                if (model.ImagePath != null)
                    model.ImagePath = imageBaseURL + model.ImagePath.Replace(" ", "%20");
            }
            return View(items);
        }
    }
}
