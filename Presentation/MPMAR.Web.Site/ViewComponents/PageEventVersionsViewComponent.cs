using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Web.Site.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Site.ViewComponents
{
    public class PageEventVersionsViewComponent : ViewComponent
    {
        private readonly IPageEventVersionsRepository _pageEventVersionsRepository;
        private readonly IPageRouteRepository _pageRouteRepository;

        public PageEventVersionsViewComponent(IPageEventVersionsRepository pageEventVersionsRepository, IPageRouteRepository pageRouteRepository)
        {
            _pageEventVersionsRepository = pageEventVersionsRepository;
            _pageRouteRepository = pageRouteRepository;
        }


        public IViewComponentResult Invoke()
        {
            var pageRoute = _pageRouteRepository.GetByControllerName(nameof(EventCalendarController)[0..^10]);
            //get top 3 events depend on date and ShowInHome bool
            var _event = _pageEventVersionsRepository.GetAllPageEvent().Where(x => x.EventStartDate > DateTime.Now).OrderByDescending(x => (x.ShowInHome ? 1 : 0)).ThenByDescending(x => x.EventStartDate).Take(3).ToList();
            ViewBag.ActivatePage = pageRoute != null && _event.Count > 0;
            return View(_event);
        }
    }
}
