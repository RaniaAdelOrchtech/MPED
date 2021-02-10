USE [MPMARAnalytics]
GO
/****** Object:  UserDefinedFunction [dbo].[GrossDomesticActivityFun2]    Script Date: 4/19/2020 11:42:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[GrossDomesticActivityFun2](@lang nvarchar(20),@yearFiscalIdList  id_list READONLY,@quarterIdList  id_list READONLY,@constant bit,@current bit,
@RGDP bit,@RGDP1617 bit,@sectorRGDP bit ,@sectorList  id_list READONLY)
RETURNS @result TABLE
(
	Id int,
  Indicator NVARCHAR(200),
  _Source NVARCHAR(200),
  Unit NVARCHAR(100),
  BaseYear NVARCHAR(100),
  _Quarter NVARCHAR(50),
  _Year NVARCHAR(50),
  Sector NVARCHAR(50),
  AgricultureForestryFishing float,
  MiningQuarrying float,
  Petroleum float,

  Gas float,
  OtherExtraction float,
  ManufacturingIndustries float,
  petroleumRefining float,
   OtherManufacturing float,
  Electricity float,
    WaterSewerageRemediationActivitie float,
  Construction float,
    TransportationAndStorage float,
  Communication float,

    Information float,
  SuezcCanal float,
    WholesaleAndRetailTrade float,
  FinancialIntermediariesAuxiliaryServices float,
    SocialSecurityAndInsurance float,
  AccommodationAndFoodServiceActivities float,
    RealEstateActivitie float,
  RealEstateOwnership float,
    BusinessServices float,
  GeneralGovernment float,

    SocialServices float,
  Education float,
    Health float,
  OtherServices float,
    TotalGDPAtFactorCost float,
    TotalGDPGrowthRateAtFactorCost float,
   RealGrowthRateUnit nvarchar(200),
  RealGrowthRate float,
   _ValueUnit nvarchar(200),
  _value float
) 
AS
BEGIN
    IF  (@lang='' or @lang = 'ar')
    BEGIN
		IF(@constant=1)
		BEGIN

		  INSERT INTO @result (Id, Indicator,_Source,Unit,BaseYear,_Quarter,_Year,Sector,AgricultureForestryFishing,
		  MiningQuarrying,Petroleum,Gas,OtherExtraction,ManufacturingIndustries,petroleumRefining,OtherManufacturing,
		  Electricity,WaterSewerageRemediationActivitie,Construction,TransportationAndStorage,Communication,
		  Information,SuezcCanal,WholesaleAndRetailTrade,FinancialIntermediariesAuxiliaryServices,
		  SocialSecurityAndInsurance,AccommodationAndFoodServiceActivities,RealEstateActivitie,
		  BusinessServices,GeneralGovernment,SocialServices,Education,Health,OtherServices,TotalGDPAtFactorCost,RealGrowthRateUnit,RealGrowthRate,_ValueUnit,_value) 

		 SELECT ActivityConstants.Id, DFIndicators.NameAr,DFSources.NameAr,DFUnits.NameAr,base.NameAr,
		 DFQuarters.NameAr,_year.NameAr,DFSectors.NameAr,AgricultureForestryFishing,
		  MiningQuarrying,Petroleum,Gas,OtherExtraction,ManufacturingIndustries,petroleumRefining,OtherManufacturing,
		  Electricity,WaterSewerageRemediationActivitie,Construction,TransportationAndStorage,Communication,
		  Information,SuezcCanal,WholesaleAndRetailTrade,FinancialIntermediariesAuxiliaryServices,
		  SocialSecurityAndInsurance,AccommodationAndFoodServiceActivities,RealEstateActivitie,
		  BusinessServices,GeneralGovernment,SocialServices,Education,Health,OtherServices,TotalGDPAtFactorCost,d2.NameAr,
		  RGDPGrowthRates.GrowthRate,d3.NameAr,RGDPGrowthRates1617.[Value]
		  
		 from ActivityConstants 

		 JOIN DFIndicators ON ActivityConstants.DFIndicatorId=DFIndicators.Id
		 JOIN DFSources ON ActivityConstants.DFSourceId=DFSources.Id
		 JOIN DFUnits ON ActivityConstants.DFUnitId=DFUnits.Id
		 JOIN DFQuarters ON ActivityConstants.DFQuarterId=DFQuarters.Id
		 JOIN DFYears base ON ActivityConstants.DFYearBaseId=base.Id
		 JOIN DFYears _year ON ActivityConstants.DFYearId=_year.Id
		 JOIN DFSectors  ON ActivityConstants.DFSectorId=DFSectors.Id
		 left JOIN RGDPGrowthRates  ON @RGDP=1  and RGDPGrowthRates.DFQuarterId=ActivityConstants.DFQuarterId
		 and RGDPGrowthRates.DFYearId=ActivityConstants.DFYearId and ActivityConstants.DFSectorId=33
		   and (RGDPGrowthRates.IsDeleted is null or RGDPGrowthRates.IsDeleted=0)
		 left join DFUnits d2 on d2.Id=RGDPGrowthRates.DFUnitId
		 left JOIN RGDPGrowthRates1617  ON @RGDP1617=1    and RGDPGrowthRates1617.DFYearFiscalId=ActivityConstants.DFYearId
		 and RGDPGrowthRates1617.DFQuarterId=ActivityConstants.DFQuarterId and ActivityConstants.DFSectorId=33
		  and (RGDPGrowthRates1617.IsDeleted is null or RGDPGrowthRates1617.IsDeleted=0)
		 left join DFUnits d3 on d3.Id=RGDPGrowthRates1617.DFUnitId
		 where ActivityConstants.DFYearId in (select * from @yearFiscalIdList)
		 and ActivityConstants.DFQuarterId in (select * from @quarterIdList)
		 and ActivityConstants.DFSectorId in (select * from @sectorList)
		 and (ActivityConstants.IsDeleted is null or ActivityConstants.IsDeleted=0)


		 IF(@sectorRGDP=1)
		  BEGIN
		  INSERT INTO @result (Id, Indicator,_Source,Unit,BaseYear,_Quarter,_Year,Sector,AgricultureForestryFishing,
		  MiningQuarrying,Petroleum,Gas,OtherExtraction,ManufacturingIndustries,petroleumRefining,OtherManufacturing,
		  Electricity,WaterSewerageRemediationActivitie,Construction,TransportationAndStorage,Communication,
		  Information,SuezcCanal,WholesaleAndRetailTrade,FinancialIntermediariesAuxiliaryServices,
		  SocialSecurityAndInsurance,AccommodationAndFoodServiceActivities,RealEstateActivitie,
		  BusinessServices,GeneralGovernment,SocialServices,Education,Health,OtherServices,TotalGDPAtFactorCost,
		  TotalGDPGrowthRateAtFactorCost) 

		 SELECT SectorGrowthRates.Id, DFIndicators.NameAr,DFSources.NameAr,DFUnits.NameAr,null,
		 DFQuarters.NameAr,_year.NameAr,DFSectors.NameAr,AgricultureForestryFishing,
		  MiningQuarrying,Petroleum,Gas,OtherExtraction,ManufacturingIndustries,petroleumRefining,OtherManufacturing,
		  Electricity,WaterSewerageRemediationActivitie,Construction,TransportationAndStorage,Communication,
		  Information,SuezcCanal,WholesaleAndRetailTrade,FinancialIntermediariesAuxiliaryServices,
		  SocialSecurityAndInsurance,AccommodationAndFoodServiceActivities,RealEstateActivitie,
		  BusinessServices,GeneralGovernment,SocialServices,Education,Health,OtherServices,null,TotalGDPAtFactorCost
		  
		 from SectorGrowthRates 

		 JOIN DFIndicators ON SectorGrowthRates.DFIndicatorId=DFIndicators.Id
		 JOIN DFSources ON SectorGrowthRates.DFSourceId=DFSources.Id
		 JOIN DFUnits ON SectorGrowthRates.DFUnitId=DFUnits.Id
		 JOIN DFQuarters ON SectorGrowthRates.DFQuarterId=DFQuarters.Id
		 JOIN DFYears _year ON SectorGrowthRates.DFYearId=_year.Id
		 JOIN DFSectors  ON SectorGrowthRates.DFSectorId=DFSectors.Id
		 where SectorGrowthRates.DFYearId in (select * from @yearFiscalIdList)
		 and SectorGrowthRates.DFQuarterId in (select * from @quarterIdList)
		 and SectorGrowthRates.DFSectorId in (select * from @sectorList)
		  and (SectorGrowthRates.IsDeleted is null or SectorGrowthRates.IsDeleted=0)
		 END

		END


		IF(@current=1)
		BEGIN

		  INSERT INTO @result (Id, Indicator,_Source,Unit,BaseYear,_Quarter,_Year,Sector,AgricultureForestryFishing,
		  MiningQuarrying,Petroleum,Gas,OtherExtraction,ManufacturingIndustries,petroleumRefining,OtherManufacturing,
		  Electricity,WaterSewerageRemediationActivitie,Construction,TransportationAndStorage,Communication,
		  Information,SuezcCanal,WholesaleAndRetailTrade,FinancialIntermediariesAuxiliaryServices,
		  SocialSecurityAndInsurance,AccommodationAndFoodServiceActivities,RealEstateActivitie,
		  BusinessServices,GeneralGovernment,SocialServices,Education,Health,OtherServices,TotalGDPAtFactorCost) 

		 SELECT ActivityCurrents.Id, DFIndicators.NameAr,DFSources.NameAr,DFUnits.NameAr,null,
		 DFQuarters.NameAr,_year.NameAr,DFSectors.NameAr,AgricultureForestryFishing,
		  MiningQuarrying,Petroleum,Gas,OtherExtraction,ManufacturingIndustries,petroleumRefining,OtherManufacturing,
		  Electricity,WaterSewerageRemediationActivitie,Construction,TransportationAndStorage,Communication,
		  Information,SuezcCanal,WholesaleAndRetailTrade,FinancialIntermediariesAuxiliaryServices,
		  SocialSecurityAndInsurance,AccommodationAndFoodServiceActivities,RealEstateActivitie,
		  BusinessServices,GeneralGovernment,SocialServices,Education,Health,OtherServices,TotalGDPAtFactorCost
		  
		 from ActivityCurrents 

		 JOIN DFIndicators ON ActivityCurrents.DFIndicatorId=DFIndicators.Id
		 JOIN DFSources ON ActivityCurrents.DFSourceId=DFSources.Id
		 JOIN DFUnits ON ActivityCurrents.DFUnitId=DFUnits.Id
		 JOIN DFQuarters ON ActivityCurrents.DFQuarterId=DFQuarters.Id
		 JOIN DFYears _year ON ActivityCurrents.DFYearId=_year.Id
		 JOIN DFSectors  ON ActivityCurrents.DFSectorId=DFSectors.Id
		 where ActivityCurrents.DFYearId in (select * from @yearFiscalIdList)
		 and ActivityCurrents.DFQuarterId in (select * from @quarterIdList)
		 and ActivityCurrents.DFSectorId in (select * from @sectorList)
		   and (ActivityCurrents.IsDeleted is null or ActivityCurrents.IsDeleted=0)

		END
    END
	if(@lang ='en')
	begin
	IF(@constant=1)
		BEGIN
	
		  INSERT INTO @result (Id, Indicator,_Source,Unit,BaseYear,_Quarter,_Year,Sector,AgricultureForestryFishing,
		  MiningQuarrying,Petroleum,Gas,OtherExtraction,ManufacturingIndustries,petroleumRefining,OtherManufacturing,
		  Electricity,WaterSewerageRemediationActivitie,Construction,TransportationAndStorage,Communication,
		  Information,SuezcCanal,WholesaleAndRetailTrade,FinancialIntermediariesAuxiliaryServices,
		  SocialSecurityAndInsurance,AccommodationAndFoodServiceActivities,RealEstateActivitie,
		  BusinessServices,GeneralGovernment,SocialServices,Education,Health,OtherServices,TotalGDPAtFactorCost
		  ,RealGrowthRateUnit,RealGrowthRate,_ValueUnit,_value) 

		 SELECT ActivityConstants.Id, DFIndicators.NameEn,DFSources.NameEn,DFUnits.NameEn,base.NameEn,
		 DFQuarters.NameEn,_year.NameEn,DFSectors.NameEn,AgricultureForestryFishing,
		  MiningQuarrying,Petroleum,Gas,OtherExtraction,ManufacturingIndustries,petroleumRefining,OtherManufacturing,
		  Electricity,WaterSewerageRemediationActivitie,Construction,TransportationAndStorage,Communication,
		  Information,SuezcCanal,WholesaleAndRetailTrade,FinancialIntermediariesAuxiliaryServices,
		  SocialSecurityAndInsurance,AccommodationAndFoodServiceActivities,RealEstateActivitie,
		  BusinessServices,GeneralGovernment,SocialServices,Education,Health,OtherServices,TotalGDPAtFactorCost,d2.NameEn,
		  RGDPGrowthRates.GrowthRate,d3.NameEn,RGDPGrowthRates1617.[Value]
		  
		 from ActivityConstants 

		 JOIN DFIndicators ON ActivityConstants.DFIndicatorId=DFIndicators.Id
		 JOIN DFSources ON ActivityConstants.DFSourceId=DFSources.Id
		 JOIN DFUnits ON ActivityConstants.DFUnitId=DFUnits.Id
		 JOIN DFQuarters ON ActivityConstants.DFQuarterId=DFQuarters.Id
		 JOIN DFYears base ON ActivityConstants.DFYearBaseId=base.Id
		 JOIN DFYears _year ON ActivityConstants.DFYearId=_year.Id
		 JOIN DFSectors  ON ActivityConstants.DFSectorId=DFSectors.Id
		 left JOIN RGDPGrowthRates  ON @RGDP=1  and RGDPGrowthRates.DFQuarterId=ActivityConstants.DFQuarterId
		 and RGDPGrowthRates.DFYearId=ActivityConstants.DFYearId and ActivityConstants.DFSectorId=33
		   and (RGDPGrowthRates.IsDeleted is null or RGDPGrowthRates.IsDeleted=0)
		 left join DFUnits d2 on d2.Id=RGDPGrowthRates.DFUnitId
		 left JOIN RGDPGrowthRates1617  ON @RGDP1617=1    and RGDPGrowthRates1617.DFYearFiscalId=ActivityConstants.DFYearId
		 and RGDPGrowthRates1617.DFQuarterId=ActivityConstants.DFQuarterId and ActivityConstants.DFSectorId=33
		  and (RGDPGrowthRates1617.IsDeleted is null or RGDPGrowthRates1617.IsDeleted=0)
		 left join DFUnits d3 on d3.Id=RGDPGrowthRates1617.DFUnitId
		 where ActivityConstants.DFYearId in (select * from @yearFiscalIdList)
		 and ActivityConstants.DFQuarterId in (select * from @quarterIdList)
		 and ActivityConstants.DFSectorId in (select * from @sectorList)
		 and (ActivityConstants.IsDeleted is null or ActivityConstants.IsDeleted=0)


		 IF(@sectorRGDP=1)
		  BEGIN
		  INSERT INTO @result (Id, Indicator,_Source,Unit,BaseYear,_Quarter,_Year,Sector,AgricultureForestryFishing,
		  MiningQuarrying,Petroleum,Gas,OtherExtraction,ManufacturingIndustries,petroleumRefining,OtherManufacturing,
		  Electricity,WaterSewerageRemediationActivitie,Construction,TransportationAndStorage,Communication,
		  Information,SuezcCanal,WholesaleAndRetailTrade,FinancialIntermediariesAuxiliaryServices,
		  SocialSecurityAndInsurance,AccommodationAndFoodServiceActivities,RealEstateActivitie,
		  BusinessServices,GeneralGovernment,SocialServices,Education,Health,OtherServices,TotalGDPAtFactorCost,
		  TotalGDPGrowthRateAtFactorCost) 

		 SELECT SectorGrowthRates.Id, DFIndicators.NameEn,DFSources.NameEn,DFUnits.NameEn,null,
		 DFQuarters.NameEn,_year.NameEn,DFSectors.NameEn,AgricultureForestryFishing,
		  MiningQuarrying,Petroleum,Gas,OtherExtraction,ManufacturingIndustries,petroleumRefining,OtherManufacturing,
		  Electricity,WaterSewerageRemediationActivitie,Construction,TransportationAndStorage,Communication,
		  Information,SuezcCanal,WholesaleAndRetailTrade,FinancialIntermediariesAuxiliaryServices,
		  SocialSecurityAndInsurance,AccommodationAndFoodServiceActivities,RealEstateActivitie,
		  BusinessServices,GeneralGovernment,SocialServices,Education,Health,OtherServices,null,TotalGDPAtFactorCost
		  
		 from SectorGrowthRates 

		 JOIN DFIndicators ON SectorGrowthRates.DFIndicatorId=DFIndicators.Id
		 JOIN DFSources ON SectorGrowthRates.DFSourceId=DFSources.Id
		 JOIN DFUnits ON SectorGrowthRates.DFUnitId=DFUnits.Id
		 JOIN DFQuarters ON SectorGrowthRates.DFQuarterId=DFQuarters.Id
		 JOIN DFYears _year ON SectorGrowthRates.DFYearId=_year.Id
		 JOIN DFSectors  ON SectorGrowthRates.DFSectorId=DFSectors.Id
		 where SectorGrowthRates.DFYearId in (select * from @yearFiscalIdList)
		 and SectorGrowthRates.DFQuarterId in (select * from @quarterIdList)
		 and SectorGrowthRates.DFSectorId in (select * from @sectorList)
		 and (SectorGrowthRates.IsDeleted is null or SectorGrowthRates.IsDeleted=0)
		 END

		END


		IF(@current=1)
		BEGIN

		  INSERT INTO @result (Id, Indicator,_Source,Unit,BaseYear,_Quarter,_Year,Sector,AgricultureForestryFishing,
		  MiningQuarrying,Petroleum,Gas,OtherExtraction,ManufacturingIndustries,petroleumRefining,OtherManufacturing,
		  Electricity,WaterSewerageRemediationActivitie,Construction,TransportationAndStorage,Communication,
		  Information,SuezcCanal,WholesaleAndRetailTrade,FinancialIntermediariesAuxiliaryServices,
		  SocialSecurityAndInsurance,AccommodationAndFoodServiceActivities,RealEstateActivitie,
		  BusinessServices,GeneralGovernment,SocialServices,Education,Health,OtherServices,TotalGDPAtFactorCost) 

		 SELECT ActivityCurrents.Id, DFIndicators.NameEn,DFSources.NameEn,DFUnits.NameEn,null,
		 DFQuarters.NameEn,_year.NameEn,DFSectors.NameEn,AgricultureForestryFishing,
		  MiningQuarrying,Petroleum,Gas,OtherExtraction,ManufacturingIndustries,petroleumRefining,OtherManufacturing,
		  Electricity,WaterSewerageRemediationActivitie,Construction,TransportationAndStorage,Communication,
		  Information,SuezcCanal,WholesaleAndRetailTrade,FinancialIntermediariesAuxiliaryServices,
		  SocialSecurityAndInsurance,AccommodationAndFoodServiceActivities,RealEstateActivitie,
		  BusinessServices,GeneralGovernment,SocialServices,Education,Health,OtherServices,TotalGDPAtFactorCost
		  
		 from ActivityCurrents 

		 JOIN DFIndicators ON ActivityCurrents.DFIndicatorId=DFIndicators.Id
		 JOIN DFSources ON ActivityCurrents.DFSourceId=DFSources.Id
		 JOIN DFUnits ON ActivityCurrents.DFUnitId=DFUnits.Id
		 JOIN DFQuarters ON ActivityCurrents.DFQuarterId=DFQuarters.Id
		 JOIN DFYears _year ON ActivityCurrents.DFYearId=_year.Id
		 JOIN DFSectors  ON ActivityCurrents.DFSectorId=DFSectors.Id
		 where ActivityCurrents.DFYearId in (select * from @yearFiscalIdList)
		 and ActivityCurrents.DFQuarterId in (select * from @quarterIdList)
		 and ActivityCurrents.DFSectorId in (select * from @sectorList)
		  and (ActivityCurrents.IsDeleted is null or ActivityCurrents.IsDeleted=0)
		END

	END

    RETURN 
END