using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class PublicationRepository : IPublicationRepository
    {
        private readonly ApplicationDbContext _db;

        public PublicationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(Publication publication)
        {
            _db.Publications.Add(publication);
            _db.SaveChanges();
        }

        public IEnumerable<Publication> GetAll()
        {
            return _db.Publications.Where(x=>!x.IsDeleted).ToList();
        }

        public Publication GetById(int id)
        {
            return _db.Publications.Find(id);
        }

        public void Update(Publication publication)
        {
            _db.Publications.Update(publication);
            _db.SaveChanges();
        }

        public PublicationVersions GetByPublicationId(int id)
        {
            return _db.Publications.Where(i => i.Id == id).Select(
                pgMinisty => new PublicationVersions()
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
                    Link1 = pgMinisty.Link1,
                    Link2= pgMinisty.Link2,
                    Link3 = pgMinisty.Link3,
                    Image1 = pgMinisty.Image1,
                    Image2 = pgMinisty.Image2,
                    Image3 = pgMinisty.Image3,
                }).FirstOrDefault();
        }
    }
}
