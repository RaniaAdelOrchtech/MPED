using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
   public interface ICityPlanYearVersionRepository
    {
        /// <summary>
        /// add city plan year version object
        /// </summary>
        /// <param name="cityPlanYear">city plan year version object</param>
        /// <returns></returns>
        CityPlanYearVersion Add(CityPlanYearVersion cityPlanYear);

        /// <summary>
        /// update city plan year version object
        /// </summary>
        /// <param name="cityPlanYearItem">city plan year version object</param>
        /// <returns></returns>
        CityPlanYearVersion Update(CityPlanYearVersion cityPlanYearItem);

        /// <summary>
        /// get city plan year versions objects
        /// </summary>
        /// <param name="cityPlanYearId">city plan year version id</param>
        /// <returns></returns>
        IEnumerable<CityPlanYearVersion> GetCityPlanYearId(int cityPlanYearId);

        /// <summary>
        /// delete city plan year versions objects
        /// </summary>
        /// <param name="id">city plan year version id</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// get city plan year versions objects
        /// </summary>
        /// <param name="id">city plan year version id</param>
        /// <returns></returns>
        IEnumerable<CityPlanYearVersion> Get(int id);

        /// <summary>
        /// get city plan year versions objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<CityPlanYearVersion> Get();

        /// <summary>
        /// get city plan year version object details
        /// </summary>
        /// <param name="id">city plan year version id</param>
        /// <returns></returns>
        CityPlanYearVersion GetDetail(int id);

        /// <summary>
        /// delete city plan year versions objects list
        /// </summary>
        /// <param name="list">city plan year versions list</param>
        /// <returns></returns>
        bool DeleteByCityPlanId(List<CityPlanYearVersion> list);

        /// <summary>
        /// get city plan year versions objects by id
        /// </summary>
        /// <param name="id">city plan year version id</param>
        /// <returns></returns>
        IEnumerable<CityPlanYearVersion> GetByCityVerId(int id);

        /// <summary>
        /// get drafts city plan year versions objects by id
        /// </summary>
        /// <param name="cityPlanVerId">city plan year version id</param>
        /// <returns></returns>
        IEnumerable<CityPlanYearVersion> GetDraftByCityId(int cityPlanVerId);

        /// <summary>
        /// get submitted city plan year versions objects by id
        /// </summary>
        /// <param name="cityPlanVerId">city plan year version id</param>
        /// <returns></returns>
        IEnumerable<CityPlanYearVersion> GetSubmitedtByCityId(int cityPlanVerId);

        /// <summary>
        /// get city plan year version object by id
        /// </summary>
        /// <param name="id">city plan year id</param>
        /// <returns></returns>
        CityPlanYearVersion GetByCityYearId(int id);

        /// <summary>
        /// get city plan year version object by id and not ignored
        /// </summary>
        /// <param name="id">city plan year id</param>
        /// <returns></returns>
        CityPlanYearVersion GetByCityYearIdNotIgnoren(int id);

        /// <summary>
        /// get city plan year version object by id
        /// </summary>
        /// <param name="id">city plan year version id</param>
        /// <returns></returns>
        CityPlanYearVersion GetByCityPlanVer(int id);
    }
}
