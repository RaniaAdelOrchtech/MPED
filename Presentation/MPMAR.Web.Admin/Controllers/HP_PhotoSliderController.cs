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
    public class HP_PhotoSliderController : Controller
    {
        private readonly IHP_PhotoSliderReopsitory _hP_PhotoSliderReopsitory;
        private readonly IHP_PhotoSliderVersionReopsitory _hP_PhotoSliderVersionReopsitory;
        private readonly IFileService _fileService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
private readonly IEventLogger<HP_PhotoSliderController> _eventLogger;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;

        public HP_PhotoSliderController(IHP_PhotoSliderReopsitory hP_PhotoSliderReopsitory, IHP_PhotoSliderVersionReopsitory hP_PhotoSliderVersionReopsitory, IFileService fileService, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<HP_PhotoSliderController> eventLogger, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _hP_PhotoSliderReopsitory = hP_PhotoSliderReopsitory;
            _hP_PhotoSliderVersionReopsitory = hP_PhotoSliderVersionReopsitory;
            _fileService = fileService;
            _userManager = userManager;
            _toastNotification = toastNotification;
_eventLogger = eventLogger;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }
        /// <summary>
        /// get HP_PhotoSlider page index
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HpPhotoSlider, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery]int pageRouteId, [FromQuery] int approvalId)
        {
            ViewBag.pageRouteId = pageRouteId;
            ViewBag.approvalId = approvalId;
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPPhotoSlider);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            return View();
        }
        /// <summary>
        /// get HP_PhotoSlider create page 
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HpPhotoSlider, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create()
        {
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPPhotoSlider);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            return View();
        }
        /// <summary>
        /// create HP_PhotoSlider
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HpPhotoSlider, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Create(HP_PhotoSliderViewModel viewModel)
        {
            viewModel.ArDescription.ValidateHtml("ArDescription", ModelState);
            viewModel.EnDescription.ValidateHtml("EnDescription", ModelState);

            if (ModelState.IsValid)
            {
                await EditMethod(viewModel);

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Home Page > Photos Slider > Add", viewModel.EnTitle);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
        /// <summary>
        /// get HP_PhotoSlider edit page
        /// </summary>
        /// <param name="id">hP_PhotoSliderVersion id</param>
        /// <param name="homePagePhotoSliderId"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HpPhotoSlider, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery]int homePagePhotoSliderId)
        {
            if (id == null)
                return NotFound();

            HP_PhotoSliderViewModel viewModel;
            var sliderVersion = _hP_PhotoSliderVersionReopsitory.GetBySliderId(homePagePhotoSliderId);
            if (sliderVersion == null || sliderVersion.VersionStatusEnum == VersionStatusEnum.Approved || sliderVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                var slider = _hP_PhotoSliderReopsitory.GetById(homePagePhotoSliderId);
                if (slider != null)
                    viewModel = slider.MapToPhotoSliderViewModel();
                else
                {
                    sliderVersion = _hP_PhotoSliderVersionReopsitory.GetById(id);
                    viewModel = sliderVersion.MapToPhotoSliderViewModel();
                }
            }
            else
            {
                viewModel = sliderVersion.MapToPhotoSliderViewModel();
            }
            var notification = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPPhotoSlider);
            ViewBag.DisableEditFlage = false;
            if (notification != null && notification.VersionStatusEnum == VersionStatusEnum.Submitted)
                ViewBag.DisableEditFlage = true;
            //remove id value from route
            ModelState.Clear();
            return View(viewModel);
        }
        /// <summary>
        /// edit HP_PhotoSlider
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HpPhotoSlider, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Edit(HP_PhotoSliderViewModel viewModel)
        {
            return await EditMethod(viewModel);
        }
        /// <summary>
        /// edit HP_PhotoSlider
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private async Task<IActionResult> EditMethod(HP_PhotoSliderViewModel viewModel)
        {
      

            viewModel.ArDescription.ValidateHtml("ArDescription", ModelState);
            viewModel.EnDescription.ValidateHtml("EnDescription", ModelState);

            ModelState.Remove(nameof(viewModel.ImageFile));
            ModelState.Remove(nameof(viewModel.HomePagePhotoSliderId));

            if (ModelState.IsValid)
            {
                var sliderVersionBySliderId = _hP_PhotoSliderVersionReopsitory.GetBySliderId(viewModel.HomePagePhotoSliderId ?? 0);
                var sliderVersionById = _hP_PhotoSliderVersionReopsitory.GetById(viewModel.Id);
                sliderVersionBySliderId = sliderVersionById == null ? sliderVersionBySliderId : sliderVersionById;
                var sliderVersionModel = viewModel.MapToPhotoSliderVersionModel();

                var user = await _userManager.GetUserAsync(HttpContext.User);

                //if hP_PhotoSliderVersion wasn't exist or  HP_PhotoSliderViewModel approved or ignored then create new version 
                if (sliderVersionBySliderId == null || sliderVersionModel.VersionStatusEnum == VersionStatusEnum.Approved || sliderVersionModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    sliderVersionModel.CreatedById = user.Id;
                    sliderVersionModel.CreationDate = DateTime.Now;
                    sliderVersionModel.VersionStatusEnum = VersionStatusEnum.Draft;
                    sliderVersionModel.ChangeActionEnum = ChangeActionEnum.Update;
                    sliderVersionModel.Id = 0;
                    sliderVersionModel.HomePagePhotoSliderId = viewModel.HomePagePhotoSliderId > 0 ? viewModel.HomePagePhotoSliderId : (int?)null;
                    if (viewModel.ImageFile != null)
                        sliderVersionModel.ImageUrl = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                    _hP_PhotoSliderVersionReopsitory.Add(sliderVersionModel);
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Photos Slider > Update", viewModel.EnTitle);
                    return RedirectToAction(nameof(Index));
                }

                sliderVersionModel.Id = sliderVersionBySliderId != null ? sliderVersionBySliderId.Id : viewModel.Id;
                if (viewModel.ImageFile != null)
                    sliderVersionModel.ImageUrl = _fileService.UploadImageUrlNew(viewModel.ImageFile);
                _hP_PhotoSliderVersionReopsitory.Update(sliderVersionModel);

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Home Page > Photos Slider > Update", viewModel.EnTitle);

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
        /// <summary>
        /// get all HP_PhotoSlider
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HpPhotoSlider, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetAllPhotoSlider()
        {
            var imageSliderData = _hP_PhotoSliderVersionReopsitory.GetAll();
            return Json(new { data = imageSliderData });
        }
        /// <summary>
        /// delete HP_PhotoSlider
        /// </summary>
        /// <param name="id">hP_PhotoSliderVersion id</param>
        /// <param name="sliderId"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HpPhotoSlider, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public async Task<bool> Delete(int id, int sliderId)
        {
            try
            {
                var sliderVersion = _hP_PhotoSliderVersionReopsitory.GetBySliderId(sliderId);
                if (sliderVersion != null)
                {
                    sliderVersion.IsDeleted = true;
                    await EditMethod(sliderVersion.MapToPhotoSliderViewModel());
                }
                else
                {
                    var slider = _hP_PhotoSliderReopsitory.GetByIdWithNoTracking(sliderId);
                    if (slider != null)
                    {
                        slider.IsDeleted = true;
                        await EditMethod(slider.MapToPhotoSliderViewModel());
                    }
                    else
                        _hP_PhotoSliderVersionReopsitory.SoftDelete(id);

                }
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Home Page > Photos Slider > Delete", " Id :" + id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// submit changes for the approval user 
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HpPhotoSlider, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanDelete, PrivilegesActions.CanEdit })]
        public async Task<IActionResult> SubmitChanges([FromQuery] int pageRouteId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var sv = _hP_PhotoSliderVersionReopsitory.GetAllDrafts();
            //if (!sv.Any())
            //{
            //    CopyMinistries(user.Id);
            //    sv = _hP_PhotoSliderVersionReopsitory.GetAllDrafts();
            //}
            foreach (var record in sv)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                _hP_PhotoSliderVersionReopsitory.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.HPPhotoSlider);

            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(HP_PhotoSliderController)[0..^10]}",
                    PageName = PagesNamesConst.HPPhotoSlider,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);
             
            }
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Home Page > Photos Slider > Submitted", "Page Id :" + pageRouteId);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// approve changes, change status to approved and update the non version
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HpPhotoSlider, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var sv = _hP_PhotoSliderVersionReopsitory.GetAllSubmitted();

            foreach (var record in sv)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;


                var s = new HomePagePhotoSlider()
                {
                    //Id = record.PageMinistryId ?? 0,
                    ArDescription = record.ArDescription,
                    ArTitle = record.ArTitle,
                    EnDescription = record.EnDescription,
                    EnTitle = record.EnTitle,
                    ImageUrl = record.ImageUrl,
                    Url = record.Url,
                    IsActive = record.IsActive,
                    IsDeleted = record.IsDeleted,
                };
                if (record.HomePagePhotoSliderId != null)
                {
                    s.Id = record.HomePagePhotoSliderId ?? 0;
                    _hP_PhotoSliderReopsitory.Update(s);
                }
                else
                {
                    s.Id = 0;
                    _hP_PhotoSliderReopsitory.Add(s);
                    record.HomePagePhotoSliderId = s.Id;
                }

                _hP_PhotoSliderVersionReopsitory.Update(record);
            }
            approval.VersionStatusEnum = VersionStatusEnum.Approved;
            approval.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approval);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Home Page > Photos Slider > Approve", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }
        /// <summary>
        /// Ignore changes and change status to ignored
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HpPhotoSlider, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore(int id, [FromQuery]int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var sv = _hP_PhotoSliderVersionReopsitory.GetAllSubmitted();

            foreach (var record in sv)
            {
                record.ModificationDate = DateTime.Now;
                record.ModifiedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                _hP_PhotoSliderVersionReopsitory.Update(record);
            }

            approval.VersionStatusEnum = VersionStatusEnum.Ignored;
            approval.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approval);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Home Page > Photos Slider > Reject", " Id :" + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }

       



    }
}