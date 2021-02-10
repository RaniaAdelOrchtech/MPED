using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class ComponentCurrent
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
        [ForeignKey("DFYearFiscal")]
        public int DFYearFiscalId { get; set; }
        public double? PrivateConsumption { get; set; }
        public double? GovernmentConsumption { get; set; }
        public double? GrossCapitalFormation { get; set; }
        public double? ExportsOfGoodsAndServices { get; set; }
        public double? ImportsOfGoodsAndServices { get; set; }
        public double? TotalGrossDomesticProductAtMarketPrices { get; set; }

        public virtual DFIndicator DFIndicator { get; set; }
        public virtual DFSource DFSource { get; set; }
        public virtual DFUnit DFUnit { get; set; }
        public virtual DFQuarter DFQuarter { get; set; }
        public virtual DFYear DFYearFiscal { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
