
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IFormerMinistriesPageInfoRepository
    {
        /// <summary>
        /// get fisrt FormerMinistriesPageInfo if exist null otherwise
        /// </summary>
        /// <returns></returns>
        FormerMinistriesPageInfo Get();
        /// <summary>
        /// update FormerMinistriesPageInfo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(FormerMinistriesPageInfo model);
        /// <summary>
        /// add new FormerMinistriesPageInfo
        /// </summary>
        /// <param name="model"></param>
        void Add(FormerMinistriesPageInfo model);
        
    }
}
