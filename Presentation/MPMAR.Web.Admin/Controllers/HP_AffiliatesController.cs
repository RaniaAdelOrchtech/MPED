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
using NToastNotify;
using MPMAR.Business;
using MPMAR.Common.Interfaces;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class HP_AffiliatesController : Controller
    {
        private readonly IHP_AffiliatesReopsitory _hP_AffiliatesReopsitory;
        private readonly IHP_AffiliatesVersionReopsitory _hP_AffiliatesVersionReopsitory;
        private readonly IFileService _fileService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<HP_AffiliatesController> _eventLogger;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;

        public HP_AffiliatesController(IHP_AffiliatesReopsitory hP_AffiliatesReopsitory, IHP_AffiliatesVersionReopsitory hP_AffiliatesVersionReopsitory, IFileService fileService, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<HP_AffiliatesController> eventLogger, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _hP_AffiliatesReopsitory = hP_AffiliatesReopsitory;
            _hP_AffiliatesVersionReopsitory = hP_AffiliatesVersionReopsitory;
            _fileService = fileService;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }
        
        /// <summary>
        /// Index for grid all affailities in system
        /// </summary>
        /// <param name="pageRouteId">page route id</param>
        /// <param name="approvalId">approved notification id</param>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPAffiliates, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery]int pageRouteId, [FromQuery] int approvalId)
        {
            ViewBag.pageRouteId = pageRouteId;
            ViewBag.approvalId = approvalId;
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPAffiliates);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            return View();
        }

        /// <summary>
        /// Get method for create new affaility object
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPAffiliates, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create()
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPAffiliates);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            return View();
        }

        /// <summary>
        /// Post method for create affaility object
        /// </summary>
        /// <param name="viewModel">affaility model data</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPAffiliates, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Create(HP_AffiliatesViewModel viewModel)
        {
            viewModel.ArDescription.ValidateHtml("ArDescription", ModelState);
            viewModel.EnDescription.ValidateHtml("EnDescription", ModelState);
            viewModel.Type = AffiliatesType.Image;
            if (ModelState.IsValid)
            {
                await EditMethod(viewModel);

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Home Page > Affiliates > Add", viewModel.EnDescription);

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        /// <summary>
        /// Get method for edit existing affaility object
        /// </summary>
        /// <param name="id">affaility version id</param>
        /// <param name="homePageAffiliatesId">affaility id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPAffiliates, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery]int homePageAffiliatesId)
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPAffiliates);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            if (id == null)
                return NotFound();

            HP_AffiliatesViewModel viewModel;
            var affiliatVersion = _hP_AffiliatesVersionReopsitory.GetByAffilitId(homePageAffiliatesId);
            if (affiliatVersion == null || affiliatVersion.VersionStatusEnum == VersionStatusEnum.Approved || affiliatVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                var slider = _hP_AffiliatesReopsitory.GetById(homePageAffiliatesId);
                if (slider != null)
                    viewModel = slider.MapToAffiliatesViewModel();
                else
                {
                    affiliatVersion = _hP_AffiliatesVersionReopsitory.GetById(id);
                    viewModel = affiliatVersion.MapToAffiliatesViewModel();
                }
            }
            else
            {
                viewModel = affiliatVersion.MapToAffiliatesViewModel();
            }
            //remove id value from route
            ModelState.Clear();
            return View(viewModel);
        }

        /// <summary>
        /// Post method for update affaility object
        /// </summary>
        /// <param name="viewModel">affaility object new data</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPAffiliates, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Edit(HP_AffiliatesViewModel viewModel)
        {
            return await EditMethod(viewModel);
        }

        /// <summary>
        /// Core method for editing afiliaty object
        /// </summary>
        /// <param name="viewModel">afiliaty object new data</param>
        /// <returns></returns>
        private async Task<IActionResult> EditMethod(HP_AffiliatesViewModel viewModel)
        {

         

            ModelState.Remove(nameof(viewModel.ImageFile));
            ModelState.Remove(nameof(viewModel.HomePageAffiliatesId));

            if (ModelState.IsValid)
            {
                var affilitVersionBySliderId = _hP_AffiliatesVersionReopsitory.GetByAffilitId(viewModel.HomePageAffiliatesId ?? 0);
                var affilitVersionById = _hP_AffiliatesVersionReopsitory.GetById(viewModel.Id);
                affilitVersionBySliderId = affilitVersionById == null ? affilitVersionBySliderId : affilitVersionById;
                var affilitVersionModel = viewModel.MapToAffiliatesVersionModel();

                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (affilitVersionBySliderId == null || affilitVersionModel.VersionStatusEnum == VersionStatusEnum.Approved || affilitVersionModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    affilitVersionModel.CreatedById = user.Id;
                    affilitVersionModel.CreationDate = DateTime.Now;
                    affilitVersionModel.VersionStatusEnum = VersionStatusEnum.Draft;
                    affilitVersionModel.ChangeActionEnum = ChangeActionEnum.Update;
                    affilitVersionModel.Id = 0;
                    affilitVersionModel.HomePageAffiliatesId = viewModel.HomePageAffiliatesId > 0 ? viewModel.HomePageAffiliatesId : (int?)null;
                    if (viewModel.ImageFile != null)
                        affilitVersionModel.ImageUrl = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                    else
                        affilitVersionModel.ImageUrl = viewModel.ImageUrl;
                    if (viewModel.Type == AffiliatesType.Title)
                        viewModel.ImageUrl = null;
                    affilitVersionModel.Type = viewModel.Type;
                    _hP_AffiliatesVersionReopsitory.Add(affilitVersionModel);
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Affiliates > Update", affilitVersionModel.EnDescription);

                    return RedirectToAction(nameof(Index));
                }

                affilitVersionModel.Id = affilitVersionBySliderId != null ? affilitVersionBySliderId.Id : viewModel.Id;

                if (viewModel.ImageFile != null)
                    affilitVersionModel.ImageUrl = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                else
                    affilitVersionModel.ImageUrl = viewModel.ImageUrl;
                if (viewModel.Type == AffiliatesType.Title)
                    viewModel.ImageUrl = null;
                affilitVersionModel.Type = viewModel.Type;
                _hP_AffiliatesVersionReopsitory.Update(affilitVersionModel);


                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Affiliates > Update", affilitVersionModel.EnDescription);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        /// <summary>
        /// get all afiliaties object for index
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPAffiliates, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetAll()
        {
            var imagesData = _hP_AffiliatesVersionReopsitory.GetAll();
            return Json(new { data = imagesData });
        }

        /// <summary>
        /// Delete affiliaty object by id
        /// </summary>
        /// <param name="id">affiliaty version id</param>
        /// <param name="affiliatId">affiliaty id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPAffiliates, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public async Task<bool> Delete(int id, int affiliatId)
        {
            try
            {
                var affiliatyVersion = _hP_AffiliatesVersionReopsitory.GetByAffilitId(affiliatId);
                if (affiliatyVersion != null)
                {
                    affiliatyVersion.IsDeleted = true;
                    await EditMethod(affiliatyVersion.MapToAffiliatesViewModel());
                }
                else
                {
                    var affiliaty = _hP_AffiliatesReopsitory.GetByIdWithNoTracking(affiliatId);
                    if (affiliaty != null)
                    {
                        affiliaty.IsDeleted = true;
                        await EditMethod(affiliaty.MapToAffiliatesViewModel());
                    }
                    else
                        _hP_AffiliatesVersionReopsitory.SoftDelete(id);

                }
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Home Page > Affiliates > Delete", "Delete id :" + id);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// submit changes for send notification to approval user with last changes
        /// </summary>
        /// <param name="pageRouteId">page route id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPAffiliates, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<IActionResult> SubmitChanges([FromQuery] int pageRouteId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var av = _hP_AffiliatesVersionReopsitory.GetAllDrafts();

            foreach (var record in av)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                _hP_AffiliatesVersionReopsitory.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPAffiliates);

            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(HP_AffiliatesController)[0..^10]}",
                    PageName = PagesNamesConst.HPAffiliates,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);

            }

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Home Page > Affiliates > Approve", "Page Id :" + pageRouteId);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Approve last changes by approval user
        /// </summary>
        /// <param name="id">page route id</param>
        /// <param name="approvalId">notification approved id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPAffiliates, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var sv = _hP_AffiliatesVersionReopsitory.GetAllSubmitted();

            foreach (var record in sv)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;


                var s = new HomePageAffiliates()
                {
                    //Id = record.PageMinistryId ?? 0,
                    ArDescription = record.ArDescription,
                    EnDescription = record.EnDescription,
                    ImageUrl = record.ImageUrl,
                    Url = record.Url,
                    IsActive = record.IsActive,
                    IsDeleted = record.IsDeleted,
                    Type = record.Type
                };
                if (record.HomePageAffiliatesId != null)
                {
                    s.Id = record.HomePageAffiliatesId ?? 0;
                    _hP_AffiliatesReopsitory.Update(s);
                }
                else
                {
                    s.Id = 0;
                    _hP_AffiliatesReopsitory.Add(s);
                    record.HomePageAffiliatesId = s.Id;
                }

                _hP_AffiliatesVersionReopsitory.Update(record);
            }
            approval.VersionStatusEnum = VersionStatusEnum.Approved;
            approval.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approval);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Home Page > Affiliates > Approve", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// Ignore last changes by approval user
        /// </summary>
        /// <param name="id">page route id</param>
        /// <param name="approvalId">notification approved id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPAffiliates, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var sv = _hP_AffiliatesVersionReopsitory.GetAllSubmitted();

            foreach (var record in sv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _hP_AffiliatesVersionReopsitory.Update(record);
            }

            approval.VersionStatusEnum = VersionStatusEnum.Ignored;
            approval.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approval);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Home Page > Affiliates > Reject", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }
    }
}