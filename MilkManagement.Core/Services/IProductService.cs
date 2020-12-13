using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MilkManagement.Core.Entities;

namespace MilkManagement.Core.Services
{
 public  interface IProductService
    {
        Task<IEnumerable<Product>> GetProductList();
        Task<Product> GetProductById(Guid productId);
        Task<IEnumerable<Product>> GetProductByName(string productName);
        Task<IEnumerable<Product>> GetProductByCategory(Guid categoryId);
        Task<Product> Create(Product productModel);
        Task<IEnumerable<Product>> Create(ICollection<Product> products);
        Task Update(Product productModel);
        Task Delete(Product productModel);
        Task Delete(ICollection<Product> products);

        //Task<IEnumerable<Product>> GetAllProducts();
        //Task<Product> GetProductById(Guid id);
        //Task<Product> CreateProduct(Product newProduct);
        //Task UpdateProduct(Product productToBeUpdated, Product product);
        //Task DeleteProduct(Product product);
    }
}
