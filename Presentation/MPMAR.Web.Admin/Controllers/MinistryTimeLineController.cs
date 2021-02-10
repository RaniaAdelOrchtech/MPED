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
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;
using MPMAR.Business;
using static MPMAR.Data.Enums.Enums;
using MPMAR.Common.Interfaces;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class MinistryTimeLineController : Controller
    {

        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly IMinistryTimeLineRepository _ministryTimeLineRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<MinistryTimeLineController> _eventLogger;

        private readonly IFileService _fileService;

        public MinistryTimeLineController(IMinistryTimeLineRepository ministryTimeLineRepository, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<MinistryTimeLineController> eventLogger, IFileService fileService)
        {
            _ministryTimeLineRepository = ministryTimeLineRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _fileService = fileService;
        }
        
        /// <summary>
        /// Get page index
        /// </summary>
        /// <param name="id">ministry time line id</param>
        /// <returns></returns>
        [Authorize]
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

            MinistryTimeLine ministryTimeLine = _ministryTimeLineRepository.Get(id);
            if (ministryTimeLine == null)
            {
                return View();
            }

            return View(ministryTimeLine);
        }

        /// <summary>
        /// Get method for create new ministry time line object
        /// </summary>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Create(int pageRouteVersionId)
        {
            MinistryTimeLineViewModel viewModel = new MinistryTimeLineViewModel();
            return View(viewModel);
        }

        /// <summary>
        /// Post method for create new ministry time line object
        /// </summary>
        /// <param name="pageMinistryViewModel">ministry time line model</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(MinistryTimeLineViewModel pageMinistryViewModel)
        {
        
            pageMinistryViewModel.StartDate = Convert.ToDateTime(pageMinistryViewModel.EventDateRange.Split('-')[0]);
            pageMinistryViewModel.EndDate = Convert.ToDateTime(pageMinistryViewModel.EventDateRange.Split('-')[1]);

            if (ModelState.IsValid)
            {
                MinistryTimeLine sectionCardVersion = pageMinistryViewModel.MapToMinistryTimeLine();// .MapToPageMinistry();

                var user = await _userManager.GetUserAsync(HttpContext.User);
                sectionCardVersion.StatusId = (int)RequestStatus.Approved;
                sectionCardVersion.CreatedById = user.Id;
                sectionCardVersion.CreationDate = DateTime.Now;
                sectionCardVersion.ApprovedById = user.Id;
                sectionCardVersion.ApprovedBy = user;
                sectionCardVersion.ApprovalDate = DateTime.Now;
         
                if (pageMinistryViewModel.Photo != null)
                    sectionCardVersion.ProfileImageUrl = _fileService.UploadImageUrl(pageMinistryViewModel.Photo);
                MinistryTimeLine newPageSectionCardVersion = _ministryTimeLineRepository.Add(sectionCardVersion);
                if (newPageSectionCardVersion != null)
                {
              

                    _toastNotification.AddSuccessToastMessage(ToasrMessages.AddSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Static Page > Ministry Time Line > Edit", pageMinistryViewModel.EnName);
                    return RedirectToAction("Index", new { id = newPageSectionCardVersion.Id });
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Static Page > Ministry Time Line > Edit", pageMinistryViewModel.EnName);
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                }
            }
            return View(pageMinistryViewModel);
        }

        /// <summary>
        /// Get method for update an existing ministry time line object
        /// </summary>
        /// <param name="id">ministry time line id</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = UserRolesConst.ContentManager + "," + UserRolesConst.SuperAdmin)]
        public IActionResult Edit(int id)
        {
            MinistryTimeLine pageSectionCardVersion = _ministryTimeLineRepository.GetDetail(id);
        

            MinistryTimeLineViewModel viewModel = pageSectionCardVersion.MapToEventdViewModel();
            return View(viewModel);
        }

        /// <summary>
        /// Post method for update ministry time line object
        /// </summary>
        /// <param name="sectionCardViewModel">ministry time line model new data</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRolesConst.ContentManager + "," + UserRolesConst.SuperAdmin)]
        public async Task<IActionResult> EditAsync(MinistryTimeLineViewModel sectionCardViewModel)
        {
           

            if (ModelState.IsValid)
            {
                MinistryTimeLine sectionCardVersion = sectionCardViewModel.MapToMinistryTimeLine();

                var user = await _userManager.GetUserAsync(HttpContext.User);

                sectionCardVersion.ApprovedBy = user;// user.Id.ToString();
                sectionCardVersion.ApprovedById = user.Id.ToString();
                sectionCardVersion.ApprovalDate = DateTime.Now;
                sectionCardVersion.CreationDate = DateTime.Now;
                sectionCardVersion.StatusId = (int)RequestStatus.Approved;
                if (sectionCardViewModel.Photo != null)
                    sectionCardVersion.ProfileImageUrl = _fileService.UploadImageUrl(sectionCardViewModel.Photo);


                MinistryTimeLine newSectionCardVersion = _ministryTimeLineRepository.Update(sectionCardVersion);
                if (newSectionCardVersion != null)
                {
              

                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Static Page > Ministry Time Line > Edit", sectionCardViewModel.EnName);

                    return RedirectToAction("Index", new { id = newSectionCardVersion.Id });
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Static Page > Ministry Time Line > Edit", sectionCardViewModel.EnName);
                    _toastNotification.AddErrorToastMessage(ToasrMessages.warning);
                }
            }

            return View(sectionCardViewModel);
        }

        /// <summary>
        /// Details ministry time line object by id
        /// </summary>
        /// <param name="id">ministry time line id</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Details(int id)
        {
            MinistryTimeLine pageSectionCardVersion = _ministryTimeLineRepository.GetDetail(id);

            return View(pageSectionCardVersion);
        }

        /// <summary>
        /// Delete ministry time line object by id
        /// </summary>
        /// <param name="id">ministry time line id</param>
        /// <returns></returns>
        [Authorize]
        public IActionResult Delete(int id)
        {
            MinistryTimeLine pageSectionCardVersion = _ministryTimeLineRepository.Delete(id);

            if (pageSectionCardVersion != null)
            {
           

                TempData[notificationMessageKey] = ToasrMessages.DeleteSuccess;
                TempData[notificationTypeKey] = notificationSuccess;

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Static Page > Ministry Time Line > Delete", " Id :" + id);

                return Json(new { });
            }

            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            return Json(new { });
        }

        /// <summary>
        /// Get all objects for index
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        public JsonResult GetPageEvent(int id)
        {
            var pageMinistry = _ministryTimeLineRepository.GetMinistryTimeLine();
            return Json(new { data = pageMinistry });
        }
     
    }
}