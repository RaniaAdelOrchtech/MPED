using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
   public interface IPageMinistryVersionsRepository
    {
        /// <summary>
        /// add new PageMinistryVersion
        /// </summary>
        /// <param name="sectionCardVersion"></param>
        /// <returns></returns>
        PageMinistryVersion Add(PageMinistryVersion sectionCardVersion);
        /// <summary>
        /// update PageMinistryVersion
        /// </summary>
        /// <param name="sectionCardVersion"></param>
        /// <returns></returns>
        PageMinistryVersion Update(PageMinistryVersion sectionCardVersion);
        /// <summary>
        /// get PageMinistryVersion by page route id
        /// </summary>
        /// <param name="pageRouteId">page route id</param>
        /// <returns></returns>
        IEnumerable<PageMinistryVersion> GetPageMinistryByPageId(int pageRouteId);
        /// <summary>
        /// delete PageMinistryVersion 
        /// </summary>
        /// <param name="id">PageMinistryVersion id</param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// get PageMinistryVersion by page route id
        /// </summary>
        /// <param name="id">page route id</param>
        /// <returns></returns>
        PageMinistryVersion GetByPageRouteId(int id);
        /// <summary>
        /// get PageMinistryVersion by id
        /// </summary>
        /// <param name="id">PageMinistryVersion id</param>
        /// <returns></returns>
        PageMinistryVersion GetDetail(int id);
        /// <summary>
        /// get list of PageMinistryVersion by page route id 
        /// </summary>
        /// <param name="pageRoteId"></param>
        /// <returns></returns>
        List<PageMinistryVersion> GetMinistries(int pageRoteId);
        /// <summary>
        /// get PageMinistryVersion by PageMinistry id
        /// </summary>
        /// <param name="id">PageMinistry id</param>
        /// <returns></returns>
        PageMinistryVersion GetByPageMinistryId(int id);
        /// <summary>
        /// get list of alldrafts PageMinistryVersion
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        IEnumerable<PageMinistryVersion> GetAllDrafts(int pageRouteId);
        /// <summary>
        /// get list of all submited PageMinistryVersion 
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        IEnumerable<PageMinistryVersion> GetAllSubmitted(int pageRouteId);
    }
}
