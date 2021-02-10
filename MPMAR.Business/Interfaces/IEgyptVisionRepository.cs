using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IEgyptVisionRepository
    {
        /// <summary>
        /// Add new egypt vision object
        /// </summary>
        /// <param name="albumMaster">egypt vision model</param>
        /// <returns></returns>
        EgyptVision Add(EgyptVision albumMaster);

        /// <summary>
        /// update an egypt vision object
        /// </summary>
        /// <param name="albumMasterItem">egypt vision model</param>
        /// <returns></returns>
        EgyptVision Update(EgyptVision albumMasterItem);
        
        /// <summary>
        /// Get all Egypt Vision
        /// </summary>
        /// <returns></returns>
        IEnumerable<EgyptVision> GetEgyptVisionId();

        /// <summary>
        /// Delete an egypt vision object
        /// </summary>
        /// <param name="id">egypt vision id</param>
        /// <returns></returns>
        EgyptVision Delete(int id);

        /// <summary>
        /// get egypt vision object by id
        /// </summary>
        /// <param name="id">egypt vision id</param>
        /// <returns></returns>
        EgyptVision Get(int id);

        /// <summary>
        /// Get all egypt vision objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<EgyptVision> Get();

        /// <summary>
        /// Details of single egypt vision object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EgyptVision GetDetail(int id);

        /// <summary>
        /// Delete egypt vision object
        /// </summary>
        /// <param name="id">egypt vision id</param>
        /// <returns></returns>
        bool SoftDelete(int id);

        /// <summary>
        /// check if egypt vision object exist ot not
        /// </summary>
        /// <param name="id">egypt vision id</param>
        /// <returns></returns>
        bool ifEgyptVisionExist(int id);

        /// <summary>
        /// Get egypt vision version by egypt vision id
        /// </summary>
        /// <param name="id">egypt vision id</param>
        /// <returns></returns>
        EgyptVisionVersion GetByEgyptVisionId(int id);
    }
}
