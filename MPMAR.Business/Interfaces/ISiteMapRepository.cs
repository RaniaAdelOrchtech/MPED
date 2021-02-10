using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface ISiteMapRepository
    {
        /// <summary>
        /// add new SiteMap
        /// </summary>
        /// <param name="footerMenuItem"></param>
        /// <returns></returns>
        SiteMap Add(SiteMap footerMenuItem);
        /// <summary>
        /// update SiteMap
        /// </summary>
        /// <param name="footerMenuItem"></param>
        /// <returns></returns>
        SiteMap Update(SiteMap footerMenuItem);
        /// <summary>
        /// delete SiteMap
        /// </summary>
        /// <param name="id">SiteMap id</param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// get SiteMap by id
        /// </summary>
        /// <param name="id">SiteMap id</param>
        /// <returns></returns>
        SiteMap Get(int id);
        /// <summary>
        /// get SiteMap by id
        /// </summary>
        /// <param name="id">SiteMap id</param>
        /// <returns></returns>
        SiteMap GetDetail(int id);
    }
}
