using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MilkManagement.Core.Entities;
using MilkManagement.Core.Repositories;
using MilkManagement.Core.Specifications;
using MilkManagement.Infrastructure.Data;
using MilkManagement.Infrastructure.Repository.Base;

namespace MilkManagement.Infrastructure.Repository
{
  public  class ProductRepository:Repository<Product>,IProductRepository
    {
        public ProductRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            
        }

        public async Task<IEnumerable<Product>> GetProductListAsync()
        {
            var spec=new ProductWithCategorySpecification();
            return await GetAsync(spec);
            //second way
            // return await GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string productName)
        {
            var spec = new ProductWithCategorySpecification(productName);
            return await GetAsync(spec);
            //second way
            //return await GetAsync(x => x.Name.ToLower().Contains(productName.ToLower()));

            //third way
            //return await DbContext.Products
            //    .Where(x => x.Name.Contains(productName))
            //    .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(Guid categoryId)
        {
            return await DbContext.Products
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
