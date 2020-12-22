using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MilkManagement.Core.Entities;
using MilkManagement.Core.Interfaces;
using MilkManagement.Core.Repositories;
using MilkManagement.Core.Services;

namespace MilkManagement.Services.Services
{
    public class ProductService : IProductService
    {
        // TODO : add validation , authorization, logging, exception handling etc. -- cross cutting activities in here.
        private readonly IProductRepository _productRepository;
        private readonly IAppLogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, IAppLogger<ProductService> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of products</returns>
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.GetAllAsync();
        }

        /// <summary>
        /// Get all products and its categories
        /// </summary>
        /// <returns>List of products and its categories</returns>
        public async Task<IEnumerable<Product>> GetProductsWithCategory()
        {
            return await _productRepository.GetProductListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<Product> GetProductById(Guid productId)
        {
            return await _productRepository.GetByIdAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetProductByName(string productName)
        {
            return await _productRepository.GetProductByNameAsync(productName);
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(Guid categoryId)
        {
            return await _productRepository.GetProductByCategoryAsync(categoryId);
        }

        public async Task<Product> Create(Product product)
        {
            await ValidateProductIfExist(product);
            var newproduct = await _productRepository.AddAsync(product);
            _logger.LogInformation("Entity successfully added - Product Service");
            return newproduct;
        }

        public async Task<IEnumerable<Product>> Create(ICollection<Product> products)
        {
            foreach (var product in products)
            {
                await ValidateProductIfExist(product);
            }
            return await _productRepository.AddRangeAsync(products);
        }

        public async Task Update(Product product)
        {
            ValidateProductIfNotExist(product);
            var editProduct = await _productRepository.GetByIdAsync(product.Id);
            if (editProduct == null)
                throw new ApplicationException("Entity could not be loaded.");
            await _productRepository.UpdateAsync(editProduct);
            _logger.LogInformation("Entity successfully updated - Productservice");
        }

        public async Task Delete(Product product)
        {
            ValidateProductIfNotExist(product);
            var deletedProduct = await _productRepository.GetByIdAsync(product.Id);
            if (deletedProduct == null)
                throw new ApplicationException("Entity could not be loaded.");

            await _productRepository.DeleteAsync(deletedProduct);
            _logger.LogInformation($"Entity successfully deleted - Productservice");
        }

        public async Task Delete(ICollection<Product> products)
        {
            foreach (var product in products)
            {
                ValidateProductIfNotExist(product);
            }
            await _productRepository.DeleteRangeAsync(products);
            _logger.LogInformation($"Entity successfully deleted - Productservice");
        }

        #region Private methods
        private async Task ValidateProductIfExist(Product product)
        {
            var existingEntity = await _productRepository.GetByIdAsync(product.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{product} with this id already exists");
        }
        private void ValidateProductIfNotExist(Product productModel)
        {
            var existingEntity = _productRepository.GetByIdAsync(productModel.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{productModel} with this id is not exists");
        }
        #endregion

    }
}
