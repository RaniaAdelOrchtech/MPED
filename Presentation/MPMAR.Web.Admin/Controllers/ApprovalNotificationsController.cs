using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Common;
using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Web.Admin.AuthRequirement;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Web.Admin.Mappers;
using MPMAR.Web.Admin.Services;
using MPMAR.Web.Admin.ViewModels;

namespace MPMAR.Web.Admin.Controllers
{
    [Auth2FactorFilter]
    public class ApprovalNotificationsController : Controller
    {
        private readonly IApprovalNotificationsRepository _approvalNotificationsRepository;

        private readonly IPageRouteVersionRepository _pageRouteVersion;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBEUsersPrivilegesService _bEUsersPrivilegesService;

        public ApprovalNotificationsController(IApprovalNotificationsRepository approvalNotificationsRepository, IPageRouteVersionRepository pageRouteVersion, UserManager<ApplicationUser> userManager, IBEUsersPrivilegesService bEUsersPrivilegesService)
        {
            _approvalNotificationsRepository = approvalNotificationsRepository;
            _pageRouteVersion = pageRouteVersion;
            _userManager = userManager;
            _bEUsersPrivilegesService = bEUsersPrivilegesService;
        }

        /// <summary>
        /// Index for all notifications to approval user
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Approval, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Index for all history nontifications approval approve or ignore it
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Approval, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public IActionResult HistoryIndex()
        {
            return View();
        }

        /// <summary>
        /// Get list of notifications from db
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Approval, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public async Task<IActionResult> GetAllIndex()
        {
            List<ApprovalNotificationsViewModel> viewModels = new List<ApprovalNotificationsViewModel>();
            var approvalData = _approvalNotificationsRepository.GetAll_Index();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            approvalData = _bEUsersPrivilegesService.FilterApprovalPages(approvalData.ToList(), user.Id);

            foreach (var data in approvalData)
            {
                viewModels.Add(data.MapToApprovalNotificationsViewModel());
            }
         

            return Json(new { data = viewModels.OrderByDescending(x=>x.Id) });
        }


        /// <summary>
        /// Get list of approver and ignored notifications from db
        /// </summary>
        /// <returns></returns>
        [BEUsersPrivilegesRequirement(PrivilegesPageType.Approval, new PrivilegesActions[] { PrivilegesActions.CanView })]
        public async Task<IActionResult> GetAllHistory()
        {
            var historyData = _approvalNotificationsRepository.GetAll_History();
            List<ApprovalNotificationsViewModel> viewModels = new List<ApprovalNotificationsViewModel>();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            historyData = _bEUsersPrivilegesService.FilterApprovalPages(historyData.ToList(), user.Id);

            foreach (var data in historyData)
            {
                viewModels.Add(data.MapToApprovalNotificationsViewModel());
            }
            return Json(new { data = viewModels.OrderByDescending(x => x.Id) });
        }
    }
}