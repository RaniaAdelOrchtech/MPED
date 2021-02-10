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
    public class CityPlanYearRepository : ICityPlanYearRepository
    {
        private readonly ApplicationDbContext _db;

        public CityPlanYearRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// add city plan year object
        /// </summary>
        /// <param name="CityPlanYearItem">city plan year object</param>
        /// <returns>added object</returns>
        public CityPlanYear Add(CityPlanYear CityPlanYearItem)
        {
            try
            {
                _db.CityPlanYear.Add(CityPlanYearItem);
                _db.SaveChanges();
                //return _db.FooterMenuItem.Include(x => x.PageRouteVersion).FirstOrDefault(c => c.Id == footerMenuItem.Id);
                return _db.CityPlanYear.FirstOrDefault(c => c.Id == CityPlanYearItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// update city plan year object
        /// </summary>
        /// <param name="cityPlanYearItem">city plan year object</param>
        /// <returns>updated object</returns>
        public CityPlanYear Update(CityPlanYear CityPlanYearItem)
        {
            try
            {
                _db.CityPlanYear.Attach(CityPlanYearItem);
                _db.Entry(CityPlanYearItem).State = EntityState.Modified;
                _db.SaveChanges();
     
                return _db.CityPlanYear.FirstOrDefault(c => c.Id == CityPlanYearItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        /// <summary>
        /// get city plan year objects by id
        /// </summary>
        /// <param name="cityPlanYearId">city plan year id</param>
        /// <returns>all city plan years objects</returns>
        public IEnumerable<CityPlanYear> GetCityPlanYearId(int CityPlanYearItemId)
        {
            var CityPlanYearItem = _db.CityPlanYear.OrderBy(s => s.Id).ToList();
            // !(s.IsDeleted && s.PageRouteVersion.StatusId == (int)RequestStatus.Approved) &&
            return CityPlanYearItem;
        }

 

        /// <summary>
        /// delete city plan year object by id
        /// </summary>
        /// <param name="id">city plan year id</param>
        /// <returns>true if deleted false otherwise</returns>
        public bool Delete(int id)
        {
            try
            {
                var item = _db.CityPlanYear.FirstOrDefault(x => x.Id == id);
                //item.IsActive = false;


                _db.CityPlanYear.Remove(item);
                //Attach(item);
                //_db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                //   return _db.FooterMenuItem.Include(x => x.PageRouteVersion).FirstOrDefault(c => c.Id == id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// delete list of city plan year objects
        /// </summary>
        /// <param name="list">city plan year list</param>
        /// <returns>true if delete false otherwise</returns>
        public bool DeleteByCityPlanId(List<CityPlanYear> list)
        {
            try
            {
                _db.CityPlanYear.RemoveRange(list);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// get all city plan year objects with id
        /// </summary>
        /// <param name="id">city plan year id</param>
        /// <returns>all city plan objects</returns>
        public IEnumerable<CityPlanYear> Get(int id)
        {
            //!(p.IsDeleted && p.PageRouteVersion.Status.Id == (int)RequestStatus.Approved) &&

            return _db.CityPlanYear.Where(p => p.CityPlanId == id);
        }

        /// <summary>
        /// get all city plan year objects
        /// </summary>
        /// <returns>all city plan objects</returns>
        public IEnumerable<CityPlanYear> Get()
        {

            return _db.CityPlanYear;
        }

        /// <summary>
        /// get details city plan year objects with id
        /// </summary>
        /// <param name="id">city plan year id</param>
        /// <returns>single city plan year object</returns>
        public CityPlanYear GetDetail(int id)
        {

            return _db.CityPlanYear.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// get city plan year object with no tracking for id
        /// </summary>
        /// <param name="cityPlanYearId">city plan year id</param>
        /// <returns>single city plan year object</returns>
        public CityPlanYear GetByIdWithNoTracking(int cityPlanYearId)
        {
            return _db.CityPlanYear.AsNoTracking().FirstOrDefault(p => p.Id == cityPlanYearId);
        }
    }
}
