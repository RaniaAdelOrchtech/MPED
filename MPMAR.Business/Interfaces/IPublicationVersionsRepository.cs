using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IPublicationVersionsRepository
    {
        /// <summary>
        /// Add new publication version to database
        /// </summary>
        /// <param name="model">publication version model</param>
        void Add(PublicationVersions model);

        /// <summary>
        /// Update publication version from database
        /// </summary>
        /// <param name="model">publication version new data</param>
        /// <returns></returns>
        bool Update(PublicationVersions model);

        /// <summary>
        /// Get all publication version objects
        /// </summary>
        /// <returns></returns>
        List<PublicationVersions> GetpublicationVersions();

        /// <summary>
        /// Get publication version by id
        /// </summary>
        /// <param name="id">publication version id</param>
        /// <returns></returns>
        PublicationVersions Get(int id);

        /// <summary>
        /// Get publication version by publication id
        /// </summary>
        /// <param name="id">publication id</param>
        /// <returns></returns>
        PublicationVersions GetByPublicationId(int id);

        /// <summary>
        /// Get all drafted publication version objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<PublicationVersions> GetAllDrafts();

        /// <summary>
        /// Get all submitted publication version objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<PublicationVersions> GetAllSubmitted();
    }
}
