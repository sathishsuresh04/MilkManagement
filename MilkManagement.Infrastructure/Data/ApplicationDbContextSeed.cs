using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MilkManagement.Core.Entities;

namespace MilkManagement.Infrastructure.Data
{
   public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext applicationDbContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            if (retry != null)
            {
                var retryForAvailability = retry.Value;

                try
                {
                    await applicationDbContext.Database.MigrateAsync();
                    await applicationDbContext.Database.EnsureCreatedAsync();

                    if (!applicationDbContext.Categories.Any())
                    {
                        await applicationDbContext.Categories.AddRangeAsync(GetPreconfiguredCategories());
                        await applicationDbContext.SaveChangesAsync();
                    }

                    if (!applicationDbContext.Products.Any())
                    {
                        await applicationDbContext.Products.AddRangeAsync(GetPreconfiguredProducts());
                        await applicationDbContext.SaveChangesAsync();
                    }
                }
                catch (Exception exception)
                {
                    if (retryForAvailability >= 10) throw;
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<ApplicationDbContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(applicationDbContext, loggerFactory, retryForAvailability);
                    throw;
                }
            }
        }

        private static IEnumerable<Category> GetPreconfiguredCategories()
        {
            return new List<Category>
            {
                new Category { Name = "Phone"},
                new Category { Name = "TV"}
            };
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>
            {
                new Product { Name = "IPhone",Number ="11111", CategoryId =new Guid("8C7B7A90-1F09-4499-A637-08D7F387BD47")  , UnitPrice = 19.55M , UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 1, Active = true },
                new Product { Name = "Samsung",Number ="11112",  CategoryId = new Guid("8C7B7A90-1F09-4499-A637-08D7F387BD47") , UnitPrice = 33.56M, UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 1, Active = true },
                new Product { Name = "LG TV", Number ="11113", CategoryId = new Guid("E8A97EB7-94B3-41AD-A638-08D7F387BD47")  , UnitPrice = 33.5M, UnitsInStock = 10, QuantityPerUnit = "2", UnitsOnOrder = 1, ReorderLevel = 1, Active = true }
            };
        }
    }
}
