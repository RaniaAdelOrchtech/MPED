using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Web.Admin.ViewModels;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using NToastNotify;
using MPMAR.Business;
using MPMAR.Common.Helpers;
using MPMAR.Web.Admin.Helpers;
using Microsoft.AspNetCore.Identity;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Data.Consts;
using MPMAR.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using MPMAR.Common;
using MPMAR.Common.Interfaces;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class StaticPageNewsController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";


        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<StaticPageNewsController> _eventLogger;
        private readonly IPageNewsRepository _PageNewsRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileService _fileService;
        private readonly INewsTypesForNewsRepository _NewsTypesForNewsRepository;
        private readonly IPageRouteRepository _pageRouteRepository;


        public StaticPageNewsController(IPageRouteVersionRepository pageRouteVersionRepository, IPageNewsRepository PageNewsRepository,
            IToastNotification toastNotification, IEventLogger<StaticPageNewsController> eventLogger, UserManager<ApplicationUser> userManager, IFileService fileService,
            INewsTypesForNewsRepository NewsTypesForNewsRepository, IPageRouteRepository pageRouteRepository)
        {

            _pageRouteVersionRepository = pageRouteVersionRepository;
            _PageNewsRepository = PageNewsRepository;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _userManager = userManager;
            _fileService = fileService;
            _NewsTypesForNewsRepository = NewsTypesForNewsRepository;
            _pageRouteRepository = pageRouteRepository;

        }
        /// <summary>
        /// get StaticPageNews page index
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.News)]
        public IActionResult Index(int pageRouteId)
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

            PageRoute pageRoute = _pageRouteRepository.Get(pageRouteId);
            if (pageRoute == null)
            {
                return NotFound();
            }

            return View(pageRouteId);
        }

        /// <summary>
        /// get StaticPageNews create page 
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd }, StaticPagesIdsConst.News)]
        public IActionResult Create(int pageRouteId)
        {
            List<PageNewsType> sectionTypes = _PageNewsRepository.GetPageNewsTypes();
            PageNewsCreateViewModel viewModel = new PageNewsCreateViewModel(sectionTypes);
            viewModel.PageRouteId = pageRouteId;
            return View(viewModel);
        }
        /// <summary>
        /// create StaticPageNews
        /// </summary>
        /// <param name="NewsViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd }, StaticPagesIdsConst.News)]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Create(PageNewsCreateViewModel NewsViewModel)
        {
            List<PageNewsType> NewsTypes;
            NewsViewModel.News.EnDescription.ValidateHtml("EnDescription", ModelState);
            NewsViewModel.News.ArDescription.ValidateHtml("ArDescription", ModelState);
            NewsViewModel.News.EnDescription.ValidateHtml("EnDescription", ModelState);
            NewsViewModel.News.ArDescription.ValidateHtml("ArDescription", ModelState);

            if (ModelState.IsValid)
            {

                PageNewsVersion PageNews = NewsViewModel.MapToPageNewsViewModel();
                if (NewsViewModel.News.Photo != null)
                    PageNews.Url = _fileService.UploadImageUrlNew(NewsViewModel.News.Photo);

                var user = await _userManager.GetUserAsync(HttpContext.User);

                PageNews.CreatedById = user.Id;
                PageNews.CreationDate = DateTime.Now;
                PageNewsVersion newPageNews = _PageNewsRepository.Add(PageNews, NewsViewModel.PageRouteId);
                if (newPageNews != null)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.AddSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Static Page > News > Add", NewsViewModel.News.EnTitle);
                    return RedirectToAction("Index", new { pageRouteId = NewsViewModel.PageRouteId });
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Static Page > News > Add", NewsViewModel.News.EnTitle);
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);

                    NewsTypes = _PageNewsRepository.GetPageNewsTypes();
                    NewsViewModel = new PageNewsCreateViewModel(NewsTypes);

                    return View(NewsViewModel);
                }
            }
            NewsTypes = _PageNewsRepository.GetPageNewsTypes();
            NewsViewModel = new PageNewsCreateViewModel(NewsTypes);

            return View(NewsViewModel);
        }
        /// <summary>
        /// get StaticPageNews
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.News)]
        public JsonResult GetPageNews(int id)
        {
            var NewsViewModel = _PageNewsRepository.GetPageNewsByPageRouteId(id);
            return Json(new { data = NewsViewModel });
        }
        /// <summary>
        /// delete StaticPageNews
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanDelete }, StaticPagesIdsConst.News)]
        public IActionResult Delete(int id)
        {
            var PageNews = _PageNewsRepository.Delete(id);

            if (PageNews)
            {
                TempData[notificationMessageKey] = ToasrMessages.DeleteSuccess;
                TempData[notificationTypeKey] = notificationSuccess;
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Static Page > News > Delete", "Delete id : " + id);
                return Json(new { });
            }

            TempData[notificationMessageKey] = "Error has been occurred.";
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Static Page > News > Delete", "Delete id : " + id);
            TempData[notificationTypeKey] = notificationError;
            return Json(new { });
        }
        /// <summary>
        /// submit changes for the approval user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit , PrivilegesActions.CanDelete }, StaticPagesIdsConst.News)]
        public async Task<IActionResult> ApplyEditRequestAsync(int id)
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            string url = $"{nameof(StaticPageNewsController)[0..^10]}/{nameof(Details)}";

            var success = _PageNewsRepository.ApplySubmitRequest(id, user.Id, url);
            if (success)
            {
                TempData[notificationMessageKey] = ToasrMessages.SubmitSuccess;

                TempData[notificationTypeKey] = notificationSuccess;
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Static Page > News > Submitted", "Submit id : " + id);

                return Json(new { });
            }


            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Static Page > News > Submit", "Submit id : " + id);




            return Json(new { });
        }
        /// <summary>
        /// get StaticPageNews edit page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.News)]
        public IActionResult Edit(int id, int pageRouteId)
        {
            var pageNews = _PageNewsRepository.Get(id);
            List<PageNewsType> NewsType = _PageNewsRepository.GetPageNewsTypes();
            PageNewsEditViewModel viewModel = new PageNewsEditViewModel(NewsType, pageNews);
            viewModel.PageRouteId = pageRouteId;
            viewModel.NewsTypesIds = pageNews.NewsTypeIds;
            return View(viewModel);
        }
        /// <summary>
        /// edit StaticPageNews
        /// </summary>
        /// <param name="PageNewsEditViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.News)]
        [RequestSizeLimit(500000000)]
        public IActionResult Edit(PageNewsEditViewModel PageNewsEditViewModel)
        {

            //PageNewsEditViewModel.News.EnDescription.ValidateHtml("EnDescription", ModelState);
            //PageNewsEditViewModel.News.ArDescription.ValidateHtml("ArDescription", ModelState);
            //PageNewsEditViewModel.News.EnShortDescription.ValidateHtml("EnShortDescription", ModelState);
            //PageNewsEditViewModel.News.ArShortDescription.ValidateHtml("ArShortDescription", ModelState);


            if (ModelState.IsValid)
            {

                PageNewsVersion PageNews = PageNewsEditViewModel.MapToPageNewsVersion();
                PageNews.NewsTypesForNewsVersions = PageNewsEditViewModel.MapToNewsTypeForNewsForEdit();
                string oldFilePath = null;
                if (PageNewsEditViewModel.News.Photo != null)
                {
                    oldFilePath = PageNews.Url;
                    PageNews.Url = _fileService.UploadImageUrlNew(PageNewsEditViewModel.News.Photo);
                }

                var newPageNews = _PageNewsRepository.Update(PageNews, PageNewsEditViewModel.PageRouteId);

                if (newPageNews != null)
                {

                    if (oldFilePath != null)
                    {
                        _fileService.RemoveImage(oldFilePath);
                    }
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Static Page > News > Edit", PageNewsEditViewModel.News.EnTitle);

                    return RedirectToAction("Index", new { pageRouteId = PageNewsEditViewModel.PageRouteId });
                }
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Static Page > News > Edit", PageNewsEditViewModel.News.EnTitle);
                _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                return View(PageNewsEditViewModel);
            }
            List<PageNewsType> NewsTypes = _PageNewsRepository.GetPageNewsTypes();
            PageNewsEditViewModel.NewsTypes = NewsTypes;

            return View(PageNewsEditViewModel);
        }

        /// <summary>
        /// get StaticPageNews details page
        /// </summary>
        /// <param name="id">StaticPageNews id</param>
        /// <param name="pageRouteId"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.News)]
        public IActionResult Details(int id, int pageRouteId, int? approvalId)
        {
            var News = _PageNewsRepository.Get(id);
            News.PageRouteId = pageRouteId;
            ViewBag.ApprovalId = approvalId;
            return View(News);
        }
        /// <summary>
        ///  approve changes,change status to approved  and update the non version 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanApprove }, StaticPagesIdsConst.News)]
        public async Task<IActionResult> ApproveAsync(int id, int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var success = _PageNewsRepository.Approve(id, approvalId, user.Id);
            if (success)
            {
                TempData[notificationMessageKey] = ToasrMessages.ApprovalSuccess;
                TempData[notificationTypeKey] = notificationSuccess;
                return RedirectToAction("Index", "ApprovalNotifications");
            }

            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Static Page > News > Approve", "id: " + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }
        /// <summary>
        /// Ignore changes and change status to ignored
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanApprove }, StaticPagesIdsConst.News)]
        public async Task<IActionResult> IgnoreAsync(int id, int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var success = _PageNewsRepository.Ignore(id, approvalId, user.Id);
            if (success)
            {
                TempData[notificationMessageKey] = ToasrMessages.ApprovalSuccess;
                TempData[notificationTypeKey] = notificationSuccess;
                return RedirectToAction("Index", "ApprovalNotifications");
            }

            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Static Page > News > Reject", "id: " + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }


    }
}