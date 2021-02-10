using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MPMAR.Analytics.Data;
using MPMAR.Business;
using MPMAR.Business.Interfaces;
using MPMAR.Business.Interfaces.Analytics;
using MPMAR.Business.Services;
using MPMAR.Business.Services.Analytics;
using MPMAR.Common.Utility;
using MPMAR.Data;
using NToastNotify;
using Sotsera.Blazor.Toaster.Core.Models;

namespace MPMAR.Web.Site
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
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
            services.AddMemoryCache();

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IPageNewsElasticSearchService, PageNewsElasticSearchService>();
            services.AddSingleton<IPhotoArchiveElasticSearchService, PhotoArchiveElasticSearchService>();
            services.AddSingleton<IGlobalElasticSearchService, GlobalElasticSearchService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddElasticsearch(Configuration);
            

            services.AddScoped<ILogRepository, LogRepository>();
          //  services.AddScoped<IUserManagmentRepository, UserManagmentRepository>();
            services.AddScoped<INavItemRepository, NavItemRepository>();
            services.AddScoped<INavItemVersionRepository, NavItemVersionRepository>();
            services.AddScoped<IContactUsRepository, ContactRepository>();
            services.AddScoped<IPageEventVersionsRepository, PageEventVersionsRepository>();
            services.AddScoped<IPhotoArchiveRepository, PhotoArchiveRepository>();
            services.AddScoped<IPhotosAlbumRepository, PhotosAlbumRepository>();

            services.AddScoped<IPageNewsRepository, PageNewsRepository>();
            services.AddScoped<IPageNewsTypeRepository, PageNewsTypeRepository>();
            services.AddScoped<INewsTypesForNewsRepository, NewsTypesForNewsRepository>();




            services.AddScoped<IGovernorateRepository, GovernorateRepository>();
            services.AddScoped<IImportFromExcel, ImportFromExcel>();
            services.AddScoped<IGrossDomesticRepository, GrossDomesticRepository>();
            services.AddScoped<IInvestmentRepository, InvestmentRepository>();


            services.AddScoped<IMinistryTimeLineRepository, MinistryTimeLineRepository>();
            services.AddScoped<IFormerMinistriesPageInfoRepository, FormerMinistriesPageInfoRepository>();
            services.AddScoped<IApprovalNotificationsRepository, ApprovalNotificationsRepository>();
            services.AddScoped<IPageRouteVersionRepository, PageRouteVersionRepository>();
            services.AddScoped<IPageRouteRepository, PageRouteRepository>();
            services.AddScoped<IMinistryVisionRepository, MinistryVisionRepository>();
            
            services.AddScoped<IHP_PhotosReopsitory, HP_PhotosReopsitory>();
            services.AddScoped<IHP_VideoReopsitory, HP_VideoReopsitory>();
            services.AddScoped<IHP_PhotoSliderReopsitory, HP_PhotoSliderReopsitory>();
            services.AddScoped<IHP_EconomicDevelopmentReopsitory, HP_EconomicDevelopmentReopsitory>();
            services.AddScoped<IPublicationRepository, PublicationRepository>();

            services.AddScoped<IHP_AffiliatesReopsitory, HP_AffiliatesReopsitory>();

            services.AddScoped<IMonitoringRepository, MonitoringRepository>();
            services.AddScoped<ICitizenPlanRepository, CitizenPlanRepository>();
            services.AddScoped<IImportFromExcel, ImportFromExcel>();
            services.AddScoped<IEconomicIndicatorRepository, EconomicIndicatorReopsitory>();
            services.AddScoped<IHP_BasicInfoReopsitory, HP_BasicInfoReopsitory>();
            services.AddScoped<IDFUnitRepository, DFUnitRepository>();
            services.AddScoped<IMyEmailSender, MyEmailSender>();

       

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
