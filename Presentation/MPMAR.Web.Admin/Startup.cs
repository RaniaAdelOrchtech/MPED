using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using MPMAR.Entities;
using MPMAR.Business;
using MPMAR.Data.Helpers;
using NLog;
using MPMAR.Data;
using MPMAR.Common;
using NToastNotify;
using Sotsera.Blazor.Toaster.Core.Models;
using Microsoft.AspNetCore.Http;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Services;
using MPMAR.Common.Helpers;
using Microsoft.AspNetCore.Routing;
using MPMAR.Analytics.Data;
using MPMAR.Business.Interfaces.Analytics;
using MPMAR.Business.Services.Analytics;
using MPMAR.Web.Admin.Helpers;
using MPMAR.Common.Interfaces;
using MPMAR.Common.Utility;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http.Features;
using MPMAR.Web.Admin.AuthRequirement;
using Microsoft.AspNetCore.Authorization;
using MPMAR.Web.Admin.AuthHandler;
using MPMAR.Web.Admin.Services;

namespace MPMAR.Web.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AnalyticsDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("AnalyticsConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 10;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredUniqueChars = 1;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc().AddNToastNotifyNoty(new NotyOptions
            {
                ProgressBar = true,
                Timeout = 6000,
                Theme = "sunset",
                CloseWith = new string[2] { "click", "button" }
            }).AddRazorRuntimeCompilation();
            services.AddToaster(config =>
            {
                //example customizations
                config.PositionClass = Defaults.Classes.Position.TopRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = false;
            });


            services.AddSingleton<IPageNewsElasticSearchService, PageNewsElasticSearchService>();
            services.AddSingleton<IPhotoArchiveElasticSearchService, PhotoArchiveElasticSearchService>();
            services.AddSingleton<IGlobalElasticSearchService, GlobalElasticSearchService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddElasticsearch(Configuration);

            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IUserManagmentRepository, UserManagmentRepository>();
            services.AddScoped<INavItemRepository, NavItemRepository>();
            services.AddScoped<INavItemVersionRepository, NavItemVersionRepository>();
            services.AddScoped<IPageRouteRepository, PageRouteRepository>();
            services.AddScoped<IPageRouteVersionRepository, PageRouteVersionRepository>();
            services.AddScoped<IContactUsRepository, ContactRepository>();
            services.AddScoped<IPageSectionRepository, PageSectionRepository>();
            services.AddScoped<IPageSectionVersionRepository, PageSectionVersionRepository>();
            services.AddScoped<ISectionCardVersionRepository, SectionCardVersionRepository>();
            services.AddScoped<IPageMinistryRepository, PageMinistryRepository>();
            services.AddScoped<IPageContactRepository, PageContactRepository>();
            services.AddScoped<IFooterMenuItemRepository, FooterMenuItemRepository>();
            services.AddScoped<IDFIndicatorRepository, DFIndicatorRepository>();
            services.AddScoped<IDFYearsRepository, DFYearsRepository>();
            services.AddScoped<IDFGovernoratesRepository, DFGovernoratesRepository>();

            services.AddScoped<IPageEventVersionsRepository, PageEventVersionsRepository>();

            services.AddScoped<IGovernorateRepository, GovernorateRepository>();

            services.AddScoped<IFileService, FileService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<DataInitializer, DataInitializer>();
            services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
            services.AddScoped<IPhotoArchiveRepository, PhotoArchiveRepository>();
            services.AddScoped<IPhotosAlbumRepository, PhotosAlbumRepository>();
            services.AddScoped<IEgyptVisionRepository, EgyptVisionRepository>();
            services.AddScoped<IPageNewsRepository, PageNewsRepository>();
            services.AddScoped<IPageNewsTypeRepository, PageNewsTypeRepository>();
            services.AddScoped<INewsTypesForNewsRepository, NewsTypesForNewsRepository>();
            services.AddScoped<IMinistryTimeLineRepository, MinistryTimeLineRepository>();
            services.AddScoped<IFormerMinistriesPageInfoRepository, FormerMinistriesPageInfoRepository>();
            services.Configure<RouteOptions>(options =>
            options.ConstraintMap.Add("username", typeof(UserNameRouteConstraint)));
            services.AddScoped<ILeftMenuItemRepository, LeftMenuItemRepository>();
            services.AddScoped<IInvestmentRepository, InvestmentRepository>();
            services.AddScoped<IDFQuartersRepository, DFQuartersRepository>();
            services.AddScoped<IDFUnitRepository, DFUnitRepository>();
            services.AddScoped<IDFSourceRepository, DFSourceRepository>();
            services.AddScoped<IComponentConstantRepository, ComponentConstantRepository>();
            services.AddScoped<IComponentCurrenttRepository, ComponentCurrenttRepository>();
            services.AddScoped<IActivityConstantRepository, ActivityConstantRepository>();
            services.AddScoped<IActivityCurrentRepository, ActivityCurrentRepository>();
            services.AddScoped<IDFSectorsRepository, DFSectorsRepository>();
            services.AddScoped<ISectorGrowthRepository, SectorGrowthRepository>();
            services.AddScoped<IRGDPRepository, RGDPRepository>();
            services.AddScoped<IRGDP1617Repository, RGDP1617Repository>();
            services.AddScoped<IFooterMenuTitleRepository, FooterMenuTitleRepository>();
            services.AddScoped<IMinistryVisionRepository, MinistryVisionRepository>();
            services.AddScoped<IPublicationRepository, PublicationRepository>();
            services.AddScoped<IHP_PhotosReopsitory, HP_PhotosReopsitory>();
            services.AddScoped<IHP_VideoReopsitory, HP_VideoReopsitory>();
            services.AddScoped<IHP_PhotoSliderReopsitory, HP_PhotoSliderReopsitory>();
            services.AddScoped<IHP_EconomicDevelopmentReopsitory, HP_EconomicDevelopmentReopsitory>();
            services.AddScoped<IHP_AffiliatesReopsitory, HP_AffiliatesReopsitory>();
            services.AddScoped<IMonitoringRepository, MonitoringRepository>();
            services.AddScoped<ICitizenPlanRepository, CitizenPlanRepository>();
            services.AddScoped<IApprovalNotificationsRepository, ApprovalNotificationsRepository>();
            services.AddScoped<IFormerMinistriesPageInfoVersionRepository, FormerMinistriesPageInfoVersionRepository>();
            services.AddScoped<IMinistryTimeLineVersionsRepository, MinistryTimeLineVersionsRepository>();
            services.AddScoped<IEgyptVisionVersionRepository, EgyptVisionVersionRepository>();
            services.AddScoped<IPageMinistryVersionsRepository, PageMinistryVersionsRepository>();

            services.AddScoped<ICityPlanRepository, CityPlanRepository>();
            services.AddScoped<ICityPlanYearRepository, CityPlanYearRepository>();
            services.AddScoped<HTMLFileHelper, HTMLFileHelper>();
            services.AddScoped<IHP_PhotosVersionRepository, HP_PhotosVersionRepository>();
            services.AddScoped<IHP_VideoVersionRepository, HP_VideoVersionRepository>();
            services.AddScoped<IMinistryVisionVersionRepository, MinistryVisionVersionRepository>();
            services.AddScoped<IHP_PhotoSliderVersionReopsitory, HP_PhotoSliderVersionReopsitory>();

            services.AddScoped<ICitizenPlanVersionsRepository, CitizenPlanVersionsRepository>();

            services.AddScoped<ISiteMapRepository, SiteMapRepository>();
            services.AddScoped<IHP_EconomicDevelopmentVersionRepository, HP_EconomicDevelopmentVersionRepository>();
            services.AddScoped<IPublicationVersionsRepository, PublicationVersionsRepository>();
            services.AddScoped<IMonitoringVersionsRepository, MonitoringVersionsRepository>();
            services.AddScoped<IHP_AffiliatesVersionReopsitory, HP_AffiliatesVersionReopsitory>();
            services.AddScoped<IPageContactVersionRepository, PageContactVersionRepository>();
            services.AddScoped<ISocialMediaVersionRepository, SocialMediaVersionRepository>();
            services.AddScoped<IFooterMenuTitleVersionsRepository, FooterMenuTitleVersionsRepository>();
            services.AddScoped<ILeftMenuItemsVersionsRepository, LeftMenuItemsVersionsRepository>();
            services.AddScoped<IFooterMenuItemVersionRepository, FooterMenuItemVersionRepository>();

            services.AddScoped<ICityPlanVersionRepository, CityPlanVersionRepository>();
            services.AddScoped<ICityPlanYearVersionRepository, CityPlanYearVersionRepository>();
            services.AddScoped<IHP_LogoLinkReopsitory, HP_LogoLinkReopsitory>();
            services.AddScoped<IHP_LogoLinkVersionRepository, HP_LogoLinkVersionRepository>();
            services.AddScoped<IEconomicIndicatorRepository, EconomicIndicatorReopsitory>();
            services.AddScoped<IEconomicIndicatorVersionsRepository, EconomicIndicatorVersionsRepository>();
            services.AddScoped<IHP_BasicInfoReopsitory, HP_BasicInfoReopsitory>();
            services.AddScoped<IDFGovRepository, DFGovRepository>();
            services.AddTransient<IMyEmailSender, MyEmailSender>();
            services.AddScoped<IBEUsersPrivilegesRepository, BEUsersPrivilegesRepository>();
            services.AddScoped<IBEUsersPrivilegesService, BEUsersPrivilegesService>();

            services.AddScoped(typeof(IEventLogger<>), typeof(EventLogger<>));
            services.Configure<FormOptions>(options =>

            {

                options.ValueLengthLimit = int.MaxValue;

                options.MultipartBodyLengthLimit = int.MaxValue;

                options.MultipartHeadersLengthLimit = int.MaxValue;

            });

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("BEUsersPrivileges", policy =>
                //    policy.AddRequirements(new BEUsersPrivilegesRequirement()));

            });

     //       services.AddScoped<IAuthorizationHandler,
     //BEUsersPrivilegesHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataInitializer dataInitializer)
        {
            GlobalDiagnosticsContext.Set("configDir", "C:\\git\\MPMAR\\Logs");
            GlobalDiagnosticsContext.Set("connectionString", Configuration.GetConnectionString("DefaultConnection"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseNToastNotify();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            dataInitializer.Initialize();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}",
                    new { controller = "Home", action = "Index" },
                    new { controller = @"^(?!User).*$" });

                endpoints.MapControllerRoute("user",
        "{username:username}/{action=Index}",
        new { controller = "TestDynamic" },
        new { controller = @"TestDynamic" }// only work user controller 
     );

                endpoints.MapRazorPages();
            });
        }
    }
    public class UserNameRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // check nulls
            object value;
            if (values.TryGetValue(routeKey, out value) && value != null)
            {

                return true;
            }

            return false;
        }
    }
}
