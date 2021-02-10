using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface ICityPlanRepository
    {
        /// <summary>
        /// Add a new cityplan object
        /// </summary>
        /// <param name="cityPlanMaster">city plan model</param>
        /// <returns></returns>
        CityPlan Add(CityPlan cityPlanMaster);

        /// <summary>
        /// update a cityplan object
        /// </summary>
        /// <param name="cityPlanMasterItem">city plan model</param>
        /// <returns></returns>
        CityPlan Update(CityPlan cityPlanMasterItem);

        /// <summary>
        /// get all cityplan objects
        /// </summary>
        /// <param name="cityPlanMasterId">city plan id</param>
        /// <returns></returns>
        IEnumerable<CityPlan> GetCityPlanId(int cityPlanMasterId);

        /// <summary>
        /// Delete cityplan object
        /// </summary>
        /// <param name="id">city plan id</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// get all cityplan objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<CityPlan> Get();

        /// <summary>
        /// get detail of city plan onject
        /// </summary>
        /// <param name="id">city plan id</param>
        /// <returns></returns>
        CityPlan GetDetail(int id);

        /// <summary>
        /// get city plan object by id with no tracking for id
        /// <param name="cityPlanId">city plan id</param>
        /// </summary>
        /// <returns></returns>
        CityPlan GetByIdWithNoTracking(int cityPlanId);
    }
}
