using Microsoft.AspNetCore.Mvc;

namespace api_with_asp.net.Controllers {
    [Route("api/[controller]")]
    public class ConferencesController : ControllerBase {
        [HttpGet]
        public IActionResult GetConferences() {
            return Ok(new { Id = 1, ConferenceName = "DefCon 2020" });
        }

    }
}