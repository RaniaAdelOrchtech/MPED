using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface INavItemRepository
    {
        /// <summary>
        /// Add a new nav item to database
        /// </summary>
        /// <param name="navItem">nav item model</param>
        /// <returns></returns>
        NavItem Add(NavItem navItem);

        /// <summary>
        /// Update a nav item from database
        /// </summary>
        /// <param name="navItem">nav item model</param>
        /// <returns></returns>
        NavItem Update(NavItem navItem);

        /// <summary>
        /// Get a nav item object from database
        /// </summary>
        /// <param name="id">nav item id</param>
        /// <returns></returns>
        NavItem Get(int id);

        /// <summary>
        /// Get all not deleted nav items from database
        /// </summary>
        /// <returns></returns>
        IEnumerable<NavItem> Get();

        /// <summary>
        /// Delete a nav item object by id
        /// </summary>
        /// <param name="id">nav item id</param>
        /// <returns></returns>
        NavItem Delete(int id);

        /// <summary>
        /// Get a nav item object from database by id but with no tracking
        /// </summary>
        /// <param name="navItemId">nav item id</param>
        /// <returns></returns>
        NavItem GetByIdWithNoTracking(int navItemId);
    }
}
