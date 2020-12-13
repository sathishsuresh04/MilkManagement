using System.Threading.Tasks;

namespace MilkManagement.Api.Models
{
   public interface IUserRepository
    {
       Task<User> GetUser(int id);
    }
}
