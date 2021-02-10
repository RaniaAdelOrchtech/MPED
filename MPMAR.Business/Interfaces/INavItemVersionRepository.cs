using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface INavItemVersionRepository
    {
        /// <summary>
        /// Add new nav item version to database
        /// </summary>
        /// <param name="navItemVersion">nav item version model</param>
        /// <returns></returns>
        NavItemVersion Add(NavItemVersion navItemVersion);
        
        /// <summary>
        /// Update an existing nav item version object
        /// </summary>
        /// <param name="navItemVersion">nav item version object new data</param>
        /// <returns></returns>
        NavItemVersion Update(NavItemVersion navItemVersion);

        /// <summary>
        /// Get nav item version object by id
        /// </summary>
        /// <param name="id">nav item version id</param>
        /// <returns></returns>
        NavItemVersion Get(int id);

        /// <summary>
        /// Get all nav item versions objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<NavItemVersion> Get();

        /// <summary>
        /// Delete nav item version object by id
        /// </summary>
        /// <param name="id">nav item version id</param>
        /// <returns></returns>
        NavItemVersion Delete(int id);

        /// <summary>
        /// request to apply changes on object
        /// </summary>
        /// <param name="id">nav item version id</param>
        /// <returns></returns>
        NavItemVersion ApplyEditRequest(int id);

        /// <summary>
        /// Get all drafted nav item versions objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<NavItemVersion> GetAllDrafts();

        /// <summary>
        /// Get all submitted nav item versions objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<NavItemVersion> GetAllSubmitted();

        /// <summary>
        /// Get nav item version by nav id
        /// </summary>
        /// <param name="navId">nav id</param>
        /// <returns></returns>
        NavItemVersion GetByNavId(int navId);
    }
}
