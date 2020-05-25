using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MilkManagement.Core.Entities;
using MilkManagement.Core.Repositories.Base;

namespace MilkManagement.Core.Repositories
{
  public  interface IProductRepository:IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductListAsync();
        Task<IEnumerable<Product>> GetProductByNameAsync(string productName);
        Task<IEnumerable<Product>> GetProductByCategoryAsync(Guid categoryId);

    }
}
