using MPMAR.Analytics.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class InvestmentCommonViewModel
    {
        public double? Agriculture { get; set; }
        public double? Petroleum { get; set; }
        public double? NaturalGas { get; set; }
        public double? OtherExtractions { get; set; }
        public double? PetroleumRefining { get; set; }
        public double? OtherManufacturing { get; set; }
        public double? Electricity { get; set; }
        public double? WaterAndSewerage { get; set; }
        public double? Construction { get; set; }
        public double? StorageAndTransportation { get; set; }
        public double? InformationAndCommunication { get; set; }
        public double? SuezCanal { get; set; }
        public double? WholesaleAndRetailTrade { get; set; }
        public double? FinancialIntermediaryInsuranceAndSocialSecurity { get; set; }
        public double? AccommodationAndFoodServiceActivities { get; set; }
        public double? RealEstateActivities { get; set; }
        public double? Education { get; set; }
        public double? Health { get; set; }
        public double? OtherSrvices { get; set; }
        public double? TotalInvestments { get; set; }

        public ChangeActionEIEnum? ChangeActionEnum { get; set; }
        public VersionStatusEIEnum? VersionStatusEnum { get; set; }
        public int? Investmentid { get; set; }

        public string CreatedById { get; set; }
    }
}
