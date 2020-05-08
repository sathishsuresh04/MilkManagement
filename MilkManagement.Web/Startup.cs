using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MilkManagement.Core;
using MilkManagement.Core.Configuration;
using MilkManagement.Core.Services;
using MilkManagement.Data;
using MilkManagement.Services;

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
                
                //.AddNewtonsoftJson(options =>
                //{
                //    options.SerializerSettings.ContractResolver =
                //        new CamelCasePropertyNamesContractResolver();
                //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //}
          
            );
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                //for migration
                // dotnet ef --startup-project MilkManagement.Web/MilkManagement.Web.csproj migrations add InitialModel -p MilkManagement.Data/MilkManagement.Data.csproj
                //dotnet ef --startup-project MilkManagement.Web/MilkManagement.Web.csproj database update
                //seed:dotnet ef --startup-project MilkManagement.Web/MilkManagement.Web.csproj migrations add SeedCoreTables -p MilkManagement.Data/MilkManagement.Data.csproj
                options.UseSqlServer(Configuration.GetConnectionString("Default"),
                    x => x.MigrationsAssembly("MilkManagement.Data"));
                //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.AddTransient<IProductService, ProductService>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Music", Version = "v1" });
            });
        }

        private void ConfigureMilkManagementServices(IServiceCollection services)
        {

            // Add Core Layer
            services.AddConfiguration<ConnectionStringConfiguration>(Configuration, ConnectionStringsSection);
            // Add Infrastructure Layer
            ConfigureDatabases(services);

        }

        /// <summary>
        /// Configure database
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureDatabases(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString(ConnectionStringName),
                    x => x.MigrationsAssembly("MilkManagement.Data")));
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

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Music V1");
            });
            //app.Run(async(context)=>await WriteContent(context));
        }

        #region Section
         private const string ConnectionStringsSection = "ConnectionStrings";
         private const string ConnectionStringName = "Default";
         private const string CachePeriod = "CachePeriod";

         #endregion
    }
}
