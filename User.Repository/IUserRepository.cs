using System.Threading.Tasks;
using User.Domain;

namespace User.Repository
{
    public interface IUserRepository
    {
        Task<Domain.User> GetByIdAsync(string id);
        Task<bool> UpsertUser(User.Domain.User user);
    }
}