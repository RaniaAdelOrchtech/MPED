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
    public class MinistryTimeLineRepository : IMinistryTimeLineRepository
    {
        private readonly ApplicationDbContext _db;

        public MinistryTimeLineRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public MinistryTimeLine Add(MinistryTimeLine pageMinistry)
        {
            try
            {
                pageMinistry.StatusId = (int)RequestStatus.Approved;
                _db.MinistryTimeLine.Add(pageMinistry);
                _db.SaveChanges();
   
                return _db.MinistryTimeLine.FirstOrDefault(c => c.Id == pageMinistry.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public MinistryTimeLine Update(MinistryTimeLine pageMinistry)
        {
            try
            {
                
                _db.MinistryTimeLine.Attach(pageMinistry);
                _db.Entry(pageMinistry).State = EntityState.Modified;
                _db.SaveChanges();
                
                return _db.MinistryTimeLine.FirstOrDefault(c => c.Id == pageMinistry.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<MinistryTimeLine> GetMinistryTimeLine()
        {
            var pageMinistrys = _db.MinistryTimeLine.OrderBy(s => s.Order).ToList();
            return pageMinistrys;
        }

        public MinistryTimeLine Delete(int id)
        {
            try
            {
                var item = _db.MinistryTimeLine.FirstOrDefault(x => x.Id == id);
                item.IsDeleted = true;

                _db.MinistryTimeLine.Attach(item);
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                return _db.MinistryTimeLine.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public MinistryTimeLine Get(int id)
        {

            return _db.MinistryTimeLine.FirstOrDefault(p => p.Id == id);
        }
        public MinistryTimeLine GetDetail(int id)
        {

            return _db.MinistryTimeLine.FirstOrDefault(p => p.Id == id);
        }   
        public MinistryTimeLine GetDetailWithNoTarcking(int id)
        {

            return _db.MinistryTimeLine.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public void DeleteAll()
        {
            var allMinistries = _db.MinistryTimeLine;

            foreach (var m in allMinistries)
                m.IsDeleted = true;

            _db.MinistryTimeLine.UpdateRange(allMinistries);
            _db.SaveChanges();
        }

        public IEnumerable<MinistryTimeLine> GetAll()
        {
            return _db.MinistryTimeLine.Where(x => !x.IsDeleted).ToList();
        }
    }
}
