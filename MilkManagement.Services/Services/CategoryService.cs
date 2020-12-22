using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MilkManagement.Core.Entities;
using MilkManagement.Core.Interfaces;
using MilkManagement.Core.Repositories;
using MilkManagement.Core.Services;

namespace MilkManagement.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAppLogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, IAppLogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            _logger.LogInformation("Get information of product");
            return await _categoryRepository.GetAllAsync();
        }
    }
}
