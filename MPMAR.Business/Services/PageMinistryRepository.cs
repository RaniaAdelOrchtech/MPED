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
    public class PageMinistryRepository : IPageMinistryRepository
    {
        private readonly ApplicationDbContext _db;

        public PageMinistryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public PageMinistry Add(PageMinistry pageMinistry)
        {
            try
            {
                pageMinistry.StatusId = (int)RequestStatus.Approved;
                _db.PageMinistry.Add(pageMinistry);
                _db.SaveChanges();
                //return _db.PageMinistry.Include(x => x.PageRouteVersion).FirstOrDefault(c => c.Id == pageMinistry.Id);
                return _db.PageMinistry.FirstOrDefault(c => c.Id == pageMinistry.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PageMinistry Update(PageMinistry pageMinistry)
        {
            try
            {
                
                pageMinistry.CreationDate = DateTime.Now;
                pageMinistry.StatusId = (int)RequestStatus.Approved;
                _db.PageMinistry.Attach(pageMinistry);
                _db.Entry(pageMinistry).State = EntityState.Modified;
                _db.SaveChanges();
              
                return _db.PageMinistry.FirstOrDefault(c => c.Id == pageMinistry.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<PageMinistry> GetPageMinistryByPageId(int pageRouteId)
        {

            var pageMinistrys = _db.PageMinistry.Where(s => s.PageRouteId == pageRouteId).OrderBy(s => s.Id).ToList();
            return pageMinistrys;
        }

       

        public bool Delete(int id)
        {
            try
            {
                var item = _db.PageMinistry.FirstOrDefault(x => x.Id == id);
                _db.PageMinistry.Remove(item);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public PageMinistry GetByPageRouteId(int id)
        {

            return _db.PageMinistry.Include(x => x.PageRoute).FirstOrDefault(p => p.PageRouteId == id);
        }
        public PageMinistry GetDetail(int id)
        {

            return _db.PageMinistry.Include(x => x.PageRoute).FirstOrDefault(p => p.Id == id);
        }
    }
}
