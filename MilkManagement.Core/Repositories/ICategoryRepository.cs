using System;
using System.Threading.Tasks;
using MilkManagement.Core.Entities;
using MilkManagement.Core.Repositories.Base;

namespace MilkManagement.Core.Repositories
{
   public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetCategoryWithProductsAsync(Guid categoryId);
    }
}
