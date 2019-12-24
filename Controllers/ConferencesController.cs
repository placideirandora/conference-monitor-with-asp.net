using Microsoft.AspNetCore.Mvc;
using ConferenceMonitorApi.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConferenceMonitorApi.Controllers {
    // Base route
    [Route("api/v1/[controller]")]
    public class ConferencesController : ControllerBase {
        private readonly DatabaseContext _context;

        // Construct a field for accessing the database
        public ConferencesController(DatabaseContext context) {
            _context = context;
        }

        // Handle POST request of a conference
        [HttpPost]
        public async Task<ActionResult<Conference>> PostConference([FromBody] Conference conference) {
            _context.Conferences.Add(conference);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConference), new { id = conference.Id }, conference );
        }

        // Handle GET request of all conferences
        [HttpGet]
        public async Task<ActionResult<Conference>> GetConferences() {
            var conferences = await _context.Conferences.ToListAsync();

            if (!conferences.Any()) {
                return this.StatusCode(StatusCodes.Status404NotFound, "No Conferences Found At The Moment");
            }

            return Ok(conferences);
        }

        // Handle GET request of a specific conference
        [HttpGet("{id}")]
        public async Task<ActionResult<Conference>> GetConference(int id) {
            var conference = await _context.Conferences.FindAsync(id);

            if (conference == null) {
                return this.StatusCode(StatusCodes.Status404NotFound, "Conference Not Found");
            }

            return Ok(conference);
        }

        // Handle DELETE request of a specific conference
        [HttpDelete("{id}")]
        public async Task<ActionResult<Conference>> DeleteConference(int id) {
            var conference = await _context.Conferences.FindAsync(id);

            if (conference == null) {
                return this.StatusCode(StatusCodes.Status404NotFound, "Conference Not Found");
            }

            _context.Conferences.Remove(conference);
            await _context.SaveChangesAsync();

            return this.StatusCode(StatusCodes.Status200OK, "Conference Deleted");

        }

        // Handle PUT request of updating a specific conference
        [HttpPut("{id}")]
        public async Task<ActionResult<Conference>> PutConference(int id, [FromBody] Conference conference) {
            if (id != conference.Id){
                 return this.StatusCode(StatusCodes.Status400BadRequest, "Ids Do not Match");
            }

            _context.Entry(conference).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return this.StatusCode(StatusCodes.Status200OK, "Conference Updated");

        }
    }
}