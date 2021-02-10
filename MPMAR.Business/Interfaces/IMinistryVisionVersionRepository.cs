using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IMinistryVisionVersionRepository
    {
        /// <summary>
        /// add new MinistryVissionVersion
        /// </summary>
        /// <param name="model"></param>
        void Add(MinistryVissionVersion model);
        /// <summary>
        /// update MinistryVissionVersion
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(MinistryVissionVersion model);
        /// <summary>
        /// get list of MinistryVissionVersion
        /// </summary>
        /// <returns></returns>
        List<MinistryVissionVersion> GetMinistryVessionVersions();
        /// <summary>
        /// get MinistryVissionVersion by id
        /// </summary>
        /// <param name="id">MinistryVissionVersion id</param>
        /// <returns></returns>
        MinistryVissionVersion Get(int id);
        /// <summary>
        /// get MinistryVissionVersion by MinistryVission id
        /// </summary>
        /// <param name="id">MinistryVission id</param>
        /// <returns></returns>
        MinistryVissionVersion GetByMinistryVessionId(int id);
        /// <summary>
        /// get list of all drafts MinistryVissionVersion
        /// </summary>
        /// <returns></returns>
        IEnumerable<MinistryVissionVersion> GetAllDrafts();
        /// <summary>
        /// get list of all submited MinistryVissionVersion
        /// </summary>
        /// <returns></returns>
        IEnumerable<MinistryVissionVersion> GetAllSubmitted();
    }
}
