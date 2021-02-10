using Microsoft.EntityFrameworkCore;
using MPMAR.Analytics.Data;
using MPMAR.Business.Interfaces.Analytics;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using MPMAR.Analytics.Data.Enums;
using MPMAR.Analytics.Data.Models;

namespace MPMAR.Business.Services.Analytics
{
    public class ComponentConstantRepository : IComponentConstantRepository
    {
        private readonly AnalyticsDbContext _db;

        public ComponentConstantRepository(AnalyticsDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// add new component constatnt
        /// </summary>
        /// <param name="componentConstant"></param>
        public void Add(ComponentConstant component)
        {
            _db.ComponentConstants.Add(component);
            _db.SaveChanges();
        }

        public void AddVer(ComponentConstantVersion component)
        {
            _db.ComponentConstantVersions.Add(component);
            _db.SaveChanges();
        }

        /// <summary>
        /// delete constant component by id
        /// </summary>
        /// <param name="id">component id</param>
        /// <returns>true if deleted successfully false otherwise</returns>
        public bool Delete(int id)
        {

            try
            {
                var model = GetById(id);
                if (model != null)
                {
                    model.IsDeleted = true;

                    Update(model);

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }



        /// <summary>
        /// get all component constant in some range
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="sortColumnName">coulmn name to sort data depend on it</param>
        /// <param name="sortDirection">descending(desc) or ascending(asc)</param>
        /// <param name="start">strarting page</param>
        /// <param name="lenght">number of rows in the page</param>
        /// <param name="totalCount">total rows count</param>
        /// <returns>IEnumerable of component view model</returns>
        public IEnumerable<ComponentViewModel> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount, string role = "")
        {
            var notApproval = string.IsNullOrWhiteSpace(role);

            var queryright = (from pm in _db.ComponentConstants.Where(d => !(d.IsDeleted ?? false)).DefaultIfEmpty()
                              from pmv in _db.ComponentConstantVersions.Where(d => d.ComponentConstantId == pm.Id && d.VersionStatusEnum != VersionStatusEIEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new ComponentViewModel
                              {
                                  ComponentConstantId = pm.Id,
                                  IsVersion = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)),
                                  Id = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.Id : pm.Id,
                                  Indicator = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFIndicator.NameEn : pm.DFIndicator.NameEn,
                                  Quarter = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFQuarter.NameEn : pm.DFQuarter.NameEn,

                                  Source = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFSource.NameEn : pm.DFSource.NameEn,

                                  Unit = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFUnit.NameEn : pm.DFUnit.NameEn,

                                  ExportsOfGoodsAndServices = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.ExportsOfGoodsAndServices : pm.ExportsOfGoodsAndServices,

                                  GovernmentConsumption = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.GovernmentConsumption : pm.GovernmentConsumption,

                                  GrossCapitalFormation = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.GrossCapitalFormation : pm.GrossCapitalFormation,

                                  ImportsOfGoodsAndServices = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.ImportsOfGoodsAndServices : pm.ImportsOfGoodsAndServices,

                                  PrivateConsumption = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.PrivateConsumption : pm.PrivateConsumption,

                                  TotalGrossDomesticProductAtMarketPrices = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.TotalGrossDomesticProductAtMarketPrices : pm.TotalGrossDomesticProductAtMarketPrices,

                                  YearFiscal = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFYearFiscal.NameEn : pm.DFYearFiscal.NameEn,


                                  IsDeleted = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.IsDeleted ?? false : pm.IsDeleted ?? false,


                                  VersionStatusEnum = pmv.VersionStatusEnum ?? VersionStatusEIEnum.Approved,
                                  ChangeActionEnum = pmv.ChangeActionEnum ?? ChangeActionEIEnum.New,

                                  CreatedById = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.CreatedById : "",
                              });


            //get the rest from HomePageAffiliatesVersions that wasn't included in previous join 
            var queryleft = (from prv in _db.ComponentConstantVersions.Where(d => d.VersionStatusEnum != VersionStatusEIEnum.Ignored)
                             where !_db.ComponentConstants.Any(d => d.Id == prv.ComponentConstantId)
                             select new ComponentViewModel
                             {
                                 Id = prv.Id,
                                 IsVersion = true,
                                 VersionStatusEnum = prv.VersionStatusEnum,
                                 ChangeActionEnum = prv.ChangeActionEnum,
                                 ComponentConstantId = prv.ComponentConstantId,
                                 IsDeleted = prv.IsDeleted ?? false,
                                 ExportsOfGoodsAndServices = prv.ExportsOfGoodsAndServices,
                                 GrossCapitalFormation = prv.GrossCapitalFormation,
                                 GovernmentConsumption = prv.GovernmentConsumption,
                                 ImportsOfGoodsAndServices = prv.ImportsOfGoodsAndServices,
                                 Indicator = prv.DFIndicator.NameEn,
                                 PrivateConsumption = prv.PrivateConsumption,
                                 Quarter = prv.DFQuarter.NameEn,
                                 Source = prv.DFSource.NameEn,
                                 TotalGrossDomesticProductAtMarketPrices = prv.TotalGrossDomesticProductAtMarketPrices,
                                 Unit = prv.DFUnit.NameEn,
                                 YearFiscal = prv.DFYearFiscal.NameEn,
                                 CreatedById = prv.CreatedById
                             });


            IQueryable<ComponentViewModel> componentData;
            if (string.IsNullOrWhiteSpace(role))
                componentData = queryright.Union(queryleft);
            else
                componentData = queryright.Union(queryleft).Where(x => x.VersionStatusEnum == VersionStatusEIEnum.Submitted);

            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                componentData = componentData.Where(x =>
                    x.Quarter.ToLower().Contains(searchValue.ToLower()) ||
                    x.YearFiscal.ToLower().Contains(searchValue.ToLower())
                    );
            }
            totalCount = componentData.Count();

            if (string.IsNullOrWhiteSpace(sortColumnName))
            {
                componentData = componentData.OrderByDescending(x => x.YearFiscal).ThenBy(x => x.Quarter);
            }
            else if (sortDirection == "asc")
                componentData = componentData.OrderBy($"{sortColumnName} asc");
            else if (sortDirection == "desc")
                componentData = componentData
                    .OrderBy($"{sortColumnName} descending");

            //paging
            return componentData.Skip(start).Take(lenght).ToList();
        }

