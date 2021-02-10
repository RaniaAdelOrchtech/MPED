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
    public class HP_VideoController : Controller
    {
        private readonly IHP_VideoReopsitory _hP_VideoReopsitory;
        private readonly IHP_VideoVersionRepository _hP_VideoVersionRepository;
        private readonly IFileService _fileService;
        private readonly IToastNotification _toastNotification;
private readonly IEventLogger<HP_VideoController> _eventLogger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        public HP_VideoController(IHP_VideoReopsitory hP_VideoReopsitory, IHP_VideoVersionRepository hP_VideoVersionRepository, IApprovalNotificationsRepository approvalNotificationsRepository, IToastNotification toastNotification, IEventLogger<HP_VideoController> eventLogger, UserManager<ApplicationUser> userManager, IFileService fileService)
        {
            _hP_VideoReopsitory = hP_VideoReopsitory;
            _hP_VideoVersionRepository = hP_VideoVersionRepository;
            _fileService = fileService;
            _userManager = userManager;
            _toastNotification = toastNotification;
_eventLogger = eventLogger;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }
        /// <summary>
        /// get HP_Video page index
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPVideo, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// get HP_Video edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPVideo, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id)
        {
            var videoVersion = _hP_VideoVersionRepository.GetByVideoId(id);
            if (videoVersion == null || videoVersion.VersionStatusEnum == VersionStatusEnum.Approved || videoVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                videoVersion = _hP_VideoReopsitory.GetByVideoId(id);
            }
            var mapped = videoVersion.MapToVideoVersionViewModel();

            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPVideo);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            return View(mapped);
        }
        /// <summary>
        /// edit HP_Video
        /// </summary>
        /// <param name="ViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPVideo, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public async Task<IActionResult> Edit(HP_VideoViewModel ViewModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var vdv = _hP_VideoVersionRepository.GetByVideoId(ViewModel.Id);
                var videoVersions = ViewModel.MapToVideoVersionModel();
                //if hP_VideoVersion wasn't exist or  HP_VideoViewModel approved or ignored then create new version 
                if (vdv == null || ViewModel.VersionStatusEnum == VersionStatusEnum.Approved || ViewModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    videoVersions.CreatedById = user.Id;
                    videoVersions.CreationDate = DateTime.Now;
                    videoVersions.VersionStatusEnum = VersionStatusEnum.Draft;
                    videoVersions.ChangeActionEnum = ChangeActionEnum.Update;
                    videoVersions.Id = 0;
                    videoVersions.HomePageVideoId = ViewModel.Id;
                    _hP_VideoVersionRepository.Add(videoVersions);
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Video > Edit", ViewModel.VideoUrl);
                    return RedirectToAction(nameof(Index));
                }

                videoVersions.CreationDate = DateTime.Now;
                videoVersions.Id = vdv.Id;
                videoVersions.VersionStatusEnum = VersionStatusEnum.Draft;
                videoVersions.ChangeActionEnum = ChangeActionEnum.Update;
                videoVersions.ApprovalDate = vdv.ApprovalDate;
                videoVersions.ApprovedById = vdv.ApprovedById;
                videoVersions.CreatedById = vdv.CreatedById;
                videoVersions.CreationDate = vdv.CreationDate;
                videoVersions.ModificationDate = vdv.ModificationDate;
                videoVersions.ModifiedById = vdv.ModifiedById;
                videoVersions.HomePageVideoId = vdv.HomePageVideoId;
                var update = _hP_VideoVersionRepository.Update(videoVersions);
                if (update)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Video > Edit", ViewModel.VideoUrl);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Home Page > Video > Edit", ViewModel.VideoUrl);
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
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPVideo, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> SubmitChanges()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var vdv = _hP_VideoVersionRepository.GetAllDrafts();
            foreach (var record in vdv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                record.ModifiedById = user.Id;
                record.ModificationDate = DateTime.Now;
                _hP_VideoVersionRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPVideo);

            //create new notification if there wasn't
            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(HP_VideoController)[0..^10]}",
                    PageName = PagesNamesConst.HPVideo,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);
            }


            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Home Page > Video > Submitted", " Submitted");

            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// approve changes,change status to approved  and update the non version 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPVideo, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var vdvs = _hP_VideoVersionRepository.GetAllSubmitted();

            foreach (var record in vdvs)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;
                _hP_VideoVersionRepository.Update(record);

                var videoObj = new HomePageVideo()
                {
                    Id = record.HomePageVideoId ?? 0,
                    ApprovalDate = record.ApprovalDate,
                    ApprovedById = record.ApprovedById,
                    CreatedById = record.CreatedById,
                    CreationDate = record.CreationDate,
                    IsActive = record.IsActive,
                    IsDeleted = record.IsDeleted,
                    ModificationDate = record.ModificationDate,
                    ModifiedById = record.ModifiedById,
                    VideoUrl = record.VideoUrl
                };
                _hP_VideoReopsitory.Update(videoObj);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPVideo);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Home Page > Video > Approve", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }
        /// <summary>
        /// Ignore changes and change status to ignored
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPVideo, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var vdvs = _hP_VideoVersionRepository.GetAllSubmitted();

            foreach (var record in vdvs)
            {
                record.ModificationDate = DateTime.Now;
                record.ModifiedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _hP_VideoVersionRepository.Update(record);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPVideo);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Home Page > Video > Reject", " Id :" + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }
        /// <summary>
        /// get all HP_Video
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPVideo, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetAllVideos()
        {
            var videoData = _hP_VideoVersionRepository.GetVideosVersions();
            return Json(new { data = videoData });
        }
    }
}