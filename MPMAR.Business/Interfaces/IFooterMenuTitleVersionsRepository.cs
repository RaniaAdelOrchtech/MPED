using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IFooterMenuTitleVersionsRepository
    {
        /// <summary>
        /// Add footer menu title version to database
        /// </summary>
        /// <param name="model">footer menu title version</param>
        /// <returns></returns>
        void Add(FooterMenuTitleVersions model);

        /// <summary>
        /// Update fotter menu title version from database
        /// </summary>
        /// <param name="model">footer menu title version new data</param>
        /// <returns></returns>
        bool Update(FooterMenuTitleVersions model);

        /// <summary>
        /// Get all footer menu title versions
        /// </summary>
        /// <returns></returns>
        List<FooterMenuTitleVersions> GetFoorterMenuTitleVersions();

        /// <summary>
        /// Get footer menu title version object by id
        /// </summary>
        /// <param name="id">footer menu title version id</param>
        /// <returns></returns>
        FooterMenuTitleVersions Get(int id);

        /// <summary>
        /// get footer menu title version by footer menu title id
        /// </summary>
        /// <param name="id">footer menu title id</param>
        /// <returns></returns>
        FooterMenuTitleVersions GetByFooterMenuTitleId(int id);

        /// <summary>
        /// Get all drafts objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<FooterMenuTitleVersions> GetAllDrafts();

        /// <summary>
        /// Get all submitted objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<FooterMenuTitleVersions> GetAllSubmitted();
    }
}
