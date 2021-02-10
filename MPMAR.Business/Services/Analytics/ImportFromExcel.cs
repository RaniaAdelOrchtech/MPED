
using ExcelDataReader;
using MPMAR.Analytics.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MPMAR.Business.Interfaces;
using MPMAR.Analytics.Data.Enums;

namespace MPMAR.Business.Services
{
    public class ImportFromExcel : IImportFromExcel
    {
        private readonly AnalyticsDbContext _db;
        public ImportFromExcel(AnalyticsDbContext db)
        {
            _db = db;
        }
        public void ActivityCurrent(string filePath, int rowsCount, int sheetNum = 2)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var activityCurrents = new List<ActivityCurrent>();
            var IDs = new Dictionary<string, int>();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var sheet = 0;

                    do
                    {
                        if (sheetNum == sheet)
                        {
                            var row = 0;
                            while (reader.Read()) //Each ROW
                            {
                                var activityCurrent = new ActivityCurrent();
                                for (int column = 0; column < 39; column++)
                                {
                                    if (row > 0 && row < rowsCount)
                                    {
                                        var currentData = reader.GetValue(column).ToString();

                                        if (column == 1)// indicator
                                        {
                                            var indicatorId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                indicatorId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var indicator = _db.DFIndicators.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (indicator != null)
                                                {
                                                    indicatorId = indicator.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), indicator.Id);
                                                }
                                            }
                                            activityCurrent.DFIndicatorId = indicatorId;
                                        }

                                        else if (column == 3)// source
                                        {
                                            var sourceId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                sourceId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var source = _db.DFSources.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (source != null)
                                                {
                                                    sourceId = source.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), source.Id);
                                                }
                                            }
                                            activityCurrent.DFSourceId = sourceId;

                                        }
                                        else if (column == 5)// unit
                                        {
                                            var unitId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                unitId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var unit = _db.DFUnits.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (unit != null)
                                                {
                                                    unitId = unit.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), unit.Id);

                                                }
                                            }
                                            activityCurrent.DFUnitId = unitId;

                                        }

                                        else if (column == 7)// quarter
                                        {
                                            var quarterId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                quarterId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var quarter = _db.DFQuarters.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (quarter != null)
                                                {
                                                    quarterId = quarter.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), quarter.Id);
                                                }
                                            }
                                            activityCurrent.DFQuarterId = quarterId;

                                        }
                                        else if (column == 8)// years
                                        {
                                            var years = currentData.Split("/");
                                            if (int.Parse(years[0]) < int.Parse(years[1]))
                                            {
                                                currentData = years[1] + "/" + years[0];
                                            }
                                            var yearId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                yearId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var year = _db.DFYears.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (year != null)
                                                {
                                                    yearId = year.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), year.Id);
                                                }

                                            }
                                            activityCurrent.DFYearId = yearId;

                                        }
                                        else if (column == 10)// sector
                                        {
                                            var sectorId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower() + "_SECTOR"))
                                                sectorId = IDs[currentData.Trim().ToLower() + "_SECTOR"];
                                            else
                                            {
                                                var sector = _db.DFSectors.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (sector != null)
                                                {
                                                    sectorId = sector.Id;
                                                    // concatenate sector as thera is other value with the same key
                                                    IDs.Add(currentData.Trim().ToLower() + "_SECTOR", sector.Id);
                                                }

                                            }
                                            activityCurrent.DFSectorId = sectorId;
                                        }
                                        else if (column == 11)
                                            activityCurrent.AgricultureForestryFishing = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 12)
                                            activityCurrent.MiningQuarrying = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 13)
                                            activityCurrent.Petroleum = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 14)
                                            activityCurrent.Gas = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 15)
                                            activityCurrent.OtherExtraction = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 16)
                                            activityCurrent.ManufacturingIndustries = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;



                                        else if (column == 17)
                                            activityCurrent.petroleumRefining = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 18)
                                            activityCurrent.OtherManufacturing = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 19)
                                            activityCurrent.Electricity = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 20)
                                            activityCurrent.WaterSewerageRemediationActivitie = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 21)
                                            activityCurrent.Construction = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 22)
                                            activityCurrent.TransportationAndStorage = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 23)
                                            activityCurrent.Communication = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 24)
                                            activityCurrent.Information = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 25)
                                            activityCurrent.SuezcCanal = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 26)
                                            activityCurrent.WholesaleAndRetailTrade = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 27)
                                            activityCurrent.FinancialIntermediariesAuxiliaryServices = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 28)
                                            activityCurrent.SocialSecurityAndInsurance = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 29)
                                            activityCurrent.AccommodationAndFoodServiceActivities = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 30)
                                            activityCurrent.RealEstateActivitie = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 31)
                                            activityCurrent.RealEstateOwnership = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 32)
                                            activityCurrent.BusinessServices = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 33)
                                            activityCurrent.GeneralGovernment = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 34)
                                            activityCurrent.SocialServices = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;

                                        else if (column == 35)
                                            activityCurrent.Education = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 36)
                                            activityCurrent.Health = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 37)
                                            activityCurrent.OtherServices = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 38)
                                            activityCurrent.TotalGDPAtFactorCost = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;

                                    }


                                }
                                if (row > 0 && row < rowsCount)
                                    activityCurrents.Add(activityCurrent);
                                row++;
                            }
                        }
                        sheet++;

                    } while (reader.NextResult()); //Move to NEXT SHEET

                    _db.ActivityCurrents.AddRange(activityCurrents);
                    _db.SaveChanges();

                }
            }
        }

        public void ActivityConstant(string filePath, int rowsCount, int sheetNum = 2)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var activityConstants = new List<ActivityConstant>();
            var IDs = new Dictionary<string, int>();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var sheet = 0;

                    do
                    {


                        if (sheetNum == sheet)
                        {
                            var row = 0;
                            while (reader.Read()) //Each ROW
                            {
                                var activityConstant = new ActivityConstant();
                                for (int column = 0; column < 39; column++)
                                {
                                    if (row > 0 && row < rowsCount)
                                    {
                                        var currentData = reader.GetValue(column).ToString();

                                        if (column == 1)// indicator
                                        {
                                            var indicatorId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                indicatorId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var indicator = _db.DFIndicators.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (indicator != null)
                                                {
                                                    indicatorId = indicator.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), indicator.Id);
                                                }
                                            }
                                            activityConstant.DFIndicatorId = indicatorId;
                                        }

                                        else if (column == 3)// source
                                        {
                                            var sourceId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                sourceId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var source = _db.DFSources.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (source != null)
                                                {
                                                    sourceId = source.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), source.Id);
                                                }
                                            }
                                            activityConstant.DFSourceId = sourceId;

                                        }
                                        else if (column == 5)// unit
                                        {
                                            var unitId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                unitId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var unit = _db.DFUnits.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (unit != null)
                                                {
                                                    unitId = unit.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), unit.Id);

                                                }
                                            }
                                            activityConstant.DFUnitId = unitId;

                                        }

                                        else if (column == 7)// quarter
                                        {
                                            var quarterId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                quarterId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var quarter = _db.DFQuarters.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (quarter != null)
                                                {
                                                    quarterId = quarter.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), quarter.Id);
                                                }
                                            }
                                            activityConstant.DFQuarterId = quarterId;

                                        }
                                        else if (column == 8)// years
                                        {
                                            var years = currentData.Split("/");
                                            if (int.Parse(years[0]) < int.Parse(years[1]))
                                            {
                                                currentData = years[1] + "/" + years[0];
                                            }

                                            var yearId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                yearId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var year = _db.DFYears.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (year != null)
                                                {
                                                    yearId = year.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), year.Id);
                                                }

                                            }
                                          
                                                activityConstant.DFYearId = yearId;
                                           

                                        }
                                        else if (column == 10)// sector
                                        {
                                            var sectorId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower() + "_SECTOR"))
                                                sectorId = IDs[currentData.Trim().ToLower() + "_SECTOR"];
                                            else
                                            {
                                                var sector = _db.DFSectors.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (sector != null)
                                                {
                                                    sectorId = sector.Id;
                                                    // concatenate sector as thera is other value with the same key
                                                    IDs.Add(currentData.Trim().ToLower() + "_SECTOR", sector.Id);
                                                }

                                            }
                                            activityConstant.DFSectorId = sectorId;
                                        }
                                        else if (column == 11)
                                            activityConstant.AgricultureForestryFishing = double.Parse(currentData.Trim());
                                        else if (column == 12)
                                            activityConstant.MiningQuarrying = double.Parse(currentData.Trim());
                                        else if (column == 13)
                                            activityConstant.Petroleum = double.Parse(currentData.Trim());
                                        else if (column == 14)
                                            activityConstant.Gas = double.Parse(currentData.Trim());
                                        else if (column == 15)
                                            activityConstant.OtherExtraction = double.Parse(currentData.Trim());
                                        else if (column == 16)
                                            activityConstant.ManufacturingIndustries = double.Parse(currentData.Trim());



                                        else if (column == 17)
                                            activityConstant.petroleumRefining = double.Parse(currentData.Trim());
                                        else if (column == 18)
                                            activityConstant.OtherManufacturing = double.Parse(currentData.Trim());
                                        else if (column == 19)
                                            activityConstant.Electricity = double.Parse(currentData.Trim());
                                        else if (column == 20)
                                            activityConstant.WaterSewerageRemediationActivitie = double.Parse(currentData.Trim());
                                        else if (column == 21)
                                            activityConstant.Construction = double.Parse(currentData.Trim());
                                        else if (column == 22)
                                            activityConstant.TransportationAndStorage = double.Parse(currentData.Trim());
                                        else if (column == 23)
                                            activityConstant.Communication = double.Parse(currentData.Trim());
                                        else if (column == 24)
                                            activityConstant.Information = double.Parse(currentData.Trim());
                                        else if (column == 25)
                                            activityConstant.SuezcCanal = double.Parse(currentData.Trim());
                                        else if (column == 26)
                                            activityConstant.WholesaleAndRetailTrade = double.Parse(currentData.Trim());
                                        else if (column == 27)
                                            activityConstant.FinancialIntermediariesAuxiliaryServices = double.Parse(currentData.Trim());
                                        else if (column == 28)
                                            activityConstant.SocialSecurityAndInsurance = double.Parse(currentData.Trim());
                                        else if (column == 29)
                                            activityConstant.AccommodationAndFoodServiceActivities = double.Parse(currentData.Trim());
                                        else if (column == 30)
                                            activityConstant.RealEstateActivitie = double.Parse(currentData.Trim());
                                        else if (column == 31)
                                            activityConstant.RealEstateOwnership = double.Parse(currentData.Trim());
                                        else if (column == 32)
                                            activityConstant.BusinessServices = double.Parse(currentData.Trim());
                                        else if (column == 33)
                                            activityConstant.GeneralGovernment = double.Parse(currentData.Trim());
                                        else if (column == 34)
                                            activityConstant.SocialServices = double.Parse(currentData.Trim());

                                        else if (column == 35)
                                            activityConstant.Education = double.Parse(currentData.Trim());
                                        else if (column == 36)
                                            activityConstant.Health = double.Parse(currentData.Trim());
                                        else if (column == 37)
                                            activityConstant.OtherServices = double.Parse(currentData.Trim());
                                        else if (column == 38)
                                            activityConstant.TotalGDPAtFactorCost = double.Parse(currentData.Trim());

                                    }


                                }
                                if (row > 0 && row < rowsCount)
                                    activityConstants.Add(activityConstant);
                                row++;
                            }
                        }
                        sheet++;

                    } while (reader.NextResult()); //Move to NEXT SHEET

                    _db.ActivityConstants.AddRange(activityConstants);
                    _db.SaveChanges();

                }
            }
        }

        public void ComponentConstant(string filePath, int rowsCount, int sheetNum = 0)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var componentConstants = new List<ComponentConstant>();
            var IDs = new Dictionary<string, int>();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var sheet = 0;

                    do
                    {


                        if (sheetNum == sheet)
                        {
                            var row = 0;
                            while (reader.Read()) //Each ROW
                            {
                                var componentConstant = new ComponentConstant();
                                for (int column = 0; column < 15; column++)
                                {
                                    if (row > 0 && row < rowsCount)
                                    {
                                        var currentData = reader.GetValue(column).ToString();

                                        if (column == 1)// indicator
                                        {
                                            var indicatorId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                indicatorId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var indicator = _db.DFIndicators.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (indicator != null)
                                                {
                                                    indicatorId = indicator.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), indicator.Id);
                                                }
                                            }
                                            componentConstant.DFIndicatorId = indicatorId;
                                        }

                                        else if (column == 3)// source
                                        {
                                            var sourceId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                sourceId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var source = _db.DFSources.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (source != null)
                                                {
                                                    sourceId = source.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), source.Id);
                                                }
                                            }
                                            componentConstant.DFSourceId = sourceId;

                                        }
                                        else if (column == 5)// unit
                                        {
                                            var unitId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                unitId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var unit = _db.DFUnits.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (unit != null)
                                                {
                                                    unitId = unit.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), unit.Id);

                                                }
                                            }
                                            componentConstant.DFUnitId = unitId;

                                        }

                                        else if (column == 7)// quarter
                                        {
                                            var quarterId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                quarterId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var quarter = _db.DFQuarters.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (quarter != null)
                                                {
                                                    quarterId = quarter.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), quarter.Id);
                                                }
                                            }
                                            componentConstant.DFQuarterId = quarterId;

                                        }
                                        else if (column == 8)// years
                                        {
                                            var years = currentData.Split("/");
                                            if (int.Parse(years[0]) < int.Parse(years[1]))
                                            {
                                                currentData = years[1] + "/" + years[0];
                                            }

                                            var yearId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                yearId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var year = _db.DFYears.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (year != null)
                                                {
                                                    yearId = year.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), year.Id);
                                                }

                                            }
                                            componentConstant.DFYearFiscalId = yearId;


                                        }
                                        else if (column == 9)
                                            componentConstant.PrivateConsumption = double.Parse(currentData.Trim());
                                        else if (column == 10)
                                            componentConstant.GovernmentConsumption = double.Parse(currentData.Trim());
                                        else if (column == 11)
                                            componentConstant.GrossCapitalFormation = double.Parse(currentData.Trim());
                                        else if (column == 12)
                                            componentConstant.ExportsOfGoodsAndServices = double.Parse(currentData.Trim());
                                        else if (column == 13)
                                            componentConstant.ImportsOfGoodsAndServices = double.Parse(currentData.Trim());
                                        else if (column == 14)
                                            componentConstant.TotalGrossDomesticProductAtMarketPrices = double.Parse(currentData.Trim());

                                    }


                                }
                                if (row > 0 && row < rowsCount)
                                    componentConstants.Add(componentConstant);
                                row++;
                            }
                        }
                        sheet++;

                    } while (reader.NextResult()); //Move to NEXT SHEET

                    _db.ComponentConstants.AddRange(componentConstants);
                    _db.SaveChanges();

                }
            }
        }

        public void ComponentCurrent(string filePath, int rowsCount, int sheetNum = 1)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var componentCurrents = new List<ComponentCurrent>();
            var IDs = new Dictionary<string, int>();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var sheet = 0;

                    do
                    {
                        if (sheetNum == sheet)
                        {
                            var row = 0;
                            while (reader.Read()) //Each ROW
                            {
                                var componentCurrent = new ComponentCurrent();
                                for (int column = 0; column < 15; column++)
                                {
                                    if (row > 0 && row < rowsCount)
                                    {
                                        var currentData = reader.GetValue(column).ToString();

                                        if (column == 1)// indicator
                                        {
                                            var indicatorId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                indicatorId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var indicator = _db.DFIndicators.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (indicator != null)
                                                {
                                                    indicatorId = indicator.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), indicator.Id);
                                                }
                                            }
                                            componentCurrent.DFIndicatorId = indicatorId;
                                        }

                                        else if (column == 3)// source
                                        {
                                            var sourceId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                sourceId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var source = _db.DFSources.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (source != null)
                                                {
                                                    sourceId = source.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), source.Id);
                                                }
                                            }
                                            componentCurrent.DFSourceId = sourceId;

                                        }
                                        else if (column == 5)// unit
                                        {
                                            var unitId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                unitId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var unit = _db.DFUnits.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (unit != null)
                                                {
                                                    unitId = unit.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), unit.Id);

                                                }
                                            }
                                            componentCurrent.DFUnitId = unitId;

                                        }

                                        else if (column == 7)// quarter
                                        {
                                            var quarterId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                quarterId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var quarter = _db.DFQuarters.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (quarter != null)
                                                {
                                                    quarterId = quarter.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), quarter.Id);
                                                }
                                            }
                                            componentCurrent.DFQuarterId = quarterId;

                                        }
                                        else if (column == 8)// years
                                        {
                                            var years = currentData.Split("/");
                                            if (int.Parse(years[0]) < int.Parse(years[1]))
                                            {
                                                currentData = years[1] + "/" + years[0];
                                            }

                                            var yearId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                yearId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var year = _db.DFYears.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (year != null)
                                                {
                                                    yearId = year.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), year.Id);
                                                }

                                            }
                                            componentCurrent.DFYearFiscalId = yearId;

                                        }
                                        else if (column == 9)
                                            componentCurrent.PrivateConsumption = double.Parse(currentData.Trim());
                                        else if (column == 10)
                                            componentCurrent.GovernmentConsumption = double.Parse(currentData.Trim());
                                        else if (column == 11)
                                            componentCurrent.GrossCapitalFormation = double.Parse(currentData.Trim());
                                        else if (column == 12)
                                            componentCurrent.ExportsOfGoodsAndServices = double.Parse(currentData.Trim());
                                        else if (column == 13)
                                            componentCurrent.ImportsOfGoodsAndServices = double.Parse(currentData.Trim());
                                        else if (column == 14)
                                            componentCurrent.TotalGrossDomesticProductAtMarketPrices = double.Parse(currentData.Trim());

                                    }


                                }
                                if (row > 0 && row < rowsCount)
                                    componentCurrents.Add(componentCurrent);
                                row++;
                            }
                        }
                        sheet++;

                    } while (reader.NextResult()); //Move to NEXT SHEET

                    _db.ComponentCurrents.AddRange(componentCurrents);
                    _db.SaveChanges();

                }
            }
        }

        public void RGDPGrowthRate(string filePath, int rowsCount, int sheetNum = 0)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var rGDPGrowthRates = new List<RGDPGrowthRate>();
            var IDs = new Dictionary<string, int>();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var sheet = 0;

                    do
                    {
                        if (sheetNum == sheet)
                        {
                            var row = 0;
                            while (reader.Read()) //Each ROW
                            {
                                var rGDPGrowthRate = new RGDPGrowthRate();
                                for (int column = 0; column < 9; column++)
                                {
                                    if (row > 0 && row < rowsCount)
                                    {
                                        var currentData = reader.GetValue(column).ToString();

                                        if (column == 1)// indicator
                                        {
                                            var indicatorId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                indicatorId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var indicator = _db.DFIndicators.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (indicator != null)
                                                {
                                                    indicatorId = indicator.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), indicator.Id);
                                                }
                                            }
                                            rGDPGrowthRate.DFIndicatorId = indicatorId;
                                        }

                                        else if (column == 3)// source
                                        {
                                            var sourceId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                sourceId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var source = _db.DFSources.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (source != null)
                                                {
                                                    sourceId = source.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), source.Id);
                                                }
                                            }
                                            rGDPGrowthRate.DFSourceId = sourceId;

                                        }
                                        else if (column == 4)// unit
                                        {
                                            rGDPGrowthRate.DFUnitId = (int)DFUnitEnum.Percentage;

                                        }

                                        else if (column == 6)// quarter
                                        {
                                            var quarterId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                quarterId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var quarter = _db.DFQuarters.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (quarter != null)
                                                {
                                                    quarterId = quarter.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), quarter.Id);
                                                }
                                            }
                                            rGDPGrowthRate.DFQuarterId = quarterId;

                                        }
                                        else if (column == 7)// years
                                        {
                                            var years = currentData.Split("/");
                                            if (int.Parse(years[0]) < int.Parse(years[1]))
                                            {
                                                currentData = years[1] + "/" + years[0];
                                            }

                                            var yearId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                yearId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var year = _db.DFYears.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (year != null)
                                                {
                                                    yearId = year.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), year.Id);
                                                }

                                            }
                                            rGDPGrowthRate.DFYearId = yearId;

                                        }
                                        else if (column == 8)
                                            rGDPGrowthRate.GrowthRate = double.Parse(currentData.Trim());

                                    }


                                }
                                if (row > 0 && row < rowsCount)
                                    rGDPGrowthRates.Add(rGDPGrowthRate);
                                row++;
                            }
                        }
                        sheet++;

                    } while (reader.NextResult()); //Move to NEXT SHEET

                    _db.RGDPGrowthRates.AddRange(rGDPGrowthRates);
                    _db.SaveChanges();

                }
            }
        }

        public void RGDPGrowthRate1617(string filePath, int rowsCount, int sheetNum = 0)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var rGDPGrowthRates = new List<RGDPGrowthRate1617>();
            var IDs = new Dictionary<string, int>();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var sheet = 0;

                    do
                    {
                        if (sheetNum == sheet)
                        {
                            var row = 0;
                            while (reader.Read()) //Each ROW
                            {
                                var rGDPGrowthRate = new RGDPGrowthRate1617();
                                for (int column = 0; column < 10; column++)
                                {
                                    if (row > 0 && row < rowsCount)
                                    {
                                        var currentData = reader.GetValue(column).ToString();

                                        if (column == 1)// indicator
                                        {
                                            var indicatorId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                indicatorId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var indicator = _db.DFIndicators.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (indicator != null)
                                                {
                                                    indicatorId = indicator.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), indicator.Id);
                                                }
                                            }
                                            rGDPGrowthRate.DFIndicatorId = indicatorId;
                                        }

                                        else if (column == 3)// source
                                        {
                                            var sourceId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                sourceId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var source = _db.DFSources.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (source != null)
                                                {
                                                    sourceId = source.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), source.Id);
                                                }
                                            }
                                            rGDPGrowthRate.DFSourceId = sourceId;

                                        }
                                        else if (column == 5)// unit
                                        {

                                            var unitId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                unitId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var unit = _db.DFUnits.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (unit != null)
                                                {
                                                    unitId = unit.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), unit.Id);

                                                }
                                            }
                                            rGDPGrowthRate.DFUnitId = unitId;

                                        }

                                        else if (column == 7)// quarter
                                        {
                                            var quarterId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                quarterId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var quarter = _db.DFQuarters.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (quarter != null)
                                                {
                                                    quarterId = quarter.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), quarter.Id);
                                                }
                                            }
                                            rGDPGrowthRate.DFQuarterId = quarterId;

                                        }
                                        else if (column == 8)// years
                                        {
                                            var yearId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                yearId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var year = _db.DFYears.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (year != null)
                                                {
                                                    yearId = year.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), year.Id);
                                                }

                                            }
                                            rGDPGrowthRate.DFYearFiscalId = yearId;

                                        }
                                        else if (column == 9)
                                            rGDPGrowthRate.Value = double.Parse(currentData.Trim());

                                    }


                                }
                                if (row > 0 && row < rowsCount)
                                    rGDPGrowthRates.Add(rGDPGrowthRate);
                                row++;
                            }
                        }
                        sheet++;

                    } while (reader.NextResult()); //Move to NEXT SHEET

                    _db.RGDPGrowthRates1617.AddRange(rGDPGrowthRates);
                    _db.SaveChanges();

                }
            }
        }

        public void SectorGrowthRate(string filePath, int rowsCount, int sheetNum = 0)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var sectorGrowthRates = new List<SectorGrowthRate>();
            var IDs = new Dictionary<string, int>();
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var sheet = 0;

                    do
                    {
                        if (sheetNum == sheet)
                        {
                            var row = 0;
                            while (reader.Read()) //Each ROW
                            {
                                var sectorGrowthRate = new SectorGrowthRate();
                                for (int column = 0; column < 38; column++)
                                {
                                    if (row > 0 && row < rowsCount)
                                    {
                                        var currentData = reader.GetValue(column).ToString();

                                        if (column == 1)// indicator
                                        {
                                            var indicatorId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                indicatorId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var indicator = _db.DFIndicators.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (indicator != null)
                                                {
                                                    indicatorId = indicator.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), indicator.Id);
                                                }
                                            }
                                            sectorGrowthRate.DFIndicatorId = indicatorId;
                                        }

                                        else if (column == 3)// source
                                        {
                                            var sourceId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                sourceId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var source = _db.DFSources.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (source != null)
                                                {
                                                    sourceId = source.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), source.Id);
                                                }
                                            }
                                            sectorGrowthRate.DFSourceId = sourceId;

                                        }
                                        else if (column == 4)// unit
                                        {

                                            sectorGrowthRate.DFUnitId = (int)DFUnitEnum.Percentage;

                                        }

                                        else if (column == 6)// quarter
                                        {
                                            var quarterId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                quarterId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var quarter = _db.DFQuarters.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (quarter != null)
                                                {
                                                    quarterId = quarter.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), quarter.Id);
                                                }
                                            }
                                            sectorGrowthRate.DFQuarterId = quarterId;

                                        }
                                        else if (column == 7)// years
                                        {
                                            var years = currentData.Split("/");
                                            if (int.Parse(years[0]) < int.Parse(years[1]))
                                            {
                                                currentData = years[1] + "/" + years[0];
                                            }

                                            var yearId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                yearId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var year = _db.DFYears.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (year != null)
                                                {
                                                    yearId = year.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), year.Id);
                                                }

                                            }
                                            sectorGrowthRate.DFYearId = yearId;

                                        }
                                        else if (column == 9)// sector
                                        {
                                            var sectorId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower() + "_SECTOR"))
                                                sectorId = IDs[currentData.Trim().ToLower() + "_SECTOR"];
                                            else
                                            {
                                                var sector = _db.DFSectors.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (sector != null)
                                                {
                                                    sectorId = sector.Id;
                                                    // concatenate sector as thera is other value with the same key
                                                    IDs.Add(currentData.Trim().ToLower() + "_SECTOR", sector.Id);
                                                }

                                            }
                                            sectorGrowthRate.DFSectorId = sectorId;
                                        }
                                        else if (column == 10)
                                            sectorGrowthRate.AgricultureForestryFishing = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 11)
                                            sectorGrowthRate.MiningQuarrying = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 12)
                                            sectorGrowthRate.Petroleum = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 13)
                                            sectorGrowthRate.Gas = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 14)
                                            sectorGrowthRate.OtherExtraction = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 15)
                                            sectorGrowthRate.ManufacturingIndustries = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;



                                        else if (column == 16)
                                            sectorGrowthRate.petroleumRefining = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 17)
                                            sectorGrowthRate.OtherManufacturing = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 18)
                                            sectorGrowthRate.Electricity = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 19)
                                            sectorGrowthRate.WaterSewerageRemediationActivitie = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 20)
                                            sectorGrowthRate.Construction = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 21)
                                            sectorGrowthRate.TransportationAndStorage = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 22)
                                            sectorGrowthRate.Communication = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 23)
                                            sectorGrowthRate.Information = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 24)
                                            sectorGrowthRate.SuezcCanal = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 25)
                                            sectorGrowthRate.WholesaleAndRetailTrade = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 26)
                                            sectorGrowthRate.FinancialIntermediariesAuxiliaryServices = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 27)
                                            sectorGrowthRate.SocialSecurityAndInsurance = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 28)
                                            sectorGrowthRate.AccommodationAndFoodServiceActivities = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 29)
                                            sectorGrowthRate.RealEstateActivitie = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 30)
                                            sectorGrowthRate.RealEstateOwnership = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 31)
                                            sectorGrowthRate.BusinessServices = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 32)
                                            sectorGrowthRate.GeneralGovernment = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 33)
                                            sectorGrowthRate.SocialServices = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;

                                        else if (column == 34)
                                            sectorGrowthRate.Education = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 35)
                                            sectorGrowthRate.Health = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 36)
                                            sectorGrowthRate.OtherServices = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 37)
                                            sectorGrowthRate.TotalGDPAtFactorCost = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;

                                    }


                                }
                                if (row > 0 && row < rowsCount)
                                    sectorGrowthRates.Add(sectorGrowthRate);
                                row++;
                            }
                        }
                        sheet++;

                    } while (reader.NextResult()); //Move to NEXT SHEET

                    _db.SectorGrowthRates.AddRange(sectorGrowthRates);
                    _db.SaveChanges();

                }
            }
        }

        public void Investments(string filePath, int rowsCount, int sheetNum = 0)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var investments = new List<Investments>();
            var IDs = new Dictionary<string, int>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var sheet = 0;

                    do
                    {
                        if (sheetNum == sheet)
                        {
                            var row = 0;
                            while (reader.Read()) //Each ROW
                            {
                                var investment = new Investments();
                                for (int column = 0; column < 29; column++)
                                {
                                    if (row > 0 && row < rowsCount)
                                    {
                                        var currentData = reader.GetValue(column).ToString();

                                        if (column == 1)// indicator
                                        {
                                            var indicatorId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                indicatorId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var indicator = _db.DFIndicators.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (indicator != null)
                                                {
                                                    indicatorId = indicator.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), indicator.Id);
                                                }
                                            }
                                            investment.DFIndicatorId = indicatorId;
                                        }

                                        else if (column == 3)// source
                                        {
                                            var sourceId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                sourceId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var source = _db.DFSources.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (source != null)
                                                {
                                                    sourceId = source.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), source.Id);
                                                }
                                            }
                                            investment.DFSourceId = sourceId;

                                        }
                                        else if (column == 5)// unit
                                        {
                                            var unitId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                unitId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var unit = _db.DFUnits.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (unit != null)
                                                {
                                                    unitId = unit.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), unit.Id);

                                                }
                                            }
                                            investment.DFUnitId = unitId;

                                        }

                                        else if (column == 7)// quarter
                                        {
                                            var quarterId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                quarterId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var quarter = _db.DFQuarters.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (quarter != null)
                                                {
                                                    quarterId = quarter.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), quarter.Id);
                                                }
                                            }
                                            investment.DFQuarterId = quarterId;

                                        }
                                        else if (column == 8)// years
                                        {
                                            var yearId = 0;
                                            if (IDs.ContainsKey(currentData.Trim().ToLower()))
                                                yearId = IDs[currentData.Trim().ToLower()];
                                            else
                                            {
                                                var year = _db.DFYears.FirstOrDefault(x => x.NameEn.Trim().ToLower() == currentData.Trim().ToLower());
                                                if (year != null)
                                                {
                                                    yearId = year.Id;
                                                    IDs.Add(currentData.Trim().ToLower(), year.Id);
                                                }

                                            }
                                            investment.DFYearId = yearId;

                                        }
                                        else if (column == 9)
                                            investment.Agriculture = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;

                                        else if (column == 10)
                                            investment.Petroleum = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 11)
                                            investment.NaturalGas = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 12)
                                            investment.OtherExtractions = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 13)
                                            investment.PetroleumRefining = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 14)
                                            investment.OtherManufacturing = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 15)
                                            investment.Electricity = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 16)
                                            investment.WaterAndSewerage = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 17)
                                            investment.Construction = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 18)
                                            investment.StorageAndTransportation = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 19)
                                            investment.InformationAndCommunication = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 20)
                                            investment.SuezCanal = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 21)
                                            investment.WholesaleAndRetailTrade = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 22)
                                            investment.FinancialIntermediaryInsuranceAndSocialSecurity = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 23)
                                            investment.AccommodationAndFoodServiceActivities = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 24)
                                            investment.RealEstateActivities = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 25)
                                            investment.Education = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 26)
                                            investment.Health = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 27)
                                            investment.OtherSrvices = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;
                                        else if (column == 28)
                                            investment.TotalInvestments = currentData.Trim().ToUpper() != "N/A" ? double.Parse(currentData.Trim()) : (double?)null;


                                    }


                                }
                                if (row > 0 && row < rowsCount)
                                    investments.Add(investment);
                                row++;
                            }
                        }
                        sheet++;

                    } while (reader.NextResult()); //Move to NEXT SHEET

                    _db.Investments.AddRange(investments);
                    _db.SaveChanges();

                }
            }
        }
    }
}
