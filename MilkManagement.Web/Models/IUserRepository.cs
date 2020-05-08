using System.Threading.Tasks;

namespace MilkManagement.Web.Models
{
   public interface IUserRepository
    {
       Task<User> GetUser(int id);
    }
}
