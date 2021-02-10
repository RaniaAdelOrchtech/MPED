using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Services;
using MPMAR.Data;
using MPMAR.Web.Site.ViewModels;

namespace MPMAR.Web.Site.Controllers
{
    public class EventCalendarController : BaseController
    {

        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly IPageEventVersionsRepository _pageEventRepository;


        private readonly ApplicationDbContext _dataAccessService;
        public EventCalendarController(ApplicationDbContext dataAccessService, IPageEventVersionsRepository pageEventRepository, IPageRouteRepository pageRouteRepository)
        {
            _dataAccessService = dataAccessService;
            _pageEventRepository = pageEventRepository;
            _pageRouteRepository = pageRouteRepository;
        }
        /// <summary>
        /// get specific week days
        /// </summary>
        /// <param name="weekStart">start day</param>
        /// <param name="startDate"></param>
        /// <param name="Week">week number</param>
        /// <returns></returns>
        public static List<DateTime> GetWeeks(DayOfWeek weekStart, DateTime startDate, int Week = 0)
        {
            List<DateTime> result = new List<DateTime>();

            DateTime current = DateTime.Today.AddDays(7 * Week).AddDays(weekStart - DateTime.Today.AddDays(7 * Week).DayOfWeek);
            result.Add(current);
            for (int i = 0; i < 6; i++)
            {
                current = current.AddDays(1);
                result.Add(current);
            }

            return result;
        }

        /// <summary>
        /// get EventCalendar index page
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public IActionResult Index(string lang)
        {
            var pageRoute = _pageRouteRepository.GetByControllerName(nameof(EventCalendarController)[0..^10]);
            if (pageRoute == null || !pageRoute.IsActive || pageRoute.IsDeleted)
            {
                return View("Error");
            }
            ViewPageEvents expando = new ViewPageEvents();
            var items = _pageEventRepository.GetAll().MapToPageEventVersionsViewModel();
            expando.ViewPageEventVersions = items;

            var Cname = HttpContext.Request.RouteValues["controller"].ToString();
            var MenuNames = _dataAccessService.PageRouteVersions.Where(i => i.ControllerName == Cname).Select(i => new { i.NavItem, i.EnName, i.ArName });

            foreach (var MenuName in MenuNames)
            {
                expando.MainEnTitle = MenuName.NavItem.EnName;
                expando.MainArTitle = MenuName.NavItem.ArName;
                expando.PageEnTitle = MenuName.EnName;
                expando.PageArTitle = MenuName.ArName;
            }
            expando.ContollerName = Cname;
           
            SetUpSEO(lang, pageRoute);
            if (lang == null || lang.ToLower() == "ar")
            {
                ViewBag.Title = pageRoute.ArName;
                ViewBag.Nav = pageRoute.NavItem.ArName;
            }
            else
            {
                ViewBag.Title = pageRoute.EnName;
                ViewBag.Nav = pageRoute.NavItem.EnName;
            }
            return View(expando);
        }
        /// <summary>
        /// get EventDetial page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult EventDetial(int id)
        {
           
            var items = _pageEventRepository.GetDetail(id).MapToPageEventVersionViewModel();
            var controllerName = HttpContext.Request.RouteValues["controller"].ToString();
            var MenuNames = _dataAccessService.PageRouteVersions.Where(i => i.ControllerName == controllerName).Select(i => new { i.NavItem,i.EnName,i.ArName });
     
            
            foreach (var MenuName in MenuNames)
            {
                items.MainEnTitle = MenuName.NavItem.EnName;
                items.MainArTitle = MenuName.NavItem.ArName;
                items.PageEnTitle = MenuName.EnName;
                items.PageArTitle = MenuName.ArName;
            }
            items.ContollerName = controllerName;

            return View(items);
        }
    }

}