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
    }
}
