using Microsoft.AspNetCore.Mvc;
using ConferenceMonitorApi.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ConferenceMonitorApi.Data;
using CryptoHelper;

namespace ConferenceMonitorApi.Controllers
{
    // Base route
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;

        // Construct a field for accessing the repository
        public AuthController(IUserRepository repository)
        {
            _repository = repository;
        }

        // Handle POST request of user registration
        [HttpPost, Route("SignUp")]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Crypto.HashPassword(user.Password);
                user.ConfirmPassword = Crypto.HashPassword(user.ConfirmPassword);

                try {
                    await _repository.CreateAsync<User>(user);

                    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
                } catch (DbUpdateException) {
                    return this.StatusCode(StatusCodes.Status409Conflict, "User with Id " + user.Id + " already exists");
                }
            }
            else
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
        }

        // Return a registered user
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _repository.FindByIdAsync<User>(id);

            return this.StatusCode(StatusCodes.Status200OK, user);
        }
    }
}