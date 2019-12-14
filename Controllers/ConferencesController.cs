using Microsoft.AspNetCore.Mvc;
using api_with_asp.net.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace api_with_asp.net.Controllers {
    [Route("api/[controller]")]
    public class ConferencesController : ControllerBase {
        private readonly AppDbContext _context;

        public ConferencesController(AppDbContext context) {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Conference>> PostConference(Conference conference) {
            _context.Conferences.Add(conference);
            await _context.SaveChangesAsync();

            // return CreatedAtAction(nameof(GetConference), new { id = conference.Id }, conference );
            return this.StatusCode(StatusCodes.Status201Created, "Conference Registered");
        }

        [HttpGet]
        public async Task<ActionResult<Conference>> GetConferences() {
            var conferences = await _context.Conferences.ToListAsync();

            if (conferences == null) {
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

    }
}