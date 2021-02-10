using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class PageNewsTypeRepository : IPageNewsTypeRepository
    {
        private readonly ApplicationDbContext _db;
      
       
        public PageNewsTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
       
        public List<PageNewsType> GetPageNewsTypes()
        {
            return _db.PageNewsType.Where(x=>!(x.IsDeleted)).ToList();
        }

        public PageNewsType Get(int id)
        {
            return _db.PageNewsType.FirstOrDefault(p => !(p.IsDeleted) && p.Id == id);
        }

        public PageNewsType Add(PageNewsType pageNewsType)
        {
            try
            {
                _db.PageNewsType.Add(pageNewsType);
                _db.SaveChanges();
                
                return _db.PageNewsType.FirstOrDefault(c => c.Id == pageNewsType.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PageNewsType Update(PageNewsType pageNewsType)
        {
            try
            {
                var item = _db.PageNewsType.Attach(pageNewsType);
                item.State = EntityState.Modified;
                _db.SaveChanges();
                return _db.PageNewsType.FirstOrDefault(s => s.Id == pageNewsType.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PageNewsType Delete(int id)
        {
            try
            {
                var item = _db.PageNewsType.FirstOrDefault(x => x.Id == id);
                item.IsDeleted = true;
                _db.PageNewsType.Attach(item);
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                return _db.PageNewsType.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
