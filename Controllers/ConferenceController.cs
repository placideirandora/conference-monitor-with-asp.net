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
    public class ConferenceController : ControllerBase
    {
        private readonly IConferenceRepository _repository;

        // Construct a field for accessing the repository
        public ConferenceController(IConferenceRepository repository)
        {
            _repository = repository;
        }

        // Handle protected POST request of a conference
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<Conference>> PostConference([FromBody] Conference conference)
        {
            if (ModelState.IsValid)
            {
                try {
                    await _repository.CreateAsync<Conference>(conference);

                    return CreatedAtAction(nameof(GetConference), new { id = conference.Id }, conference);
                } catch (DbUpdateException) {
                    return Conflict(new { message = $"Conference with Id {conference.Id} already exists" });
                }
            }
            else
            {
                return BadRequest(new { message = "Validation Errors", ModelState });
            }
        }

        // Handle GET request of all Conference
        [HttpGet]
        public async Task<ActionResult<Conference>> GetConference()
        {
            var Conferences = await _repository.FindAllAsync<Conference>();

            if (!Conferences.Any())
            {
                return NotFound(new { message = "No conferences found at the moment" });
            }

            return Ok(new { message = "Conferences found", Conferences });
        }

        // Handle GET request of a specific conference
        [HttpGet("{id}")]
        public async Task<ActionResult<Conference>> GetConference(int id)
        {
            var conference = await _repository.FindByIdAsync<Conference>(id);

            if (conference == null)
            {
                return NotFound(new { message = "Conference not found" });
            }

            return Ok(new { message = "Conference found", conference });
        }

        // Handle protected DELETE request of a specific conference
        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Conference>> DeleteConference(int id)
        {
            var conference = await _repository.FindByIdAsync<Conference>(id);

            if (conference == null) return NotFound(new { message = "Conference not found" });

            await _repository.DeleteAsync<Conference>(conference);

            return Ok(new { message = "Conference deleted"});
        }

        // Handle protected PUT request of updating a specific conference
        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Conference>> PutConference(int id, [FromBody] Conference conference)
        {
            if (id != conference.Id) return BadRequest(new { message = "IDs do not match", parameterID = id, conferenceID = conference.Id });

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync<Conference>(id, conference);

                    return Ok(new { message = "Conference updated", conference });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound(new { message = "Conference not found" });
                }
            }
            else
            {
                return BadRequest(new { message = "Validation Errors", ModelState });
            }

        }
    }
}