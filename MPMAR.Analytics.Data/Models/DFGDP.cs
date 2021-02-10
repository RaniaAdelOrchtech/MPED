using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class DFGDP
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }

        public string NameEn { get; set; }
        public bool IsDeleted { get; set; }
        public int Type { get; set; }
        public int order { get; set; }
        public bool IsBasic { get; set; }
        public int? DFGDPId { get; set; }

        [InverseProperty("DFGDPs")]
        public virtual DFGDP DFGDp { get; set; }
        public virtual ICollection<DFGDP> DFGDPs { get; set; }
    }
}
