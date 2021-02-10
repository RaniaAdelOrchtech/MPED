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
    public class SocialMediaRepository : ISocialMediaRepository
    {
        private readonly ApplicationDbContext _db;

        public SocialMediaRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public SocialMedia Add(SocialMedia socialMedia)
        {
            try
            {
                _db.SocialMedia.Add(socialMedia);
                _db.SaveChanges();
       
                return _db.SocialMedia.FirstOrDefault(c => c.Id == socialMedia.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SocialMedia Update(SocialMedia socialMedia)
        {
            try
            {
                _db.SocialMedia.Attach(socialMedia);
                _db.Entry(socialMedia).State = EntityState.Modified;
                _db.SaveChanges();
                return _db.SocialMedia.FirstOrDefault(c => c.Id == socialMedia.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

       
        public IEnumerable<SocialMedia> GetFooterSocialMediaLink()
        {
            var socialMedias = _db.SocialMedia.OrderBy(s => s.Id).ToList();
            return socialMedias;
        }

        
        public bool Delete(int id)
        {
            try
            {
                var item = _db.SocialMedia.FirstOrDefault(x => x.Id == id);
               
                _db.SocialMedia.Remove(item);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public SocialMedia Get(int id)
        {

            return _db.SocialMedia.FirstOrDefault(p => p.Id == id);
        }
        public SocialMedia GetByIdWithNoTracking(int id)
        {

            return _db.SocialMedia.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }
        public SocialMedia GetDetail(int id)
        {

            return _db.SocialMedia.FirstOrDefault(p => p.Id == id);
        }


    }
}
