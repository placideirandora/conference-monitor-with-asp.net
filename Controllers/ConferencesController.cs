using Microsoft.AspNetCore.Mvc;
using api_with_asp.net.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
            return this.StatusCode(StatusCodes.Status201Created, "Conference Registered!");
        }

        [HttpGet]
        public IActionResult GetConferences() {
            return Ok(new { Id = 1, ConferenceName = "DefCon 2020" });
        }

    }
}