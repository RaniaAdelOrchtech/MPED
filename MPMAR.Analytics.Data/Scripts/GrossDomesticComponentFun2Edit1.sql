USE [MPMARAnalytics]
GO
/****** Object:  UserDefinedFunction [dbo].[GrossDomesticComponentFun2]    Script Date: 4/19/2020 11:45:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[GrossDomesticComponentFun2](@lang nvarchar(20),@yearFiscalIdList  id_list READONLY,@quarterIdList  id_list READONLY,@constant bit,@current bit,
@RGDP bit,@RGDP1617 bit)
RETURNS @result TABLE
(
	Id int,
  Indicator nvarchar(200),
  _Source nvarchar(200),
  Unit nvarchar(100),
  _Quarter nvarchar(100),
  BaseYear nvarchar(50),
  FiscalYear nvarchar(50),
  PrivateConsumption float,
  GovernmentConsumption float,
  GrossCapitalFormation float,
  ExportsOfGoodsAndServices float,
  ImportsOfGoodsAndServices float,
  TotalGrossDomesticProductAtMarketPrices float,
   RealGrowthRateUnit nvarchar(200),
  RealGrowthRate float,
   _ValueUnit nvarchar(200),
  _value float
) 
AS
BEGIN
    IF  (@lang ='' or @lang = 'ar')
    BEGIN
		IF(@constant=1)
		BEGIN

		  INSERT INTO @result (Id,Indicator,_Source,Unit,_Quarter,BaseYear,FiscalYear,PrivateConsumption
		 ,GovernmentConsumption,GrossCapitalFormation,ExportsOfGoodsAndServices,
		 ImportsOfGoodsAndServices,TotalGrossDomesticProductAtMarketPrices,RealGrowthRateUnit,RealGrowthRate,_ValueUnit,_value) 
		 
		 SELECT ComponentConstants.Id,DFIndicators.NameAr,DFSources.NameAr,DFUnits.NameAr,DFQuarters.NameAr,
		 base.NameAr,fiscal.NameAr,PrivateConsumption,GovernmentConsumption,GrossCapitalFormation,ExportsOfGoodsAndServices,
		 ImportsOfGoodsAndServices,TotalGrossDomesticProductAtMarketPrices,d2.NameAr,RGDPGrowthRates.GrowthRate,d3.NameAr,
		 RGDPGrowthRates1617.[Value]

		 from ComponentConstants 
		 JOIN DFIndicators ON ComponentConstants.DFIndicatorId=DFIndicators.Id
		 JOIN DFSources ON ComponentConstants.DFSourceId=DFSources.Id
		 JOIN DFUnits ON ComponentConstants.DFUnitId=DFUnits.Id
		 JOIN DFQuarters ON ComponentConstants.DFQuarterId=DFQuarters.Id
		 JOIN DFYears base ON ComponentConstants.DFYearBaseId=base.Id
		 JOIN DFYears fiscal ON ComponentConstants.DFYearFiscalId=fiscal.Id
		 left JOIN RGDPGrowthRates  ON @RGDP=1  and RGDPGrowthRates.DFQuarterId=ComponentConstants.DFQuarterId
		 and RGDPGrowthRates.DFYearId=ComponentConstants.DFYearFiscalId 
		 and (RGDPGrowthRates.IsDeleted is null or RGDPGrowthRates.IsDeleted=0)
		left join DFUnits d2 on d2.Id=RGDPGrowthRates.DFUnitId
		left JOIN RGDPGrowthRates1617  ON @RGDP1617=1 and  RGDPGrowthRates1617.DFQuarterId=ComponentConstants.DFQuarterId
		and	RGDPGrowthRates1617.DFYearFiscalId=ComponentConstants.DFYearFiscalId
		 and (RGDPGrowthRates1617.IsDeleted is null or RGDPGrowthRates1617.IsDeleted=0)
		left join DFUnits d3 on d3.Id=RGDPGrowthRates1617.DFUnitId
		 where ComponentConstants.DFYearFiscalId in (select * from @yearFiscalIdList)
		 and ComponentConstants.DFQuarterId in (select * from @quarterIdList)
		 and (ComponentConstants.IsDeleted is null or ComponentConstants.IsDeleted=0)
		end
		IF(@current=1)
		BEGIN

		  INSERT INTO @result (Id,Indicator,_Source,Unit,_Quarter,BaseYear,FiscalYear,PrivateConsumption
		 ,GovernmentConsumption,GrossCapitalFormation,ExportsOfGoodsAndServices,
		 ImportsOfGoodsAndServices,TotalGrossDomesticProductAtMarketPrices) 
		 
		 SELECT ComponentCurrents.Id,DFIndicators.NameAr,DFSources.NameAr,DFUnits.NameAr,DFQuarters.NameAr,
		 null,fiscal.NameAr,PrivateConsumption,GovernmentConsumption,GrossCapitalFormation,ExportsOfGoodsAndServices,
		 ImportsOfGoodsAndServices,TotalGrossDomesticProductAtMarketPrices

		 from ComponentCurrents 
		 JOIN DFIndicators ON ComponentCurrents.DFIndicatorId=DFIndicators.Id
		 JOIN DFSources ON ComponentCurrents.DFSourceId=DFSources.Id
		 JOIN DFUnits ON ComponentCurrents.DFUnitId=DFUnits.Id
		 JOIN DFQuarters ON ComponentCurrents.DFQuarterId=DFQuarters.Id
		 JOIN DFYears fiscal ON ComponentCurrents.DFYearFiscalId=fiscal.Id
		 where ComponentCurrents.DFYearFiscalId in (select * from @yearFiscalIdList)
		 and ComponentCurrents.DFQuarterId in (select * from @quarterIdList)
		  and (ComponentCurrents.IsDeleted is null or ComponentCurrents.IsDeleted=0)

		END
    END

	IF  (@lang = 'en')
    BEGIN
		IF(@constant=1)
		BEGIN

		  INSERT INTO @result (Id,Indicator,_Source,Unit,_Quarter,BaseYear,FiscalYear,PrivateConsumption
		 ,GovernmentConsumption,GrossCapitalFormation,ExportsOfGoodsAndServices,
		 ImportsOfGoodsAndServices,TotalGrossDomesticProductAtMarketPrices,RealGrowthRateUnit,RealGrowthRate,_ValueUnit,_value) 
		 
		 SELECT ComponentConstants.Id,DFIndicators.NameEn,DFSources.NameEn,DFUnits.NameEn,DFQuarters.NameEn,
		 base.NameEn,fiscal.NameEn,PrivateConsumption,GovernmentConsumption,GrossCapitalFormation,ExportsOfGoodsAndServices,
		 ImportsOfGoodsAndServices,TotalGrossDomesticProductAtMarketPrices,d2.NameEn,RGDPGrowthRates.GrowthRate,d3.NameEn,
		 RGDPGrowthRates1617.[Value]

		 from ComponentConstants 
		 JOIN DFIndicators ON ComponentConstants.DFIndicatorId=DFIndicators.Id
		 JOIN DFSources ON ComponentConstants.DFSourceId=DFSources.Id
		 JOIN DFUnits ON ComponentConstants.DFUnitId=DFUnits.Id
		 JOIN DFQuarters ON ComponentConstants.DFQuarterId=DFQuarters.Id
		 JOIN DFYears base ON ComponentConstants.DFYearBaseId=base.Id
		 JOIN DFYears fiscal ON ComponentConstants.DFYearFiscalId=fiscal.Id
		 left JOIN RGDPGrowthRates  ON @RGDP=1  and RGDPGrowthRates.DFQuarterId=ComponentConstants.DFQuarterId
		 and RGDPGrowthRates.DFYearId=ComponentConstants.DFYearFiscalId 
		 and (RGDPGrowthRates.IsDeleted is null or RGDPGrowthRates.IsDeleted=0)
		left join DFUnits d2 on d2.Id=RGDPGrowthRates.DFUnitId
		left JOIN RGDPGrowthRates1617  ON @RGDP1617=1 and  RGDPGrowthRates1617.DFQuarterId=ComponentConstants.DFQuarterId
		and	RGDPGrowthRates1617.DFYearFiscalId=ComponentConstants.DFYearFiscalId
		 and (RGDPGrowthRates1617.IsDeleted is null or RGDPGrowthRates1617.IsDeleted=0)
		left join DFUnits d3 on d3.Id=RGDPGrowthRates1617.DFUnitId
		where ComponentConstants.DFYearFiscalId in (select * from @yearFiscalIdList)
		 and ComponentConstants.DFQuarterId in (select * from @quarterIdList)
		 and (ComponentConstants.IsDeleted is null or ComponentConstants.IsDeleted=0)
		end
		IF(@current=1)
		BEGIN

		  INSERT INTO @result (Id,Indicator,_Source,Unit,_Quarter,BaseYear,FiscalYear,PrivateConsumption
		 ,GovernmentConsumption,GrossCapitalFormation,ExportsOfGoodsAndServices,
		 ImportsOfGoodsAndServices,TotalGrossDomesticProductAtMarketPrices) 
		 
		 SELECT ComponentCurrents.Id,DFIndicators.NameEn,DFSources.NameEn,DFUnits.NameEn,DFQuarters.NameEn,
		 null,fiscal.NameEn,PrivateConsumption,GovernmentConsumption,GrossCapitalFormation,ExportsOfGoodsAndServices,
		 ImportsOfGoodsAndServices,TotalGrossDomesticProductAtMarketPrices

		 from ComponentCurrents 
		 JOIN DFIndicators ON ComponentCurrents.DFIndicatorId=DFIndicators.Id
		 JOIN DFSources ON ComponentCurrents.DFSourceId=DFSources.Id
		 JOIN DFUnits ON ComponentCurrents.DFUnitId=DFUnits.Id
		 JOIN DFQuarters ON ComponentCurrents.DFQuarterId=DFQuarters.Id
		 JOIN DFYears fiscal ON ComponentCurrents.DFYearFiscalId=fiscal.Id
		where ComponentCurrents.DFYearFiscalId in (select * from @yearFiscalIdList)
		 and ComponentCurrents.DFQuarterId in (select * from @quarterIdList)
		  and (ComponentCurrents.IsDeleted is null or ComponentCurrents.IsDeleted=0)

		END
    END

    RETURN 
END