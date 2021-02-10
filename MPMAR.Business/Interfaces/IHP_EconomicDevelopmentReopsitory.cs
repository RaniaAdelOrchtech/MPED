using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IHP_EconomicDevelopmentReopsitory
    {
        /// <summary>
        /// get all EconomicDevelopment
        /// </summary>
        /// <returns></returns>
        IEnumerable<EconomicDevelopment> GetAll();
        /// <summary>
        /// get EconomicDevelopment by  id
        /// </summary>
        /// <param name="id">EconomicDevelopment id</param>
        /// <returns></returns>
        EconomicDevelopment GetById(int id);
        /// <summary>
        /// update EconomicDevelopment
        /// </summary>
        /// <param name="homePagePhoto"></param>
        void Update(EconomicDevelopment homePagePhoto);
        /// <summary>
        /// get EconomicDevelopmentVersions by EconomicDevelopment id
        /// </summary>
        /// <param name="id">EconomicDevelopment id</param>
        /// <returns></returns>
        EconomicDevelopmentVersions GetByEcoDevId(int id);
    }
}
