using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IPageContactRepository
    {
        /// <summary>
        /// Add new page contact to database
        /// </summary>
        /// <param name="pageContact">page contact model data</param>
        /// <returns></returns>
        PageContact Add(PageContact pageContact);

        /// <summary>
        /// Update an existing page contact to database
        /// </summary>
        /// <param name="pageContact">page contact model new data</param>
        /// <returns></returns>
        PageContact Update(PageContact pageContact);

        /// <summary>
        /// Get page contact by page route version id
        /// </summary>
        /// <param name="pageRouteVersionId">page route version id</param>
        /// <returns></returns>
        IEnumerable<PageContact> GetPageContactByPageId(int pageRouteVersionId);

        /// <summary>
        /// Delete page contact object from database
        /// </summary>
        /// <param name="id">page contact id</param>
        /// <returns></returns>
        PageContact Delete(int id);

        /// <summary>
        /// Get page contact by page route version id
        /// </summary>
        /// <param name="id">page route version id</param>
        /// <returns></returns>
        PageContact Get(int pageRouteVersionid);

        /// <summary>
        /// Get page contact by page contact id
        /// </summary>
        /// <param name="id">page contact id</param>
        /// <returns></returns>
        PageContact GetDetail(int id);

        /// <summary>
        /// Get page contact version by page contact id
        /// </summary>
        /// <param name="id">page contact id</param>
        /// <returns></returns>
        PageContactVersions GetByPageContactId(int id);
    }
}
