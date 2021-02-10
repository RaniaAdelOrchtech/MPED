using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MPMAR.Data.Mappers;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using MPMAR.Data.Helpers;

namespace MPMAR.Data.Helpers
{
    public class DataInitializer
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataInitializer(IConfiguration configuration, ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            SeedRoles();

            SeedUsers();

            var statuses = _db.Statuses;
            if (statuses.Count() == 0)
            {
                SeedStatuses();
            }

            var navItems = _db.NavItems;
            if (navItems.Count() == 0)
            {
                SeedNavItems();
            }

            var pageRoutes = _db.PageRoutes;
            if (pageRoutes.Count() == 0)
            {
                SeedPageRoutes();
            }

            var pageSectionTypes = _db.PageSectionTypes;
            if (pageSectionTypes.Count() == 0)
            {
                SeedPageSectionTypes();
            }
        }

        private void SeedRoles()
        {
            bool roleExists = _db.Roles.Any(r => r.Name == "Admin");
            if (!roleExists)
            {
                var adminRole = new IdentityRole();
                adminRole.Name = "Admin";
                adminRole.NormalizedName = adminRole.Name.ToUpper();
                adminRole.ConcurrencyStamp = Guid.NewGuid().ToString();

                _db.Roles.Add(adminRole);
                _db.SaveChanges();
            }

            roleExists = _db.Roles.Any(r => r.Name == "SuperAdmin");
            if (!roleExists)
            {
                var role = new IdentityRole();
                role.Name = "SuperAdmin";
                role.NormalizedName = role.Name.ToUpper();
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                _db.Roles.Add(role);
                _db.SaveChanges();
            }

            roleExists = _db.Roles.Any(r => r.Name == "Approval");
            if (!roleExists)
            {
                var role = new IdentityRole();
                role.Name = "Approval";
                role.NormalizedName = role.Name.ToUpper();
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                _db.Roles.Add(role);
                _db.SaveChanges();
            }

            roleExists = _db.Roles.Any(r => r.Name == "ContentManager");
            if (!roleExists)
            {
                var role = new IdentityRole();
                role.Name = "ContentManager";
                role.NormalizedName = role.Name.ToUpper();
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                _db.Roles.Add(role);
                _db.SaveChanges();
            }

            roleExists = _db.Roles.Any(r => r.Name == "Viewer");
            if (!roleExists)
            {
                var role = new IdentityRole();
                role.Name = "Viewer";
                role.NormalizedName = role.Name.ToUpper();
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                _db.Roles.Add(role);
                _db.SaveChanges();
            }
        }

        private void SeedUsers()
        {
            var adminEmail = _configuration["InitialAdminEmail"];
            var adminPassword = _configuration["InitialAdminPassword"];
            bool adminExists = _db.Users.Any(u => u.Email == adminEmail);
            if (!adminExists)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = adminEmail;
                user.Email = adminEmail;
                user.EmailConfirmed = true;

                IdentityResult result = _userManager.CreateAsync(user, adminPassword).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "SuperAdmin").Wait();
                }
            }
        }

        private void SeedStatuses()
        {
            List<Status> statuses = InitialData.GetStatuses();

            _db.Statuses.AddRange(statuses);
            _db.SaveChanges();
        }

        private void SeedNavItems()
        {
            List<NavItemVersion> initialNavItemVersions = InitialData.GetNavItemVersions();

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    _db.NavItemVersions.AddRange(initialNavItemVersions);
                    _db.SaveChanges();

                    var navItems = initialNavItemVersions.MapToNavItems();

                    _db.NavItems.AddRange(navItems);
                    _db.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        private void SeedPageRoutes()
        {
            List<PageRoute> initialPageRoutes = InitialData.GetPageRoutes();

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    _db.PageRoutes.AddRange(initialPageRoutes);
                    _db.SaveChanges();

                    var pageRouteVersions = initialPageRoutes.MapToPageRouteVersions();

                    _db.PageRouteVersions.AddRange(pageRouteVersions);
                    _db.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        private void SeedPageSectionTypes()
        {
            List<PageSectionType> initialPageSectionTypes = InitialData.GetPageSectionTypes();
            initialPageSectionTypes.Reverse();
            _db.PageSectionTypes.AddRange(initialPageSectionTypes);
            _db.SaveChanges();
        }
    }
}
