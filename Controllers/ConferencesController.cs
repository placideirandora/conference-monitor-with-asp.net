using Microsoft.AspNetCore.Mvc;
using ConferenceMonitorApi.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ConferenceMonitorApi.Data;

namespace ConferenceMonitorApi.Controllers
{
    // Base route
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConferencesController : ControllerBase
    {
        private readonly IConferenceRepository _repository;

        // Construct a field for accessing the repository
        public ConferencesController(IConferenceRepository repository)
        {
            _repository = repository;
        }

        // Handle POST request of a conference
        [HttpPost]
        public async Task<ActionResult<Conference>> PostConference([FromBody] Conference conference)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateAsync<Conference>(conference);

                return CreatedAtAction(nameof(GetConference), new { id = conference.Id }, conference);
            }
            else
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
        }

        // Handle GET request of all conferences
        [HttpGet]
        public async Task<ActionResult<Conference>> GetConferences()
        {
            var conferences = await _repository.FindAllAsync<Conference>();

            if (!conferences.Any())
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "No Conferences Found At The Moment");
            }

            return Ok(conferences);
        }

        // Handle GET request of a specific conference
        [HttpGet("{id}")]
        public async Task<ActionResult<Conference>> GetConference(int id)
        {
            var conference = await _repository.FindByIdAsync<Conference>(id);

            if (conference == null)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, "Conference Not Found");
            }

            return Ok(conference);
        }

        // Handle DELETE request of a specific conference
        [HttpDelete("{id}")]
        public async Task<ActionResult<Conference>> DeleteConference(int id)
        {
            var conference = await _repository.FindByIdAsync<Conference>(id);

            if (conference == null) return this.StatusCode(StatusCodes.Status404NotFound, "Conference Not Found");

            await _repository.DeleteAsync<Conference>(conference);

            return this.StatusCode(StatusCodes.Status200OK, "Conference Deleted");
        }

        // Handle PUT request of updating a specific conference
        [HttpPut("{id}")]
        public async Task<ActionResult<Conference>> PutConference(int id, [FromBody] Conference conference)
        {
            if (id != conference.Id) return this.StatusCode(StatusCodes.Status400BadRequest, "Ids Do not Match");

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync<Conference>(id, conference);

                    return this.StatusCode(StatusCodes.Status200OK, "Conference Updated");
                }
                catch (DbUpdateConcurrencyException)
                {
                    return this.StatusCode(StatusCodes.Status404NotFound, "Conference Not Found");
                }
            }
            else
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

        }
    }
}