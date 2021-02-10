using MPMAR.Analytics.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Services.Analytics.ViewModels
{
    public class ActivityVM
    {
        public int Id { get; set; }
        public string DFIndicator { get; set; }
        public string DFSource { get; set; }
        public string DFUnit { get; set; }
        public string DFQuarter { get; set; }
        public string DFYearBase { get; set; }
        public string DFYear { get; set; }
        public string DFSector { get; set; }
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
        public bool? IsDeleted { get; set; }
        public ChangeActionEIEnum? ChangeActionEnum { get; set; }
        public VersionStatusEIEnum? VersionStatusEnum { get; set; }
        public int? ActivityCurrentId { get; set; }
        public bool IsVersion { get; set; }

        public string CreatedById { get; set; }
    }
}
