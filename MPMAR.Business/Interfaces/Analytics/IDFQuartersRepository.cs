using MPMAR.Analytics.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces.Analytics
{
   public interface IDFQuartersRepository
    {
        /// <summary>
        /// get all df quarters
        /// </summary>
        /// <returns></returns>
        IEnumerable<DFQuarter> GetAll();
    }
}
