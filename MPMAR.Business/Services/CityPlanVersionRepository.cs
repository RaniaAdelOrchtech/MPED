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
    public class CityPlanVersionRepository : ICityPlanVersionRepository
    {
        private readonly ApplicationDbContext _db;

        public CityPlanVersionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Adding a new city plan version
        /// </summary>
        /// <param name="cityPlanMaster">city plan version model</param>
        /// <returns>added object</returns>
        public CityPlanVersion Add(CityPlanVersion CityPlanItem)
        {
            try
            {
                _db.CityPlanVersions.Add(CityPlanItem);
                _db.SaveChanges();
                return _db.CityPlanVersions.FirstOrDefault(c => c.Id == CityPlanItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// update a city plan version
        /// </summary>
        /// <param name="CityPlanItem">city plan version model</param>
        /// <returns>updated object</returns>
        public CityPlanVersion Update(CityPlanVersion CityPlanItem)
        {
            try
            {
                _db.CityPlanVersions.Update(CityPlanItem);
                _db.SaveChanges();
                return _db.CityPlanVersions.FirstOrDefault(c => c.Id == CityPlanItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// get city plan versions objects
        /// </summary>
        /// <param name="CityPlanItemId">city plan version id</param>
        /// <returns>all objects from city plan version type</returns>
        public IEnumerable<CityPlanVersion> GetCityPlanId(int CityPlanItemId)
        {
            var CityPlanItem = _db.CityPlanVersions.OrderBy(s => s.Id).ToList();
            return CityPlanItem;
        }

        /// <summary>
        /// delete city plan version object
        /// </summary>
        /// <param name="id">city plan version id</param>
        /// <returns>true if deleted false otherwise</returns>
        public bool Delete(int id)
        {
            try
            {
                var item = _db.CityPlanVersions.FirstOrDefault(x => x.Id == id);
                item.IsDeleted = true;
                Update(item);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// get city plan versions objects
        /// </summary>
        /// <returns>city plan versions objects</returns>
        public IEnumerable<CityPlanVersion> Get()
        {
            //join between version and non version CityPlan take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from pm in _db.CityPlan.Where(d => !d.IsDeleted).DefaultIfEmpty()
                              from pmv in _db.CityPlanVersions.Where(d => d.CityPlanId == pm.Id)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new CityPlanVersion
                              {
                                  CityPlanId = pm.Id,
                                  Id = pmv.Id,
                                  ArPageDescription = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ArPageDescription : pm.ArPageDescription,

                                  EnPageDescription = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.EnPageDescription : pm.EnPageDescription,

                                  IsActive = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsActive : pm.IsActive,

                                  IsDeleted = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsDeleted : pm.IsDeleted,

                                  VersionStatusEnum = pmv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = pmv.ChangeActionEnum ?? ChangeActionEnum.Update
                              });



            //get the rest from CityPlanVersions that wasn't included in previous join 
            var queryleft = (from prv in _db.CityPlanVersions.Where(d => !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored)
                             where !_db.CityPlan.Any(d => d.Id == prv.CityPlanId)
                             select new CityPlanVersion
                             {
                                 Id = prv.Id,
                                 CityPlanId = prv.CityPlanId,
                                 ArPageDescription = prv.ArPageDescription,
                                 EnPageDescription = prv.EnPageDescription,
                                 IsActive = prv.IsActive,
                                 IsDeleted = prv.IsDeleted,
                                 VersionStatusEnum = prv.VersionStatusEnum,
                                 ChangeActionEnum = prv.ChangeActionEnum,

                             });
            return queryright.Union(queryleft).Where(x => !x.IsDeleted).ToList();
        }

        /// <summary>
        /// get detail city plan version object
        /// </summary>
        /// <param name="id">city plan version id</param>
        /// <returns>single city plan version object</returns>
        public CityPlanVersion GetDetail(int id)
        {

            return _db.CityPlanVersions.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// get draft city plan version object
        /// </summary>
        /// <param name="cityPlanId">city plan version id</param>
        /// <returns>single city plan version object</returns>
        public CityPlanVersion GetDraftByCityId(int cityPlanId)
        {
            return _db.CityPlanVersions.OrderByDescending(x => x.Id).FirstOrDefault(e => e.CityPlanId == cityPlanId && e.VersionStatusEnum == VersionStatusEnum.Draft);
        }

        /// <summary>
        /// get subbmitted city plan version object
        /// </summary>
        /// <param name="cityPlanId">city plan version id</param>
        /// <returns>single city plan version object</returns>
        public CityPlanVersion GetSubmitedByCityId(int cityPlanId)
        {
            return _db.CityPlanVersions.OrderByDescending(x => x.Id).FirstOrDefault(e => e.CityPlanId == cityPlanId && e.VersionStatusEnum == VersionStatusEnum.Submitted);
        }

        /// <summary>
        /// get city plan version object
        /// </summary>
        /// <param name="cityId">city plan id</param>
        /// <returns>single city plan version object</returns>
        public CityPlanVersion GetByCityId(int cityId)
        {
            return _db.CityPlanVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.CityPlanId == cityId);
        }
    }
}
