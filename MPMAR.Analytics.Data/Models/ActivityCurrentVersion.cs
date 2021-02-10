using MPMAR.Analytics.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Analytics.Data.Models
{
    public class ActivityCurrentVersion
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
        [ForeignKey("DFSector")]
        public int DFSectorId { get; set; }
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

        public virtual DFIndicator DFIndicator { get; set; }
        public virtual DFSource DFSource { get; set; }
        public virtual DFUnit DFUnit { get; set; }
        public virtual DFQuarter DFQuarter { get; set; }
        public virtual DFYear DFYear { get; set; }
        public virtual DFSector DFSector { get; set; }
        public bool? IsDeleted { get; set; }

        public ChangeActionEIEnum? ChangeActionEnum { get; set; }
        public VersionStatusEIEnum? VersionStatusEnum { get; set; }
        public int? ActivityCurrentId { get; set; }
        public ActivityCurrent ActivityCurrent { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public string CreatedById { get; set; }

    }
}
