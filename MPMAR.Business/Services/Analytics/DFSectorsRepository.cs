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
    public class DFSectorsRepository : IDFSectorsRepository
    {
        private readonly AnalyticsDbContext _db;
        public DFSectorsRepository(AnalyticsDbContext db)
        {
            _db = db;
        }
        public  IEnumerable<DFSector> GetAll()
        {
            var sectors = _db.DFSectors.Where(x=>!x.IsDeleted && x.Type == 1).OrderByDescending(x=>x.NameEn).ToList();
            return sectors;
        }
    }
}
