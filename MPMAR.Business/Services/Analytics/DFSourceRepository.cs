using MPMAR.Analytics.Data;
using MPMAR.Business.Interfaces.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services.Analytics
{
    public class DFSourceRepository : IDFSourceRepository
    {
        private readonly AnalyticsDbContext _db;
        public DFSourceRepository(AnalyticsDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// get df source by id
        /// </summary>
        /// <param name="id">df source id</param>
        /// <returns></returns>
        public DFSource GetByID(int id)
        {
            return _db.DFSources.FirstOrDefault(x => x.Id == id);
        }
    }
}
