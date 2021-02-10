using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Enums;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Interfaces.Analytics;
using MPMAR.Business.Services.Analytics.ViewModels;
using MPMAR.Common;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.Mappers;
using NToastNotify;
using MPMAR.Business;
using MPMAR.Common.Interfaces;
using MPMAR.Data.Consts;
using System.Diagnostics;
using Abp.Events.Bus.Entities;
using Microsoft.AspNetCore.Identity;
using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class ActivityController : Controller
    {
        private readonly IActivityConstantRepository _activityConstantRepository;
        private readonly IActivityCurrentRepository _activityCurrenttRepository;
        private readonly IDFIndicatorRepository _dFIndicatorRepository;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<ActivityController> _eventLogger;
        private readonly IDFYearsRepository _dFYearsRepository;
        private readonly IDFQuartersRepository _dFQuartersRepository;
        private readonly IDFSourceRepository _dFSourceRepository;
        private readonly IDFUnitRepository _dFUnitRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;
        private readonly IDFSectorsRepository _dFSectorsRepository;
        private readonly ISectorGrowthRepository _sectorGrowthRepository;

        public ActivityController(IActivityConstantRepository activityConstantRepository, ISectorGrowthRepository sectorGrowthRepository, IDFSectorsRepository dFSectorsRepository, IActivityCurrentRepository activityCurrenttRepository, IToastNotification toastNotification, IEventLogger<ActivityController> eventLogger, IDFIndicatorRepository dFIndicatorRepository, IDFYearsRepository dFYearsRepository, IDFQuartersRepository dFQuartersRepository, IDFSourceRepository dFSourceRepository, IDFUnitRepository dFUnitRepository, UserManager<ApplicationUser> userManager, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _activityConstantRepository = activityConstantRepository;
            _activityCurrenttRepository = activityCurrenttRepository;
            _dFIndicatorRepository = dFIndicatorRepository;
            _dFYearsRepository = dFYearsRepository;
            _dFQuartersRepository = dFQuartersRepository;
            _dFSourceRepository = dFSourceRepository;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _dFUnitRepository = dFUnitRepository;
            _userManager = userManager;
            _approvalNotificationsRepository = approvalNotificationsRepository;
            _dFSectorsRepository = dFSectorsRepository;
            _sectorGrowthRepository = sectorGrowthRepository;
        }
        /// <summary>
        /// Get Activity View Based On Sheet Type
        /// </summary>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.EconomicIndicator, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery] int sheetType, [FromQuery]int approvalId)
        {
            if (sheetType == (int)SheetTypeEnum.ActivityConst)
                ViewBag.Type = ActivityConstants.ActivityConstTypeName;

            if (sheetType == (int)SheetTypeEnum.ActivityCurrent)
                ViewBag.Type = ActivityConstants.ActivityCurrentTypeName;

            if (sheetType == (int)SheetTypeEnum.SectorGrowthRates)
                ViewBag.Type = ActivityConstants.SectorGrowthRatesTypeName;
            ViewBag.approvalId = approvalId;
            return View();
        }
        /// <summary>
        /// Get Create New Activity View For Specefic Sheet Type
        /// </summary>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.EconomicIndicator, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create([FromQuery] int sheetType)
        {
            ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn");
            ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn");
            ViewBag.DFSectorId = new SelectList(_dFSectorsRepository.GetAll(), "Id", "NameEn");

            if (sheetType == (int)SheetTypeEnum.ActivityConst)
            {
                ViewBag.Type = ActivityConstants.ActivityConstTypeName;
                ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(ActivityConstants.ActivityConstIndicatiorId).NameEn;
                ViewBag.SourceName = _dFSourceRepository.GetByID(ActivityConstants.ActivityConstSourceId).NameEn;
                ViewBag.Unit = ActivityConstants.ActivityConstUnit;
                ViewBag.DFYearBaseId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn");
            }

            if (sheetType == (int)SheetTypeEnum.ActivityCurrent)
            {
                ViewBag.Type = ActivityConstants.ActivityCurrentTypeName;
                ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(ActivityConstants.ActivityCurrentIndicatiorId).NameEn;
                ViewBag.SourceName = _dFSourceRepository.GetByID(ActivityConstants.ActivityCurrentSourceId).NameEn;
                ViewBag.Unit = ActivityConstants.ActivityCurrentUnit;
            }

            if (sheetType == (int)SheetTypeEnum.SectorGrowthRates)
            {
                ViewBag.Type = ActivityConstants.SectorGrowthRatesTypeName;
                ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(ActivityConstants.SectorGrowthRatesIndicatiorId).NameEn;
                ViewBag.SourceName = _dFSourceRepository.GetByID(ActivityConstants.SectorGrowthRatesSourceId).NameEn;
                ViewBag.Unit = ActivityConstants.SectorGrowthRatesUnit;
            }

            return View();
        }
        /// <summary>
        /// Create New Activity For Specefic Sheet Type
        /// </summary>
        /// <param name="activityVM">Activity View Model</param>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ActivityVMForCreateEdit activityVM, [FromQuery] int sheetType) //Post method for create new activity
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            activityVM.CreatedById = user.Id;
            if (!ModelState.IsValid)
            {
                if (sheetType == (int)SheetTypeEnum.ActivityConst)
                {
                    ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(ActivityConstants.ActivityConstIndicatiorId).NameEn;
                    ViewBag.SourceName = _dFSourceRepository.GetByID(ActivityConstants.ActivityConstSourceId).NameEn;
                    ViewBag.Unit = ActivityConstants.ActivityConstUnit;
                    ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn");
                    ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn");
                    ViewBag.DFSectorId = new SelectList(_dFSectorsRepository.GetAll(), "Id", "NameEn");
                    ViewBag.DFYearBaseId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn");
                    ViewBag.Type = ActivityConstants.ActivityConstTypeName;
                }
                if (sheetType == (int)SheetTypeEnum.ActivityCurrent)
                {
                    ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(ActivityConstants.ActivityCurrentIndicatiorId).NameEn;
                    ViewBag.SourceName = _dFSourceRepository.GetByID(ActivityConstants.ActivityCurrentSourceId).NameEn;
                    ViewBag.Unit = ActivityConstants.ActivityCurrentUnit;
                    ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn");
                    ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn");
                    ViewBag.DFSectorId = new SelectList(_dFSectorsRepository.GetAll(), "Id", "NameEn");
                    ViewBag.Type = ActivityConstants.ActivityCurrentTypeName;
                }
                if (sheetType == (int)SheetTypeEnum.SectorGrowthRates)
                {
                    ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(ActivityConstants.SectorGrowthRatesIndicatiorId).NameEn;
                    ViewBag.SourceName = _dFSourceRepository.GetByID(ActivityConstants.SectorGrowthRatesSourceId).NameEn;
                    ViewBag.Unit = ActivityConstants.SectorGrowthRatesUnit;
                    ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn");
                    ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn");
                    ViewBag.DFSectorId = new SelectList(_dFSectorsRepository.GetAll(), "Id", "NameEn");
                    ViewBag.Type = ActivityConstants.SectorGrowthRatesTypeName;
                }
                return View(activityVM);
            }


            activityVM.DFSourceId = ActivityConstants.ActivityConstIndicatiorId;
            if (sheetType == (int)SheetTypeEnum.ActivityConst)
            {
                activityVM.DFIndicatorId = ActivityConstants.ActivityConstIndicatiorId;
                activityVM.DFUnitId = ActivityConstants.ActivityConstUnitId;

                _activityConstantRepository.Add(activityVM.MapToActivityConst());
            }
            if (sheetType == (int)SheetTypeEnum.ActivityCurrent)
            {
                activityVM.DFIndicatorId = ActivityConstants.ActivityCurrentIndicatiorId;
                activityVM.DFUnitId = ActivityConstants.ActivityCurrentUnitId;
                activityVM.DFSourceId = ActivityConstants.ActivityConstSourceId;
                _activityCurrenttRepository.AddVer(activityVM.MapToActivityCurrentVer());
            }
            if (sheetType == (int)SheetTypeEnum.SectorGrowthRates)
            {
                activityVM.DFIndicatorId = ActivityConstants.SectorGrowthRatesIndicatiorId;
                activityVM.DFUnitId = ActivityConstants.SectorGrowthRatesUnitId;
                activityVM.DFSourceId = ActivityConstants.SectorGrowthRatesSourceId;
                _sectorGrowthRepository.AddVer(activityVM.MapToSectorGrowthVer());
            }

            _toastNotification.AddSuccessToastMessage("Element has been Created successfully.");


            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Definitions > Economic Indicators > Activity > " + sheetType + " > Add", sheetType + "Element has been Created successfully.");


            return RedirectToAction(nameof(Index), new { sheetType = sheetType });
        }

        /// <summary>
        /// Get Edit Activity View For Specefic activity and Sheet Type
        /// </summary>
        /// <param name="id">activity id</param>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.EconomicIndicator, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery] int sheetType, [FromQuery]bool isVersion)
        {
            if (id == null)
                return NotFound();

            ActivityConstant activityConstant = null;
            ActivityCurrent activityCurrent = null;
            SectorGrowthRate sectorGrowthRate = null;
            ActivityVMForCreateEdit activityVMForCreateEdit = null;


            //if (sheetType == (int)SheetTypeEnum.ActivityConst)
            //    activityConstant = _activityConstantRepository.GetById(id);
            //if (sheetType == (int)SheetTypeEnum.ActivityCurrent)
            //    activityCurrent = _activityCurrenttRepository.GetById(id);
            //if (sheetType == (int)SheetTypeEnum.SectorGrowthRates)
            //    sectorGrowthRate = _sectorGrowthRepository.GetById(id);

            //if (activityConstant == null && activityCurrent == null && sectorGrowthRate == null)
            //    return NotFound(); 

            if (sheetType == (int)SheetTypeEnum.ActivityConst)
            {
                ViewBag.Type = ActivityConstants.ActivityConstTypeName;
                ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(activityConstant.DFIndicatorId).NameEn;
                ViewBag.IndicatorId = activityConstant.DFIndicatorId;
                ViewBag.SourceName = _dFSourceRepository.GetByID(activityConstant.DFSourceId).NameEn;
                ViewBag.Unit = _dFUnitRepository.GetByID(activityConstant.DFUnitId).NameEn;
                ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn", activityConstant.DFQuarterId);
                ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn", activityConstant.DFYearId);
                ViewBag.DFSectorId = new SelectList(_dFSectorsRepository.GetAll(), "Id", "NameEn", activityConstant.DFSectorId);
                return View(activityConstant.MapToActivityConstVM());
            }

            if (sheetType == (int)SheetTypeEnum.ActivityCurrent)
            {

                var activityCurrentVerModel = _activityCurrenttRepository.GetVerById(id);
                if (activityCurrentVerModel != null && isVersion)
                    activityVMForCreateEdit = activityCurrentVerModel.MapToActivityCurrentVMVer();
                else
                {
                    var componentRGDPModel = _activityCurrenttRepository.GetById(id);
                    activityVMForCreateEdit = componentRGDPModel.MapToActivityCurrentVM();
                }

                ViewBag.Type = ActivityConstants.ActivityCurrentTypeName;
                ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(activityVMForCreateEdit.DFIndicatorId).NameEn;
                ViewBag.IndicatorId = activityVMForCreateEdit.DFIndicatorId;
                ViewBag.SourceName = _dFSourceRepository.GetByID(activityVMForCreateEdit.DFSourceId).NameEn;
                ViewBag.Unit = _dFUnitRepository.GetByID(activityVMForCreateEdit.DFUnitId).NameEn;
                ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn", activityVMForCreateEdit.DFQuarterId);
                ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn", activityVMForCreateEdit.DFYearId);
                ViewBag.DFSectorId = new SelectList(_dFSectorsRepository.GetAll(), "Id", "NameEn", activityVMForCreateEdit.DFSectorId);

                return View(activityVMForCreateEdit);
            }

            if (sheetType == (int)SheetTypeEnum.SectorGrowthRates)
            {

                var sRGDPVerModel = _sectorGrowthRepository.GetVerById(id);
                if (sRGDPVerModel != null && isVersion)
                    activityVMForCreateEdit = sRGDPVerModel.MapToSectorGrowthVMVer();
                else
                {
                    var componentRGDPModel = _sectorGrowthRepository.GetById(id);
                    activityVMForCreateEdit = componentRGDPModel.MapToSectorGrowthVM();
                }

                ViewBag.Type = ActivityConstants.SectorGrowthRatesTypeName;
                ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(activityVMForCreateEdit.DFIndicatorId).NameEn;
                ViewBag.IndicatorId = activityVMForCreateEdit.DFIndicatorId;
                ViewBag.SourceName = _dFSourceRepository.GetByID(activityVMForCreateEdit.DFSourceId).NameEn;
                ViewBag.Unit = _dFUnitRepository.GetByID(activityVMForCreateEdit.DFUnitId).NameEn;
                ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn", activityVMForCreateEdit.DFQuarterId);
                ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn", activityVMForCreateEdit.DFYearId);
                ViewBag.DFSectorId = new SelectList(_dFSectorsRepository.GetAll(), "Id", "NameEn", activityVMForCreateEdit.DFSectorId);
                return View(activityVMForCreateEdit);
            }

            return View();
        }

        /// <summary>
        /// Edit Activity For Specefic Sheet Type
        /// </summary>
        /// <param name="activityVM">Activity View Model</param>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult Edit(ActivityVMForCreateEdit activityVM, [FromQuery] int sheetType)
        {
            if (ModelState.IsValid)
            {
                activityVM.DFSourceId = ActivityConstants.ActivityConstSourceId;
                if (sheetType == (int)SheetTypeEnum.ActivityConst)
                {
                    activityVM.DFIndicatorId = ActivityConstants.ActivityConstIndicatiorId;
                    activityVM.DFUnitId = ActivityConstants.ActivityConstUnitId;
                    _activityConstantRepository.Update(activityVM.MapToActivityConst());
                }
                if (sheetType == (int)SheetTypeEnum.ActivityCurrent)
                {
                    activityVM.DFIndicatorId = ActivityConstants.ActivityCurrentIndicatiorId;
                    activityVM.DFUnitId = ActivityConstants.ActivityCurrentUnitId;
                    return EditActivityVer(activityVM, out int id);
                }
                if (sheetType == (int)SheetTypeEnum.SectorGrowthRates)
                {
                    activityVM.DFIndicatorId = ActivityConstants.SectorGrowthRatesIndicatiorId;
                    activityVM.DFUnitId = ActivityConstants.SectorGrowthRatesUnitId;
                    return EditSRGDP(activityVM, out int id);
                }
                _toastNotification.AddSuccessToastMessage("Element has been Edited successfully.");
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Definitions > Economic Indicators > Activity > " + sheetType + " > Edit", sheetType + "Element has been Edited successfully.");

                return RedirectToAction(nameof(Index), new { sheetType = sheetType });
            }
            if (sheetType == (int)SheetTypeEnum.ActivityConst)
            {
                ViewBag.Type = "Const";
                activityVM.DFIndicatorId = 5;
                activityVM.DFUnitId = 2;
                ViewBag.DFYearBaseId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn", activityVM.DFYearBaseId);
                ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(activityVM.DFIndicatorId).NameEn;
                ViewBag.IndicatorId = activityVM.DFIndicatorId;
                activityVM.DFSourceId = 1;
                ViewBag.SourceName = _dFSourceRepository.GetByID(activityVM.DFSourceId).NameEn;
                ViewBag.Unit = _dFUnitRepository.GetByID(activityVM.DFUnitId).NameEn;
                ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn", activityVM.DFQuarterId);
                ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn", activityVM.DFYearId);
                ViewBag.DFSectorId = new SelectList(_dFSectorsRepository.GetAll(), "Id", "NameEn", activityVM.DFSectorId);
            }

            if (sheetType == (int)SheetTypeEnum.ActivityCurrent)
            {
                activityVM.DFIndicatorId = 6;
                activityVM.DFUnitId = 2;
                ViewBag.Type = "Current";
                ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(activityVM.DFIndicatorId).NameEn;
                ViewBag.IndicatorId = activityVM.DFIndicatorId;
                activityVM.DFSourceId = 1;
                ViewBag.SourceName = _dFSourceRepository.GetByID(activityVM.DFSourceId).NameEn;
                ViewBag.Unit = _dFUnitRepository.GetByID(activityVM.DFUnitId).NameEn;
                ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn", activityVM.DFQuarterId);
                ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn", activityVM.DFYearId);
                ViewBag.DFSectorId = new SelectList(_dFSectorsRepository.GetAll(), "Id", "NameEn", activityVM.DFSectorId);
            }

            if (sheetType == (int)SheetTypeEnum.SectorGrowthRates)
            {
                activityVM.DFIndicatorId = 11;
                activityVM.DFUnitId = 3;
                ViewBag.Type = "Sector";
                ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(activityVM.DFIndicatorId).NameEn;
                ViewBag.IndicatorId = activityVM.DFIndicatorId;
                activityVM.DFSourceId = 1;
                ViewBag.SourceName = _dFSourceRepository.GetByID(activityVM.DFSourceId).NameEn;
                ViewBag.Unit = _dFUnitRepository.GetByID(activityVM.DFUnitId).NameEn;
                ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn", activityVM.DFQuarterId);
                ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn", activityVM.DFYearId);
                ViewBag.DFSectorId = new SelectList(_dFSectorsRepository.GetAll(), "Id", "NameEn", activityVM.DFSectorId);
            }
            return View(activityVM);
        }
        /// <summary>
        /// edit sector growth rate
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="id"></param>
        /// <param name="fromDelete"></param>
        /// <returns></returns>

        private IActionResult EditSRGDP(ActivityVMForCreateEdit viewModel, out int id, bool fromDelete = false)
        {
            var srgdpVersion = _sectorGrowthRepository.GetBySRGDPId(viewModel.ActivityCurrentId ?? 0);
            var srgdpId = _sectorGrowthRepository.GetVerById(viewModel.Id);
            srgdpVersion = srgdpId == null ? srgdpVersion : srgdpId;
            var rgdpVersionModel = viewModel.MapToSectorGrowthVer();

            rgdpVersionModel.SectorGrowthRateId = viewModel.ActivityCurrentId > 0 ? viewModel.ActivityCurrentId : (int?)null;
            if (!fromDelete)
            {
                rgdpVersionModel.ChangeActionEnum = ChangeActionEIEnum.Update;
                rgdpVersionModel.IsDeleted = false;
            }
            else
                rgdpVersionModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
            if (srgdpVersion == null || rgdpVersionModel.VersionStatusEnum == VersionStatusEIEnum.Approved || rgdpVersionModel.VersionStatusEnum == VersionStatusEIEnum.Ignored)
            {

                rgdpVersionModel.VersionStatusEnum = VersionStatusEIEnum.Draft;
                rgdpVersionModel.Id = 0;
                var user = _userManager.GetUserAsync(HttpContext.User);
                rgdpVersionModel.CreatedById = user.Result.Id;

                _sectorGrowthRepository.AddVer(rgdpVersionModel);
                _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                id = rgdpVersionModel.Id;
                return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.SectorGrowthRates });
            }

            rgdpVersionModel.Id = srgdpVersion != null ? srgdpVersion.Id : viewModel.Id;

            _sectorGrowthRepository.UpdateVer(rgdpVersionModel);
            id = rgdpVersionModel.Id;
            _toastNotification.AddSuccessToastMessage("Element has been Edited successfully.");


            return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.SectorGrowthRates });
        }
        /// <summary>
        /// sector Activity current
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="id"></param>
        /// <param name="fromDelete"></param>
        /// <returns></returns>

        private IActionResult EditActivityVer(ActivityVMForCreateEdit viewModel, out int id, bool fromDelete = false)
        {
            var activityCurrentVersion = _activityCurrenttRepository.GetByActivityCurrentId(viewModel.ActivityCurrentId ?? 0);
            var activityCurrentById = _activityCurrenttRepository.GetVerById(viewModel.Id);
            activityCurrentVersion = activityCurrentById == null ? activityCurrentVersion : activityCurrentById;
            var rgdpVersionModel = viewModel.MapToActivityCurrentVer();

            rgdpVersionModel.ActivityCurrentId = viewModel.ActivityCurrentId > 0 ? viewModel.ActivityCurrentId : (int?)null;
            if (!fromDelete)
            {
                rgdpVersionModel.ChangeActionEnum = ChangeActionEIEnum.Update;
                rgdpVersionModel.IsDeleted = false;
            }
            else
                rgdpVersionModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
            if (activityCurrentVersion == null || rgdpVersionModel.VersionStatusEnum == VersionStatusEIEnum.Approved || rgdpVersionModel.VersionStatusEnum == VersionStatusEIEnum.Ignored)
            {

                rgdpVersionModel.VersionStatusEnum = VersionStatusEIEnum.Draft;
                rgdpVersionModel.Id = 0;
                var user = _userManager.GetUserAsync(HttpContext.User);
                rgdpVersionModel.CreatedById = user.Result.Id;

                _activityCurrenttRepository.AddVer(rgdpVersionModel);
                _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                id = rgdpVersionModel.Id;
                return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.ActivityCurrent });
            }

            rgdpVersionModel.Id = activityCurrentVersion != null ? activityCurrentVersion.Id : viewModel.Id;

            _activityCurrenttRepository.UpdateVer(rgdpVersionModel);
            id = rgdpVersionModel.Id;
            _toastNotification.AddSuccessToastMessage("Element has been Edited successfully.");


            return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.ActivityCurrent });
        }

        /// <summary>
        /// Delete Activity For Specefic Sheet Type
        /// </summary>
        /// <param name="id">activity id</param>
        /// <param name="sheetType"></param>
        /// <returns>true if deleted successfully false otherwise</returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.EconomicIndicator, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public async Task<bool> Delete(int id, int sheetType, bool isVersion)
        {

            try
            {

                if (sheetType == (int)SheetTypeEnum.ActivityConst)
                {

                    //isDeleted = _activityConstantRepository.Delete(id);
                }
                if (sheetType == (int)SheetTypeEnum.ActivityCurrent)
                {
                    int newid = 0;
                    var verModel = _activityCurrenttRepository.GetVerById(id);
                    if (verModel == null || !isVersion)
                    {
                        var model = _activityCurrenttRepository.GetById(id);
                        var viewModel = model.MapToActivityCurrentVM();
                        viewModel.IsDeleted = true;
                        viewModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
                        EditActivityVer(viewModel, out newid, true);
                    }
                    else
                    {
                        verModel.IsDeleted = true;
                        verModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
                        EditActivityVer(verModel.MapToActivityCurrentVMVer(), out newid, true);
                    }
                    //  await SubmitActivity(newid, false);

                    //ViewBag.Component = "";
                    //ViewBag.ComponentEnum = (int)SheetTypeEnum.ActivityCurrent;
                }
                if (sheetType == (int)SheetTypeEnum.SectorGrowthRates)
                {
                    int newid = 0;
                    var verModel = _sectorGrowthRepository.GetVerById(id);
                    if (verModel == null || !isVersion)
                    {
                        var model = _sectorGrowthRepository.GetById(id);
                        var viewModel = model.MapToSectorGrowthVM();
                        viewModel.IsDeleted = true;
                        viewModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
                        EditSRGDP(viewModel, out newid, true);
                    }
                    else
                    {
                        verModel.IsDeleted = true;
                        verModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
                        EditSRGDP(verModel.MapToSectorGrowthVMVer(), out newid, true);
                    }
                    //  await SubmitSector(newid, false);
                }

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Definitions > Economic Indicators > Activity > " + sheetType + " > Add", sheetType + "Element has been deleted successfully.");
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get List of Activity Constant
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult GetAllActivityConstants()
        {
            //for server-side pagination
            int start = Convert.ToInt32(Request.Form["start"]);
            int lenght = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            int totalCount = 0;
            var activityConstantsData = _activityConstantRepository.GetAll(searchValue, sortColumnName, sortDirection, start, lenght, out totalCount);
            int totalRawsAfterFiltering = activityConstantsData.Count();

            return Json(new
            {
                data = activityConstantsData,
                draw = Request.Form["draw"],
                recordsTotal = totalCount,
                recordsFiltered = totalCount
            });
        }

        /// <summary>
        /// Get List of Activity Current
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> GetAllActivityCurrents(int approvalId)
        {
            //for server-side pagination
            int start = Convert.ToInt32(Request.Form["start"]);
            int lenght = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            int totalCount = 0;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = _userManager.GetRolesAsync(user);
            var activityCurrentsData = _activityCurrenttRepository.GetAll(searchValue, sortColumnName, sortDirection, start, lenght, out totalCount, approvalId > 0 ? UserRolesConst.ApprovalUser : "");
            int totalRawsAfterFiltering = activityCurrentsData.Count();

            return Json(new
            {
                data = activityCurrentsData,
                draw = Request.Form["draw"],
                recordsTotal = totalCount,
                recordsFiltered = totalCount
            });
        }

        /// <summary>
        /// Get List of Activity Sector Growth
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> GetAllSectorGrowth(int approvalId)
        {
            int start = Convert.ToInt32(Request.Form["start"]);
            int lenght = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            int totalCount = 0;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = _userManager.GetRolesAsync(user);
            var sectorGrowthData = _sectorGrowthRepository.GetAll(searchValue, sortColumnName, sortDirection, start, lenght, out totalCount, approvalId > 0 ? UserRolesConst.ApprovalUser : "");
            int totalRawsAfterFiltering = sectorGrowthData.Count();



            return Json(new
            {
                data = sectorGrowthData,
                draw = Request.Form["draw"],
                recordsTotal = totalCount,
                recordsFiltered = totalCount
            });
        }
        /// <summary>
        /// submit changes for activity current
        /// </summary>
        /// <param name="rgdpVersionId"></param>
        /// <returns></returns>

        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ActivityCurrent, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<bool> SubmitChangesActivity(int rgdpVersionId)
        {
            return await SubmitActivity(rgdpVersionId);
        }
        /// <summary>
        /// submit changes for sector growth rate
        /// </summary>
        /// <param name="rgdpVersionId"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.SectorGrowthRates, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<bool> SubmitChangesSector(int rgdpVersionId)
        {
            return await SubmitSector(rgdpVersionId);
        }

        private async Task<bool> SubmitSector(int rgdpVersionId, bool notFromDelete = true)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var activityVer = _sectorGrowthRepository.GetVerById(rgdpVersionId, false);
            bool newVerFlag = false;
            if (activityVer != null)
            {
                if (newVerFlag = (activityVer.SectorGrowthRateId != null || notFromDelete))
                {
                    activityVer.VersionStatusEnum = VersionStatusEIEnum.Submitted;
                    _sectorGrowthRepository.UpdateVer(activityVer);
                }
            }
            if (newVerFlag)
            {
                var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.SectorGrowthRate);

                if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    ApprovalNotification approval = new ApprovalNotification()
                    {
                        ChangeAction = ChangeActionEnum.Update,
                        VersionStatusEnum = VersionStatusEnum.Submitted,
                        ChangesDateTime = DateTime.Now,
                        ChangeType = ChangeType.PageContent,
                        PageLink = $"/{nameof(ActivityController)[0..^10]}?sheetType=5",
                        PageName = PagesNamesConst.SectorGrowthRate,
                        PageType = PageType.Static,
                        ContentManagerId = user.Id,
                    };
                    _approvalNotificationsRepository.Add(approval);

                }
            }


            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            return true;
        }

        private async Task<bool> SubmitActivity(int rgdpVersionId, bool notFromDelete = true)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var activityVer = _activityCurrenttRepository.GetVerById(rgdpVersionId, false);
            bool newVerFlag = false;
            if (activityVer != null)
            {
                if (newVerFlag = (activityVer.ActivityCurrentId != null || notFromDelete))
                {
                    activityVer.VersionStatusEnum = VersionStatusEIEnum.Submitted;
                    _activityCurrenttRepository.UpdateVer(activityVer);
                }
            }
            if (newVerFlag)
            {
                var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.ActivityCurrent);

                if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    ApprovalNotification approval = new ApprovalNotification()
                    {
                        ChangeAction = ChangeActionEnum.Update,
                        VersionStatusEnum = VersionStatusEnum.Submitted,
                        ChangesDateTime = DateTime.Now,
                        ChangeType = ChangeType.PageContent,
                        PageLink = $"/{nameof(ActivityController)[0..^10]}?sheetType=3",
                        PageName = PagesNamesConst.ActivityCurrent,
                        PageType = PageType.Static,
                        ContentManagerId = user.Id,
                    };
                    _approvalNotificationsRepository.Add(approval);

                }
            }


            _toastNotification.AddSuccessToastMessage(ToasrMessages.SubmitSuccess);
            return true;
        }
        /// <summary>
        /// approve changes for activity current
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="componentVersionId"></param>
        /// <returns></returns>

        [BEUsersPrivilegesRequirement(PrivilegesPageType.ActivityCurrent, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> ApproveActivity([FromQuery]int approvalId, [FromQuery] int componentVersionId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var activityVersion = _activityCurrenttRepository.GetVerById(componentVersionId, false);

            if (activityVersion != null)
            {
                activityVersion.VersionStatusEnum = VersionStatusEIEnum.Approved;

                var activity = new ActivityCurrent()
                {

                    IsDeleted = activityVersion.IsDeleted,
                    DFIndicatorId = activityVersion.DFIndicatorId,
                    DFQuarterId = activityVersion.DFQuarterId,
                    DFSourceId = activityVersion.DFSourceId,
                    DFUnitId = activityVersion.DFUnitId,
                    AccommodationAndFoodServiceActivities = activityVersion.AccommodationAndFoodServiceActivities,
                    AgricultureForestryFishing = activityVersion.AgricultureForestryFishing,
                    BusinessServices = activityVersion.BusinessServices,
                    DFYearId = activityVersion.DFYearId,
                    Communication = activityVersion.Communication,
                    Construction = activityVersion.Construction,
                    DFSectorId = activityVersion.DFSectorId,
                    WholesaleAndRetailTrade = activityVersion.WholesaleAndRetailTrade,
                    WaterSewerageRemediationActivitie = activityVersion.WaterSewerageRemediationActivitie,
                    TransportationAndStorage = activityVersion.TransportationAndStorage,
                    TotalGDPAtFactorCost = activityVersion.TotalGDPAtFactorCost,
                    SuezcCanal = activityVersion.SuezcCanal,
                    SocialServices = activityVersion.SocialServices,
                    SocialSecurityAndInsurance = activityVersion.SocialSecurityAndInsurance,
                    RealEstateOwnership = activityVersion.RealEstateOwnership,
                    RealEstateActivitie = activityVersion.RealEstateActivitie,
                    petroleumRefining = activityVersion.petroleumRefining,
                    Petroleum = activityVersion.Petroleum,
                    OtherServices = activityVersion.OtherServices,
                    OtherManufacturing = activityVersion.OtherManufacturing,
                    OtherExtraction = activityVersion.OtherExtraction,
                    MiningQuarrying = activityVersion.MiningQuarrying,
                    ManufacturingIndustries = activityVersion.ManufacturingIndustries,
                    Information = activityVersion.Information,
                    Health = activityVersion.Health,
                    GeneralGovernment = activityVersion.GeneralGovernment,
                    Gas = activityVersion.Gas,
                    FinancialIntermediariesAuxiliaryServices = activityVersion.FinancialIntermediariesAuxiliaryServices,
                    Electricity = activityVersion.Electricity,
                    Education = activityVersion.Education,

                };
                if (activityVersion.ActivityCurrentId != null)
                {
                    activity.Id = activityVersion.ActivityCurrentId ?? 0;
                    _activityCurrenttRepository.Update(activity);
                }
                else
                {
                    activity.Id = 0;
                    _activityCurrenttRepository.Add(activity);
                    activityVersion.ActivityCurrentId = activity.Id;
                }

                _activityCurrenttRepository.UpdateVer(activityVersion);
            }

            var allSubmited = _activityCurrenttRepository.GetAllSubmited();
            if (!allSubmited.Any())
            {
                approval.VersionStatusEnum = VersionStatusEnum.Approved;
                approval.ChangesDateTime = DateTime.Now;
                _approvalNotificationsRepository.Update(approval);

                return RedirectToAction("Index", "ApprovalNotifications");
            }

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);

            return RedirectToAction("Index", new { sheetType = 3, approvalId });
        }
        /// <summary>
        /// approve changes for sector growth rate
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="componentVersionId"></param>
        /// <returns></returns>

        [BEUsersPrivilegesRequirement(PrivilegesPageType.SectorGrowthRates, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> ApproveSector([FromQuery]int approvalId, [FromQuery] int componentVersionId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var srgdpVersion = _sectorGrowthRepository.GetVerById(componentVersionId, false);

            if (srgdpVersion != null)
            {
                srgdpVersion.VersionStatusEnum = VersionStatusEIEnum.Approved;

                var srgdp = new SectorGrowthRate()
                {

                    IsDeleted = srgdpVersion.IsDeleted,
                    DFIndicatorId = srgdpVersion.DFIndicatorId,
                    DFQuarterId = srgdpVersion.DFQuarterId,
                    DFSourceId = srgdpVersion.DFSourceId,
                    DFUnitId = srgdpVersion.DFUnitId,
                    AccommodationAndFoodServiceActivities = srgdpVersion.AccommodationAndFoodServiceActivities,
                    AgricultureForestryFishing = srgdpVersion.AgricultureForestryFishing,
                    BusinessServices = srgdpVersion.BusinessServices,
                    DFYearId = srgdpVersion.DFYearId,
                    Communication = srgdpVersion.Communication,
                    Construction = srgdpVersion.Construction,
                    DFSectorId = srgdpVersion.DFSectorId,
                    WholesaleAndRetailTrade = srgdpVersion.WholesaleAndRetailTrade,
                    WaterSewerageRemediationActivitie = srgdpVersion.WaterSewerageRemediationActivitie,
                    TransportationAndStorage = srgdpVersion.TransportationAndStorage,
                    TotalGDPAtFactorCost = srgdpVersion.TotalGDPAtFactorCost,
                    SuezcCanal = srgdpVersion.SuezcCanal,
                    SocialServices = srgdpVersion.SocialServices,
                    SocialSecurityAndInsurance = srgdpVersion.SocialSecurityAndInsurance,
                    RealEstateOwnership = srgdpVersion.RealEstateOwnership,
                    RealEstateActivitie = srgdpVersion.RealEstateActivitie,
                    petroleumRefining = srgdpVersion.petroleumRefining,
                    Petroleum = srgdpVersion.Petroleum,
                    OtherServices = srgdpVersion.OtherServices,
                    OtherManufacturing = srgdpVersion.OtherManufacturing,
                    OtherExtraction = srgdpVersion.OtherExtraction,
                    MiningQuarrying = srgdpVersion.MiningQuarrying,
                    ManufacturingIndustries = srgdpVersion.ManufacturingIndustries,
                    Information = srgdpVersion.Information,
                    Health = srgdpVersion.Health,
                    GeneralGovernment = srgdpVersion.GeneralGovernment,
                    Gas = srgdpVersion.Gas,
                    FinancialIntermediariesAuxiliaryServices = srgdpVersion.FinancialIntermediariesAuxiliaryServices,
                    Electricity = srgdpVersion.Electricity,
                    Education = srgdpVersion.Education,

                };
                if (srgdpVersion.SectorGrowthRateId != null)
                {
                    srgdp.Id = srgdpVersion.SectorGrowthRateId ?? 0;
                    _sectorGrowthRepository.Update(srgdp);
                }
                else
                {
                    srgdp.Id = 0;
                    _sectorGrowthRepository.Add(srgdp);
                    srgdpVersion.SectorGrowthRateId = srgdp.Id;
                }

                _sectorGrowthRepository.UpdateVer(srgdpVersion);
            }

            var allSubmited = _sectorGrowthRepository.GetAllSubmited();
            if (!allSubmited.Any())
            {
                approval.VersionStatusEnum = VersionStatusEnum.Approved;
                approval.ChangesDateTime = DateTime.Now;
                _approvalNotificationsRepository.Update(approval);

                return RedirectToAction("Index", "ApprovalNotifications");
            }

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);

            return RedirectToAction("Index", new { sheetType = 5, approvalId });
        }
        /// <summary>
        /// ignore changes for activity current
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="componentVersionId"></param>
        /// <returns></returns>

        [BEUsersPrivilegesRequirement(PrivilegesPageType.ActivityCurrent, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> IgnoreActivity([FromQuery]int approvalId, [FromQuery] int componentVersionId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var componentVersion = _activityCurrenttRepository.GetVerById(componentVersionId, false);

            if (componentVersion != null)
            {
                componentVersion.VersionStatusEnum = VersionStatusEIEnum.Ignored;
                _activityCurrenttRepository.UpdateVer(componentVersion);
            }

            var allSubmited = _activityCurrenttRepository.GetAllSubmited();
            if (!allSubmited.Any())
            {
                approval.VersionStatusEnum = VersionStatusEnum.Ignored;
                approval.ChangesDateTime = DateTime.Now;
                _approvalNotificationsRepository.Update(approval);

                return RedirectToAction("Index", "ApprovalNotifications");
            }

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);


            return RedirectToAction("Index", new { sheetType = 3, approvalId });
        }
        /// <summary>
        /// ignore changes for sector growth rate
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="componentVersionId"></param>
        /// <returns></returns>

        [BEUsersPrivilegesRequirement(PrivilegesPageType.SectorGrowthRates, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> IgnoreSector([FromQuery]int approvalId, [FromQuery] int componentVersionId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var componentVersion = _sectorGrowthRepository.GetVerById(componentVersionId, false);

            if (componentVersion != null)
            {
                componentVersion.VersionStatusEnum = VersionStatusEIEnum.Ignored;
                _sectorGrowthRepository.UpdateVer(componentVersion);
            }

            var allSubmited = _sectorGrowthRepository.GetAllSubmited();
            if (!allSubmited.Any())
            {
                approval.VersionStatusEnum = VersionStatusEnum.Ignored;
                approval.ChangesDateTime = DateTime.Now;
                _approvalNotificationsRepository.Update(approval);

                return RedirectToAction("Index", "ApprovalNotifications");
            }

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);


            return RedirectToAction("Index", new { sheetType = 5, approvalId });
        }
    }
}