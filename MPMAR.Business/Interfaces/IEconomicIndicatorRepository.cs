using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IEconomicIndicatorRepository
    {
        /// <summary>
        /// Get all economic indicators screens
        /// </summary>
        /// <returns></returns>
        IEnumerable<EconomicIndicators> GetAll();

        /// <summary>
        /// Get economic indicator by id
        /// </summary>
        /// <param name="id">economic indicator id</param>
        /// <returns></returns>
        EconomicIndicators GetById(int id);

        /// <summary>
        /// Update economic indicator object from database
        /// </summary>
        /// <param name="economicIndicators">economic indicator new data</param>
        /// <returns></returns>
        void Update(EconomicIndicators economicIndicators);

        /// <summary>
        /// Get economic indicator version object by id
        /// </summary>
        /// <param name="id">economic indicator id</param>
        /// <returns></returns>
        EconomicIndicatorsVersion GetByEcoIndiId(int id);
    }
}
