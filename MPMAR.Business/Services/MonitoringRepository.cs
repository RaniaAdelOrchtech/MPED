using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class MonitoringRepository : IMonitoringRepository
    {
        private readonly ApplicationDbContext _db;

        public MonitoringRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public Monitoring Get()
        {
            return _db.Monitoring.FirstOrDefault(x => x.IsActive && !x.IsDeleted);
        }
        public void Update(Monitoring monitoring)
        {
            _db.Monitoring.Update(monitoring);
            _db.SaveChanges();
        }

        public MonitoringVersions GetByMonitringId(int id)
        {
            return _db.Monitoring.Where(i => i.Id == id).Select(
                    pgMinisty => new MonitoringVersions()
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
                        ArTitle2 = pgMinisty.ArTitle2,
                        BackGroundImage = pgMinisty.BackGroundImage,
                        EnTitle2 = pgMinisty.EnTitle2,
                        Image1 = pgMinisty.Image1,
                        Link1 = pgMinisty.Link1,
                        Link2 = pgMinisty.Link2
                    }).FirstOrDefault();
        }
    }
}
