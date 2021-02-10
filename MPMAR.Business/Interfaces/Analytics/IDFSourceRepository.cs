using MPMAR.Analytics.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces.Analytics
{
    public interface IDFSourceRepository
    {
        /// <summary>
        /// get df source by id
        /// </summary>
        /// <param name="id">df source id</param>
        /// <returns></returns>
        DFSource GetByID(int id);
    }
}
