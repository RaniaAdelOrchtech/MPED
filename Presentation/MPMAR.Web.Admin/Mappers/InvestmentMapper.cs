using MPMAR.Analytics.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Mappers
{
    public static class InvestmentMapper
    {
        public static List<InvestmentViewModel> MapToInvestmentViewModel(this IEnumerable<Investments> models)
        {
            return models.Select(x => new InvestmentViewModel()
            {
                Id=x.Id,
                Indicator=x.DFIndicator.NameEn,
                _Source=x.DFSource.NameEn,
                _Year=x.DFYear.NameEn,
                Unit=x.DFUnit.NameEn,
                _Quarter = x.DFQuarter.NameEn,
                Agriculture =x.Agriculture,
                AccommodationAndFoodServiceActivities=x.AccommodationAndFoodServiceActivities,
                Construction=x.Construction,
                Education=x.Education,
                Electricity=x.Electricity,
                FinancialIntermediaryInsuranceAndSocialSecurity=x.FinancialIntermediaryInsuranceAndSocialSecurity,
                Health=x.Health,
                InformationAndCommunication=x.InformationAndCommunication,
                NaturalGas=x.NaturalGas,
                OtherExtractions=x.OtherExtractions,
                OtherManufacturing=x.OtherManufacturing,
                OtherSrvices=x.OtherSrvices,
                Petroleum=x.Petroleum,
                PetroleumRefining=x.PetroleumRefining,
                RealEstateActivities=x.RealEstateActivities,
                StorageAndTransportation=x.StorageAndTransportation,
                SuezCanal=x.SuezCanal,
                TotalInvestments=x.TotalInvestments,
                WaterAndSewerage=x.WaterAndSewerage,
                WholesaleAndRetailTrade=x.WholesaleAndRetailTrade,
                
                

            }).ToList();
        }
    }
}
