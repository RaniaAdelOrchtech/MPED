using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface ICitizenPlanVersionsRepository
    {
        /// <summary>
        /// add new CitizenPlanVersions
        /// </summary>
        /// <param name="model"></param>
        void Add(CitizenPlanVersions model);
        /// <summary>
        /// update CitizenPlanVersions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(CitizenPlanVersions model);
        /// <summary>
        /// get list of CitizenPlanVersions
        /// </summary>
        /// <returns></returns>
        List<CitizenPlanVersions> GetCitizenPlanVersions();
        /// <summary>
        /// get CitizenPlanVersions by id
        /// </summary>
        /// <param name="id">CitizenPlanVersions id</param>
        /// <returns></returns>
        CitizenPlanVersions Get(int id);
        /// <summary>
        /// get CitizenPlanVersions by CitizenPlan id
        /// </summary>
        /// <param name="id">CitizenPlan id</param>
        /// <returns></returns>
        CitizenPlanVersions GetByCitizenPlanId(int id);
        /// <summary>
        /// get list of all drafts CitizenPlanVersions
        /// </summary>
        /// <returns></returns>
        IEnumerable<CitizenPlanVersions> GetAllDrafts();
        /// <summary>
        /// get list of all submitted CitizenPlanVersions
        /// </summary>
        /// <returns></returns>
        IEnumerable<CitizenPlanVersions> GetAllSubmitted();
    }
}
