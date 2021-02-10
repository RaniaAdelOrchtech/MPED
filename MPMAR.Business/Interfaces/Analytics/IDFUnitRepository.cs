using MPMAR.Analytics.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces.Analytics
{
    public interface IDFUnitRepository
    {
        /// <summary>
        /// get df unit by id
        /// </summary>
        /// <param name="id">df unit id</param>
        /// <returns></returns>
        DFUnit GetByID(int id);
    }
}
