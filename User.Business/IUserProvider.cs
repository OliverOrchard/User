using System.Threading.Tasks;
using User.Domain;

namespace User.Business
{
    public interface IUserProvider
    {
        Task<User.Domain.User> GetUser(string id);
        Task<bool> UpsertUser(Domain.User user);
        Task<bool> DeleteUser(string id);
    }
}