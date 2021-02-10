using MPMAR.Business.Models;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IPageRouteRepository
    {
        //PageRoute Add(PageRoute pageRoute);

        /// <summary>
        /// Update a page route object values
        /// </summary>
        /// <param name="pageRoute">page route model</param>
        /// <returns></returns>
        PageRoute Update(PageRoute pageRoute);
       
        /// <summary>
        /// Get a page route object from database by id
        /// </summary>
        /// <param name="id">page route id</param>
        /// <returns></returns>
        PageRoute Get(int id);

        /// <summary>
        /// Get all not deleted dynamic pages from database
        /// </summary>
        /// <returns></returns>
        IEnumerable<PageRoute> GetDynamicPages();

        /// <summary>
        /// Get all not deleted static pages from database
        /// </summary>
        /// <returns></returns>
        IEnumerable<PageRoute> GetStaticPages();

        /// <summary>
        /// Delete a page route object from database by id
        /// </summary>
        /// <param name="id">page route id</param>
        /// <returns></returns>
        PageRoute Delete(int id);
        /// <summary>
        /// get all active not deleted page routes ids
        /// </summary>
        /// <returns></returns>
        List<int> GetAllId();

        /// <summary>
        /// Add a new page route to database
        /// </summary>
        /// <param name="pageRoute">page route model</param>
        /// <returns></returns>
        void Add(PageRoute pageRoute);

        /// <summary>
        /// Update a page route object values
        /// </summary>
        /// <param name="pageRoute">page route model</param>
        /// <returns></returns>
        void UpdatePageRoute(PageRoute pageRoute);

        /// <summary>
        /// Get all page routes which contains the controller name sent in parameter
        /// </summary>
        /// <param name="controllerName">controller name value</param>
        /// <returns></returns>
        PageRoute GetByControllerName(string controllerName);
        /// <summary>
        /// get global searche model which used in elastic search
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        GlobalSearchModel GetPageData(int pageRouteId);
    }
}
