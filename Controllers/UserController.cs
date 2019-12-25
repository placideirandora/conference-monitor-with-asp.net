using Microsoft.AspNetCore.Mvc;
using ConferenceMonitorApi.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ConferenceMonitorApi.Data;
using Microsoft.AspNetCore.Authorization;

namespace ConferenceMonitorApi.Controllers
{
    // Base route
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        // Construct a field for accessing the repository
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        // Handle POST request of a user
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
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

        // Handle GET request of all User
        [HttpGet]
        public async Task<ActionResult<User>> GetUsers()
        {
            var User = await _repository.FindAllAsync<User>();

            if (!User.Any())
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "No User Found At The Moment");
            }

            return this.StatusCode(StatusCodes.Status200OK, User);
        }

        // Handle GET request of a specific user
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _repository.FindByIdAsync<User>(id);

            if (user == null)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "User Not Found");
            }

            return this.StatusCode(StatusCodes.Status200OK, user);
        }

        // Handle DELETE request of a specific user
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _repository.FindByIdAsync<User>(id);

            if (user == null) return this.StatusCode(StatusCodes.Status404NotFound, "User Not Found");

            await _repository.DeleteAsync<User>(user);

            return this.StatusCode(StatusCodes.Status200OK, "User Deleted");
        }

        // Handle PUT request of updating a specific user
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> PutUser(int id, [FromBody] User user)
        {
            if (id != user.Id) return this.StatusCode(StatusCodes.Status400BadRequest, "Ids Do not Match");

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync<User>(id, user);

                    return this.StatusCode(StatusCodes.Status200OK, "User Updated");
                }
                catch (DbUpdateConcurrencyException)
                {
                    return this.StatusCode(StatusCodes.Status404NotFound, "User Not Found");
                }
            }
            else
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

        }
    }
}