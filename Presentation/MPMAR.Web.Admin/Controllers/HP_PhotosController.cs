using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Services;
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
    public class HP_PhotosController : Controller
    {
        private readonly IHP_PhotosReopsitory _hP_PhotosReopsitory;
        private readonly IHP_PhotosVersionRepository _hP_PhotosVersionRepository;
        private readonly IFileService _fileService;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<HP_PhotosController> _eventLogger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        public HP_PhotosController(IHP_PhotosReopsitory hP_PhotosReopsitory, IHP_PhotosVersionRepository hP_PhotosVersionRepository, IApprovalNotificationsRepository approvalNotificationsRepository, IToastNotification toastNotification, IEventLogger<HP_PhotosController> eventLogger, UserManager<ApplicationUser> userManager, IFileService fileService)
        {
            _hP_PhotosReopsitory = hP_PhotosReopsitory;
            _hP_PhotosVersionRepository = hP_PhotosVersionRepository;
            _fileService = fileService;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }

        /// <summary>
        /// Index for griding all approved photo objects
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPhotos, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery]int approvalId)
        {
            ViewBag.approvalId = approvalId;
            return View();
        }

        /// <summary>
        /// Get method for update an existing photo object
        /// </summary>
        /// <param name="id">photo id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPhotos, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery]int approvalId)
        {
            var photoVersion = _hP_PhotosVersionRepository.GetByPhotoId(id);
            if (photoVersion == null || photoVersion.VersionStatusEnum == VersionStatusEnum.Approved || photoVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                photoVersion = _hP_PhotosReopsitory.GetByPhotoId(id);
            }
            var mapped = photoVersion.MapToPhotoVersionViewModel();
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPPhotos);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            ViewBag.approvalId = approvalId;
            return View(mapped);
        }

        /// <summary>
        /// Post method for update an existing photo object
        /// </summary>
        /// <param name="ViewModel">photo model new data</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPhotos, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Edit(HP_PhotoViewModel ViewModel)
        {
            ViewModel.ArDescription.ValidateHtml("ArDescription", ModelState);
            ViewModel.EnDescription.ValidateHtml("EnDescription", ModelState);

            ModelState.Remove(nameof(ViewModel.ImageFile));

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var phv = _hP_PhotosVersionRepository.GetByPhotoId(ViewModel.Id);
                var photoVersion = ViewModel.MapToPhotoVersionModel();

                if (phv == null || ViewModel.VersionStatusEnum == VersionStatusEnum.Approved || ViewModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    photoVersion.CreatedById = user.Id;
                    photoVersion.CreationDate = DateTime.Now;
                    photoVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                    photoVersion.ChangeActionEnum = ChangeActionEnum.Update;
                    photoVersion.Id = 0;
                    photoVersion.HomePagePhotoId = ViewModel.Id;
                    if (ViewModel.ImageFile != null)
                        photoVersion.ImageUrl = _fileService.UploadImageUrlNew(ViewModel.ImageFile);
                    _hP_PhotosVersionRepository.Add(photoVersion);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Photos > Update", ViewModel.EnTitle);
                    return RedirectToAction(nameof(Index));
                }


                if (ViewModel.ImageFile != null)
                    photoVersion.ImageUrl = _fileService.UploadImageUrlNew(ViewModel.ImageFile);

                photoVersion.CreationDate = DateTime.Now;
                photoVersion.Id = phv.Id;
                photoVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                photoVersion.ChangeActionEnum = ChangeActionEnum.Update;
                photoVersion.ApprovalDate = phv.ApprovalDate;
                photoVersion.ApprovedById = phv.ApprovedById;
                photoVersion.CreatedById = phv.CreatedById;
                photoVersion.CreationDate = phv.CreationDate;
                photoVersion.ModificationDate = phv.ModificationDate;
                photoVersion.ModifiedById = phv.ModifiedById;
                photoVersion.HomePagePhotoId = phv.HomePagePhotoId;
                var update = _hP_PhotosVersionRepository.Update(photoVersion);
                if (update)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Photos > Update", ViewModel.EnTitle);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Home Page > Photos > Warning", ViewModel.EnTitle);
                    return View(ViewModel);
                }
            }
            return View(ViewModel);
        }

        /// <summary>
        /// Submit changes method for sed approval user notification with last changes.
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPhotos, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> SubmitChanges()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var phv = _hP_PhotosVersionRepository.GetAllDrafts();
            foreach (var record in phv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                record.ModifiedById = user.Id;
                record.ModificationDate = DateTime.Now;
                _hP_PhotosVersionRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPPhotos);
            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(HP_PhotosController)[0..^10]}",
                    PageName = PagesNamesConst.HPPhotos,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);
            }

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Home Page > Photos > Submitted", " Submitted");
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Approve method to approve last changes to appears on website
        /// </summary>
        /// <param name="id">logo link id</param>
        /// <param name="approvalId">notification approve id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPhotos, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var phvs = _hP_PhotosVersionRepository.GetAllSubmitted();

            foreach (var record in phvs)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;
                _hP_PhotosVersionRepository.Update(record);

                var photoObj = new HomePagePhoto()
                {
                    Id = record.HomePagePhotoId ?? 0,
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
                    ImageUrl = record.ImageUrl,
                    Url = record.Url
                };
                _hP_PhotosReopsitory.Update(photoObj);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPPhotos);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Home Page > Photos > Approve", " Id :" + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Ignore method to ignore last changes
        /// </summary>
        /// <param name="id">logo link id</param>
        /// <param name="approvalId">notification approve id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPhotos, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var phvs = _hP_PhotosVersionRepository.GetAllSubmitted();

            foreach (var record in phvs)
            {
                record.ModificationDate = DateTime.Now;
                record.ModifiedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _hP_PhotosVersionRepository.Update(record);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPPhotos);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Home Page > Photos > Reject", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Get all photos objects for index
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPhotos, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetAllPhotos()
        {
            var imagesData = _hP_PhotosVersionRepository.GetPhotosVersions();
            return Json(new { data = imagesData });
        }
    }
}