using Microsoft.AspNetCore.Mvc;
using ConferenceMonitorApi.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ConferenceMonitorApi.Data;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Collections.Generic;

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

        /// <summary>
        /// Publish a Conference (protected)
        /// </summary>

        // Handle protected POST request of a conference
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<Conference>> PublishConference([FromBody] Conference conference)
        {
            if (ModelState.IsValid)
            {   
                Claim idClaim = User.Claims.FirstOrDefault(claim => claim.Type.Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                Claim emailClaim = User.Claims.FirstOrDefault(claim => claim.Type.Equals("UserEmail", StringComparison.InvariantCultureIgnoreCase));

                    conference.PublisherID = Convert.ToInt32(idClaim.Value);
                    conference.PublisherEmail = emailClaim.Value;

                    await _repository.CreateAsync<Conference>(conference);

                    return CreatedAtAction(nameof(GetSingleConference), new { id = conference.Id }, conference);
                
            }
            else
            {
                return BadRequest(new { message = "Validation Errors", ModelState });
            }
        }

        /// <summary>
        /// Retrieve All Conferences (public)
        /// </summary>

        // Handle GET request of all Conference
        [HttpGet]
        public async Task<ActionResult<Conference>> GetAllConferences()
        {
            List<Conference> Conferences = await _repository.FindAllAsync<Conference>();

            if (!Conferences.Any())
            {
                return NotFound(new { message = "No conferences found at the moment" });
            }

            return Ok(new { message = "Conferences found", Conferences });
        }

        /// <summary>
        /// Retrieve a Specific Conference (public)
        /// </summary>

        // Handle GET request of a specific conference
        [HttpGet("{id}")]
        public async Task<ActionResult<Conference>> GetSingleConference(int id)
        {
            Conference conference = await _repository.FindByIdAsync<Conference>(id);

            if (conference == null)
            {
                return NotFound(new { message = "Conference not found" });
            }

            return Ok(new { message = "Conference found", conference });
        }

        /// <summary>
        /// Delete a Specific Conference (protected)
        /// </summary>

        // Handle protected DELETE request of a specific conference
        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Conference>> DeleteConference(int id)
        {
            Conference conference = await _repository.FindByIdAsync<Conference>(id);

            if (conference == null) return NotFound(new { message = "Conference not found" });

            Claim idClaim = User.Claims.FirstOrDefault(claim => claim.Type.Equals("UserID", StringComparison.InvariantCultureIgnoreCase));

            if (Convert.ToInt32(idClaim.Value) != conference.PublisherID) return Unauthorized(new { message = $"You can only delete conferences that you published. This one ({conference.Name}) was published by {conference.PublisherEmail}" });

            await _repository.DeleteAsync<Conference>(conference);

            return Ok(new { message = $"Conference ({conference.Name}) deleted"});
        }

        /// <summary>
        /// Update a Specific Conference (protected)
        /// </summary>

        // Handle protected PUT request of updating a specific conference
        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Conference>> UpdateConference(int id, [FromBody] Conference conference)
        {
            if (id != conference.Id) return BadRequest(new { message = "IDs do not match", parameterID = id, conferenceID = conference.Id });

            Claim idClaim = User.Claims.FirstOrDefault(claim => claim.Type.Equals("UserID", StringComparison.InvariantCultureIgnoreCase));

            if (Convert.ToInt32(idClaim.Value) != conference.PublisherID) return Unauthorized(new { message = $"You can only update conferences that you published. This one ({conference.Name}) was published by {conference.PublisherEmail}" });

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