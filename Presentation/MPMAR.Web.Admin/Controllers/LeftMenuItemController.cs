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
    public class LeftMenuItemController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly ILeftMenuItemRepository _leftMenuItemRepository;
        private readonly ILeftMenuItemsVersionsRepository _leftMenuItemsVersionsRepository;
        private readonly IFileService _fileService;
        private readonly IToastNotification _toastNotification;
private readonly IEventLogger<LeftMenuItemController> _eventLogger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        public LeftMenuItemController(ILeftMenuItemRepository leftMenuItemRepository, ILeftMenuItemsVersionsRepository leftMenuItemsVersionsRepository, IApprovalNotificationsRepository approvalNotificationsRepository, IToastNotification toastNotification, IEventLogger<LeftMenuItemController> eventLogger, UserManager<ApplicationUser> userManager, IFileService fileService)
        {
            _leftMenuItemRepository = leftMenuItemRepository;
            _leftMenuItemsVersionsRepository = leftMenuItemsVersionsRepository;
            _fileService = fileService;
            _userManager = userManager;
            _toastNotification = toastNotification;
_eventLogger = eventLogger;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }
        /// <summary>
        /// get LeftMenuItem page index
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.LeftMenuItems, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery]int approvalId)
        {
            ViewBag.approvalId = approvalId;
            return View();
        }
        /// <summary>
        /// get list of LeftMenuItem
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.LeftMenuItems, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetLeftMenuItem()
        {
            var pageMinistry = _leftMenuItemsVersionsRepository.GetLeftMenuItemVersions();
            return Json(new { data = pageMinistry });
        }
        /// <summary>
        /// get LeftMenuItem edit page
        /// </summary>
        /// <param name="id">leftMenuItems id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.LeftMenuItems, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery]int approvalId)
        {
            var leftMenuItem = _leftMenuItemsVersionsRepository.GetByLeftMenuItemId(id);
            if (leftMenuItem == null || leftMenuItem.VersionStatusEnum == VersionStatusEnum.Approved || leftMenuItem.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                leftMenuItem = _leftMenuItemRepository.GetByLeftMenuItemId(id);
            }
            var mapped = leftMenuItem.MapToLeftMenuItemVersionsViewModel();
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.LeftMenuItem);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            ViewBag.approvalId = approvalId;
            return View(mapped);
        }
        /// <summary>
        /// edit LeftMenuItem
        /// </summary>
        /// <param name="ViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.LeftMenuItems, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Edit(LeftMenuItemViewModel ViewModel)
        {
            ModelState.Remove(nameof(ViewModel.ImageFile));

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var lmi = _leftMenuItemsVersionsRepository.GetByLeftMenuItemId(ViewModel.Id);
                var leftMenuItemVersion = ViewModel.MapToLeftMenuItemVersionsModel();

                if (lmi == null || ViewModel.VersionStatusEnum == VersionStatusEnum.Approved || ViewModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    leftMenuItemVersion.CreatedById = user.Id;
                    leftMenuItemVersion.CreationDate = DateTime.Now;
                    leftMenuItemVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                    leftMenuItemVersion.ChangeActionEnum = ChangeActionEnum.Update;
                    leftMenuItemVersion.Id = 0;
                    leftMenuItemVersion.LeftMenuItemId = ViewModel.Id;
                    if (ViewModel.ImageFile != null)
                        leftMenuItemVersion.ImagePath = _fileService.UploadImageUrlNew(ViewModel.ImageFile);
                    _leftMenuItemsVersionsRepository.Add(leftMenuItemVersion);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Definitions > Left Menu Item > Edit", ViewModel.EnTitle);
                    return RedirectToAction(nameof(Index));
                }


                if (ViewModel.ImageFile != null)
                    leftMenuItemVersion.ImagePath = _fileService.UploadImageUrlNew(ViewModel.ImageFile);

                leftMenuItemVersion.CreationDate = DateTime.Now;
                leftMenuItemVersion.Id = lmi.Id;
                leftMenuItemVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                leftMenuItemVersion.ChangeActionEnum = ChangeActionEnum.Update;
                leftMenuItemVersion.ApprovalDate = lmi.ApprovalDate;
                leftMenuItemVersion.ApprovedById = lmi.ApprovedById;
                leftMenuItemVersion.CreatedById = lmi.CreatedById;
                leftMenuItemVersion.CreationDate = lmi.CreationDate;
                leftMenuItemVersion.LeftMenuItemId = lmi.LeftMenuItemId;
                var update = _leftMenuItemsVersionsRepository.Update(leftMenuItemVersion);
                if (update)
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Definitions > Left Menu Item > Edit", ViewModel.EnTitle);
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Definitions > Left Menu Item > Edit", ViewModel.EnTitle);
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    return View(ViewModel);
                }
            }
            return View(ViewModel);
        }
        /// <summary>
        /// submit changes for the approval user 
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.LeftMenuItems, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> SubmitChanges()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var lmi = _leftMenuItemsVersionsRepository.GetAllDrafts();
            foreach (var record in lmi)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                _leftMenuItemsVersionsRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.LeftMenuItem);
            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(LeftMenuItemController)[0..^10]}",
                    PageName = PagesNamesConst.LeftMenuItem,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);
            }
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Definitions > Left Menu Item > Submitted", " Submitted");

            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// approve changes,change status to approved  and update the non version 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.LeftMenuItems, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var lmiv = _leftMenuItemsVersionsRepository.GetAllSubmitted();

            foreach (var record in lmiv)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;
                _leftMenuItemsVersionsRepository.Update(record);

                var leftmenuObj = new LeftMenuItem()
                {
                    Id = record.LeftMenuItemId ?? 0,
                    ApprovalDate = record.ApprovalDate,
                    ApprovedById = record.ApprovedById,
                    CreatedById = record.CreatedById,
                    CreationDate = record.CreationDate,
                    IsActive = record.IsActive,
                    IsDeleted = record.IsDeleted,
                    ArTitle = record.ArTitle,
                    EnTitle = record.EnTitle,
                    ImagePath = record.ImagePath,
                    LeftMenuType = record.LeftMenuType,
                    Link = record.Link,
                    Order = record.Order
                };
                _leftMenuItemRepository.Update(leftmenuObj);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.LeftMenuItem);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Definitions > Left Menu Item > Approve", " Id :" + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }
        /// <summary>
        /// Ignore changes and change status to ignored
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.LeftMenuItems, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var lmiv = _leftMenuItemsVersionsRepository.GetAllSubmitted();

            foreach (var record in lmiv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _leftMenuItemsVersionsRepository.Update(record);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.LeftMenuItem);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Definitions > Left Menu Item > Reject", " Id :" + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }
    }
}