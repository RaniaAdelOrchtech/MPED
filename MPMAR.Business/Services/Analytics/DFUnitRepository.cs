using MPMAR.Analytics.Data;
using MPMAR.Business.Interfaces.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services.Analytics
{
    public class DFUnitRepository : IDFUnitRepository
    {
        private readonly AnalyticsDbContext _db;
        public DFUnitRepository(AnalyticsDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// get df unit by id
        /// </summary>
        /// <param name="id">df unit id</param>
        /// <returns></returns>
        public DFUnit GetByID(int id)
        {
            return _db.DFUnits.FirstOrDefault(x=>x.Id==id);
        }
    }
}
