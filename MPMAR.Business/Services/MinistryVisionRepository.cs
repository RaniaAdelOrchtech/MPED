using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class MinistryVisionRepository : IMinistryVisionRepository
    {
        private readonly ApplicationDbContext _db;

        public MinistryVisionRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public MinistryVission Get()
        {
            return _db.MinistryVissions.FirstOrDefault();
        }

        public bool Update(MinistryVission model)
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

        public MinistryVissionVersion GetByMinistryVessionId(int id)
        {
            return _db.MinistryVissions.Where(i => i.Id == id).Select(
                pgMinisty => new MinistryVissionVersion()
                {
                    Id = pgMinisty.Id,
                    IsActive = pgMinisty.IsActive,
                    IsDeleted = pgMinisty.IsDeleted,
                    ArTitle = pgMinisty.ArTitle,
                    EnTitle = pgMinisty.EnTitle,
                    ArDescription = pgMinisty.ArDescription,
                    EnDescription = pgMinisty.EnDescription,
                    Link = pgMinisty.Link,
                    ApprovalDate = pgMinisty.ApprovalDate,
                    ApprovedById = pgMinisty.ApprovedById,
                    CreatedById = pgMinisty.CreatedById,
                    CreationDate = pgMinisty.CreationDate,
                    ModificationDate = pgMinisty.ModificationDate,
                    ModifiedById = pgMinisty.ModifiedById,
                    BackGroundImage = pgMinisty.BackGroundImage
                }).FirstOrDefault();
        }
    }
}
