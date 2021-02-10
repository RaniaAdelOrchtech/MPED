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
    public class MonitoringVersionsRepository : IMonitoringVersionsRepository
    {
        private readonly ApplicationDbContext _db;

        public MonitoringVersionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(MonitoringVersions model)
        {
            _db.MonitoringVersions.Add(model);
            _db.SaveChanges();
        }

        public bool Update(MonitoringVersions model)
        {
            try
            {
                _db.MonitoringVersions.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<MonitoringVersions> GetMonitringVersions()
        {

            //join between version and non version Monitoring take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from m in _db.Monitoring.Where(d => !d.IsDeleted)
                              from mv in _db.MonitoringVersions.Where(d => d.MonitoringId == m.Id && !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new MonitoringVersions
                              {
                                  Id = m.Id,
                                  ArMainTitle = (mv != null && (mv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mv.ArMainTitle : m.ArMainTitle,
                                  EnMainTitle = (mv != null && (mv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mv.EnMainTitle : m.EnMainTitle,
                                  BackGroundImage = (mv != null && (mv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mv.BackGroundImage : m.BackGroundImage,
                                  ArDescription1 = (mv != null && (mv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mv.ArDescription1 : m.ArDescription1,
                                  EnDescription1 = (mv != null && (mv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mv.EnDescription1 : m.EnDescription1,
                                  ArTitle2 = (mv != null && (mv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mv.ArTitle2 : m.ArTitle2,
                                  EnTitle2 = (mv != null && (mv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mv.EnTitle2 : m.EnTitle2,
                                  ArDescription2 = (mv != null && (mv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mv.ArDescription2 : m.ArDescription2,
                                  EnDescription2 = (mv != null && (mv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mv.EnDescription2 : m.EnDescription2,

                                  Link1 = (mv != null && (mv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mv.Link1 : m.Link1,
                                  Link2 = (mv != null && (mv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mv.Link2 : m.Link2,

                                  Image1 = (mv != null && (mv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mv.Image1 : m.Image1,

                                  IsActive = (mv != null && (mv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mv.IsActive : m.IsActive,
                                  IsDeleted = (mv != null && (mv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || mv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? mv.IsDeleted : m.IsDeleted,
                                  MonitoringId = m.Id,
                                  VersionStatusEnum = mv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = mv.ChangeActionEnum ?? ChangeActionEnum.New
                              });

            return queryright.ToList();
        }

        public MonitoringVersions Get(int id)
        {
            return _db.MonitoringVersions.FirstOrDefault(i => i.Id == id);
        }

        public MonitoringVersions GetByMonitringId(int id)
        {
            return _db.MonitoringVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.MonitoringId == id);
        }

        public IEnumerable<MonitoringVersions> GetAllDrafts()
        {
            return _db.MonitoringVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }

        public IEnumerable<MonitoringVersions> GetAllSubmitted()
        {
            return _db.MonitoringVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
