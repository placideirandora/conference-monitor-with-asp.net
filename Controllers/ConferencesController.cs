using Microsoft.AspNetCore.Mvc;

namespace api_with_asp.net.Controllers {
    [Route("api/[controller]")]
    public class ConferencesController : ControllerBase {
        public object Get() {
            return new { Id = 1, ConferenceName = "DefCon 2020" };
        }

    }
}