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
    public class DFIndicatorRepository : IDFIndicatorRepository
    {
        private readonly AnalyticsDbContext _db;
        public DFIndicatorRepository(AnalyticsDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// get df indicator by id
        /// </summary>
        /// <param name="id">df indicator id</param>
        /// <returns></returns>
        public DFIndicator GetByID(int id)
        {
            var indicator = _db.DFIndicators.Where(i => i.Id == id).FirstOrDefault();
            return indicator;
        }
    }
}
