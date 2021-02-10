using MPMAR.Business.Interfaces;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class DFGovRepository : IDFGovRepository
    {
        private readonly ApplicationDbContext _db;
        public DFGovRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Get all DfGov objects
        /// </summary>
        /// <returns>all objects of DFGov</returns>
        public IEnumerable<DFGov> GetAll()
        {
            return _db.DFGovs.ToList();
        }
    }
}