        public IEnumerable<ComponentConstantVersion> GetAllSubmited()
        {
            return _db.ComponentConstantVersions.Where(x => x.VersionStatusEnum == VersionStatusEIEnum.Submitted);
        }

        public ComponentConstantVersion GetByComponentConstId(int id)
        {
            return _db.ComponentConstantVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.ComponentConstantId == id);
        }

        /// <summary>
        /// get specific component const by id
        /// </summary>
        /// <param name="id">component id</param>
        /// <returns></returns>
        public ComponentConstant GetById(int id)
        {
            return _db.ComponentConstants.FirstOrDefault(x => x.Id == id);
        }

        public ComponentConstantVersion GetVerById(int id, bool disableTracking = true)
        {
            return disableTracking ? _db.ComponentConstantVersions.AsNoTracking().FirstOrDefault(x => x.Id == id) : _db.ComponentConstantVersions.FirstOrDefault(x => x.Id == id);
        }



        /// <summary>
        /// update constant component
        /// </summary>
        /// <param name="component"></param>
        public void Update(ComponentConstant component)
        {
            _db.ComponentConstants.Attach(component);
            _db.Entry(component).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void UpdateVer(ComponentConstantVersion componentConstantVersion)
        {
            _db.ComponentConstantVersions.Attach(componentConstantVersion);
            _db.Entry(componentConstantVersion).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
