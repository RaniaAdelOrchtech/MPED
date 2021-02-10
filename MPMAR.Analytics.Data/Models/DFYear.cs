using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class DFYear
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }

        public string NameEn { get; set; }
        public bool IsDeleted { get; set; }
        public int? Order { get; set; }
        //[InverseProperty("DFYearBase")]
        //public ICollection<ComponentConstant> BaseComponentConstants { get; set; }
        [InverseProperty("DFYearFiscal")]
        public ICollection<ComponentConstant> FiscalComponentConstants { get; set; }
        public ICollection<ComponentCurrent> ComponentCurrents { get; set; }
        [InverseProperty("DFYear")]
        public ICollection<ActivityConstant> ActivityConstants { get; set; }
        //[InverseProperty("DFYearBase")]
        //public ICollection<ActivityConstant> BaseActivityConstants { get; set; }
        public ICollection<ActivityCurrent> ActivityCurrents { get; set; }
        public ICollection<RGDPGrowthRate> RGDPGrowthRates { get; set; }
        public ICollection<RGDPGrowthRate1617> RGDPGrowthRates1617 { get; set; }
        public ICollection<SectorGrowthRate> SectorGrowthRates { get; set; }
        public ICollection<Investments> Investments { get; set; }

    }
}
