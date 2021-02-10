using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MPMAR.Analytics.Data;
using MPMAR.Analytics.Data.Enums;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Interfaces.Analytics;
using MPMAR.Common;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;
using MPMAR.Business;
using System;
using System.Collections.Generic;
using MPMAR.Common.Interfaces;
using MPMAR.Data.Consts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System.Linq;
using MPMAR.Web.Admin.AuthRequirement;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class InvestmentController : Controller
    {
        private readonly IInvestmentRepository _investmentRepository;
        private readonly IDFIndicatorRepository _dFIndicatorRepository;
        private readonly IToastNotification _toastNotification;
        private readonly IEventLogger<InvestmentController> _eventLogger;
        private readonly IDFYearsRepository _dFYearsRepository;
        private readonly IDFQuartersRepository _dFQuartersRepository;
        private readonly IDFSourceRepository _dFSourceRepository;
        private readonly IDFUnitRepository _dFUnitRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;


        public InvestmentController(IInvestmentRepository investmentRepository, IToastNotification toastNotification, IEventLogger<InvestmentController> eventLogger, IDFIndicatorRepository dFIndicatorRepository, IDFYearsRepository dFYearsRepository, IDFQuartersRepository dFQuartersRepository, IDFSourceRepository dFSourceRepository, IDFUnitRepository dFUnitRepository, UserManager<ApplicationUser> userManager, IApprovalNotificationsRepository approvalNotificationsRepository)
        {
            _investmentRepository = investmentRepository;
            _dFIndicatorRepository = dFIndicatorRepository;
            _dFYearsRepository = dFYearsRepository;
            _dFQuartersRepository = dFQuartersRepository;
            _dFSourceRepository = dFSourceRepository;
            _toastNotification = toastNotification;
            _eventLogger = eventLogger;
            _dFUnitRepository = dFUnitRepository;
            _userManager = userManager;

            _approvalNotificationsRepository = approvalNotificationsRepository;
        }
        /// <summary>
        /// get Investment page index
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Investment, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index([FromQuery] int approvalId)
        {
            ViewBag.approvalId = approvalId;
            return View();
        }
        /// <summary>
        /// get list of Investment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Investment, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public async Task<JsonResult> GetAll(int approvalId)
        {
            int start = Convert.ToInt32(Request.Form["start"]);
            int lenght = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            int totalCount = 0;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = _userManager.GetRolesAsync(user);

            IEnumerable<InvestmentViewModel> investmentData = _investmentRepository.GetAll(searchValue, sortColumnName, sortDirection, start, lenght, out totalCount, approvalId > 0 ? UserRolesConst.ApprovalUser : "");


            return Json(new
            {
                data = investmentData,
                draw = Request.Form["draw"],
                recordsTotal = totalCount,
                recordsFiltered = totalCount
            });
        }
        /// <summary>
        /// get Investment create page
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Investment, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public IActionResult Create()
        {
            FormCommon();
            return View();
        }
        /// <summary>
        /// create Investment
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Investment, new PrivilegesActions[] { PrivilegesActions.CanAdd })]
        public async Task<IActionResult> Create(InvestmentFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                viewModel.CreatedById = user.Id;

                _investmentRepository.AddVer(viewModel.MapToInvestmentModelVer());
                _toastNotification.AddSuccessToastMessage("Element has been Created successfully.");

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Add, "Definitions > Economic Indicators > Investment > Add", " Id :" + viewModel.Id);

                return RedirectToAction(nameof(Index));
            }
            FormCommon();
            return View(viewModel);
        }
        /// <summary>
        /// get Investment edit page
        /// </summary>
        /// <param name="id">investment id</param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Investment, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(int id, [FromQuery]bool isVersion)
        {
            InvestmentFormViewModel investmentViewModel;
            var investmentVerModel = _investmentRepository.GetVerById(id);
            if (investmentVerModel != null && isVersion)
                investmentViewModel = investmentVerModel.MapToInvestmentFormViewModelVer();
            else
            {
                var componentRGDPModel = _investmentRepository.GetById(id);
                investmentViewModel = componentRGDPModel.MapToInvestmentFormViewModel();
            }


            ViewBag.Indicator = _dFIndicatorRepository.GetByID((int)DFIndicatorEnum.PublicInvestments).NameEn;
            ViewBag.Source = _dFSourceRepository.GetByID((int)DFSourceEnum.MinistryOfPlanning).NameEn;
            ViewBag.Unit = _dFUnitRepository.GetByID((int)DFUnitEnum.MillionEGP).NameEn;
            ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn", investmentViewModel.DFYearId);
            ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn", investmentViewModel.DFQuarterId);

            return View(investmentViewModel);

        }
        /// <summary>
        /// edit investment
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Investment, new PrivilegesActions[] { PrivilegesActions.CanEdit })]
        public IActionResult Edit(InvestmentFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return EditInvetment(viewModel, out int id);

            }

            ViewBag.Indicator = _dFIndicatorRepository.GetByID((int)DFIndicatorEnum.PublicInvestments).NameEn;
            ViewBag.Source = _dFSourceRepository.GetByID((int)DFSourceEnum.MinistryOfPlanning).NameEn;
            ViewBag.Unit = _dFUnitRepository.GetByID((int)DFUnitEnum.MillionEGP).NameEn;
            ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn", viewModel.DFYearId);
            ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn", viewModel.DFQuarterId);

            return View(viewModel);
        }

        private IActionResult EditInvetment(InvestmentFormViewModel viewModel, out int id, bool fromDelete = false)
        {
            var invetmentVersion = _investmentRepository.GetByInvetmentId(viewModel.Investmentid ?? 0);
            var investmentById = _investmentRepository.GetVerById(viewModel.Id);
            invetmentVersion = investmentById == null ? invetmentVersion : investmentById;
            var invementVersionModel = viewModel.MapToInvestmentModelVer();

            invementVersionModel.InvestmentsId = viewModel.Investmentid > 0 ? viewModel.Investmentid : (int?)null;
            if (!fromDelete)
            {
                invementVersionModel.ChangeActionEnum = ChangeActionEIEnum.Update;
                invementVersionModel.isDeleted = false;
            }
            else
                invementVersionModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
            if (invetmentVersion == null || invementVersionModel.VersionStatusEnum == VersionStatusEIEnum.Approved || invementVersionModel.VersionStatusEnum == VersionStatusEIEnum.Ignored)
            {

                invementVersionModel.VersionStatusEnum = VersionStatusEIEnum.Draft;
                invementVersionModel.Id = 0;
                var user = _userManager.GetUserAsync(HttpContext.User);
                invementVersionModel.CreatedById = user.Result.Id;


                _investmentRepository.AddVer(invementVersionModel);
                _toastNotification.AddSuccessToastMessage(ToasrMessages.EditSuccess);
                id = invementVersionModel.Id;
                return RedirectToAction(nameof(Index));
            }

            invementVersionModel.Id = invetmentVersion != null ? invetmentVersion.Id : viewModel.Id;

            _investmentRepository.UpdateVer(invementVersionModel);
            id = invementVersionModel.Id;

            _toastNotification.AddSuccessToastMessage("Element has been Edited successfully.");

            _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Update, "Definitions > Economic Indicators > Investment > Edit", " Id :" + viewModel.Id);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// common needed data 
        /// </summary>
        private void FormCommon()
        {
            ViewBag.Indicator = _dFIndicatorRepository.GetByID((int)DFIndicatorEnum.PublicInvestments).NameEn;
            ViewBag.Source = _dFSourceRepository.GetByID((int)DFSourceEnum.MinistryOfPlanning).NameEn;
            ViewBag.Unit = _dFUnitRepository.GetByID((int)DFUnitEnum.MillionEGP).NameEn;
            ViewBag.DFYearId = new SelectList(_dFYearsRepository.GetAll(), "Id", "NameEn");
            ViewBag.DFQuarterId = new SelectList(_dFQuartersRepository.GetAll(), "Id", "NameEn");
        }
        /// <summary>
        /// delete investment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Investment, new PrivilegesActions[] { PrivilegesActions.CanDelete })]
        public  bool Delete(int id, bool isVersion)
        {
            try
            {
                int newid = 0;
                var verModel = _investmentRepository.GetVerById(id);
                if (verModel == null || !isVersion)
                {
                    var model = _investmentRepository.GetById(id);
                    var viewModel = model.MapToInvestmentFormViewModel();
                    viewModel.IsDeleted = true;
                    viewModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
                    EditInvetment(viewModel, out newid, true);
                }
                else
                {
                    verModel.isDeleted = true;
                    verModel.ChangeActionEnum = ChangeActionEIEnum.Delete;
                    EditInvetment(verModel.MapToInvestmentFormViewModelVer(), out newid, true);
                }
              //  await SubmitInvetment(newid, false);

            

                _eventLogger.LogInfoEvent(HttpContext.User.Identity.Name, Common.ActivityEnum.Delete, "Definitions > Economic Indicators > Investment > Delete", " Id :" + id);

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// submit changes for investment report
        /// </summary>
        /// <param name="investmentVersionId"></param>
        /// <returns></returns>
        [HttpPost]
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Investment, new PrivilegesActions[] { PrivilegesActions.CanAdd, PrivilegesActions.CanDelete, PrivilegesActions.CanEdit })]
        public async Task<bool> SubmitChangesConst(int investmentVersionId)
        {
            return await SubmitInvetment(investmentVersionId);
        }

        private async Task<bool> SubmitInvetment(int investmentVersionId, bool notFromDelete = true)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var investmentVer = _investmentRepository.GetVerById(investmentVersionId, false);
            bool newVerFlag = false;
            if (investmentVer != null)
            {
                if (newVerFlag = (investmentVer.InvestmentsId != null || notFromDelete))
                {
                    investmentVer.VersionStatusEnum = VersionStatusEIEnum.Submitted;
                    _investmentRepository.UpdateVer(investmentVer);
                }
            }
            if (newVerFlag)
            {
                var ifApprovalExist = _approvalNotificationsRepository.GetByPageName(PagesNamesConst.Investment);

                if (ifApprovalExist == null || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Approved || ifApprovalExist.VersionStatusEnum == VersionStatusEnum.Ignored)
                {
                    ApprovalNotification approval = new ApprovalNotification()
                    {
                        ChangeAction = ChangeActionEnum.Update,
                        VersionStatusEnum = VersionStatusEnum.Submitted,
                        ChangesDateTime = DateTime.Now,
                        ChangeType = ChangeType.PageContent,
                        PageLink = $"/{nameof(InvestmentController)[0..^10]}",
                        PageName = PagesNamesConst.Investment,
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
        /// approve changes for investment report
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="componentVersionId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Investment, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Approve([FromQuery] int approvalId, [FromQuery] int componentVersionId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var investmentVersion = _investmentRepository.GetVerById(componentVersionId, false);

            if (investmentVersion != null)
            {
                investmentVersion.VersionStatusEnum = VersionStatusEIEnum.Approved;

                var invetment = new Investments()
                {

                    isDeleted = investmentVersion.isDeleted,
                    DFIndicatorId = investmentVersion.DFIndicatorId,
                    DFQuarterId = investmentVersion.DFQuarterId,
                    DFSourceId = investmentVersion.DFSourceId,
                    DFUnitId = investmentVersion.DFUnitId,
                    AccommodationAndFoodServiceActivities = investmentVersion.AccommodationAndFoodServiceActivities,
                    DFYearId = investmentVersion.DFYearId,
                    Agriculture = investmentVersion.Agriculture,
                    Construction = investmentVersion.Construction,
                    DFIndicator = investmentVersion.DFIndicator,
                    Education = investmentVersion.Education,
                    Electricity = investmentVersion.Electricity,
                    FinancialIntermediaryInsuranceAndSocialSecurity = investmentVersion.FinancialIntermediaryInsuranceAndSocialSecurity,
                    WholesaleAndRetailTrade = investmentVersion.WholesaleAndRetailTrade,
                    Health = investmentVersion.Health,
                    InformationAndCommunication = investmentVersion.InformationAndCommunication,
                    PetroleumRefining = investmentVersion.PetroleumRefining,
                    NaturalGas = investmentVersion.NaturalGas,
                    WaterAndSewerage = investmentVersion.WaterAndSewerage,
                    TotalInvestments = investmentVersion.TotalInvestments,
                    SuezCanal = investmentVersion.SuezCanal,
                    StorageAndTransportation = investmentVersion.StorageAndTransportation,
                    RealEstateActivities = investmentVersion.RealEstateActivities,
                    Petroleum = investmentVersion.Petroleum,
                    OtherSrvices = investmentVersion.OtherSrvices,
                    OtherManufacturing = investmentVersion.OtherManufacturing,
                    OtherExtractions = investmentVersion.OtherExtractions,
                };
                if (investmentVersion.InvestmentsId != null)
                {
                    invetment.Id = investmentVersion.InvestmentsId ?? 0;
                    _investmentRepository.Update(invetment);
                }
                else
                {
                    invetment.Id = 0;
                    _investmentRepository.Add(invetment);
                    investmentVersion.InvestmentsId = invetment.Id;
                }

                _investmentRepository.UpdateVer(investmentVersion);
            }

            var allSubmited = _investmentRepository.GetAllSubmited();
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
        /// Ignore changes for investment report
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="componentVersionId"></param>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Investment, new PrivilegesActions[] { PrivilegesActions.CanApprove })]
        public async Task<IActionResult> Ignore([FromQuery] int approvalId, [FromQuery] int componentVersionId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var approval = _approvalNotificationsRepository.GetById(approvalId);

            var componentVersion = _investmentRepository.GetVerById(componentVersionId, false);

            if (componentVersion != null)
            {
                componentVersion.VersionStatusEnum = VersionStatusEIEnum.Ignored;
                _investmentRepository.UpdateVer(componentVersion);
            }

            var allSubmited = _investmentRepository.GetAllSubmited();
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