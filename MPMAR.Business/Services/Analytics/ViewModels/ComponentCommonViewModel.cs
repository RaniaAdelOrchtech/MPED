using MPMAR.Analytics.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Services.Analytics.ViewModels
{
   public class ComponentCommonViewModel
    {
        public int Id { get; set; }
        public double? PrivateConsumption { get; set; }
        public double? GovernmentConsumption { get; set; }
        public double? GrossCapitalFormation { get; set; }
        public double? ExportsOfGoodsAndServices { get; set; }
        public double? ImportsOfGoodsAndServices { get; set; }
        public double? TotalGrossDomesticProductAtMarketPrices { get; set; }
        public ChangeActionEIEnum? ChangeActionEnum { get; set; }
        public VersionStatusEIEnum? VersionStatusEnum { get; set; }
        public bool IsVersion { get; set; }
        public string CreatedById { get; set; }
    }
}
