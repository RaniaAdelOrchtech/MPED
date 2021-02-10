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
    public class HP_EconomicDevelopmentController : Controller
    {
        private readonly IHP_EconomicDevelopmentReopsitory _hP_EconomicDevelopmentReopsitory;
        private readonly IHP_EconomicDevelopmentVersionRepository _hP_EconomicDevelopmentVersionRepository;
        private readonly IFileService _fileService;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<HP_EconomicDevelopmentController> _eventLogger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;

        public HP_EconomicDevelopmentController(IHP_EconomicDevelopmentReopsitory hP_EconomicDevelopmentReopsitory, IHP_EconomicDevelopmentVersionRepository hP_EconomicDevelopmentVersionRepository,
            IFileService fileService, IToastNotification toastNotification, IEventLogger<HP_EconomicDevelopmentController> eventLogger, UserManager<ApplicationUser> userManager, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _hP_EconomicDevelopmentReopsitory = hP_EconomicDevelopmentReopsitory;
            _hP_EconomicDevelopmentVersionRepository = hP_EconomicDevelopmentVersionRepository;
            _fileService = fileService;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _userManager = userManager;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }
        
        /// <summary>
        /// Index for griding all approved economical development objects
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPEconomicDevelopment, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get method for update an economical development object from database
        /// </summary>
        /// <param name="id">economical development id</param>
        /// <param name="order">order of selected object</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPEconomicDevelopment, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery]int order)
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.EconomicDevelopment);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            if (order <= 0 && order >= 3)
                return NotFound();
            var edv = _hP_EconomicDevelopmentVersionRepository.GetByEcoDevId(id);
            if (edv == null || edv.VersionStatusEnum == VersionStatusEnum.Approved || edv.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                edv = _hP_EconomicDevelopmentReopsitory.GetByEcoDevId(id);
            }
            var mapped = edv.MapToEcoDevVersionViewModel();
            ViewBag.order = order;
            mapped.order = order;
            return View(mapped);
        }

        /// <summary>
        /// Post method for update an economical development object
        /// </summary>
        /// <param name="viewModel">economical development model</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPEconomicDevelopment, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Edit(HP_EconomicDevViewModel viewModel)
        {
            viewModel.ArDescription1.ValidateHtml("ArDescription1", ModelState);
            viewModel.ArDescription2.ValidateHtml("ArDescription2", ModelState);
            viewModel.ArDescription3.ValidateHtml("ArDescription3", ModelState);
            viewModel.EnDescription1.ValidateHtml("EnDescription1", ModelState);
            viewModel.EnDescription2.ValidateHtml("EnDescription2", ModelState);
            viewModel.EnDescription3.ValidateHtml("EnDescription3", ModelState);

            ModelState.Remove(nameof(viewModel.BackGroundImageFile));

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var edv = _hP_EconomicDevelopmentVersionRepository.GetByEcoDevId(viewModel.Id);
                var ecoDevVersion = viewModel.MapToEcoDevVersionModel();

                if (edv == null || viewModel.VersionStatusEnum == VersionStatusEnum.Approved || viewModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    ecoDevVersion.CreatedById = user.Id;
                    ecoDevVersion.CreationDate = DateTime.Now;
                    ecoDevVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                    ecoDevVersion.ChangeActionEnum = ChangeActionEnum.Update;
                    ecoDevVersion.Id = 0;
                    ecoDevVersion.EconomicDevelopmentId = viewModel.Id;
                    if (viewModel.BackGroundImageFile != null)
                        ecoDevVersion.BackGroundImage = _fileService.UploadImageUrlNew(viewModel.BackGroundImageFile);
                    _hP_EconomicDevelopmentVersionRepository.Add(ecoDevVersion);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Economic Development > Update", viewModel.EnTitle1);
                    return RedirectToAction(nameof(Index));
                }

                if (viewModel.BackGroundImageFile != null)
                    ecoDevVersion.BackGroundImage = _fileService.UploadImageUrlNew(viewModel.BackGroundImageFile);
                ecoDevVersion.CreationDate = DateTime.Now;
                ecoDevVersion.Id = edv.Id;
                ecoDevVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                ecoDevVersion.ChangeActionEnum = ChangeActionEnum.Update;
                ecoDevVersion.ApprovalDate = edv.ApprovalDate;
                ecoDevVersion.ApprovedById = edv.ApprovedById;
                ecoDevVersion.CreatedById = edv.CreatedById;
                ecoDevVersion.CreationDate = edv.CreationDate;
                ecoDevVersion.ModificationDate = edv.ModificationDate;
                ecoDevVersion.ModifiedById = edv.ModifiedById;
                ecoDevVersion.EconomicDevelopmentId = edv.EconomicDevelopmentId;
                var update = _hP_EconomicDevelopmentVersionRepository.Update(ecoDevVersion);
                if (update)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Economic Development > Update", viewModel.EnTitle1);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    ViewBag.order = viewModel.order;
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Home Page > Economic Development > Warning", viewModel.EnTitle1);
                    return View(viewModel);
                }
            }
            ViewBag.order = viewModel.order;
            return View(viewModel);
        }

        /// <summary>
        /// Submit changes method that allow users to save last canges and send notification to approval user
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPEconomicDevelopment, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> SubmitChanges()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var edv = _hP_EconomicDevelopmentVersionRepository.GetAllDrafts();
            foreach (var record in edv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                record.ModifiedById = user.Id;
                record.ModificationDate = DateTime.Now;
                _hP_EconomicDevelopmentVersionRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.EconomicDevelopment);
            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(HP_EconomicDevelopmentController)[0..^10]}",
                    PageName = PagesNamesConst.EconomicDevelopment,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);
            }
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Home Page > Economic Development > Submitted", " Submitted");
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Approve last changes to appear in website
        /// </summary>
        /// <returns></returns>

        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPEconomicDevelopment, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var edv = _hP_EconomicDevelopmentVersionRepository.GetAllSubmitted();

            foreach (var record in edv)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;
                _hP_EconomicDevelopmentVersionRepository.Update(record);

                var ecoObj = new EconomicDevelopment()
                {
                    Id = record.EconomicDevelopmentId ?? 0,
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
                    EnDescription3 = record.EnDescription3,
                    ArDescription3 = record.ArDescription3,
                    ArTitle1 = record.ArTitle1,
                    ArTitle2 = record.ArTitle2,
                    ArTitle3 = record.ArTitle3,
                    EnTitle1 = record.EnTitle1,
                    EnTitle2 = record.EnTitle2,
                    EnTitle3 = record.EnTitle3,
                    Url1 = record.Url1,
                    Url2 = record.Url2,
                    Url3 = record.Url3,
                    BackGroundImage = record.BackGroundImage
                };
                _hP_EconomicDevelopmentReopsitory.Update(ecoObj);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.EconomicDevelopment);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Home Page > Economic Development > Approve", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Ignore last changes
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPEconomicDevelopment, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var edv = _hP_EconomicDevelopmentVersionRepository.GetAllSubmitted();

            foreach (var record in edv)
            {
                record.ModificationDate = DateTime.Now;
                record.ModifiedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _hP_EconomicDevelopmentVersionRepository.Update(record);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.EconomicDevelopment);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Home Page > Economic Development > Reject", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Get all approved economical development objects for index
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPEconomicDevelopment, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetAllEconomic()
        {
            var data = _hP_EconomicDevelopmentVersionRepository.GetEcoDevVersions().FirstOrDefault();

            List<HomeCommonViewModel> viewModel = new List<HomeCommonViewModel>();
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
                ArTitle = data.ArTitle1,
                EnTitle = data.EnTitle1,
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
            viewModel.Add(new HomeCommonViewModel()
            {
                Id = data.Id,
                ArTitle = data.ArTitle3,
                EnTitle = data.EnTitle3,
                ArDescription = data.ArDescription3,
                EnDescription = data.EnDescription3,
                Order = 3,
                Type = HomeTypeEnum.Detail
            });

            return Json(new { data = viewModel.OrderBy(x => x.Order) });
        }

    }
}