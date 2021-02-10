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
    public class ComponentController : Controller
    {
        private readonly IComponentConstantRepository _componentConstantRepository;
        private readonly IComponentCurrenttRepository _componentCurrenttRepository;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<ComponentController> _eventLogger;
        private readonly IDFIndicatorRepository _dFIndicatorRepository;
        private readonly IDFYearsRepository _dFYearsRepository;
        private readonly IDFQuartersRepository _dFQuartersRepository;
        private readonly IDFSourceRepository _dFSourceRepository;
        private readonly IDFUnitRepository _dFUnitRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;

        public ComponentController(IComponentConstantRepository componentConstantRepository, IComponentCurrenttRepository componentCurrenttRepository, IToastNotification toastNotification, IEventLogger<ComponentController> eventLogger, IDFIndicatorRepository dFIndicatorRepository, IDFYearsRepository dFYearsRepository, IDFQuartersRepository dFQuartersRepository, IDFSourceRepository dFSourceRepository, IDFUnitRepository dFUnitRepository, UserManager<ApplicationUser> userManager, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _componentConstantRepository = componentConstantRepository;
            _componentCurrenttRepository = componentCurrenttRepository;
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
        /// Get component View Based On Sheet Type
        /// </summary>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.EconomicIndicator, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery]int sheetType, [FromQuery]int approvalId)
        {
            if (sheetType == (int)SheetTypeEnum.ComponentConst)
            {
                ViewBag.Component = "Constant";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.ComponentConst;
            }
            else if (sheetType == (int)SheetTypeEnum.ComponentCurrent)
            {
                ViewBag.Component = "Current";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.ComponentCurrent;
            }
            ViewBag.approvalId = approvalId;
            return View();
        }

        /// <summary>
        /// Get List of Component Constant
        /// </summary>
        /// <param name="sheetType">sheet type of component current</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<JsonResult> GetAll(int sheetType, int approvalId)
        {
            int start = Convert.ToInt32(Request.Form["start"]);
            int lenght = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            int totalCount = 0;


            IEnumerable<ComponentViewModel> componentData = null;

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = _userManager.GetRolesAsync(user);

            if (sheetType == (int)SheetTypeEnum.ComponentConst)
            {
                componentData = _componentConstantRepository.GetAll(searchValue, sortColumnName, sortDirection, start, lenght, out totalCount, approvalId > 0 ? UserRolesConst.ApprovalUser : "");

            }
            else if (sheetType == (int)SheetTypeEnum.ComponentCurrent)
                componentData = _componentCurrenttRepository.GetAll(searchValue, sortColumnName, sortDirection, start, lenght, out totalCount, approvalId > 0 ? UserRolesConst.ApprovalUser : "");

            return Json(new
            {
                data = componentData,
                draw = Request.Form["draw"],
                recordsTotal = totalCount,
                recordsFiltered = totalCount
            });


        }

        /// <summary>
        /// Get Create New constant View For Specefic Sheet Type
        /// </summary>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.EconomicIndicator, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create([FromQuery]int sheetType)
        {
            CreateCommon(sheetType);
            return View();
        }

        /// <summary>
        /// Create New constant For Specefic Sheet Type
        /// </summary>
        /// <param name="viewModel">constant View Model</param>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ComponentFormViewModel viewModel, [FromQuery]int sheetType)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                viewModel.CreatedById = user.Id;

                if (sheetType == (int)SheetTypeEnum.ComponentConst)
                {
                    viewModel.DFIndicatorId = (int)DFIndicatorEnum.GDPByExpenditureAtMarketPricesConstantprices;
                    _componentConstantRepository.AddVer(viewModel.MapToComponentConstantVerModel());
                    _toastNotification.AddSuccessToastMessage("Element has been Created successfully.");
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Definitions > Economic Indicators > Component > Constant", "Constant ID " + viewModel.DFIndicatorId);
                    return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.ComponentConst });
                }
                if (sheetType == (int)SheetTypeEnum.ComponentCurrent)
                {
                    viewModel.DFIndicatorId = (int)DFIndicatorEnum.GDPByExpenditureAtMarketPricesCurrentPrices;
                    _componentCurrenttRepository.AddVer(viewModel.MapToComponentCurrentVerModel());
                    _toastNotification.AddSuccessToastMessage("Element has been Created successfully.");
                    _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Definitions > Economic Indicators > Component > Current", "Current  ID " + viewModel.DFIndicatorId);
                    return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.ComponentCurrent });
                }

            }
            CreateCommon(sheetType);
            return View(viewModel);
        }

        /// <summary>
        /// Core Create based on sheet type
        /// </summary>
        /// <param name="sheetType">sheet type of component const</param>
        /// <returns></returns>
        private void CreateCommon(int sheetType)
        {
            if (sheetType == (int)SheetTypeEnum.ComponentConst)
            {
                FormCreateCommon((int)DFIndicatorEnum.GDPByExpenditureAtMarketPricesConstantprices);
                ViewBag.Component = "Constant";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.ComponentConst;
            }
            else if (sheetType == (int)SheetTypeEnum.ComponentCurrent)
            {
                FormCreateCommon((int)DFIndicatorEnum.GDPByExpenditureAtMarketPricesConstantprices);
                ViewBag.Component = "Current";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.ComponentCurrent;
            }
        }

        /// <summary>
        /// Core form Create based on sheet type
        /// </summary>
        /// <param name="indicator">indicator id</param>
        /// <returns></returns>
        private void FormCreateCommon(int indicator)
        {
            ViewBag.Indicator = _dFIndicatorRepository.GetByID(indicator).NameEn;
            ViewBag.Source = _dFSourceRepository.GetByID((int)DFSourceEnum.MinistryOfPlanning).NameEn;
            ViewBag.Unit = _dFUnitRepository.GetByID((int)DFUnitEnum.BillionEGP).NameEn;
            var allYears = _dFYearsRepository.GetAll();
            ViewBag.DFYearBaseId = new SelectList(allYears, "Id", "NameEn");
            ViewBag.DFYearFiscalId = new SelectList(allYears, "Id", "NameEn");
            ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn");
        }

        /// <summary>
        /// Core form edit based on sheet type
        /// </summary>
        /// <param name="indicator">indicator id</param>
        /// <param name="componentFormViewModel">component edited model</param>
        /// <returns></returns>
        private void FormEditCommon(int indicator, ComponentFormViewModel componentFormViewModel)
        {
            ViewBag.Indicator = _dFIndicatorRepository.GetByID(indicator).NameEn;
            ViewBag.Source = _dFSourceRepository.GetByID((int)DFSourceEnum.MinistryOfPlanning).NameEn;
            ViewBag.Unit = _dFUnitRepository.GetByID((int)DFUnitEnum.BillionEGP).NameEn;
            var allYears = _dFYearsRepository.GetAll();
            ViewBag.DFYearBaseId = new SelectList(allYears, "Id", "NameEn", componentFormViewModel.DFYearBaseId);
            ViewBag.DFYearFiscalId = new SelectList(allYears, "Id", "NameEn", componentFormViewModel.DFYearFiscalId);
            ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn", componentFormViewModel.DFQuarterId);
        }

        /// <summary>
        /// Get Edit constant View For Specefic constant and Sheet Type
        /// </summary>
        /// <param name="id">activity id</param>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.EconomicIndicator, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit([FromQuery]int sheetType, [FromQuery]bool isVersion, int id)
        {
            ComponentFormViewModel componentViewModel = null;
            if (sheetType == (int)SheetTypeEnum.ComponentConst)
            {

                var componentConstVerModel = _componentConstantRepository.GetVerById(id);
                if (componentConstVerModel != null && isVersion)
                    componentViewModel = componentConstVerModel.MapToComponentConstantViewModel();
                else
                {
                    var componentConstModel = _componentConstantRepository.GetById(id);
                    componentViewModel = componentConstModel.MapToComponentConstantViewModel();
                }


                ViewBag.Indicator = _dFIndicatorRepository.GetByID((int)DFIndicatorEnum.GDPByExpenditureAtMarketPricesConstantprices).NameEn;
                FormEditCommon((int)DFIndicatorEnum.GDPByExpenditureAtMarketPricesConstantprices, componentViewModel);

                ViewBag.Component = "Constant";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.ComponentConst;
            }
            else if (sheetType == (int)SheetTypeEnum.ComponentCurrent)
            {
                var componentCurrentVerModel = _componentCurrenttRepository.GetVerById(id);
                if (componentCurrentVerModel != null && isVersion)
                    componentViewModel = componentCurrentVerModel.MapToComponentCurrentViewModel();
                else
                {
                    var componentCurrenttModel = _componentCurrenttRepository.GetById(id);
                    componentViewModel = componentCurrenttModel.MapToComponentCurrentViewModel();
                }

                FormEditCommon((int)DFIndicatorEnum.GDPByExpenditureAtMarketPricesCurrentPrices, componentViewModel);

                ViewBag.Component = "Current";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.ComponentCurrent;
            }

            return View(componentViewModel);

        }

        /// <summary>
        /// Edit constant For Specefic Sheet Type
        /// </summary>
        /// <param name="viewModel">constant View Model</param>
        /// <param name="sheetType"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult Edit(ComponentFormViewModel viewModel, [FromQuery]int sheetType)
        {
            if (ModelState.IsValid)
            {
                if (sheetType == (int)SheetTypeEnum.ComponentConst)
                {
                    return EditConst(viewModel, out int id);
                }
                else if (sheetType == (int)SheetTypeEnum.ComponentCurrent)
                {
                    return EditCurrent(viewModel, out int id);
                }
            }

            if (sheetType == (int)SheetTypeEnum.ComponentConst)
            {
                FormEditCommon((int)DFIndicatorEnum.GDPByExpenditureAtMarketPricesConstantprices, viewModel);

                ViewBag.Component = "Constant";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.ComponentConst;
            }
            else if (sheetType == (int)SheetTypeEnum.ComponentCurrent)
            {
                FormEditCommon((int)DFIndicatorEnum.GDPByExpenditureAtMarketPricesCurrentPrices, viewModel);

                ViewBag.Component = "Current";
                ViewBag.ComponentEnum = (int)SheetTypeEnum.ComponentCurrent;
            }

            return View(viewModel);
        }

        private IActionResult EditConst(ComponentFormViewModel viewModel, out int id, bool fromDelete = false)
        {
            var componentConstVersion = _componentConstantRepository.GetByComponentConstId(viewModel.ComponentId ?? 0);
            var componentConstById = _componentConstantRepository.GetVerById(viewModel.Id);
            componentConstVersion = componentConstById == null ? componentConstVersion : componentConstById;
            var componentConstVersionModel = viewModel.MapToComponentConstantVerModel();


            componentConstVersionModel.ComponentConstantId = viewModel.ComponentId > 0 ? viewModel.ComponentId : (int?)null;
            if (!fromDelete)
            {

                componentConstVersionModel.ChangeActionEnum = ChangeActionEIEnum.Update;
                componentConstVersionModel.IsDeleted = false;
            }
            else
                componentConstVersionModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
            if (componentConstVersion == null || componentConstVersionModel.VersionStatusEnum == VersionStatusEIEnum.Approved || componentConstVersionModel.VersionStatusEnum == VersionStatusEIEnum.Ignored)
            {
                var user = _userManager.GetUserAsync(HttpContext.User);
                componentConstVersionModel.VersionStatusEnum = VersionStatusEIEnum.Draft;
                componentConstVersionModel.CreatedById = user.Result.Id;
                componentConstVersionModel.Id = 0;

                _componentConstantRepository.AddVer(componentConstVersionModel);
                _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                id = componentConstVersionModel.Id;
                return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.ComponentConst });
            }

            componentConstVersionModel.Id = componentConstVersion != null ? componentConstVersion.Id : viewModel.Id;

            _componentConstantRepository.UpdateVer(componentConstVersionModel);
            id = componentConstVersionModel.Id;
            _toastNotification.AddSuccessToastMessage("Element has been Edited successfully.");
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Definitions > Economic Indicators > Component > Const", "Const  ID " + viewModel.Id);
            return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.ComponentConst });
        }

        private IActionResult EditCurrent(ComponentFormViewModel viewModel, out int id, bool fromDelete = false)
        {
            var componentCurrentVersion = _componentCurrenttRepository.GetByComponentCurrentId(viewModel.ComponentId ?? 0);
            var componentCurrentById = _componentCurrenttRepository.GetVerById(viewModel.Id);
            componentCurrentVersion = componentCurrentById == null ? componentCurrentVersion : componentCurrentById;
            var componentCurrentVersionModel = viewModel.MapToComponentCurrentVerModel();

            componentCurrentVersionModel.ComponentCurrentId = viewModel.ComponentId > 0 ? viewModel.ComponentId : (int?)null;
            if (!fromDelete)
            {
                componentCurrentVersionModel.ChangeActionEnum = ChangeActionEIEnum.Update;
                componentCurrentVersionModel.IsDeleted = false;
            }
            else
                componentCurrentVersionModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
            if (componentCurrentVersion == null || componentCurrentVersionModel.VersionStatusEnum == VersionStatusEIEnum.Approved || componentCurrentVersionModel.VersionStatusEnum == VersionStatusEIEnum.Ignored)
            {
                var user = _userManager.GetUserAsync(HttpContext.User);
                componentCurrentVersionModel.VersionStatusEnum = VersionStatusEIEnum.Draft;
                componentCurrentVersionModel.Id = 0;
                componentCurrentVersionModel.CreatedById = user.Result.Id;


                _componentCurrenttRepository.AddVer(componentCurrentVersionModel);
                _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                id = componentCurrentVersionModel.Id;
                return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.ComponentCurrent });
            }

            componentCurrentVersionModel.Id = componentCurrentVersion != null ? componentCurrentVersion.Id : viewModel.Id;

            _componentCurrenttRepository.UpdateVer(componentCurrentVersionModel);
            id = componentCurrentVersionModel.Id;
            _toastNotification.AddSuccessToastMessage("Element has been Edited successfully.");
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Definitions > Economic Indicators > Component > current", "current  ID " + viewModel.Id);
            return RedirectToAction(nameof(Index), new { sheetType = (int)SheetTypeEnum.ComponentCurrent });
        }

        /// <summary>
        /// Delete Constant For Specefic Sheet Type
        /// </summary>
        /// <param name="id">activity id</param>
        /// <param name="sheetType"></param>
        /// <returns>true if deleted successfully false otherwise</returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.EconomicIndicator, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public async Task<bool> Delete(int id, int sheetType, bool isVersion)
        {

            try
            {
                if (sheetType == (int)SheetTypeEnum.ComponentConst)
                {
                    int newid = 0;
                    var verModel = _componentConstantRepository.GetVerById(id);
                    if (verModel == null || !isVersion)
                    {
                        var model = _componentConstantRepository.GetById(id);
                        var viewModel = model.MapToComponentConstantViewModel();
                        viewModel.IsDeleted = true;
                        viewModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
                        EditConst(viewModel, out newid, true);
                    }
                    else
                    {
                        verModel.IsDeleted = true;
                        verModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
                        EditConst(verModel.MapToComponentConstantViewModel(), out newid, true);
                    }
                    //   await SubmitConst(newid, false);
                    ViewBag.Component = "Constant";
                    ViewBag.ComponentEnum = (int)SheetTypeEnum.ComponentConst;
                }
                else if (sheetType == (int)SheetTypeEnum.ComponentCurrent)
                {
                    int newid = 0;
                    var verModel = _componentCurrenttRepository.GetVerById(id);
                    if (verModel == null || !isVersion)
                    {
                        var model = _componentCurrenttRepository.GetById(id);
                        var viewModel = model.MapToComponentCurrentViewModel();
                        viewModel.IsDeleted = true;
                        viewModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
                        EditCurrent(viewModel, out newid, true);
                    }
                    else
                    {
                        verModel.IsDeleted = true;
                        verModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
                        EditCurrent(verModel.MapToComponentCurrentViewModel(), out newid, true);
                    }
                    //  await SubmitCurrent(newid, false);
                    ViewBag.Component = "Current";
                    ViewBag.ComponentEnum = (int)SheetTypeEnum.ComponentCurrent;
                }
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Definitions > Economic Indicators > Component > " + ViewBag.Component, ViewBag.Component + "  ID " + id);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// submit changes for component const 
        /// </summary>
        /// <param name="componentVersionId"></param>
        /// <param name="notFromDelete"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ComponentConstant, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<bool> SubmitChangesConst(int componentVersionId)
        {
            return await SubmitConst(componentVersionId);
        }

        private async Task<bool> SubmitConst(int componentVersionId, bool notFromDelete = true)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var componentVer = _componentConstantRepository.GetVerById(componentVersionId, false);
            bool newVerFlag = false;
            if (componentVer != null)
            {
                if (newVerFlag = (componentVer.ComponentConstantId != null || notFromDelete))
                {
                    componentVer.VersionStatusEnum = VersionStatusEIEnum.Submitted;
                    _componentConstantRepository.UpdateVer(componentVer);
                }
            }

            if (newVerFlag)
            {
                var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.ComponentConst);

                if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    ApprovalNotification approval = new ApprovalNotification()
                    {
                        ChangeAction = ChangeActionEnum.Update,
                        VersionStatusEnum = VersionStatusEnum.Submitted,
                        ChangesDateTime = DateTime.Now,
                        ChangeType = ChangeType.PageContent,
                        PageLink = $"/{nameof(ComponentController)[0..^10]}?sheetType=1",
                        PageName = PagesNamesConst.ComponentConst,
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
        /// submit changes for component current 
        /// </summary>
        /// <param name="componentVersionId"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ComponentCurrent, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit, PrivilegesActions.CanDelete })]
        public async Task<bool> SubmitChangesCurrent(int componentVersionId)
        {
            return await SubmitCurrent(componentVersionId);
        }

        private async Task<bool> SubmitCurrent(int componentVersionId, bool notFromDelete = true)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var componentVer = _componentCurrenttRepository.GetVerById(componentVersionId, false);
            bool newVerFlag = false;
            if (componentVer != null)
            {
                if (newVerFlag = (componentVer.ComponentCurrentId != null || notFromDelete))
                {
                    componentVer.VersionStatusEnum = VersionStatusEIEnum.Submitted;
                    _componentCurrenttRepository.UpdateVer(componentVer);
                }
            }
            if (newVerFlag)
            {
                var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.ComponentCurrent);

                if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    ApprovalNotification approval = new ApprovalNotification()
                    {
                        ChangeAction = ChangeActionEnum.Update,
                        VersionStatusEnum = VersionStatusEnum.Submitted,
                        ChangesDateTime = DateTime.Now,
                        ChangeType = ChangeType.PageContent,
                        PageLink = $"/{nameof(ComponentController)[0..^10]}?sheetType=2",
                        PageName = PagesNamesConst.ComponentCurrent,
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
        /// approve changes for component const 
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="componentVersionId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ComponentConstant, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> ApproveConst([FromQuery]int approvalId, [FromQuery] int componentVersionId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var componentVersion = _componentConstantRepository.GetVerById(componentVersionId, false);

            if (componentVersion != null)
            {
                componentVersion.VersionStatusEnum = VersionStatusEIEnum.Approved;

                var component = new ComponentConstant()
                {

                    IsDeleted = componentVersion.IsDeleted,
                    DFIndicatorId = componentVersion.DFIndicatorId,
                    DFQuarterId = componentVersion.DFQuarterId,
                    DFSourceId = componentVersion.DFSourceId,
                    DFUnitId = componentVersion.DFUnitId,
                    DFYearFiscalId = componentVersion.DFYearFiscalId,
                    ExportsOfGoodsAndServices = componentVersion.ExportsOfGoodsAndServices,
                    GovernmentConsumption = componentVersion.GovernmentConsumption,
                    TotalGrossDomesticProductAtMarketPrices = componentVersion.TotalGrossDomesticProductAtMarketPrices,
                    PrivateConsumption = componentVersion.PrivateConsumption,
                    ImportsOfGoodsAndServices = componentVersion.ImportsOfGoodsAndServices,
                    GrossCapitalFormation = componentVersion.GrossCapitalFormation,

                };
                if (componentVersion.ComponentConstantId != null)
                {
                    component.Id = componentVersion.ComponentConstantId ?? 0;
                    _componentConstantRepository.Update(component);
                }
                else
                {
                    component.Id = 0;
                    _componentConstantRepository.Add(component);
                    componentVersion.ComponentConstantId = component.Id;
                }

                _componentConstantRepository.UpdateVer(componentVersion);
            }

            var allSubmited = _componentConstantRepository.GetAllSubmited();
            if (!allSubmited.Any())
            {
                approval.VersionStatusEnum = VersionStatusEnum.Approved;
                approval.ChangesDateTime = DateTime.Now;
                _approvalNotificationsRepository.Update(approval);

                return RedirectToAction("Index", "ApprovalNotifications");

            }

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);

            return RedirectToAction("Index", new { sheetType = 1, approvalId });
        }
        /// <summary>
        /// approve changes for component current 
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="componentVersionId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ComponentCurrent, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> ApproveCurrent([FromQuery]int approvalId, [FromQuery] int componentVersionId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var componentVersion = _componentCurrenttRepository.GetVerById(componentVersionId, false);

            if (componentVersion != null)
            {
                componentVersion.VersionStatusEnum = VersionStatusEIEnum.Approved;

                var component = new ComponentCurrent()
                {

                    IsDeleted = componentVersion.IsDeleted,
                    DFIndicatorId = componentVersion.DFIndicatorId,
                    DFQuarterId = componentVersion.DFQuarterId,
                    DFSourceId = componentVersion.DFSourceId,
                    DFUnitId = componentVersion.DFUnitId,
                    DFYearFiscalId = componentVersion.DFYearFiscalId,
                    ExportsOfGoodsAndServices = componentVersion.ExportsOfGoodsAndServices,
                    GovernmentConsumption = componentVersion.GovernmentConsumption,
                    TotalGrossDomesticProductAtMarketPrices = componentVersion.TotalGrossDomesticProductAtMarketPrices,
                    PrivateConsumption = componentVersion.PrivateConsumption,
                    ImportsOfGoodsAndServices = componentVersion.ImportsOfGoodsAndServices,
                    GrossCapitalFormation = componentVersion.GrossCapitalFormation,

                };
                if (componentVersion.ComponentCurrentId != null)
                {
                    component.Id = componentVersion.ComponentCurrentId ?? 0;
                    _componentCurrenttRepository.Update(component);
                }
                else
                {
                    component.Id = 0;
                    _componentCurrenttRepository.Add(component);
                    componentVersion.ComponentCurrentId = component.Id;
                }

                _componentCurrenttRepository.UpdateVer(componentVersion);
            }

            var allSubmited = _componentCurrenttRepository.GetAllSubmited();
            if (!allSubmited.Any())
            {
                approval.VersionStatusEnum = VersionStatusEnum.Approved;
                approval.ChangesDateTime = DateTime.Now;
                _approvalNotificationsRepository.Update(approval);

                return RedirectToAction("Index", "ApprovalNotifications");
            }

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);

            return RedirectToAction("Index", new { sheetType = 2, approvalId });
        }


        /// <summary>
        /// Ignore changes for component const 
        /// </summary>
        /// <param name="id">page route id</param>
        /// <param name="approvalId">notification approved id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ComponentConstant, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> IgnoreConst([FromQuery]int approvalId, [FromQuery] int componentVersionId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var componentVersion = _componentConstantRepository.GetVerById(componentVersionId, false);

            if (componentVersion != null)
            {
                componentVersion.VersionStatusEnum = VersionStatusEIEnum.Ignored;
                _componentConstantRepository.UpdateVer(componentVersion);
            }

            var allSubmited = _componentConstantRepository.GetAllSubmited();
            if (!allSubmited.Any())
            {
                approval.VersionStatusEnum = VersionStatusEnum.Ignored;
                approval.ChangesDateTime = DateTime.Now;
                _approvalNotificationsRepository.Update(approval);

                return RedirectToAction("Index", "ApprovalNotifications");
            }

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);


            return RedirectToAction("Index", new { sheetType = 1, approvalId });
        }
        /// <summary>
        /// Ignore changes for component current 
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="componentVersionId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.ComponentCurrent, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> IgnoreCurrent([FromQuery]int approvalId, [FromQuery] int componentVersionId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var componentVersion = _componentCurrenttRepository.GetVerById(componentVersionId, false);

            if (componentVersion != null)
            {
                componentVersion.VersionStatusEnum = VersionStatusEIEnum.Ignored;
                _componentCurrenttRepository.UpdateVer(componentVersion);
            }

            var allSubmited = _componentCurrenttRepository.GetAllSubmited();
            if (!allSubmited.Any())
            {
                approval.VersionStatusEnum = VersionStatusEnum.Ignored;
                approval.ChangesDateTime = DateTime.Now;
                _approvalNotificationsRepository.Update(approval);

                return RedirectToAction("Index", "ApprovalNotifications");
            }

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);


            return RedirectToAction("Index", new { sheetType = 2, approvalId });
        }
    }
}