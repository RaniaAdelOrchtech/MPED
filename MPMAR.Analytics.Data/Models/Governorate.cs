using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class Governorate
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("DFIndicatorId")]
        public int DFIndicatorId { get; set; }
        public virtual DFIndicator DFIndicator { get; set; }
        public string Unit { get; set; }
        [Required]
        [ForeignKey("DFGovernorateId")]
        public int DFGovernorateId { get; set; }
        public virtual DFGovernorate DFGovernorate { get; set; }
        [Required]
        [ForeignKey("DFYearId")]
        public int DFYearId { get; set; }
        public virtual DFYear DFYear { get; set; }
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
        public bool? isDeleted { get; set; }

    }
}
