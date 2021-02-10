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
    public class HP_PhotoSliderReopsitory : IHP_PhotoSliderReopsitory
    {
        private readonly ApplicationDbContext _db;
        public HP_PhotoSliderReopsitory(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<HomePagePhotoSlider> GetAll()
        {
            return _db.homePagePhotoSlider.Where(ps => ps.IsDeleted != true).ToList();
        }

        public bool SoftDelete(int id)
        {
            try
            {
                HomePagePhotoSlider model = _db.homePagePhotoSlider.FirstOrDefault(x => x.Id == id);
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

        public void Add(HomePagePhotoSlider photoSlider)
        {
            _db.homePagePhotoSlider.Add(photoSlider);
            _db.SaveChanges();
        }


        public HomePagePhotoSlider GetById(int id)
        {
            return _db.homePagePhotoSlider.FirstOrDefault(x => x.Id == id);
        }
        public HomePagePhotoSlider GetByIdWithNoTracking(int id)
        {
            return _db.homePagePhotoSlider.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
        public void Update(HomePagePhotoSlider homePagePhotoSlider)
        {
            _db.homePagePhotoSlider.Attach(homePagePhotoSlider);
            _db.Entry(homePagePhotoSlider).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
