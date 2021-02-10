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
using static MPMAR.Data.Enums.Enums;
using MPMAR.Common.Interfaces;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class PageMinistryController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;
        private readonly IPageMinistryRepository _pageMinistryRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<PageMinistryController> _eventLogger;
        private readonly IFileService _fileService;
        private readonly IPageMinistryVersionsRepository _pageMinistryVersionsRepository;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly IGlobalElasticSearchService _globalElasticSearchService;

        public PageMinistryController(IPageRouteVersionRepository pageRouteVersionRepository, IPageMinistryRepository pageMinistryRepository, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<PageMinistryController> eventLogger, IFileService fileService, IPageMinistryVersionsRepository pageMinistryVersionsRepository, IApprovalNotificationsRepository approvalNotificationsRepository, IPageRouteRepository pageRouteRepository, IGlobalElasticSearchService globalElasticSearchService)
        {
            _pageRouteVersionRepository = pageRouteVersionRepository;
            _pageMinistryRepository = pageMinistryRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _fileService = fileService;
            _pageMinistryVersionsRepository = pageMinistryVersionsRepository;
            _approvalNotificationsRepository = approvalNotificationsRepository;
            _pageRouteRepository = pageRouteRepository;
            _globalElasticSearchService = globalElasticSearchService;
        }
        /// <summary>
        /// get PageMinistry page index
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <param name="pageRouteId"></param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.PageMinistry, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index(int id, [FromQuery] int pageRouteId, [FromQuery] int approvalId)
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
            ViewBag.pageRouteVersionId = id;
            ViewBag.approvalId = approvalId;
            var pageSectionVersion = _pageMinistryVersionsRepository.GetByPageRouteId(pageRouteId);
            if (pageSectionVersion == null)
            {
                pageSectionVersion = new PageMinistryVersion();
                pageSectionVersion.PageRouteId = pageRouteId;
                return View(pageSectionVersion);
            }
            var pageRouteVer = _pageRouteVersionRepository.GetByPageRoute(pageRouteId);
            return View(pageSectionVersion);
        }



        /// <summary>
        /// get PageMinistry edit page
        /// </summary>
        /// <param name="id">pageMinistryVersions id</param>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.PageMinistry, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery] int pageRouteId)
        {
            PageMinistryEditViewModel viewModel = GetDetails(id, pageRouteId);

            return View(viewModel);


        }
        /// <summary>
        /// get PageMinistry by id and pageroute id
        /// </summary>
        /// <param name="id">pageroute id></param>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        private PageMinistryEditViewModel GetDetails(int id, int pageRouteId)
        {
            PageMinistryEditViewModel viewModel;
            var pageMinistryVersion = _pageMinistryVersionsRepository.GetByPageMinistryId(id);
            if (pageMinistryVersion == null || pageMinistryVersion.VersionStatusEnum == VersionStatusEnum.Approved || pageMinistryVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                var pageMinistry = _pageMinistryRepository.GetDetail(id);
                viewModel = pageMinistry.MapToSctionCardViewModel();
            }
            else
            {
                viewModel = pageMinistryVersion.MapToSctionCardViewModel();
            }
            if (viewModel.PageMinistryId == (int)PageMinistryEnum.MinistrySocialId)
            {
                if (viewModel.EnContent != null)
                {
                    string[] ObjSocialMedia = viewModel.EnContent.Split(',');
                    if (ObjSocialMedia.Length == 3)
                    {
                        viewModel.Twitter = ObjSocialMedia[0].Split(';')[1];
                        viewModel.Instagram = ObjSocialMedia[1].Split(';')[1];
                        viewModel.Globe = ObjSocialMedia[2].Split(';')[1];
                    }
                }
            }
            ViewBag.pageRouteVersionId = pageRouteId;
            var pr = _pageRouteRepository.Get(pageRouteId);
            var notification = _approvalNotificationsRepository.GetByPageNameAndChangeType(pr.SectionName);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            return viewModel;
        }
        /// <summary>
        /// edit pageroute id
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Edit(PageMinistryEditViewModel viewModel)
        {
            var pageRouteVer = _pageRouteVersionRepository.GetByPageRoute((int)viewModel.PageRouteId);
            if (ModelState.IsValid)
            {
                var pageMinistryVersion = _pageMinistryVersionsRepository.GetByPageMinistryId(viewModel.Id);
                var pageMinistryVersionModel = viewModel.MapToPageMinistryVersion();
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (viewModel.Id == (int)PageMinistryEnum.MinistrySocialId)
                {
                    pageMinistryVersionModel.EnContent = "Twitter;" + viewModel.Twitter + "," + "Instagram;" + viewModel.Instagram + "," +
                        "Globe;" + viewModel.Globe;

                }
                else
                {
                    pageMinistryVersionModel.EnContent = viewModel.EnContent;
                }
                if (pageMinistryVersion == null || pageMinistryVersionModel.VersionStatusEnum == VersionStatusEnum.Approved || pageMinistryVersionModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    pageMinistryVersionModel.CreatedById = user.Id;
                    pageMinistryVersionModel.CreationDate = DateTime.Now;
                    pageMinistryVersionModel.VersionStatusEnum = VersionStatusEnum.Draft;
                    pageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                    pageMinistryVersionModel.ChangeActionEnum = ChangeActionEnum.Update;
                    pageMinistryVersionModel.Id = 0;
                    pageMinistryVersionModel.PageMinistryId = viewModel.Id;
                    pageMinistryVersionModel.NavItemId = viewModel.PageRouteId;
                    pageMinistryVersionModel.StatusId = (int)RequestStatus.Approved;
                    if (viewModel.ImageFile != null)
                        pageMinistryVersionModel.ImageUrl = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                    if (viewModel.EnImageFile != null)
                        pageMinistryVersionModel.EnImageUrl = _fileService.UploadImageUrlNew(viewModel.EnImageFile);
                    _pageMinistryVersionsRepository.Add(pageMinistryVersionModel);
                    _pageRouteVersionRepository.Update(pageRouteVer);
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Static Page > Page Ministry > Edit", viewModel.EnName);
                    return RedirectToAction(nameof(Index), new { id = pageRouteVer.Id, pageRouteId = viewModel.PageRouteId });
                }


                pageMinistryVersionModel.CreationDate = DateTime.Now;
                pageMinistryVersionModel.NavItemId = viewModel.PageRouteId;
                pageMinistryVersionModel.StatusId = (int)RequestStatus.Approved;
                pageMinistryVersionModel.Id = pageMinistryVersion.Id;
                pageMinistryVersionModel.VersionStatusEnum = VersionStatusEnum.Draft;
                pageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                pageMinistryVersionModel.ApprovalDate = pageMinistryVersion.ApprovalDate;
                pageMinistryVersionModel.ApprovedById = pageMinistryVersion.ApprovedById;
                pageMinistryVersionModel.CreatedById = pageMinistryVersion.CreatedById;
                pageMinistryVersionModel.CreationDate = pageMinistryVersion.CreationDate;
                pageMinistryVersionModel.ChangeActionEnum = ChangeActionEnum.Update;
                if (viewModel.ImageFile != null)
                    pageMinistryVersionModel.ImageUrl = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                if (viewModel.EnImageFile != null)
                    pageMinistryVersionModel.EnImageUrl = _fileService.UploadImageUrlNew(viewModel.EnImageFile);
                var newSectionCardVersion = _pageMinistryVersionsRepository.Update(pageMinistryVersionModel);
                _pageRouteVersionRepository.Update(pageRouteVer);
                if (newSectionCardVersion != null)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Static Page > Page Ministry > Edit", viewModel.EnName);
                    return RedirectToAction(nameof(Index), new { id = pageRouteVer.Id, pageRouteId = pageMinistryVersion.PageRouteId });
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Static Page > Page Ministry > Edit", viewModel.EnName);
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                }


            }

            return View(viewModel);
        }
        /// <summary>
        /// submit changes for the approval user 
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.PageMinistry, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> SubmitChanges([FromQuery] int pageRouteId)
        {
            var pageRouteVer = _pageRouteVersionRepository.GetByPageRoute(pageRouteId);

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var pmv = _pageMinistryVersionsRepository.GetAllDrafts(pageRouteId);
            var pr = _pageRouteRepository.Get(pageRouteId);
            foreach (var record in pmv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                _pageMinistryVersionsRepository.Update(record);
            }
            pageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Submitted;
            _pageRouteVersionRepository.Update(pageRouteVer);

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageNameAndChangeType(pr.SectionName);

            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(PageMinistryController)[0..^10]}?pageRouteId={pageRouteId}",
                    PageName = pr.SectionName,
                    PageType = PageType.Static,
                    RelatedVersionId = pageRouteId,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);




            }
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Static Page > Page Ministry > Submitted", "Submitted Id :" + pageRouteId);
            return RedirectToAction(nameof(Index), new { id = pageRouteVer.Id, pageRouteId = pageRouteId });
        }
        /// <summary>
        /// get details of PageMinistry
        /// </summary>
        /// <param name="id">PageMinistry id</param>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.PageMinistry, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Details(int id, [FromQuery] int pageRouteId, [FromQuery] int approvalId)
        {
            PageMinistryEditViewModel viewModel = GetDetails(id, pageRouteId);
            ViewBag.approvalId = approvalId;
            return View(viewModel);
        }



        /// <summary>
        ///  approve changes,change status to approved  and update the non version 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.PageMinistry, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery] int approvalId, int pageRouteId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var evvs = _pageMinistryVersionsRepository.GetAllSubmitted(approval.RelatedVersionId ?? 0);

            foreach (var record in evvs)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;
                _pageMinistryVersionsRepository.Update(record);

                var pageMinistry = new PageMinistry()
                {
                    Id = record.PageMinistryId ?? 0,
                    ApprovalDate = record.ApprovalDate,
                    ApprovedById = record.ApprovedById,
                    ArContent = record.ArContent,
                    ArName = record.ArName,
                    CreatedById = record.CreatedById,
                    CreationDate = record.CreationDate,
                    EnContent = record.EnContent,
                    EnName = record.EnName,
                    ImageUrl = record.ImageUrl,
                    EnImageUrl = record.EnImageUrl,
                    IsDobulQuote = record.IsDobulQuote,
                    IsHeading = record.IsHeading,
                    IsSection = record.IsSection,
                    PageRouteId = record.PageRouteId,
                    NavItemId = record.NavItemId,
                    IsActive = record.IsActive,
                    IsDeleted = record.IsDeleted,
                    Order = record.Order,
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

                _pageMinistryRepository.Update(pageMinistry);
            }

            approval.VersionStatusEnum = VersionStatusEnum.Approved;
            approval.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approval);

            var prv = _pageRouteVersionRepository.GetByPageRoute((int)approval.RelatedVersionId);
            prv.ContentVersionStatusEnum = VersionStatusEnum.Approved;
            _pageRouteVersionRepository.Update(prv);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Static Page > Page Ministry > Approve", " Id :" + id);

            try
            {
                await _globalElasticSearchService.DeleteAsync(prv.PageRouteId ?? 0);
                await _globalElasticSearchService.AddAsync(_pageRouteRepository.GetPageData(prv.PageRouteId ?? 0));
            }
            catch { }

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        ///  Ignore changes and change status to ignored
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.PageMinistry, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery] int approvalId, int pageRouteId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var evvs = _pageMinistryVersionsRepository.GetAllSubmitted(approval.RelatedVersionId ?? 0);

            foreach (var record in evvs)
            {
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _pageMinistryVersionsRepository.Update(record);
            }

            approval.VersionStatusEnum = VersionStatusEnum.Ignored;
            approval.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approval);

            var prv = _pageRouteVersionRepository.GetByPageRoute((int)approval.RelatedVersionId);
            prv.ContentVersionStatusEnum = VersionStatusEnum.Approved;
            _pageRouteVersionRepository.Update(prv);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Static Page > Page Ministry > Reject", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// get list of PageMinistry 
        /// </summary>
        /// <param name="id">pageMinistryVersion id</param>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.PageMinistry, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetPageMinistry(int pageRouteId)
        {
            var pageMinistryVersion = _pageMinistryVersionsRepository.GetMinistries(pageRouteId);
            return Json(new { data = pageMinistryVersion });
        }

    }
}