using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Web.Admin.ViewModels;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Common.Helpers;
using NToastNotify;
using MPMAR.Business;
using MPMAR.Data;
using Microsoft.AspNetCore.Identity;
using MPMAR.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using MPMAR.Common;
using MPMAR.Data.HomePageModels;
using MPMAR.Data.Consts;
using MPMAR.Common.Interfaces;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class HP_PublicationsController : Controller
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly IPublicationVersionsRepository _publicationVersionsRepository;
        private readonly IFileService _fileService;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<HP_PublicationsController> _eventLogger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;

        public HP_PublicationsController(IPublicationRepository publicationRepository, IPublicationVersionsRepository publicationVersionsRepository,
            IFileService fileService, IToastNotification toastNotification, IEventLogger<HP_PublicationsController> eventLogger, UserManager<ApplicationUser> userManager, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _publicationRepository = publicationRepository;
            _publicationVersionsRepository = publicationVersionsRepository;
            _fileService = fileService;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _userManager = userManager;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }

        /// <summary>
        /// Index for griding all approved publication objects
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPublications, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery]int approvalId)
        {
            ViewBag.approvalId = approvalId;

            return View();
        }

        /// <summary>
        /// Get all publication objects for index
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPublications, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetAll()
        {
            var data = _publicationVersionsRepository.GetpublicationVersions().FirstOrDefault();
            List<HomeCommonViewModel> viewModel = new List<HomeCommonViewModel>();
            if (data != null)
            {
                viewModel.Add(new HomeCommonViewModel()
                {
                    Id = data.Id,
                    ArTitle = data.ArMainTitle,
                    EnTitle = data.EnMainTitle,
                    ArDescription = "",
                    EnDescription = "",
                    Order = 0,
                    Type = HomeTypeEnum.Title
                });
                viewModel.Add(new HomeCommonViewModel()
                {
                    Id = data.Id,
                    ArTitle = data.ArTitle1,
                    EnTitle = data.EnTitle1,
                    ArDescription = data.ArDescription1,
                    EnDescription = data.EnDescription1,
                    Order = 1,
                    Type = HomeTypeEnum.Detail
                });
                viewModel.Add(new HomeCommonViewModel()
                {
                    Id = data.Id,
                    ArTitle = data.ArTitle2,
                    EnTitle = data.EnTitle2,
                    ArDescription = data.ArDescription2,
                    EnDescription = data.EnDescription2,
                    Order = 2,
                    Type = HomeTypeEnum.Detail
                });
                viewModel.Add(new HomeCommonViewModel()
                {
                    Id = data.Id,
                    ArTitle = data.ArTitle3,
                    EnTitle = data.EnTitle3,
                    ArDescription = data.ArDescription3,
                    EnDescription = data.EnDescription3,
                    Order = 3,
                    Type = HomeTypeEnum.Detail
                });
            }


            return Json(new { data = viewModel.OrderBy(x => x.Order) });
        }

        /// <summary>
        /// Get method for creating new publication object
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPublications, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create()
        {

            return View();
        }

        /// <summary>
        /// Post method for creating new publication object
        /// </summary>
        /// <param name="viewModel">publication model</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPublications, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        [RequestSizeLimit(500000000)]
        public IActionResult Create(PublicationViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var model = viewModel.MapToPublicationModel();
                if (viewModel.ImageFile1 != null)
                    model.Image1 = _fileService.UploadImageUrlNew(viewModel.ImageFile1);
                if (viewModel.ImageFile2 != null)
                    model.Image2 = _fileService.UploadImageUrlNew(viewModel.ImageFile2);
                if (viewModel.ImageFile3 != null)
                    model.Image3 = _fileService.UploadImageUrlNew(viewModel.ImageFile3);
                _publicationRepository.Add(model);
                _toastNotification.AddSuccessToastMessage(ToasrMessages.AddSuccess);
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Home Page > Publication > Add", viewModel.EnTitle1);

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        /// <summary>
        /// Get method for updating an existing publication object
        /// </summary>
        /// <param name="id">publication id</param>
        /// <param name="order">order of choosen object to update</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPublications, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery]int order, [FromQuery]int approvalId)
        {
            if (order <= 0 && order >= 3)
                return NotFound();
            var pv = _publicationVersionsRepository.GetByPublicationId(id);
            if (pv == null || pv.VersionStatusEnum == VersionStatusEnum.Approved || pv.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                pv = _publicationRepository.GetByPublicationId(id);
            }
            var mapped = pv.MapToPublicationVersionViewModel();
            ViewBag.order = order;
            mapped.Order = order;
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.Publication);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            ViewBag.approvalId = approvalId;
            return View(mapped);
        }

        /// <summary>
        /// Post method for updating an exisiting publication object
        /// </summary>
        /// <param name="viewModel">publication model new data</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPublications, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Edit(PublicationViewModel viewModel)
        {
            viewModel.ArDescription1.ValidateHtml("ArDescription1", ModelState);
            viewModel.ArDescription2.ValidateHtml("ArDescription2", ModelState);
            viewModel.ArDescription3.ValidateHtml("ArDescription3", ModelState);
            viewModel.EnDescription1.ValidateHtml("EnDescription1", ModelState);
            viewModel.EnDescription2.ValidateHtml("EnDescription2", ModelState);
            viewModel.EnDescription3.ValidateHtml("EnDescription3", ModelState);

            ModelState.Remove(nameof(viewModel.ImageFile1));
            ModelState.Remove(nameof(viewModel.ImageFile2));
            ModelState.Remove(nameof(viewModel.ImageFile3));

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var pv = _publicationVersionsRepository.GetByPublicationId(viewModel.Id);
                var PublicationVersion = viewModel.MapToPublicationVersionModel();

                if (pv == null || viewModel.VersionStatusEnum == VersionStatusEnum.Approved || viewModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    PublicationVersion.CreatedById = user.Id;
                    PublicationVersion.CreationDate = DateTime.Now;
                    PublicationVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                    PublicationVersion.ChangeActionEnum = ChangeActionEnum.Update;
                    PublicationVersion.Id = 0;
                    PublicationVersion.PublicationId = viewModel.Id;
                    if (viewModel.ImageFile1 != null)
                        PublicationVersion.Image1 = _fileService.UploadImageUrlNew(viewModel.ImageFile1);
                    if (viewModel.ImageFile2 != null)
                        PublicationVersion.Image2 = _fileService.UploadImageUrlNew(viewModel.ImageFile2);
                    if (viewModel.ImageFile3 != null)
                        PublicationVersion.Image3 = _fileService.UploadImageUrlNew(viewModel.ImageFile3);
                    _publicationVersionsRepository.Add(PublicationVersion);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Publication > Update", viewModel.EnTitle1);
                    return RedirectToAction(nameof(Index));
                }

                if (viewModel.ImageFile1 != null)
                    PublicationVersion.Image1 = _fileService.UploadImageUrlNew(viewModel.ImageFile1);
                if (viewModel.ImageFile2 != null)
                    PublicationVersion.Image2 = _fileService.UploadImageUrlNew(viewModel.ImageFile2);
                if (viewModel.ImageFile3 != null)
                    PublicationVersion.Image3 = _fileService.UploadImageUrlNew(viewModel.ImageFile3);
                PublicationVersion.CreationDate = DateTime.Now;
                PublicationVersion.Id = pv.Id;
                PublicationVersion.VersionStatusEnum = VersionStatusEnum.Draft;
                PublicationVersion.ChangeActionEnum = ChangeActionEnum.Update;
                PublicationVersion.ApprovalDate = pv.ApprovalDate;
                PublicationVersion.ApprovedById = pv.ApprovedById;
                PublicationVersion.CreatedById = pv.CreatedById;
                PublicationVersion.CreationDate = pv.CreationDate;
                PublicationVersion.ModificationDate = pv.ModificationDate;
                PublicationVersion.ModifiedById = pv.ModifiedById;
                PublicationVersion.PublicationId = pv.PublicationId;
                var update = _publicationVersionsRepository.Update(PublicationVersion);
                if (update)
                {
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Publication > Update", viewModel.EnTitle1);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                    ViewBag.order = viewModel.Order;
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Home Page > Publication > Warning", viewModel.EnTitle1);
                    return View(viewModel);
                }
            }
            ViewBag.order = viewModel.Order;
            return View(viewModel);
        }


        /// <summary>
        /// submit changes method that send notification to approval user with last changes
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPublications, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanDelete, PrivilegesActions.CanEdit })]
        public async Task<IActionResult> SubmitChanges()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var pv = _publicationVersionsRepository.GetAllDrafts();
            foreach (var record in pv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                record.ModifiedById = user.Id;
                record.ModificationDate = DateTime.Now;
                _publicationVersionsRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.Publication);
            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(HP_PublicationsController)[0..^10]}",
                    PageName = PagesNamesConst.Publication,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);
            }
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Home Page > Publication > Submitted", " Submitted");

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Approve method that allow approval user to approve last changes to allow it to appears in website
        /// </summary>
        /// <param name="id">publication id</param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPublications, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var pv = _publicationVersionsRepository.GetAllSubmitted();

            foreach (var record in pv)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;
                _publicationVersionsRepository.Update(record);

                var pubObj = new Publication()
                {
                    Id = record.PublicationId ?? 0,
                    ApprovalDate = record.ApprovalDate,
                    ApprovedById = record.ApprovedById,
                    CreatedById = record.CreatedById,
                    CreationDate = record.CreationDate,
                    IsActive = record.IsActive,
                    IsDeleted = record.IsDeleted,
                    ModificationDate = record.ModificationDate,
                    ModifiedById = record.ModifiedById,
                    ArMainTitle = record.ArMainTitle,
                    EnMainTitle = record.EnMainTitle,
                    EnDescription1 = record.EnDescription1,
                    ArDescription1 = record.ArDescription1,
                    EnDescription2 = record.EnDescription2,
                    ArDescription2 = record.ArDescription2,
                    EnDescription3 = record.EnDescription3,
                    ArDescription3 = record.ArDescription3,
                    ArTitle1 = record.ArTitle1,
                    ArTitle2 = record.ArTitle2,
                    ArTitle3 = record.ArTitle3,
                    EnTitle1 = record.EnTitle1,
                    EnTitle2 = record.EnTitle2,
                    EnTitle3 = record.EnTitle3,
                    Link1 = record.Link1,
                    Link2 = record.Link2,
                    Link3 = record.Link3,
                    Image1 = record.Image1,
                    Image2 = record.Image2,
                    Image3 = record.Image3
                };
                _publicationRepository.Update(pubObj);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.Publication);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Approved;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Home Page > Publication > Approve", " Id :" + id);
            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Ignore method that allow approval user to ignore last changes
        /// </summary>
        /// <param name="id">publication id</param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPublications, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var pv = _publicationVersionsRepository.GetAllSubmitted();

            foreach (var record in pv)
            {
                record.ModificationDate = DateTime.Now;
                record.ModifiedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _publicationVersionsRepository.Update(record);
            }

            var approvalItem = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.Publication);
            approvalItem.VersionStatusEnum = VersionStatusEnum.Ignored;
            approvalItem.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approvalItem);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Home Page > Publication > Reject", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Delete publication object
        /// </summary>
        /// <param name="id">publication id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPPublications, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public bool Delete(int id)
        {
            try
            {
                var model = _publicationRepository.GetById(id);
                model.IsDeleted = true;
                _publicationRepository.Update(model);
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Home Page > Publication > Delete", " Id :" + id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}