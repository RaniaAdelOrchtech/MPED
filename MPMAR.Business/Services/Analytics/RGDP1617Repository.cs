using Microsoft.EntityFrameworkCore;
using MPMAR.Analytics.Data;
using MPMAR.Business.Interfaces.Analytics;
using MPMAR.Business.Services.Analytics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;

namespace MPMAR.Business.Services.Analytics
{
    public class RGDP1617Repository : IRGDP1617Repository
    {
        private readonly AnalyticsDbContext _db;

        public RGDP1617Repository(AnalyticsDbContext db)
        {
            _db = db;
        }
        public void Add(RGDPGrowthRate1617 rgdp)
        {
            _db.RGDPGrowthRates1617.Add(rgdp);
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

        public IEnumerable<RGDPViewModel> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount)
        {
            var componentData = _db.RGDPGrowthRates1617.Where(x => !(x.IsDeleted ?? false)).OrderByDescending(x => x.DFYear.Order).ThenBy(x => x.DFQuarterId).Include(x => x.DFIndicator).Include(x => x.DFSource).Include(x => x.DFQuarter).Include(x => x.DFUnit).Include(x => x.DFYear).Select(x => new RGDPViewModel
       ()
            {
                Id = x.Id,
                Indicator = x.DFIndicator.NameEn,
                Source = x.DFSource.NameEn,
                Unit = x.DFUnit.NameEn,
                YearFiscal = x.DFYear.NameEn,
                Quarter = x.DFQuarter.NameEn,
                Value = x.Value,
                IsDeleted = x.IsDeleted ?? false

            });

            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                componentData = componentData.Where(x =>
                    x.Quarter.ToLower().Contains(searchValue.ToLower()) ||
                    x.YearFiscal.ToLower().Contains(searchValue.ToLower())
                    );
            }
            totalCount = componentData.Count();
            if (sortDirection == "asc")
                componentData = componentData.OrderBy($"{sortColumnName} asc");
            else if (sortDirection == "desc")
                componentData = componentData
                    .OrderBy($"{sortColumnName} descending");

            //paging
            return componentData.Skip(start).Take(lenght).ToList();
        }

        public RGDPGrowthRate1617 GetById(int id)
        {
            return _db.RGDPGrowthRates1617.FirstOrDefault(x => x.Id == id);
        }

        public void Update(RGDPGrowthRate1617 rgdp)
        {
            _db.RGDPGrowthRates1617.Attach(rgdp);
            _db.Entry(rgdp).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
