using MPMAR.Business.Interfaces;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class FormerMinistriesPageInfoRepository : IFormerMinistriesPageInfoRepository
    {
        private readonly ApplicationDbContext _db;

        public FormerMinistriesPageInfoRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(FormerMinistriesPageInfo model)
        {
            _db.FormerMinistriesPageInfos.Add(model);
            _db.SaveChanges();
        }

        public FormerMinistriesPageInfo Get()
        {
            return _db.FormerMinistriesPageInfos.FirstOrDefault();
        }

        public bool Update(FormerMinistriesPageInfo model)
        {
            try
            {
                _db.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
