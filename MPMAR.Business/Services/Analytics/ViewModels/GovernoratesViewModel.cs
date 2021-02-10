using MPMAR.Analytics.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Business.Services.Analytics.ViewModels
{
    public class GovernoratesViewModel
    {
        public int Id { get; set; }
        public string Indicator { get; set; }
        public string Unit { get; set; }
        public string Governorate { get; set; }
        public string Year { get; set; }
        public double? Agriculture { get; set; }
        public double? CrudePetroleumExtraction { get; set; }
        public double? OtherExtractions { get; set; }
        public double? PetroleumRefinement { get; set; }
        public double? ManufacturingIndustries { get; set; }
        public double? ElectricityandGas { get; set; }
        public double? Water { get; set; }
        public double? Sewerage { get; set; }
        public double? WasteRecycling { get; set; }
        public double? Construction { get; set; }
        public double? WholesaleandRetailTrade { get; set; }
        public double? Communication { get; set; }
        public double? Information { get; set; }
        public double? TransportationandStorage { get; set; }
        public double? AccommodationandFoodServiceActivities { get; set; }
        public double? RealEstateOwnership { get; set; }
        public double? BusinessServices { get; set; }
        public double? Education { get; set; }
        public double? Health { get; set; }
        public double? OtherServices { get; set; }
        public double? NonFinancialCorporations { get; set; }
        public double? FinancialCorporations { get; set; }
        public double? GeneralGovernment { get; set; }
        public double? NonProfitInstitutionsServingHouseholdSector { get; set; }
        public double? DomesticWorkers { get; set; }
        public double? TotalGovernorateGDP { get; set; }
        public double? CustomFees { get; set; }
        public double? TotalGDPEgyptWithCustomFees { get; set; }
        public bool?  IsDeleted { get; set; }

        public ChangeActionEIEnum? ChangeActionEnum { get; set; }
        public VersionStatusEIEnum? VersionStatusEnum { get; set; }
        public int? GovernorateId { get; set; }

        public bool IsVersion { get; set; }

        public string CreatedById { get; set; }
    }
}
