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
    public class CityPlanYearVersionRepository : ICityPlanYearVersionRepository
    {
        private readonly ApplicationDbContext _db;

        public CityPlanYearVersionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// add city plan year version object
        /// </summary>
        /// <param name="cityPlanYear">city plan year version object</param>
        /// <returns>Added object</returns>
        public CityPlanYearVersion Add(CityPlanYearVersion CityPlanYearItem)
        {
            try
            {
                _db.CityPlanYearVersions.Add(CityPlanYearItem);
                _db.SaveChanges();
                //return _db.FooterMenuItem.Include(x => x.PageRouteVersion).FirstOrDefault(c => c.Id == footerMenuItem.Id);
                return _db.CityPlanYearVersions.FirstOrDefault(c => c.Id == CityPlanYearItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// update city plan year version object
        /// </summary>
        /// <param name="CityPlanYearItem">city plan year version object</param>
        /// <returns>single city plan version object</returns>
        public CityPlanYearVersion Update(CityPlanYearVersion CityPlanYearItem)
        {
            try
            {

                _db.CityPlanYearVersions.Update(CityPlanYearItem);
                _db.SaveChanges();

                return _db.CityPlanYearVersions.FirstOrDefault(c => c.Id == CityPlanYearItem.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        /// <summary>
        /// get city plan year versions objects
        /// </summary>
        /// <param name="CityPlanYearItemId">city plan year version id</param>
        /// <returns>all objects of city plan year version</returns>
        public IEnumerable<CityPlanYearVersion> GetCityPlanYearId(int CityPlanYearItemId)
        {
            var CityPlanYearItem = _db.CityPlanYearVersions.OrderBy(s => s.Id).ToList();
            // !(s.IsDeleted && s.PageRouteVersion.StatusId == (int)RequestStatus.Approved) &&
            return CityPlanYearItem;
        }



        /// <summary>
        /// delete city plan year versions objects
        /// </summary>
        /// <param name="id">city plan year version id</param>
        /// <returns>true if deleted false otherwise</returns>
        public bool Delete(int id)
        {
            try
            {
                var item = _db.CityPlanYearVersions.FirstOrDefault(x => x.Id == id);
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
        /// delete city plan year versions objects list
        /// </summary>
        /// <param name="list">city plan year versions list</param>
        /// <returns>true if deleted otherwise false</returns>
        public bool DeleteByCityPlanId(List<CityPlanYearVersion> list)
        {
            try
            {
                _db.CityPlanYearVersions.RemoveRange(list);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// get city plan year versions objects
        /// </summary>
        /// <param name="id">city plan year version id</param>
        /// <returns>city plan year versions objects</returns>
        public IEnumerable<CityPlanYearVersion> Get(int id)
        {

            return _db.CityPlanYearVersions.Where(p => p.CityPlanVersionId == id);
        }

        /// <summary>
        /// get city plan year versions objects
        /// </summary>
        /// <returns>city plan year versions objects</returns>
        public IEnumerable<CityPlanYearVersion> Get()
        {

            return _db.CityPlanYearVersions;
        }

        /// <summary>
        /// get city plan year version object details
        /// </summary>
        /// <param name="id">city plan year version id</param>
        /// <returns>city plan year version object</returns>
        public CityPlanYearVersion GetDetail(int id)
        {

            return _db.CityPlanYearVersions.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// get city plan year versions objects by id
        /// </summary>
        /// <param name="cityPlanVerId">city plan year version id</param>
        /// <returns>list of city plan year versions objects</returns>
        public IEnumerable<CityPlanYearVersion> GetByCityVerId(int cityPlanVerId)
        {

            var cityPlanId = _db.CityPlanVersions.OrderBy(x => x.CreationDate).FirstOrDefault(d => d.Id == cityPlanVerId).CityPlanId;

            //join between version and non version CityPlanYear take the value from non version if
            //the version wasn't exist or was draft or submited
            var queryright = (from pm in _db.CityPlanYear.Where(d => !d.IsDeleted && d.CityPlanId == cityPlanId).DefaultIfEmpty()
                              from pmv in _db.CityPlanYearVersions.Where(d => d.CityPlanYearId == pm.Id && d.VersionStatusEnum != VersionStatusEnum.Ignored && d.CityPlanYearId==pm.Id  && d.CityPlanVersionId == cityPlanVerId)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new CityPlanYearVersion
                              {
                                  CityPlanYearId = pm.Id,
                                  Id = pmv.Id,
                                  CityPlanVersionId = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.CityPlanVersionId : 0,

                                  IsMapActive = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsMapActive : pm.IsMapActive,

                                  ArFileUrl = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.ArFileUrl : pm.ArFileUrl,

                                  EnFileUrl = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.EnFileUrl : pm.EnFileUrl,

                                  GovName = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.DFGov.EnName : pm.DFGov.EnName,

                                  GovYear = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.GovYear : pm.GovYear,


                                  IsActive = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsActive : pm.IsActive,

                                  IsDeleted = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEnum.Submitted)) ? pmv.IsDeleted : pm.IsDeleted,

                                  VersionStatusEnum = pmv.VersionStatusEnum ?? VersionStatusEnum.Draft,
                                  ChangeActionEnum = pmv.ChangeActionEnum ?? ChangeActionEnum.Update
                              });


            //get the rest from CityPlanYearVersions that wasn't included in previous join 
            var queryleft = (from prv in _db.CityPlanYearVersions.Where(d => !d.IsDeleted && d.VersionStatusEnum != VersionStatusEnum.Ignored && d.CityPlanVersionId == cityPlanVerId)
                             where !_db.CityPlanYear.Any(d => d.Id == prv.CityPlanYearId)
                             select new CityPlanYearVersion
                             {
                                 Id = prv.Id,
                                 CityPlanYearId = prv.CityPlanYearId,
                                 CityPlanVersionId = prv.CityPlanVersionId,
                                 EnFileUrl = prv.EnFileUrl,
                                 IsMapActive = prv.IsMapActive,
                                 GovYear = prv.GovYear,
                                 GovName = prv.DFGov.EnName,
                                 ArFileUrl = prv.ArFileUrl,
                                 IsActive = prv.IsActive,
                                 IsDeleted = prv.IsDeleted,
                                 VersionStatusEnum = prv.VersionStatusEnum,
                                 ChangeActionEnum = prv.ChangeActionEnum,

                             });
            return queryright.Union(queryleft).Where(x => !x.IsDeleted).ToList();
        }

        /// <summary>
        /// get drafts city plan year versions objects by id
        /// </summary>
        /// <param name="cityPlanVerId">city plan year version id</param>
        /// <returns>all drafts city plan year versions objects</returns>
        public IEnumerable<CityPlanYearVersion> GetDraftByCityId(int cityPlanVerId)
        {
            return _db.CityPlanYearVersions.AsNoTracking().Where(e => e.VersionStatusEnum == VersionStatusEnum.Draft && e.CityPlanVersionId == cityPlanVerId).ToList();
        }

        /// <summary>
        /// get submitted city plan year versions objects by id
        /// </summary>
        /// <param name="cityPlanVerId">city plan year version id</param>
        /// <returns>all submitted city plan year versions objects</returns>
        public IEnumerable<CityPlanYearVersion> GetSubmitedtByCityId(int cityPlanVerId)
        {
            return _db.CityPlanYearVersions.AsNoTracking().Where(e => e.VersionStatusEnum == VersionStatusEnum.Submitted && e.CityPlanVersionId == cityPlanVerId).ToList();
        }

        /// <summary>
        /// get city plan year version object by id
        /// </summary>
        /// <param name="id">city plan year id</param>
        /// <returns>city plan year version</returns>
        public CityPlanYearVersion GetByCityYearId(int id)
        {
            return _db.CityPlanYearVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.CityPlanYearId == id);
        }

        /// <summary>
        /// get city plan year version object by id and not ignored
        /// </summary>
        /// <param name="id">city plan year id</param>
        /// <returns>city plan year version objects</returns>
        public CityPlanYearVersion GetByCityYearIdNotIgnoren(int id)
        {
            return _db.CityPlanYearVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.CityPlanYearId == id&&i.VersionStatusEnum!=VersionStatusEnum.Ignored);
        }

        /// <summary>
        /// get city plan year version object by id
        /// </summary>
        /// <param name="id">city plan year version id</param>
        /// <returns>city plan year version</returns>
        public CityPlanYearVersion GetByCityPlanVer(int id)
        {
            return _db.CityPlanYearVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.CityPlanVersionId == id);
        }
    }
}
