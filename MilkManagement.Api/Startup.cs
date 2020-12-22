using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MilkManagement.Core.Configuration;
using MilkManagement.Infrastructure.Data;

namespace MilkManagement.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add Core Layer
            services.AddConfiguration<ConnectionStringConfiguration>(Configuration, ConnectionStringsSection);
            // aspnetrun dependencies
            services.ConfigureMilkManagementServices();
            ConfigureDatabases(services);
            services.AddControllers().AddJsonOptions(options =>
                            {
                                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                                options.JsonSerializerOptions.WriteIndented = true;
                            }
            );
            services.ConfigureSwagger();
            services.AddCors(options => options.AddPolicy(AllCors, build => build.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));
        }
        private void ConfigureDatabases(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(c =>
            {
                c.UseSqlServer(Configuration.GetConnectionString(ConnectionStringName)
                                                                                , x => x.MigrationsAssembly("MilkManagement.Infrastructure"));
            });

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "";
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Milk Management Api V1");
                });

            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseCors(AllCors);
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region Section
        //	private const string CachePeriod = "CachePeriod";
        private const string AllCors = "All";
        private const string ConnectionStringsSection = "ConnectionStrings";
        private const string ConnectionStringName = "Default";

        #endregion
    }
}
