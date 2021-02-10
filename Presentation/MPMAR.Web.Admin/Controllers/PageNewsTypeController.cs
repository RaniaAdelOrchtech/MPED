using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Web.Admin.ViewModels;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using NToastNotify;
using MPMAR.Business;
using MPMAR.Common.Helpers;
using MPMAR.Web.Admin.Helpers;
using Microsoft.AspNetCore.Identity;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Data.Consts;
using MPMAR.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MPMAR.Common;
using MPMAR.Web.Admin.AuthRequirement;
using MPMAR.Data.Enums;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class PageNewsTypeController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";


       
        private readonly IToastNotification _toastNotification;
private readonly IEventLogger<PageNewsTypeController> _eventLogger;
        private readonly IPageNewsTypeRepository _PageNewsTypeRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public PageNewsTypeController(IPageNewsTypeRepository PageNewsTypeRepository, IToastNotification toastNotification, IEventLogger<PageNewsTypeController> eventLogger, UserManager<ApplicationUser> userManager)
        {
            _PageNewsTypeRepository = PageNewsTypeRepository;
            _toastNotification = toastNotification;
_eventLogger = eventLogger;
            _userManager = userManager;

        }
        /// <summary>
        /// get PageNewsType page index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NewsType, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index()
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
            return View();
        }

        /// <summary>
        /// get PageNewsType create page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NewsType, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create()
        {
            PageNewsTypeCreateViewModel viewModel = new PageNewsTypeCreateViewModel();
            return View(viewModel);
        }

        /// <summary>
        /// create PageNewsType
        /// </summary>
        /// <param name="NewsTypeViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NewsType, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public async Task<IActionResult> Create(PageNewsTypeCreateViewModel NewsTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                PageNewsType PageNewsType = NewsTypeViewModel.MapToPageNewsTypeViewModel();
                
                var user = await _userManager.GetUserAsync(HttpContext.User);

                PageNewsType.CreatedById = user.Id;
                PageNewsType.CreationDate = DateTime.Now;
                PageNewsType newPageNewsType = _PageNewsTypeRepository.Add(PageNewsType);
                if (newPageNewsType != null)
                {

                    _toastNotification.AddSuccessToastMessage(ToasrMessages.AddSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Definitions > News Type > Add", newPageNewsType.EnName);
                    return RedirectToAction("Index");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Definitions > News Type > Add", PageNewsType.EnName);
                    return View(NewsTypeViewModel);
                }
            }
            return View(NewsTypeViewModel);
        }
        /// <summary>
        /// get PageNewsType
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NewsType, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetPageNewsType(int id)
        {
            var NewsType = _PageNewsTypeRepository.GetPageNewsTypes();
            var NewsTypeViewModel = NewsType.MapToPageNewsTypeListViewModel();
            return Json(new { data = NewsTypeViewModel });
        }
        /// <summary>
        /// delete PageNewsType by id
        /// </summary>
        /// <param name="id">PageNewsType id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NewsType, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public IActionResult Delete(int id)
        {
            PageNewsType NewsType = _PageNewsTypeRepository.Delete(id);

            if (NewsType != null)
            {
                TempData[notificationMessageKey] = "Element has been deleted successfully. </br> It will take effect after admin approval.";
                TempData[notificationTypeKey] = notificationSuccess;

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Definitions > News Type > Delete", NewsType.EnName);

                return Json(new { });
            }

            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Definitions > News Type > Delete", "Error has been occurred.");
            return Json(new { });
        }
        /// <summary>
        /// get PageNewsType edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NewsType, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id)
        {
            PageNewsType PageNewsType = _PageNewsTypeRepository.Get(id);
            NewsTypeViewModel NewsTypeVm = PageNewsType.MapToPageNewsTypeViewModelInEdit();
            PageNewsTypeEditViewModel viewModel = new PageNewsTypeEditViewModel(NewsTypeVm);
            return View(viewModel);
        }
        /// <summary>
        /// edit PageNewsType
        /// </summary>
        /// <param name="pageNewsTypeEditViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NewsType, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(PageNewsTypeEditViewModel pageNewsTypeEditViewModel)
        {
            if (ModelState.IsValid)
            {
                PageNewsType PageNewsType = pageNewsTypeEditViewModel.MapToPageNewsTypeVersion();

                PageNewsType newPageNewsType = _PageNewsTypeRepository.Update(PageNewsType);
                if (newPageNewsType != null)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Definitions > News Type > Edit",  pageNewsTypeEditViewModel.NewsType.EnName);

                    return RedirectToAction("Index");
                }
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Definitions > News Type > Edit", pageNewsTypeEditViewModel.NewsType.EnName);
                _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                return View(pageNewsTypeEditViewModel);
            }
            return View(pageNewsTypeEditViewModel);
        }



    }
}