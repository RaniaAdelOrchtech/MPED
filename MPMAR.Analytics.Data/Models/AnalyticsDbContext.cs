using Microsoft.EntityFrameworkCore;
using MPMAR.Analytics.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Analytics.Data
{
    public class AnalyticsDbContext : DbContext
    {
        public AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options) : base(options)
        {

        }


        public DbSet<StaticActivity> StaticActivities { get; set; }

        public DbSet<Governorate> Governorates { get; set; }

        public DbSet<DFGovernorate> DFGovernorates { get; set; }
        public DbSet<DFIndicator> DFIndicators { get; set; }
        public DbSet<DFQuarter> DFQuarters { get; set; }
        public DbSet<DFRegion> DFRegions { get; set; }
        public DbSet<DFSector> DFSectors { get; set; }
        public DbSet<DFYear> DFYears { get; set; }

        public DbSet<LocalizedColumnName> LocalizedColumnNames { get; set; }
        public DbSet<DFUnit> DFUnits { get; set; }
        public DbSet<DFSource> DFSources { get; set; }
        public DbSet<ComponentConstant> ComponentConstants { get; set; }
        public DbSet<ComponentCurrent> ComponentCurrents { get; set; }
        public DbSet<ActivityConstant> ActivityConstants { get; set; }
        public DbSet<ActivityCurrent> ActivityCurrents { get; set; }
        public DbSet<RGDPGrowthRate> RGDPGrowthRates { get; set; }
        public DbSet<RGDPGrowthRate1617> RGDPGrowthRates1617 { get; set; }
        public DbSet<SectorGrowthRate> SectorGrowthRates { get; set; }
        public DbSet<DFGDP> DFGDP { get; set; }
        public DbSet<GrossDomesticComponentViewModel> GrossDomesticComponentViewModel { get; set; }
        public DbSet<GrossDomesticActivity> GrossDomesticActivity { get; set; }
        public DbSet<Investments> Investments { get; set; }
        public DbSet<ComponentConstantVersion> ComponentConstantVersions { get; set; }
        public DbSet<ComponentCurrentVersion> ComponentCurrentVersions { get; set; }
        public DbSet<RGDPGrowthRateVersion> RGDPGrowthRateVersions { get; set; }
        public DbSet<ActivityCurrentVersion> ActivityCurrentVersions { get; set; }
        public DbSet<SectorGrowthRateVersion> SectorGrowthRateVersion { get; set; }
        public DbSet<InvestmentVersion> InvestmentVersions { get; set; }
        public DbSet<GovernorateVersion> GovernorateVersions { get; set; }

    }
}
