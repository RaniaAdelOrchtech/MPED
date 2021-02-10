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
using Microsoft.AspNetCore.Mvc.RazorPages;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class EconomicIndicatorsController : Controller
    {
        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;
        private readonly IEconomicIndicatorRepository _economicIndicatorRepository;
        private readonly IEconomicIndicatorVersionsRepository _economicIndicatorVersionsRepository;
        private readonly IFileService _fileService;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<HP_EconomicDevelopmentController> _eventLogger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        private readonly IGlobalElasticSearchService _globalElasticSearchService;
        private readonly IPageRouteRepository _pageRouteRepository;

        public EconomicIndicatorsController(IPageRouteVersionRepository pageRouteVersionRepository, IEconomicIndicatorRepository economicIndicatorRepository, IEconomicIndicatorVersionsRepository economicIndicatorVersionsRepository,
            IFileService fileService, IToastNotification toastNotification, IEventLogger<HP_EconomicDevelopmentController> eventLogger, UserManager<ApplicationUser> userManager, IApprovalNotificationsRepository approvalNotificationsRepository, IGlobalElasticSearchService globalElasticSearchService, IPageRouteRepository pageRouteRepository)
        {
            _pageRouteVersionRepository = pageRouteVersionRepository;
            _economicIndicatorRepository = economicIndicatorRepository;
            _economicIndicatorVersionsRepository = economicIndicatorVersionsRepository;
            _fileService = fileService;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _userManager = userManager;
            _approvalNotificationsRepository = approvalNotificationsRepository;
            _globalElasticSearchService = globalElasticSearchService;
            _pageRouteRepository = pageRouteRepository;
        }

        /// <summary>
        /// index for griding all submitted economic indicators objects
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.EconomicIndicators)]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get method for edit economic indicator object
        /// </summary>
        /// <param name="id">economic indicator id</param>
        /// <param name="order">type of economic indicator</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.EconomicIndicators)]
        public IActionResult Edit(int id, [FromQuery] int order)
        {
            var notification = _approvalNotificationsRepository.GetByPageNameAndChangeType(PagesNamesConst.EconomicIndicators);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            if (order <= 0 && order >= 3)
                return NotFound();
            var eiv = _economicIndicatorVersionsRepository.GetByEcoIndiId(id);
            if (eiv == null || eiv.VersionStatusEnum == VersionStatusEnum.Approved || eiv.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                eiv = _economicIndicatorRepository.GetByEcoIndiId(id);
            }
            var mapped = eiv.MapToEcoIndiVersionViewModel();
            ViewBag.order = order;
            mapped.order = order;
            return View(mapped);
        }

        /// <summary>
        /// Post method for edit economic indicator object
        /// </summary>
        /// <param name="viewModel">economic indicator data</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.EconomicIndicators)]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Edit(EconomicIndicatorViewModel viewModel)
        {
            var pageRouteVersion = _pageRouteVersionRepository.GetPageRouteVersionByPageType(PageTypeConsts.EcnomicIndicators);

            viewModel.ImageDiscriptionAr1.ValidateHtml("ImageDiscriptionAr1", ModelState);
            viewModel.ImageDiscriptionAr2.ValidateHtml("ImageDiscriptionAr2", ModelState);
            viewModel.ImageDiscriptionAr3.ValidateHtml("ImageDiscriptionAr3", ModelState);

            ModelState.Remove(nameof(viewModel.ImageFile));

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var eiv = _economicIndicatorVersionsRepository.GetByEcoIndiId(viewModel.Id);
                var ecoIndiVersion = viewModel.MapToEcoIndiVersionModel();

                if (eiv == null || viewModel.VersionStatusEnum == VersionStatusEnum.Approved || viewModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    ecoIndiVersion.CreatedById = user.Id;
                    ecoIndiVersion.CreationDate = DateTime.Now;
                    ecoIndiVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                    ecoIndiVersion.ChangeActionEnum = ChangeActionEnum.Update;
                    ecoIndiVersion.Id = 0;
                    ecoIndiVersion.EconomicIndicatorsId = viewModel.Id;
                    pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                    if (viewModel.ImageFile != null)
                    {
                        if (viewModel.order == 1)
                            ecoIndiVersion.ImageUrl1 = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                        if (viewModel.order == 2)
                            ecoIndiVersion.ImageUrl2 = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                        if (viewModel.order == 3)
                            ecoIndiVersion.ImageUrl3 = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                    }
                    _economicIndicatorVersionsRepository.Add(ecoIndiVersion);
                    _pageRouteVersionRepository.Update(pageRouteVersion);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Economic Indicators Static Page > Update", null);
                    return RedirectToAction(nameof(Index));
                }

                if (viewModel.ImageFile != null)
                {
                    if (viewModel.order == 1)
                        ecoIndiVersion.ImageUrl1 = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                    if (viewModel.order == 2)
                        ecoIndiVersion.ImageUrl2 = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                    if (viewModel.order == 3)
                        ecoIndiVersion.ImageUrl3 = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                }

                ecoIndiVersion.CreationDate = DateTime.Now;
                ecoIndiVersion.Id = eiv.Id;
                ecoIndiVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                ecoIndiVersion.ChangeActionEnum = ChangeActionEnum.Update;
                ecoIndiVersion.ApprovalDate = eiv.ApprovalDate;
                ecoIndiVersion.ApprovedById = eiv.ApprovedById;
                ecoIndiVersion.CreatedById = eiv.CreatedById;
                ecoIndiVersion.CreationDate = eiv.CreationDate;
                ecoIndiVersion.ModificationDate = eiv.ModificationDate;
                ecoIndiVersion.ModifiedById = eiv.ModifiedById;
                ecoIndiVersion.EconomicIndicatorsId = eiv.EconomicIndicatorsId;
                pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Draft;

                var update = _economicIndicatorVersionsRepository.Update(ecoIndiVersion);

                if (update)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _pageRouteVersionRepository.Update(pageRouteVersion);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Economic Indicators Static Page > Update", null);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    ViewBag.order = viewModel.order;
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Economic Indicators Static Page > Update", null);
                    return View(viewModel);
                }
            }
            ViewBag.order = viewModel.order;
            return View(viewModel);
        }

        /// <summary>
        /// submit changes for approval user to get last edits
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete }, StaticPagesIdsConst.EconomicIndicators)]
        public async Task<IActionResult> SubmitChanges()
        {
            var pageRouteVersion = _pageRouteVersionRepository.GetPageRouteVersionByPageType(PageTypeConsts.EcnomicIndicators);

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var eiv = _economicIndicatorVersionsRepository.GetAllDrafts();
            foreach (var record in eiv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                record.ModifiedById = user.Id;
                record.ModificationDate = DateTime.Now;
                _economicIndicatorVersionsRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageNameAndChangeType(PagesNamesConst.EconomicIndicators);
            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(EconomicIndicatorsController)[0..^10]}",
                    PageName = PagesNamesConst.EconomicIndicators,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);
            }

            pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Submitted;
            _pageRouteVersionRepository.Update(pageRouteVersion);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Economic Indicators > Submitted", " Submitted");
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Approve method for approve last changes
        /// </summary>
        /// <param name="id">economic indicator id</param>
        /// <param name="approvalId">notification approval id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanApprove }, StaticPagesIdsConst.EconomicIndicators)]
        public async Task<IActionResult> Approve(int id, [FromQuery] int approvalId)
        {
            var pageRouteVersion = _pageRouteVersionRepository.GetPageRouteVersionByPageType(PageTypeConsts.EcnomicIndicators);

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var eiv = _economicIndicatorVersionsRepository.GetAllSubmitted();

            foreach (var record in eiv)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;
                _economicIndicatorVersionsRepository.Update(record);

                var ecoObj = new EconomicIndicators()
                {
                    Id = record.EconomicIndicatorsId ?? 0,
                    ApprovalDate = record.ApprovalDate,
                    ApprovedById = record.ApprovedById,
                    CreatedById = record.CreatedById,
                    CreationDate = record.CreationDate,
                    ModificationDate = record.ModificationDate,
                    ModifiedById = record.ModifiedById,
                    ImageDiscriptionAr1 = record.ImageDiscriptionAr1,
                    ImageDiscriptionAr2 = record.ImageDiscriptionAr2,
                    ImageDiscriptionAr3 = record.ImageDiscriptionAr3,
                    ImageTitleAr1 = record.ImageTitleAr1,
                    ImageTitleAr2 = record.ImageTitleAr2,
                    ImageTitleAr3 = record.ImageTitleAr3,
                    MainDiscriptionAr = record.MainDiscriptionAr,
                    ImageDiscriptionEn1 = record.ImageDiscriptionEn1,
                    ImageDiscriptionEn2 = record.ImageDiscriptionEn2,
                    ImageDiscriptionEn3 = record.ImageDiscriptionEn3,
                    ImageTitleEn1 = record.ImageTitleEn1,
                    ImageTitleEn2 = record.ImageTitleEn2,
                    ImageTitleEn3 = record.ImageTitleEn3,
                    MainDiscriptionEn = record.MainDiscriptionEn,
                    Link1 = record.Link1,
                    Link2 = record.Link2,
                    Link3 = record.Link3,
                    ImageUrl1 = record.ImageUrl1,
                    ImageUrl2 = record.ImageUrl2,
                    ImageUrl3 = record.ImageUrl3

                };
                _economicIndicatorRepository.Update(ecoObj);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.EconomicIndicators);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Approved;
            _pageRouteVersionRepository.Update(pageRouteVersion);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Economic Indicators > Approve", " Id :" + id);

            try
            {
                await _globalElasticSearchService.DeleteAsync(pageRouteVersion.PageRouteId ?? 0);
                await _globalElasticSearchService.AddAsync(_pageRouteRepository.GetPageData(pageRouteVersion.PageRouteId ?? 0));
            }
            catch { }

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Ignore method for approve last changes
        /// </summary>
        /// <param name="id">economic indicator id</param>
        /// <param name="approvalId">notification approval id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanApprove }, StaticPagesIdsConst.EconomicIndicators)]
        public async Task<IActionResult> Ignore(int id, [FromQuery] int approvalId)
        {
            var pageRouteVersion = _pageRouteVersionRepository.GetPageRouteVersionByPageType(PageTypeConsts.EcnomicIndicators);

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var eiv = _economicIndicatorVersionsRepository.GetAllSubmitted();

            foreach (var record in eiv)
            {
                record.ModificationDate = DateTime.Now;
                record.ModifiedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _economicIndicatorVersionsRepository.Update(record);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.EconomicIndicators);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Approved;
            _pageRouteVersionRepository.Update(pageRouteVersion);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Economic Indicator > Reject", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Get all economic indicators objects for index
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.EconomicIndicators)]
        public JsonResult GetAllEconomic()
        {
            var data = _economicIndicatorVersionsRepository.GetEcoIndiVersions().FirstOrDefault();

            List<HomeCommonViewModel> viewModel = new List<HomeCommonViewModel>();
            viewModel.Add(new HomeCommonViewModel()
            {
                Id = data.Id,
                ArTitle = "",
                EnTitle = "",
                ArDescription = data.MainDiscriptionAr,
                EnDescription = data.MainDiscriptionEn,
                Order = 0,
                Type = HomeTypeEnum.Title
            });
            viewModel.Add(new HomeCommonViewModel()
            {
                Id = data.Id,
                ArTitle = data.ImageTitleAr1,
                EnTitle = data.ImageTitleEn1,
                ArDescription = data.ImageDiscriptionAr1,
                EnDescription = data.ImageDiscriptionEn1,
                Order = 1,
                Type = HomeTypeEnum.Detail
            });
            viewModel.Add(new HomeCommonViewModel()
            {
                Id = data.Id,
                ArTitle = data.ImageTitleAr2,
                EnTitle = data.ImageTitleEn2,
                ArDescription = data.ImageDiscriptionAr2,
                EnDescription = data.ImageDiscriptionEn2,
                Order = 2,
                Type = HomeTypeEnum.Detail
            });
            viewModel.Add(new HomeCommonViewModel()
            {
                Id = data.Id,
                ArTitle = data.ImageTitleAr3,
                EnTitle = data.ImageTitleEn3,
                ArDescription = data.ImageDiscriptionAr3,
                EnDescription = data.ImageDiscriptionEn3,
                Order = 3,
                Type = HomeTypeEnum.Detail
            });

            return Json(new { data = viewModel.OrderBy(x => x.Order) });
        }

    }
}