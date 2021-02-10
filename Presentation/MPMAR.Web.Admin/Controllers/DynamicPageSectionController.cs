using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using MPMAR.Business.Interfaces;
using MPMAR.Business.ViewModels;
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
    public class DynamicPageSectionController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;
        private readonly IPageSectionRepository _dynamicPageSectionRepository;
        private readonly IPageSectionVersionRepository _dynamicPageSectionVersionRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<DynamicPageSectionController> _eventLogger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IFileService _fileService;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        private readonly IPageSectionVersionRepository _pageSectionVersionRepository;
        private readonly IPageSectionRepository _pageSectionRepository;
        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly IGlobalElasticSearchService _globalElasticSearchService;
        private readonly HTMLFileHelper _htmlHelper;

        public DynamicPageSectionController(IPageSectionRepository pageSectionRepository, IPageSectionVersionRepository pageSectionVersionRepository,
            IApprovalNotificationsRepository approvalNotificationsRepository, IPageRouteVersionRepository pageRouteVersionRepository,
            IPageSectionRepository dynamicPageSectionRepository, IPageSectionVersionRepository dynamicPageSectionVersionRepository,
            UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<DynamicPageSectionController> eventLogger, IWebHostEnvironment hostingEnvironment,
            IFileService fileService, HTMLFileHelper htmlHelper, IPageRouteRepository pagRouteRepository, IGlobalElasticSearchService globalElasticSearchService)
        {
            _pageRouteVersionRepository = pageRouteVersionRepository;
            _dynamicPageSectionRepository = dynamicPageSectionRepository;
            _dynamicPageSectionVersionRepository = dynamicPageSectionVersionRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _hostingEnvironment = hostingEnvironment;
            _fileService = fileService;
            _approvalNotificationsRepository = approvalNotificationsRepository;
            _pageSectionVersionRepository = pageSectionVersionRepository;
            _pageSectionRepository = pageSectionRepository;
            _htmlHelper = htmlHelper;
            _pageRouteRepository = pagRouteRepository;
            _globalElasticSearchService = globalElasticSearchService;
        }

        /// <summary>
        /// index for griding dynamic page section objects
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <param name="approvalId">approved notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index(int pageRouteVersionId, [FromQuery] int approvalId)
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

            PageRouteVersion pageRouteVersion = _pageRouteVersionRepository.Get(pageRouteVersionId);
            var notification = _approvalNotificationsRepository.GetByRelatedIdAndType(pageRouteVersion.Id,ChangeType.PageContent);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            if (pageRouteVersion == null)
            {
                return NotFound();
            }
            ViewBag.DynamicPageName = pageRouteVersion.EnName;
            ViewBag.approvalId = approvalId;
            ViewBag.pageRouteVersionId = pageRouteVersionId;

            return View(pageRouteVersion.Id);
        }

        /// <summary>
        /// Get method for create new dynamic page section
        /// </summary>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create(int pageRouteVersionId)
        {
            List<PageSectionType> sectionTypes = _dynamicPageSectionRepository.GetPageSectionTypes();
            PageSectionCreateViewModel viewModel = new PageSectionCreateViewModel(sectionTypes);
            viewModel.pageRouteVersionId = pageRouteVersionId;
            PageRouteVersion pageRouteVersion = _pageRouteVersionRepository.Get(pageRouteVersionId);
            var notification = _approvalNotificationsRepository.GetByRelatedIdAndType(pageRouteVersion.Id, ChangeType.PageContent);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            if (pageRouteVersion == null)
            {
                return NotFound();
            }
            ViewBag.DynamicPageName = pageRouteVersion.EnName;
            return View(viewModel);
        }

        /// <summary>
        /// Post method for create new dynamic page section objects
        /// </summary>
        /// <param name="sectionViewModel">page section object data</param>
        /// <returns></returns>
        [HttpPost]
        //[BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        [Authorize]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Create(PageSectionCreateViewModel sectionViewModel)
        {
            List<PageSectionType> sectionTypes;

            RemoveFieldsFromModelStateDependOnSectionType(ModelState, sectionViewModel.SectionTypeId ?? 0);

            sectionViewModel.Section.EnDescription.ValidateHtml("EnDescription", ModelState);
            sectionViewModel.Section.ArDescription.ValidateHtml("ArDescription", ModelState);
            PageSectionType sectionType = null;
            if (sectionViewModel.SectionTypeId != null)
            {
                sectionType = _dynamicPageSectionRepository.GetPageSectionType(sectionViewModel.SectionTypeId.Value);
                if (sectionType.MediaType == SectionMediaType.Image.ToString() && sectionViewModel.Section.Photo == null)
                {
                    ModelState.AddModelError(nameof(sectionViewModel.Section.Photo), "you should uplaod a photo.");
                    _toastNotification.AddWarningToastMessage("you should uplaod a photo.");
                }
            }


            if (ModelState.IsValid)
            {
                sectionViewModel = RemoveTagsFromDescription(sectionViewModel);

                PageSectionVersion pageSectionVersion = sectionViewModel.MapToPageSectionVersion();


                if (sectionType.MediaType == SectionMediaType.Image.ToString())
                {
                    pageSectionVersion.Url = _fileService.UploadImageUrlNew(sectionViewModel.Section.Photo);
                }
                var user = await _userManager.GetUserAsync(HttpContext.User);

                pageSectionVersion.CreatedById = user.Id;
                pageSectionVersion.CreationDate = DateTime.Now;

                PageSectionVersion newDynamicPageSectionVersion = _dynamicPageSectionVersionRepository.Add(pageSectionVersion);
                if (newDynamicPageSectionVersion != null)
                {

                    _toastNotification.AddSuccessToastMessage(ToasrMessages.AddSuccess);
                    if (sectionViewModel.submit != null)
                    {
                        return RedirectToAction("Index", "DynamicSectionCard", new { id = newDynamicPageSectionVersion.Id, pageRouteVersionId = newDynamicPageSectionVersion.PageRouteVersionId });
                    }

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Dynamic Page > " + ViewBag.DynamicPageName + " > Sections > Add ", pageSectionVersion.EnTitle);

                    return RedirectToAction("Index", new { pageRouteVersionId = newDynamicPageSectionVersion.PageRouteVersionId });
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Dynamic Page > " + ViewBag.DynamicPageName + " > Sections > Warning in Add", pageSectionVersion.EnTitle);
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);

                    sectionTypes = _dynamicPageSectionRepository.GetPageSectionTypes();
                    sectionViewModel = new PageSectionCreateViewModel(sectionTypes);

                    return View(sectionViewModel);
                }
            }
            sectionTypes = _dynamicPageSectionRepository.GetPageSectionTypes();
            var pageRouteVer = sectionViewModel.pageRouteVersionId;
            sectionViewModel = new PageSectionCreateViewModel(sectionTypes);
            sectionViewModel.pageRouteVersionId = pageRouteVer;

            return View(sectionViewModel);
        }

        /// <summary>
        /// Get method for update a dynamic page section
        /// </summary>
        /// <param name="id">dynamic page section id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, int pageRouteVersionId)
        {

            SectionViewModel sectionVm = _dynamicPageSectionVersionRepository.Get(id, pageRouteVersionId);


            List<PageSectionType> sectionTypes = _dynamicPageSectionRepository.GetPageSectionTypes();
            PageSectionEditViewModel viewModel = new PageSectionEditViewModel(sectionTypes, sectionVm);
            viewModel.Id = id;
            viewModel.pageRouteVersionId = pageRouteVersionId;
            PageRouteVersion pageRouteVersion = _pageRouteVersionRepository.Get(pageRouteVersionId);
            var notification = _approvalNotificationsRepository.GetByRelatedIdAndType(pageRouteVersion.Id, ChangeType.PageContent);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            if (pageRouteVersion == null)
            {
                return NotFound();
            }
            ViewBag.DynamicPageName = pageRouteVersion.EnName;
            return View(viewModel);
        }

        /// <summary>
        /// Post method for update a dynamic page section
        /// </summary>
        /// <param name="sectionViewModel">page section object data</param>
        /// <returns></returns>
        [HttpPost]
        //[BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        [Authorize]
        [RequestSizeLimit(500000000)]
        public IActionResult Edit(PageSectionEditViewModel sectionViewModel)
        {
            RemoveFieldsFromModelStateDependOnSectionType(ModelState, sectionViewModel.SectionTypeId.Value);

            sectionViewModel.Section.EnDescription.ValidateHtml("EnDescription", ModelState);
            sectionViewModel.Section.ArDescription.ValidateHtml("ArDescription", ModelState);

            if (ModelState.IsValid)
            {
                sectionViewModel = RemoveTagsFromDescription(sectionViewModel);

                PageSectionVersion pageSectionVersion = sectionViewModel.MapToPageSectionVersion();

                string oldFilePath = null;
                var sectionType = _dynamicPageSectionRepository.GetPageSectionType(sectionViewModel.SectionTypeId.Value);
                if (sectionType.MediaType == SectionMediaType.Image.ToString() && sectionViewModel.Section.Photo != null)
                {
                    oldFilePath = pageSectionVersion.Url;
                    pageSectionVersion.Url = _fileService.UploadImageUrlNew(sectionViewModel.Section.Photo);
                }
                if (sectionType.MediaType == SectionMediaType.Video.ToString())
                {
                    pageSectionVersion.Url = sectionViewModel.Section.Url;
                }

                PageSectionVersion newPageSectionVersion = _dynamicPageSectionVersionRepository.Update(pageSectionVersion);
                if (newPageSectionVersion != null)
                {


                    if (oldFilePath != null)
                    {
                        _fileService.RemoveImage(oldFilePath);
                    }
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Dynamic Page > " + ViewBag.DynamicPageName + " > Sections > Edit", newPageSectionVersion.EnTitle);

                    return RedirectToAction("Index", new { pageRouteVersionId = newPageSectionVersion.PageRouteVersionId });
                }
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Dynamic Page > " + ViewBag.DynamicPageName + " > Sections > Warning in Edit", pageSectionVersion.EnTitle);
                _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                return View(sectionViewModel);
            }

            List<PageSectionType> sectionTypes = _dynamicPageSectionRepository.GetPageSectionTypes();
            sectionViewModel.SectionTypes = sectionTypes;

            return View(sectionViewModel);
        }

        /// <summary>
        /// Remove Tags From Description in edit method
        /// </summary>
        /// <param name="sectionViewModel">page section object data</param>
        /// <returns></returns>
        private PageSectionEditViewModel RemoveTagsFromDescription(PageSectionEditViewModel sectionViewModel)
        {
            if (sectionViewModel.Section.ArDescription != null)
            {
                sectionViewModel.Section.ArDescription = sectionViewModel.Section.ArDescription.Replace("<p>", "");
                sectionViewModel.Section.ArDescription = sectionViewModel.Section.ArDescription.Replace("</p>", "");
            }
            if (sectionViewModel.Section.EnDescription != null)
            {
                sectionViewModel.Section.EnDescription = sectionViewModel.Section.EnDescription.Replace("<p>", "");
                sectionViewModel.Section.EnDescription = sectionViewModel.Section.EnDescription.Replace("</p>", "");
            }
            return sectionViewModel;
        }

        /// <summary>
        /// Remove Tags From Description in create method
        /// </summary>
        /// <param name="sectionViewModel">page section object data</param>
        /// <returns></returns>
        private PageSectionCreateViewModel RemoveTagsFromDescription(PageSectionCreateViewModel sectionViewModel)
        {
            if (sectionViewModel.Section.ArDescription != null)
            {
                sectionViewModel.Section.ArDescription = sectionViewModel.Section.ArDescription.Replace("<p>", "");
                sectionViewModel.Section.ArDescription = sectionViewModel.Section.ArDescription.Replace("</p>", "");
            }
            if (sectionViewModel.Section.EnDescription != null)
            {
                sectionViewModel.Section.EnDescription = sectionViewModel.Section.EnDescription.Replace("<p>", "");
                sectionViewModel.Section.EnDescription = sectionViewModel.Section.EnDescription.Replace("</p>", "");
            }

            return sectionViewModel;
        }

        /// <summary>
        /// Get method for details a dynamic page section
        /// </summary>
        /// <param name="id">page section id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Details(int id, int pageRouteVersionId, int approvalId)
        {
            SectionViewModel pageSectionVersion = _dynamicPageSectionVersionRepository.Get(id, pageRouteVersionId);

            var pageRouteVersion = _pageRouteVersionRepository.GetById(pageSectionVersion.PageRouteVersionId);

            var dynamicPageSectionDetailsViewModel = new DynamicPageSectionDetailsViewModel()
            {
                PageRouteVersion = pageRouteVersion,
                SectionViewModel = pageSectionVersion
            };
            dynamicPageSectionDetailsViewModel.ApprovalId = approvalId;
            return View(dynamicPageSectionDetailsViewModel);
        }

        /// <summary>
        /// Delete a page section from db
        /// </summary>
        /// <param name="id">page section id</param>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public IActionResult Delete(int id, int pageRouteVersionId)
        {
            PageSectionVersion pageSectionVersion = _dynamicPageSectionVersionRepository.Delete(id, pageRouteVersionId);

            if (pageSectionVersion != null)
            {
                if (pageSectionVersion.PageRouteVersion.StatusId != (int)RequestStatus.Draft)
                {
                    _pageRouteVersionRepository.ChangeStatus(pageSectionVersion.PageRouteVersionId, RequestStatus.Draft);
                }

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Dynamic Page > " + ViewBag.DynamicPageName + " > Sections > Delete", pageSectionVersion.EnTitle);

                TempData[notificationMessageKey] = ToasrMessages.DeleteSuccess;
                TempData[notificationTypeKey] = notificationSuccess;
                return Json(new { });
            }
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Dynamic Page > " + ViewBag.DynamicPageName + " > Sections > Warning in Delete", pageSectionVersion.EnTitle);
            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            return Json(new { });
        }

        /// <summary>
        /// Get page sections for griding in index
        /// </summary>
        /// <param name="id">page route id</param>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetPageSections(int pageRouteVersionId)
        {
            var sectionVersions = _dynamicPageSectionVersionRepository.GetPageSectionsByPageRouteId(pageRouteVersionId);
            return Json(new { data = sectionVersions });
        }

        /// <summary>
        /// Get page section types
        /// </summary>
        /// <param name="id">page route id</param>
        /// <returns></returns>
        /// 
        [Authorize]
        public JsonResult GetSectionTypes(int id)
        {
            var sectionType = _dynamicPageSectionRepository.GetPageSectionType(id);
            return Json(new { data = sectionType });
        }

        /// <summary>
        /// Remove Fields From Model State Depend On Section Type
        /// </summary>
        /// <param name="modelState">Model data</param>
        /// <param name="sectionId">page section id</param>
        [NonAction]
        private void RemoveFieldsFromModelStateDependOnSectionType(ModelStateDictionary modelState, int sectionId)
        {
            PageSectionType sectionType = _dynamicPageSectionRepository.GetPageSectionType(sectionId);
            if (sectionId > 0)
            {
                if (sectionType != null)
                {
                    if (sectionType.MediaType == "None")
                    {
                        modelState.Remove("Section.Url");
                        modelState.Remove("Section.Photo");
                        modelState.Remove("Section.EnImageAlt");
                        modelState.Remove("Section.ArImageAlt");
                    }

                    if (sectionType.MediaType == "Video")
                    {
                        modelState.Remove("Section.Photo");
                        modelState.Remove("Section.EnImageAlt");
                        modelState.Remove("Section.ArImageAlt");
                    }

                    if (sectionType.MediaType == "Image")
                    {
                        modelState.Remove("Section.Url");
                    }
                }
            }
        }

        /// <summary>
        /// Approve changes from approval user
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <param name="approvalId">approvad notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery] int approvalId)
        {
            var pageRouteVersion = _pageRouteVersionRepository.Get(id);
            if (pageRouteVersion.PageRouteId == null)
            {
                _toastNotification.AddErrorToastMessage("you must approve basic info first.");
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Dynamic Page > " + ViewBag.DynamicPageName + " > Sections > Approve", "you must approve basic info first.");
            }
            else
            {

                UpdatePageRouteVersionStatus(pageRouteVersion, VersionStatusEnum.Approved);

                var user = await _userManager.GetUserAsync(HttpContext.User);

                var pageSections = _pageSectionVersionRepository.GetPageSections(pageRouteVersion.PageRouteId ?? 0);
                foreach (var section in pageSections)
                {
                    _pageSectionRepository.SoftDelete(section.Id);
                }
                var psvs = _pageSectionVersionRepository.GetPageSectionVersions(id);

                foreach (var record in psvs)
                {
                    record.ApprovalDate = DateTime.Now;
                    record.ApprovedById = user.Id;

                    PageSection pageSection = new PageSection()
                    {
                        ApprovalDate = DateTime.Now,
                        ApprovedById = user.Id,
                        ArDescription = record.ArDescription,
                        ArImageAlt = record.ArImageAlt,
                        ArTitle = record.ArTitle,
                        CreatedById = user.Id,
                        CreationDate = record.CreationDate,
                        EnImageAlt = record.EnImageAlt,
                        EnDescription = record.EnDescription,
                        EnTitle = record.EnTitle,
                        IsActive = record.IsActive,
                        IsDeleted = record.IsDeleted,
                        Url = record.Url,
                        PageSectionVersionId = record.Id,
                        PageSectionTypeId = record.PageSectionTypeId,
                        ModificationDate = DateTime.Now,
                        ModifiedById = user.Id,
                        PageRouteId = record.PageRouteVersion.PageRouteId ?? 0,
                        Order = record.Order,
                    };

                    pageSection.PageSectionCards = new List<PageSectionCard>();
                    foreach (var card in record.PageSectionCardVersions)
                    {
                        pageSection.PageSectionCards.Add(new PageSectionCard
                        {
                            ApprovalDate = DateTime.Now,
                            ApprovedById = user.Id,
                            ArDescription = card.ArDescription,
                            ArImageAlt = card.ArImageAlt,
                            ArTitle = card.ArTitle,
                            CreatedById = card.CreatedById,
                            CreationDate = card.CreationDate,
                            EnDescription = card.EnDescription,
                            EnImageAlt = card.EnImageAlt,
                            EnTitle = card.EnTitle,
                            FileUrl = card.FileUrl,
                            ImageUrl = card.ImageUrl,
                            IsActive = card.IsActive,
                            IsDeleted = card.IsDeleted,
                            Order = card.Order,
                            PageSectionId = pageSection.Id
                        });
                    }
                    _pageSectionRepository.Add(pageSection);
                    record.PageSectionId = pageSection.Id;
                    _pageSectionVersionRepository.NormalUpdate(record);
                }

                var prv = _pageRouteVersionRepository.Get(id);
                _pageRouteVersionRepository.ApprovePageRoute(prv, ChangeType.PageContent);

                var approvalItem = _approvalNotificationsRepository.GetById(approvalId);
                if (approvalItem == null)
                {
                    _toastNotification.AddWarningToastMessage("This Notification Has Been Deleted");
                    return RedirectToAction("Index", "ApprovalNotifications");
                }
                approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
                approvalItem.ChangesDateTime = DateTime.Now;
                _approvalNotificationsRepository.Update(approvalItem);

                var pr = _pageRouteRepository.Get(prv.PageRouteId.Value);
                //generate html dynamic page
                _htmlHelper.ApplyPageChanges(prv.Id, pr);
                _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Dynamic Page > " + ViewBag.DynamicPageName + " > Sections > Approve", "Approved");

                try
                {
                    await _globalElasticSearchService.DeleteAsync(pr.Id);
                    await _globalElasticSearchService.AddAsync(_pageRouteRepository.GetPageData(pr.Id));
                }
                catch { }
            }

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Ignore changes from approval user
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <param name="approvalId">approvad notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.DynamicPageSection, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery] int approvalId)
        {
            var prv = _pageRouteVersionRepository.Get(id);
            if (prv.PageRouteId == null)
            {
                _toastNotification.AddErrorToastMessage("you must approve basic info first.");
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Dynamic Page > " + ViewBag.DynamicPageName + " > Sections > Approve", "you must approve basic info first.");
            }
            else
            {
                UpdatePageRouteVersionStatus(prv, VersionStatusEnum.Ignored);

                var approvalItem = _approvalNotificationsRepository.GetById(approvalId);

                prv.VersionStatusEnum = VersionStatusEnum.Ignored;

                if (prv.ChangeActionEnum == ChangeActionEnum.New )
                {
                    prv.VersionStatusEnum = VersionStatusEnum.Approved;
                }

                var user = await _userManager.GetUserAsync(HttpContext.User);



                _pageRouteVersionRepository.Update(prv);


                if (approvalItem == null)
                {
                    _toastNotification.AddWarningToastMessage("This Notification Has Been Deleted");
                    return RedirectToAction("Index", "ApprovalNotifications");
                }
                approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
                approvalItem.ChangesDateTime = DateTime.Now;
                _approvalNotificationsRepository.Update(approvalItem);

                _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);


                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Dynamic Page > " + ViewBag.DynamicPageName + " > Sections > Reject", "Rejected");
            }

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        private static void UpdatePageRouteVersionStatus(PageRouteVersion prv, VersionStatusEnum versionStatusEnum)
        {
            if (prv.VersionStatusEnum == VersionStatusEnum.Draft || prv.VersionStatusEnum == VersionStatusEnum.Submitted)
            {
                prv.VersionStatusEnum = VersionStatusEnum.Submitted;
            }
            prv.ContentVersionStatusEnum = versionStatusEnum;
        }
    }
}