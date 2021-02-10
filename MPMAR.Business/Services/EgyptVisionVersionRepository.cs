using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class EgyptVisionVersionRepository : IEgyptVisionVersionRepository
    {
        private readonly ApplicationDbContext _db;

        public EgyptVisionVersionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add egypt vision version object to database
        /// </summary>
        /// <param name="model">egypt vision version data</param>
        /// <returns></returns>
        public void Add(EgyptVisionVersion model)
        {
            _db.EgyptVisionVersions.Add(model);
            _db.SaveChanges();
        }

        /// <summary>
        /// update egypt vision version object from database
        /// </summary>
        /// <param name="model">egypt vision version new data</param>
        /// <returns>true if update false otherwise</returns>
        public bool Update(EgyptVisionVersion model)
        {
            try
            {
                _db.EgyptVisionVersions.Update(model);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Get all egypt vision version objects
        /// </summary>
        /// <returns>list of egypt vision version objects</returns>
        public List<EgyptVisionVersion> GetEgyptVisionVersions()
        {
            //join between version and non version EgyptVision take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from ev in _db.EgyptVision.Where(d => !d.IsDeleted)
                              from evv in _db.EgyptVisionVersions.Where(d => d.EgyptVisionId == ev.Id && !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new EgyptVisionVersion
                              {
                                  Id = ev.Id,
                                  PageRouteVersionId = evv.Id,
                                  EnEgyptVisionName = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.EnEgyptVisionName : ev.EnEgyptVisionName,
                                  ArEgyptVisionName = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.ArEgyptVisionName : ev.ArEgyptVisionName,
                                  EnEgyptVisionSmallDesc = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.EnEgyptVisionSmallDesc : ev.EnEgyptVisionSmallDesc,
                                  ArEgyptVisionSmallDesc = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.ArEgyptVisionSmallDesc : ev.ArEgyptVisionSmallDesc,
                                  EnEgyptVisionDesc = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.EnEgyptVisionDesc : ev.EnEgyptVisionDesc,
                                  ArEgyptVisionDesc = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.ArEgyptVisionDesc : ev.ArEgyptVisionDesc,
                                  StatusId = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.StatusId : ev.StatusId,
                                  EnImagePath = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.EnImagePath : ev.EnImagePath,
                                  ArImagePath = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.ArImagePath : ev.ArImagePath,
                                  BgColor = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.BgColor : ev.BgColor,
                                  LineColor = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.LineColor : ev.LineColor,
                                  ImagePositionIsRight = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.ImagePositionIsRight : ev.ImagePositionIsRight,
                                  Order = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.Order : ev.Order,
                                  IsActive = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.IsActive : ev.IsActive,
                                  IsDeleted = (evv != null && (evv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || evv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? evv.IsDeleted : ev.IsDeleted,
                                  EgyptVisionId = ev.Id,
                                  VersionStatusEnum = evv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = evv.ChangeActionEnum ?? ChangeActionEnum.New
                              });

            return queryright.ToList();
        }

        /// <summary>
        /// Get an egypt vision version object from database
        /// </summary>
        /// <param name="id">egypt vision version object</param>
        /// <returns>egypt vision version object</returns>
        public EgyptVisionVersion Get(int id)
        {
            return _db.EgyptVisionVersions.FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// get egypt vision version object by egypt vision id
        /// </summary>
        /// <param name="id">egypt vision id</param>
        /// <returns>egypt vision version object</returns>
        public EgyptVisionVersion GetByEgyptVisionId(int id)
        {
            return _db.EgyptVisionVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.EgyptVisionId == id);
        }

        /// <summary>
        /// Get all egypt vision versions drafts
        /// </summary>
        /// <returns>IEnumerable of egypt vision versions drafts</returns>
        public IEnumerable<EgyptVisionVersion> GetAllDrafts()
        {
            return _db.EgyptVisionVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft).ToList();
        }

        /// <summary>
        /// Get all egypt vision versions submitted
        /// </summary>
        /// <returns>IEnumerable of egypt vision versions submitted</returns>
        public IEnumerable<EgyptVisionVersion> GetAllSubmitted()
        {
            return _db.EgyptVisionVersions.Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted).ToList();
        }
    }
}
