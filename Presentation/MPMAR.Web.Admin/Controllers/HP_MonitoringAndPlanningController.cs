using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPMAR.Analytics.Data.Migrations;
using MPMAR.Business.Interfaces;
using MPMAR.Common;
using MPMAR.Common.Helpers;
using MPMAR.Data;
using MPMAR.Data.Consts;
using MPMAR.Data.Enums;
using MPMAR.Data.HomePageModels;
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
    public class HP_MonitoringAndPlanningController : Controller
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly IMonitoringVersionsRepository _monitoringVersionsRepository;
        private readonly IFileService _fileService;
        private readonly IToastNotification _toastNotification;
private readonly IEventLogger<HP_MonitoringAndPlanningController> _eventLogger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;

        public HP_MonitoringAndPlanningController(IMonitoringRepository monitoringRepository, IMonitoringVersionsRepository monitoringVersionsRepository,
            IFileService fileService, IToastNotification toastNotification, IEventLogger<HP_MonitoringAndPlanningController> eventLogger, UserManager<ApplicationUser> userManager, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _monitoringRepository = monitoringRepository;
            _monitoringVersionsRepository = monitoringVersionsRepository;
            _fileService = fileService;
            _toastNotification = toastNotification;
_eventLogger = eventLogger;
            _userManager = userManager;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }
        /// <summary>
        /// get HP_MonitoringAndPlanning page index
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPMonitoringAndPlanning, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// get all HP_MonitoringAndPlanning
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPMonitoringAndPlanning, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetAll()
        {
            var data = _monitoringVersionsRepository.GetMonitringVersions().FirstOrDefault();
            List<HomeCommonViewModel> viewModel = new List<HomeCommonViewModel>();
            if (data != null)
            {
                viewModel.Add(new HomeCommonViewModel()
                {
                    Id = data.Id,
                    ArTitle = data.ArMainTitle,
                    EnTitle = data.EnMainTitle,
                    ArDescription = "",
                    EnDescription = "",
                    Order = 0,
                    Type = HomeTypeEnum.Title
                });
                viewModel.Add(new HomeCommonViewModel()
                {
                    Id = data.Id,
                    ArDescription = data.ArDescription1,
                    EnDescription = data.EnDescription1,
                    Order = 1,
                    Type = HomeTypeEnum.Detail
                });
                viewModel.Add(new HomeCommonViewModel()
                {
                    Id = data.Id,
                    ArTitle = data.ArTitle2,
                    EnTitle = data.EnTitle2,
                    ArDescription = data.ArDescription2,
                    EnDescription = data.EnDescription2,
                    Order = 2,
                    Type = HomeTypeEnum.Detail
                });
            }


            return Json(new { data = viewModel.OrderBy(x => x.Order) });
        }
        /// <summary>
        /// get HP_MonitoringAndPlanning edit page
        /// </summary>
        /// <param name="id">monitoringVersions id</param>
        /// <param name="order">order of monitoringVersions</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPMonitoringAndPlanning, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery]int order)
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.Monitring);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            if (order <= 0 && order >= 3)
                return NotFound();
            var mv = _monitoringVersionsRepository.GetByMonitringId(id);
            if (mv == null || mv.VersionStatusEnum == VersionStatusEnum.Approved || mv.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                mv = _monitoringRepository.GetByMonitringId(id);
            }
            var mapped = mv.MapToMonitringVersionViewModel();
            ViewBag.order = order;
            mapped.Order = order;
            return View(mapped);

            var model = _monitoringRepository.Get();
            var viewModel = model.MapToMonitoringViewModel();
            viewModel.Order = order;
            ViewBag.Order = order;
            return View(viewModel);
        }
        /// <summary>
        /// edit HP_MonitoringAndPlanning
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPMonitoringAndPlanning, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Edit(MonitoringViewModel viewModel)
        {
            viewModel.ArDescription1.ValidateHtml("ArDescription1", ModelState);
            viewModel.ArDescription2.ValidateHtml("ArDescription2", ModelState);
            viewModel.EnDescription1.ValidateHtml("EnDescription1", ModelState);
            viewModel.EnDescription2.ValidateHtml("EnDescription2", ModelState);

            ModelState.Remove(nameof(viewModel.ImageFile1));
            ModelState.Remove(nameof(viewModel.BackGroundImageFile));

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var mv = _monitoringVersionsRepository.GetByMonitringId(viewModel.Id);
                var MonitringVersion = viewModel.MapToMonitringVersionModel();
                //if monitoringVersions wasn't exist or  MonitoringViewModel approved or ignored then create new version 
                if (mv == null || viewModel.VersionStatusEnum == VersionStatusEnum.Approved || viewModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    MonitringVersion.CreatedById = user.Id;
                    MonitringVersion.CreationDate = DateTime.Now;
                    MonitringVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                    MonitringVersion.ChangeActionEnum = ChangeActionEnum.Update;
                    MonitringVersion.Id = 0;
                    MonitringVersion.MonitoringId = viewModel.Id;
                    if (viewModel.ImageFile1 != null)
                        MonitringVersion.Image1 = _fileService.UploadImageUrlNew(viewModel.ImageFile1);
                    if (viewModel.BackGroundImageFile != null)
                        MonitringVersion.BackGroundImage = _fileService.UploadImageUrlNew(viewModel.BackGroundImageFile);
                    _monitoringVersionsRepository.Add(MonitringVersion);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Monitoring And Planning > Update", viewModel.EnTitle2);
                    return RedirectToAction(nameof(Index));
                }
                if (viewModel.ImageFile1 != null)
                    MonitringVersion.Image1 = _fileService.UploadImageUrlNew(viewModel.ImageFile1);
                if (viewModel.BackGroundImageFile != null)
                    MonitringVersion.BackGroundImage = _fileService.UploadImageUrlNew(viewModel.BackGroundImageFile);
                MonitringVersion.CreationDate = DateTime.Now;
                MonitringVersion.Id = mv.Id;
                MonitringVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                MonitringVersion.ChangeActionEnum = ChangeActionEnum.Update;
                MonitringVersion.ApprovalDate = mv.ApprovalDate;
                MonitringVersion.ApprovedById = mv.ApprovedById;
                MonitringVersion.CreatedById = mv.CreatedById;
                MonitringVersion.CreationDate = mv.CreationDate;
                MonitringVersion.ModificationDate = mv.ModificationDate;
                MonitringVersion.ModifiedById = mv.ModifiedById;
                MonitringVersion.MonitoringId = mv.MonitoringId;
                var update = _monitoringVersionsRepository.Update(MonitringVersion);
                if (update)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Monitoring And Planning > Update",viewModel.EnTitle2);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Home Page > Monitoring And Planning > Warning", viewModel.EnTitle2);
                    ViewBag.order = viewModel.Order;
                    return View(viewModel);
                }
            }
            ViewBag.order = viewModel.Order;
            return View(viewModel);
        }
        /// <summary>
        /// submit changes for the approval user 
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPMonitoringAndPlanning, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> SubmitChanges()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var mv = _monitoringVersionsRepository.GetAllDrafts();
            foreach (var record in mv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                record.ModifiedById = user.Id;
                record.ModificationDate = DateTime.Now;
                _monitoringVersionsRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.Monitring);
            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(HP_MonitoringAndPlanningController)[0..^10]}",
                    PageName = PagesNamesConst.Monitring,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);
            }
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Home Page > Monitoring And Planning > Submitted", " Submitted" );

            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// approve changes,change status to approved  and update the non version 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPMonitoringAndPlanning, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var mv = _monitoringVersionsRepository.GetAllSubmitted();

            foreach (var record in mv)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;
                _monitoringVersionsRepository.Update(record);

                var monitringObj = new Monitoring()
                {
                    Id = record.MonitoringId ?? 0,
                    ApprovalDate = record.ApprovalDate,
                    ApprovedById = record.ApprovedById,
                    CreatedById = record.CreatedById,
                    CreationDate = record.CreationDate,
                    IsActive = record.IsActive,
                    IsDeleted = record.IsDeleted,
                    ModificationDate = record.ModificationDate,
                    ModifiedById = record.ModifiedById,
                    ArMainTitle = record.ArMainTitle,
                    EnMainTitle = record.EnMainTitle,
                    EnDescription1 = record.EnDescription1,
                    ArDescription1 = record.ArDescription1,
                    EnDescription2 = record.EnDescription2,
                    ArDescription2 = record.ArDescription2,
                    BackGroundImage = record.BackGroundImage,
                    ArTitle2 = record.ArTitle2,
                    EnTitle2 = record.EnTitle2,
                    Link1 = record.Link1,
                    Link2 = record.Link2,
                    Image1 = record.Image1,
                };
                _monitoringRepository.Update(monitringObj);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.Monitring);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Home Page > Monitoring And Planning > Approve", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }
        /// <summary>
        /// Ignore changes and change status to ignored
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPMonitoringAndPlanning, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var mv = _monitoringVersionsRepository.GetAllSubmitted();

            foreach (var record in mv)
            {
                record.ModificationDate = DateTime.Now;
                record.ModifiedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _monitoringVersionsRepository.Update(record);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.Monitring);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Home Page > Monitoring And Planning > Reject", " Id :" + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }
    }
}