using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IHP_LogoLinkVersionRepository
    {
        /// <summary>
        /// Add new logo link version to database
        /// </summary>
        /// <param name="model">logo link version model</param>
        void Add(HomePageLogoLinkVersions model);

        /// <summary>
        /// Update an existing logo link version object
        /// </summary>
        /// <param name="model">logo link version new data</param>
        /// <returns></returns>
        bool Update(HomePageLogoLinkVersions model);

        /// <summary>
        /// Get all logo link versions objects
        /// </summary>
        /// <returns></returns>
        List<HomePageLogoLinkVersions> GetLogoLinkVersions();

        /// <summary>
        /// Get logo link version object by id
        /// </summary>
        /// <param name="id">logo link version id</param>
        /// <returns></returns>
        HomePageLogoLinkVersions Get(int id);

        /// <summary>
        /// Get logo link version by logolink id
        /// </summary>
        /// <param name="id"> logo link id</param>
        /// <returns></returns>
        HomePageLogoLinkVersions GetByLogoLinkId(int id);

        /// <summary>
        /// Get all logolink versions drafts
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePageLogoLinkVersions> GetAllDrafts();

        /// <summary>
        /// Get all logolink versions submitted objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePageLogoLinkVersions> GetAllSubmitted();
    }
}
