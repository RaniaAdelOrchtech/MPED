using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IPageNewsTypeRepository
    {

        /// <summary>
        /// add new PageNewsType
        /// </summary>
        /// <param name="PageNewsType"></param>
        /// <returns></returns>
        PageNewsType Add(PageNewsType PageNewsType);
        /// <summary>
        /// update PageNewsType
        /// </summary>
        /// <param name="PageNewsType"></param>
        /// <returns></returns>
        PageNewsType Update(PageNewsType PageNewsType);
        /// <summary>
        /// get list of PageNewsType
        /// </summary>
        /// <returns></returns>
        List<PageNewsType> GetPageNewsTypes();
        /// <summary>
        /// delete PageNewsType
        /// </summary>
        /// <param name="id">PageNewsType id</param>
        /// <returns></returns>
        PageNewsType Delete(int id);
        /// <summary>
        /// get PageNewsType by id
        /// </summary>
        /// <param name="id">PageNewsType id</param>
        /// <returns></returns>
        PageNewsType Get(int id);
    }
}

