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
    public class HP_LogoLinksController : Controller
    {
        private readonly IHP_LogoLinkReopsitory _hP_LogoLinkReopsitory;
        private readonly IHP_LogoLinkVersionRepository _hP_LogoLinkVersionRepository;
        private readonly IFileService _fileService;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<HP_PhotosController> _eventLogger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        public HP_LogoLinksController(IHP_LogoLinkReopsitory hP_LogoLinkReopsitory, IHP_LogoLinkVersionRepository hP_LogoLinkVersionRepository,
            IApprovalNotificationsRepository approvalNotificationsRepository, IToastNotification toastNotification, IEventLogger<HP_PhotosController> eventLogger, UserManager<ApplicationUser> userManager, IFileService fileService)
        {
            _hP_LogoLinkReopsitory = hP_LogoLinkReopsitory;
            _hP_LogoLinkVersionRepository = hP_LogoLinkVersionRepository;
            _fileService = fileService;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }

        /// <summary>
        /// Index for griding all approved logo link objects 
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPLogoLinks, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery]int approvalId)
        {
            ViewBag.approvalId = approvalId;
            return View();
        }

        /// <summary>
        /// Get method for update an existing logo link object by id
        /// </summary>
        /// <param name="id">logo link id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPLogoLinks, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery]int approvalId)
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPLogoLink);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            var logoLinkVersion = _hP_LogoLinkVersionRepository.GetByLogoLinkId(id);
            if (logoLinkVersion == null || logoLinkVersion.VersionStatusEnum == VersionStatusEnum.Approved || logoLinkVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                logoLinkVersion = _hP_LogoLinkReopsitory.GetByLogoLinkId(id);
            }
            var mapped = logoLinkVersion.MapToLogoLinkVersionViewModel();
            ViewBag.approvalId = approvalId;
            return View(mapped);
        }

        /// <summary>
        /// Poest method for update an existing logo link object
        /// </summary>
        /// <param name="ViewModel">logo link new data</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPLogoLinks, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Edit(HP_LogoLinkViewModel ViewModel)
        {
            ModelState.Remove(nameof(ViewModel.ImageFile));

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var llv = _hP_LogoLinkVersionRepository.GetByLogoLinkId(ViewModel.Id);
                var logolinkversion = ViewModel.MapToLogoLinkVersionModel();

                if (llv == null || ViewModel.VersionStatusEnum == VersionStatusEnum.Approved || ViewModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    logolinkversion.CreatedById = user.Id;
                    logolinkversion.CreationDate = DateTime.Now;
                    logolinkversion.VersionStatusEnum = VersionStatusEnum.Draft;
                    logolinkversion.ChangeActionEnum = ChangeActionEnum.Update;
                    logolinkversion.Id = 0;
                    logolinkversion.HomePageLogoLinkId = ViewModel.Id;
                    if (ViewModel.ImageFile != null)
                        logolinkversion.ImageUrl = _fileService.UploadImageUrlNew(ViewModel.ImageFile);
                    _hP_LogoLinkVersionRepository.Add(logolinkversion);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Logo Link > Update", ViewModel.EnTitle);
                    return RedirectToAction(nameof(Index));
                }


                if (ViewModel.ImageFile != null)
                    logolinkversion.ImageUrl = _fileService.UploadImageUrlNew(ViewModel.ImageFile);

                logolinkversion.CreationDate = DateTime.Now;
                logolinkversion.Id = llv.Id;
                logolinkversion.VersionStatusEnum = VersionStatusEnum.Draft;
                logolinkversion.ChangeActionEnum = ChangeActionEnum.Update;
                logolinkversion.ApprovalDate = llv.ApprovalDate;
                logolinkversion.ApprovedById = llv.ApprovedById;
                logolinkversion.CreatedById = llv.CreatedById;
                logolinkversion.CreationDate = llv.CreationDate;
                logolinkversion.ModificationDate = llv.ModificationDate;
                logolinkversion.ModifiedById = llv.ModifiedById;
                logolinkversion.HomePageLogoLinkId = llv.HomePageLogoLinkId;
                var update = _hP_LogoLinkVersionRepository.Update(logolinkversion);
                if (update)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Logo Link > Update", ViewModel.EnTitle);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Home Page > Logo Link > Warning", ViewModel.EnTitle);
                    return View(ViewModel);
                }
            }
            return View(ViewModel);
        }

        /// <summary>
        /// Submit changes method that send notification to approval user with last changes
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPLogoLinks, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanDelete, PrivilegesActions.CanEdit })]
        public async Task<IActionResult> SubmitChanges()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var phv = _hP_LogoLinkVersionRepository.GetAllDrafts();
            foreach (var record in phv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                record.ModifiedById = user.Id;
                record.ModificationDate = DateTime.Now;
                _hP_LogoLinkVersionRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPLogoLink);
            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(HP_LogoLinksController)[0..^10]}",
                    PageName = PagesNamesConst.HPLogoLink,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);
            }

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Home Page > Logo Link > Submitted", " Submitted");
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Approve method that save last changes to appears in the home page
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPLogoLinks, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var llvs = _hP_LogoLinkVersionRepository.GetAllSubmitted();

            foreach (var record in llvs)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;
                _hP_LogoLinkVersionRepository.Update(record);

                var logoLinkObj = new HomePageLogoLink()
                {
                    Id = record.HomePageLogoLinkId ?? 0,
                    ApprovalDate = record.ApprovalDate,
                    ApprovedById = record.ApprovedById,
                    CreatedById = record.CreatedById,
                    CreationDate = record.CreationDate,
                    ModificationDate = record.ModificationDate,
                    ModifiedById = record.ModifiedById,
                    ArTitle = record.ArTitle,
                    EnTitle = record.EnTitle,
                    ImageUrl = record.ImageUrl,
                    Url = record.Url
                };
                _hP_LogoLinkReopsitory.Update(logoLinkObj);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPLogoLink);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Home Page > Logo Link > Approve", " Id :" + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Ignore method that ignore last changes
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPLogoLinks, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var llvs = _hP_LogoLinkVersionRepository.GetAllSubmitted();

            foreach (var record in llvs)
            {
                record.ModificationDate = DateTime.Now;
                record.ModifiedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _hP_LogoLinkVersionRepository.Update(record);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPLogoLink);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Home Page > Logo Link > Reject", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Get all logo links object to index
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPLogoLinks, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetAllLogoLinks()
        {
            var logoLinkData = _hP_LogoLinkVersionRepository.GetLogoLinkVersions();
            return Json(new { data = logoLinkData });
        }
    }
}