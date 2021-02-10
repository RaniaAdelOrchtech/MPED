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
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;
using MPMAR.Business;
using static MPMAR.Data.Enums.Enums;
using MPMAR.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Data.Consts;
using MPMAR.Business.ViewModels;
using MPMAR.Web.Admin.AuthRequirement;
using MPMAR.Data.Enums;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class PhotoArchiveController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly IPhotoArchiveRepository _photoArchiveRepository;
        private readonly IPhotosAlbumRepository _photoAlbumRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<PhotoArchiveController> _eventLogger;
        private readonly IFileService _fileService;
        private readonly IPageRouteRepository _pageRouteRepository;

        public PhotoArchiveController(IPhotoArchiveRepository photoArchiveRepository, IPhotosAlbumRepository photoAlbumRepository, 
            UserManager<ApplicationUser> userManager, IToastNotification toastNotification, 
            IEventLogger<PhotoArchiveController> eventLogger, IFileService fileService,
            IPageRouteRepository pageRouteRepository)
        {
            _photoArchiveRepository = photoArchiveRepository;
            _photoAlbumRepository = photoAlbumRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _fileService = fileService;
            _pageRouteRepository = pageRouteRepository;
        }
        /// <summary>
        /// get PhotoArchive page index
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.PicturesLibrary)]
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
        /// get PhotoArchive create page
        /// </summary>
        /// <param name="pageRouteVersionId"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd }, StaticPagesIdsConst.PicturesLibrary)]
        public IActionResult Create(int pageRouteId)
        {
            var photoArchives = _photoArchiveRepository.GetVersion();
            PhotoArchiveEditViewModel viewModel = new PhotoArchiveEditViewModel { PageRouteId=pageRouteId};
           

            var listPhotoArchiveType = photoArchives.Select(m => new { m.EnPhotoArchiveType, m.ArPhotoArchiveType })
                         .Distinct()
                         .ToList();
            List<SelectListItem> photoArchiveType = listPhotoArchiveType.Select(i => new SelectListItem { Text = i.EnPhotoArchiveType + "( " + i.ArPhotoArchiveType + " )", Value = i.EnPhotoArchiveType + "$" + i.ArPhotoArchiveType }).ToList();

            var countrytip = new SelectListItem()
            {
                Value = "Select ArchiveType$Select ArchiveType",
                Text = "Select ArchiveType"
            };
            photoArchiveType.Insert(0, countrytip);
            countrytip = new SelectListItem()
            {
                Value = "Other",
                Text = "Other"
            };
            photoArchiveType.Insert(photoArchiveType.Count, countrytip);
            viewModel.PhotoArchiveType = photoArchiveType;
            return View(viewModel);
        }
        /// <summary>
        /// create PhotoArchive
        /// </summary>
        /// <param name="pageMinistryViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd }, StaticPagesIdsConst.PicturesLibrary)]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> CreateAsync(PhotoArchiveEditViewModel photoArchiveViewModel)
        {
            if (ModelState.IsValid)
            {
                var photoArchiveVersion = photoArchiveViewModel.MapToPhotoArchive();
                PhotosAlbumVersion photoA = new PhotosAlbumVersion();
                var user = await _userManager.GetUserAsync(HttpContext.User);
                photoA.CreatedById = user.Id;
                photoA.Order = 1;
                photoA.IsActive = true;
                photoA.CreationDate = DateTime.Now;
                photoA.ImagePath = _fileService.UploadImageUrlNew(photoArchiveViewModel.ImageFile);
                photoArchiveVersion.PhotosAlbumVersions.Add(photoA);
                photoArchiveVersion.CreatedById = user.Id;
                photoArchiveVersion.CreationDate = DateTime.Now;
                photoArchiveVersion.ImageUrl = _fileService.UploadImageUrlNew(photoArchiveViewModel.ImageFile);
                var newPageSectionCardVersion = _photoArchiveRepository.Add(photoArchiveVersion, photoArchiveViewModel.PageRouteId);
                if (newPageSectionCardVersion != null)
                {
                    
                    _toastNotification.AddSuccessToastMessage("Element has been added successfully. </br> It will take effect after admin approval.");
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Photo Archive", photoArchiveViewModel.EnPhotoArchiveName);

                    return RedirectToAction("Index", new { pageRouteId = photoArchiveViewModel.PageRouteId });
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Photo Archive", photoArchiveViewModel.EnPhotoArchiveName);
                    _toastNotification.AddErrorToastMessage("Error has been occurred");
                }
            }
            var photoArchives = _photoArchiveRepository.GetVersion();
            var listPhotoArchiveType = photoArchives.Select(m => new { m.EnPhotoArchiveType, m.ArPhotoArchiveType })
                        .Distinct()
                        .ToList();
            List<SelectListItem> photoArchiveType = listPhotoArchiveType.Select(i => new SelectListItem { Text = i.EnPhotoArchiveType + "( " + i.ArPhotoArchiveType + " )", Value = i.EnPhotoArchiveType + "$" + i.ArPhotoArchiveType }).ToList();

            var countrytip = new SelectListItem()
            {
                Value = "Select ArchiveType$Select ArchiveType",
                Text = "Select ArchiveType"
            };
            photoArchiveType.Insert(0, countrytip);
            countrytip = new SelectListItem()
            {
                Value = "Other",
                Text = "Other"
            };
            photoArchiveType.Insert(photoArchiveType.Count, countrytip);
            photoArchiveViewModel.PhotoArchiveType = photoArchiveType;
            return View(photoArchiveViewModel);
        }

        /// <summary>
        /// get PhotoArchive edit page
        /// </summary>
        /// <param name="id">photoArchive id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.PicturesLibrary)]
        public IActionResult Edit(int id, int pageRouteId)
        {
            var viewModel = _photoArchiveRepository.GetDetail(id);
            viewModel.PageRouteId = pageRouteId;
            var photoArchives = _photoArchiveRepository.GetVersion();
           var listPhotoArchiveType = photoArchives.Select(m => new { m.EnPhotoArchiveType, m.ArPhotoArchiveType })
                        .Distinct()
                        .ToList();
            List<SelectListItem> photoArchiveType = listPhotoArchiveType.Select(i => new SelectListItem { Text = i.EnPhotoArchiveType + "( " + i.ArPhotoArchiveType + " )", Value = i.EnPhotoArchiveType + "$" + i.ArPhotoArchiveType }).ToList();

            var countrytip = new SelectListItem()
            {
                Value = "Select ArchiveType$Select ArchiveType",
                Text = "Select ArchiveType"
            };
            photoArchiveType.Insert(0, countrytip);
            countrytip = new SelectListItem()
            {
                Value = "Other",
                Text = "Other"
            };
            photoArchiveType.Insert(photoArchiveType.Count, countrytip);
            viewModel.PhotoArchiveType = photoArchiveType;
            viewModel.PageRouteId = pageRouteId;
            return View(viewModel);
        }

        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.PicturesLibrary)]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> EditAsync(PhotoArchiveEditViewModel photoArchiveViewModel)
        {
            ModelState.Remove(nameof(photoArchiveViewModel.PageRouteId));
            if (ModelState.IsValid)
            {
                var photoArchiveVersion = photoArchiveViewModel.MapToPhotoArchive();

                var user = await _userManager.GetUserAsync(HttpContext.User);


                if (photoArchiveViewModel.ImageFile != null)
                    photoArchiveVersion.ImageUrl = _fileService.UploadImageUrlNew(photoArchiveViewModel.ImageFile);
                var newPhotoArchiveVersion = _photoArchiveRepository.Update(photoArchiveVersion, photoArchiveViewModel.PageRouteId);
                if (newPhotoArchiveVersion != null)
                {
                    _toastNotification.AddSuccessToastMessage("Element has been added successfully. </br> It will take effect after admin approval.");
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Photo Archive", photoArchiveViewModel.EnPhotoArchiveName);

                    return RedirectToAction("Index", new { pageRouteId = photoArchiveViewModel.PageRouteId });
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Photo Archive", photoArchiveViewModel.EnPhotoArchiveName);
                    _toastNotification.AddErrorToastMessage("Error has been occurred");
                }
            }
            var photoArchives = _photoArchiveRepository.GetVersion();
            var listPhotoArchiveType = photoArchives.Select(m => new { m.EnPhotoArchiveType, m.ArPhotoArchiveType })
                        .Distinct()
                        .ToList();
            List<SelectListItem> photoArchiveType = listPhotoArchiveType.Select(i => new SelectListItem { Text = i.EnPhotoArchiveType + "( " + i.ArPhotoArchiveType + " )", Value = i.EnPhotoArchiveType + "$" + i.ArPhotoArchiveType }).ToList();

            var countrytip = new SelectListItem()
            {
                Value = "Select ArchiveType$Select ArchiveType",
                Text = "Select ArchiveType"
            };
            photoArchiveType.Insert(0, countrytip);
            countrytip = new SelectListItem()
            {
                Value = "Other",
                Text = "Other"
            };
            photoArchiveType.Insert(photoArchiveType.Count, countrytip);
            photoArchiveViewModel.PhotoArchiveType = photoArchiveType;
            return View(photoArchiveViewModel);
        }
        /// <summary>
        /// get details of PhotoArchive
        /// </summary>
        /// <param name="id">PhotoArchive id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.PicturesLibrary)]
        public IActionResult Details(int id, int pageRouteId, int? approvalId)
        {
            var photoArchive = _photoArchiveRepository.GetVersion(id,true);
            ViewBag.PageRouteId = pageRouteId;
            ViewBag.ApprovalId = approvalId;
            return View(photoArchive);
        }
        /// <summary>
        /// delete PhotoArchive 
        /// </summary>
        /// <param name="id">PhotoArchive id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanDelete }, StaticPagesIdsConst.PicturesLibrary)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string url = $"{nameof(PhotoArchiveController)[0..^10]}/{nameof(Details)}";
            if (_photoArchiveRepository.DeleteVer(id,user.Id,url))
            {
                
                    TempData[notificationMessageKey] = "Element has been deleted successfully.";
                    TempData[notificationTypeKey] = notificationSuccess;
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Static Page > Photo Archive > Delete", "id: " + id);

                    return Json(new { });
                
            }

            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Static Page > Photo Archive > Delete", "id: " + id);
            return Json(new { });
        }


        /// <summary>
        /// gte PhotoArchive 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.PicturesLibrary)]
        public JsonResult GetPhotoArchive(int id)
        {
            var photos = _photoArchiveRepository.GetPhotoArchiveByPageRouteId(id);
            return Json(new { data = photos });
        }
        /// <summary>
        /// get PhotoArchiveType
        /// </summary>
        /// <param name="Prefix"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.PicturesLibrary)]
        public JsonResult GetPhotoArchiveType(string Prefix)
        {
            //Note : you can bind same list from database  
            var pageMinistry = _photoArchiveRepository.Get();
            //Searching records from list using LINQ query  
            var PhotoArchiveType = (from N in pageMinistry
                                    where N.EnPhotoArchiveType.StartsWith(Prefix)
                                    select new { N.EnPhotoArchiveType, N.ArPhotoArchiveType });
            return Json(new { data = PhotoArchiveType });
        }
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete }, StaticPagesIdsConst.PicturesLibrary)]
        public async Task<IActionResult> ApplyEditRequestAsync(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string url = $"{nameof(PhotoArchiveController)[0..^10]}/{nameof(Details)}";

            var success = _photoArchiveRepository.ApplySubmitRequest(id, user.Id, url);
            if (success)
            {
                TempData[notificationMessageKey] = ToasrMessages.SubmitSuccess;

                TempData[notificationTypeKey] = notificationSuccess;
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Photo Archive", "Submit id : " + id);

                return Json(new { });
            }

            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Photo Archive", "Submit id : " + id);
            return Json(new { });
        }
        /// <summary>
        /// approve changes and change status to approved
        /// </summary>
        /// <param name="id"></param>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanApprove }, StaticPagesIdsConst.PicturesLibrary)]
        public async Task<IActionResult> ApproveAsync(int id, int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var success = _photoArchiveRepository.Approve(id, approvalId, user.Id);
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
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanApprove }, StaticPagesIdsConst.PicturesLibrary)]
        public async Task<IActionResult> IgnoreAsync(int id, int approvalId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var success = _photoArchiveRepository.Ignore(id, approvalId, user.Id);
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