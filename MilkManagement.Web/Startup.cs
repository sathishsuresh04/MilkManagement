using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MilkManagement.Core.Configuration;
using MilkManagement.Core.Interfaces;
using MilkManagement.Core.Repositories;
using MilkManagement.Core.Repositories.Base;
using MilkManagement.Core.Services;
using MilkManagement.Infrastructure.Data;
using MilkManagement.Infrastructure.Logging;
using MilkManagement.Infrastructure.Repository;
using MilkManagement.Infrastructure.Repository.Base;
using MilkManagement.Services.Services;
using MilkManagement.Web.HealthCheck;

namespace MilkManagement.Web
{
    public class Startup
    {
        //   private readonly IConfiguration _config;
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // aspnetrun dependencies
            ConfigureMilkManagementServices(services);

            services.AddControllers().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.WriteIndented = true;
                }
            );

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //    services.AddScoped<IUnitOfWork, UnitOfWork>();
            // services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //for migration
            // dotnet ef --startup-project MilkManagement.Web/MilkManagement.Web.csproj migrations add InitialModel -p MilkManagement.Data/MilkManagement.Data.csproj
            //dotnet ef --startup-project MilkManagement.Web/MilkManagement.Web.csproj database update
            //seed:dotnet ef --startup-project MilkManagement.Web/MilkManagement.Web.csproj migrations add SeedCoreTables -p MilkManagement.Data/MilkManagement.Data.csproj
            //  options.UseSqlServer(Configuration.GetConnectionString("Default"),
            //   x => x.MigrationsAssembly("MilkManagement.Data"));
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            // });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Music", Version = "v1" });
            });

         //   services.AddRazorPages();
        }

        private void ConfigureMilkManagementServices(IServiceCollection services)
        {

            // Add Core Layer
            services.AddConfiguration<ConnectionStringConfiguration>(Configuration, ConnectionStringsSection);
            // Add Infrastructure Layer
            ConfigureDatabases(services);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            // Add Service Layer
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            // Add Web Layer
            services.AddAutoMapper(typeof(Startup)); // Add AutoMapper

            // Add Miscellaneous
            services.AddHttpContextAccessor();
            services.AddHealthChecks()
                .AddCheck<IndexPageHealthCheck>("home_page_health_check");
        }

        /// <summary>
        /// Configure database
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureDatabases(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString(ConnectionStringName)//));
                    ,x => x.MigrationsAssembly("MilkManagement.Infrastructure")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            //  var cachePeriod = env.IsDevelopment() ? "600" : "604800";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(new DeveloperExceptionPageOptions
                {
                    //number of source code line to be displayed before and after exception line
                    SourceCodeLineCount = 2
                });
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseSwagger();
            //set default file option if you are not going to use default.html,default.htm, Index.html, index.htm 
            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("foo.html");

            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            //fileServerOptions.StaticFileOptions.OnPrepareResponse = obj => { obj.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}"); };
            //File server handles both use static files, use default files and directory browsing.
            //     app.UseFileServer(fileServerOptions);
            //app.UseDefaultFiles(defaultFilesOptions);

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={Configuration[CachePeriod]}");
                },

            });
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Music V1");
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});
        }

        #region Section
        private const string ConnectionStringsSection = "ConnectionStrings";
        private const string ConnectionStringName = "Default";
        private const string CachePeriod = "CachePeriod";

        #endregion
    }
}
