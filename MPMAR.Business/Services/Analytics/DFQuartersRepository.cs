using MPMAR.Analytics.Data;
using MPMAR.Business.Interfaces.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MPMAR.Business.Services.Analytics
{
  public  class DFQuartersRepository: IDFQuartersRepository
    {
        private readonly AnalyticsDbContext _db;
        public DFQuartersRepository(AnalyticsDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// get all df quarters
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DFQuarter> GetAll()
        {
            var quarters = _db.DFQuarters.Where(x => !x.IsDeleted).OrderBy(x => x.Id).ToList();
            return quarters;
        }
    }
}
