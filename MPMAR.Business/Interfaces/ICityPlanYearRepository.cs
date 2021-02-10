using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface ICityPlanYearRepository
    {
        /// <summary>
        /// add city plan year object
        /// </summary>
        /// <param name="cityPlanYear">city plan year object</param>
        /// <returns></returns>
        CityPlanYear Add(CityPlanYear cityPlanYear);

        /// <summary>
        /// update city plan year object
        /// </summary>
        /// <param name="cityPlanYearItem">city plan year object</param>
        /// <returns></returns>
        CityPlanYear Update(CityPlanYear cityPlanYearItem);

        /// <summary>
        /// get city plan year objects by id
        /// </summary>
        /// <param name="cityPlanYearId">city plan year id</param>
        /// <returns></returns>
        IEnumerable<CityPlanYear> GetCityPlanYearId(int cityPlanYearId);

        /// <summary>
        /// delete city plan year object by id
        /// </summary>
        /// <param name="id">city plan year id</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// get all city plan year objects with id
        /// </summary>
        /// <param name="id">city plan year id</param>
        /// <returns></returns>
        IEnumerable<CityPlanYear> Get(int id);

        /// <summary>
        /// get all city plan year objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<CityPlanYear> Get();

        /// <summary>
        /// get details city plan year objects with id
        /// </summary>
        /// <param name="id">city plan year id</param>
        /// <returns></returns>
        CityPlanYear GetDetail(int id);

        /// <summary>
        /// delete list of city plan year objects
        /// </summary>
        /// <param name="list">city plan year list</param>
        /// <returns></returns>
        bool DeleteByCityPlanId(List<CityPlanYear> list);

        /// <summary>
        /// get city plan year object with no tracking for id
        /// </summary>
        /// <param name="cityPlanYearId">city plan year id</param>
        /// <returns></returns>
        CityPlanYear GetByIdWithNoTracking(int cityPlanYearId);
    }
}
