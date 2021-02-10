using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business;
using MPMAR.Business.Interfaces;
using MPMAR.Common;
using MPMAR.Data;
using MPMAR.Data.Consts;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;
using MPMAR.Business;
using Sotsera.Blazor.Toaster;
using static MPMAR.Data.Enums.Enums;
using MPMAR.Common.Interfaces;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class NavItemController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly INavItemRepository _navItemRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<NavItemController> _eventLogger;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        public readonly INavItemVersionRepository _navItemVersionRepository;

        public NavItemController(INavItemRepository navItemRepository, INavItemVersionRepository navItemVersionRepository, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<NavItemController> eventLogger, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _navItemRepository = navItemRepository;
            _navItemVersionRepository = navItemVersionRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }

        /// <summary>
        /// Index for griding all approved nav item objects
        /// </summary>
        /// <param name="pageRouteId">page route id</param>
        /// <param name="approvalId">approved notification id</param>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NavItems, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery]int pageRouteId, [FromQuery] int approvalId)
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.NavItems);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            ViewBag.pageRouteId = pageRouteId;
            ViewBag.approvalId = approvalId;
            return View();
        }

        /// <summary>
        /// Get method for creating nav item object
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NavItems, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create()
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.NavItems);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            return View();
        }

        /// <summary>
        /// Post method for creating nav item object
        /// </summary>
        /// <param name="navItemVersion">nav item model data</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NavItems, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public async Task<IActionResult> Create(NavItemVersionViewModel navItemVersion)
        {
            if (ModelState.IsValid)
            {
                await EditMethod(navItemVersion);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        /// <summary>
        /// Get method for updating nav item object
        /// </summary>
        /// <param name="id">nav item version id</param>
        /// <param name="navItemId">nav item id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NavItems, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery]int navItemId)
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.NavItems);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            return DeetailsMethod(id, navItemId);
        }

        /// <summary>
        /// Details nav item object by id
        /// </summary>
        /// <param name="id">nav item version id</param>
        /// <param name="navItemId">nav item id</param>
        /// <returns></returns>
        private IActionResult DeetailsMethod(int id, int navItemId,int approvalId=0)
        {
            NavItemVersionViewModel viewModel;
            var navVersion = _navItemVersionRepository.GetByNavId(navItemId);
            if (navVersion == null || navVersion.VersionStatusEnum == VersionStatusEnum.Approved || navVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                var nav = _navItemRepository.Get(navItemId);
                if (nav != null)
                    viewModel = nav.MapToNavViewModel();
                else
                {
                    navVersion = _navItemVersionRepository.Get(id);
                    viewModel = navVersion.MapToNavViewModel();
                }
            }
            else
            {
                viewModel = navVersion.MapToNavViewModel();
            }
            //remove id value from route
            viewModel.approvalId = approvalId;
            ModelState.Clear();
            return View(viewModel);
        }

        /// <summary>
        /// Post method for updating nav item object
        /// </summary>
        /// <param name="navItemVersion">nav item model data</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NavItems, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public async Task<IActionResult> Edit(NavItemVersionViewModel navItemVersion)
        {
            return await EditMethod(navItemVersion);
        }

        /// <summary>
        /// Core method for update the object
        /// </summary>
        /// <param name="viewModel">nav item new data</param>
        /// <returns></returns>
        private async Task<IActionResult> EditMethod(NavItemVersionViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var navVersionByNavId = _navItemVersionRepository.GetByNavId(viewModel.NavItemId ?? 0);
                var navVersionById = _navItemVersionRepository.Get(viewModel.Id);
                navVersionByNavId = navVersionById == null ? navVersionByNavId : navVersionById;
                var navVersionModel = viewModel.MapToNavVersionModel();

                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (navVersionByNavId == null || navVersionModel.VersionStatusEnum == VersionStatusEnum.Approved || navVersionModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    navVersionModel.CreatedById = user.Id;
                    navVersionModel.CreationDate = DateTime.Now;
                    navVersionModel.VersionStatusEnum = VersionStatusEnum.Draft;
                    navVersionModel.ChangeActionEnum = ChangeActionEnum.Update;
                    navVersionModel.Id = 0;
                    navVersionModel.NavItemId = viewModel.NavItemId > 0 ? viewModel.NavItemId : (int?)null;
                    _navItemVersionRepository.Add(navVersionModel);
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Definitions > Nav Items > Add", viewModel.EnName);

                    return RedirectToAction(nameof(Index));
                }
                else if (navVersionModel.VersionStatusEnum == VersionStatusEnum.Submitted)
                { _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess); }
                navVersionModel.Id = navVersionByNavId != null ? navVersionByNavId.Id : viewModel.Id;

                _navItemVersionRepository.Update(navVersionModel);


                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Definitions > Nav Items > Edit", viewModel.EnName);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        /// <summary>
        /// get method for details nav item object
        /// </summary>
        /// <param name="id">nav item version id</param>
        /// <param name="navItemId">nav item id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NavItems, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Details(int id, [FromQuery]int navItemId, int approvalId)
        {
            return DeetailsMethod(id, navItemId,approvalId);
        }

        /// <summary>
        /// Delete nav item object by id
        /// </summary>
        /// <param name="id">nav item version id</param>
        /// <param name="navItemId">nav item id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NavItems, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public async Task<IActionResult> Delete(int id, int navItemId)
        {
            try
            {
                var navVersion = _navItemVersionRepository.GetByNavId(navItemId);
                if (navVersion != null)
                {

                    if (navVersion.NavItem.PageRoutes.Any())
                    {
                        if (navVersion.NavItem.PageRoutes.Count > navVersion.NavItem.PageRoutes.Where(x => x.IsDeleted).Count())
                        {
                            _toastNotification.AddWarningToastMessage("You Can't Delete Nav Has Related Pages.");

                            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Definitions > Nav Items > Delete", " Id :" + id);

                            return Json(new { });

                        }

                    }
                    else if (navVersion.NavItem.NavItemList.Any())
                    {
                        _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Definitions > Nav Items > Delete", " Id :" + id);
                        _toastNotification.AddWarningToastMessage("You Can't Delete Nav Has Child Navs.");
                        return Json(new { });
                    }

                    navVersion.IsDeleted = true;
                    await EditMethod(navVersion.MapToNavViewModel());

                }
                else
                {
                    var nav = _navItemRepository.GetByIdWithNoTracking(navItemId);
                    if (nav != null)
                    {
                        if (nav.PageRoutes.Any())
                        {
                            if (nav.PageRoutes.Count > nav.PageRoutes.Where(x => x.IsDeleted).Count())
                            {
                                _toastNotification.AddWarningToastMessage("You Can't Delete Nav Has Related Pages.");
                                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Definitions > Nav Items > Delete", " Id :" + id);
                                return Json(new { });

                            }
                        }
                        else if (nav.NavItemList.Any())
                        {
                            _toastNotification.AddWarningToastMessage("You Can't Delete Nav Has Child Navs.");
                            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Definitions > Nav Items > Delete", " Id :" + id);
                            return Json(new { });
                        }
                        nav.IsDeleted = true;
                        await EditMethod(nav.MapToNavViewModel());
                    }
                    else
                        _navItemVersionRepository.Delete(id);

                }


                _toastNotification.AddSuccessToastMessage(ToasrMessages.DeleteSuccess);

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Definitions > Nav Items > Delete", " Id :" + id);
                return Json(new { });
            }
            catch
            {

                _toastNotification.AddWarningToastMessage("Error has been occurred.");
                return Json(new { });
            }
        }



        /// <summary>
        /// Get all nav item objects for index
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NavItems, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetNavItems()
        {
            var navItemVersions = _navItemVersionRepository.Get();
            //var navItemViewModels = navItemVersions.MapToNavItemViewModels();
            return Json(new { data = navItemVersions });
        }


        /// <summary>
        /// Submit changes that send notification to approval user with last changes
        /// </summary>
        /// <param name="pageRouteId">page route id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NavItems, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> SubmitChanges([FromQuery] int pageRouteId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var av = _navItemVersionRepository.GetAllDrafts();

            foreach (var record in av)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                _navItemVersionRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.NavItems);

            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(NavItemController)[0..^10]}",
                    PageName = PagesNamesConst.NavItems,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);

            }
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Definitions > Nav Items > Submitted", " Id :" + pageRouteId);
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Approve method that allow approval user to approve last changes
        /// </summary>
        /// <param name="id">nav item id</param>
        /// <param name="approvalId">approced notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NavItems, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var sv = _navItemVersionRepository.GetAllSubmitted();

            foreach (var record in sv)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;

                var s = new NavItem()
                {
                    //Id = record.PageMinistryId ?? 0,
                    Order = record.Order,
                    ArName = record.ArName,
                    EnName = record.EnName,
                    ParentNavItemId = record.ParentNavItemId,
                    IsActive = record.IsActive,
                    IsDeleted = record.IsDeleted,
                    CreatedById = user.Id,
                    CreationDate = DateTime.Now,
                };
                if (record.NavItemId != null)
                {
                    s.Id = record.NavItemId ?? 0;
                    _navItemRepository.Update(s);
                }
                else
                {
                    s.Id = 0;
                    _navItemRepository.Add(s);
                    record.NavItemId = s.Id;
                }

                _navItemVersionRepository.Update(record);
            }
            approval.VersionStatusEnum = VersionStatusEnum.Approved;
            approval.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approval);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Definitions > Nav Items > Approve", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Ignore method that allow approval user to ignore last changes
        /// </summary>
        /// <param name="id">nav item id</param>
        /// <param name="approvalId">approced notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.NavItems, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var sv = _navItemVersionRepository.GetAllSubmitted();

            foreach (var record in sv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _navItemVersionRepository.Update(record);
            }

            approval.VersionStatusEnum = VersionStatusEnum.Ignored;
            approval.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approval);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Definitions > Nav Items > Reject", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }
    }
}