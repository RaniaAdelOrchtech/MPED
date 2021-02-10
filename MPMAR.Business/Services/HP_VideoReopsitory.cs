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
    public class HP_VideoReopsitory : IHP_VideoReopsitory
    {
        private readonly ApplicationDbContext _db;
        public HP_VideoReopsitory(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<HomePageVideo> GetAll()
        {
            return _db.homePageVideos.ToList();
        }

        public HomePageVideo GetById(int id)
        {
            return _db.homePageVideos.FirstOrDefault(x => x.Id == id);
        }

        public void Update(HomePageVideo homePageVideo)
        {
            _db.homePageVideos.Attach(homePageVideo);
            _db.Entry(homePageVideo).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public HomePageVideoVersions GetByVideoId(int id)
        {
            return _db.homePageVideos.Where(i => i.Id == id).Select(
                pgMinisty => new HomePageVideoVersions()
                {
                    Id = pgMinisty.Id,
                    IsActive = pgMinisty.IsActive,
                    IsDeleted = pgMinisty.IsDeleted,
                    VideoUrl = pgMinisty.VideoUrl,
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