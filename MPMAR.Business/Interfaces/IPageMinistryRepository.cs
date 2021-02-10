using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IPageMinistryRepository 
    {
        /// <summary>
        /// add new PageMinistry
        /// </summary>
        /// <param name="sectionCardVersion"></param>
        /// <returns></returns>
        PageMinistry Add(PageMinistry sectionCardVersion);
        /// <summary>
        /// update PageMinistry
        /// </summary>
        /// <param name="sectionCardVersion"></param>
        /// <returns></returns>
        PageMinistry Update(PageMinistry sectionCardVersion);
        /// <summary>
        /// get PageMinistry by page route id
        /// </summary>
        /// <param name="pageRoutId">page route id</param>
        /// <returns></returns>
        IEnumerable<PageMinistry> GetPageMinistryByPageId(int pageRoutId);
        /// <summary>
        /// delete PageMinistry 
        /// </summary>
        /// <param name="id">PageMinistry id</param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// get PageMinistry by PageRouteId id
        /// </summary>
        /// <param name="id">PageRouteId id</param>
        /// <returns></returns>
        PageMinistry GetByPageRouteId(int id);
        /// <summary>
        /// get PageMinistry by id
        /// </summary>
        /// <param name="id">PageMinistry id</param>
        /// <returns></returns>
        PageMinistry GetDetail(int id);
    }
}
