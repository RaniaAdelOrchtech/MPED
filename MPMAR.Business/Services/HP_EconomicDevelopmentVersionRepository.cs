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
    public class HP_EconomicDevelopmentVersionRepository : IHP_EconomicDevelopmentVersionRepository
    {
        private readonly ApplicationDbContext _db;

        public HP_EconomicDevelopmentVersionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(EconomicDevelopmentVersions model)
        {
            _db.EconomicDevelopmentVersions.Add(model);
            _db.SaveChanges();
        }

        public bool Update(EconomicDevelopmentVersions model)
        {
            try
            {
                _db.EconomicDevelopmentVersions.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<EconomicDevelopmentVersions> GetEcoDevVersions()
        {
            //join between version and non version EconomicDevelopments take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from ed in _db.EconomicDevelopments.Where(d => !d.IsDeleted)
                              from edv in _db.EconomicDevelopmentVersions.Where(d => d.EconomicDevelopmentId == ed.Id && !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new EconomicDevelopmentVersions
                              {
                                  Id = ed.Id,
                                  ArMainTitle = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.ArMainTitle : ed.ArMainTitle,
                                  EnMainTitle = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.EnMainTitle : ed.EnMainTitle,
                                  ArTitle1 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.ArTitle1 : ed.ArTitle1,
                                  EnTitle1 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.EnTitle1 : ed.EnTitle1,
                                  ArDescription1 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.ArDescription1 : ed.ArDescription1,
                                  EnDescription1 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.EnDescription1 : ed.EnDescription1,
                                  Url1 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.Url1 : ed.Url1,
                                  ArTitle2 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.ArTitle2 : ed.ArTitle2,
                                  EnTitle2 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.EnTitle2 : ed.EnTitle2,
                                  ArDescription2 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.ArDescription2 : ed.ArDescription2,
                                  EnDescription2 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.EnDescription2 : ed.EnDescription2,
                                  Url2 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.Url2 : ed.Url2,
                                  ArTitle3 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.ArTitle3 : ed.ArTitle3,
                                  EnTitle3 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.EnTitle3 : ed.EnTitle3,
                                  ArDescription3 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.ArDescription3 : ed.ArDescription3,
                                  EnDescription3 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.EnDescription3 : ed.EnDescription3,
                                  Url3 = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.Url3 : ed.Url3,
                                  IsActive = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.IsActive : ed.IsActive,
                                  IsDeleted = (edv != null && (edv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || edv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? edv.IsDeleted : ed.IsDeleted,
                                  EconomicDevelopmentId = ed.Id,
                                  VersionStatusEnum = edv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = edv.ChangeActionEnum ?? ChangeActionEnum.New
                              });

            return queryright.ToList();
        }

        public EconomicDevelopmentVersions Get(int id)
        {
            return _db.EconomicDevelopmentVersions.FirstOrDefault(i => i.Id == id);
        }

        public EconomicDevelopmentVersions GetByEcoDevId(int id)
        {
            return _db.EconomicDevelopmentVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.EconomicDevelopmentId == id);
        }

        public IEnumerable<EconomicDevelopmentVersions> GetAllDrafts()
        {
            return _db.EconomicDevelopmentVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }

        public IEnumerable<EconomicDevelopmentVersions> GetAllSubmitted()
        {
            return _db.EconomicDevelopmentVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
