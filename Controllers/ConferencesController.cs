using Microsoft.AspNetCore.Mvc;
using ConferenceMonitorApi.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConferenceMonitorApi.Controllers {
    [Route("api/v1/[controller]")]
    public class ConferencesController : ControllerBase {
        private readonly ConfDbContext _context;

        public ConferencesController(ConfDbContext context) {
            _context = context;
        }

        public ConferencesController()
        {
        }

        [HttpPost]
        public async Task<ActionResult<Conference>> PostConference([FromBody] Conference conference) {
            _context.Conferences.Add(conference);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConference), new { id = conference.Id }, conference );
        }

        [HttpGet]
        public async Task<ActionResult<Conference>> GetConferences() {
            var conferences = await _context.Conferences.ToListAsync();

            if (!conferences.Any()) {
                return this.StatusCode(StatusCodes.Status404NotFound, "No Conferences Found At The Moment");
            }

            return Ok(conferences);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Conference>> GetConference(int id) {
            var conference = await _context.Conferences.FindAsync(id);

            if (conference == null) {
                return this.StatusCode(StatusCodes.Status404NotFound, "Conference Not Found");
            }

            return Ok(conference);
        }

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