using System;
using System.Threading.Tasks;
using MilkManagement.Core;
using MilkManagement.Core.Repositories;
using MilkManagement.Data.Repositories;

namespace MilkManagement.Data
{
  public  class UnitOfWork:IUnitOfWork
  {
      private readonly ApplicationDbContext _context;
      private ProductRepository _productRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
      public UnitOfWork(ApplicationDbContext context)
      {
          _context = context;
      }
        /// <summary>
        /// Dispose context
        /// </summary>
        public void Dispose()
        {
             _context.Dispose();
        }

        public IProductRepository Products => _productRepository ??= new ProductRepository(_context);
        public Task<int> CommitAsync()
        {
            throw new NotImplementedException();
        }
    }
}
