using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Common.Helpers;
using MPMAR.Data;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;
using MPMAR.Business;
using MPMAR.Web.Admin.Helpers;
using static MPMAR.Data.Enums.Enums;
using MPMAR.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using MPMAR.Common;
using MPMAR.Data.Consts;
using MPMAR.Common.Interfaces;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class EgyptVisionController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly IEgyptVisionRepository _egyptVisionRepository;
        private readonly IEgyptVisionVersionRepository _egyptVisionVersionRepository;
        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<EgyptVisionController> _eventLogger;
        private readonly IFileService _fileService;
        private readonly IGlobalElasticSearchService _globalElasticSearchService;
        private readonly IPageRouteRepository _pageRouteRepository;

        public EgyptVisionController(IPageRouteVersionRepository pageRouteVersionRepository, IApprovalNotificationsRepository approvalNotificationsRepository, IEgyptVisionRepository egyptVisionRepository, IEgyptVisionVersionRepository egyptVisionVersionRepository, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<EgyptVisionController> eventLogger, IFileService fileService, IGlobalElasticSearchService globalElasticSearchService, IPageRouteRepository pageRouteRepository)
        {
            _egyptVisionRepository = egyptVisionRepository;
            _egyptVisionVersionRepository = egyptVisionVersionRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _fileService = fileService;
            _globalElasticSearchService = globalElasticSearchService;
            _pageRouteRepository = pageRouteRepository;
            _approvalNotificationsRepository = approvalNotificationsRepository;
            _pageRouteVersionRepository = pageRouteVersionRepository;
        }

        /// <summary>
        /// Index for griding egypt vision objects
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <param name="relatedId">related object id in notification</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.EgyptVision2030)]
        public IActionResult Index(int id, [FromQuery] int relatedId, [FromQuery] int approvalId)
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

            ViewBag.RelatedId = relatedId;
            ViewBag.pageRouteVersionId = id;
            ViewBag.approvalId = approvalId;

            return View();
        }



        /// <summary>
        /// Get method for edit egypt vision object
        /// </summary>
        /// <param name="id">egypt vision object</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.EgyptVision2030)]
        public IActionResult Edit(int id)
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.EgyptVision);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            var egyptVisionVersion = _egyptVisionVersionRepository.GetByEgyptVisionId(id);
            ViewBag.Id = egyptVisionVersion.EgyptVisionId;
            if (egyptVisionVersion == null || egyptVisionVersion.VersionStatusEnum == VersionStatusEnum.Approved || egyptVisionVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                egyptVisionVersion = _egyptVisionRepository.GetByEgyptVisionId(id);
                ViewBag.Id = egyptVisionVersion.Id;
            }
            var mapped = egyptVisionVersion.MapToEgyptVisionVersionViewModel();
            return View(mapped);
        }

        /// <summary>
        /// Post method for edit egypt vision object
        /// </summary>
        /// <param name="ViewModel">egypt vision object new data</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.EgyptVision2030)]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Edit(EgyptVisionVersionEditViewModel ViewModel)
        {
            var pageRouteVer = _pageRouteVersionRepository.Get(ViewModel.PageRouteVersionId);
            ViewModel.EnEgyptVisionDesc.ValidateHtml("EnEgyptVisionDesc", ModelState);
            ViewModel.ArEgyptVisionDesc.ValidateHtml("ArEgyptVisionDesc", ModelState);

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var evv = _egyptVisionVersionRepository.GetByEgyptVisionId(ViewModel.Id);
                var egyptVisionVersion = ViewModel.MapToEgyptVisionVersionModel();

                if (evv == null || ViewModel.VersionStatusEnum == VersionStatusEnum.Approved || ViewModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    egyptVisionVersion.CreatedById = user.Id;
                    egyptVisionVersion.CreationDate = DateTime.Now;
                    egyptVisionVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                    pageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                    egyptVisionVersion.ChangeActionEnum = ChangeActionEnum.Update;
                    egyptVisionVersion.Id = 0;
                    egyptVisionVersion.EgyptVisionId = ViewModel.Id;
                    if (ViewModel.EnImageFile != null)
                        egyptVisionVersion.EnImagePath = _fileService.UploadImageUrlNew(ViewModel.EnImageFile);
                    if (ViewModel.ArImageFile != null)
                        egyptVisionVersion.ArImagePath = _fileService.UploadImageUrlNew(ViewModel.ArImageFile);
                    _egyptVisionVersionRepository.Add(egyptVisionVersion);
                    _pageRouteVersionRepository.Update(pageRouteVer);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Static Page > Page Egypt Vision > Edit", ViewModel.EnEgyptVisionName);

                    return RedirectToAction(nameof(Index), new { id = pageRouteVer.Id });
                }


                if (ViewModel.EnImageFile != null)
                    egyptVisionVersion.EnImagePath = _fileService.UploadImageUrlNew(ViewModel.EnImageFile);
                if (ViewModel.ArImageFile != null)
                    egyptVisionVersion.ArImagePath = _fileService.UploadImageUrlNew(ViewModel.ArImageFile);
                egyptVisionVersion.CreationDate = DateTime.Now;
                egyptVisionVersion.Id = evv.Id;
                egyptVisionVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                pageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                egyptVisionVersion.ChangeActionEnum = ChangeActionEnum.Update;
                egyptVisionVersion.ApprovalDate = evv.ApprovalDate;
                egyptVisionVersion.ApprovedById = evv.ApprovedById;
                egyptVisionVersion.CreatedById = evv.CreatedById;
                egyptVisionVersion.CreationDate = evv.CreationDate;
                egyptVisionVersion.ModificationDate = evv.ModificationDate;
                egyptVisionVersion.ModifiedById = evv.ModifiedById;
                egyptVisionVersion.StatusId = evv.StatusId;
                egyptVisionVersion.EgyptVisionId = evv.EgyptVisionId;
                _pageRouteVersionRepository.Update(pageRouteVer);
                var update = _egyptVisionVersionRepository.Update(egyptVisionVersion);
                if (update)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Static Page > Page Egypt Vision > Edit", ViewModel.EnEgyptVisionName);

                    return RedirectToAction(nameof(Index), new { id = pageRouteVer.Id });
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Static Page > Page Egypt Vision > Warning in Edit", ViewModel.EnEgyptVisionName);
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    return View(ViewModel);
                }
            }
            return View(ViewModel);
        }

        /// <summary>
        /// subbmit changes method for approval user to recive notification by changes
        /// </summary>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit, PrivilegesActions.CanAdd, PrivilegesActions.CanDelete }, StaticPagesIdsConst.EgyptVision2030)]
        public async Task<IActionResult> SubmitChanges(int pageRouteVersionId)
        {
            var pageRouteVer = _pageRouteVersionRepository.Get(pageRouteVersionId);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var evv = _egyptVisionVersionRepository.GetAllDrafts();
            foreach (var record in evv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                record.ModifiedById = user.Id;
                record.ModificationDate = DateTime.Now;
                _egyptVisionVersionRepository.Update(record);
            }
            pageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Submitted;
            _pageRouteVersionRepository.Update(pageRouteVer);

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.EgyptVision);
            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(EgyptVisionController)[0..^10]}",
                    PageName = PagesNamesConst.EgyptVision,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id,
                    RelatedVersionId = pageRouteVer.Id
                };
                _approvalNotificationsRepository.Add(approval);

            }
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Static Page > Page Egypt Vision > Submitted", "Submitted ");
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            return RedirectToAction(nameof(Index), new { id = pageRouteVer.Id });
        }

        /// <summary>
        /// Details for egypt vision object
        /// </summary>
        /// <param name="id">egypt vision id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.EgyptVision2030)]
        public IActionResult Details(int id, [FromQuery] int approvalId)
        {
            var pageSectionCardVersion = _egyptVisionVersionRepository.GetByEgyptVisionId(id);
            ViewBag.Id = pageSectionCardVersion.EgyptVisionId;
            ViewBag.IsVersion = "true";
            if (pageSectionCardVersion == null)
            {
                pageSectionCardVersion = _egyptVisionRepository.GetByEgyptVisionId(id);
                ViewBag.Id = pageSectionCardVersion.Id;
                ViewBag.IsVersion = "false";
            }
            ViewBag.approvalId = approvalId;
            return View(pageSectionCardVersion);
        }



        /// <summary>
        /// Approve last changes by approval user 
        /// </summary>
        /// <param name="approvalId">notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanApprove }, StaticPagesIdsConst.EgyptVision2030)]
        public async Task<IActionResult> Approve(int id, [FromQuery] int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var evvs = _egyptVisionVersionRepository.GetAllSubmitted();

            foreach (var record in evvs)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;
                _egyptVisionVersionRepository.Update(record);

                var egyptVisionObj = new EgyptVision()
                {
                    Id = record.EgyptVisionId ?? 0,
                    ApprovalDate = record.ApprovalDate,
                    ApprovedById = record.ApprovedById,
                    ArEgyptVisionDesc = record.ArEgyptVisionDesc,
                    ArEgyptVisionName = record.ArEgyptVisionName,
                    ArEgyptVisionSmallDesc = record.ArEgyptVisionSmallDesc,
                    ArImagePath = record.ArImagePath,
                    BgColor = record.BgColor,
                    CreatedById = record.CreatedById,
                    CreationDate = record.CreationDate,
                    EnEgyptVisionDesc = record.EnEgyptVisionDesc,
                    EnEgyptVisionName = record.EnEgyptVisionName,
                    EnEgyptVisionSmallDesc = record.EnEgyptVisionSmallDesc,
                    EnImagePath = record.EnImagePath,
                    ImagePositionIsRight = record.ImagePositionIsRight,
                    IsActive = record.IsActive,
                    IsDeleted = record.IsDeleted,
                    LineColor = record.LineColor,
                    ModificationDate = record.ModificationDate,
                    ModifiedById = record.ModifiedById,
                    Order = record.Order,
                    PageRouteVersionId = record.PageRouteVersionId,
                    StatusId = record.StatusId,
                    SeoDescriptionAR = record.SeoDescriptionAR,
                    SeoDescriptionEN = record.SeoDescriptionEN,
                    SeoOgTitleAR = record.SeoOgTitleAR,
                    SeoOgTitleEN = record.SeoOgTitleEN,
                    SeoTitleAR = record.SeoTitleAR,
                    SeoTitleEN = record.SeoTitleEN,
                    SeoTwitterCardAR = record.SeoTwitterCardAR,
                    SeoTwitterCardEN = record.SeoTwitterCardEN
                };
                _egyptVisionRepository.Update(egyptVisionObj);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.EgyptVision);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            var prv = _pageRouteVersionRepository.Get((int)approvalItem.RelatedVersionId);
            prv.ContentVersionStatusEnum = VersionStatusEnum.Approved;
            _pageRouteVersionRepository.Update(prv);


            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Static Page > Page Egypt Vision > Approve", "Approveed ");
            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            try
            {
                await _globalElasticSearchService.DeleteAsync(prv.PageRouteId ?? 0);
                await _globalElasticSearchService.AddAsync(_pageRouteRepository.GetPageData(prv.PageRouteId ?? 0));
            }
            catch { }
            return RedirectToAction("Index", "ApprovalNotifications");

        }

        /// <summary>
        /// Ignore last changes by approval user 
        /// </summary>
        /// <param name="approvalId">notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanApprove }, StaticPagesIdsConst.EgyptVision2030)]
        public async Task<IActionResult> Ignore(int id, [FromQuery] int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var evvs = _egyptVisionVersionRepository.GetAllSubmitted();

            foreach (var record in evvs)
            {
                record.ModificationDate = DateTime.Now;
                record.ModifiedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _egyptVisionVersionRepository.Update(record);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.EgyptVision);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            var prv = _pageRouteVersionRepository.Get((int)approvalItem.RelatedVersionId);
            prv.ContentVersionStatusEnum = VersionStatusEnum.Approved;
            _pageRouteVersionRepository.Update(prv);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Static Page > Page Egypt Vision > Reject", "Rejected ");
            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);
            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Get all egypt vision objects for index
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.EgyptVision2030)]
        public JsonResult GetEgyptVision(int id)
        {
            var egyptVision = _egyptVisionVersionRepository.GetEgyptVisionVersions();

            return Json(new { data = egyptVision });
        }
    }
}