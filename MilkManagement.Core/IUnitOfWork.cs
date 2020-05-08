using System;
using System.Threading.Tasks;
using MilkManagement.Core.Repositories;

namespace MilkManagement.Core
{
  public interface IUnitOfWork:IDisposable
    {
        IProductRepository Products { get; }
        Task<int> CommitAsync();
    }
}
