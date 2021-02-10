using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IEgyptVisionVersionRepository
    {
        /// <summary>
        /// Add egypt vision version object to database
        /// </summary>
        /// <param name="model">egypt vision version data</param>
        /// <returns></returns>
        void Add(EgyptVisionVersion model);

        /// <summary>
        /// update egypt vision version object from database
        /// </summary>
        /// <param name="model">egypt vision version new data</param>
        /// <returns></returns>
        bool Update(EgyptVisionVersion model);

        /// <summary>
        /// Get all egypt vision version objects
        /// </summary>
        /// <returns></returns>
        List<EgyptVisionVersion> GetEgyptVisionVersions();
        
        /// <summary>
        /// Get an egypt vision version object from database
        /// </summary>
        /// <param name="id">egypt vision version object</param>
        /// <returns></returns>
        EgyptVisionVersion Get(int id);

        /// <summary>
        /// get egypt vision version object by egypt vision id
        /// </summary>
        /// <param name="id">egypt vision id</param>
        /// <returns></returns>
        EgyptVisionVersion GetByEgyptVisionId(int id);
        
        /// <summary>
        /// Get all egypt vision versions drafts
        /// </summary>
        /// <returns></returns>
        IEnumerable<EgyptVisionVersion> GetAllDrafts();

        /// <summary>
        /// Get all egypt vision versions submitted
        /// </summary>
        /// <returns></returns>
        IEnumerable<EgyptVisionVersion> GetAllSubmitted();
    }
}
