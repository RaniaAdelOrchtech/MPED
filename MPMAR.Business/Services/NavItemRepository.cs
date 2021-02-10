using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Business.Services
{
    public class NavItemRepository : INavItemRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly INavItemVersionRepository _navItemVersionRepository;
        private readonly ILogger<NavItemRepository> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string userName;
        private readonly string _userId;

        public NavItemRepository(ApplicationDbContext db, INavItemVersionRepository navItemVersionRepository, ILogger<NavItemRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _navItemVersionRepository = navItemVersionRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            _userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        /// <summary>
        /// Add a new nav item to database
        /// </summary>
        /// <param name="navItem">nav item model</param>
        /// <returns>Added nav item object</returns>
        public NavItem Add(NavItem navItem)
        {
            try
            {
                _db.NavItems.Add(navItem);
                _db.SaveChanges();

                _logger.LogInformation($"User: {userName} has added nav item with name: {navItem.EnName}");
            }
            catch (Exception ex)
            {
                navItem = null;
            }

            return navItem;
        }

        /// <summary>
        /// Update a nav item from database
        /// </summary>
        /// <param name="navItem">nav item model</param>
        /// <returns>Updated object</returns>
        public NavItem Update(NavItem navItem)
        {
            try
            {
                _db.NavItems.Update(navItem);
                _db.SaveChanges();

                return navItem;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Delete a nav item object by id
        /// </summary>
        /// <param name="id">nav item id</param>
        /// <returns>The deleted object</returns>
        public NavItem Delete(int id)
        {
            NavItem navItem = _db.NavItems.Find(id);
            try
            {
                NavItemVersion navItemVersion = navItem.MapToNavItemVersion();
                navItemVersion.IsDeleted = true;
                navItemVersion.CreatedById = _userId;
                navItemVersion.CreationDate = DateTime.Now;
                _db.NavItemVersions.Add(navItemVersion);
                int deleted = _db.SaveChanges();
                if (deleted > 0)
                {
                    _logger.LogInformation($"User: {userName} has deleted nav item with name: {navItem.EnName}");
                }

                return navItem;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get a nav item object from database
        /// </summary>
        /// <param name="id">nav item id</param>
        /// <returns>Single nav item object</returns>
        public NavItem Get(int id)
        {
            return _db.NavItems.Find(id);
        }

        /// <summary>
        /// Get all not deleted nav items from database
        /// </summary>
        /// <returns>IEnumerable of navitems</returns>
        public IEnumerable<NavItem> Get()
        {
         

            return _db.NavItems.Where(n => !n.IsDeleted).OrderBy(n => n.Id).ToList();
        }

        /// <summary>
        /// Get a nav item object from database by id but with no tracking
        /// </summary>
        /// <param name="navItemId">nav item id</param>
        /// <returns>Single nav item</returns>
        public NavItem GetByIdWithNoTracking(int navItemId)
        {
            return _db.NavItems.Include(x=>x.PageRoutes).Include(x=>x.NavItemList).AsNoTracking().FirstOrDefault(x => x.Id == navItemId);
        }
    }
}
