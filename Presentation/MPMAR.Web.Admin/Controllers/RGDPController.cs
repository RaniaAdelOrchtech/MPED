using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Analytics.Data.Enums;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Interfaces.Analytics;
using MPMAR.Business.Services.Analytics.Mappers;
using MPMAR.Business.Services.Analytics.ViewModels;
using MPMAR.Common;
using MPMAR.Web.Admin.Helpers;
using NToastNotify;
using MPMAR.Business;
using MPMAR.Common.Interfaces;
using MPMAR.Data.Consts;
using Microsoft.AspNetCore.Identity;
using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Analytics.Data;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class RGDPController : Controller
    {
        private readonly IRGDPRepository _rGDPRepository;
        private readonly IRGDP1617Repository _rGDP1617Repository;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<RGDPController> _eventLogger;
        private readonly IDFIndicatorRepository _dFIndicatorRepository;
        private readonly IDFYearsRepository _dFYearsRepository;
        private readonly IDFQuartersRepository _dFQuartersRepository;
        private readonly IDFSourceRepository _dFSourceRepository;
        private readonly IDFUnitRepository _dFUnitRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;

        public RGDPController(IRGDPRepository rGDPRepository, IRGDP1617Repository rGDP1617Repository, IToastNotification toastNotification, IEventLogger<RGDPController> eventLogger, IDFIndicatorRepository dFIndicatorRepository, IDFYearsRepository dFYearsRepository, IDFQuartersRepository dFQuartersRepository, IDFSourceRepository dFSourceRepository, IDFUnitRepository dFUnitRepository, UserManager<ApplicationUser> userManager, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _rGDPRepository = rGDPRepository;
            _rGDP1617Repository = rGDP1617Repository;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _dFIndicatorRepository = dFIndicatorRepository;
            _dFYearsRepository = dFYearsRepository;
            _dFQuartersRepository = dFQuartersRepository;
            _dFSourceRepository = dFSourceRepository;
            _dFUnitRepository = dFUnitRepository;
            _userManager = userManager;
            _approvalNotificationsRepository = approvalNotificationsRepository;
        }
        /// <summary>
        /// get RGDP page inedx
        /// </summary>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.RGDP, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery]int sheetType, [FromQuery]int approvalId)
        {
            if (sheetType == (int)SheetTypeEnum.RGDP)
            {
                ViewBag.Component = "";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.RGDP;
            }
            else if (sheetType == (int)SheetTypeEnum.RGDP1617)
            {
                ViewBag.Component = "16/17";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.RGDP1617;
            }
            ViewBag.approvalId = approvalId;
            return View();
        }
        /// <summary>
        /// get all RGDPC
        /// </summary>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.RGDP, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public async Task<JsonResult> GetAll(int sheetType, int approvalId)
        {
            int start = Convert.ToInt32(Request.Form["start"]);
            int lenght = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            int totalCount = 0;

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = _userManager.GetRolesAsync(user);

            IEnumerable<RGDPViewModel> rgdpData = null;

            if (sheetType == (int)SheetTypeEnum.RGDP)
                rgdpData = _rGDPRepository.GetAll(searchValue, sortColumnName, sortDirection, start, lenght, out totalCount, approvalId > 0 ? UserRolesConst.ApprovalUser : "");
            else if (sheetType == (int)SheetTypeEnum.RGDP1617)
                rgdpData = _rGDP1617Repository.GetAll(searchValue, sortColumnName, sortDirection, start, lenght, out totalCount);

            return Json(new
            {
                data = rgdpData,
                draw = Request.Form["draw"],
                recordsTotal = totalCount,
                recordsFiltered = totalCount
            });


        }
        /// <summary>
        /// create RGDPC page index
        /// </summary>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.RGDP, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create([FromQuery]int sheetType)
        {
            CreateCommon(sheetType);
            return View();
        }
        /// <summary>
        /// create needed common valus
        /// </summary>
        /// <param name="sheetType"></param>
        private void CreateCommon(int sheetType)
        {
            if (sheetType == (int)SheetTypeEnum.RGDP)
            {
                FormCreateCommon((int)DFIndicatorEnum.RealGrowthRateGDPAtMarketPrices, (int)DFUnitEnum.Percentage);
                ViewBag.Component = "";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.RGDP;
            }
            else if (sheetType == (int)SheetTypeEnum.RGDP1617)
            {
                FormCreateCommon((int)DFIndicatorEnum.GDPAtMarketPricesConstantPricesFor1617, (int)DFUnitEnum.BillionEGP);
                ViewBag.Component = "16/17";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.RGDP1617;
            }
        }
        /// <summary>
        /// creat form common
        /// </summary>
        /// <param name="indicator"></param>
        /// <param name="unit"></param>
        private void FormCreateCommon(int indicator, int unit)
        {
            ViewBag.Indicator = _dFIndicatorRepository.GetByID(indicator).NameEn;
            ViewBag.Source = _dFSourceRepository.GetByID((int)DFSourceEnum.MinistryOfPlanning).NameEn;
            ViewBag.Unit = _dFUnitRepository.GetByID(unit).NameEn;
            ViewBag.DFYearFiscalId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn");
            ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn");
        }
        /// <summary>
        /// get RGDP create page
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.RGDP, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public async Task<IActionResult> Create(RGDPFormViewModel viewModel, [FromQuery]int sheetType)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                viewModel.CreatedById = user.Id;
                if (sheetType == (int)SheetTypeEnum.RGDP)
                {
                    viewModel.Indicator = (int)DFIndicatorEnum.RealGrowthRateGDPAtMarketPrices;
                    viewModel.Unit = (int)DFUnitEnum.Percentage;
                    _rGDPRepository.AddVer(viewModel.MapToRGDPVerModel());
                    _toastNotification.AddSuccessToastMessage("Element has been Created successfully.");
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Definitions > Economic Indicators > RGDP > Add", viewModel.Indicator.ToString());
                    return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.RGDP });
                }
                if (sheetType == (int)SheetTypeEnum.RGDP1617)
                {
                    viewModel.Indicator = (int)DFIndicatorEnum.GDPAtMarketPricesConstantPricesFor1617;
                    viewModel.Unit = (int)DFUnitEnum.BillionEGP;
                    _rGDP1617Repository.Add(viewModel.MapToRGDP1617Model());
                    _toastNotification.AddSuccessToastMessage("Element has been Created successfully.");
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Definitions > Economic Indicators > RGDP1617 > Add", viewModel.Indicator.ToString());
                    return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.RGDP1617 });
                }

            }
            CreateCommon(sheetType);
            return View(viewModel);
        }
        /// <summary>
        /// get RGDP edit page
        /// </summary>
        /// <param name="sheetType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.RGDP, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit([FromQuery]int sheetType, int id, [FromQuery]bool isVersion)
        {
            RGDPFormViewModel viewModel = null;
            if (sheetType == (int)SheetTypeEnum.RGDP)
            {
                var componentRGDPVerModel = _rGDPRepository.GetVerById(id);
                if (componentRGDPVerModel != null && isVersion)
                    viewModel = componentRGDPVerModel.MapToRGDPViewModel();
                else
                {
                    var componentRGDPModel = _rGDPRepository.GetById(id);
                    viewModel = componentRGDPModel.MapToRGDPViewModel();
                }

                FormditCommon((int)DFIndicatorEnum.RealGrowthRateGDPAtMarketPrices, (int)DFUnitEnum.Percentage, viewModel);

                ViewBag.Component = "";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.RGDP;
            }
            else if (sheetType == (int)SheetTypeEnum.RGDP1617)
            {
                var model = _rGDP1617Repository.GetById(id);
                viewModel = model.MapToRGDP1617ViewModel();

                FormditCommon((int)DFIndicatorEnum.GDPAtMarketPricesConstantPricesFor1617, (int)DFUnitEnum.BillionEGP, viewModel);

                ViewBag.Component = "16/17";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.RGDP1617;
            }

            return View(viewModel);

        }
        /// <summary>
        /// edit RGDPC
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.RGDP, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(RGDPFormViewModel viewModel, [FromQuery]int sheetType)
        {
            if (ModelState.IsValid)
            {
                if (sheetType == (int)SheetTypeEnum.RGDP)
                {

                    return EditRGDP(viewModel, out int id);

                }
                else if (sheetType == (int)SheetTypeEnum.RGDP1617)
                {
                    _rGDP1617Repository.Update(viewModel.MapToRGDP1617Model());
                    _toastNotification.AddSuccessToastMessage("Element has been Edited successfully.");
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Definitions > Economic Indicators > RGDP > Edit", "RGDP1617 " + viewModel.Indicator);
                    return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.RGDP1617 });
                }
            }

            if (sheetType == (int)SheetTypeEnum.RGDP)
            {
                FormditCommon((int)DFIndicatorEnum.RealGrowthRateGDPAtMarketPrices, (int)DFUnitEnum.Percentage, viewModel);

                ViewBag.Component = "";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.RGDP;
            }
            else if (sheetType == (int)SheetTypeEnum.RGDP1617)
            {
                FormditCommon((int)DFIndicatorEnum.GDPAtMarketPricesConstantPricesFor1617, (int)DFUnitEnum.BillionEGP, viewModel);

                ViewBag.Component = "16/17";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.RGDP1617;
            }

            return View(viewModel);
        }

        private IActionResult EditRGDP(RGDPFormViewModel viewModel, out int id, bool fromDelete = false)
        {
            var rgdpVersion = _rGDPRepository.GetByRGDPId(viewModel.RGDPId ?? 0);
            var rgdpById = _rGDPRepository.GetVerById(viewModel.Id);
            rgdpVersion = rgdpById == null ? rgdpVersion : rgdpById;
            var rgdpVersionModel = viewModel.MapToRGDPVerModel();

            rgdpVersionModel.RGDPGrowthRateId = viewModel.RGDPId > 0 ? viewModel.RGDPId : (int?)null;
            if (!fromDelete)
            {
                rgdpVersionModel.ChangeActionEnum = ChangeActionEIEnum.Update;
                rgdpVersionModel.IsDeleted = false;
            }
            else
                rgdpVersionModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
            if (rgdpVersion == null || rgdpVersionModel.VersionStatusEnum == VersionStatusEIEnum.Approved || rgdpVersionModel.VersionStatusEnum == VersionStatusEIEnum.Ignored)
            {

                rgdpVersionModel.VersionStatusEnum = VersionStatusEIEnum.Draft;
                rgdpVersionModel.Id = 0;
                var user = _userManager.GetUserAsync(HttpContext.User);
                rgdpVersionModel.CreatedById = user.Result.Id;

                _rGDPRepository.AddVer(rgdpVersionModel);
                _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                id = rgdpVersionModel.Id;
                return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.RGDP });
            }

            rgdpVersionModel.Id = rgdpVersion != null ? rgdpVersion.Id : viewModel.Id;

            _rGDPRepository.UpdateVer(rgdpVersionModel);
            id = rgdpVersionModel.Id;
            _toastNotification.AddSuccessToastMessage("Element has been Edited successfully.");

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Definitions > Economic Indicators > RGDP > Edit", viewModel.Indicator.ToString());

            return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.RGDP });
        }

        /// <summary>
        /// edit form common data
        /// </summary>
        /// <param name="indicator"></param>
        /// <param name="unit"></param>
        /// <param name="viewModel"></param>
        /// 
        private void FormditCommon(int indicator, int unit, RGDPFormViewModel viewModel)
        {
            ViewBag.Indicator = _dFIndicatorRepository.GetByID(indicator).NameEn;
            ViewBag.Source = _dFSourceRepository.GetByID((int)DFSourceEnum.MinistryOfPlanning).NameEn;
            ViewBag.Unit = _dFUnitRepository.GetByID(unit).NameEn;
            ViewBag.DFYearFiscalId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn", viewModel.DFYearFiscalId);
            ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn", viewModel.DFQuarterId);
        }
        /// <summary>
        /// delete RGDP
        /// </summary>
        /// <param name="id">RGDP id</param>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.RGDP, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public async Task<bool> Delete(int id, int sheetType, bool isVersion)
        {

            try
            {
                if (sheetType == (int)SheetTypeEnum.RGDP)
                {
                    int newid = 0;
                    var verModel = _rGDPRepository.GetVerById(id);
                    if (verModel == null || !isVersion)
                    {
                        var model = _rGDPRepository.GetById(id);
                        var viewModel = model.MapToRGDPViewModel();
                        viewModel.IsDeleted = true;
                        viewModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
                        EditRGDP(viewModel, out newid, true);
                    }
                    else
                    {
                        verModel.IsDeleted = true;
                        verModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
                        EditRGDP(verModel.MapToRGDPViewModel(), out newid, true);
                    }
                //    await SubmitRGDP(newid, false);

                    ViewBag.Component = "";
                    ViewBag.ComponentEnum = (int)SheetTypeEnum.RGDP;
                }
                else if (sheetType == (int)SheetTypeEnum.RGDP1617)
                {
                    //isDeleted = _rGDP1617Repository.Delete(id);

                    ViewBag.Component = "16/17";
                    ViewBag.ComponentEnum = (int)SheetTypeEnum.RGDP1617;
                }

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Definitions > Economic Indicators > RGDP > Delete", "id: " + id);
                return true;
            }
            catch
            {
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Warning, "Definitions > Economic Indicators > RGDP > Delete", "id: " + id);
                return false;
            }
        }

        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.RGDP, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanDelete, PrivilegesActions.CanEdit })]
        public async Task<bool> SubmitChangesConst(int rgdpVersionId)
        {
            return await SubmitRGDP(rgdpVersionId);
        }
        /// <summary>
        /// submit changes for RGDP report
        /// </summary>
        /// <param name="rgdpVersionId"></param>
        /// <param name="notFromDelete"></param>
        /// <returns></returns>
        private async Task<bool> SubmitRGDP(int rgdpVersionId, bool notFromDelete = true)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var rgdpVer = _rGDPRepository.GetVerById(rgdpVersionId, false);
            bool newVerFlag = false;
            if (rgdpVer != null)
            {
                if (newVerFlag = (rgdpVer.RGDPGrowthRateId != null || notFromDelete))
                {
                    rgdpVer.VersionStatusEnum = VersionStatusEIEnum.Submitted;
                    _rGDPRepository.UpdateVer(rgdpVer);
                }
            }
            if (newVerFlag)
            {
                var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.RGDP);

                if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    ApprovalNotification approval = new ApprovalNotification()
                    {
                        ChangeAction = ChangeActionEnum.Update,
                        VersionStatusEnum = VersionStatusEnum.Submitted,
                        ChangesDateTime = DateTime.Now,
                        ChangeType = ChangeType.PageContent,
                        PageLink = $"/{nameof(RGDPController)[0..^10]}?sheetType=6",
                        PageName = PagesNamesConst.RGDP,
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
        /// approve changes for RGDP report
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="componentVersionId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.RGDP, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve([FromQuery]int approvalId, [FromQuery] int componentVersionId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var rgdpVersion = _rGDPRepository.GetVerById(componentVersionId, false);

            if (rgdpVersion != null)
            {
                rgdpVersion.VersionStatusEnum = VersionStatusEIEnum.Approved;

                var rgdp = new RGDPGrowthRate()
                {

                    IsDeleted = rgdpVersion.IsDeleted,
                    DFIndicatorId = rgdpVersion.DFIndicatorId,
                    DFQuarterId = rgdpVersion.DFQuarterId,
                    DFSourceId = rgdpVersion.DFSourceId,
                    DFUnitId = rgdpVersion.DFUnitId,
                    GrowthRate = rgdpVersion.GrowthRate,
                    DFYearId = rgdpVersion.DFYearId,
                };
                if (rgdpVersion.RGDPGrowthRateId != null)
                {
                    rgdp.Id = rgdpVersion.RGDPGrowthRateId ?? 0;
                    _rGDPRepository.Update(rgdp);
                }
                else
                {
                    rgdp.Id = 0;
                    _rGDPRepository.Add(rgdp);
                    rgdpVersion.RGDPGrowthRateId = rgdp.Id;
                }

                _rGDPRepository.UpdateVer(rgdpVersion);
            }

            var allSubmited = _rGDPRepository.GetAllSubmited();
            if (!allSubmited.Any())
            {
                approval.VersionStatusEnum = VersionStatusEnum.Approved;
                approval.ChangesDateTime = DateTime.Now;
                _approvalNotificationsRepository.Update(approval);

                return RedirectToAction("Index", "ApprovalNotifications");
            }

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);

            return RedirectToAction("Index", new { sheetType = 6, approvalId });
        }
        /// <summary>
        /// Ignore changes for RGDP report
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="componentVersionId"></param>
        /// <returns></returns>

        [BEUsersPrivilegesRequirement(PrivilegesPageType.RGDP, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore([FromQuery]int approvalId, [FromQuery] int componentVersionId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var componentVersion = _rGDPRepository.GetVerById(componentVersionId, false);

            if (componentVersion != null)
            {
                componentVersion.VersionStatusEnum = VersionStatusEIEnum.Ignored;
                _rGDPRepository.UpdateVer(componentVersion);
            }

            var allSubmited = _rGDPRepository.GetAllSubmited();
            if (!allSubmited.Any())
            {
                approval.VersionStatusEnum = VersionStatusEnum.Ignored;
                approval.ChangesDateTime = DateTime.Now;
                _approvalNotificationsRepository.Update(approval);

                return RedirectToAction("Index", "ApprovalNotifications");
            }

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);


            return RedirectToAction("Index", new { sheetType = 6, approvalId });
        }
    }
}