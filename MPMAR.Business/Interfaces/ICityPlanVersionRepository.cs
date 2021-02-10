using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
  public  interface ICityPlanVersionRepository
    {
        /// <summary>
        /// Adding a new city plan version
        /// </summary>
        /// <param name="cityPlanMaster">city plan version model</param>
        /// <returns></returns>
        CityPlanVersion Add(CityPlanVersion cityPlanMaster);

        /// <summary>
        /// update a city plan version
        /// </summary>
        /// <param name="cityPlanMasterItem">city plan version model</param>
        /// <returns></returns>
        CityPlanVersion Update(CityPlanVersion cityPlanMasterItem);

        /// <summary>
        /// get city plan versions objects
        /// </summary>
        /// <param name="cityPlanMasterId">city plan version id</param>
        /// <returns></returns>
        IEnumerable<CityPlanVersion> GetCityPlanId(int cityPlanMasterId);

        /// <summary>
        /// delete city plan version object
        /// </summary>
        /// <param name="id">city plan version id</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// get city plan versions objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<CityPlanVersion> Get();

        /// <summary>
        /// get detail city plan version object
        /// </summary>
        /// <param name="id">city plan version id</param>
        /// <returns></returns>
        CityPlanVersion GetDetail(int id);

        /// <summary>
        /// get draft city plan version object
        /// </summary>
        /// <param name="cityPlanId">city plan version id</param>
        /// <returns></returns>
        CityPlanVersion GetDraftByCityId(int cityPlanId);

        /// <summary>
        /// get subbmitted city plan version object
        /// </summary>
        /// <param name="cityPlanId">city plan version id</param>
        /// <returns></returns>
        CityPlanVersion GetSubmitedByCityId(int cityPlanId);

        /// <summary>
        /// get city plan version object
        /// </summary>
        /// <param name="cityId">city plan id</param>
        /// <returns></returns>
        CityPlanVersion GetByCityId(int cityId);
    }
}
