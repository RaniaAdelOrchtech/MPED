using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class DFGovernorate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }

        public string NameEn { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("DFRegionId")]
        public int? DFRegionId { get; set; }
        public virtual DFRegion DFRegion { get; set; }

        public bool? isTotal { get; set; }

    }
}
