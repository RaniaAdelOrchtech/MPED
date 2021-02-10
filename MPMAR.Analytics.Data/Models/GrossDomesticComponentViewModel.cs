using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class GrossDomesticComponentViewModel
    {

        public int Id { get; set; }
        public string Indicator { get; set; }
        public string _Source { get; set; }
        public string Unit { get; set; }
        public string _Quarter { get; set; }
        public string FiscalYear { get; set; }
        public double? PrivateConsumption { get; set; }
        public double? GovernmentConsumption { get; set; }
        public double? GrossCapitalFormation { get; set; }
        public double? ExportsOfGoodsAndServices { get; set; }
        public double? ImportsOfGoodsAndServices { get; set; }
        public double? TotalGrossDomesticProductAtMarketPrices { get; set; }
        public string RealGrowthRateUnit { get; set; }
        public double? RealGrowthRate { get; set; }
        public string _ValueUnit { get; set; }
        public double? _Value { get; set; }

    }
}
