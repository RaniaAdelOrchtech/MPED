using MPMAR.Analytics.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPMAR.Analytics.Data.Models
{
   public class InvestmentVersion
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

        public virtual DFIndicator DFIndicator { get; set; }
        public virtual DFSource DFSource { get; set; }
        public virtual DFUnit DFUnit { get; set; }
        public virtual DFQuarter DFQuarter { get; set; }
        public virtual DFYear DFYear { get; set; }

        public bool? isDeleted { get; set; }

        public ChangeActionEIEnum? ChangeActionEnum { get; set; }
        public VersionStatusEIEnum? VersionStatusEnum { get; set; }
        public int? InvestmentsId { get; set; }
        public Investments Investments { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public string CreatedById { get; set; }
    }
}
