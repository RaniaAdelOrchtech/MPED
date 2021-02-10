using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IHP_EconomicDevelopmentVersionRepository
    {
        /// <summary>
        /// Add new economical development version object to database
        /// </summary>
        /// <param name="model">economical development version model</param>
        void Add(EconomicDevelopmentVersions model);

        /// <summary>
        /// Update economical development version object from database
        /// </summary>
        /// <param name="model">economical development version model</param>
        /// <returns></returns>
        bool Update(EconomicDevelopmentVersions model);

        /// <summary>
        /// Get all versions of economical development 
        /// </summary>
        /// <returns></returns>
        List<EconomicDevelopmentVersions> GetEcoDevVersions();
        
        /// <summary>
        /// Get economical development version by id
        /// </summary>
        /// <param name="id">economical development version id</param>
        /// <returns></returns>
        EconomicDevelopmentVersions Get(int id);

        /// <summary>
        /// Get economical development version by economical development id
        /// </summary>
        /// <param name="id">economical development id</param>
        /// <returns></returns>
        EconomicDevelopmentVersions GetByEcoDevId(int id);

        /// <summary>
        /// Get all drafts from economical development versions
        /// </summary>
        /// <returns></returns>
        IEnumerable<EconomicDevelopmentVersions> GetAllDrafts();

        /// <summary>
        /// Get all submitted from economical development versions
        /// </summary>
        /// <returns></returns>
        IEnumerable<EconomicDevelopmentVersions> GetAllSubmitted();
    }
}
