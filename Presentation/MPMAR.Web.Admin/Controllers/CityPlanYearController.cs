using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Common.Helpers;
using MPMAR.Data;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;
using MPMAR.Business;
using static MPMAR.Data.Enums.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Data.Consts;
using MPMAR.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using MPMAR.Common;
using MPMAR.Common.Interfaces;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.AuthRequirement;
using Microsoft.Extensions.Configuration;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class CityPlanYearController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly ICityPlanYearRepository _cityPlanYearRepository;
        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<CityPlanYearController> _eventLogger;
        private readonly IFileService _fileService;
        private readonly IDFGovRepository _dFGovRepository;
        private readonly ICityPlanYearVersionRepository _cityPlanYearVersionRepository;
        private readonly IConfiguration _configuration;

        public CityPlanYearController(IPageRouteVersionRepository pageRouteVersionRepository, ICityPlanYearRepository cityPlanYearRepository, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<CityPlanYearController> eventLogger, IFileService fileService, IDFGovRepository dFGovRepository, ICityPlanYearVersionRepository cityPlanYearVersionRepository, IConfiguration Configuration)
        {
            _pageRouteVersionRepository = pageRouteVersionRepository;
            _cityPlanYearRepository = cityPlanYearRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _fileService = fileService;
            _dFGovRepository = dFGovRepository;
            _cityPlanYearVersionRepository = cityPlanYearVersionRepository;
            _configuration = Configuration;
        }

        /// <summary>
        /// index for city plan year objects
        /// </summary>
        /// <param name="approvalId">approval notification id</param>
        /// <param name="cityPlanVerId">city plan version id</param>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.CitizenPlan)]
        public IActionResult Index([FromQuery] int approvalId, [FromQuery] int cityPlanVerId)
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

            List<CityPlanYear> pageSectionVersion = _cityPlanYearRepository.Get().ToList();
            ViewBag.cityPlanVerId = cityPlanVerId;
            ViewBag.approvalId = approvalId;
            if (pageSectionVersion == null)
            {
                return View();
            }

            return View(pageSectionVersion);
        }

        /// <summary>
        /// Get method for create city plan year
        /// </summary>
        /// <param name="cityPlanVerId">city plan year version id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd }, StaticPagesIdsConst.CitizenPlan)]
        public IActionResult Create(int cityPlanVerId)
        {
            CityPlanYearEditViewModel viewModel = new CityPlanYearEditViewModel();
            viewModel.CityPlanId = cityPlanVerId;

            ViewBag.GovYear = getYears();
            List<string> objGov = new List<string>();
            string year = (DateTime.Now.Year - 1).ToString() + "/" + (DateTime.Now.Year).ToString();
            objGov = _cityPlanYearRepository.Get().Where(i => i.GovYear == year).Select(j => j.GovName).Distinct().ToList();

            ViewBag.DFGovId = new SelectList(_dFGovRepository.GetAll(), "Id", "EnName");


            return View(viewModel);
        }

        /// <summary>
        /// get years list
        /// </summary>
        /// <param name="selected">selected year</param>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.CitizenPlan)]
        public List<SelectListItem> getYears(string selected = "")
        {
            List<SelectListItem> objListYear = new List<SelectListItem>();
            for (int i = DateTime.Now.Year - 10; i < DateTime.Now.Year + 10; i++)
            {
                SelectListItem objItem = new SelectListItem((i - 1).ToString() + "/" + i.ToString(), (i - 1).ToString() + "/" + i.ToString());
                if (selected == "")
                { objItem.Selected = (DateTime.Now.Year == i ? true : false); }
                else
                {
                    if (selected == objItem.Value)
                        objItem.Selected = true;
                }


                objListYear.Add(objItem);
            }
            return objListYear;
        }

        /// <summary>
        /// post method for create city plan year
        /// </summary>
        /// <param name="cityPlanYear">city plan year model</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd }, StaticPagesIdsConst.CitizenPlan)]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Create(CityPlanYearEditViewModel cityPlanYear)
        {

            if (ModelState.IsValid)
            {
                await EditMethod(cityPlanYear);


                return RedirectToAction(nameof(Index), new { cityPlanVerId = cityPlanYear.CityPlanId });
            }
            return View(cityPlanYear);
        }

        /// <summary>
        /// Get method for update city plan year
        /// </summary>
        /// <param name="cityPlanYearId">city plan year id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.CitizenPlan)]
        public IActionResult Edit(int id, [FromQuery] int cityPlanYearId)
        {
            var detailModel = GetDetailModel(id, cityPlanYearId);


            ViewBag.GovYear = getYears(detailModel.GovYear);

            ViewBag.DFGovId = new SelectList(_dFGovRepository.GetAll(), "Id", "EnName", detailModel.DFGovId);

            ModelState.Clear();
            return View(detailModel);
        }

        /// <summary>
        /// core method for update city plan year
        /// </summary>
        /// <param name="viewModel">view model for city plan year</param>
        /// <returns></returns>
        private async Task<IActionResult> EditMethod(CityPlanYearEditViewModel viewModel)
        {
            var pageRouteVersion = _pageRouteVersionRepository.GetPageRouteVersionByPageType(PageTypeConsts.CityPlan);
            if (ModelState.IsValid)
            {
                var cityPlanYearVersionByCityPlanYearId = _cityPlanYearVersionRepository.GetByCityYearId(viewModel.CityPlanYearId ?? 0);
                var cityPlanYearVersionById = _cityPlanYearVersionRepository.GetDetail(viewModel.Id);
                cityPlanYearVersionByCityPlanYearId = cityPlanYearVersionById == null ? cityPlanYearVersionByCityPlanYearId : cityPlanYearVersionById;
                var cityPlanYearVersionModel = viewModel.MapToCityPlanYearVersion();

                if (viewModel.EnFile != null)
                    cityPlanYearVersionModel.EnFileUrl = _fileService.UploadFileUrlNew(viewModel.EnFile);
                if (viewModel.ArFile != null)
                    cityPlanYearVersionModel.ArFileUrl = _fileService.UploadFileUrlNew(viewModel.ArFile);

                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (cityPlanYearVersionByCityPlanYearId == null || cityPlanYearVersionModel.VersionStatusEnum == VersionStatusEnum.Approved || cityPlanYearVersionModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    cityPlanYearVersionModel.CreatedById = user.Id;
                    cityPlanYearVersionModel.CreationDate = DateTime.Now;
                    cityPlanYearVersionModel.VersionStatusEnum = VersionStatusEnum.Draft;
                    cityPlanYearVersionModel.ChangeActionEnum = ChangeActionEnum.Update;
                    cityPlanYearVersionModel.Id = 0;
                    cityPlanYearVersionModel.CityPlanYearId = viewModel.CityPlanYearId > 0 ? viewModel.CityPlanYearId : (int?)null;
                    pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                    _cityPlanYearVersionRepository.Add(cityPlanYearVersionModel);
                    _pageRouteVersionRepository.Update(pageRouteVersion);
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Static Page > Citizen Plan > Year Plan > Edit", "Year Plan ID " + cityPlanYearVersionModel.CityPlanYearId);

                    return RedirectToAction(nameof(Index), new { cityPlanVerId = cityPlanYearVersionModel.CityPlanVersionId });
                }

                cityPlanYearVersionModel.Id = cityPlanYearVersionByCityPlanYearId != null ? cityPlanYearVersionByCityPlanYearId.Id : viewModel.Id;
                pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Draft;

                _cityPlanYearVersionRepository.Update(cityPlanYearVersionModel);
                _pageRouteVersionRepository.Update(pageRouteVersion);
                _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Static Page > Citizen Plan > Year Plan > Edit", "Year Plan ID " + cityPlanYearVersionModel.Id);

                return RedirectToAction(nameof(Index), new { cityPlanVerId = cityPlanYearVersionModel.CityPlanVersionId });
            }
            return View(viewModel);
        }

        /// <summary>
        /// details for city plan year 
        /// </summary>
        /// <param name="cityPlanYearId">city plan year id</param>
        /// <param name="id">city plan id</param>
        /// <returns></returns>
        private IActionResult DetailsMethod(int id, int cityPlanYearId)
        {
            CityPlanYearEditViewModel viewModel;
            viewModel = GetDetailModel(id, cityPlanYearId);
            //remove id value from route
            ModelState.Clear();
            return View(viewModel);
        }

        /// <summary>
        /// Core method for details city plan year
        /// </summary>
        /// <param name="id">city plan id</param>
        /// <param name="cityPlanYearId">city plan year id</param>
        /// <returns></returns>
        private CityPlanYearEditViewModel GetDetailModel(int id, int cityPlanYearId)
        {
            CityPlanYearEditViewModel viewModel;
            var cityPlanYearVersion = _cityPlanYearVersionRepository.GetByCityYearId(cityPlanYearId);
            if (cityPlanYearVersion == null || cityPlanYearVersion.VersionStatusEnum == VersionStatusEnum.Approved || cityPlanYearVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                var cityPlanYear = _cityPlanYearRepository.GetDetail(cityPlanYearId);
                if (cityPlanYear != null)
                {
                    viewModel = cityPlanYear.MapToCityPlanYearViewModel();
                    viewModel.CityPlanId = cityPlanYearVersion != null ? cityPlanYearVersion.CityPlanVersionId : viewModel.CityPlanId;
                    if (cityPlanYearVersion != null)
                    {
                        viewModel.VersionStatusEnum = cityPlanYearVersion.VersionStatusEnum;
                        viewModel.CityPlanId = id;
                    }
                }
                else
                {
                    cityPlanYearVersion = _cityPlanYearVersionRepository.GetByCityPlanVer(id);
                    viewModel = cityPlanYearVersion.MapToCityPlanYearViewModel();
                }
            }
            else
            {
                viewModel = cityPlanYearVersion.MapToCityPlanYearViewModel();
            }

            return viewModel;
        }

        /// <summary>
        /// Post method for update
        /// </summary>
        /// <param name="cityPlanYear">city plan year object</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.CitizenPlan)]
        [RequestSizeLimit(500000000)]
        public async Task<IActionResult> Edit(CityPlanYearEditViewModel cityPlanYear)
        {
            return await EditMethod(cityPlanYear);
        }

        /// <summary>
        /// Get method for details city plan year
        /// </summary>
        /// <param name="id">city plan id</param>
        /// <param name="cityPlanYearId">city plan year id</param>
        /// <param name="approvalId">approval notification id</param>
        /// <param name="cityPlanVerId">city plan version id</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.CitizenPlan)]
        public IActionResult Details(int id, [FromQuery] int cityPlanYearId, [FromQuery] int approvalId, [FromQuery] int cityPlanVerId)
        {
            ViewBag.cityPlanVerId = cityPlanVerId;
            ViewBag.approvalId = approvalId;
            return DetailsMethod(id, cityPlanYearId);
        }

        /// <summary>
        /// Delete city plan year
        /// </summary>
        /// <param name="cityPlanVersionId">city plan id</param>
        /// <param name="cityPlanYearId">city plan year id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanDelete }, StaticPagesIdsConst.CitizenPlan)]
        public async Task<IActionResult> Delete(int id, int cityPlanVersionId, int cityPlanYearId)
        {
            try
            {
                var pageRouteVersion = _pageRouteVersionRepository.GetPageRouteVersionByPageType(PageTypeConsts.CityPlan);
                var cityPlanYearVersion = _cityPlanYearVersionRepository.GetByCityYearIdNotIgnoren(cityPlanYearId);
                if (cityPlanYearVersion != null)
                {
                    cityPlanYearVersion.CityPlanVersionId = cityPlanVersionId; cityPlanYearVersion.IsDeleted = true;
                    await EditMethod(cityPlanYearVersion.MapToCityPlanYearViewModel());
                }
                else
                {
                    var cityPlanYear = _cityPlanYearRepository.GetByIdWithNoTracking(cityPlanYearId);
                    if (cityPlanYear != null)
                    {
                        cityPlanYear.IsDeleted = true;
                        await EditMethod(cityPlanYear.MapToCityPlanYearViewModel());
                    }
                    else
                    {
                        _cityPlanYearVersionRepository.Delete(id);
                        pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                        _pageRouteVersionRepository.Update(pageRouteVersion);
                    }

                }


                TempData[notificationMessageKey] = ToasrMessages.DeleteSuccess;// </br> It will take effect after admin approval.";
                TempData[notificationTypeKey] = notificationSuccess;
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Static Page > Citizen Plan > Year Plan > Delete", "Year Plan ID " + cityPlanYearId);
                return Json(new { });
            }
            catch
            {
                TempData[notificationMessageKey] = "Error has been occurred.";
                TempData[notificationTypeKey] = notificationError;
                return Json(new { });
            }


        }

        /// <summary>
        /// Get all city plan year objects
        /// </summary>
        /// <param name="id">city plan id</param>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.CitizenPlan)]
        public JsonResult GetCityPlanYear(int id)
        {
            var pageMinistry = _cityPlanYearVersionRepository.GetByCityVerId(id);
            var subApplicationName = _configuration.GetSection("SubApplicationName").Value;
            if (!string.IsNullOrWhiteSpace(subApplicationName))
                foreach (var item in pageMinistry)
                {
                    item.ArFileUrl = subApplicationName + item.ArFileUrl;
                    item.EnFileUrl = subApplicationName + item.EnFileUrl;
                }
            return Json(new { data = pageMinistry });
        }

        /// <summary>
        /// Get all remaining gov objects
        /// </summary>
        /// <param name="year">selected year</param>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.CitizenPlan)]
        public JsonResult GetRemainingGov(string year)
        {
            List<string> objGov = new List<string>();
            objGov = _cityPlanYearRepository.Get().Where(i => i.GovYear == year).Select(j => j.GovName).Distinct().ToList();
            var governs = _dFGovRepository.GetAll().Where(i => !objGov.Contains(i.EnName)).Select(i => new { Id = i.Id, Text = i.EnName });
            return Json(governs);
        }
    }
}