using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class StaticActivity
    {
        public int Id { get; set; }
        public string BaseYear { get; set; }
        public string Quarter { get; set; }
        public string Year { get; set; }
        public string PerEGMillion { get; set; }
        public double? AgricultureForestryandFishing { get; set; }
        public double? Extractions { get; set; }
        public double? Oil { get; set; }
        public double? Gas { get; set; }
        public double? ExtractionsOthers { get; set; }
        public double? TransformativeIndustries { get; set; }
        public double? PetroleumRefining { get; set; }
        public double? AnotherExtension { get; set; }
        public double? Electricity { get; set; }
        public double? WaterandRecycling { get; set; }
        public double? ConstructionandBuilding { get; set; }
        public double? TransportationandSaving { get; set; }
        public double? CommunicationandInformation { get; set; }
        public double? SuezCanal { get; set; }
        public double? WholesaleandRetailTrade { get; set; }
        public double? FinancialIntermediationandAuxiliaryActivities { get; set; }
        public double? SocialInsurance { get; set; }
        public double? HotelandRestaurants { get; set; }
        public double? RealEstateActivities { get; set; }
        public double? RealEstateProperty { get; set; }
        public double? BusinessServices { get; set; }
        public double? GeneralGovernment { get; set; }
        public double? EducationandPersonalServices { get; set; }
        public double? Education { get; set; }
        public double? Health { get; set; }
        public double? OtherServices { get; set; }
        public double? TotalGeneral { get; set; }
    }
}
