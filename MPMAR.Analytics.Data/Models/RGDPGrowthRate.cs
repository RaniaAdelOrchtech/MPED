using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class RGDPGrowthRate
    {
        public int Id { get; set; }
        [ForeignKey("DFIndicator")]
        public int DFIndicatorId { get; set; }
        [ForeignKey("DFSource")]
        public int DFSourceId { get; set; }
        [ForeignKey("DFUnit")]
        public int DFUnitId { get; set; }
        [ForeignKey("DFQuarter")]
        public int DFQuarterId { get; set; }
        [ForeignKey("DFYear")]
        public int DFYearId { get; set; }
        public double? GrowthRate { get; set; }
        public bool? IsDeleted { get; set; }


        public virtual DFIndicator DFIndicator { get; set; }
        public virtual DFSource DFSource { get; set; }
        public virtual DFUnit DFUnit { get; set; }
        public virtual DFQuarter DFQuarter { get; set; }
        public virtual DFYear DFYear { get; set; }
    }
}
