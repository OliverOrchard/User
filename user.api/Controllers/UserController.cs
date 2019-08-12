using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.Business;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserProvider _UserProvider { get; set; }

        public UserController(IUserProvider userProvider)
        {
            _UserProvider = userProvider;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Response<UserViewModel>), 200)]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _UserProvider.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            var response = new Response<UserViewModel>()
            {
                Data = new UserViewModel(user)
            };

            return Ok(response);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class UserViewModel
    {
        private object user;

        public UserViewModel(Domain.User user)
        {
            this.user = user;
        }
    }
}
