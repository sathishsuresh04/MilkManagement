using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MilkManagement.Core.Entities;

namespace MilkManagement.Core.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsWithCategory();
        Task<Product> GetProductById(Guid productId);
        Task<IEnumerable<Product>> GetProductByName(string productName);
        Task<IEnumerable<Product>> GetProductByCategory(Guid categoryId);
        Task<Product> Create(Product productModel);
        Task<IEnumerable<Product>> Create(ICollection<Product> products);
        Task Update(Product productModel);
        Task Delete(Product productModel);
        Task Delete(ICollection<Product> products);
    }
}
