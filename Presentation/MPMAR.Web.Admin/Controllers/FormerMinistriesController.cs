using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Common.Helpers;
using MPMAR.Data;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.ViewModels;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Data.Enums;
using Microsoft.AspNetCore.Identity;
using MPMAR.Common;
using Microsoft.AspNetCore.Authorization;
using NToastNotify;
using MPMAR.Business;
using MPMAR.Data.Consts;
using MPMAR.Common.Interfaces;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class FormerMinistriesController : Controller
    {
        private readonly IFormerMinistriesPageInfoRepository _formerMinistriesPageInfoRepository;
        private readonly IMinistryTimeLineRepository _ministryTimeLineRepository;
        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;
        private readonly IFileService _fileService;
        private readonly IFormerMinistriesPageInfoVersionRepository _formerMinistriesPageInfoVersionRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        private readonly IMinistryTimeLineVersionsRepository _ministryTimeLineVersionsRepository;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<FormerMinistriesController> _eventLogger;
        private readonly IGlobalElasticSearchService _globalElasticSearchService;
        private readonly IPageRouteRepository _pageRouteRepository;

        public FormerMinistriesController(IPageRouteVersionRepository pageRouteVersionRepository, IFormerMinistriesPageInfoRepository formerMinistriesPageInfoRepository, IMinistryTimeLineRepository ministryTimeLineRepository, IFileService fileService, IFormerMinistriesPageInfoVersionRepository formerMinistriesPageInfoVersionRepository, IConfiguration configuration, UserManager<ApplicationUser> userManager, IApprovalNotificationsRepository approvalNotificationsRepository, IMinistryTimeLineVersionsRepository ministryTimeLineVersionsRepository, IToastNotification toastNotification, IEventLogger<FormerMinistriesController> eventLogger, IGlobalElasticSearchService globalElasticSearchService, IPageRouteRepository pageRouteRepository)
        {
            _formerMinistriesPageInfoRepository = formerMinistriesPageInfoRepository;
            _ministryTimeLineRepository = ministryTimeLineRepository;
            _pageRouteVersionRepository = pageRouteVersionRepository;
            _fileService = fileService;
            _formerMinistriesPageInfoVersionRepository = formerMinistriesPageInfoVersionRepository;
            _configuration = configuration;
            _userManager = userManager;
            _approvalNotificationsRepository = approvalNotificationsRepository;
            _ministryTimeLineVersionsRepository = ministryTimeLineVersionsRepository;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _globalElasticSearchService = globalElasticSearchService;
            _pageRouteRepository = pageRouteRepository;
        }
        /// <summary>
        /// get FormerMinistries index page
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <param name="approvalId">approve notification id</param>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.FormerMinistries)]
        public async Task<IActionResult> Index(int id, [FromQuery] int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var pageInfo = _formerMinistriesPageInfoVersionRepository.Get(user.Id);
            ViewBag.ApprovalId = approvalId;
            ViewBag.pageRouteVersionId = id;

            return View(pageInfo);
        }
        /// <summary>
        /// submit changes for the approval user 
        /// </summary>
        /// <param name="pageRouteVersionId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete }, StaticPagesIdsConst.FormerMinistries)]
        public async Task<IActionResult> SubmitChanges([FromQuery] int pageRouteVersionId)
        {

            await SaveAndSubmitMethod(ChangeActionEnum.Update, pageRouteVersionId);

            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// save and submit changes for the approval user 
        /// </summary>
        /// <param name="changeActionEnum"></param>
        /// <param name="relatedId">related object id in notification</param>
        /// <returns></returns>
        private async Task SaveAndSubmitMethod(ChangeActionEnum changeActionEnum, int relatedId)
        {
            var pageRouteVer = _pageRouteVersionRepository.Get(relatedId);
            var frv = _formerMinistriesPageInfoVersionRepository.Get();
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (frv.VersionStatusEnum != VersionStatusEnum.Submitted)
            {
                frv.VersionStatusEnum = VersionStatusEnum.Submitted;
                pageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Submitted;
                _pageRouteVersionRepository.Update(pageRouteVer);
                _formerMinistriesPageInfoVersionRepository.Update(frv);
                var ministries = _ministryTimeLineVersionsRepository.GetAllByPageInfo(frv);
                SubmitMethod(user, changeActionEnum, relatedId);

            }
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Static Page > Former Ministries", "Submitted ");
            if (!frv.MinistryTimeLineVersions.Any())
            {
                CopyMinistries(frv);
            }



        }
        /// <summary>
        /// submit changes for the approval user 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="changeActionEnum"></param>
        /// <param name="relatedId">related object id in notification</param>
        [NonAction]
        private void SubmitMethod(ApplicationUser user, ChangeActionEnum changeActionEnum, int relatedId)
        {
            ApprovalNotification approval = new ApprovalNotification()
            {
                ChangeAction = changeActionEnum,
                VersionStatusEnum = VersionStatusEnum.Submitted,
                ChangesDateTime = DateTime.Now,
                ChangeType = ChangeType.PageContent,
                PageLink = $"/{nameof(FormerMinistriesController)[0..^10]}",
                PageName = PagesNamesConst.FormerMinistries,
                PageType = PageType.Static,
                ContentManagerId = user.Id,
                RelatedVersionId = relatedId,
                RelatedPageEnum = RelatedPageEnum.PageRouteVersion
            };
            _approvalNotificationsRepository.Add(approval);
        }
        /// <summary>
        /// approve changes,change status to approved  and copy it to the non version 
        /// </summary>
        /// <param name="approvalId">approve notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanApprove }, StaticPagesIdsConst.FormerMinistries)]
        public async Task<IActionResult> Approve([FromQuery] int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var frv = _formerMinistriesPageInfoVersionRepository.Get("", false);

            var ministries = _ministryTimeLineVersionsRepository.GetAllByPageInfo(frv);

            frv.VersionStatusEnum = VersionStatusEnum.Approved;


            _ministryTimeLineRepository.DeleteAll();
            foreach (var m in ministries)
            {
                var ministryTimeLine = new MinistryTimeLine();
                ministryTimeLine.CreationDate = DateTime.Now;
                ministryTimeLine.ApprovalDate = DateTime.Now;
                ministryTimeLine.ApprovedById = user.Id;
                ministryTimeLine.CreatedById = user.Id;
                ministryTimeLine.ArDescription = m.ArDescription;
                ministryTimeLine.ArName = m.ArName;
                ministryTimeLine.Email = m.Email;
                ministryTimeLine.EnDescription = m.EnDescription;
                ministryTimeLine.EnName = m.EnName;
                ministryTimeLine.Facebook = m.Facebook;
                ministryTimeLine.IsActive = m.IsActive;
                ministryTimeLine.IsDeleted = m.IsDeleted;
                ministryTimeLine.Order = m.Order;
                ministryTimeLine.PeriodAr = m.PeriodAr;
                ministryTimeLine.PeriodEn = m.PeriodEn;
                ministryTimeLine.ProfileImageUrl = m.ProfileImageUrl;
                ministryTimeLine.Twitter = m.Twitter;
                _ministryTimeLineRepository.Add(ministryTimeLine);

                m.VersionStatusEnum = VersionStatusEnum.Approved;
                m.MinistryTimeLineId = ministryTimeLine.Id;
                m.FormerMinistriesPageInfoVersionsId = frv.Id;

                _ministryTimeLineVersionsRepository.Update(m);
            }

            var approvalItem = _approvalNotificationsRepository.GetById(approvalId);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            if (frv.ChangeActionEnum == ChangeActionEnum.New)
            {
                var fr = new FormerMinistriesPageInfo()
                {
                    ApprovalDate = DateTime.Now,
                    ApprovedById = user.Id,
                    IsActive = true,
                    IsDeleted = false,
                    ModificationDate = DateTime.Now,
                    ModifiedById = user.Id,
                    Title1Ar = frv.Title1Ar,
                    Title1En = frv.Title1En,
                    Title2Ar = frv.Title2Ar,
                    Title2En = frv.Title2En,
                    DescriptionAr = frv.DescriptionAr,
                    DescriptionEn = frv.DescriptionEn,
                    CreationDate = DateTime.Now,
                    CreatedById = user.Id,

                };
                _formerMinistriesPageInfoRepository.Add(fr);
                frv.FormerMinistriesPageInfoId = fr.Id;
            }
            else if (frv.ChangeActionEnum == ChangeActionEnum.Update)
            {
                var fr = _formerMinistriesPageInfoRepository.Get();
                if (fr != null)
                {
                    fr.ApprovalDate = DateTime.Now;
                    fr.ApprovedById = user.Id;
                    fr.IsActive = true;
                    fr.IsDeleted = false;
                    fr.ModificationDate = DateTime.Now;
                    fr.ModifiedById = user.Id;
                    fr.Title1Ar = frv.Title1Ar;
                    fr.Title1En = frv.Title1En;
                    fr.Title2Ar = frv.Title2Ar;
                    fr.Title2En = frv.Title2En;
                    fr.DescriptionAr = frv.DescriptionAr;
                    fr.DescriptionEn = frv.DescriptionEn;
                    fr.CreationDate = DateTime.Now;
                    fr.CreatedById = user.Id;
                    _formerMinistriesPageInfoRepository.Update(fr);
                    frv.FormerMinistriesPageInfoId = fr.Id;
                }
                else
                {
                    var newFr = new FormerMinistriesPageInfo();
                    newFr.ApprovalDate = DateTime.Now;
                    newFr.ApprovedById = user.Id;
                    newFr.IsActive = true;
                    newFr.IsDeleted = false;
                    newFr.ModificationDate = DateTime.Now;
                    newFr.ModifiedById = user.Id;
                    newFr.Title1Ar = frv.Title1Ar;
                    newFr.Title1En = frv.Title1En;
                    newFr.Title2Ar = frv.Title2Ar;
                    newFr.Title2En = frv.Title2En;
                    newFr.DescriptionAr = frv.DescriptionAr;
                    newFr.DescriptionEn = frv.DescriptionEn;
                    newFr.CreationDate = DateTime.Now;
                    newFr.CreatedById = user.Id;
                    _formerMinistriesPageInfoRepository.Add(newFr);
                    frv.FormerMinistriesPageInfoId = newFr.Id;

                }
            }

            _formerMinistriesPageInfoVersionRepository.Update(frv);

            var prv = _pageRouteVersionRepository.Get((int)approvalItem.RelatedVersionId);
            prv.ContentVersionStatusEnum = VersionStatusEnum.Approved;
            _pageRouteVersionRepository.Update(prv);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Static Page > Former Ministries", "Approved ");
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
        /// Ignore changes and change status to ignored
        /// </summary>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanApprove }, StaticPagesIdsConst.FormerMinistries)]
        public async Task<IActionResult> Ignore([FromQuery] int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var frv = _formerMinistriesPageInfoVersionRepository.Get();
            frv.VersionStatusEnum = VersionStatusEnum.Ignored;
            frv.ModificationDate = DateTime.Now;
            _formerMinistriesPageInfoVersionRepository.Update(frv);


            var approvalItem = _approvalNotificationsRepository.GetById(approvalId);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            var prv = _pageRouteVersionRepository.Get((int)approvalItem.RelatedVersionId);
            prv.ContentVersionStatusEnum = VersionStatusEnum.Approved;
            _pageRouteVersionRepository.Update(prv);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Static Page > Former Ministries", " Reject");
            return RedirectToAction("Index", "ApprovalNotifications");
        }

        #region PageInfo
        /// <summary>
        /// get edit page info page
        /// </summary>
        /// <param name="pageRouteVersionId"></param>
        /// <param name="approvalId">approve notification id</param>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.FormerMinistries)]
        public IActionResult EditPageInfo([FromQuery] int pageRouteVersionId, [FromQuery] int approvalId)
        {
            var notification = _approvalNotificationsRepository.GetByPageNameAndChangeType(PagesNamesConst.FormerMinistries);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            var pageInfo = _formerMinistriesPageInfoVersionRepository.Get();
            ViewBag.pageRouteVersionId = pageRouteVersionId;
            return View(pageInfo);
        }
        /// <summary>
        /// edit page info 
        /// </summary>
        /// <param name="pageRouteVersionId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.FormerMinistries)]
        public async Task<IActionResult> EditPageInfo(int pageRouteVersionId, FormerMinistriesPageInfoVersions model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return EditPageInfoMethod(model, VersionStatusEnum.Draft, model.VersionStatusEnum ?? VersionStatusEnum.Draft, ChangeActionEnum.Update, user.Id, out int formerMinistriesPageInfoId, pageRouteVersionId);
        }
        /// <summary>
        /// edit page info 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="newVersionStatusEnum"></param>
        /// <param name="oldVersionStatusEnum"></param>
        /// <param name="changeActionEnum"></param>
        /// <param name="userId"></param>
        /// <param name="formerMinistriesPageInfoId"></param>
        /// <param name="pageRouteVersionId"></param>
        /// <returns></returns>
        private IActionResult EditPageInfoMethod(FormerMinistriesPageInfoVersions model, VersionStatusEnum newVersionStatusEnum, VersionStatusEnum oldVersionStatusEnum, ChangeActionEnum changeActionEnum, string userId, out int formerMinistriesPageInfoId, int pageRouteVersionId = 0)
        {
            var pageRouteVer = _pageRouteVersionRepository.Get(pageRouteVersionId);
            model.ModificationDate = DateTime.Today;
            model.DescriptionAr.ValidateHtml("DescriptionAr", ModelState);
            model.DescriptionEn.ValidateHtml("DescriptionEn", ModelState);
            formerMinistriesPageInfoId = 0;

            if (ModelState.IsValid)
            {
                //create new if olde version was approved or ignored
                if (oldVersionStatusEnum == VersionStatusEnum.Approved || oldVersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    model.CreatedById = userId;
                    model.CreationDate = DateTime.Now;
                    model.VersionStatusEnum = newVersionStatusEnum;
                    pageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                    model.ChangeActionEnum = changeActionEnum;
                    model.Id = 0;
                    _formerMinistriesPageInfoVersionRepository.Add(model);
                    formerMinistriesPageInfoId = model.Id;

                    CopyMinistries(model);
                    _pageRouteVersionRepository.Update(pageRouteVer);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Static Page > Former Ministries > Ministries > Add", model.Title1En + " " + model.Title2En);

                    return RedirectToAction(nameof(Index), new { id = pageRouteVer.Id });
                }
                _pageRouteVersionRepository.Update(pageRouteVer);
                var isUpdated = _formerMinistriesPageInfoVersionRepository.Update(model);
                if (isUpdated)
                {
                    formerMinistriesPageInfoId = model.Id;
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Static Page > Former Ministries > Ministries > Update", model.Title1En + " " + model.Title2En);
                    return RedirectToAction(nameof(Index), new { id = pageRouteVer.Id });
                }
            }
            return View(model);
        }

        #endregion

        #region Ministries
        /// <summary>
        /// get page ministries
        /// </summary>
        /// <param name="pageRouteVersionId"></param>
        /// <param name="approvalId">approve notification id</param>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.FormerMinistries)]
        public IActionResult Ministries([FromQuery] int pageRouteVersionId, [FromQuery] int approvalId)
        {
            var notification = _approvalNotificationsRepository.GetByPageNameAndChangeType(PagesNamesConst.FormerMinistries);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            ViewBag.pageRouteVersionId = pageRouteVersionId;
            ViewBag.approvalId = approvalId;
            return View();
        }
        /// <summary>
        /// get all ministries
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.FormerMinistries)]
        public JsonResult GetAllMinistries()
        {
            var pageInfo = _formerMinistriesPageInfoVersionRepository.Get();
            var ministries = _ministryTimeLineVersionsRepository.GetAllByPageInfo(pageInfo);

            return Json(new { data = ministries });
        }
        /// <summary>
        /// get create new ministry page
        /// </summary>
        /// <param name="pageRouteVersionId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd }, StaticPagesIdsConst.FormerMinistries)]
        public IActionResult CreateMinistr([FromQuery] int pageRouteVersionId)
        {
            var notification = _approvalNotificationsRepository.GetByPageNameAndChangeType(PagesNamesConst.FormerMinistries);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            ViewBag.pageRouteVersionId = pageRouteVersionId;
            return View();
        }
        /// <summary>
        /// create new ministry 
        /// </summary>
        /// <param name="pageRouteVersionId"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd }, StaticPagesIdsConst.FormerMinistries)]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> CreateMinistr(int pageRouteVersionId, MinistriesViewModel viewModel)
        {
            var pageRouteVer = _pageRouteVersionRepository.Get(pageRouteVersionId);
            viewModel.ArDescription.ValidateHtml("ArDescription", ModelState);
            viewModel.EnDescription.ValidateHtml("EnDescription", ModelState);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var pageInfo = _formerMinistriesPageInfoVersionRepository.Get();
            if (ModelState.IsValid)
            {
                //if page info was approved or ignored then create new one and copy ministies to it
                if (pageInfo.VersionStatusEnum == VersionStatusEnum.Approved || pageInfo.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    pageInfo.Id = 0;
                    pageInfo.CreationDate = DateTime.Now;
                    pageInfo.ModificationDate = DateTime.Now;
                    pageInfo.ApprovedById = user.Id;
                    pageInfo.ModifiedById = user.Id;
                    pageInfo.VersionStatusEnum = VersionStatusEnum.Draft;
                    pageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                    pageInfo.ChangeActionEnum = ChangeActionEnum.Update;

                    _formerMinistriesPageInfoVersionRepository.Add(pageInfo);
                    _pageRouteVersionRepository.Update(pageRouteVer);
                    CopyMinistries(pageInfo);
                }
                var ministrModel = viewModel.MapToMinistryTimeLineVersionModel();
                ministrModel.StatusId = 1;
                ministrModel.ChangeActionEnum = ChangeActionEnum.New;
                ministrModel.VersionStatusEnum = VersionStatusEnum.Draft;
                pageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                ministrModel.CreationDate = DateTime.Today;
                ministrModel.CreatedById = user.Id;
                ministrModel.IsActive = true;
                ministrModel.FormerMinistriesPageInfoVersionsId = pageInfo.Id;
                if (viewModel.ImageFile != null)
                    ministrModel.ProfileImageUrl = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                _ministryTimeLineVersionsRepository.Add(ministrModel);
                _pageRouteVersionRepository.Update(pageRouteVer);

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Static Page > Former Ministries > Ministries > Add", ministrModel.EnName);

                return RedirectToAction(nameof(Ministries), new { pageRouteVersionId = pageRouteVer.Id });
            }
            return View(viewModel);
        }
        /// <summary>
        /// copy ministries to page ingo
        /// </summary>
        /// <param name="pageInfo"></param>
        private void CopyMinistries(FormerMinistriesPageInfoVersions pageInfo)
        {
            var mtl = _ministryTimeLineRepository.GetAll();
            _ministryTimeLineVersionsRepository.MarkAllAsDeleted();
            foreach (var x in mtl)
            {
                _ministryTimeLineVersionsRepository.Add(new MinistryTimeLineVersions()
                {
                    FormerMinistriesPageInfoVersionsId = pageInfo.Id,
                    MinistryTimeLineId = x.Id,
                    ArName = x.ArName,
                    EnName = x.EnName,
                    VersionStatusEnum = VersionStatusEnum.Approved,
                    IsActive = x.IsActive,
                    Order = x.Order,
                    EnDescription = x.EnDescription,
                    ArDescription = x.ArDescription,
                    ProfileImageUrl = x.ProfileImageUrl,
                    IsDeleted = x.IsDeleted,
                    EventSocialLinks = x.EventSocialLinks,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    PeriodAr = x.PeriodAr,
                    Facebook = x.Facebook,
                    Twitter = x.Twitter,
                    Email = x.Email,
                    PeriodEn = x.PeriodEn,
                    StatusId = x.StatusId,
                    ChangeActionEnum = ChangeActionEnum.Update,
                    CreationDate = x.CreationDate,
                    CreatedById = x.CreatedById,
                    ApprovedById = x.ApprovedById,
                    ApprovalDate = x.ApprovalDate,
                });
            }
        }
        /// <summary>
        /// get details Ministr page
        /// </summary>
        /// <param name="id">ministryTimeLine id</param>
        /// <param name="pageRouteVersionId"></param>
        /// <param name="approvalId">approve notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.FormerMinistries)]
        [Authorize]
        public IActionResult DetailsMinistr(int id, [FromQuery] int pageRouteVersionId, [FromQuery] int approvalId)
        {
            ViewBag.pageRouteVersionId = pageRouteVersionId;
            ViewBag.approvalId = approvalId;
            return DetailsMinisterMethod(id, pageRouteVersionId);
        }
        /// <summary>
        /// get edit Ministr page
        /// </summary>
        /// <param name="id">ministryTimeLine id</param>
        /// <param name="pageRouteVersionId"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.FormerMinistries)]
        public IActionResult EditMinistr(int id, [FromQuery] int pageRouteVersionId)
        {
            var notification = _approvalNotificationsRepository.GetByPageNameAndChangeType(PagesNamesConst.FormerMinistries);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            ViewBag.pageRouteVersionId = pageRouteVersionId;
            return DetailsMinisterMethod(id, pageRouteVersionId);
        }
        /// <summary>
        /// get details Ministr
        /// </summary>
        /// <param name="id">ministryTimeLine id</param>
        /// <param name="pageRouteVersionId"></param>
        /// <returns></returns>
        private IActionResult DetailsMinisterMethod(int id, int pageRouteVersionId)
        {
            var pageInfo = _formerMinistriesPageInfoVersionRepository.Get("", false);

            if (pageInfo.VersionStatusEnum == VersionStatusEnum.Approved || pageInfo.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                var ministerTl = _ministryTimeLineRepository.GetDetail(id);

                return View(ministerTl.MapToMinistrViewModel());
            }
            var minister = _ministryTimeLineVersionsRepository.GetDetail(id);
            ViewBag.pageRouteVersionId = pageRouteVersionId;


            return View(minister.MapToMinistrViewModel());
        }
        /// <summary>
        /// edit Ministr 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="pageRouteVersionId"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.FormerMinistries)]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> EditMinistr(MinistriesViewModel viewModel, int pageRouteVersionId)
        {
            return await EditMinistrMethod(viewModel, pageRouteVersionId);
        }
        /// <summary>
        /// edit Ministr 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="pageRouteVersionId"></param>
        /// <returns></returns>
        private async Task<IActionResult> EditMinistrMethod(MinistriesViewModel viewModel, int pageRouteVersionId = 0)
        {
            var pageRouteVer = _pageRouteVersionRepository.Get(pageRouteVersionId);
            viewModel.ArDescription.ValidateHtml("ArDescription", ModelState);
            viewModel.EnDescription.ValidateHtml("EnDescription", ModelState);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                try
                {


                    var pageInfo = _formerMinistriesPageInfoVersionRepository.Get("", false);
                    //create new if olde version was approved or ignored
                    if (pageInfo.VersionStatusEnum == VersionStatusEnum.Approved || pageInfo.VersionStatusEnum == VersionStatusEnum.Ignored)
                    {
                        pageInfo.Id = 0;
                        pageInfo.CreationDate = DateTime.Now;
                        pageInfo.ModificationDate = DateTime.Now;
                        pageInfo.ApprovedById = user.Id;
                        pageInfo.ModifiedById = user.Id;
                        pageInfo.VersionStatusEnum = VersionStatusEnum.Draft;
                        pageInfo.ChangeActionEnum = ChangeActionEnum.Update;


                        _formerMinistriesPageInfoVersionRepository.Add(pageInfo);
                        CopyMinistries(pageInfo);

                        var savedMinister = _ministryTimeLineVersionsRepository.GetByCondition(x => x.MinistryTimeLineId == viewModel.Id);

                        viewModel.Id = savedMinister.Id;
                        viewModel.FormerMinistriesPageInfoVersionsId = pageInfo.Id;
                        viewModel.CreatedById = user.Id;

                        var ministrModel = viewModel.MapToMinistryTimeLineVersionModel();
                        ministrModel.StatusId = 1;
                        if (viewModel.ImageFile != null)
                            ministrModel.ProfileImageUrl = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                        else
                            ministrModel.ProfileImageUrl = viewModel.ImageURL;
                        ministrModel.CreationDate = DateTime.Now;
                        if (pageRouteVer != null)
                        {
                            pageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                            _pageRouteVersionRepository.Update(pageRouteVer);
                        }
                        _ministryTimeLineVersionsRepository.Update(ministrModel);

                        _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Static Page > Former Ministries > Ministries > Update", ministrModel.EnName);

                        return RedirectToAction(nameof(Ministries), new { pageRouteVersionId = pageRouteVer.Id });
                    }
                    else
                    {
                        var ministrModel = viewModel.MapToMinistryTimeLineVersionModel();
                        ministrModel.StatusId = 1;
                        if (viewModel.ImageFile != null)
                            ministrModel.ProfileImageUrl = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                        else
                            ministrModel.ProfileImageUrl = viewModel.ImageURL;
                        ministrModel.CreationDate = DateTime.Now;
                        if (pageRouteVer != null)
                        {
                            pageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                            _pageRouteVersionRepository.Update(pageRouteVer);
                        }
                        _ministryTimeLineVersionsRepository.Update(ministrModel);
                        _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Static Page > Former Ministries > Ministries > Update", ministrModel.EnName);
                    }


                    return RedirectToAction(nameof(Ministries), new { pageRouteVersionId = pageRouteVer == null ? pageRouteVer.Id : 0 });
                }
                catch (Exception ex)
                {

                }
            }
            return View(viewModel);
        }
        /// <summary>
        /// change active ministr
        /// </summary>
        /// <param name="id"></param>
        /// <param name="active"></param>
        /// <returns>true if the change occurred successfully false otherwise</returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.FormerMinistries)]
        public async Task<bool> ChangeActiveMinistr(int id, bool active)
        {
            try
            {
                var pageInfo = _formerMinistriesPageInfoVersionRepository.Get("", false);
                if (pageInfo.VersionStatusEnum == VersionStatusEnum.Approved || pageInfo.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    var savedMinister = _ministryTimeLineRepository.GetDetailWithNoTarcking(id);
                    savedMinister.IsActive = active;
                    await EditMinistrMethod(savedMinister.MapToMinistrViewModel());
                }
                else
                {
                    var ministr = _ministryTimeLineVersionsRepository.GetDetailWithNoTarcking(id);
                    ministr.IsActive = active;
                    await EditMinistrMethod(ministr.MapToMinistrViewModel());
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// delete ministr
        /// </summary>
        /// <param name="id">ministryTimeLine id</param>
        /// <param name="pageRouteVersionId"></param>
        /// <returns>true if the delete occurred successfully false otherwise</returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanDelete }, StaticPagesIdsConst.FormerMinistries)]
        public async Task<bool> DeleteMinistr(int id, int pageRouteVersionId)
        {
            try
            {
                var pageRouteVer = _pageRouteVersionRepository.Get(pageRouteVersionId);
                var pageInfo = _formerMinistriesPageInfoVersionRepository.Get("", false);
                if (pageInfo.VersionStatusEnum == VersionStatusEnum.Approved || pageInfo.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    var savedMinister = _ministryTimeLineRepository.GetDetailWithNoTarcking(id);
                    savedMinister.IsDeleted = true;
                    await EditMinistrMethod(savedMinister.MapToMinistrViewModel(), pageRouteVersionId);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Static Page > Former Ministries > Ministries > Delete", savedMinister.EnName);
                }
                else
                {
                    var ministr = _ministryTimeLineVersionsRepository.GetDetailWithNoTarcking(id);
                    ministr.IsDeleted = true;
                    await EditMinistrMethod(ministr.MapToMinistrViewModel(), pageRouteVersionId);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Static Page > Former Ministries > Ministries > Delete", ministr.EnName);
                }

                pageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                _pageRouteVersionRepository.Update(pageRouteVer);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion


    }
}