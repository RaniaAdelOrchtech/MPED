using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class HP_AffiliatesReopsitory : IHP_AffiliatesReopsitory
    {
        private readonly ApplicationDbContext _db;
        public HP_AffiliatesReopsitory(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<HomePageAffiliates> GetAll()
        {
            return _db.HomePageAffiliates.Where(af => !af.IsDeleted).OrderBy(af=>af.Type).ToList();
        }

        public HomePageAffiliates GetById(int id)
        {
            return _db.HomePageAffiliates.FirstOrDefault(x => x.Id == id);
        }
        public HomePageAffiliates GetByIdWithNoTracking(int id)
        {
            return _db.HomePageAffiliates.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
        public void Update(HomePageAffiliates homePageAffiliates)
        {
            _db.HomePageAffiliates.Attach(homePageAffiliates);
            _db.Entry(homePageAffiliates).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Add(HomePageAffiliates homePageAffiliates)
        {
            _db.HomePageAffiliates.Add(homePageAffiliates);
            _db.SaveChanges();
        }

        public bool SoftDelete(int id)
        {
            try
            {
                HomePageAffiliates model = _db.HomePageAffiliates.FirstOrDefault(x => x.Id == id);
                if (model != null)
                {
                    model.IsDeleted = true;

                    Update(model);

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
