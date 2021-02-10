using Microsoft.AspNetCore.Mvc;
using MPMAR.Data;
using MPMAR.Web.Site.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewComponents
{
    public class RightMenuHomePageViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _dataAccessService;

        public RightMenuHomePageViewComponent(ApplicationDbContext dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }



        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
