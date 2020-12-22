using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MilkManagement.Core.Interfaces;
using MilkManagement.Core.Repositories;
using MilkManagement.Core.Repositories.Base;
using MilkManagement.Core.Services;
using MilkManagement.Infrastructure.Data;
using MilkManagement.Infrastructure.Logging;
using MilkManagement.Infrastructure.Repository;
using MilkManagement.Infrastructure.Repository.Base;
using MilkManagement.Services.Services;
using MilkManagement.Api.HealthCheck;
using System.Reflection;
using System.IO;
using System;
using Microsoft.Extensions.Options;
namespace MilkManagement.Api
{
	public static class ConfigurationExtension
	{
		/// <summary>
		/// Extension to confgure settings to strongly typed object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <param name="configurationTag"></param>
		public static void AddConfiguration<T>(
				this IServiceCollection services,
				IConfiguration configuration,
				string configurationTag = null)
				where T : class
		{
			if (string.IsNullOrEmpty(configurationTag))
			{
				configurationTag = typeof(T).Name;
			}

			var instance = Activator.CreateInstance<T>();
			new ConfigureFromConfigurationOptions<T>(configuration.GetSection(configurationTag)).Configure(instance);
			services.AddSingleton(instance);
		}
		/// <summary>
		/// Configure Swagger open api
		/// </summary>
		/// <param name="services"></param>
		public static void ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1",
					new OpenApiInfo
					{
						Title = "Milk Management Api",
						Version = "v1",
						Description = "Milk management Api describes the endpoints can be consumed."

					}
					);
				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				options.IncludeXmlComments(xmlPath);
			});
		}
		/// <summary>
		/// Configure all the required services
		/// </summary>
		/// <param name="services"></param>
		public static void ConfigureMilkManagementServices(this IServiceCollection services)
		{
			// Add Infrastructure Layer
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
	}
}
