using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
   public  interface IDFGovRepository
    {
        /// <summary>
        /// Get all DfGov objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<DFGov> GetAll();
    }
}
