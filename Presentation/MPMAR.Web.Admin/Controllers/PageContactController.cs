using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;
using MPMAR.Business;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.Helpers;
using Microsoft.AspNetCore.Authorization;
using MPMAR.Common;
using MPMAR.Data.Consts;
using MPMAR.Common.Interfaces;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class PageContactController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly IPageContactRepository _pageContactRepository;
        private readonly IPageContactVersionRepository _pageContactVersionRepository;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<PageContactController> _eventLogger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        public PageContactController(IPageContactRepository pageContactRepository, IPageContactVersionRepository pageContactVersionRepository, IApprovalNotificationsRepository approvalNotificationsRepository, IToastNotification toastNotification, IEventLogger<PageContactController> eventLogger, UserManager<ApplicationUser> userManager)
        {
            _pageContactRepository = pageContactRepository;
            _pageContactVersionRepository = pageContactVersionRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }

        /// <summary>
        /// Index for griding approved page contact objects
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ContactUs, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index(int id)
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

            PageContact pageSectionVersion = _pageContactRepository.Get(id);
            if (pageSectionVersion == null)
            {
                pageSectionVersion = new PageContact();
                pageSectionVersion.PageRouteVersionId = id;
                return View(pageSectionVersion);
            }

            return View(pageSectionVersion);
        }

        /// <summary>
        /// Get method for creating new page contact object
        /// </summary>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ContactUs, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create(int pageRouteVersionId)
        {
            PageContactEditViewModel viewModel = new PageContactEditViewModel();
            viewModel.PageRouteVersionId = pageRouteVersionId;
            return View(viewModel);
        }

        /// <summary>
        /// Post method for creating new page contact object
        /// </summary>
        /// <param name="pageMinistryViewModel">page contact model data</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ContactUs, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public async Task<IActionResult> CreateAsync(PageContactEditViewModel pageMinistryViewModel)
        {


            if (ModelState.IsValid)
            {
                PageContact sectionCardVersion = pageMinistryViewModel.MapToPageContact();

                var user = await _userManager.GetUserAsync(HttpContext.User);

                sectionCardVersion.CreatedById = user.Id;
                sectionCardVersion.CreationDate = DateTime.Now;


                PageContact newPageSectionCardVersion = _pageContactRepository.Add(sectionCardVersion);
                if (newPageSectionCardVersion != null)
                {

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Contact Us > Page Contact Details > Add", pageMinistryViewModel.EnPageName);
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.AddSuccess);
                    return RedirectToAction("Index", new { id = newPageSectionCardVersion.PageRouteVersionId });
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Contact Us > Page Contact Details > Add", pageMinistryViewModel.EnPageName);
                    _toastNotification.AddWarningToastMessage(ToasrMessages.warning);
                }
            }
            return View(pageMinistryViewModel);
        }

        /// <summary>
        /// Get method for update an existing page contact object
        /// </summary>
        /// <param name="id">page contact id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ContactUs, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id)
        {
            var pcv = _pageContactVersionRepository.GetByPageContactId(id);
            if (pcv == null || pcv.VersionStatusEnum == VersionStatusEnum.Approved || pcv.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                pcv = _pageContactRepository.GetByPageContactId(id);
            }
            var mapped = pcv.MapToPageContactVersionViewModel();

            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.PageContact);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;

            return View(mapped);
        }

        /// <summary>
        /// Post method for update an existing page contact object
        /// </summary>
        /// <param name="ViewModel">page contact model new data</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ContactUs, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public async Task<IActionResult> Edit(PageContactEditViewModel ViewModel)
        {
            ViewModel.SeoDescriptionAR.ValidateHtml("SeoDescriptionAR", ModelState);
            ViewModel.SeoDescriptionEN.ValidateHtml("SeoDescriptionEN", ModelState);

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var pcv = _pageContactVersionRepository.GetByPageContactId(ViewModel.Id);
                var pageContactVersion = ViewModel.MapToPageContactVersionModel();

                if (pcv == null || ViewModel.VersionStatusEnum == VersionStatusEnum.Approved || ViewModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    pageContactVersion.CreatedById = user.Id;
                    pageContactVersion.CreationDate = DateTime.Now;
                    pageContactVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                    pageContactVersion.ChangeActionEnum = ChangeActionEnum.Update;
                    pageContactVersion.Id = 0;
                    pageContactVersion.PageContactId = ViewModel.Id;
                    pageContactVersion.PageRouteVersionId = ViewModel.PageRouteVersionId != 0 ? (int?)ViewModel.PageRouteVersionId : null;
                    _pageContactVersionRepository.Add(pageContactVersion);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Contact Us > Page Contact Details > Edit", ViewModel.EnPageName);

                    return RedirectToAction(nameof(Details));
                }

                pageContactVersion.CreationDate = DateTime.Now;
                pageContactVersion.Id = pcv.Id;
                pageContactVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                pageContactVersion.ChangeActionEnum = ChangeActionEnum.Update;
                pageContactVersion.ApprovalDate = pcv.ApprovalDate;
                pageContactVersion.ApprovedById = pcv.ApprovedById;
                pageContactVersion.CreatedById = pcv.CreatedById;
                pageContactVersion.CreationDate = pcv.CreationDate;
                pageContactVersion.ModificationDate = pcv.ModificationDate;
                pageContactVersion.ModifiedById = pcv.ModifiedById;
                pageContactVersion.PageContactId = pcv.PageContactId;
                pageContactVersion.PageRouteVersionId = pcv.PageRouteVersionId;
                var update = _pageContactVersionRepository.Update(pageContactVersion);
                if (update)
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Contact Us > Page Contact Details > Edit", ViewModel.EnPageName);
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    return RedirectToAction(nameof(Details));
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Contact Us > Page Contact Details > Edit", ViewModel.EnPageName);
                    return View(ViewModel);
                }
            }
            return View(ViewModel);
        }

        /// <summary>
        /// Submit changes that send notification to approval user with last changes
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ContactUs, new PrivilegesActions[] { PrivilegesActions.CanEdit, PrivilegesActions.CanAdd, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> SubmitChanges()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var pcv = _pageContactVersionRepository.GetAllDrafts();
            foreach (var record in pcv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                record.ModifiedById = user.Id;
                record.ModificationDate = DateTime.Now;
                _pageContactVersionRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.PageContact);
            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(PageContactController)[0..^10]}" + "/Details",
                    PageName = PagesNamesConst.PageContact,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);
            }

            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Contact Us > Page Contact Details > Submitted", " Submitted");

            return RedirectToAction(nameof(Details));
        }

        /// <summary>
        /// Approve method that allows approval user to approve last changes to appears in website
        /// </summary>
        /// <param name="approvalId">notification approved id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ContactUs, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var pcv = _pageContactVersionRepository.GetAllSubmitted();

            foreach (var record in pcv)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;
                _pageContactVersionRepository.Update(record);

                var pageContactObj = new PageContact()
                {
                    Id = record.PageContactId ?? 0,
                    ApprovalDate = record.ApprovalDate,
                    ApprovedById = record.ApprovedById,
                    CreatedById = record.CreatedById,
                    CreationDate = record.CreationDate,
                    IsActive = record.IsActive,
                    IsDeleted = record.IsDeleted,
                    ModificationDate = record.ModificationDate,
                    ModifiedById = record.ModifiedById,
                    PageRouteVersionId = record.PageRouteVersionId??0,
                    ArAddress = record.ArAddress,
                    ArMapTitle = record.ArMapTitle,
                    PhoneNumber = record.PhoneNumber,
                    Order = record.Order,
                    MapUrl = record.MapUrl,
                    FaxNumber = record.FaxNumber,
                    ArPageName = record.ArPageName,
                    ArParticipateTitle = record.ArParticipateTitle,
                    EmailParticipateEmail = record.EmailParticipateEmail,
                    EnAddress = record.EnAddress,
                    EnMapTitle = record.EnMapTitle,
                    EnPageName = record.EnPageName,
                    EnParticipateTitle = record.EnParticipateTitle,
                    FormParticipateActive = record.FormParticipateActive,
                    SeoDescriptionAR = record.SeoDescriptionAR,
                    SeoTwitterCardEN = record.SeoTwitterCardEN,
                    SeoTwitterCardAR = record.SeoTwitterCardAR,
                    SeoTitleEN = record.SeoTitleEN,
                    SeoTitleAR = record.SeoTitleAR,
                    SeoOgTitleEN = record.SeoOgTitleEN,
                    SeoOgTitleAR = record.SeoOgTitleAR,
                    SeoDescriptionEN = record.SeoDescriptionEN,

                };
                _pageContactRepository.Update(pageContactObj);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.PageContact);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Contact Us > Page Contact Details > Approve", " Id :" + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Ignore method that allows approval user to ignore last changes.
        /// </summary>
        /// <param name="approvalId">notification approved id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ContactUs, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var pcv = _pageContactVersionRepository.GetAllSubmitted();

            foreach (var record in pcv)
            {
                record.ModificationDate = DateTime.Now;
                record.ModifiedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _pageContactVersionRepository.Update(record);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.PageContact);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Contact Us > Page Contact Details > Reject", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// get details page of PageContact
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ContactUs, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Details()
        {
            var pageContactVersions = _pageContactVersionRepository.GetPageContactVersions().FirstOrDefault();
            return View(pageContactVersions);
        }
        /// <summary>
        /// delet PageContact
        /// </summary>
        /// <param name="id">PageContact id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ContactUs, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public IActionResult Delete(int id)
        {
            PageContact pageSectionCardVersion = _pageContactRepository.Delete(id);

            if (pageSectionCardVersion != null)
            {
                TempData[notificationMessageKey] = "Element has been deleted successfully. </br> It will take effect after admin approval.";
                TempData[notificationTypeKey] = notificationSuccess;

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Contact Us > Page Contact Details > Delete", " Id :" + id);
                return Json(new { });
            }

            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Contact Us > Page Contact Details > Delete", " Id :" + id);
            return Json(new { });
        }

        /// <summary>
        /// get PageContact 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ContactUs, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetPageContact(int id)
        {
            var pageMinistry = _pageContactRepository.GetPageContactByPageId(id);
            var pageMinistryViewModel = pageMinistry.MapToPageContactViewModel();
            return Json(new { data = pageMinistryViewModel });
        }
    }
}