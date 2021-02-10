using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;
using MPMAR.Business;
using NUglify.Helpers;
using static MPMAR.Data.Enums.Enums;

using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MPMAR.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MPMAR.Common;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Data.Consts;
using MPMAR.Common.Interfaces;
using MPMAR.Web.Admin.AuthRequirement;
using MPMAR.Web.Admin.Services;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class DynamicPageRouteController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";
        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<DynamicPageRouteController> _eventLogger;
        private readonly INavItemRepository _navItemRepository;
        private readonly IPageSectionVersionRepository _IPageSectionVersionRepository;
        private readonly ISectionCardVersionRepository _ISectionCardVersionRepository;
        private readonly IWebHostEnvironment _IWebHostEnvironment;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        public IPageRouteVersionRepository _pageRouteVersionRepository { get; }
        private readonly HTMLFileHelper _htmlHelper;
        private readonly IGlobalElasticSearchService _globalElasticSearchService;
        private readonly IBEUsersPrivilegesService _iBEUsersPrivilegesService;

        public DynamicPageRouteController(IPageRouteRepository pageRouteRepository, IPageRouteVersionRepository pageRouteVersionRepository,
            UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<DynamicPageRouteController> eventLogger, INavItemRepository navItemRepository,
            IPageSectionVersionRepository PageSectionVersionRepository
            , ISectionCardVersionRepository SectionCardVersionRepository, IWebHostEnvironment WebHostEnvironment,
            IApprovalNotificationsRepository approvalNotificationsRepository, HTMLFileHelper htmlHelper, IGlobalElasticSearchService globalElasticSearchService, IBEUsersPrivilegesService iBEUsersPrivilegesService)

        {
            _pageRouteRepository = pageRouteRepository;
            _pageRouteVersionRepository = pageRouteVersionRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _navItemRepository = navItemRepository;

            _IPageSectionVersionRepository = PageSectionVersionRepository;
            _ISectionCardVersionRepository = SectionCardVersionRepository;
            _IWebHostEnvironment = WebHostEnvironment;
            _approvalNotificationsRepository = approvalNotificationsRepository;
            _htmlHelper = htmlHelper;
            _globalElasticSearchService = globalElasticSearchService;
            _iBEUsersPrivilegesService = iBEUsersPrivilegesService;
        }

        /// <summary>
        /// Index for grid all approved and not deleted dynamic page routes
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPage, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index()
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

            return View();
        }

        /// <summary>
        /// Get method for create a new dynamic page route
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPage, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create()
        {
            List<NavItem> navItems = _navItemRepository.Get().ToList();
            PageRouteCreateViewModel viewModel = new PageRouteCreateViewModel(navItems);
            return View(viewModel);
        }

        /// <summary>
        /// Post method for Create a new dynamic page route
        /// </summary>
        /// <param name="pageRouteViewModel">page route data</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPage, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public async Task<IActionResult> Create(PageRouteCreateViewModel pageRouteViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                PageRouteVersion pageRouteVersion = new PageRouteVersion();
                pageRouteVersion = pageRouteViewModel.MapToPageRouteVersion(pageRouteVersion);
                pageRouteVersion.CreatedById = user.Id;
                pageRouteVersion.CreationDate = DateTime.Now;

                pageRouteVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                pageRouteVersion.ChangeActionEnum = ChangeActionEnum.New;

                PageRouteVersion newPageRouteVersion = _pageRouteVersionRepository.Add(pageRouteVersion);
                if (newPageRouteVersion != null)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.AddSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Dynamic Pages ", pageRouteViewModel.EnName);
                    return RedirectToAction("Index");
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Dynamic Pages ", "Insertion Error");
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    return View();
                }
            }
            List<NavItem> navItems = _navItemRepository.Get().ToList();
            PageRouteCreateViewModel viewModel = new PageRouteCreateViewModel(navItems);

            return View(viewModel);
        }

        /// <summary>
        /// Get method for update a dynamic page route
        /// </summary>
        /// <param name="id">dynamic page route id</param>
        /// <param name="approvalId">approved notification id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPage, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public async Task<IActionResult> Edit(int id, [FromQuery] int approvalId)
        {
            return await GetDetails(id, approvalId);
        }
        /// <summary>
        /// get DP details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        private async Task<IActionResult> GetDetails(int id, int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var role = (await _userManager.GetRolesAsync(user).ConfigureAwait(false)).FirstOrDefault();

            PageRouteVersion pageRouteVersion = _pageRouteVersionRepository.GetWithNoTracking(id);
            if (pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Approved || pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                var pageRoute = _pageRouteRepository.Get(pageRouteVersion.PageRouteId ?? 0);
                if(pageRoute!=null)
                {
                    pageRouteVersion.ApprovalDate = pageRoute.ApprovalDate;
                    pageRouteVersion.ApprovedById = pageRoute.ApprovedById;
                    pageRouteVersion.ArName = pageRoute.ArName;
                    pageRouteVersion.ControllerName = pageRoute.ControllerName;
                    pageRouteVersion.CreatedById = pageRoute.CreatedById;
                    pageRouteVersion.CreationDate = pageRoute.CreationDate;
                    pageRouteVersion.EnName = pageRoute.EnName;
                    pageRouteVersion.HasNavItem = pageRoute.HasNavItem;
                    pageRouteVersion.IsActive = pageRoute.IsActive;
                    pageRouteVersion.IsDeleted = pageRoute.IsDeleted;
                    pageRouteVersion.IsDynamicPage = pageRoute.IsDynamicPage;
                    pageRouteVersion.NavItemId = pageRoute.NavItemId;
                    pageRouteVersion.Order = pageRoute.Order;
                    pageRouteVersion.PageFilePathAr = pageRoute.PageFilePathAr;
                    pageRouteVersion.PageFilePathEn = pageRoute.PageFilePathEn;
                    pageRouteVersion.SeoDescriptionAR = pageRoute.SeoDescriptionAR;
                    pageRouteVersion.SectionName = pageRoute.SectionName;
                    pageRouteVersion.SeoDescriptionEN = pageRoute.SeoDescriptionEN;
                    pageRouteVersion.SeoOgTitleAR = pageRoute.SeoOgTitleAR;
                    pageRouteVersion.SeoOgTitleEN = pageRoute.SeoOgTitleEN;
                    pageRouteVersion.SeoTitleAR = pageRoute.SeoTitleAR;
                    pageRouteVersion.SeoTitleEN = pageRoute.SeoTitleEN;
                    pageRouteVersion.SeoTwitterCardAR = pageRoute.SeoTwitterCardAR;
                    pageRouteVersion.SeoTwitterCardEN = pageRoute.SeoTwitterCardEN;
                }
            }
            var notification = _approvalNotificationsRepository.GetByRelatedIdAndType(pageRouteVersion.Id, ChangeType.BasicInfo);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            if (pageRouteVersion == null)
                return NotFound();

            List<NavItem> navItems = _navItemRepository.Get().ToList();
            PageRouteEditViewModel viewModel = new PageRouteEditViewModel(navItems);
            viewModel = pageRouteVersion.MapToPageRouteViewModel(viewModel);

            ViewBag.ApprovalId = approvalId;

            return View(viewModel);
        }

        /// <summary>
        /// Post method for update a dynamic page route
        /// </summary>
        /// <param name="pageRouteViewModel">dynamic page route new data</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPage, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public async Task<IActionResult> Edit(PageRouteEditViewModel pageRouteViewModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return EditMethod(pageRouteViewModel, VersionStatusEnum.Draft, pageRouteViewModel.VersionStatusEnum ?? VersionStatusEnum.Draft, ChangeActionEnum.Update, user.Id, out int pageRoutVersionId);


        }

        /// <summary>
        /// Core method for update dynamic page route
        /// </summary>
        /// <param name="pageRouteViewModel">dynamic page route new data</param>
        /// <param name="newVersionStatusEnum">object version status enum after editing</param>
        /// <param name="oldVersionStatusEnum">object version status enum before editing</param>
        /// <param name="changeActionEnum">object change action enum after editing</param>
        /// <param name="userId">logged in user id</param>
        /// <param name="pageRoutVersionId">page route version id</param>
        /// <returns></returns>
        [NonAction]
        private IActionResult EditMethod(PageRouteEditViewModel pageRouteViewModel, VersionStatusEnum newVersionStatusEnum, VersionStatusEnum oldVersionStatusEnum, ChangeActionEnum changeActionEnum, string userId, out int pageRoutVersionId, bool fromSaveAndSubmit = false)
        {

            List<NavItem> navItems;
            if (!pageRouteViewModel.HasNavItem)
            {
                ModelState.Remove("NavItemId");
            }
            PageRouteVersion pageRouteVersion = pageRouteViewModel.MapToPageRouteVersion();


            if (ModelState.IsValid)
            {
                if (changeActionEnum == ChangeActionEnum.Delete)
                {
                    pageRouteVersion.ChangeActionEnum = changeActionEnum;
                }
                if (oldVersionStatusEnum == VersionStatusEnum.Approved || oldVersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    pageRouteVersion.CreatedById = userId;
                    pageRouteVersion.CreationDate = DateTime.Now;
                    pageRouteVersion.VersionStatusEnum = newVersionStatusEnum;
                    pageRouteVersion.ChangeActionEnum = changeActionEnum;
                    pageRouteVersion.Id = 0;
                    _pageRouteVersionRepository.Add(pageRouteVersion);
                    _IPageSectionVersionRepository.CopyPageSectionVersions(pageRouteVersion);
                    pageRoutVersionId = pageRouteVersion.Id;
                    if (!fromSaveAndSubmit)
                        _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Dynamic Pages ", pageRouteVersion.EnName);

                    return RedirectToAction(nameof(Index));
                }
                PageRouteVersion newPageRouteVersion = _pageRouteVersionRepository.Update(pageRouteVersion);
                if (newPageRouteVersion != null)
                {
                    if (!fromSaveAndSubmit)
                        _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Dynamic Pages ", pageRouteVersion.EnName);
                    pageRoutVersionId = pageRouteVersion.Id;
                    return RedirectToAction("Index");
                }
                else
                {
                    _toastNotification.AddWarningToastMessage(ToasrMessages.warning);
                    navItems = _navItemRepository.Get().ToList();
                    pageRouteViewModel.NavItems = navItems;
                    pageRoutVersionId = pageRouteVersion.Id;
                    return View(pageRouteViewModel);
                }
            }
            navItems = _navItemRepository.Get().ToList();
            pageRouteViewModel.NavItems = navItems;
            pageRoutVersionId = pageRouteVersion.Id;
            return View(pageRouteViewModel);
        }

        /// <summary>
        /// Get method for details a dynamic page route object
        /// </summary>
        /// <param name="id">dunamic page route id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPage, new PrivilegesActions[] { PrivilegesActions.CanViewDP_BI })]
        public async Task<IActionResult> Details(int id, [FromQuery] int approvalId)
        {
            return await GetDetails(id, approvalId);
        }

        /// <summary>
        /// Delete a dynamic page route version by id
        /// </summary>
        /// <param name="id">dynamic page route version id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPage, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public async Task<IActionResult> Delete(int id)
        {
            PageRouteVersion pageRouteVersion = _pageRouteVersionRepository.GetWithNoTracking(id);

            if (pageRouteVersion != null)
            {
                TempData[notificationMessageKey] = ToasrMessages.DeleteSuccess;
                TempData[notificationTypeKey] = notificationSuccess;

                if (pageRouteVersion.ChangeActionEnum == ChangeActionEnum.New && pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Draft)
                {
                    _pageRouteVersionRepository.SoftDelete(id);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Dynamic Pages ", "Soft Delele " + pageRouteVersion.EnName);
                }
                else
                {

                    await SaveAndSubmitMethod(pageRouteVersion.MapToPageRouteViewModel(new PageRouteEditViewModel()), ChangeActionEnum.Delete);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Dynamic Pages ", "Delele " + pageRouteVersion.EnName);
                }

                return Json(new { });
            }

            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            return Json(new { });
        }

        /// <summary>
        /// Apply Edit Request
        /// </summary>
        /// <param name="id">dynamic page route version id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPage, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> ApplyEditRequestAsync(int id)
        {
            PageRouteVersion pageRouteVersion = _pageRouteVersionRepository.Get(id);
            string notificationMessage = null;
            if (ValidatePageRouteForApply(pageRouteVersion, out notificationMessage))
            {
                //var isApply = ApplyPageChanges(id, pageRouteVersion);
                var user = await _userManager.GetUserAsync(HttpContext.User);
                string url = $"{nameof(DynamicPageRouteController)[0..^10]}/{nameof(Edit)}";
                string sectionUrl = $"{nameof(DynamicPageSectionController)[0..^10]}/{nameof(Index)}";
                var isApply = _pageRouteVersionRepository.ApplySubmitRequest(pageRouteVersion, user.Id, url, sectionUrl);
                if (isApply)
                {

                    TempData[notificationMessageKey] = ToasrMessages.SubmitSuccess;
                    TempData[notificationTypeKey] = notificationSuccess;
                    return Json(new { });
                }

                TempData[notificationMessageKey] = "Error has been occurred.";
                TempData[notificationTypeKey] = notificationError;
                return Json(new { });
            }

            TempData[notificationMessageKey] = notificationMessage;
            TempData[notificationTypeKey] = notificationWarning;
            return Json(new { });
        }

        /// <summary>
        /// Get all page routes for grid it
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPage, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public async Task<JsonResult> GetPageRoutes()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var pageRouteViewModels = _pageRouteVersionRepository.GetDynamicPages();

            pageRouteViewModels = _iBEUsersPrivilegesService.FilterDynamicPages(pageRouteViewModels, user.Id);

            return Json(new { data = pageRouteViewModels });
        }

        /// <summary>
        /// Validate page route for apply changes
        /// </summary>
        /// <param name="pageRouteVersion">page route version object</param>
        /// <param name="message">out message which will appers to user</param>
        /// <returns></returns>
        [NonAction]
        private bool ValidatePageRouteForApply(PageRouteVersion pageRouteVersion, out string message)
        {
            if (pageRouteVersion.IsDeleted)
            {
                message = null;
                return true;
            }
            int sectionsCount = pageRouteVersion.PageSectionVersions.Count;
            int deletedSectionsCount = pageRouteVersion.PageSectionVersions.Count(s => s.IsDeleted);

            if (sectionsCount == deletedSectionsCount)
            {
                message = "This page is empty, please add sections to apply the changes.";
                return false;
            }
            else
            {
                int notDeletedSectionsWhooseAllCardsIsDeleted = 0;
                var notDeletedSectionsOfTypeCard = pageRouteVersion.PageSectionVersions.Where(s => !s.IsDeleted && s.PageSectionType.HasCards);
                var notDeletedSectionsOfTypeOtherThanCard = pageRouteVersion.PageSectionVersions.Where(s => !s.IsDeleted && !s.PageSectionType.HasCards);

                if (notDeletedSectionsOfTypeCard.Any(s => s.PageSectionCardVersions.Count == 0))
                {
                    message = "There is one or more section of type cards does not has any card. </br> Please add a new card to these sections or delete these sections in order to apply page changes.";
                    return false;
                }

                notDeletedSectionsOfTypeCard.ForEach(section =>
                {
                    int cardsCount = section.PageSectionCardVersions.Count;
                    int deletedCardsCount = section.PageSectionCardVersions.Count(c => c.IsDeleted);
                    if (cardsCount == deletedCardsCount)
                    {
                        notDeletedSectionsWhooseAllCardsIsDeleted++;
                    }
                });

                if (notDeletedSectionsWhooseAllCardsIsDeleted > 0 && notDeletedSectionsOfTypeOtherThanCard.Count() == 0)
                {
                    message = "There is one or more section of type cards all it's cards are flagged as deleted. </br> Please add a new card to these sections or delete these sections in order to apply page changes.";
                    return false;
                }
                else
                {
                    message = null;
                    return true;
                }
            }
        }

        /// <summary>
        /// Submit changes for approval user to approve or ignore it
        /// </summary>
        /// <param name="pageRouteViewModel"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPage, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> SubmitChanges(PageRouteEditViewModel pageRouteViewModel)
        {
            List<NavItem> navItems;
            if (!pageRouteViewModel.HasNavItem)
            {
                ModelState.Remove("NavItemId");
            }
            var prv = _pageRouteVersionRepository.GetWithNoTracking(pageRouteViewModel.Id);
            var pageSections = _IPageSectionVersionRepository.GetPageSectionsByPageRouteId(prv.Id);
            if (pageSections.Any())
            {

                if (!ModelState.IsValid)
                {
                    navItems = _navItemRepository.Get().ToList();
                    pageRouteViewModel.NavItems = navItems;
                    return View(nameof(Edit), pageRouteViewModel);
                }

                await SaveAndSubmitMethod(pageRouteViewModel, ChangeActionEnum.Update);
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Dynamic Pages ", "Submitted " + pageRouteViewModel.EnName);
                _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                navItems = _navItemRepository.Get().ToList();
                pageRouteViewModel.NavItems = navItems;
                _toastNotification.AddWarningToastMessage("Can't Submit Page Without Sections");
                return View(nameof(Edit), pageRouteViewModel);
            }
        }

        /// <summary>
        /// Core method for Submit changes
        /// </summary>
        /// <param name="pageRouteViewModel">page route data mapped to view model</param>
        /// <param name="changeActionEnum">change action enum after request submit</param>
        /// <returns></returns>
        private async Task SaveAndSubmitMethod(PageRouteEditViewModel pageRouteViewModel, ChangeActionEnum changeActionEnum, bool CheckSections = true)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var pageRoutVersionId = 0;
            var oldVersionStatusEnum = pageRouteViewModel.VersionStatusEnum ?? VersionStatusEnum.Submitted;

            pageRouteViewModel.VersionStatusEnum = VersionStatusEnum.Submitted;
            EditMethod(pageRouteViewModel, VersionStatusEnum.Submitted, oldVersionStatusEnum, changeActionEnum, user.Id, out pageRoutVersionId, true);
            var pageRouteVersion = _pageRouteVersionRepository.GetWithNoTracking(pageRoutVersionId);
            var BI_Notofication = _approvalNotificationsRepository.GetByRelatedIdAndType(pageRouteVersion.Id, ChangeType.BasicInfo);

            if (pageRoutVersionId != pageRouteViewModel.Id || BI_Notofication == null || BI_Notofication.VersionStatusEnum != VersionStatusEnum.Submitted)
            {
                SubmitMethod(user, pageRouteVersion, CheckSections);
            }
        }

        /// <summary>
        /// extract another method that help to submit changes
        /// </summary>
        /// <param name="user">logged in user</param>
        /// <param name="pageRouteVersion">page route version</param>
        [NonAction]
        private void SubmitMethod(ApplicationUser user, PageRouteVersion pageRouteVersion, bool CheckSections = true)
        {
            var existPageRouteVer = _pageRouteVersionRepository.Get(pageRouteVersion.Id);

            existPageRouteVer.VersionStatusEnum = VersionStatusEnum.Submitted;
            ApprovalNotification approval = new ApprovalNotification()
            {
                ChangeAction = existPageRouteVer.ChangeActionEnum ?? ChangeActionEnum.New,
                VersionStatusEnum = VersionStatusEnum.Submitted,
                ChangesDateTime = DateTime.Now,
                ChangeType = ChangeType.BasicInfo,
                PageLink = $"/{nameof(DynamicPageRouteController)[0..^10]}/{nameof(Edit)}/{existPageRouteVer.Id}",
                PageName = existPageRouteVer.EnName,
                PageType = PageType.Dynamic,
                ContentManagerId = user.Id,
                RelatedVersionId = existPageRouteVer.Id,
                RelatedPageEnum = RelatedPageEnum.PageRouteVersion
            };
            _approvalNotificationsRepository.Add(approval);
            if (existPageRouteVer.PageSectionVersions.Any() && existPageRouteVer.ContentVersionStatusEnum == VersionStatusEnum.Draft && CheckSections)
            {
                existPageRouteVer.ContentVersionStatusEnum = VersionStatusEnum.Submitted;

                ApprovalNotification approvalSections = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(DynamicPageSectionController)[0..^10]}/{nameof(Index)}?pageRouteVersionId={existPageRouteVer.Id}",
                    PageName = existPageRouteVer.EnName,
                    PageType = PageType.Dynamic,
                    ContentManagerId = user.Id,
                    RelatedVersionId = existPageRouteVer.Id,
                    RelatedPageEnum = RelatedPageEnum.PageRouteVersion
                };
                _approvalNotificationsRepository.Add(approvalSections);
            }

            _pageRouteVersionRepository.Update(existPageRouteVer);
        }

        /// <summary>
        /// Approve changes method by approval user
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <param name="approvalId">notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPage, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery] int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var prv = _pageRouteVersionRepository.GetByIdWithoutIncludes(id);

            _pageRouteVersionRepository.ApprovePageRoute(prv, ChangeType.BasicInfo);

            var approvalItem = _approvalNotificationsRepository.GetById(approvalId);


            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);
            PageRoute pr = new PageRoute();

            UpdatePageRouteVersionStatus(approvalItem, prv);

            if (prv.ChangeActionEnum == ChangeActionEnum.New)
            {
                pr = new PageRoute()
                {
                    ApprovalDate = DateTime.Now,
                    ApprovedById = user.Id,
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    ModifiedById = user.Id,
                    ModificationDate = DateTime.Now,
                    CreatedById = prv.CreatedById,
                    ArName = prv.ArName,
                    ControllerName = prv.ControllerName,
                    EnName = prv.EnName,
                    HasNavItem = prv.HasNavItem,
                    IsActive = prv.IsActive,
                    IsDynamicPage = prv.IsDynamicPage,
                    NavItemId = prv.NavItemId,
                    Order = prv.Order,
                    PageFilePathAr = prv.PageFilePathAr,
                    PageFilePathEn = prv.PageFilePathEn,
                    SeoTwitterCardEN = prv.SeoTwitterCardEN,
                    SeoTwitterCardAR = prv.SeoTwitterCardAR,
                    SeoTitleEN = prv.SeoTitleEN,
                    SeoTitleAR = prv.SeoTitleAR,
                    SeoOgTitleEN = prv.SeoOgTitleEN,
                    SeoOgTitleAR = prv.SeoOgTitleAR,
                    SeoDescriptionEN = prv.SeoDescriptionEN,
                    SeoDescriptionAR = prv.SeoDescriptionAR,
                    SectionName = prv.SectionName,
                    PageType = prv.PageType,
                };
                _pageRouteRepository.Add(pr);
                prv.PageRouteId = pr.Id;

                try
                {
                    _iBEUsersPrivilegesService.UpdateWithNewDynamicPages(pr.Id);
                    await _globalElasticSearchService.AddAsync(_pageRouteRepository.GetPageData(pr.Id));
                }
                catch { }
            }
            else if (prv.ChangeActionEnum == ChangeActionEnum.Update)
            {
                pr = _pageRouteRepository.Get(prv.PageRouteId ?? 0);

                pr.ApprovalDate = DateTime.Now;
                pr.ApprovedById = user.Id;
                pr.CreationDate = DateTime.Now;
                pr.ModifiedById = user.Id;
                pr.ModificationDate = DateTime.Now;
                pr.IsDeleted = false;
                pr.ArName = prv.ArName;
                pr.ControllerName = prv.ControllerName;
                pr.EnName = prv.EnName;
                pr.HasNavItem = prv.HasNavItem;
                pr.IsActive = prv.IsActive;
                pr.IsDynamicPage = prv.IsDynamicPage;
                pr.NavItemId = prv.NavItemId;
                pr.Order = prv.Order;
                pr.PageFilePathAr = prv.PageFilePathAr;
                pr.PageFilePathEn = prv.PageFilePathEn;
                pr.SeoTwitterCardEN = prv.SeoTwitterCardEN;
                pr.SeoTwitterCardAR = prv.SeoTwitterCardAR;
                pr.SeoTitleEN = prv.SeoTitleEN;
                pr.SeoTitleAR = prv.SeoTitleAR;
                pr.SeoOgTitleEN = prv.SeoOgTitleEN;
                pr.SeoOgTitleAR = prv.SeoOgTitleAR;
                pr.SeoDescriptionEN = prv.SeoDescriptionEN;
                pr.SeoDescriptionAR = prv.SeoDescriptionAR;
                pr.SectionName = prv.SectionName;
                pr.PageType = prv.PageType;

                _pageRouteRepository.UpdatePageRoute(pr);

                try
                {
                    if (pr.IsDeleted)
                    {
                        _iBEUsersPrivilegesService.UpdateWithNewDynamicPages(pr.Id, true);
                    }

                    await _globalElasticSearchService.DeleteAsync(pr.Id);
                    await _globalElasticSearchService.AddAsync(_pageRouteRepository.GetPageData(pr.Id));
                }
                catch { }
            }
            else if (prv.ChangeActionEnum == ChangeActionEnum.Delete)
            {
                pr = _pageRouteRepository.Get(prv.PageRouteId ?? 0);
                pr.ApprovalDate = DateTime.Now;
                pr.ApprovedById = user.Id;
                pr.CreationDate = DateTime.Now;
                pr.ModifiedById = user.Id;
                pr.ModificationDate = DateTime.Now;
                pr.IsDeleted = true;
                prv.IsDeleted = true;
                _pageRouteRepository.UpdatePageRoute(pr);

                if (pr != null)
                {
                    try
                    {
                        _approvalNotificationsRepository.DeleteAllRelatedNotificationsToDynamicPage(pr.Id);
                        _iBEUsersPrivilegesService.UpdateWithNewDynamicPages(pr.Id, true);
                        await _globalElasticSearchService.DeleteAsync(pr.Id);
                    }
                    catch { }
                }

            }

            _pageRouteVersionRepository.Update(prv);

            //create dynamic page
            _htmlHelper.ApplyPageChanges(prv.Id, pr);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Dynamic Pages ", "Approve ");

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Ignore changes method by approval user
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <param name="approvalId">notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPage, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery] int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approvalItem = _approvalNotificationsRepository.GetById(approvalId);
            var prv = _pageRouteVersionRepository.Get(id);
            prv.VersionStatusEnum = VersionStatusEnum.Ignored;

            UpdatePageRouteVersionStatus(approvalItem, prv);

            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            if (prv.ChangeActionEnum == ChangeActionEnum.New)
            {
                var approval = _approvalNotificationsRepository.GetByIdRelatedId(id, approvalId);
                if (approval != null)
                {
                    approval.VersionStatusEnum = VersionStatusEnum.Ignored;
                    approval.ChangesDateTime = DateTime.Now;
                    _approvalNotificationsRepository.Update(approval);
                }
                prv.VersionStatusEnum = VersionStatusEnum.Ignored;
            }
            _pageRouteVersionRepository.Update(prv);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Dynamic Pages ", "Reject ");

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        private static void UpdatePageRouteVersionStatus(ApprovalNotification approvalItem, PageRouteVersion prv)
        {
            if (approvalItem.ChangeType == ChangeType.BasicInfo && (prv.ContentVersionStatusEnum == null || prv.ContentVersionStatusEnum == VersionStatusEnum.Draft || prv.ContentVersionStatusEnum == VersionStatusEnum.Submitted))
            {
                prv.VersionStatusEnum = VersionStatusEnum.Submitted;

            }

        }
    }
}