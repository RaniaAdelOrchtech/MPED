using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IHP_AffiliatesVersionReopsitory
    {
        /// <summary>
        /// Get all affiliates versions objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePageAffiliatesVersions> GetAll();

        /// <summary>
        /// Get affaility by id
        /// </summary>
        /// <param name="id">affaility id</param>
        /// <returns></returns>
        HomePageAffiliatesVersions GetById(int id);

        /// <summary>
        /// Update affaility object
        /// </summary>
        /// <param name="homePageAffiliates">affaility model new data</param>
        void Update(HomePageAffiliatesVersions homePageAffiliates);

        /// <summary>
        /// Add new affaility model to database
        /// </summary>
        /// <param name="homePageAffiliates">affaility object model</param>
        void Add(HomePageAffiliatesVersions homePageAffiliates);

        /// <summary>
        /// Delete affailty object by id
        /// </summary>
        /// <param name="id">affaility id</param>
        /// <returns></returns>
        bool SoftDelete(int id);

        /// <summary>
        /// Get affaility version by affility id
        /// </summary>
        /// <param name="v">affaility id</param>
        /// <returns></returns>
        HomePageAffiliatesVersions GetByAffilitId(int v);

        /// <summary>
        /// Get all draftted affailities
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePageAffiliatesVersions> GetAllDrafts();

        /// <summary>
        /// Get all submitted affailities
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePageAffiliatesVersions> GetAllSubmitted();
    }
}
