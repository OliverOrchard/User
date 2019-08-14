using System;
using System.Threading.Tasks;
using User.Domain;

namespace User.Business
{
    public interface IUserProvider
    {
        Task<User.Domain.User> GetUser(Guid id);
        Task<bool> UpdateUser(Guid id, string password, string email, string firstName, string surname);
        Task<bool> DeleteUser(Guid id);
        Task<bool> CreateUser(string password, string email, string firstName, string surname);
    }
}