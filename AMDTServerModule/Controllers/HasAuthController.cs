using AMDTServerModule.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMDTServerModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HasAuthController : ControllerBase
    {
        [Authorize]
        public async Task<ActionResult> Get()
        {
            return StatusCode(200);
        }
    }
}
