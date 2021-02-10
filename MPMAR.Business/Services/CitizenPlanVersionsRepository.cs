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
    public class CitizenPlanVersionsRepository : ICitizenPlanVersionsRepository
    {
        private readonly ApplicationDbContext _db;

        public CitizenPlanVersionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(CitizenPlanVersions model)
        {
            _db.CitizenPlanVersions.Add(model);
            _db.SaveChanges();
        }

        public bool Update(CitizenPlanVersions model)
        {
            try
            {
                _db.CitizenPlanVersions.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<CitizenPlanVersions> GetCitizenPlanVersions()
        {
            //join between version and non version citizen plan take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from cp in _db.CitizenPlan.Where(d => !d.IsDeleted)
                              from cpv in _db.CitizenPlanVersions.Where(d => d.CitizenPlanId == cp.Id && !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new CitizenPlanVersions
                              {
                                  Id = cp.Id,
                                  ArMainTitle = (cpv != null && (cpv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || cpv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? cpv.ArMainTitle : cp.ArMainTitle,
                                  EnMainTitle = (cpv != null && (cpv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || cpv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? cpv.EnMainTitle : cp.EnMainTitle,
                                  ArTitle = (cpv != null && (cpv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || cpv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? cpv.ArTitle : cp.ArTitle,
                                  EnTitle = (cpv != null && (cpv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || cpv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? cpv.EnTitle : cp.EnTitle,
                                  ArDescription = (cpv != null && (cpv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || cpv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? cpv.ArDescription : cp.ArDescription,
                                  EnDescription = (cpv != null && (cpv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || cpv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? cpv.EnDescription : cp.EnDescription,
                                  Link = (cpv != null && (cpv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || cpv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? cpv.Link : cp.Link,
                                  Image = (cpv != null && (cpv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || cpv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? cpv.Image : cp.Image,

                                  EnImage = (cpv != null && (cpv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || cpv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? cpv.EnImage : cp.EnImage,

                                  IsActive = (cpv != null && (cpv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || cpv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? cpv.IsActive : cp.IsActive,
                                  IsDeleted = (cpv != null && (cpv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || cpv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? cpv.IsDeleted : cp.IsDeleted,
                                  CitizenPlanId = cp.Id,
                                  VersionStatusEnum = cpv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = cpv.ChangeActionEnum ?? ChangeActionEnum.New,
                              });

            return queryright.ToList();
        }

        public CitizenPlanVersions Get(int id)
        {
            return _db.CitizenPlanVersions.FirstOrDefault(i => i.Id == id);
        }

        public CitizenPlanVersions GetByCitizenPlanId(int id)
        {
            return _db.CitizenPlanVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.CitizenPlanId == id);
        }

        public IEnumerable<CitizenPlanVersions> GetAllDrafts()
        {
            return _db.CitizenPlanVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }

        public IEnumerable<CitizenPlanVersions> GetAllSubmitted()
        {
            return _db.CitizenPlanVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
