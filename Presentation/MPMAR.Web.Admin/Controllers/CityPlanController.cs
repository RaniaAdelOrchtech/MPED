using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MPMAR.Business.Interfaces;
using MPMAR.Common;
using MPMAR.Common.Helpers;
using MPMAR.Data;
using MPMAR.Data.Consts;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;
using MPMAR.Business;
using static MPMAR.Data.Enums.Enums;
using MPMAR.Common.Interfaces;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class CityPlanController : Controller
    {
        private string notificationMessageKey = "NotificationMessage";
        private string notificationTypeKey = "NotificationType";
        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";

        private readonly IPageRouteVersionRepository _pageRouteVersionRepository;
        private readonly ICityPlanRepository _cityPlanRepository;
        private readonly ICityPlanYearRepository _cityPlanYearRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<CityPlanController> _eventLogger;
        private readonly IFileService _fileService;
        private readonly ICityPlanVersionRepository _cityPlanVersionRepository;
        private readonly ICityPlanYearVersionRepository _cityPlanYearVersionRepository;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;

        public CityPlanController(IPageRouteVersionRepository pageRouteVersionRepository, ICityPlanRepository cityPlanRepository, ICityPlanYearRepository cityPlanYearRepository, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IEventLogger<CityPlanController> eventLogger, IFileService fileService, ICityPlanVersionRepository cityPlanVersionRepository, ICityPlanYearVersionRepository cityPlanYearVersionRepository, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _pageRouteVersionRepository = pageRouteVersionRepository;
            _cityPlanRepository = cityPlanRepository;
            _cityPlanYearRepository = cityPlanYearRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _fileService = fileService;
            _cityPlanVersionRepository = cityPlanVersionRepository;
            _cityPlanYearVersionRepository = cityPlanYearVersionRepository;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }

        /// <summary>
        /// Index for all approved city plan objects
        /// </summary>
        /// <param name="pageRouteId">page route id</param>
        /// <param name="approvalId">Approval notification id</param>
        /// <param name="cityPlanVerId">city plan version id</param>
        /// <returns></returns>
        [Authorize]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.CitizenPlan)]
        public IActionResult Index([FromQuery] int pageRouteId, [FromQuery] int approvalId, [FromQuery] int cityPlanVerId)
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

            List<CityPlan> pageSectionVersion = _cityPlanRepository.Get().ToList();
            if (pageSectionVersion == null)
            {
                return View();
            }

            ViewBag.cityPlanVerId = cityPlanVerId;
            ViewBag.pageRouteId = pageRouteId;
            ViewBag.approvalId = approvalId;
            return View(pageSectionVersion);
        }

        /// <summary>
        /// Get method for create a new city plan object
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd }, StaticPagesIdsConst.CitizenPlan)]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post method for adding a new city plan object
        /// </summary>
        /// <param name="cityPlanVM">city plan view model</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd }, StaticPagesIdsConst.CitizenPlan)]
        public async Task<IActionResult> Create(CityPlanEditViewModel cityPlanVM)
        {
            if (ModelState.IsValid)
            {

                await EditMethod(cityPlanVM);

                return RedirectToAction(nameof(Index));
            }
            return View(cityPlanVM);
        }

        /// <summary>
        /// Get method for editing a city plan object
        /// </summary>
        /// <param name="id">city plan version id</param>
        /// <param name="CityPlanId">city plan id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.CitizenPlan)]
        public IActionResult Edit(int id, [FromQuery] int CityPlanId)
        {
            return DetailsMethod(id, CityPlanId);
        }

        /// <summary>
        /// Post method for editing a city plan object
        /// </summary>
        /// <param name="viewModel">city plan view model</param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanEdit }, StaticPagesIdsConst.CitizenPlan)]
        public async Task<IActionResult> Edit(CityPlanEditViewModel viewModel)
        {
            return await EditMethod(viewModel);
        }

        /// <summary>
        /// Core method for create or edit a city plan object
        /// </summary>
        /// <param name="viewModel">city plan view model</param>
        /// <returns></returns>
        private async Task<IActionResult> EditMethod(CityPlanEditViewModel viewModel)
        {
            var pageRouteVersion = _pageRouteVersionRepository.GetPageRouteVersionByPageType(PageTypeConsts.CityPlan);
            if (ModelState.IsValid)
            {
                var cityPlanVersionByCityPlanId = _cityPlanVersionRepository.GetByCityId(viewModel.CityPlanId ?? 0);
                var cityPlanVersionById = _cityPlanVersionRepository.GetDetail(viewModel.Id);
                cityPlanVersionByCityPlanId = cityPlanVersionById == null ? cityPlanVersionByCityPlanId : cityPlanVersionById;
                var cityPlanVersionModel = viewModel.MapToCityPlanVersion();

                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (cityPlanVersionByCityPlanId == null || cityPlanVersionModel.VersionStatusEnum == VersionStatusEnum.Approved || cityPlanVersionModel.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    cityPlanVersionModel.CreatedById = user.Id;
                    cityPlanVersionModel.CreationDate = DateTime.Now;
                    cityPlanVersionModel.VersionStatusEnum = VersionStatusEnum.Draft;
                    cityPlanVersionModel.ChangeActionEnum = ChangeActionEnum.Update;
                    cityPlanVersionModel.Id = 0;
                    cityPlanVersionModel.CityPlanId = viewModel.CityPlanId > 0 ? viewModel.CityPlanId : (int?)null;
                    pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Draft;

                    _cityPlanVersionRepository.Add(cityPlanVersionModel);
                    _pageRouteVersionRepository.Update(pageRouteVersion);
                    _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                    return RedirectToAction(nameof(Index));
                }

                cityPlanVersionModel.Id = cityPlanVersionByCityPlanId != null ? cityPlanVersionByCityPlanId.Id : viewModel.Id;
                pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Draft;

                _cityPlanVersionRepository.Update(cityPlanVersionModel);
                _pageRouteVersionRepository.Update(pageRouteVersion);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        /// <summary>
        /// Core method for Details a city plan object
        /// </summary>
        /// <param name="id">city plan version id</param>
        /// <param name="cityPlanId">city plan id</param>
        /// <returns></returns>
        [Authorize]
        private IActionResult DetailsMethod(int id, int cityPlanId)
        {
            CityPlanEditViewModel viewModel;
            var cityPlanVersion = _cityPlanVersionRepository.GetByCityId(cityPlanId);
            if (cityPlanVersion == null || cityPlanVersion.VersionStatusEnum == VersionStatusEnum.Approved || cityPlanVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                var cityPlan = _cityPlanRepository.GetDetail(cityPlanId);

                if (cityPlan != null)
                {
                    viewModel = cityPlan.MapToCityPlanViewModel();
                    if (cityPlanVersion != null && cityPlanVersion.VersionStatusEnum == VersionStatusEnum.Ignored)
                    {
                        viewModel.VersionStatusEnum = VersionStatusEnum.Ignored;
                    }
                }
                else
                {
                    cityPlanVersion = _cityPlanVersionRepository.GetDetail(id);
                    viewModel = cityPlanVersion.MapToCityPlanViewModel();
                }
            }
            else
            {
                viewModel = cityPlanVersion.MapToCityPlanViewModel();
            }
            //remove id value from route
            ModelState.Clear();
            return View(viewModel);
        }

        /// <summary>
        /// Get method for details a city plan object
        /// </summary>
        /// <param name="id">city plan version id</param>
        /// <param name="cityPlanId">city plan id</param>
        /// <param name="approvalId">approval notification id</param>
        /// <returns></returns>
        [HttpGet]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.CitizenPlan)]
        public IActionResult Details(int id, [FromQuery] int cityPlanId, [FromQuery] int approvalId)
        {
            ViewBag.cityPlanVerId = id;
            ViewBag.approvalId = approvalId;
            return DetailsMethod(id, cityPlanId);
        }

        /// <summary>
        /// Delete a city plan object
        /// </summary>
        /// <param name="id">city plan version</param>
        /// <param name="cityPlanId">city plan id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanDelete }, StaticPagesIdsConst.CitizenPlan)]
        public async Task<IActionResult> Delete(int id, int cityPlanId)
        {

            try
            {
                var pageRouteVersion = _pageRouteVersionRepository.GetPageRouteVersionByPageType(PageTypeConsts.CityPlan);
                var cityPlanVersion = _cityPlanVersionRepository.GetByCityId(cityPlanId);
                if (cityPlanVersion != null)
                {
                    cityPlanVersion.IsDeleted = true;
                    await EditMethod(cityPlanVersion.MapToCityPlanViewModel());
                }
                else
                {
                    var cityPlan = _cityPlanRepository.GetByIdWithNoTracking(cityPlanId);
                    if (cityPlan != null)
                    {
                        cityPlan.IsDeleted = true;
                        await EditMethod(cityPlan.MapToCityPlanViewModel());
                    }
                    else
                    {
                        _cityPlanVersionRepository.Delete(id);
                        pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Draft;
                        _pageRouteVersionRepository.Update(pageRouteVersion);
                    }

                }


                TempData[notificationMessageKey] = ToasrMessages.DeleteSuccess;// </br> It will take effect after admin approval.";
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Static Page > Citizen Plan > Delete", "Citizen Plan ID " + cityPlanVersion.Id);
                TempData[notificationTypeKey] = notificationSuccess;
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
        /// Get list for city plan approved objects
        /// </summary>
        /// <param name="id">city plan version id</param>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanView }, StaticPagesIdsConst.CitizenPlan)]
        public JsonResult GetCityPlan(int id)
        {
            var pageMinistry = _cityPlanVersionRepository.Get();
            if (id != 0)
            {
                pageMinistry = pageMinistry.Where(x => x.Id == id).ToList();
                ViewBag.cityPlanVerId = id;
            }

            return Json(new { data = pageMinistry });
        }

        /// <summary>
        /// method allow super admin and content manager for submit their changes in the city plan objects
        /// </summary>
        /// <param name="cityPlanId">city plan id</param>
        /// <param name="id">city plan version id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanDelete, PrivilegesActions.CanEdit }, StaticPagesIdsConst.CitizenPlan)]
        public async Task<IActionResult> SubmitChanges([FromQuery] int cityPlanId, [FromQuery] int id)
        {
            var pageRouteVersion = _pageRouteVersionRepository.GetPageRouteVersionByPageType(PageTypeConsts.CityPlan);
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var cityPlanVersion = _cityPlanVersionRepository.GetDetail(id);

            if (cityPlanVersion != null)
            {
                cityPlanVersion.VersionStatusEnum = VersionStatusEnum.Submitted;
                _cityPlanVersionRepository.Update(cityPlanVersion);
            }
            var cityPlanYearVersion = _cityPlanYearVersionRepository.GetDraftByCityId(cityPlanVersion.Id);
            foreach (var record in cityPlanYearVersion)
            {
                record.VersionStatusEnum = VersionStatusEnum.Submitted;
                _cityPlanYearVersionRepository.Update(record);
            }

            var ifApprovalExist = _approvalNotificationsRepository.GetByPageNameAndRelatedId(PagesNamesConst.CityPlan, id);

            if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
            {
                ApprovalNotification approval = new ApprovalNotification()
                {
                    ChangeAction = ChangeActionEnum.Update,
                    VersionStatusEnum = VersionStatusEnum.Submitted,
                    ChangesDateTime = DateTime.Now,
                    ChangeType = ChangeType.PageContent,
                    PageLink = $"/{nameof(CityPlanController)[0..^10]}?cityPlanVerId={id}",
                    PageName = PagesNamesConst.CityPlan,
                    RelatedVersionId = id,
                    PageType = PageType.Static,
                    ContentManagerId = user.Id
                };
                _approvalNotificationsRepository.Add(approval);

            }
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Submitted, "Static Page > Citizen Plan > Submitted", "Citizen Plan ID " + id);
            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);

            pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Submitted;
            _pageRouteVersionRepository.Update(pageRouteVersion);



            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// method allow approval user for approve changes in the city plan objects
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <param name="approvalId">approval notification id</param>
        /// <param name="cityPlanVerId">city plan version id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanApprove }, StaticPagesIdsConst.CitizenPlan)]
        public async Task<IActionResult> Approve(int id, [FromQuery] int approvalId, [FromQuery] int cityPlanVerId)
        {
            var pageRouteVersion = _pageRouteVersionRepository.GetPageRouteVersionByPageType(PageTypeConsts.CityPlan);
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var cityPlanVersion = _cityPlanVersionRepository.GetDetail(cityPlanVerId);
            var newCityPlan = new CityPlan();
            if (cityPlanVersion != null)
            {
                cityPlanVersion.ApprovalDate = DateTime.Now;
                cityPlanVersion.ApprovedById = user.Id;
                cityPlanVersion.VersionStatusEnum = VersionStatusEnum.Approved;

                newCityPlan = new CityPlan()
                {
                    ArAlexandria = cityPlanVersion.ArAlexandria,
                    ArAswan = cityPlanVersion.ArAswan,
                    ArAsyut = cityPlanVersion.ArAlexandria,
                    ArBeheira = cityPlanVersion.ArBeheira,
                    ArCairo = cityPlanVersion.ArCairo,
                    ArDakahlia = cityPlanVersion.ArDakahlia,
                    ArDamietta = cityPlanVersion.ArDamietta,
                    ArFaiyum = cityPlanVersion.ArFaiyum,
                    ArGharbia = cityPlanVersion.ArGharbia,
                    ArBeniSuef = cityPlanVersion.ArBeniSuef,
                    ArGiza = cityPlanVersion.ArGiza,
                    ArIsmailia = cityPlanVersion.ArIsmailia,
                    ArKafrElSheikh = cityPlanVersion.ArKafrElSheikh,
                    ArLuxor = cityPlanVersion.ArLuxor,
                    ArMatruh = cityPlanVersion.ArMatruh,
                    ArMinya = cityPlanVersion.ArMinya,
                    ArMonufia = cityPlanVersion.ArMonufia,
                    ArNewValley = cityPlanVersion.ArNewValley,
                    ArPageDescription = cityPlanVersion.ArPageDescription,
                    ArNorthSinai = cityPlanVersion.ArNorthSinai,
                    ArPortSaid = cityPlanVersion.ArPortSaid,
                    ArQalyubia = cityPlanVersion.ArQalyubia,
                    ArQena = cityPlanVersion.ArQena,
                    ArRedSea = cityPlanVersion.ArRedSea,
                    ArSharqia = cityPlanVersion.ArSharqia,
                    ArSohag = cityPlanVersion.ArSohag,
                    ArSuez = cityPlanVersion.ArSuez,
                    ArSouthSinai = cityPlanVersion.ArSouthSinai,

                    EnAlexandria = cityPlanVersion.EnAlexandria,
                    EnAswan = cityPlanVersion.EnAswan,
                    EnAsyut = cityPlanVersion.EnAlexandria,
                    EnBeheira = cityPlanVersion.EnBeheira,
                    EnCairo = cityPlanVersion.EnCairo,
                    EnDakahlia = cityPlanVersion.EnDakahlia,
                    EnDamietta = cityPlanVersion.EnDamietta,
                    EnFaiyum = cityPlanVersion.EnFaiyum,
                    EnGharbia = cityPlanVersion.EnGharbia,
                    EnBeniSuef = cityPlanVersion.EnBeniSuef,
                    EnGiza = cityPlanVersion.EnGiza,
                    EnIsmailia = cityPlanVersion.EnIsmailia,
                    EnKafrElSheikh = cityPlanVersion.EnKafrElSheikh,
                    EnLuxor = cityPlanVersion.EnLuxor,
                    EnMatruh = cityPlanVersion.EnMatruh,
                    EnMinya = cityPlanVersion.EnMinya,
                    EnMonufia = cityPlanVersion.EnMonufia,
                    EnNewValley = cityPlanVersion.EnNewValley,
                    EnPageDescription = cityPlanVersion.EnPageDescription,
                    EnNorthSinai = cityPlanVersion.EnNorthSinai,
                    EnPortSaid = cityPlanVersion.EnPortSaid,
                    EnQalyubia = cityPlanVersion.EnQalyubia,
                    EnQena = cityPlanVersion.EnQena,
                    EnRedSea = cityPlanVersion.EnRedSea,
                    EnSharqia = cityPlanVersion.EnSharqia,
                    EnSohag = cityPlanVersion.EnSohag,
                    EnSuez = cityPlanVersion.EnSuez,
                    EnSouthSinai = cityPlanVersion.EnSouthSinai,

                    CreationDate = DateTime.Now,
                    CreatedById = user.Id,
                    IsActive = cityPlanVersion.IsActive,
                    IsDeleted = cityPlanVersion.IsDeleted,

                };

                if (cityPlanVersion.CityPlanId != null)
                {
                    newCityPlan.Id = cityPlanVersion.CityPlanId ?? 0;
                    _cityPlanRepository.Update(newCityPlan);
                }
                else
                {
                    newCityPlan.Id = 0;
                    _cityPlanRepository.Add(newCityPlan);
                    cityPlanVersion.CityPlanId = newCityPlan.Id;
                }
                _cityPlanVersionRepository.Update(cityPlanVersion);
            }

            var cityPlanYearVersion = _cityPlanYearVersionRepository.GetSubmitedtByCityId(cityPlanVerId);

            foreach (var record in cityPlanYearVersion)
            {
                record.ApprovalDate = DateTime.Now;
                record.ApprovedById = user.Id;
                record.VersionStatusEnum = VersionStatusEnum.Approved;


                var newCityPlanYear = new CityPlanYear()
                {
                    //Id = record.PageMinistryId ?? 0,
                    ArFileUrl = record.ArFileUrl,
                    EnFileUrl = record.EnFileUrl,
                    GovName = record.GovName,
                    GovYear = record.GovYear,
                    IsMapActive = record.IsMapActive,
                    CreatedById = user.Id,
                    CreationDate = DateTime.Now,
                    CityPlanId = newCityPlan.Id,
                    IsActive = record.IsActive,
                    IsDeleted = record.IsDeleted,
                    DFGovId = record.DFGovId
                };
                if (record.CityPlanYearId != null)
                {
                    newCityPlanYear.Id = record.CityPlanYearId ?? 0;
                    _cityPlanYearRepository.Update(newCityPlanYear);
                }
                else
                {
                    newCityPlanYear.Id = 0;
                    _cityPlanYearRepository.Add(newCityPlanYear);
                    record.CityPlanYearId = newCityPlanYear.Id;
                }

                _cityPlanYearVersionRepository.Update(record);
            }

            approval.VersionStatusEnum = VersionStatusEnum.Approved;
            approval.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approval);

            pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Approved;
            _pageRouteVersionRepository.Update(pageRouteVersion);

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Approve, "Static Page > Citizen Plan > Approved", "Citizen Plan ID " + id);

            return RedirectToAction("Index", "ApprovalNotifications");
        }

        /// <summary>
        /// method allow approval user for ignore changes in the city plan objects
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <param name="approvalId">approval notification id</param>
        /// <param name="cityPlanVerId">city plan version id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.StaticPage, new PrivilegesActions[] { PrivilegesActions.CanApprove }, StaticPagesIdsConst.CitizenPlan)]
        public async Task<IActionResult> Ignore(int id, [FromQuery] int approvalId, [FromQuery] int cityPlanVerId)
        {
            var pageRouteVersion = _pageRouteVersionRepository.GetPageRouteVersionByPageType(PageTypeConsts.CityPlan);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var cityPlanVersion = _cityPlanVersionRepository.GetDetail(cityPlanVerId);

            if (cityPlanVersion != null)
            {
                cityPlanVersion.ApprovalDate = DateTime.Now;
                cityPlanVersion.ApprovedById = user.Id;
                cityPlanVersion.VersionStatusEnum = VersionStatusEnum.Ignored;
            }
            _cityPlanVersionRepository.Update(cityPlanVersion);

            var cityPlanYearVersion = _cityPlanYearVersionRepository.GetSubmitedtByCityId(cityPlanVerId);

            foreach (var record in cityPlanYearVersion)
            {
                record.VersionStatusEnum = VersionStatusEnum.Ignored;
                cityPlanVersion.ApprovalDate = DateTime.Now;
                cityPlanVersion.ApprovedById = user.Id;
                _cityPlanYearVersionRepository.Update(record);
            }

            approval.VersionStatusEnum = VersionStatusEnum.Ignored;
            approval.ChangesDateTime = DateTime.Now;
            _approvalNotificationsRepository.Update(approval);

            pageRouteVersion.ContentVersionStatusEnum = VersionStatusEnum.Approved;
            _pageRouteVersionRepository.Update(pageRouteVersion);

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Reject, "Static Page > Citizen Plan > Ignored", "Citizen Plan ID " + id);


            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);
            return RedirectToAction("Index", "ApprovalNotifications");
        }

    }


}