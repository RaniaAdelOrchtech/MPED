using Microsoft.AspNetCore.Identity;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPMAR.Business
{
    public class SeedDataTest
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedDataTest()
        {
        }

        public SeedDataTest(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> SeedRoles()
        {
            var adminRole = await _roleManager.RoleExistsAsync("Admin");
            if (!adminRole)
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                await _roleManager.CreateAsync(role);
            }
            var superAdmin = await _roleManager.RoleExistsAsync("SuperAdmin");
            if (!superAdmin)
            {
                var role = new IdentityRole();
                role.Name = "SuperAdmin";
                await _roleManager.CreateAsync(role);
            }
            bool approvalRole = await _roleManager.RoleExistsAsync("Approval");
            if (!approvalRole)
            {
                var role = new IdentityRole();
                role.Name = "Approval";
                await _roleManager.CreateAsync(role);
            }
            bool contentManager = await _roleManager.RoleExistsAsync("ContentManager");
            if (!contentManager)
            {
                var role = new IdentityRole();
                role.Name = "ContentManager";
                await _roleManager.CreateAsync(role);
            }
            bool viewer = await _roleManager.RoleExistsAsync("Viewer");
            if (!approvalRole)
            {
                var role = new IdentityRole();
                role.Name = "Viewer";
                await _roleManager.CreateAsync(role);
            }
            return true;
        }
    }
}
