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

        public async Task<Domain.User> GetUser(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpsertUser(Domain.User user)
        {
            var existingUser = await _userRepository.GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                return await _userRepository.UpsertUser(user);
            }

            existingUser.Update(user);
            return await _userRepository.UpsertUser(existingUser);
        }

        public async Task<bool> DeleteUser(string id)
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
