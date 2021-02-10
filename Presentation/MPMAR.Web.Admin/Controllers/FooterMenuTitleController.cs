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
    public class FooterMenuTitleController : Controller
    {
        private readonly IFooterMenuTitleRepository _footerMenuTitleRepository;
        private readonly IFooterMenuTitleVersionsRepository _footerMenuTitleVersionsRepository;
        private readonly IFileService _fileService;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<FooterMenuTitleController> _eventLogger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        public FooterMenuTitleController(IFooterMenuTitleRepository footerMenuTitleRepository, IFooterMenuTitleVersionsRepository footerMenuTitleVersionsRepository, IApprovalNotificationsRepository approvalNotificationsRepository, IToastNotification toastNotification, IEventLogger<FooterMenuTitleController> eventLogger, UserManager<ApplicationUser> userManager, IFileService fileService)
        {
            _footerMenuTitleRepository = footerMenuTitleRepository;
            _footerMenuTitleVersionsRepository = footerMenuTitleVersionsRepository;
            _fileService = fileService;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }

        /// <summary>
        /// Index for griding footer menu title objects
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuTitles, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery]int approvalId)
        {
            ViewBag.approvalId = approvalId;
            return View();
        }

        /// <summary>
        /// Get footer menu title objects for index
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuTitles, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetAll()
        {
            var data = _footerMenuTitleVersionsRepository.GetFoorterMenuTitleVersions();

            return Json(new { data = data });
        }

        /// <summary>
        /// Get method for update footer menu title object
        /// </summary>
        /// <param name="id">footer menu title version id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuTitles, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery]int approvalId)
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.FooterMenuTitle);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            var footerMenuTitle = _footerMenuTitleVersionsRepository.GetByFooterMenuTitleId(id);
            if (footerMenuTitle == null || footerMenuTitle.VersionStatusEnum == VersionStatusEnum.Approved || footerMenuTitle.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                footerMenuTitle = _footerMenuTitleRepository.GetByFooterMenuTitleId(id);
            }
            var mapped = footerMenuTitle.MapToLeftMenuItemVersionsViewModel();
            ViewBag.approvalId = approvalId;
            return View(mapped);
        }

        /// <summary>
        /// Post method for edit footer menu title object
        /// </summary>
        /// <param name="ViewModel">footer menu title new data</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuTitles, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public async Task<IActionResult> Edit(FooterMenuTitleViewModel ViewModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var fmtv = _footerMenuTitleVersionsRepository.GetByFooterMenuTitleId(ViewModel.Id);
                var footerVersion = ViewModel.MapToFooterMenuItemVersionModel();

                if (fmtv == null || ViewModel.VersionStatusEnum == VersionStatusEnum.Approved || ViewModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    footerVersion.CreatedById = user.Id;
                    footerVersion.CreationDate = DateTime.Now;
                    footerVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                    footerVersion.ChangeActionEnum = ChangeActionEnum.Update;
                    footerVersion.Id = 0;
                    footerVersion.FooterMenuTitleId = ViewModel.Id;

                    _footerMenuTitleVersionsRepository.Add(footerVersion);
                    return RedirectToAction(nameof(Index));
                }

                footerVersion.CreationDate = DateTime.Now;
                footerVersion.Id = fmtv.Id;
                footerVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                footerVersion.ChangeActionEnum = ChangeActionEnum.Update;
                footerVersion.ApprovalDate = fmtv.ApprovalDate;
                footerVersion.ApprovedById = fmtv.ApprovedById;
                footerVersion.CreatedById = fmtv.CreatedById;
                footerVersion.CreationDate = fmtv.CreationDate;
                footerVersion.FooterMenuTitleId = fmtv.FooterMenuTitleId;
                var update = _footerMenuTitleVersionsRepository.Update(footerVersion);
                if (update)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    return View(ViewModel);
                }
            }
            return View(ViewModel);
        }

        /// <summary>
        /// Sent notification for approval user with last changes
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuTitles, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> SubmitChanges()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var fmtv = _footerMenuTitleVersionsRepository.GetAllDrafts();
            foreach (var record in fmtv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                _footerMenuTitleVersionsRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.FooterMenuTitle);
            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(FooterMenuTitleController)[0..^10]}",
                    PageName = PagesNamesConst.FooterMenuTitle,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);
            }
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Footer Menu Title > Submit", "Submitted ");
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Approve method for approve last changes
        /// </summary>
        /// <param name="approvalId">notification approved id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuTitles, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var fmtv = _footerMenuTitleVersionsRepository.GetAllSubmitted();

            foreach (var record in fmtv)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;
                _footerMenuTitleVersionsRepository.Update(record);

                var footerObj = new FooterMenuTitle()
                {
                    Id = record.FooterMenuTitleId ?? 0,
                    ApprovalDate = record.ApprovalDate,
                    ApprovedById = record.ApprovedById,
                    CreatedById = record.CreatedById,
                    CreationDate = record.CreationDate,
                    IsActive = record.IsActive,
                    IsDeleted = record.IsDeleted,
                    Order = record.Order,
                    ArTitle = record.ArTitle,
                    EnTitle = record.EnTitle,
                };
                _footerMenuTitleRepository.Update(footerObj);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.FooterMenuTitle);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Footer Menu Title > Approve", "Approved ");
            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Approve method for ignore last changes
        /// </summary>
        /// <param name="approvalId">notification approved id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.FooterMenuTitles, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var fmtv = _footerMenuTitleVersionsRepository.GetAllSubmitted();

            foreach (var record in fmtv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _footerMenuTitleVersionsRepository.Update(record);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.FooterMenuTitle);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Footer Menu Title > Reject", "Rejected ");

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);
            return RedirectToAction("Index", "ApprovalNotifications");
        }
    }
}