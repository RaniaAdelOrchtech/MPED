using MPMAR.Analytics.Data;
using MPMAR.Business.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MPMAR.Business.Services
{
    public class DFGovernoratesRepository : IDFGovernoratesRepository
    {
        private readonly AnalyticsDbContext _db;
        public DFGovernoratesRepository(AnalyticsDbContext db)
        {
            _db = db;
        }
        public IEnumerable<DFGovernorate> GetAll()
        {
            var governorate = _db.DFGovernorates.Where(g => g.IsDeleted != true).ToList();
            return governorate;
        }

        public IEnumerable<DFGovernorate> GetAllGover()
        {
            var governorate = _db.DFGovernorates.Where(g => g.isTotal == null || g.isTotal == false).ToList();
            return governorate;
        }
        public IEnumerable<DFGovernorate> GetAllRegion()
        {
            var governorate = _db.DFGovernorates.Where(g => g.isTotal == true).ToList();

            foreach (var govern in governorate)
            {
                if (govern.NameEn.ToLower().Contains("total"))
                {
                    govern.NameEn = govern.NameEn.ToLower().Replace("total ","");
                    govern.NameEn = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(govern.NameEn);
                }
            }

            return governorate;
        }
        public IEnumerable<DFGovernorate> GetGovernsByRegionId(int id)
        {
            var governorate = _db.DFGovernorates.Where(g => g.DFRegionId == id && g.isTotal != true).ToList();
            return governorate;
        }

        public DFGovernorate GetGovernsByRegionIdWithTrue(int id)
        {
            var governorate = _db.DFGovernorates.Where(g => g.DFRegionId == id && g.isTotal == true).FirstOrDefault();
            return governorate;
        }

        public DFGovernorate GetGoverById(int govID)
        {
            var governorate = _db.DFGovernorates.FirstOrDefault(g => g.Id == govID);
            return governorate;
        }


    }
}
