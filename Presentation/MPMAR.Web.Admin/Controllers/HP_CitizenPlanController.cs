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
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;
using MPMAR.Business;
using MPMAR.Common.Interfaces;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class HP_CitizenPlanController : Controller
    {
        private readonly ICitizenPlanRepository _citizenPlanRepository;
        private readonly IFileService _fileService;
        private readonly ICitizenPlanVersionsRepository _citizenPlanVersionsRepository;
        private readonly IToastNotification _toastNotification;
private readonly IEventLogger<HP_CitizenPlanController> _eventLogger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;

        public HP_CitizenPlanController(ICitizenPlanRepository citizenPlanRepository, ICitizenPlanVersionsRepository citizenPlanVersionsRepository,
            IFileService fileService, IToastNotification toastNotification, IEventLogger<HP_CitizenPlanController> eventLogger, UserManager<ApplicationUser> userManager, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _citizenPlanRepository = citizenPlanRepository;
            _citizenPlanVersionsRepository = citizenPlanVersionsRepository;
            _fileService = fileService;
            _toastNotification = toastNotification;
_eventLogger = eventLogger;
            _userManager = userManager;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }
        /// <summary>
        /// get  HP_CitizenPlan page index
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPCitizenPlan, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index()
        {
            var citizenPlanVersions = _citizenPlanVersionsRepository.GetCitizenPlanVersions();
            return View(citizenPlanVersions[0]);
        }
        /// <summary>
        /// get HP_CitizenPlan edit page
        /// </summary>
        /// <param name="id">citizenPlan id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPCitizenPlan, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id)
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.CitizenPlanHP);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            var cpv = _citizenPlanVersionsRepository.GetByCitizenPlanId(id);
            if (cpv == null || cpv.VersionStatusEnum == VersionStatusEnum.Approved || cpv.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                cpv = _citizenPlanRepository.GetByCitizenPlanId(id);
            }
            var mapped = cpv.MapToCitizenPlanVersionViewModel();
            return View(mapped);
        }
        /// <summary>
        /// edit HP_CitizenPlan 
        /// </summary>
        /// <param name="ViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPCitizenPlan, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Edit(CitizenPlanViewModel ViewModel)
        {
            ViewModel.ArDescription.ValidateHtml("ArDescription", ModelState);
            ViewModel.EnDescription.ValidateHtml("EnDescription", ModelState);

            ModelState.Remove(nameof(ViewModel.ImageFile));
            ModelState.Remove(nameof(ViewModel.EnImageFile));

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var cpv = _citizenPlanVersionsRepository.GetByCitizenPlanId(ViewModel.Id);
                var citizenPlanVersion = ViewModel.MapToCitizenPlanVersionModel();

                //if citizenPlanVersions wasn't exist or  CitizenPlanViewModel approved or ignored then create new version  
                if (cpv == null || ViewModel.VersionStatusEnum == VersionStatusEnum.Approved || ViewModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    citizenPlanVersion.CreatedById = user.Id;
                    citizenPlanVersion.CreationDate = DateTime.Now;
                    citizenPlanVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                    citizenPlanVersion.ChangeActionEnum = ChangeActionEnum.Update;
                    citizenPlanVersion.Id = 0;
                    citizenPlanVersion.CitizenPlanId = ViewModel.Id;
                    if (ViewModel.ImageFile != null)
                        citizenPlanVersion.Image = _fileService.UploadImageUrlNew(ViewModel.ImageFile);
                    if (ViewModel.EnImageFile != null)
                        citizenPlanVersion.EnImage = _fileService.UploadImageUrlNew(ViewModel.EnImageFile);
                    _citizenPlanVersionsRepository.Add(citizenPlanVersion);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Citizen Plan > Update", ViewModel.EnTitle);
                    return RedirectToAction(nameof(Index));
                }


                if (ViewModel.ImageFile != null)
                    citizenPlanVersion.Image = _fileService.UploadImageUrlNew(ViewModel.ImageFile);
                if (ViewModel.EnImageFile != null)
                    citizenPlanVersion.EnImage = _fileService.UploadImageUrlNew(ViewModel.EnImageFile);

                citizenPlanVersion.CreationDate = DateTime.Now;
                citizenPlanVersion.Id = cpv.Id;
                citizenPlanVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                citizenPlanVersion.ChangeActionEnum = ChangeActionEnum.Update;
                citizenPlanVersion.ApprovalDate = cpv.ApprovalDate;
                citizenPlanVersion.ApprovedById = cpv.ApprovedById;
                citizenPlanVersion.CreatedById = cpv.CreatedById;
                citizenPlanVersion.CreationDate = cpv.CreationDate;
                citizenPlanVersion.ModificationDate = cpv.ModificationDate;
                citizenPlanVersion.ModifiedById = cpv.ModifiedById;
                citizenPlanVersion.CitizenPlanId = cpv.CitizenPlanId;
                var update = _citizenPlanVersionsRepository.Update(citizenPlanVersion);
                if (update)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Citizen Plan > Update", ViewModel.EnTitle);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning , "Home Page > Citizen Plan > Warning", ViewModel.EnTitle);
                    return View(ViewModel);
                }
            }
            return View(ViewModel);
        }

        /// <summary>
        /// submit changes for the approval user 
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPCitizenPlan, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanDelete, PrivilegesActions.CanEdit })]
        public async Task<IActionResult> SubmitChanges()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var cpv = _citizenPlanVersionsRepository.GetAllDrafts();
            //mark all drafts citizenPlanVersions as submitted
            foreach (var record in cpv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                record.ModifiedById = user.Id;
                record.ModificationDate = DateTime.Now;
                _citizenPlanVersionsRepository.Update(record);
            }

            //create new notification if there wasn't
            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.CitizenPlanHP);
            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(HP_CitizenPlanController)[0..^10]}",
                    PageName = PagesNamesConst.CitizenPlanHP,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);
            }

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Home Page > Citizen Plan > Submitted", " Submitted" );
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// approve changes,change status to approved  and update the non version 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPCitizenPlan, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var cpv = _citizenPlanVersionsRepository.GetAllSubmitted();
            //mark all submited citizenPlanVersions as approved
            foreach (var record in cpv)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;
                _citizenPlanVersionsRepository.Update(record);

                var citizenPlanObj = new CitizenPlan()
                {
                    Id = record.CitizenPlanId ?? 0,
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
                    Image = record.Image,
                    EnImage = record.EnImage,
                    Link = record.Link,
                    ArMainTitle = record.ArMainTitle,
                    EnMainTitle = record.EnMainTitle
                };
                _citizenPlanRepository.Update(citizenPlanObj);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.CitizenPlanHP);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Home Page > Citizen Plan > Approve", " Id :" + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }
        /// <summary>
        /// Ignore changes and change status to ignored
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPCitizenPlan, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var cpv = _citizenPlanVersionsRepository.GetAllSubmitted();
            //mark all submited citizenPlanVersions as ignored
            foreach (var record in cpv)
            {
                record.ModificationDate = DateTime.Now;
                record.ModifiedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _citizenPlanVersionsRepository.Update(record);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.CitizenPlanHP);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Home Page > Citizen Plan > Reject", " Id :" + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }
    }
}