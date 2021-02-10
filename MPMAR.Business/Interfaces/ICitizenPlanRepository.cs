using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface ICitizenPlanRepository
    {
        /// <summary>
        /// get first CitizenPlan if exist null otherwise
        /// </summary>
        /// <returns></returns>
        CitizenPlan Get();
        /// <summary>
        /// update CitizenPlan
        /// </summary>
        /// <param name="model">CitizenPlan model</param>
        /// <returns></returns>
        bool Update(CitizenPlan model);
        /// <summary>
        ///get  CitizenPlanVersion by CitizenPlan id
        /// </summary>
        /// <param name="id">CitizenPlan id</param>
        /// <returns></returns>
        CitizenPlanVersions GetByCitizenPlanId(int id);
    }
}
