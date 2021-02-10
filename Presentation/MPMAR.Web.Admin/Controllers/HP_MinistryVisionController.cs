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
using MPMAR.Data.HomePageModels;
using MPMAR.Data.HomePageModels.ViewModels;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.Mappers;
using NToastNotify;
using MPMAR.Business;
using MPMAR.Common.Interfaces;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class HP_MinistryVisionController : Controller
    {
        private readonly IMinistryVisionRepository _ministryVissionRepository;
        private readonly IMinistryVisionVersionRepository _ministryVisionVersionRepository;
        private readonly IFileService _fileService;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<HP_MinistryVisionController> _eventLogger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;

        public HP_MinistryVisionController(IMinistryVisionRepository ministryVissionRepository, IMinistryVisionVersionRepository ministryVisionVersionRepository,
            IFileService fileService, IToastNotification toastNotification, IEventLogger<HP_MinistryVisionController> eventLogger, UserManager<ApplicationUser> userManager, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _ministryVissionRepository = ministryVissionRepository;
            _ministryVisionVersionRepository = ministryVisionVersionRepository;
            _fileService = fileService;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _userManager = userManager;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }
        /// <summary>
        /// get HP_MinistryVision page index
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPMinistryMessage, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index()
        {
            var ministryVision = _ministryVisionVersionRepository.GetMinistryVessionVersions();
            return View(ministryVision[0]);
        }
        /// <summary>
        /// get HP_MinistryVision edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPMinistryMessage, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id)
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.MinistryVisionHP);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            var mvv = _ministryVisionVersionRepository.GetByMinistryVessionId(id);
            if (mvv == null || mvv.VersionStatusEnum == VersionStatusEnum.Approved || mvv.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                mvv = _ministryVissionRepository.GetByMinistryVessionId(id);
            }
            var mapped = mvv.MapToMinistryVissionVersionViewModel();
            return View(mapped);
        }
        /// <summary>
        /// edit HP_MinistryVision
        /// </summary>
        /// <param name="ViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPMinistryMessage, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Edit(MinistrtVisionViewModel ViewModel)
        {
            ViewModel.ArDescription.ValidateHtml("ArDescription", ModelState);
            ViewModel.EnDescription.ValidateHtml("EnDescription", ModelState);

            ModelState.Remove(nameof(ViewModel.BackGroundImageFile));

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var mvv = _ministryVisionVersionRepository.GetByMinistryVessionId(ViewModel.Id);
                var minVersion = ViewModel.MapToMinistryVissionVersionModel();
                //if ministryVisionVersion wasn't exist or  MinistrtVisionViewModel approved or ignored then create new version 
                if (mvv == null || ViewModel.VersionStatusEnum == VersionStatusEnum.Approved || ViewModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    minVersion.CreatedById = user.Id;
                    minVersion.CreationDate = DateTime.Now;
                    minVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                    minVersion.ChangeActionEnum = ChangeActionEnum.Update;
                    minVersion.Id = 0;
                    minVersion.MinistryVissionId = ViewModel.Id;
                    if (ViewModel.BackGroundImageFile != null)
                        minVersion.BackGroundImage = _fileService.UploadImageUrlNew(ViewModel.BackGroundImageFile);
                    _ministryVisionVersionRepository.Add(minVersion);
                    return RedirectToAction(nameof(Index));
                }
                if (ViewModel.BackGroundImageFile != null)
                    minVersion.BackGroundImage = _fileService.UploadImageUrlNew(ViewModel.BackGroundImageFile);
                minVersion.CreationDate = DateTime.Now;
                minVersion.Id = mvv.Id;
                minVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                minVersion.ChangeActionEnum = ChangeActionEnum.Update;
                minVersion.ApprovalDate = mvv.ApprovalDate;
                minVersion.ApprovedById = mvv.ApprovedById;
                minVersion.CreatedById = mvv.CreatedById;
                minVersion.CreationDate = mvv.CreationDate;
                minVersion.ModificationDate = mvv.ModificationDate;
                minVersion.ModifiedById = mvv.ModifiedById;
                minVersion.MinistryVissionId = mvv.MinistryVissionId;
                var update = _ministryVisionVersionRepository.Update(minVersion);
                if (update)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Ministry Vision > Update", ViewModel.EnTitle);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Home Page > Ministry Vision > Warning", ViewModel.EnTitle);
                    return View(ViewModel);
                }
            }
            return View(ViewModel);
        }
        /// <summary>
        /// submit changes for the approval user 
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPMinistryMessage, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> SubmitChanges()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var mvv = _ministryVisionVersionRepository.GetAllDrafts();
            //mark all drafts ministryVisionVersio as submitted
            foreach (var record in mvv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                record.ModifiedById = user.Id;
                record.ModificationDate = DateTime.Now;
                _ministryVisionVersionRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.MinistryVisionHP);
            //create new notification if there wasn't
            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(HP_MinistryVisionController)[0..^10]}",
                    PageName = PagesNamesConst.MinistryVisionHP,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);
            }
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Home Page > Ministry Vision > Submitted", " Submitted");
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPMinistryMessage, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var mvv = _ministryVisionVersionRepository.GetAllSubmitted();
            //mark all submited ministryVisionVersion as approved
            foreach (var record in mvv)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;
                _ministryVisionVersionRepository.Update(record);

                var minObj = new MinistryVission()
                {
                    Id = record.MinistryVissionId ?? 0,
                    ApprovalDate = record.ApprovalDate,
                    ApprovedById = record.ApprovedById,
                    CreatedById = record.CreatedById,
                    CreationDate = record.CreationDate,
                    IsActive = record.IsActive,
                    IsDeleted = record.IsDeleted,
                    ModificationDate = record.ModificationDate,
                    ModifiedById = record.ModifiedById,
                    ArDescription = record.ArDescription,
                    ArTitle = record.ArTitle,
                    EnDescription = record.EnDescription,
                    EnTitle = record.EnTitle,
                    Link = record.Link,
                    BackGroundImage = record.BackGroundImage
                };
                _ministryVissionRepository.Update(minObj);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.MinistryVisionHP);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Home Page > Ministry Vision > Approve", " Id :" + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }
        /// <summary>
        /// Ignore changes and change status to ignored
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPMinistryMessage, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var mvv = _ministryVisionVersionRepository.GetAllSubmitted();
            //mark all submited ministryVisionVersion as ignored
            foreach (var record in mvv)
            {
                record.ModificationDate = DateTime.Now;
                record.ModifiedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _ministryVisionVersionRepository.Update(record);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.MinistryVisionHP);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Home Page > Ministry Vision > Reject", " Id :" + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }
    }
}