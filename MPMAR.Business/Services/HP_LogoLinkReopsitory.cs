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
    public class HP_LogoLinkReopsitory : IHP_LogoLinkReopsitory
    {
        private readonly ApplicationDbContext _db;
        public HP_LogoLinkReopsitory(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<HomePageLogoLink> GetAll()
        {
            return _db.HomePageLogoLinks.ToList();
        }

        public HomePageLogoLink GetById(int id)
        {
            return _db.HomePageLogoLinks.FirstOrDefault(x => x.Id == id);
        }

        public void Update(HomePageLogoLink homePageLogoLink)
        {
            _db.HomePageLogoLinks.Attach(homePageLogoLink);
            _db.Entry(homePageLogoLink).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public HomePageLogoLinkVersions GetByLogoLinkId(int id)
        {
            return _db.HomePageLogoLinks.Where(i => i.Id == id).Select(
                pgMinisty => new HomePageLogoLinkVersions()
                {
                    Id = pgMinisty.Id,
                    EnTitle = pgMinisty.EnTitle,
                    ArTitle = pgMinisty.ArTitle,
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
