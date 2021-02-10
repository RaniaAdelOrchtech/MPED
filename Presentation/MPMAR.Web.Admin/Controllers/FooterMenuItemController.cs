using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Business.Interfaces;
using MPMAR.Common;
using MPMAR.Common.Helpers;
using MPMAR.Data;
using MPMAR.Data.Consts;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;
using MPMAR.Business;
using MPMAR.Common.Interfaces;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class FooterMenuItemController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly IFooterMenuItemRepository _footerMenuItemRepository;
        private readonly IFooterMenuItemVersionRepository _footerMenuItemVersionRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<FooterMenuItemController> _eventLogger;
        private readonly IFooterMenuTitleRepository _footerMenuTitleRepository;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;

        public FooterMenuItemController(IFooterMenuItemRepository footerMenuItemRepository, IFooterMenuItemVersionRepository footerMenuItemVersionRepository, 
            UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<FooterMenuItemController> eventLogger, 
            IFooterMenuTitleRepository footerMenuTitleRepository, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _footerMenuItemRepository = footerMenuItemRepository;
            _footerMenuItemVersionRepository = footerMenuItemVersionRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _footerMenuTitleRepository = footerMenuTitleRepository;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }
        
        /// <summary>
        /// Index for griding footer menu items objects
        /// </summary>
        /// <param name="pageRouteId">page route id</param>
        /// <param name="approvalId">notification approved id</param>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuItems, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery]int pageRouteId, [FromQuery] int approvalId)
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.FooterItems);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
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
            ViewBag.pageRouteId = pageRouteId;
            ViewBag.approvalId = approvalId;
            return View();

        }

        /// <summary>
        /// Get method for create footer menu item
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuItems, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create()
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.FooterItems);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            ViewBag.FooterMenuTitleId = new SelectList(_footerMenuTitleRepository.GetAll(), "Id", "EnTitle");
            return View();
        }

        /// <summary>
        /// Post method for create footer menu item
        /// </summary>
        /// <param name="footerMenuItem">Footer menu item model</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuItems, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public async Task<IActionResult> Create(FooterMenuItemViewModel footerMenuItem)
        {

            if (ModelState.IsValid)
            {
                await EditMethod(footerMenuItem);
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Footer Menu Items > Add", "Add " + footerMenuItem.Link);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.FooterMenuTitleId = new SelectList(_footerMenuTitleRepository.GetAll(), "Id", "EnTitle");
            return View(footerMenuItem);
        }

        /// <summary>
        /// Get method for update footer menu item object
        /// </summary>
        /// <param name="id">footer menu item version id</param>
        /// <param name="footerMenuItemId">footer menu item id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuItems, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery]int footerMenuItemId)
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.FooterItems);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            return DetailsMethod(id, footerMenuItemId);

        }

        /// <summary>
        /// Post method for edit footer menu item object
        /// </summary>
        /// <param name="viewModel">footer menu item new data</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuItems, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public async Task<IActionResult> Edit(FooterMenuItemViewModel viewModel)
        {
            return await EditMethod(viewModel);
        }

        /// <summary>
        /// Core method for update operation
        /// </summary>
        /// <param name="viewModel">footer menu item new data</param>
        /// <returns></returns>
        private async Task<IActionResult> EditMethod(FooterMenuItemViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var footerItemVersionByItemId = _footerMenuItemVersionRepository.GetByItemId(viewModel.FooterMenuItemId ?? 0);
                var footerItemVersionById = _footerMenuItemVersionRepository.Get(viewModel.Id);
                footerItemVersionByItemId = footerItemVersionById == null ? footerItemVersionByItemId : footerItemVersionById;
                var footerItemVersionModel = viewModel.MapToFooterItemVersionModel();

                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (footerItemVersionByItemId == null || footerItemVersionModel.VersionStatusEnum == VersionStatusEnum.Approved || footerItemVersionModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    footerItemVersionModel.CreatedById = user.Id;
                    footerItemVersionModel.CreationDate = DateTime.Now;
                    footerItemVersionModel.VersionStatusEnum = VersionStatusEnum.Draft;
                    footerItemVersionModel.ChangeActionEnum = ChangeActionEnum.Update;
                    footerItemVersionModel.Id = 0;
                    footerItemVersionModel.FooterMenuItemId = viewModel.FooterMenuItemId > 0 ? viewModel.FooterMenuItemId : (int?)null;
                    _footerMenuItemVersionRepository.Add(footerItemVersionModel);
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    return RedirectToAction(nameof(Index));
                }

                footerItemVersionModel.Id = footerItemVersionByItemId != null ? footerItemVersionByItemId.Id : viewModel.Id;

                _footerMenuItemVersionRepository.Update(footerItemVersionModel);
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Footer Menu Items > Update", "Update " + viewModel.Link);

                return RedirectToAction(nameof(Index));
            }
            ViewBag.FooterMenuTitleId = new SelectList(_footerMenuTitleRepository.GetAll(), "Id", "EnTitle", viewModel.FooterMenuTitleId);
            return View(viewModel);
        }

        /// <summary>
        /// Details for footer menu item object
        /// </summary>
        /// <param name="id">footer menu item version id</param>
        /// <param name="footerMenuItemId">footer menu item id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuItems, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Details(int id, [FromQuery]int footerMenuItemId, [FromQuery] int approvalId)
        {
            ViewBag.approvalId = approvalId;
            return DetailsMethod(id, footerMenuItemId);
        }

        /// <summary>
        /// core details operation for footer menu item object
        /// </summary>
        /// <param name="id">footer menu item version id</param>
        /// <param name="footerMenuItemId">footer menu item id</param>
        /// <returns></returns>
        private IActionResult DetailsMethod(int id, int footerMenuItemId)
        {
            FooterMenuItemViewModel viewModel;
            var footerItemVersion = _footerMenuItemVersionRepository.GetByItemId(footerMenuItemId);
            if (footerItemVersion == null || footerItemVersion.VersionStatusEnum == VersionStatusEnum.Approved || footerItemVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                var footerItem = _footerMenuItemRepository.Get(footerMenuItemId);
                if (footerItem != null)
                    viewModel = footerItem.MapToFooterItemViewModel();
                else
                {
                    footerItemVersion = _footerMenuItemVersionRepository.Get(id);
                    viewModel = footerItemVersion.MapToFooterItemViewModel();
                }
            }
            else
            {
                viewModel = footerItemVersion.MapToFooterItemViewModel();
            }
            //remove id value from route
            ModelState.Clear();
            ViewBag.FooterMenuTitleId = new SelectList(_footerMenuTitleRepository.GetAll(), "Id", "EnTitle", viewModel.FooterMenuTitleId);
            return View(viewModel);
        }

        /// <summary>
        /// Delete footer menu item by id
        /// </summary>
        /// <param name="id">footer menu version id</param>
        /// <param name="footerMenuItemId">footer menu item id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuItems, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public async Task<IActionResult> Delete(int id, int footerMenuItemId)
        {
            try
            {
                var socialVersion = _footerMenuItemVersionRepository.GetByItemId(footerMenuItemId);
                if (socialVersion != null)
                {
                    socialVersion.IsDeleted = true;
                    await EditMethod(socialVersion.MapToFooterItemViewModel());
                }
                else
                {
                    var social = _footerMenuItemRepository.GetByIdWithNoTracking(footerMenuItemId);
                    if (social != null)
                    {
                        social.IsDeleted = true;
                        await EditMethod(social.MapToFooterItemViewModel());
                    }
                    else
                        _footerMenuItemVersionRepository.Delete(id);

                }
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Footer Menu Items > Delete", "Delete ");

                TempData[notificationMessageKey] = ToasrMessages.DeleteSuccess;// </br> It will take effect after admin approval.";
                TempData[notificationTypeKey] = notificationSuccess;
                return Json(new { });
            }
            catch
            {
                TempData[notificationMessageKey] = "Error has been occurred.";
                TempData[notificationTypeKey] = notificationError;
                return Json(new { });
            }



        }

        /// <summary>
        /// Get footer menu item objects for index
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuItems, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetFooterMenuItem()
        {
            var pageMinistry = _footerMenuItemVersionRepository.GetFooterMenuItemId();
            return Json(new { data = pageMinistry });
        }

        /// <summary>
        /// Sent notification for approval user with last changes
        /// </summary>
        /// <param name="pageRouteId">page route id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuItems, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> SubmitChanges([FromQuery] int pageRouteId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var av = _footerMenuItemVersionRepository.GetAllDrafts();

            foreach (var record in av)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                _footerMenuItemVersionRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.FooterItems);

            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(FooterMenuItemController)[0..^10]}",
                    PageName = PagesNamesConst.FooterItems,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);

            }
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Footer Menu Items > Submitted", "Submitted ");
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Approve method for approve last changes
        /// </summary>
        /// <param name="approvalId">notification approved id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuItems, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var sv = _footerMenuItemVersionRepository.GetAllSubmitted();

            foreach (var record in sv)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;


                var s = new FooterMenuItem()
                {
                    Order = record.Order,
                    ArColumnPostion = record.ArColumnPostion,
                    ArTitle = record.ArTitle,
                    FooterMenuTitleId = record.FooterMenuTitleId,
                    IsActive = record.IsActive,
                    IsDeleted = record.IsDeleted,
                    Link = record.Link,
                    EnColumnPostion = record.EnColumnPostion,
                    EnTitle = record.EnTitle,
                };
                if (record.FooterMenuItemId != null)
                {
                    s.Id = record.FooterMenuItemId ?? 0;
                    _footerMenuItemRepository.Update(s);
                }
                else
                {
                    s.Id = 0;
                    _footerMenuItemRepository.Add(s);
                    record.FooterMenuItemId = s.Id;
                }

                _footerMenuItemVersionRepository.Update(record);
            }
            approval.VersionStatusEnum = VersionStatusEnum.Approved;
            approval.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approval);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Footer Menu Items > Approve", "Approved ");
            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Approve method for ignore last changes
        /// </summary>
        /// <param name="approvalId">notification approved id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuItems, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var sv = _footerMenuItemVersionRepository.GetAllSubmitted();

            foreach (var record in sv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _footerMenuItemVersionRepository.Update(record);
            }

            approval.VersionStatusEnum = VersionStatusEnum.Ignored;
            approval.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approval);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Footer Menu Items > Reject", "Rejected ");
            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);
            return RedirectToAction("Index", "ApprovalNotifications");
        }

    }
}