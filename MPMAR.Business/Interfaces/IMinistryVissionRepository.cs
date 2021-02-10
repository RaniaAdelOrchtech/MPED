using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
   public interface IMinistryVisionRepository
    {
        /// <summary>
        /// get first MinistryVission if exists null otherwise
        /// </summary>
        /// <returns></returns>
        MinistryVission Get();
        /// <summary>
        /// update MinistryVission 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(MinistryVission model);
        /// <summary>
        /// get MinistryVissionVersion by MinistryVission id
        /// </summary>
        /// <param name="id">MinistryVission id</param>
        /// <returns></returns>
        MinistryVissionVersion GetByMinistryVessionId(int id);
    }
}
