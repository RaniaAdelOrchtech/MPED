using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Analytics.Data;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Services.Analytics.ViewModels;
using MPMAR.Common;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;
using MPMAR.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using MPMAR.Common.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MPMAR.Data;
using MPMAR.Analytics.Data.Models;
using MPMAR.Data.Consts;
using MPMAR.Analytics.Data.Enums;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class GovernorateController : Controller
    {

        private const string notificationSuccess = "Success";
        private const string notificationWarning = "Warning";
        private const string notificationError = "Error";


        private readonly IGovernorateRepository _governorateRepository;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<GovernorateController> _eventLogger;
        private readonly IDFIndicatorRepository _dFIndicatorRepository;
        private readonly IDFYearsRepository _dFYearsRepository;
        private readonly IDFGovernoratesRepository _dFGovernoratesRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;

        public GovernorateController(IGovernorateRepository governorateRepository, IToastNotification toastNotification, IEventLogger<GovernorateController> eventLogger, IDFIndicatorRepository dFIndicatorRepository, IDFYearsRepository dFYearsRepository, IDFGovernoratesRepository dFGovernoratesRepository, UserManager<ApplicationUser> userManager, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _governorateRepository = governorateRepository;
            _dFIndicatorRepository = dFIndicatorRepository;
            _dFYearsRepository = dFYearsRepository;
            _dFGovernoratesRepository = dFGovernoratesRepository;
            _approvalNotificationsRepository = approvalNotificationsRepository;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
        }
        /// <summary>
        /// get GovernorateController page index
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Governorate, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery] int approvalId)
        {
            ViewBag.approvalId = approvalId;
            return View();
        }
        /// <summary>
        /// get all Governorates json
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Governorate, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public async Task<JsonResult> GetAllGovernorates(int approvalId)
        {
            //serverSideParams
            int start = Convert.ToInt32(Request.Form["start"]);
            int lenght = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            int totalCount = 0;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = _userManager.GetRolesAsync(user);
            var governorates = _governorateRepository.GetAll(searchValue, sortColumnName, sortDirection, start, lenght, out totalCount, approvalId > 0 ? UserRolesConst.ApprovalUser : "");
            int totalRawsAfterFiltering = governorates.Count();
            //sorting


            return Json(new
            {
                data = governorates,
                draw = Request.Form["draw"],
                recordsTotal = totalCount,
                recordsFiltered = totalCount
            });
        }
        /// <summary>
        /// get create Governorates page
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Governorate, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create()
        {
            ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(1).NameEn;
            ViewBag.Unit = "One thousand EGP";
            ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn");
            ViewBag.DFRegionId = new SelectList(_dFGovernoratesRepository.GetAllRegion(), "DFRegionId", "NameEn");
            ViewBag.DFGovernorateId = new SelectList(_dFGovernoratesRepository.GetAllGover(), "Id", "NameEn");
            ViewBag.NormalTotal = new List<SelectListItem>()
            {
                new SelectListItem { Value = "normal", Text = "Normal" },
                new SelectListItem { Value = "total", Text = "Total" }
            };

            return View();
        }
        /// <summary>
        /// create Governorates
        /// </summary>
        /// <param name="governorateVM"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Governorate, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public async Task<IActionResult> Create(GovernorateVM governorateVM)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            governorateVM.CreatedById = user.Id;
            if (!ModelState.IsValid)
            {
                ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(1).NameEn;
                ViewBag.Unit = "One thousand EGP";
                ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn");
                ViewBag.NormalTotal = new List<SelectListItem>()
                {
                    new SelectListItem { Value = "normal", Text = "Normal" },
                    new SelectListItem { Value = "total", Text = "Total" }
                };
                ViewBag.DFRegionId = new SelectList(_dFGovernoratesRepository.GetAllRegion(), "DFRegionId", "NameEn");
                ViewBag.DFGovernorateId = new SelectList(_dFGovernoratesRepository.GetAllGover(), "Id", "NameEn");
                return View(governorateVM);
            }

            if (governorateVM.NormalTotal == "total")
            {
                var govID = _dFGovernoratesRepository.GetGovernsByRegionIdWithTrue(governorateVM.DFRegionId);
                governorateVM.DFGovernorateId = govID.Id;
            }

            GovernorateVersion governorate = governorateVM.MapToGovernorateVer();
            _governorateRepository.AddVer(governorate);
            _toastNotification.AddSuccessToastMessage("Element has been Created successfully.");
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Definitions > Economic Indicators > Governorates > Add", "Add Id :" + governorate.Id);
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// get edit Governorates page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Governorate, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery]bool isVersion)
        {
            //if (id == null)
            //    return NotFound();
            //var governrate = _governorateRepository.findById(id);

            //if (governrate == null)
            //    return NotFound();
            string indicatorName = "";
            GovernorateVM governrateViewModel;
            var govVerModel = _governorateRepository.GetVerById(id);
            if (govVerModel != null && isVersion)
            {
                indicatorName = govVerModel.DFIndicator.NameEn;
                governrateViewModel = govVerModel.MapToGovernorateVM();

            }
            else
            {
                var govModel = _governorateRepository.findById(id);
                indicatorName = govModel.DFIndicator.NameEn;
                governrateViewModel = govModel.MapToGovernorateVM();
            }


            ViewBag.IndicatorName = indicatorName;
            ViewBag.IndicatorId = governrateViewModel.DFIndicatorId;
            ViewBag.Unit = governrateViewModel.Unit;
            ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn", governrateViewModel.DFYearId);

            var govern = _dFGovernoratesRepository.GetGoverById(governrateViewModel.DFGovernorateId);

            if (govern.isTotal ?? false)
            {
                ViewBag.NormalTotal = new List<SelectListItem>()
                {
                    new SelectListItem { Value = "total", Text = "Total" },
                    new SelectListItem { Value = "normal", Text = "Normal" }
                };

            }
            else
            {
                ViewBag.NormalTotal = new List<SelectListItem>()
                {
                    new SelectListItem { Value = "normal", Text = "Normal" },
                    new SelectListItem { Value = "total", Text = "Total" }
                };

            }

            ViewBag.DFRegionId = new SelectList(_dFGovernoratesRepository.GetAllRegion(), "DFRegionId", "NameEn", govern.DFRegionId);
            ViewBag.DFGovernorateId = new SelectList(_dFGovernoratesRepository.GetAllGover(), "Id", "NameEn", governrateViewModel.DFGovernorateId);
            ViewBag.DFGovernorateIdSelected = governrateViewModel.DFGovernorateId;

            return View(governrateViewModel);
        }
        /// <summary>
        /// edit Governorates page
        /// </summary>
        /// <param name="governorateVM"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Governorate, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(GovernorateVM governorateVM)
        {
            if (ModelState.IsValid)
            {

                return EditGov(governorateVM, out int id);

            }
            ViewBag.IndicatorName = _dFIndicatorRepository.GetByID(1).NameEn;
            ViewBag.IndicatorId = governorateVM.DFIndicatorId;
            ViewBag.Unit = governorateVM.Unit;
            ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn", governorateVM.DFYearId);
            ViewBag.DFRegionId = new SelectList(_dFGovernoratesRepository.GetAllRegion(), "DFRegionId", "NameEn", governorateVM.DFGovernorateId);
            ViewBag.DFGovernorateId = new SelectList(_dFGovernoratesRepository.GetAllGover(), "Id", "NameEn", governorateVM.DFGovernorateId);
            ViewBag.NormalTotal = new List<SelectListItem>()
            {
                new SelectListItem { Value = "normal", Text = "Normal" },
                new SelectListItem { Value = "total", Text = "Total" }
            };
            return View(governorateVM);
        }

        private IActionResult EditGov(GovernorateVM viewModel, out int id, bool fromDelete = false)
        {
            if (viewModel.NormalTotal == "total")
            {
                var govID = _dFGovernoratesRepository.GetGovernsByRegionIdWithTrue(viewModel.DFRegionId);
                viewModel.DFGovernorateId = govID.Id;
            }

            var govVersion = _governorateRepository.GetByGovId(viewModel.GovernorateId ?? 0);
            var investmentById = _governorateRepository.GetVerById(viewModel.Id);
            govVersion = investmentById == null ? govVersion : investmentById;
            var govVersionModel = viewModel.MapToGovernorateVer();

            govVersionModel.GovernorateId = viewModel.GovernorateId > 0 ? viewModel.GovernorateId : (int?)null;
            if (!fromDelete)
            {
                govVersionModel.ChangeActionEnum = ChangeActionEIEnum.Update;
                govVersionModel.isDeleted = false;

            }
            else
                govVersionModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
            if (govVersion == null || govVersionModel.VersionStatusEnum == VersionStatusEIEnum.Approved || govVersionModel.VersionStatusEnum == VersionStatusEIEnum.Ignored)
            {
                var user = _userManager.GetUserAsync(HttpContext.User);
                govVersionModel.CreatedById = user.Result.Id;
                govVersionModel.VersionStatusEnum = VersionStatusEIEnum.Draft;
                govVersionModel.Id = 0;


                _governorateRepository.AddVer(govVersionModel);
                _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                id = govVersionModel.Id;
                return RedirectToAction(nameof(Index));
            }

            govVersionModel.Id = govVersion != null ? govVersion.Id : viewModel.Id;

            _governorateRepository.UpdateVer(govVersionModel);
            id = govVersionModel.Id;

            _toastNotification.AddSuccessToastMessage("Element has been Edited successfully.");
            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Definitions > Economic Indicators > Governorates > Update", "Update Id :" + viewModel.Id);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// get Governorates by region id
        /// </summary>
        /// <param name="id">region id</param>
        /// <returns></returns>
        /// 
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Governorate, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public JsonResult getGovernsByRegion(int id)
        {
            var governs = _dFGovernoratesRepository.GetGovernsByRegionId(id);

            return Json(governs);
        }
        /// <summary>
        /// delete Governorates by id
        /// </summary>
        /// <param name="id">Governorates id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Governorate, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public async Task<bool> Delete(int id, bool isVersion)
        {
            try
            {
                int newid = 0;
                var verModel = _governorateRepository.GetVerById(id);
                if (verModel == null || !isVersion)
                {
                    var model = _governorateRepository.findById(id);
                    var viewModel = model.MapToGovernorateVM();
                    viewModel.isDeleted = true;
                    viewModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
                    EditGov(viewModel, out newid, true);
                }
                else
                {
                    verModel.isDeleted = true;
                    verModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
                    EditGov(verModel.MapToGovernorateVM(), out newid, true);
                }
                // await SubmitGov(newid, false);
                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Definitions > Economic Indicators > Governorates > Delete", "Soft Delete Id :" + id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// submit changes for governorate report
        /// </summary>
        /// <param name="govVersionId"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Governorate, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanEdit})]
        public async Task<bool> SubmitChangesConst(int govVersionId)
        {
            return await SubmitGov(govVersionId);
        }

        private async Task<bool> SubmitGov(int govVersionId, bool notFromDelete = true)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var govVer = _governorateRepository.GetVerById(govVersionId, false);
            bool newVerFlag = false;
            if (govVer != null)
            {
                if (newVerFlag = (govVer.GovernorateId != null || notFromDelete))
                {
                    govVer.VersionStatusEnum = VersionStatusEIEnum.Submitted;
                    _governorateRepository.UpdateVer(govVer);
                }
            }
            if (newVerFlag)
            {
                var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.Governorate);

                if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    ApprovalNotification approval = new ApprovalNotification()
                    {
                        ChangeAction = ChangeActionEnum.Update,
                        VersionStatusEnum = VersionStatusEnum.Submitted,
                        ChangesDateTime = DateTime.Now,
                        ChangeType = ChangeType.PageContent,
                        PageLink = $"/{nameof(GovernorateController)[0..^10]}",
                        PageName = PagesNamesConst.Governorate,
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
        /// approve changes for governorate report
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="componentVersionId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Governorate, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve([FromQuery] int approvalId, [FromQuery] int componentVersionId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var investmentVersion = _governorateRepository.GetVerById(componentVersionId, false);

            if (investmentVersion != null)
            {
                investmentVersion.VersionStatusEnum = VersionStatusEIEnum.Approved;

                var gov = new Governorate()
                {

                    isDeleted = investmentVersion.isDeleted,
                    DFIndicatorId = investmentVersion.DFIndicatorId,

                    DFYearId = investmentVersion.DFYearId,
                    Agriculture = investmentVersion.Agriculture,
                    Construction = investmentVersion.Construction,
                    DFIndicator = investmentVersion.DFIndicator,
                    Education = investmentVersion.Education,
                    AccommodationandFoodServiceActivities = investmentVersion.AccommodationandFoodServiceActivities,
                    BusinessServices = investmentVersion.BusinessServices,
                    Communication = investmentVersion.Communication,
                    CrudePetroleumExtraction = investmentVersion.CrudePetroleumExtraction,
                    CustomFees = investmentVersion.CustomFees,
                    DomesticWorkers = investmentVersion.DomesticWorkers,
                    ElectricityandGas = investmentVersion.ElectricityandGas,
                    Sewerage = investmentVersion.Sewerage,
                    WholesaleandRetailTrade = investmentVersion.WholesaleandRetailTrade,
                    Water = investmentVersion.Water,
                    WasteRecycling = investmentVersion.WasteRecycling,
                    Unit = investmentVersion.Unit,
                    DFGovernorateId = investmentVersion.DFGovernorateId,
                    FinancialCorporations = investmentVersion.FinancialCorporations,
                    GeneralGovernment = investmentVersion.GeneralGovernment,
                    Information = investmentVersion.Information,
                    ManufacturingIndustries = investmentVersion.ManufacturingIndustries,
                    NonFinancialCorporations = investmentVersion.NonFinancialCorporations,
                    NonProfitInstitutionsServingHouseholdSector = investmentVersion.NonProfitInstitutionsServingHouseholdSector,
                    OtherServices = investmentVersion.OtherServices,
                    PetroleumRefinement = investmentVersion.PetroleumRefinement,
                    RealEstateOwnership = investmentVersion.RealEstateOwnership,
                    TransportationandStorage = investmentVersion.TransportationandStorage,
                    TotalGDPEgyptWithCustomFees = investmentVersion.TotalGDPEgyptWithCustomFees,
                    TotalGovernorateGDP = investmentVersion.TotalGovernorateGDP,

                    Health = investmentVersion.Health,

                    OtherExtractions = investmentVersion.OtherExtractions,
                };
                if (investmentVersion.GovernorateId != null)
                {
                    gov.Id = investmentVersion.GovernorateId ?? 0;
                    _governorateRepository.Update(gov);
                }
                else
                {
                    gov.Id = 0;
                    _governorateRepository.Add(gov);
                    investmentVersion.GovernorateId = gov.Id;
                }

                _governorateRepository.UpdateVer(investmentVersion);
            }

            var allSubmited = _governorateRepository.GetAllSubmited();
            if (!allSubmited.Any())
            {
                approval.VersionStatusEnum = VersionStatusEnum.Approved;
                approval.ChangesDateTime = DateTime.Now;
                _approvalNotificationsRepository.Update(approval);

                return RedirectToAction("Index", "ApprovalNotifications");
            }

            _toastNotification.AddSuccessToastMessage(ToasrMessages.ApprovalSuccess);

            return RedirectToAction("Index", new { approvalId });
        }
        /// <summary>
        /// Ignore changes for governorate report
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="componentVersionId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Governorate, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore([FromQuery] int approvalId, [FromQuery] int componentVersionId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var govVersion = _governorateRepository.GetVerById(componentVersionId, false);

            if (govVersion != null)
            {
                govVersion.VersionStatusEnum = VersionStatusEIEnum.Ignored;
                _governorateRepository.UpdateVer(govVersion);
            }

            var allSubmited = _governorateRepository.GetAllSubmited();
            if (!allSubmited.Any())
            {
                approval.VersionStatusEnum = VersionStatusEnum.Ignored;
                approval.ChangesDateTime = DateTime.Now;
                _approvalNotificationsRepository.Update(approval);

                return RedirectToAction("Index", "ApprovalNotifications");
            }

            _toastNotification.AddSuccessToastMessage(ToasrMessages.IgnoreSuccess);


            return RedirectToAction("Index", new { approvalId });
        }

    }
}