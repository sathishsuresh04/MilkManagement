using System;
using System.Linq;
using System.Threading.Tasks;
using MilkManagement.Core.Entities;
using MilkManagement.Core.Repositories;
using MilkManagement.Core.Specifications;
using MilkManagement.Infrastructure.Data;
using MilkManagement.Infrastructure.Repository.Base;

namespace MilkManagement.Infrastructure.Repository
{
   public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> GetCategoryWithProductsAsync(Guid categoryId)
        {
            var spec = new CategoryWithProductsSpecification(categoryId);
            return (await GetAsync(spec)).FirstOrDefault();
        }
    }
}
