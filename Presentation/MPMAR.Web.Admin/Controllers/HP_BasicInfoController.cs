using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Common;
using MPMAR.Common.Helpers;
using MPMAR.Data.Consts;
using MPMAR.Data.Enums;
using MPMAR.Data.HomePageModels;
using MPMAR.Web.Admin.AuthRequirement;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;

namespace MPMAR.Web.Admin.Controllers
{
    public class HP_BasicInfoController : Controller
    {
        private readonly IHP_BasicInfoReopsitory _hP_BasicInfoReopsitory;
        private readonly IToastNotification _toastNotification;
        private readonly IFileService _fileService;

        public HP_BasicInfoController(IHP_BasicInfoReopsitory hP_BasicInfoReopsitory, IToastNotification toastNotification, IFileService fileService)
        {
            _hP_BasicInfoReopsitory = hP_BasicInfoReopsitory;
            _toastNotification = toastNotification;
            _fileService = fileService;
        }
        /// <summary>
        /// get home page info page index
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPBasicInfo, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// get edit home page info page
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPBasicInfo, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit()
        {
            var basicInfo = _hP_BasicInfoReopsitory.GetAll().FirstOrDefault();
            return View(basicInfo.MapToViewModel());
        }
        /// <summary>
        /// edit home page info
        /// </summary>
        /// <param name="homePageBasicInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPBasicInfo, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        [RequestSizeLimit(500000000)]
        public IActionResult Edit(HomePageBasicInfoViewModel homePageBasicInfo)
        {
            if (ModelState.IsValid)
            {
                if (homePageBasicInfo.LogoFile != null)
                    homePageBasicInfo.LogoUrl = _fileService.UploadImageUrlNew(homePageBasicInfo.LogoFile);
                if (homePageBasicInfo.FavIconFile != null)
                    homePageBasicInfo.FavIconUrl = _fileService.UploadImageUrlNew(homePageBasicInfo.FavIconFile);

                _hP_BasicInfoReopsitory.Update(homePageBasicInfo.MapToModel());
                _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                return RedirectToAction(nameof(Index));
            }
            return View(homePageBasicInfo);
        }
        /// <summary>
        /// get all home page info
        /// </summary>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.HPBasicInfo, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult GetAll()
        {
            return Json(new { data = _hP_BasicInfoReopsitory.GetAll() });
        }
    }
}