using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Enums;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class MinistryVisionVersionRepository : IMinistryVisionVersionRepository
    {
        private readonly ApplicationDbContext _db;

        public MinistryVisionVersionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(MinistryVissionVersion model)
        {
            _db.MinistryVissionVersions.Add(model);
            _db.SaveChanges();
        }

        public bool Update(MinistryVissionVersion model)
        {
            try
            {
                _db.MinistryVissionVersions.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<MinistryVissionVersion> GetMinistryVessionVersions()
        {

            //join between version and non version Ministry Vission take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from mv in _db.MinistryVissions.Where(d => !d.IsDeleted)
                              from mvv in _db.MinistryVissionVersions.Where(d => d.MinistryVissionId == mv.Id && !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new MinistryVissionVersion
                              {
                                  Id = mv.Id,
                                  EnTitle = (mvv != null && (mvv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mvv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mvv.EnTitle : mv.EnTitle,
                                  ArTitle = (mvv != null && (mvv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mvv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mvv.ArTitle : mv.ArTitle,
                                  ArDescription = (mvv != null && (mvv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mvv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mvv.ArDescription : mv.ArDescription,
                                  EnDescription = (mvv != null && (mvv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mvv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mvv.EnDescription : mv.EnDescription,
                                  Link = (mvv != null && (mvv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mvv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mvv.Link : mv.Link,
                                  IsActive = (mvv != null && (mvv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mvv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mvv.IsActive : mv.IsActive,
                                  IsDeleted = (mvv != null && (mvv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mvv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mvv.IsDeleted : mv.IsDeleted,
                                  BackGroundImage = (mvv != null && (mvv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mvv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mvv.BackGroundImage : mv.BackGroundImage,
                                  MinistryVissionId = mv.Id,
                                  VersionStatusEnum = mvv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = mvv.ChangeActionEnum ?? ChangeActionEnum.New
                              });

            return queryright.ToList();
        }

        public MinistryVissionVersion Get(int id)
        {
            return _db.MinistryVissionVersions.FirstOrDefault(i => i.Id == id);
        }

        public MinistryVissionVersion GetByMinistryVessionId(int id)
        {
            return _db.MinistryVissionVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.MinistryVissionId == id);
        }

        public IEnumerable<MinistryVissionVersion> GetAllDrafts()
        {
            return _db.MinistryVissionVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }

        public IEnumerable<MinistryVissionVersion> GetAllSubmitted()
        {
            return _db.MinistryVissionVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
