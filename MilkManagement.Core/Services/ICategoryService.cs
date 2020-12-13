using System.Collections.Generic;
using System.Threading.Tasks;
using MilkManagement.Core.Entities;

namespace MilkManagement.Core.Services
{
   public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoryList();
    }
}
