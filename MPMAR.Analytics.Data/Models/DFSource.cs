using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class DFSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }

        public string NameEn { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ComponentConstant> ComponentConstants { get; set; }
        public ICollection<ComponentCurrent> ComponentCurrents { get; set; }
        public ICollection<ActivityConstant> ActivityConstants { get; set; }
        public ICollection<ActivityCurrent> ActivityCurrents { get; set; }
        public ICollection<RGDPGrowthRate> RGDPGrowthRates { get; set; }
        public ICollection<RGDPGrowthRate1617> RGDPGrowthRates1617 { get; set; }
        public ICollection<SectorGrowthRate> SectorGrowthRates { get; set; }
        public ICollection<Investments> Investments { get; set; }
    }
}
