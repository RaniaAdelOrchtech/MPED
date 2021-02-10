using Microsoft.EntityFrameworkCore;
using MPMAR.Analytics.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MPMAR.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MPMAR.Business.Services
{
    public class DFYearsRepository : IDFYearsRepository
    {
        private readonly AnalyticsDbContext _db;
        public DFYearsRepository(AnalyticsDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// get all df years
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DFYear> GetAll()
        {
            var years = _db.DFYears.Where(x=>!x.IsDeleted).OrderByDescending(x=>x.Order).ToList();
            return years;
        }
    }
}
