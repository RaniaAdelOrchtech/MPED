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
    public class SocialMediaController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly ISocialMediaRepository _socialMediaRepository;
        private readonly ISocialMediaVersionRepository _socialMediaVersionRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<SocialMediaController> _eventLogger;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;

        public SocialMediaController(ISocialMediaRepository socialMediaRepository, ISocialMediaVersionRepository socialMediaVersionRepository, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<SocialMediaController> eventLogger, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _socialMediaRepository = socialMediaRepository;
            _socialMediaVersionRepository = socialMediaVersionRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }
        /// <summary>
        /// get SocialMedia page index
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.SocialMediaLinks, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery]int pageRouteId, [FromQuery] int approvalId)
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

            ViewBag.pageRouteId = pageRouteId;
            ViewBag.approvalId = approvalId;

            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.SocialMedia);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;

            return View();
        }
        /// <summary>
        /// get SocialMedia create page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.SocialMediaLinks, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create()
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.SocialMedia);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;

            return View();
        }
        /// <summary>
        /// create SocialMedia
        /// </summary>
        /// <param name="socialMedia"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.SocialMediaLinks, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public async Task<IActionResult> Create(SocialMediaViewModel socialMedia)
        {
            if (ModelState.IsValid)
            {
                await EditMethod(socialMedia);
                return RedirectToAction(nameof(Index));
            }
            return View(socialMedia);
        }

        /// <summary>
        /// get SocialMedia edit page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="socialMediaId">SocialMedia id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.SocialMediaLinks, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery]int socialMediaId)
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.SocialMedia);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            return DetailsMethod(id, socialMediaId);
        }
        /// <summary>
        /// get details of SocialMedia
        /// </summary>
        /// <param name="id">socialMediaVersion id</param>
        /// <param name="socialMediaId"></param>
        /// <returns></returns>
        private IActionResult DetailsMethod(int id, int socialMediaId, int approvalId=0)
        {
            SocialMediaViewModel viewModel;
            var socialVersion = _socialMediaVersionRepository.GetBySocialId(socialMediaId);
            if (socialVersion == null || socialVersion.VersionStatusEnum == VersionStatusEnum.Approved || socialVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                var slider = _socialMediaRepository.Get(socialMediaId);
                if (slider != null)
                    viewModel = slider.MapToSocialViewModel();
                else
                {
                    socialVersion = _socialMediaVersionRepository.GetById(id);
                    viewModel = socialVersion.MapToSocialViewModel();
                }
            }
            else
            {
                viewModel = socialVersion.MapToSocialViewModel();
            }
            //remove id value from route
            ModelState.Clear();
            viewModel.ApprovalId = approvalId;
            return View(viewModel);
        }
        /// <summary>
        /// edit SocialMedia
        /// </summary>
        /// <param name="socialMedia"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.SocialMediaLinks, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public async Task<IActionResult> Edit(SocialMediaViewModel socialMedia)
        {
            return await EditMethod(socialMedia);
        }
        /// <summary>
        /// edit SocialMedia
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private async Task<IActionResult> EditMethod(SocialMediaViewModel viewModel)
        {

            ModelState.Remove(nameof(viewModel.SocialMediaId));

            if (ModelState.IsValid)
            {
                var socialVersionBySliderId = _socialMediaVersionRepository.GetBySocialId(viewModel.SocialMediaId ?? 0);
                var socialVersionById = _socialMediaVersionRepository.GetById(viewModel.Id);
                socialVersionBySliderId = socialVersionById == null ? socialVersionBySliderId : socialVersionById;
                var socialVersionModel = viewModel.MapToSocialVersionModel();
                var user = await _userManager.GetUserAsync(HttpContext.User);
                //if socialMediaVersion wasn't exist or  SocialMediaViewModel approved or ignored then create new version 
                if (socialVersionBySliderId == null || socialVersionModel.VersionStatusEnum == VersionStatusEnum.Approved || socialVersionModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    socialVersionModel.CreatedById = user.Id;
                    socialVersionModel.CreationDate = DateTime.Now;
                    socialVersionModel.VersionStatusEnum = VersionStatusEnum.Draft;
                    socialVersionModel.ChangeActionEnum = ChangeActionEnum.Update;
                    socialVersionModel.Id = 0;
                    socialVersionModel.SocialMediaId = viewModel.SocialMediaId > 0 ? viewModel.SocialMediaId : (int?)null;
                    _socialMediaVersionRepository.Add(socialVersionModel);
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Social Media Links > Add", viewModel.SocialMediaName);
                    return RedirectToAction(nameof(Index));
                }

                socialVersionModel.Id = socialVersionBySliderId != null ? socialVersionBySliderId.Id : viewModel.Id;

                _socialMediaVersionRepository.Update(socialVersionModel);
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Social Media Links > Edit", viewModel.SocialMediaName);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
        /// <summary>
        /// get socialMediaVersion details page
        /// </summary>
        /// <param name="id">socialMediaVersion id</param>
        /// <param name="socialMediaId"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.SocialMediaLinks, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Details(int id, int socialMediaId, int approvalId)
        {
            return DetailsMethod(id, socialMediaId,approvalId);
        }
        [BEUsersPrivilegesRequirement(PrivilegesPageType.SocialMediaLinks, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public async Task<IActionResult> Delete(int id, int socialId)
        {

            try
            {
                var socialVersion = _socialMediaVersionRepository.GetBySocialId(socialId);
                if (socialVersion != null)
                {
                    socialVersion.IsDeleted = true;
                    await EditMethod(socialVersion.MapToSocialViewModel());
                }
                else
                {
                    var social = _socialMediaRepository.GetByIdWithNoTracking(socialId);
                    if (social != null)
                    {
                        social.IsDeleted = true;
                        await EditMethod(social.MapToSocialViewModel());
                    }
                    else
                        _socialMediaVersionRepository.Delete(id);

                }


                TempData[notificationMessageKey] = ToasrMessages.DeleteSuccess;// </br> It will take effect after admin approval.";
                TempData[notificationTypeKey] = notificationSuccess;
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Social Media Links > Delete", "id: " + id);
                return Json(new { });
            }
            catch
            {
                TempData[notificationMessageKey] = "Error has been occurred.";
                TempData[notificationTypeKey] = notificationError;
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Social Media Links > Delete", "id: " + id);
                return Json(new { });
            }



        }


        /// <summary>
        /// get SocialMedia
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSocialMedia()
        {
            var social = _socialMediaVersionRepository.GetAll();
            return Json(new { data = social });
        }
        /// <summary>
        /// submit changes for the approval user 
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.SocialMediaLinks, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> SubmitChanges([FromQuery] int pageRouteId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var sv = _socialMediaVersionRepository.GetAllDrafts();

            foreach (var record in sv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                _socialMediaVersionRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.SocialMedia);

            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(SocialMediaController)[0..^10]}",
                    PageName = PagesNamesConst.SocialMedia,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);

            }
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Social Media Links > Submitted", "page route id: " + pageRouteId);
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// approve changes,change status to approved  and update the non version 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.SocialMediaLinks, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var sv = _socialMediaVersionRepository.GetAllSubmitted();

            foreach (var record in sv)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;


                var s = new SocialMedia()
                {
                    //Id = record.PageMinistryId ?? 0,
                    Order = record.Order,
                    Link = record.Link,
                    SocialMediaName = record.SocialMediaName,
                    IsActive = record.IsActive,
                    CreatedById = user.Id,
                    CreationDate = DateTime.Now,
                    IsDeleted = record.IsDeleted,
                };
                if (record.SocialMediaId != null)
                {
                    s.Id = record.SocialMediaId ?? 0;
                    _socialMediaRepository.Update(s);
                }
                else
                {
                    s.Id = 0;
                    _socialMediaRepository.Add(s);
                    record.SocialMediaId = s.Id;
                }

                _socialMediaVersionRepository.Update(record);
            }
            approval.VersionStatusEnum = VersionStatusEnum.Approved;
            approval.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approval);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Social Media Links > Approve", "id: " + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }
        /// <summary>
        /// Ignore changes and change status to ignored
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.SocialMediaLinks, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var sv = _socialMediaVersionRepository.GetAllSubmitted();

            foreach (var record in sv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _socialMediaVersionRepository.Update(record);
            }

            approval.VersionStatusEnum = VersionStatusEnum.Ignored;
            approval.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approval);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Social Media Links > Reject", "id: " + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }

    
    }
}