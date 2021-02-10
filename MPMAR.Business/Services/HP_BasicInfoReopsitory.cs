using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class HP_BasicInfoReopsitory : IHP_BasicInfoReopsitory
    {
        private readonly ApplicationDbContext _db;
        public HP_BasicInfoReopsitory(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<HomePageBasicInfo> GetAll()
        {
            return _db.HomePageBasicInfo.ToList();
        }

        public HomePageBasicInfo GetById(int id)
        {
            return _db.HomePageBasicInfo.FirstOrDefault(x => x.Id == id);
        }

        public void Update(HomePageBasicInfo homeBasicInfo)
        {
            try
            {
                _db.HomePageBasicInfo.Update(homeBasicInfo);
                _db.SaveChanges();
            }
            catch { }
        }
    }
}
