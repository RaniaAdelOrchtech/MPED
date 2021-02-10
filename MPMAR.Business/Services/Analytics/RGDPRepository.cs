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
    public class RGDPRepository : IRGDPRepository
    {
        private readonly AnalyticsDbContext _db;

        public RGDPRepository(AnalyticsDbContext db)
        {
            _db = db;
        }
        public void Add(RGDPGrowthRate rgdp)
        {
            _db.RGDPGrowthRates.Add(rgdp);
            _db.SaveChanges();
        }

        public void AddVer(RGDPGrowthRateVersion rGDPGrowthRate)
        {
            _db.RGDPGrowthRateVersions.Add(rGDPGrowthRate);
            _db.SaveChanges();
        }

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

        public IEnumerable<RGDPViewModel> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount, string role = "")
        {
            var notApproval = string.IsNullOrWhiteSpace(role);

            var queryright = (from pm in _db.RGDPGrowthRates.Where(d => !(d.IsDeleted ?? false)).DefaultIfEmpty()
                              from pmv in _db.RGDPGrowthRateVersions.Where(d => d.RGDPGrowthRateId == pm.Id && d.VersionStatusEnum != VersionStatusEIEnum.Ignored)
                              .OrderByDescending(d => d.Id).DefaultIfEmpty().Take(1)
                              select new RGDPViewModel
                              {
                                  RGDPId = pm.Id,
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

                                  YearFiscal = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFYear.NameEn : pm.DFYear.NameEn,

                                  Unit = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.DFUnit.NameEn : pm.DFUnit.NameEn,

                                  Value = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.GrowthRate : pm.GrowthRate,

                                  IsDeleted = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.IsDeleted ?? false : pm.IsDeleted ?? false,


                                  VersionStatusEnum = pmv.VersionStatusEnum ?? VersionStatusEIEnum.Approved,
                                  ChangeActionEnum = pmv.ChangeActionEnum ?? ChangeActionEIEnum.New,

                                   CreatedById = (pmv != null && (pmv.VersionStatusEnum == VersionStatusEIEnum.Draft
                                  || pmv.VersionStatusEnum == VersionStatusEIEnum.Submitted)) ? pmv.CreatedById : "",
                              });


            //get the rest from HomePageAffiliatesVersions that wasn't included in previous join 
            var queryleft = (from prv in _db.RGDPGrowthRateVersions.Where(d => d.VersionStatusEnum != VersionStatusEIEnum.Ignored)
                             where !_db.RGDPGrowthRates.Any(d => d.Id == prv.RGDPGrowthRateId)
                             select new RGDPViewModel
                             {
                                 Id = prv.Id,
                                 IsVersion = true,
                                 VersionStatusEnum = prv.VersionStatusEnum,
                                 ChangeActionEnum = prv.ChangeActionEnum,
                                 RGDPId = prv.RGDPGrowthRateId,
                                 IsDeleted = prv.IsDeleted ?? false,
                                 Indicator = prv.DFIndicator.NameEn,
                                 Quarter = prv.DFQuarter.NameEn,
                                 Source = prv.DFSource.NameEn,
                                 Unit = prv.DFUnit.NameEn,
                                 YearFiscal = prv.DFYear.NameEn,
                                 Value = prv.GrowthRate,
                                 CreatedById = prv.CreatedById


                             });


            IQueryable<RGDPViewModel> componentData;
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

        public IEnumerable<RGDPGrowthRateVersion> GetAllSubmited()
        {
            return _db.RGDPGrowthRateVersions.Where(x => x.VersionStatusEnum == VersionStatusEIEnum.Submitted);
        }

        public RGDPGrowthRate GetById(int id)
        {
            return _db.RGDPGrowthRates.FirstOrDefault(x => x.Id == id);
        }

        public RGDPGrowthRateVersion GetByRGDPId(int id)
        {
            return _db.RGDPGrowthRateVersions.OrderByDescending(i => i.Id).AsNoTracking().FirstOrDefault(i => i.RGDPGrowthRateId == id);
        }

        public RGDPGrowthRateVersion GetVerById(int id, bool disableTracking = true)
        {
            return disableTracking ? _db.RGDPGrowthRateVersions.AsNoTracking().FirstOrDefault(x => x.Id == id) : _db.RGDPGrowthRateVersions.FirstOrDefault(x => x.Id == id);
        }

        public void Update(RGDPGrowthRate rgdp)
        {
            _db.RGDPGrowthRates.Attach(rgdp);
            _db.Entry(rgdp).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void UpdateVer(RGDPGrowthRateVersion rgdpVersionModel)
        {
            _db.RGDPGrowthRateVersions.Update(rgdpVersionModel);
            _db.SaveChanges();
        }
    }
}
