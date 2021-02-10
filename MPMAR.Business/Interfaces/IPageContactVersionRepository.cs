using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IPageContactVersionRepository
    {
        /// <summary>
        /// Add page contact version object to database
        /// </summary>
        /// <param name="model">page contact version model</param>
        void Add(PageContactVersions model);

        /// <summary>
        /// Update page contact version object from database
        /// </summary>
        /// <param name="model">page contact version model new data</param>
        bool Update(PageContactVersions model);

        /// <summary>
        /// Get all page contact version objects
        /// </summary>
        /// <returns></returns>
        List<PageContactVersions> GetPageContactVersions();

        /// <summary>
        /// Get page contact version by id
        /// </summary>
        /// <param name="id">page contact version id</param>
        /// <returns></returns>
        PageContactVersions Get(int id);

        /// <summary>
        /// Get page contact version by page contact id
        /// </summary>
        /// <param name="id">page contact id</param>
        /// <returns></returns>
        PageContactVersions GetByPageContactId(int id);

        /// <summary>
        /// Get all drafts page contact version objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<PageContactVersions> GetAllDrafts();

        /// <summary>
        /// Get all submitted page contact version objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<PageContactVersions> GetAllSubmitted();
    }
}
