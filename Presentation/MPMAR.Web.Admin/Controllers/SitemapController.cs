using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Common;
using MPMAR.Data;
using MPMAR.Data.Consts;
using MPMAR.Web.Admin.Helpers;
using NToastNotify;
using MPMAR.Business;
using MPMAR.Common.Interfaces;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class SitemapController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly ISiteMapRepository _siteMapRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
private readonly IEventLogger<SitemapController> _eventLogger;


        public SitemapController(ISiteMapRepository siteMapRepository, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<SitemapController> eventLogger)
        {
            _siteMapRepository = siteMapRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
_eventLogger = eventLogger;
        }
        /// <summary>
        /// get Sitemap page index
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public IActionResult Index(int id)
        {
            if (TempData[notificationMessageKey] != null)
            {
                switch (TempData[notificationTypeKey])
                {
                    case notificationSuccess:
                        _toastNotification.AddSuccessToastMessage(TempData[notificationMessageKey].ToString());
                        break;
                    case notificationWarning:
                        _toastNotification.AddWarningToastMessage(TempData[notificationMessageKey].ToString());
                        break;
                    case notificationError:
                        _toastNotification.AddErrorToastMessage(TempData[notificationMessageKey].ToString());
                        break;
                }
            }

            SiteMap pageSectionVersion = _siteMapRepository.GetDetail(id);
            if (pageSectionVersion == null)
            {
                pageSectionVersion = new SiteMap();
                return View(pageSectionVersion);
            }
            return View(pageSectionVersion);
        }
        /// <summary>
        /// delete Sitemap
        /// </summary>
        /// <param name="id">Sitemap id</param>
        /// <returns></returns>
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (_siteMapRepository.Delete(id))
            {
                TempData[notificationMessageKey] = "Element has been deleted successfully. </br> It will take effect after admin approval.";
                TempData[notificationTypeKey] = notificationSuccess;
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Definitions > Page SiteMap Details > Delete", "Delete Id: "+id);
                return Json(new { });
            }

            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Definitions > Page SiteMap Details > Delete", "Delete Id: " + id);
            return Json(new { });
        }
        /// <summary>
        /// get Sitemap edit page
        /// </summary>
        /// <param name="id">Sitemap id</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            SiteMap pageSectionCardVersion = _siteMapRepository.GetDetail(id);
            return View(pageSectionCardVersion);
        }
        /// <summary>
        /// edit Sitemap
        /// </summary>
        /// <param name="siteMap"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditAsync(SiteMap siteMap)
        {
           
            if (ModelState.IsValid)
            {

                var user = await _userManager.GetUserAsync(HttpContext.User);

                siteMap.ApprovedBy = user;// user.Id.ToString();
                siteMap.ApprovedById = user.Id.ToString();
                siteMap.ApprovalDate = DateTime.Now;
                siteMap.CreationDate = DateTime.Now;
                SiteMap newSectionCardVersion = _siteMapRepository.Update(siteMap);
                if (newSectionCardVersion != null)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Definitions > Page SiteMap Details > Edit", siteMap.EnContent);

                    return RedirectToAction("Index", new { id = newSectionCardVersion.Id });
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Definitions > Page SiteMap Details > Edit", siteMap.EnContent);
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                }
            }
            return View(_siteMapRepository);
        }


    }
}