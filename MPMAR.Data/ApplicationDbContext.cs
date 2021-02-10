using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MPMAR.Analytics.Data;
using MPMAR.Data.Helpers;
using MPMAR.Data.HomePageModels;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for Application Database Context which initialize all database tables
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddNavigationProperties();
            // modelBuilder.Seed();
        }

        public DbSet<NavItem> NavItems { get; set; }
        public DbSet<NavItemVersion> NavItemVersions { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<PageRoute> PageRoutes { get; set; }
        public DbSet<PageRouteVersion> PageRouteVersions { get; set; }
        public DbSet<PageSection> PageSections { get; set; }
        public DbSet<PageSectionVersion> PageSectionVersions { get; set; }
        public DbSet<PageSectionCard> PageSectionCards { get; set; }
        public DbSet<PageSectionType> PageSectionTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<PageSectionCardVersion> PageSectionCardVersions { get; set; }
        public DbSet<PageMinistry> PageMinistry { get; set; }
        public DbSet<PageContact> PageContact { get; set; }
        public DbSet<FooterMenuItem> FooterMenuItem { get; set; }


        public DbSet<PageEventVersions> PageEventVersions { get; set; }
        public DbSet<PhotoArchive> PhotoArchive { get; set; }
        public DbSet<PhotosAlbum> PhotosAlbum { get; set; }
        public DbSet<EgyptVision> EgyptVision { get; set; }
        public DbSet<SocialMedia> SocialMedia { get; set; }
        public DbSet<MinistryTimeLine> MinistryTimeLine { get; set; }




        public DbSet<PageNewsType> PageNewsType { get; set; }
        public DbSet<PageNews> PageNews { get; set; }

        public DbSet<NewsTypesForNews> NewsTypesForNews { get; set; }

        public DbSet<FormerMinistriesPageInfo> FormerMinistriesPageInfos { get; set; }
        public DbSet<LeftMenuItem> LeftMenuItem { get; set; }
        public DbSet<FooterMenuTitle> FooterMenuTitles { get; set; }
        public DbSet<HomePagePhoto> homePagePhotos { get; set; }
        public DbSet<HomePagePhotoSlider> homePagePhotoSlider { get; set; }
        public DbSet<HomePageVideo> homePageVideos { get; set; }
        public DbSet<MinistryVission> MinistryVissions { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<EconomicDevelopment> EconomicDevelopments { get; set; }

        public DbSet<HomePageAffiliates> HomePageAffiliates { get; set; }
        public DbSet<Monitoring> Monitoring { get; set; }
        public DbSet<CitizenPlan> CitizenPlan { get; set; }

        public DbSet<ApprovalNotification> ApprovalNotifications { get; set; }
        public DbSet<FormerMinistriesPageInfoVersions> FormerMinistriesPageInfoVersions { get; set; }
        public DbSet<MinistryTimeLineVersions> MinistryTimeLineVersions { get; set; }
        public DbSet<EgyptVisionVersion> EgyptVisionVersions { get; set; }
        public DbSet<PageMinistryVersion> PageMinistryVersions { get; set; }
        public DbSet<CityPlan> CityPlan { get; set; }
        public DbSet<CityPlanYear> CityPlanYear { get; set; }
        public DbSet<DFGovernorate> DFGovernorates { get; set; }
        public DbSet<HomePagePhotoVersions> HomePagePhotoVersions { get; set; }
        public DbSet<HomePageVideoVersions> HomePageVideoVersions { get; set; }
        public DbSet<MinistryVissionVersion> MinistryVissionVersions { get; set; }
        public DbSet<SiteMap> SiteMap { get; set; }
        public DbSet<CitizenPlanVersions> CitizenPlanVersions { get; set; }
        public DbSet<HomePagePhotoSliderVersion> HomePagePhotoSliderVersions { get; set; }
        public DbSet<EconomicDevelopmentVersions> EconomicDevelopmentVersions { get; set; }
        public DbSet<PublicationVersions> PublicationVersions { get; set; }
        public DbSet<MonitoringVersions> MonitoringVersions { get; set; }
        public DbSet<HomePageAffiliatesVersions> HomePageAffiliatesVersions { get; set; }
        public DbSet<SocialMediaVersion> SocialMediaVersions { get; set; }
        public DbSet<PageContactVersions> PageContactVersions { get; set; }
        public DbSet<FooterMenuItemVersion> FooterMenuItemVersions { get; set; }
        public DbSet<FooterMenuTitleVersions> FooterMenuTitleVersions { get; set; }
        public DbSet<LeftMenuItemVersions> LeftMenuItemVersions { get; set; }
        public DbSet<CityPlanVersion> CityPlanVersions { get; set; }
        public DbSet<CityPlanYearVersion> CityPlanYearVersions { get; set; }
        public DbSet<PageNewsVersion> PageNewsVersions { get; set; }
        public DbSet<NewsTypesForNewsVersion> NewsTypesForNewsVersions { get; set; }
        public DbSet<HomePageLogoLink> HomePageLogoLinks { get; set; }
        public DbSet<HomePageLogoLinkVersions> HomePageLogoLinkVersions { get; set; }
        public DbSet<PhotoArchiveVersion> PhotoArchiveVersions { get; set; }
        public DbSet<PageEvent> PageEvents { get; set; }
        public DbSet<EconomicIndicators> EconomicIndicators { get; set; }
        public DbSet<EconomicIndicatorsVersion> EconomicIndicatorsVersion { get; set; }
        public DbSet<HomePageBasicInfo> HomePageBasicInfo { get; set; }
        public DbSet<PhotosAlbumVersion> PhotosAlbumVersions { get; set; }
        public DbSet<DFGov> DFGovs { get; set; }
        public DbSet<BEUsersPrivileges> BEUsersPrivileges { get; set; }
    }

}