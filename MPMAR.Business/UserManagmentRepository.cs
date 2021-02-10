using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MPMAR.Data;
using MPMAR.Data.Consts;
using MPMAR.Data.Enums;
using MPMAR.Entities;
namespace MPMAR.Business
{
    public class UserManagmentRepository : IUserManagmentRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserManagmentRepository> _logger;
        private readonly IMyEmailSender _emailSender;
        private readonly ApplicationDbContext _db;

        public UserManagmentRepository()
        {

        }


        public UserManagmentRepository(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<UserManagmentRepository> logger,
            IMyEmailSender emailSender
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }




        /// <summary>
        ///  delete account
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>true if create false otherwise</returns>
        public async Task<bool> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    _logger.LogInformation(user.Email + " Was Deleted" + " by Super Admin");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///  edit account
        /// </summary>
        /// <param name="editUser">user model</param>
        /// <returns>true if create false otherwise</returns>
        public async Task<bool> Edit(EditUserRoleViewModel editUser)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(editUser.Id);
                var userRole = await _userManager.GetRolesAsync(user);
                var result = await _userManager.RemoveFromRolesAsync(user, userRole);
                if (result.Succeeded)
                {

                    result = await _userManager.AddToRoleAsync(user, editUser.IsSuperAdmin ? "SuperAdmin" : "ContentManager");
                    _logger.LogInformation(user.Email + " Was Edited by Super Admin");


                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
