using Microsoft.EntityFrameworkCore;
using MilkManagement.Core.Models;
using MilkManagement.Core.Repositories;

namespace MilkManagement.Data.Repositories
{
   public class ProductRepository:Repository<Product>,IProductRepository
    {

        public ProductRepository(DbContext context) : base(context)
        {
        }

        private void Data()
        {
            var data = ApplicationDbContext.Products.ToListAsync();
        }
        private ApplicationDbContext ApplicationDbContext => Context as ApplicationDbContext;
    }
}
