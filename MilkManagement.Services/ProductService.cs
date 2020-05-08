using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MilkManagement.Core;
using MilkManagement.Core.Models;
using MilkManagement.Core.Services;

namespace MilkManagement.Services
{
    public class ProductService:IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return  await _unitOfWork.Products.GetAllAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetProductById(Guid id)
        {
            return await _unitOfWork.Products
                .GetByIdAsync(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newProduct"></param>
        /// <returns></returns>
        public async Task<Product> CreateProduct(Product newProduct)
        {
            await _unitOfWork.Products.AddAsync(newProduct);
             await _unitOfWork.CommitAsync();
             return newProduct;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productToBeUpdated"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task UpdateProduct(Product productToBeUpdated, Product product)
        {
            productToBeUpdated.Name = product.Name;
            productToBeUpdated.Active = product.Active;
            await _unitOfWork.CommitAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task DeleteProduct(Product product)
        {
            _unitOfWork.Products.Remove(product);
            await _unitOfWork.CommitAsync();
        }
    }
}
