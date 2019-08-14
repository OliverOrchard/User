using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.Api.Models;
using User.Business;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserProvider _userProvider;

        public UserController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Response<UserRequestModel>), 200)]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userProvider.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            var response = new Response<UserRequestModel>()
            {
                Data = new UserRequestModel(user)
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserRequestModel user)
        {
            var success = await _userProvider.CreateUser(user.Password, user.Email, user.FirstName, user.Surname);

            if (!success)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]UserRequestModel user)
        {
            var success = await _userProvider.UpdateUser(id, user.Password,user.Email,user.FirstName,user.Surname);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _userProvider.DeleteUser(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
