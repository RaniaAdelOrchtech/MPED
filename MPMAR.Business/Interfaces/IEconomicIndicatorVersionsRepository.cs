using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IEconomicIndicatorVersionsRepository
    {
        /// <summary>
        /// Add Economic indecator version object
        /// </summary>
        /// <param name="model">economic indicator version model</param>
        /// <returns></returns>
        void Add(EconomicIndicatorsVersion model);
        
        /// <summary>
        /// Update economic indicator object from database
        /// </summary>
        /// <param name="model">economic indicator object new data</param>
        /// <returns></returns>
        bool Update(EconomicIndicatorsVersion model);

        /// <summary>
        /// Get all economic indicator versions
        /// </summary>
        /// <returns></returns>
        List<EconomicIndicatorsVersion> GetEcoIndiVersions();

        /// <summary>
        /// Get economic indicator object by id
        /// </summary>
        /// <param name="id">economic indicator id</param>
        /// <returns></returns>
        EconomicIndicatorsVersion Get(int id);

        /// <summary>
        /// Get economic indicator version object by economic indicator id
        /// </summary>
        /// <param name="id">economic indicator id</param>
        /// <returns></returns>
        EconomicIndicatorsVersion GetByEcoIndiId(int id);

        /// <summary>
        /// Get all drafts economic indicators objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<EconomicIndicatorsVersion> GetAllDrafts();

        /// <summary>
        /// Get all submitted economic indicators objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<EconomicIndicatorsVersion> GetAllSubmitted();
    }
}
