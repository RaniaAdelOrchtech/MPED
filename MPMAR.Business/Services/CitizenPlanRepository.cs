using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class CitizenPlanRepository : ICitizenPlanRepository
    {
        private readonly ApplicationDbContext _db;

        public CitizenPlanRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public CitizenPlan Get()
        {
            return _db.CitizenPlan.FirstOrDefault();
        }

        public bool Update(CitizenPlan model)
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

        public CitizenPlanVersions GetByCitizenPlanId(int id)
        {
            return _db.CitizenPlan.Where(i => i.Id == id).Select(
                pgMinisty => new CitizenPlanVersions()
                {
                    Id = pgMinisty.Id,
                    IsActive = pgMinisty.IsActive,
                    IsDeleted = pgMinisty.IsDeleted,
                    EnDescription = pgMinisty.EnDescription,
                    EnTitle = pgMinisty.EnTitle,
                    ArTitle = pgMinisty.ArTitle,
                    ArDescription = pgMinisty.ArDescription,
                    ArMainTitle = pgMinisty.ArMainTitle,
                    EnMainTitle = pgMinisty.EnMainTitle,
                    Image = pgMinisty.Image,
                    EnImage = pgMinisty.EnImage,
                    Link = pgMinisty.Link,
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

