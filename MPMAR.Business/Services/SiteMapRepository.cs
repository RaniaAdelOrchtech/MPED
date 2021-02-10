using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Business.Services
{
    public class SiteMapRepository : ISiteMapRepository
    {
        private readonly ApplicationDbContext _db;

        public SiteMapRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public SiteMap Add(SiteMap SiteMap)
        {
            try
            {
                _db.SiteMap.Add(SiteMap);
                _db.SaveChanges();
                return _db.SiteMap.FirstOrDefault(c => c.Id == SiteMap.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SiteMap Update(SiteMap SiteMap)
        {
            try
            {
               
                _db.SiteMap.Attach(SiteMap);
                _db.Entry(SiteMap).State = EntityState.Modified;
                _db.SaveChanges();
                
                return _db.SiteMap.FirstOrDefault(c => c.Id == SiteMap.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

      
        public bool Delete(int id)
        {
            try
            {
                var item = _db.SiteMap.FirstOrDefault(x => x.Id == id);
               
                _db.SiteMap.Remove(item);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public SiteMap Get(int id)
        {

            return _db.SiteMap.FirstOrDefault(p => p.Id == id);
        }
        public SiteMap GetDetail(int id)
        {

            return _db.SiteMap.FirstOrDefault(p => p.Id == id);
        }


    }
}
