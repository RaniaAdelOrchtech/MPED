using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class GrossDomesticActivity
    {
        public int Id { get; set; }
        public string Indicator { get; set; }
        public string _Source { get; set; }
        public string Unit { get; set; }
        public string _Quarter { get; set; }
        public string _Year { get; set; }
        public string Sector { get; set; }
        public double? AgricultureForestryFishing { get; set; }
        public double? MiningQuarrying { get; set; }
        public double? Petroleum { get; set; }

        public double? Gas { get; set; }
        public double? OtherExtraction { get; set; }
        public double? ManufacturingIndustries { get; set; }
        public double? petroleumRefining { get; set; }
        public double? OtherManufacturing { get; set; }
        public double? Electricity { get; set; }
        public double? WaterSewerageRemediationActivitie { get; set; }
        public double? Construction { get; set; }
        public double? TransportationAndStorage { get; set; }
        public double? Communication { get; set; }

        public double? Information { get; set; }
        public double? SuezcCanal { get; set; }
        public double? WholesaleAndRetailTrade { get; set; }
        public double? FinancialIntermediariesAuxiliaryServices { get; set; }
        public double? SocialSecurityAndInsurance { get; set; }
        public double? AccommodationAndFoodServiceActivities { get; set; }
        public double? RealEstateActivitie { get; set; }
        public double? RealEstateOwnership { get; set; }
        public double? BusinessServices { get; set; }
        public double? GeneralGovernment { get; set; }

        public double? SocialServices { get; set; }
        public double? Education { get; set; }
        public double? Health { get; set; }
        public double? OtherServices { get; set; }
        public double? TotalGDPAtFactorCost { get; set; }
        public double? TotalGDPGrowthRateAtFactorCost { get; set; }
        public string RealGrowthRateUnit { get; set; }
        public double? RealGrowthRate { get; set; }
        public string _ValueUnit { get; set; }
        public double? _Value { get; set; }


    }
}
