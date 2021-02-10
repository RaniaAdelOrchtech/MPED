using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class DFRegion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }

        public string NameEn { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<DFGovernorate> DFGovernorates { get; set; }


    }
}
