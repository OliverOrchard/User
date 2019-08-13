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

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]UserViewModel user)
        {
            var success = await _UserProvider.UpsertUser(Domain.User.Create(user.Id,user.Password,user.Email,user.FirstName,user.Surname));

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _UserProvider.DeleteUser(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
