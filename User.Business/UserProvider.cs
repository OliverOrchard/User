using System;
using System.Threading.Tasks;
using User.Domain;
using User.Repository;
using User.Business;

namespace User.Business
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepository;

        public UserProvider(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<Domain.User> GetUser(Guid id)
        {
            return await _userRepository.GetByIdAsync(id.ToString());
        }

        public async Task<bool> UpdateUser(Guid id, string password, string email, string firstName, string surname)
        {
            var existingUser = await _userRepository.GetByIdAsync(id.ToString());
            if (existingUser == null)
            {
                return false;
            }

            existingUser.Update(password,email,firstName,surname);
            return await _userRepository.UpsertUser(existingUser);
        }

        public async Task<bool> CreateUser(string password, string email, string firstName, string surname)
        {
            var user = Domain.User.Create(Guid.NewGuid().ToString(), password, email, firstName, surname);
            return await _userRepository.UpsertUser(user);
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await GetUser(id);

            if (user == null)
            {
                return false;
            }

            return await _userRepository.DeleteUser(user);
        }
    }
}
