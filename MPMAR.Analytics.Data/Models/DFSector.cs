using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class DFSector
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }

        public string NameEn { get; set; }
        public bool IsDeleted { get; set; }
        public int Type { get; set; }
        public int? Order { get; set; }
        public ICollection<ActivityConstant> ActivityConstants { get; set; }
        public ICollection<ActivityCurrent> ActivityCurrents { get; set; }
        public ICollection<SectorGrowthRate> SectorGrowthRates { get; set; }
    }
}
