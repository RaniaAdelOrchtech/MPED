using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MPMAR.Business.Interfaces;
using MPMAR.Business.ViewModels;
using MPMAR.Common;
using MPMAR.Common.Helpers;
using MPMAR.Data;
using MPMAR.Data.Consts;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;
using MPMAR.Business;
using static MPMAR.Data.Enums.Enums;
using MPMAR.Common.Interfaces;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.AuthRequirement;
using Microsoft.Extensions.Configuration;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class DynamicSectionCardController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;
        private readonly IPageSectionVersionRepository _pageSectionVersionRepository;
        private readonly ISectionCardVersionRepository _sectionCardVersionRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<DynamicSectionCardController> _eventLogger;
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;

        public DynamicSectionCardController(IPageRouteVersionRepository pageRouteVersionRepository, IPageSectionVersionRepository pageSectionVersionRepository, ISectionCardVersionRepository sectionCardVersionRepository, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<DynamicSectionCardController> eventLogger, IFileService fileService, IConfiguration Configuration)
        {
            _pageRouteVersionRepository = pageRouteVersionRepository;
            _pageSectionVersionRepository = pageSectionVersionRepository;
            _sectionCardVersionRepository = sectionCardVersionRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _fileService = fileService;
            _configuration = Configuration;
        }

        /// <summary>
        /// Index for griding dynamic section cards
        /// </summary>
        /// <param name="id">page section version id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <param name="approvalId">approve notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index(int id, int pageRouteVersionId, int? approvalId)
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
            ViewBag.ApprovalId = approvalId;
            SectionViewModel pageSectionVersion = _pageSectionVersionRepository.Get(id, pageRouteVersionId);
            if (pageSectionVersion == null)
            {
                return NotFound();
            }

            return View(pageSectionVersion);
        }

        /// <summary>
        /// Get method for create a new dynamic section card
        /// </summary>
        /// <param name="sectionVersionId"> page section version id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create(int sectionVersionId, int pageRouteVersionId)
        {
            SectionCardCreateViewModel viewModel = new SectionCardCreateViewModel();
            viewModel.SectionVersionId = sectionVersionId;
            viewModel.PageRouteVersionId = pageRouteVersionId;
            return View(viewModel);
        }

        /// <summary>
        /// Post method for create a new dynamic section card
        /// </summary>
        /// <param name="sectionCardViewModel">section card model data</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Create(SectionCardCreateViewModel sectionCardViewModel)
        {
            sectionCardViewModel.EnDescription.ValidateHtml("EnDescription", ModelState);
            sectionCardViewModel.EnDescription.ValidateHtml("ArDescription", ModelState);
            if (sectionCardViewModel.Photo == null || sectionCardViewModel.File == null)
            {
                ModelState.AddModelError(nameof(sectionCardViewModel.Photo), "you should uplaod a photo and file.");
                ModelState.AddModelError(nameof(sectionCardViewModel.File), "you should uplaod a photo and file.");
                _toastNotification.AddWarningToastMessage("you should uplaod a photo and file.");
            }
            if (ModelState.IsValid)
            {
                PageSectionCardVersion sectionCardVersion = sectionCardViewModel.MapToSectionCardVersion();

                sectionCardVersion.ImageUrl = _fileService.UploadImageUrlNew(sectionCardViewModel.Photo);
                sectionCardVersion.FileUrl = _fileService.UploadFileUrlNew(sectionCardViewModel.File);

                var user = await _userManager.GetUserAsync(HttpContext.User);

                sectionCardVersion.CreatedById = user.Id;
                sectionCardVersion.CreationDate = DateTime.Now;

                PageSectionCardVersion newPageSectionCardVersion = _sectionCardVersionRepository.Add(sectionCardVersion, sectionCardViewModel.PageRouteVersionId);
                if (newPageSectionCardVersion != null)
                {


                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Dynamic Page > Card > Add", sectionCardVersion.EnTitle);

                    _toastNotification.AddSuccessToastMessage(ToasrMessages.AddSuccess);
                    return RedirectToAction("Index", new
                    {
                        id = newPageSectionCardVersion.PageSectionVersionId,
                        pageRouteVersionId = newPageSectionCardVersion.PageSectionVersion.PageRouteVersionId
                    });
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Dynamic Page > Card > Warning", sectionCardVersion.EnTitle);
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                }
            }
            return View(sectionCardViewModel);
        }

        /// <summary>
        /// Get method for edit a dynamic section card
        /// </summary>
        /// <param name="id">section card version id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, int pageRouteVersionId)
        {
            SectionCardEditViewModel viewModel = _sectionCardVersionRepository.Get(id, pageRouteVersionId);

            var subApplicationName = _configuration.GetSection("SubApplicationName").Value;
            if (!string.IsNullOrWhiteSpace(subApplicationName))
                viewModel.FileUrl = subApplicationName + viewModel.FileUrl;

            return View(viewModel);
        }

        /// <summary>
        /// Post method for edit a dynamic section card
        /// </summary>
        /// <param name="sectionCardViewModel">section card model new data</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRolesConst.ContentManager + "," + UserRolesConst.SuperAdmin)]
        [RequestSizeLimit(500000000)]
        public IActionResult Edit(SectionCardEditViewModel sectionCardViewModel)
        {
            sectionCardViewModel.EnDescription.ValidateHtml("EnDescription", ModelState);
            sectionCardViewModel.ArDescription.ValidateHtml("ArDescription", ModelState);

            if (ModelState.IsValid)
            {
                PageSectionCardVersion sectionCardVersion = sectionCardViewModel.MapToSectionCardVersion();
                string oldPhotoPath = null;
                string oldFilePath = null;

                if (sectionCardViewModel.Photo != null)
                {
                    oldPhotoPath = sectionCardVersion.ImageUrl;
                    sectionCardVersion.ImageUrl = _fileService.UploadImageUrlNew(sectionCardViewModel.Photo);
                }

                if (sectionCardViewModel.File != null)
                {
                    oldFilePath = sectionCardVersion.FileUrl;
                    sectionCardVersion.FileUrl = _fileService.UploadFileUrlNew(sectionCardViewModel.File);
                }

                var subApplicationName = _configuration.GetSection("SubApplicationName").Value;
                if (!string.IsNullOrWhiteSpace(subApplicationName))
                {
                    sectionCardViewModel.FileUrl = sectionCardViewModel.FileUrl.ToLower().Replace(subApplicationName.ToLower(), "");
                    sectionCardVersion.FileUrl = sectionCardViewModel.FileUrl;
                }

                PageSectionCardVersion newSectionCardVersion = _sectionCardVersionRepository.Update(sectionCardVersion, sectionCardViewModel.PageRouteVersionId);
                if (newSectionCardVersion != null)
                {
                    if (newSectionCardVersion.PageSectionVersion.PageRouteVersion.StatusId != (int)RequestStatus.Draft)
                    {
                        _pageRouteVersionRepository.ChangeStatus(newSectionCardVersion.PageSectionVersion.PageRouteVersionId, RequestStatus.Draft);
                    }

                    if (oldPhotoPath != null)
                    {
                        _fileService.RemoveImage(oldPhotoPath);
                    }

                    if (oldFilePath != null)
                    {
                        _fileService.RemoveFile(oldFilePath);
                    }

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Dynamic Page > Card > Warning", newSectionCardVersion.EnTitle);

                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    return RedirectToAction("Index", new
                    {
                        id = newSectionCardVersion.PageSectionVersionId,
                        pageRouteVersionId = newSectionCardVersion.PageSectionVersion.PageRouteVersionId
                    });
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Dynamic Page > Card > Warning", sectionCardVersion.EnTitle);

                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                }
            }


            return View(sectionCardViewModel);
        }

        /// <summary>
        /// Get method for details a section card
        /// </summary>
        /// <param name="id">section card id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Details(int id, int pageRouteVersionId, int approvalId)
        {
            var pageSectionCardVersion = _sectionCardVersionRepository.Get(id, pageRouteVersionId);
            pageSectionCardVersion.ApprovalId = approvalId;

            var subApplicationName = _configuration.GetSection("SubApplicationName").Value;
            if (!string.IsNullOrWhiteSpace(subApplicationName))
                pageSectionCardVersion.FileUrl = subApplicationName + pageSectionCardVersion.FileUrl;

            return View(pageSectionCardVersion);
        }

        /// <summary>
        /// Delete a page section card by id
        /// </summary>
        /// <param name="id">page section card id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public IActionResult Delete(int id, int pageRouteVersionId, int pagesectionversionid)
        {
            var deleteCardsViewModel = _sectionCardVersionRepository.Delete(id, pageRouteVersionId);

            if (deleteCardsViewModel.pageSectionCardVersion != null)
            {
                if (deleteCardsViewModel.pageSectionCardVersion.PageSectionVersion.PageRouteVersion.StatusId != (int)RequestStatus.Draft)
                {
                    _pageRouteVersionRepository.ChangeStatus(deleteCardsViewModel.pageSectionCardVersion.PageSectionVersion.PageRouteVersionId, RequestStatus.Draft);
                }

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Dynamic Page > Card > Delete", deleteCardsViewModel.pageSectionCardVersion.EnTitle);
                TempData[notificationMessageKey] = ToasrMessages.DeleteSuccess;
                TempData[notificationTypeKey] = notificationSuccess;
                return Json(new { link = $"/DynamicSectionCard/index/{deleteCardsViewModel.Link}" });
            }
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Dynamic Page > Card > Warning", "" + pageRouteVersionId);
            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            return Json(new { link = $"/DynamicSectionCard/index/{pagesectionversionid}?pageRouteVersionId={pageRouteVersionId}" });
        }

        /// <summary>
        /// Get all approved and not deleted section cards for index
        /// </summary>
        /// <param name="id">section version id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetSectionCards(int id, int pageRouteVersionId)
        {
            var cardVersions = _sectionCardVersionRepository.GetCardsBySectionId(id, pageRouteVersionId);
            return Json(new { data = cardVersions });
        }

        /// <summary>
        /// Download file
        /// </summary>
        /// <param name="fileName">file name which i will download</param>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult DownloadFile(string fileName)
        {
            return _fileService.DownloadFile(fileName);
        }
    }
}