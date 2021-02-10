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
using MPMAR.Business.ViewModels;
using MPMAR.Web.Admin.AuthRequirement;
using MPMAR.Data.Enums;
using MPMAR.Data.Consts;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class PhotosAlbumController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly IPhotosAlbumRepository _photoAlbumRepository;
        private readonly IPhotoArchiveRepository _photoArchiveRepository;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<PhotosAlbumController> _eventLogger;
        private readonly IFileService _fileService;

        public PhotosAlbumController(IPhotosAlbumRepository photoAlbumRepository, UserManager<ApplicationUser> userManager,
            IToastNotification toastNotification, IEventLogger<PhotosAlbumController> eventLogger, IFileService fileService
            , IPhotoArchiveRepository photoArchiveRepository)
        {
            _photoAlbumRepository = photoAlbumRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _fileService = fileService;
            _photoArchiveRepository = photoArchiveRepository;
        }
        /// <summary>
        /// get PhotosAlbum
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.PicturesLibrary)]
        public IActionResult Index(int id, int pageRouteId, int approvalId)
        {
            ViewBag.PageRouteId = pageRouteId;
            ViewBag.approvalId = approvalId;
            ViewBag.Id = id;
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

            var photoArchiveVersion = _photoArchiveRepository.GetVersion(id);
            if (photoArchiveVersion == null)
            {
                return View();
            }

            return View(id);
        }
        /// <summary>
        /// create PhotosAlbum index index
        /// </summary>
        /// <param name="photoArchiveId"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd }, StaticPagesIdsConst.PicturesLibrary)]
        public IActionResult Create(int photoArchiveVerId, int pageRouteId)
        {
            PhotosAlbumEditViewModel viewModel = new PhotosAlbumEditViewModel();
            viewModel.PhotoArchiveVersionId = photoArchiveVerId;
            viewModel.PageRouteId = pageRouteId;
            return View(viewModel);
        }
        /// <summary>
        /// create PhotosAlbum
        /// </summary>
        /// <param name="photosAlbumEditViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd }, StaticPagesIdsConst.PicturesLibrary)]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> CreateAsync(PhotosAlbumEditViewModel photoAlbumViewModel)
        {
            if (ModelState.IsValid)
            {
                var photosAlbumVersion = photoAlbumViewModel.MapToPhotosAlbum();

                var user = await _userManager.GetUserAsync(HttpContext.User);
                photosAlbumVersion.CreatedById = user.Id;
                photosAlbumVersion.CreationDate = DateTime.Now;
                photosAlbumVersion.ImagePath = _fileService.UploadImageUrlNew(photoAlbumViewModel.ImageFile);
                var photosAlbum = _photoAlbumRepository.Add(photosAlbumVersion, photoAlbumViewModel.PageRouteId);
                if (photosAlbum != null)
                {
                    _toastNotification.AddSuccessToastMessage("Element has been added successfully. </br> It will take effect after admin approval.");

                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Photo Album", photosAlbum.Id.ToString());
                    return RedirectToAction("index", "PhotosAlbum", new { id = photosAlbum.PhotoArchiveVersionId, pageRouteId = photoAlbumViewModel.PageRouteId });
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Photo Album", photosAlbum.Id.ToString());
                    _toastNotification.AddErrorToastMessage("Error has been occurred");
                }
            }
            return View(photoAlbumViewModel);
        }

        /// <summary>
        /// get PhotosAlbum edit page
        /// </summary>
        /// <param name="id">photo album id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.PicturesLibrary)]
        public IActionResult Edit(int id, int pageRouteId)
        {
            var viewModel = _photoAlbumRepository.GetDetail(id);
            viewModel.PageRouteId = pageRouteId;
            return View(viewModel);
        }

        /// <summary>
        /// edit PhotosAlbum
        /// </summary>
        /// <param name="photosAlbumEditViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.PicturesLibrary)]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> EditAsync(PhotosAlbumEditViewModel photosAlbumEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var photosAlbumVersion = photosAlbumEditViewModel.MapToPhotosAlbum();

                var user = await _userManager.GetUserAsync(HttpContext.User);

                photosAlbumVersion.ApprovedBy = user;// user.Id.ToString();
                photosAlbumVersion.ApprovedById = user.Id.ToString();
                photosAlbumVersion.ApprovalDate = DateTime.Now;
                photosAlbumVersion.CreationDate = DateTime.Now;
                if (photosAlbumEditViewModel.ImageFile != null)
                    photosAlbumVersion.ImagePath = _fileService.UploadImageUrlNew(photosAlbumEditViewModel.ImageFile);

                var photosAlbum = _photoAlbumRepository.Update(photosAlbumVersion, photosAlbumEditViewModel.PageRouteId);
                if (photosAlbum != null)
                {
                    _toastNotification.AddSuccessToastMessage("Element has been added successfully. </br> It will take effect after admin approval.");
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Photo Album", photosAlbumEditViewModel.ImageUrl);

                    return RedirectToAction("index", "PhotosAlbum", new { id = photosAlbum.PhotoArchiveVersionId, pageRouteId = photosAlbumEditViewModel.PageRouteId });
                }
                else
                {
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Photo Archive", photosAlbumEditViewModel.ImageUrl);
                    _toastNotification.AddErrorToastMessage("Error has been occurred");
                }
            }

            return View(photosAlbumEditViewModel);
        }
        /// <summary>
        /// get PhotosAlbum details page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.PicturesLibrary)]
        public IActionResult Details(int id, int pageRouteId, int approvalId)
        {
            var viewModel = _photoAlbumRepository.GetDetail(id);
            viewModel.PageRouteId = pageRouteId;
            ViewBag.PageRouteId = pageRouteId;
            ViewBag.approvalId = approvalId;



            return View(viewModel);
        }
        /// <summary>
        /// delete PhotosAlbum
        /// </summary>
        /// <param name="id">PhotosAlbum id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanDelete }, StaticPagesIdsConst.PicturesLibrary)]
        public IActionResult Delete(int id, int pageRouteId)
        {
            // _photoArchiveRepository.Delete(id);
            string impPath = _photoAlbumRepository.GetDetail(id).ImageUrl;


            if (_photoAlbumRepository.Delete(id, pageRouteId))
            {
                _fileService.RemoveImageUrl(impPath);

                TempData[notificationMessageKey] = "Element has been deleted successfully.";
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Static Page > Photo Archive > Photo Album > Delete", "id: " + id);
                TempData[notificationTypeKey] = notificationSuccess;
                return Json(new { });
            }

            TempData[notificationMessageKey] = "Error has been occurred.";
            TempData[notificationTypeKey] = notificationError;
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Static Page > Photo Archive > Photo Album > Delete", "id: " + id);

            return Json(new { });
        }
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.PicturesLibrary)]
        public JsonResult GetPhotosAlbum(int id)
        {
            var photosAlbums = _photoAlbumRepository.GetPhotoAlbums(id);
            return Json(new { data = photosAlbums });
        }
    }
}