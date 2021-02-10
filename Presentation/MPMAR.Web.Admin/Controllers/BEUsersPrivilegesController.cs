using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MPMAR.Business.Interfaces;
using MPMAR.Common;
using MPMAR.Data;
using MPMAR.Web.Admin.ViewModels;
using NToastNotify;

namespace MPMAR.Web.Admin.Controllers
{
    public class BEUsersPrivilegesController : Controller
    {
        private readonly IBEUsersPrivilegesRepository _iBEUsersPrivilegesRepository;
        private readonly IToastNotification _toastNotification;

        public BEUsersPrivilegesController(IBEUsersPrivilegesRepository iBEUsersPrivilegesRepository, IToastNotification toastNotification)
        {
            _iBEUsersPrivilegesRepository = iBEUsersPrivilegesRepository;
            _toastNotification = toastNotification;
        }
        /// <summary>
        /// edit user Privileges for a specific user
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = UserRolesConst.SuperAdmin)]
        public IActionResult Edit()
        {
            return View(_iBEUsersPrivilegesRepository.GetNotSuperAdminUsers());
        }
        /// <summary>
        /// get user Privileges for a specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRolesConst.SuperAdmin)]
        public IActionResult GetUserPrivileges(string userId)
        {
            var userPrivileges = _iBEUsersPrivilegesRepository.GetUserPrivileges(userId).ToList();
            var userPravligesViewModel = userPrivileges.Select(x => new BEUsersPrivilegesEntityViewModel()
            {
                Id = x.Id,
                CanDelete = x.CanDelete,
                CanView = x.CanView,
                OldCanView = x.CanView,
                ApplicationUserId = x.ApplicationUserId,
                CanAdd = x.CanAdd,
                CanApprove = x.CanApprove,
                CanEdit = x.CanEdit,
                IsDeleted = x.IsDeleted,
                PageName = x.PageName,
                PageRouteId = x.PageRouteId,
                PageTypeId = x.PageTypeId,
                PageRoute = x.PageRoute
            }).ToList();
            ViewBag.HPNames = _iBEUsersPrivilegesRepository.GetHomePageSectionsNames();
            return PartialView("_EditUserPrivileges", userPravligesViewModel);
        }
        /// <summary>
        /// edit user Privileges for a specific user
        /// </summary>
        /// <param name="userPrivilegesViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRolesConst.SuperAdmin)]
        public IActionResult Edit(List<BEUsersPrivilegesEntityViewModel> userPrivilegesViewModel)
        {
            var userPrivilege = userPrivilegesViewModel.Select(x => new BEUsersPrivileges()
            {
                Id = x.Id,
                CanDelete = x.CanDelete,
                CanView = x.CanView,
                ApplicationUserId = x.ApplicationUserId,
                CanAdd = x.CanAdd,
                CanApprove = x.CanApprove,
                CanEdit = x.CanEdit,
                IsDeleted = x.IsDeleted,
                PageName = x.PageName,
                PageRouteId = x.PageRouteId,
                PageTypeId = x.PageTypeId,
                PageRoute = x.PageRoute
            }).ToList();

            _iBEUsersPrivilegesRepository.UpdateUserPrivileges(userPrivilege);
            _toastNotification.AddSuccessToastMessage("Data Edited Successfully");
            ViewBag.SelectedUserId = userPrivilege[0]?.ApplicationUserId;
            return Ok("Data Updated Successfully");
        }
        /// <summary>
        /// reset all user privileges for all non super admin users
        /// </summary>
        /// <returns></returns>
        [HttpGet("reset-users-privileges")]
        [Authorize(Roles = UserRolesConst.SuperAdmin)]
        public IActionResult Reset()
        {
            try
            {
                _iBEUsersPrivilegesRepository.ResetUsersPrivileges();
                return Ok("users privileges reseted");
            }
            catch (Exception ex)
            {
                return BadRequest("faild to reset");
            }
        }
    }
}