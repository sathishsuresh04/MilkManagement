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
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
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
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Milk Management Api", Version = "v1" });
			});
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
							, x => x.MigrationsAssembly("MilkManagement.Infrastructure")));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
		{
			if (env.IsDevelopment())
			{
				//app.UseDeveloperExceptionPage(new DeveloperExceptionPageOptions
				//{
				//	SourceCodeLineCount = 2
				//});
				app.UseExceptionHandler("/error-local-development");
			//	app.UseDatabaseErrorPage();
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
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		#region Section
		private const string ConnectionStringsSection = "ConnectionStrings";
		private const string ConnectionStringName = "Default";
		private const string CachePeriod = "CachePeriod";

		#endregion
	}
}
