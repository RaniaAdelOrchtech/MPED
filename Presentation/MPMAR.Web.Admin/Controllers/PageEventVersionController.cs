using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Common;
using MPMAR.Common.Helpers;
using MPMAR.Data;
using MPMAR.Data.Consts;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;
using MPMAR.Business;
using static MPMAR.Data.Enums.Enums;
using MPMAR.Common.Interfaces;
using MPMAR.Business.ViewModels;
using MPMAR.Web.Admin.AuthRequirement;
using MPMAR.Data.Enums;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class PageEventVersionController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly IPageEventVersionsRepository _pageEventVersionRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<PageEventVersionController> _eventLogger;

        private readonly IFileService _fileService;

        private readonly IPageRouteRepository _pageRouteRepository;
        public PageEventVersionController(IPageEventVersionsRepository pageEventVersionRepository, UserManager<ApplicationUser> userManager,
            IToastNotification toastNotification, IEventLogger<PageEventVersionController> eventLogger, IFileService fileService
            , IPageRouteRepository pageRouteRepository)
        {
            _pageEventVersionRepository = pageEventVersionRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _fileService = fileService;
            _pageRouteRepository = pageRouteRepository;
        }
        /// <summary>
        /// get PageEventVersion index page
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.MinistryEvents)]
        public IActionResult Index(int pageRouteId)
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

            PageRoute pageRoute = _pageRouteRepository.Get(pageRouteId);
            if (pageRoute == null)
            {
                return NotFound();
            }

            return View(pageRouteId);
        }
        /// <summary>
        /// get PageEventVersion create page
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd }, StaticPagesIdsConst.MinistryEvents)]
        public IActionResult Create(int pageRouteId)
        {
            PageEventVersionViewModel viewModel = new PageEventVersionViewModel();
            viewModel.PageRouteId = pageRouteId;
            return View(viewModel);
        }
        /// <summary>
        /// create PageEventVersion
        /// </summary>
        /// <param name="pageEventViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd }, StaticPagesIdsConst.MinistryEvents)]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> CreateAsync(PageEventVersionViewModel pageEventViewModel)
        {
            pageEventViewModel.EventStartDate = Convert.ToDateTime(pageEventViewModel.EventDateRange.Split('-')[0]);
            pageEventViewModel.EventEndDate = Convert.ToDateTime(pageEventViewModel.EventDateRange.Split('-')[1]);

            if (ModelState.IsValid)
            {
                PageEventVersions pageEventVersion = pageEventViewModel.MapToPageEventVersion();

                var user = await _userManager.GetUserAsync(HttpContext.User);
                pageEventVersion.CreatedById = user.Id;
                pageEventVersion.CreationDate = DateTime.Now;
                pageEventVersion.ApprovedById = user.Id;
                pageEventVersion.ApprovedBy = user;
                pageEventVersion.ApprovalDate = DateTime.Now;
                if (pageEventViewModel.Photo != null)
                    pageEventVersion.EnUrl = _fileService.UploadImageUrlNew(pageEventViewModel.Photo);
                if (pageEventViewModel.DetailPhoto != null)
                    pageEventVersion.ArUrl = _fileService.UploadImageUrlNew(pageEventViewModel.DetailPhoto);

                PageEventVersions newEventVersion = _pageEventVersionRepository.Add(pageEventVersion, pageEventViewModel.PageRouteId);
                if (newEventVersion != null)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.AddSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Page Event", pageEventViewModel.EnTitle);

                    return RedirectToAction("Index", new { pageRouteId = pageEventViewModel.PageRouteId });
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                }
            }
            return View(pageEventViewModel);

        }

        /// <summary>
        /// get PageEventVersionedit page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.MinistryEvents)]
        public IActionResult Edit(int id, int pageRouteId)
        {
            var viewModel = _pageEventVersionRepository.GetDetail(id);
            viewModel.PageRouteId = pageRouteId;
            return View(viewModel);
        }
        /// <summary>
        /// edit PageEventVersion
        /// </summary>
        /// <param name="eventVersionViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.MinistryEvents)]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> EditAsync(PageEventVersionViewModel eventVersionViewModel)
        {
            eventVersionViewModel.EventStartDate = Convert.ToDateTime(eventVersionViewModel.EventDateRange.Split('-')[0]);
            eventVersionViewModel.EventEndDate = Convert.ToDateTime(eventVersionViewModel.EventDateRange.Split('-')[1]);

            if (ModelState.IsValid)
            {
                PageEventVersions eventVersion = eventVersionViewModel.MapToPageEventVersion();

                var user = await _userManager.GetUserAsync(HttpContext.User);

                eventVersion.ApprovedBy = user;
                eventVersion.ApprovedById = user.Id.ToString();
                eventVersion.ApprovalDate = DateTime.Now;
                eventVersion.CreationDate = DateTime.Now;
                if (eventVersionViewModel.Photo != null)
                    eventVersion.EnUrl = _fileService.UploadImageUrlNew(eventVersionViewModel.Photo);
                if (eventVersionViewModel.DetailPhoto != null)
                    eventVersion.ArUrl = _fileService.UploadImageUrlNew(eventVersionViewModel.DetailPhoto);


                PageEventVersions newEventVer = _pageEventVersionRepository.Update(eventVersion, eventVersionViewModel.PageRouteId);
                if (newEventVer != null)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Page Event", eventVersionViewModel.EnTitle);

                    return RedirectToAction("Index", new { pageRouteId = eventVersionViewModel.PageRouteId });
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                }
            }

            return View(eventVersionViewModel);
        }
        /// <summary>
        /// get details page of PageEventVersion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.MinistryEvents)]
        public IActionResult Details(int id)
        {
            var pageSectionCardVersion = _pageEventVersionRepository.GetDetail(id);

            return View(pageSectionCardVersion);
        }
        /// <summary>
        /// delete PageEventVersion
        /// </summary>
        /// <param name="id">pageEventVersion id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanDelete }, StaticPagesIdsConst.MinistryEvents)]
        public IActionResult Delete(int id)
        {
            var deleEvent = _pageEventVersionRepository.GetDetail(id);
            string impPopupPath = deleEvent.Url;

            if (_pageEventVersionRepository.Delete(id))
            {
                if (impPopupPath != null)
                    _fileService.RemoveImageUrl(impPopupPath);

                TempData[notificationMessageKey] = ToasrMessages.DeleteSuccess;// </br> It will take effect after admin approval.";
                TempData[notificationTypeKey] = notificationSuccess;

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Page Event", " Id :" + id);

                return Json(new { });
            }

            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            return Json(new { });
        }


        /// <summary>
        /// get pageEventVersion by page route id
        /// </summary>
        /// <param name="id">page route id</param>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.MinistryEvents)]
        public JsonResult GetPageEvent(int id)
        {
            var pageMinistry = _pageEventVersionRepository.GetPageEventByPageRouteId(id);
            return Json(new { data = pageMinistry });
        }
        /// <summary>
        /// submit changes for the approval user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete }, StaticPagesIdsConst.MinistryEvents)]
        public async Task<IActionResult> ApplyEditRequestAsync(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string url = $"{nameof(PageEventVersionController)[0..^10]}/{nameof(Details)}";
            var success = _pageEventVersionRepository.ApplySubmitRequest(id, user.Id, url);
            if (success)
            {
                TempData[notificationMessageKey] = ToasrMessages.SubmitSuccess;

                TempData[notificationTypeKey] = notificationSuccess;
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Events", "Submit id : " + id);

                return Json(new { });
            }

            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            return Json(new { });
        }
    }
}