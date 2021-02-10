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

namespace MPMAR.Business.Services.Analytics
{
    public class ComponentCurrenttRepository : IComponentCurrenttRepository
    {
        private readonly AnalyticsDbContext _db;

        public ComponentCurrenttRepository(AnalyticsDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// add new component current
        /// </summary>
        /// <param name="component"></param>
        public void Add(ComponentCurrent component)
        {
            _db.ComponentCurrents.Add(component);
            _db.SaveChanges();
        }

        public void AddVer(ComponentCurrentVersion componentCurrentVersion)
        {
            _db.ComponentCurrentVersions.Add(componentCurrentVersion);
            _db.SaveChanges();
        }

        /// <summary>
        /// delete current component by id
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

        //public IEnumerable<ComponentViewModel> GetAll()
        //{
        //    var componentData = _db.ComponentCurrents.Where(x => !(x.IsDeleted ?? false)).OrderByDescending(x => x.DFYearFiscal.Order).ThenBy(x => x.DFQuarterId).Include(x => x.DFIndicator).Include(x => x.DFSource).Include(x => x.DFQuarter).Include(x => x.DFUnit).Include(x => x.DFYearFiscal).Select(x => new ComponentViewModel
        //      ()
        //    {
        //        Id=x.Id,
        //        Indicator = x.DFIndicator.NameEn,
        //        Source = x.DFSource.NameEn,
        //        Unit = x.DFUnit.NameEn,
        //        YearFiscal = x.DFYearFiscal.NameEn,
        //        Quarter = x.DFQuarter.NameEn,
        //        ExportsOfGoodsAndServices = x.ExportsOfGoodsAndServices,
        //        GovernmentConsumption = x.GovernmentConsumption,
        //        GrossCapitalFormation = x.GrossCapitalFormation,
        //        TotalGrossDomesticProductAtMarketPrices = x.TotalGrossDomesticProductAtMarketPrices,
        //        ImportsOfGoodsAndServices = x.ImportsOfGoodsAndServices,
        //        PrivateConsumption = x.PrivateConsumption

        //    }).ToList();

        //    return componentData;
        //}

        /// <summary>
        /// get all component current in some range
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

            var queryright = (from pm in _db.ComponentCurrents.Where(d => !(d.IsDeleted ?? false)).DefaultIfEmpty()
                              from pmv in _db.ComponentCurrentVersions.Where(d => d.ComponentCurrentId == pm.Id && d.VersionStatusEnum != VersionStatusEIEnum.Ignored)
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
            var queryleft = (from prv in _db.ComponentCurrentVersions.Where(d => d.VersionStatusEnum != VersionStatusEIEnum.Ignored)
                             where !_db.ComponentCurrents.Any(d => d.Id == prv.ComponentCurrentId)
                             select new ComponentViewModel
                             {
                                 Id = prv.Id,
                                 IsVersion = true,
                                 VersionStatusEnum = prv.VersionStatusEnum,
                                 ChangeActionEnum = prv.ChangeActionEnum,
                                 ComponentConstantId = prv.ComponentCurrentId,
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
                             }); ;


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

        public IEnumerable<ComponentCurrentVersion> GetAllSubmited()
        {
            return _db.ComponentCurrentVersions.Where(x => x.VersionStatusEnum == VersionStatusEIEnum.Submitted);
        }


        public ComponentCurrentVersion GetByComponentCurrentId(int id)
        {
            return _db.ComponentCurrentVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.ComponentCurrentId == id);
        }

        /// <summary>
        /// get specific component current by id
        /// </summary>
        /// <param name="id">component id</param>
        /// <returns></returns>
        public ComponentCurrent GetById(int id)
        {
            return _db.ComponentCurrents.FirstOrDefault(x => x.Id == id);
        }

        public ComponentCurrentVersion GetVerById(int id, bool disableTracking = true)
        {
            return disableTracking ? _db.ComponentCurrentVersions.AsNoTracking().FirstOrDefault(x => x.Id == id) : _db.ComponentCurrentVersions.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// update current component
        /// </summary>
        /// <param name="component"></param>
        public void Update(ComponentCurrent component)
        {
            _db.ComponentCurrents.Attach(component);
            _db.Entry(component).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void UpdateVer(ComponentCurrentVersion componentCurrentVersionModel)
        {
            _db.ComponentCurrentVersions.Update(componentCurrentVersionModel);
            _db.SaveChanges();
        }
    }
}
