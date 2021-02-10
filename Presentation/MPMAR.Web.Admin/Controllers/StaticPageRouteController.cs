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

using System.IO;
using Microsoft.AspNetCore.Hosting;
using static MPMAR.Data.Enums.Enums;
using MPMAR.Data.Enums;
using MPMAR.Common;
using Microsoft.AspNetCore.Authorization;
using MPMAR.Data.Consts;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Common.Interfaces;
using Abp.Extensions;
using MPMAR.Web.Admin.AuthRequirement;
using static MPMAR.Web.Admin.AuthRequirement.BEUsersPrivilegesRequirementAttribute;
using MPMAR.Web.Admin.Services;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class StaticPageRouteController : Controller
    {


        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";



        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<StaticPageRouteController> _eventLogger;
        private readonly INavItemRepository _navItemRepository;

        private readonly IWebHostEnvironment _IWebHostEnvironment;
        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        private readonly IGlobalElasticSearchService _globalElasticSearchService;
        private readonly IBEUsersPrivilegesService _iBEUsersPrivilegesService;
        private readonly IPageNewsRepository _IPageNewsnRepository;

        public StaticPageRouteController(IPageRouteVersionRepository pageRouteVersionRepository, IPageNewsRepository PageNewsRepository, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<StaticPageRouteController> eventLogger, INavItemRepository navItemRepository, IWebHostEnvironment WebHostEnvironment, IPageRouteRepository pageRouteRepository,
            IApprovalNotificationsRepository approvalNotificationsRepository, IGlobalElasticSearchService globalElasticSearchService, IBEUsersPrivilegesService iBEUsersPrivilegesService)

        {
            _pageRouteVersionRepository = pageRouteVersionRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _navItemRepository = navItemRepository;

            _IWebHostEnvironment = WebHostEnvironment;
            _pageRouteRepository = pageRouteRepository;
            _approvalNotificationsRepository = approvalNotificationsRepository;
            _globalElasticSearchService = globalElasticSearchService;
            _iBEUsersPrivilegesService = iBEUsersPrivilegesService;
            _IPageNewsnRepository = PageNewsRepository;

        }
        /// <summary>
        /// get StaticPageRoute page index
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView })]
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
        /// get StaticPageRoute edit page 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public async Task<IActionResult> Edit(int id, [FromQuery] int approvalId)
        {
            return await GetDetails(id, approvalId);
        }

        private async Task<IActionResult> GetDetails(int id, int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);

            PageRouteVersion pageRouteVersion = _pageRouteVersionRepository.Get(id);
            var notification = _approvalNotificationsRepository.GetByPageNameAndChangeType(pageRouteVersion.EnName, ChangeType.BasicInfo);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            if (pageRouteVersion == null || (roles.Contains(UserRolesConst.MediaCenterContentManager) && (pageRouteVersion.ControllerName != "News" && pageRouteVersion.ControllerName != "PhotoArchive" && pageRouteVersion.ControllerName != "EventCalendar")))
                return NotFound();

            List<NavItem> navItems = _navItemRepository.Get().ToList();
            PageRouteEditViewModel viewModel = new PageRouteEditViewModel(navItems);
            viewModel = pageRouteVersion.MapToPageRouteViewModel(viewModel);

            ViewBag.ApprovalId = approvalId;

            return View(viewModel);
        }

        /// <summary>
        /// edit StaticPageRoute
        /// </summary>
        /// <param name="pageRouteViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public async Task<IActionResult> Edit(PageRouteEditViewModel pageRouteViewModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return EditMethod(pageRouteViewModel, VersionStatusEnum.Draft, pageRouteViewModel.VersionStatusEnum ?? VersionStatusEnum.Draft, ChangeActionEnum.Update, user.Id, out int pageRoutVersionId);


        }
        /// <summary>
        /// get StaticPageRoute details page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanViewSP_BI })]
        public async Task<IActionResult> Details(int id, [FromQuery] int approvalId)
        {
            return await GetDetails(id, approvalId);
        }
        /// <summary>
        /// delete StaticPageRoute
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public async Task<IActionResult> Delete(int id)
        {
            PageRouteVersion pageRouteVersion = _pageRouteVersionRepository.Get(id);

            if (pageRouteVersion != null)
            {
                TempData[notificationMessageKey] = ToasrMessages.DeleteSuccess;
                TempData[notificationTypeKey] = notificationSuccess;

                if (pageRouteVersion.ChangeActionEnum == ChangeActionEnum.New && pageRouteVersion.VersionStatusEnum == VersionStatusEnum.Draft)
                {
                    _pageRouteVersionRepository.SoftDelete(id);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Definitions > Static Pages > Delete", "Soft Delete id: " + id);
                }
                else
                {

                    await SaveAndSubmitMethod(pageRouteVersion.MapToPageRouteViewModel(new PageRouteEditViewModel()), ChangeActionEnum.Delete);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Definitions > Static Pages > Delete", "Delete id: " + id);
                }

                return Json(new { });
            }

            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            return Json(new { });
        }
        /// <summary>
        /// get StaticPageRoute
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public async Task<JsonResult> GetPageRoutes()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var pageRoutes = _pageRouteVersionRepository.GetStaticPages();



            pageRoutes = _iBEUsersPrivilegesService.FilterStaticPages(pageRoutes, user.Id);

            return Json(new { data = pageRoutes });

        }
        /// <summary>
        /// submit changes for the approval user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public IActionResult ApplyEditRequest(int id)
        {
            PageRouteVersion pageRouteVersion = _pageRouteVersionRepository.Get(id);
            string notificationMessage = null;
            if (ValidatePageRouteForApply(pageRouteVersion, out notificationMessage))
            {
                PageRouteVersion newPageRouteVersion = _pageRouteVersionRepository.ApplyEditRequest(id, null, null);

                if (newPageRouteVersion != null)
                {
                    TempData[notificationMessageKey] = ToasrMessages.SubmitSuccess;
                    TempData[notificationTypeKey] = notificationSuccess;

                    return Json(new { });
                }
            }



            TempData[notificationMessageKey] = notificationMessage;
            TempData[notificationTypeKey] = notificationWarning;
            return Json(new { });
        }

        /// <summary>
        /// apply template on news type
        /// </summary>
        /// <param name="Template"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        /// 
        public string ApplyTemplateOnGeneratedHtmlFileForNewsTypes(string Template, string language)
        {
            var TemplateHeadParts = Template.Split("<!-- /NewsTypeSplit -->");

            var NewsTypes = _IPageNewsnRepository.GetPageNewsTypes();

            string TypeVal = "&lt;&lt;TypeVal&gt;&gt;";
            string TypeText = "&lt;&lt;TypeText&gt;&gt;";

            var text = "";
            text += TemplateHeadParts[0];
            foreach (var NewsType in NewsTypes)
            {

                var templateFields = TemplateHeadParts[1];
                try
                {

                    if (Template.Contains(TypeVal))
                    {
                        templateFields = templateFields.Replace(TypeVal, NewsType != null ? "." + NewsType.EnName : "");
                    }

                    if (Template.Contains(TypeText))
                    {

                        templateFields = templateFields.Replace(TypeText, NewsType != null ? (language == "en" ? NewsType.EnName : NewsType.ArName) : "");
                    }
                }
                catch
                {
                }
                text += templateFields;
            }
            text += TemplateHeadParts[2];

            return text;
        }
        /// <summary>
        /// apply template on news
        /// </summary>
        /// <param name="Template"></param>
        /// <param name="LatestNewsOnly"></param>
        /// <param name="News"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public string ApplyTemplateOnGeneratedHtmlFileForNews(string Template, bool LatestNewsOnly, List<PageNews> News, string language)
        {
            //ApplyTemplateOnGeneratedHtmlFile
            string tittle = "&lt;&lt;tittle&gt;&gt;";
            string Shortdesc = "&lt;&lt;ShortDescription&gt;&gt;";
            string Image = "&lt;&lt;Image&gt;&gt;";
            string Newsdate = "&lt;&lt;Date&gt;&gt;";
            string Type = "&lt;&lt;Type&gt;&gt;";
            string id = "&lt;&lt;DivId&gt;&gt;";
            string newsCount = "&lt;&lt;newsCount&gt;&gt;";
            string ClassType = "&lt;&lt;ClassType&gt;&gt;";
            string desc = "&lt;&lt;Description&gt;&gt;";
            string HeadID = "&lt;&lt;HeadID&gt;&gt;";
            string ContentID = "&lt;&lt;ContentID&gt;&gt;";
            string index = "&lt;&lt;index&gt;&gt;";
            string TittleID = "&lt;&lt;TittleID&gt;&gt;";
            string Day = "&lt;&lt;Day&gt;&gt;";
            string month = "&lt;&lt;month&gt;&gt;";
            string OtherNewsId = "&lt;&lt;OtherNewsId&gt;&gt;";

            if (Template.Contains(newsCount))
                Template = Template.Replace(newsCount, News.Count().ToString());

            var text = "";

            var i = 1;

            if (LatestNewsOnly == true)
            {
                News = News.Take(4).ToList();
            }

            foreach (var NewsItem in News)
            {

                var templateFields = Template;
                try
                {
                    var NewsTypesIdsList = NewsItem.NewsTypesForNews.ToList();
                    var typeList = _IPageNewsnRepository.GetPageNewsType(NewsTypesIdsList.Select(a => a.NewsTypeId).ToList());

                    if (Template.Contains(tittle))
                        templateFields = templateFields.Replace(tittle, language == "en" ? NewsItem.EnTitle : NewsItem.ArTitle);
                    if (Template.Contains(Newsdate))
                        templateFields = templateFields.Replace(Newsdate, NewsItem.Date.Value.ToString("dd MMM yyy"));
                    if (Template.Contains(Type))
                    {
                        templateFields = templateFields.Replace(Type, typeList.Count > 0 ? (language == "en" ? (string.Join(",", typeList.Select(a => a.EnName))) : (string.Join(",", typeList.Select(a => a.ArName)))) : "");
                    }
                    if (Template.Contains(ClassType))
                    {
                        templateFields = templateFields.Replace(ClassType, typeList.Count > 0 ? (string.Join(" ", typeList.Select(a => a.EnName))) : "");
                    }
                    if (Template.Contains(Image))
                        templateFields = templateFields.Replace(Image, _IWebHostEnvironment.WebRootPath + "\\Uploads\\Images\\" + NewsItem.Url);
                    if (Template.Contains(Shortdesc))
                    {
                        templateFields = templateFields.Replace(Shortdesc, language == "en" ? NewsItem.EnShortDescription : NewsItem.ArShortDescription);
                    }
                    if (Template.Contains(id))
                    {
                        templateFields = templateFields.Replace(id, "div" + i);
                    }
                    if (Template.Contains(desc))
                    {
                        templateFields = templateFields.Replace(desc, language == "en" ? NewsItem.EnDescription : NewsItem.ArDescription);
                    }
                    if (Template.Contains(HeadID))
                    {
                        templateFields = templateFields.Replace(HeadID, "HeadID" + i);
                    }
                    if (Template.Contains(ContentID))
                    {
                        templateFields = templateFields.Replace(ContentID, "ContentID" + i);
                    }
                    if (Template.Contains(index))
                    {
                        templateFields = templateFields.Replace(index, i.ToString());
                    }
                    if (Template.Contains(TittleID))
                    {
                        templateFields = templateFields.Replace(TittleID, "tittle" + i);
                    }
                    if (Template.Contains(month))
                    {
                        templateFields = templateFields.Replace(month, NewsItem.Date.Value.ToString("MMM"));
                    }
                    if (Template.Contains(Day))
                    {
                        templateFields = templateFields.Replace(Day, NewsItem.Date.Value.ToString("dd"));
                    }
                    if (Template.Contains(OtherNewsId))
                    {
                        templateFields = templateFields.Replace(OtherNewsId, "OtherNewsId" + i.ToString());
                    }

                }
                catch
                {
                }
                text += templateFields;
                i++;

            }
            return text;
        }


        /// <summary>
        /// validate page before apply
        /// </summary>
        /// <param name="pageRouteVersion"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        private bool ValidatePageRouteForApply(PageRouteVersion pageRouteVersion, out string message)
        {
            if (pageRouteVersion.IsDeleted)
            {
                message = null;
                return true;
            }
            int NewsCount = pageRouteVersion.PageNewsVersions.Count;
            int deletedNewsCount = pageRouteVersion.PageNewsVersions.Count(s => s.IsDeleted);

            if (NewsCount == deletedNewsCount)
            {
                message = "All page News is flagged as deleted. </br> Please add a new news in order to apply page changes.";
                return false;
            }
            message = null;
            return true;
        }
        /// <summary>
        /// submit changes method that send notification to approval user with last changes
        /// </summary>
        /// <param name="pageRouteViewModel"></param>
        /// <returns></returns>

        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> SubmitChanges(PageRouteEditViewModel pageRouteViewModel)
        {
            List<NavItem> navItems;
            if (!pageRouteViewModel.HasNavItem)
            {
                ModelState.Remove("NavItemId");
            }
            if (!ModelState.IsValid)
            {
                navItems = _navItemRepository.Get().ToList();
                pageRouteViewModel.NavItems = navItems;
                return View(nameof(Edit), pageRouteViewModel);
            }

            await SaveAndSubmitMethod(pageRouteViewModel, ChangeActionEnum.Update);

            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// edit static page route
        /// </summary>
        /// <param name="pageRouteViewModel"></param>
        /// <param name="newVersionStatusEnum"></param>
        /// <param name="oldVersionStatusEnum"></param>
        /// <param name="changeActionEnum"></param>
        /// <param name="userId"></param>
        /// <param name="pageRoutVersionId"></param>
        /// <returns></returns>
        private IActionResult EditMethod(PageRouteEditViewModel pageRouteViewModel, VersionStatusEnum newVersionStatusEnum, VersionStatusEnum oldVersionStatusEnum, ChangeActionEnum changeActionEnum, string userId, out int pageRoutVersionId,bool fromSaveAndSubmit=false)
        {

            List<NavItem> navItems;
            if (!pageRouteViewModel.HasNavItem)
            {
                ModelState.Remove("NavItemId");
            }


            PageRouteVersion pageRouteVersion = pageRouteViewModel.MapToPageRouteVersion();
            if (ModelState.IsValid)
            {
                if (oldVersionStatusEnum == VersionStatusEnum.Approved || oldVersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    pageRouteVersion.CreatedById = userId;
                    pageRouteVersion.CreationDate = DateTime.Now;
                    pageRouteVersion.VersionStatusEnum = newVersionStatusEnum;
                    pageRouteVersion.ChangeActionEnum = changeActionEnum;
                    pageRouteVersion.Id = 0;
                    _pageRouteVersionRepository.AddStaticPage(pageRouteVersion);
                    pageRoutVersionId = pageRouteVersion.Id;
                    if(!fromSaveAndSubmit)
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Definitions > Static Pages > Add", pageRouteViewModel.EnName);
                    return RedirectToAction(nameof(Index));
                }
                PageRouteVersion newPageRouteVersion = _pageRouteVersionRepository.Update(pageRouteVersion);
                if (newPageRouteVersion != null)
                {
                    if (!fromSaveAndSubmit)
                        _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Definitions > Static Pages > Edit", pageRouteViewModel.EnName);
                    pageRoutVersionId = pageRouteVersion.Id;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    navItems = _navItemRepository.Get().ToList();
                    pageRouteViewModel.NavItems = navItems;
                    pageRoutVersionId = pageRouteVersion.Id;
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Definitions > Static Pages > Edit", pageRouteViewModel.EnName);
                    return View(pageRouteViewModel);
                }
            }
            navItems = _navItemRepository.Get().ToList();
            pageRouteViewModel.NavItems = navItems;
            pageRoutVersionId = pageRouteVersion.Id;
            return View(pageRouteViewModel);
        }
        /// <summary>
        /// save and submit static page route
        /// </summary>
        /// <param name="pageRouteViewModel"></param>
        /// <param name="changeActionEnum"></param>
        /// <returns></returns>
        private async Task SaveAndSubmitMethod(PageRouteEditViewModel pageRouteViewModel, ChangeActionEnum changeActionEnum)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var pageRoutVersionId = 0;
            var oldVersionStatusEnum = pageRouteViewModel.VersionStatusEnum ?? VersionStatusEnum.Submitted;
            pageRouteViewModel.VersionStatusEnum = VersionStatusEnum.Submitted;
            EditMethod(pageRouteViewModel, VersionStatusEnum.Submitted, oldVersionStatusEnum, changeActionEnum, user.Id, out pageRoutVersionId,true);
            var pageRouteVersion = _pageRouteVersionRepository.Get(pageRoutVersionId);

            if (pageRoutVersionId != pageRouteViewModel.Id || oldVersionStatusEnum != VersionStatusEnum.Submitted)
            {
                SubmitMethod(user, pageRouteVersion);
            }
        }
        /// <summary>
        /// submmit static page route
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pageRouteVersion"></param>
        [NonAction]
        private void SubmitMethod(ApplicationUser user, PageRouteVersion pageRouteVersion)
        {
            ApprovalNotification approval = new ApprovalNotification()
            {
                ChangeAction = pageRouteVersion.ChangeActionEnum ?? ChangeActionEnum.New,
                VersionStatusEnum = VersionStatusEnum.Submitted,
                ChangesDateTime = DateTime.Now,
                ChangeType = ChangeType.BasicInfo,
                PageLink = $"/{nameof(StaticPageRouteController)[0..^10]}/{nameof(Edit)}/{pageRouteVersion.Id}",
                PageName = pageRouteVersion.EnName,
                PageType = PageType.Static,
                ContentManagerId = user.Id,
                RelatedVersionId = pageRouteVersion.Id,
                RelatedPageEnum = RelatedPageEnum.PageRouteVersion
            };
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Definitions > Static Pages > Submitted", "id: " + pageRouteVersion.Id);
            _approvalNotificationsRepository.Add(approval);
        }
        /// <summary>
        /// Approve method that allow approval user to approve last changes to allow it to appears in website
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery] int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var prv = _pageRouteVersionRepository.Get(id);
            prv.VersionStatusEnum = VersionStatusEnum.Approved;

            var approvalItem = _approvalNotificationsRepository.GetById(approvalId);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            if (prv.ChangeActionEnum == ChangeActionEnum.New)
            {
                var pr = new PageRoute()
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
            }
            else if (prv.ChangeActionEnum == ChangeActionEnum.Update)
            {
                var pr = _pageRouteRepository.Get(prv.PageRouteId ?? 0);

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
                    await _globalElasticSearchService.DeleteAsync(pr.Id);
                    await _globalElasticSearchService.AddAsync(_pageRouteRepository.GetPageData(pr.Id));
                }
                catch { }

            }
            else if (prv.ChangeActionEnum == ChangeActionEnum.Delete)
            {
                var pr = _pageRouteRepository.Get(prv.PageRouteId ?? 0);
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
                        await _globalElasticSearchService.DeleteAsync(pr.Id);
                    }
                    catch { }
                }

            }
            _pageRouteVersionRepository.Update(prv);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Definitions > Static Pages > Approve", "id: " + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }
        /// <summary>
        /// Ignore method that allow approval user to ignore last changes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery] int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var prv = _pageRouteVersionRepository.Get(id);
            prv.VersionStatusEnum = VersionStatusEnum.Ignored;
            _pageRouteVersionRepository.Update(prv);

            var approvalItem = _approvalNotificationsRepository.GetById(approvalId);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Definitions > Static Pages > Reject", "id: " + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }
    }
}