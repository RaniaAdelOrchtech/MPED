using Microsoft.EntityFrameworkCore;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MPMAR.Data.Enums.Enums;

namespace MPMAR.Business.Services
{
    public class CityPlanRepository : ICityPlanRepository
    {
        private readonly ApplicationDbContext _db;

        public CityPlanRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Add a new cityplan object
        /// </summary>
        /// <param name="CityPlanItem">city plan model</param>
        /// <returns>added object</returns>
        public CityPlan Add(CityPlan CityPlanItem)
        {
            try
            {
                _db.CityPlan.Add(CityPlanItem);
                _db.SaveChanges();
                return _db.CityPlan.FirstOrDefault(c => c.Id == CityPlanItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// update a cityplan object
        /// </summary>
        /// <param name="CityPlanItem">city plan model</param>
        /// <returns>updated object</returns>
        public CityPlan Update(CityPlan CityPlanItem)
        {
            try
            {
                _db.CityPlan.Update(CityPlanItem);
                _db.SaveChanges();
                return _db.CityPlan.FirstOrDefault(c => c.Id == CityPlanItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// get all cityplan objects
        /// </summary>
        /// <param name="CityPlanItemId">city plan id</param>
        /// <returns>cityplan objets</returns>
        public IEnumerable<CityPlan> GetCityPlanId(int CityPlanItemId)
        {
            var CityPlanItem = _db.CityPlan.OrderBy(s => s.Id).ToList();
            // !(s.IsDeleted && s.PageRouteVersion.StatusId == (int)RequestStatus.Approved) &&
            return CityPlanItem;
        }

        /// <summary>
        /// Delete cityplan object
        /// </summary>
        /// <param name="id">city plan id</param>
        /// <returns>True if deleted false otherwise</returns>
        public bool Delete(int id)
        {
            try
            {
                var item = _db.CityPlan.FirstOrDefault(x => x.Id == id);
                _db.CityPlan.Remove(item);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// get all cityplan objects
        /// </summary>
        /// <returns>all city plan objects</returns>
        public IEnumerable<CityPlan> Get()
        {
         

            return _db.CityPlan;
        }

        /// <summary>
        /// get detail of city plan onject
        /// </summary>
        /// <param name="id">city plan id</param>
        /// <returns>city paln object</returns>
        public CityPlan GetDetail(int id)
        {
            //!(p.IsDeleted && p.PageRouteVersion.Status.Id == (int)RequestStatus.Approved) &&

            return _db.CityPlan.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// get city plan object by id with no tracking for id
        /// <param name="cityPlanId">city plan id</param>
        /// </summary>
        /// <returns>city plan object</returns>
        public CityPlan GetByIdWithNoTracking(int cityPlanId)
        {
            return _db.CityPlan.AsNoTracking().FirstOrDefault(p => p.Id == cityPlanId);
        }
    }
}
