using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
   public interface IFormerMinistriesPageInfoVersionRepository
    {
        /// <summary>
        /// get all FormerMinistriesPageInfoVersions
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="includeFlage">true to incude MinistryTimeLineVersions false otherwise</param>
        /// <returns></returns>
        FormerMinistriesPageInfoVersions Get(string userId = "", bool includeFlage = true);
        /// <summary>
        /// update FormerMinistriesPageInfoVersions
        /// </summary>
        /// <param name="model">FormerMinistriesPageInfoVersions model</param>
        /// <returns></returns>
        bool Update(FormerMinistriesPageInfoVersions model);
        /// <summary>
        /// add new FormerMinistriesPageInfoVersions
        /// </summary>
        /// <param name="model">FormerMinistriesPageInfoVersions model</param>
        void Add(FormerMinistriesPageInfoVersions model);
    }
}
