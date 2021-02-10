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
    public class HP_EconomicDevelopmentReopsitory : IHP_EconomicDevelopmentReopsitory
    {
        private readonly ApplicationDbContext _db;
        public HP_EconomicDevelopmentReopsitory(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<EconomicDevelopment> GetAll()
        {
            return _db.EconomicDevelopments.ToList();
        }

        public EconomicDevelopment GetById(int id)
        {
            return _db.EconomicDevelopments.FirstOrDefault(x => x.Id == id);
        }

        public void Update(EconomicDevelopment economicDevelopment)
        {
            _db.EconomicDevelopments.Attach(economicDevelopment);
            _db.Entry(economicDevelopment).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public EconomicDevelopmentVersions GetByEcoDevId(int id)
        {
            return _db.EconomicDevelopments.Where(i => i.Id == id).Select(
                pgMinisty => new EconomicDevelopmentVersions()
                {
                    Id = pgMinisty.Id,
                    IsActive = pgMinisty.IsActive,
                    IsDeleted = pgMinisty.IsDeleted,
                    ApprovalDate = pgMinisty.ApprovalDate,
                    ApprovedById = pgMinisty.ApprovedById,
                    CreatedById = pgMinisty.CreatedById,
                    CreationDate = pgMinisty.CreationDate,
                    ModificationDate = pgMinisty.ModificationDate,
                    ModifiedById = pgMinisty.ModifiedById,
                    ArMainTitle = pgMinisty.ArMainTitle,
                    EnMainTitle = pgMinisty.EnMainTitle,
                    EnDescription1 = pgMinisty.EnDescription1,
                    ArDescription1 = pgMinisty.ArDescription1,
                    EnDescription2 = pgMinisty.EnDescription2,
                    ArDescription2 = pgMinisty.ArDescription2,
                    EnDescription3 = pgMinisty.EnDescription3,
                    ArDescription3 = pgMinisty.ArDescription3,
                    ArTitle1 = pgMinisty.ArTitle1,
                    ArTitle2 = pgMinisty.ArTitle2,
                    ArTitle3 = pgMinisty.ArTitle3,
                    EnTitle1 = pgMinisty.EnTitle1,
                    EnTitle2 = pgMinisty.EnTitle2,
                    EnTitle3 = pgMinisty.EnTitle3,
                    Url1 = pgMinisty.Url1,
                    Url2 = pgMinisty.Url2,
                    Url3 = pgMinisty.Url3,
                    BackGroundImage = pgMinisty.BackGroundImage
                }).FirstOrDefault();
        }
    }
}
