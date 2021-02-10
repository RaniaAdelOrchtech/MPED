using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Web.Site.ViewModels;

namespace MPMAR.Web.Site.Controllers
{
    public class PhotosAlbumController : BaseController
    {
        private readonly ApplicationDbContext _dataAccessService;
        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly IConfiguration _configuration;

        public PhotosAlbumController(ApplicationDbContext dataAccessService, IPageRouteRepository pageRouteRepository, IConfiguration configuration)
        {
            _dataAccessService = dataAccessService;
            _pageRouteRepository = pageRouteRepository;
            _configuration = configuration;
        }
        /// <summary>
        /// get PhotosAlbum page index
        /// </summary>
        /// <param name="PAId">PhotoArchiveId id</param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public IActionResult Index(int PAId, string lang)
        {
            var pageRoute = _pageRouteRepository.GetByControllerName(nameof(PhotoArchiveController)[0..^10]);
            if (pageRoute == null || !pageRoute.IsActive || pageRoute.IsDeleted)
            {
                return View("Error");
            }
            ViewPhotoAlbum obj = new ViewPhotoAlbum();
            var items = _dataAccessService.PhotosAlbum.Where(i => i.IsDeleted != true && i.IsActive == true && i.PhotoArchiveId == PAId).OrderBy(i => i.Order).ToList();
            PhotosAlbum objPhAl = new PhotosAlbum();
            PhotoArchive objPhotoArchive = _dataAccessService.PhotoArchive.SingleOrDefault(i => i.Id == PAId);
            //GetMenuItemsAsync(HttpContext.User);
            objPhAl.ImagePath = objPhotoArchive.ImageUrl;
            items.Add(objPhAl);
            obj.PhotosAlbums = items;
            obj.PhotoArchiveEnName = objPhotoArchive.EnPhotoArchiveName;
            obj.PhotoArchiveArName = objPhotoArchive.ArPhotoArchiveName;
            obj.PhotoArchiveArDetails = objPhotoArchive.ArPhotoArchiveDesc;
            obj.PhotoArchiveEnDetails = objPhotoArchive.EnPhotoArchiveDesc;
            obj.ModifyDate = (objPhotoArchive.ApprovalDate != null ? objPhotoArchive.ApprovalDate : objPhotoArchive.CreationDate);

            if (lang == null || lang.ToLower() == "ar")
            {
                ViewBag.Title = obj.PhotoArchiveArName;
                ViewBag.Nav = pageRoute.NavItem.ArName;
                ViewBag.parentPage = pageRoute.ArName;
            }
            else
            {
                ViewBag.Title = obj.PhotoArchiveEnName;
                ViewBag.Nav = pageRoute.NavItem.EnName;
                ViewBag.parentPage = pageRoute.EnName;
            }
            var imageBaseURL = _configuration.GetValue<string>("BackEndDomain");
            foreach (var item in obj.PhotosAlbums)
            {
                if (item.ImagePath != null)
                    item.ImagePath = imageBaseURL + item.ImagePath.Replace(" ", "%20");
            }
            return View(obj);
        }
    }
}