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
    public class HP_PhotosReopsitory : IHP_PhotosReopsitory
    {
        private readonly ApplicationDbContext _db;
        public HP_PhotosReopsitory(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<HomePagePhoto> GetAll()
        {
            return _db.homePagePhotos.ToList();
        }

        public HomePagePhoto GetById(int id)
        {
            return _db.homePagePhotos.FirstOrDefault(x => x.Id == id);
        }

        public void Update(HomePagePhoto homePagePhoto)
        {
            _db.homePagePhotos.Attach(homePagePhoto);
            _db.Entry(homePagePhoto).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public HomePagePhotoVersions GetByPhotoId(int id)
        {
            return _db.homePagePhotos.Where(i => i.Id == id).Select(
                pgMinisty => new HomePagePhotoVersions()
                {
                    Id = pgMinisty.Id,
                    IsActive = pgMinisty.IsActive,
                    IsDeleted = pgMinisty.IsDeleted,
                    EnDescription = pgMinisty.EnDescription,
                    EnTitle = pgMinisty.EnTitle,
                    ArTitle = pgMinisty.ArTitle,
                    ArDescription= pgMinisty.ArDescription,
                    ImageUrl = pgMinisty.ImageUrl,
                    Url = pgMinisty.Url,
                    ApprovalDate = pgMinisty.ApprovalDate,
                    ApprovedById = pgMinisty.ApprovedById,
                    CreatedById = pgMinisty.CreatedById,
                    CreationDate = pgMinisty.CreationDate,
                    ModificationDate = pgMinisty.ModificationDate,
                    ModifiedById = pgMinisty.ModifiedById
                }).FirstOrDefault();
        }
    }
}
