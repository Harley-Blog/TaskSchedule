using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BC.TS.Api.Controllers
{
    /// <summary>
    /// Health check
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {

        [HttpGet("health")]
        public async Task<string> HealthCheck()
        {
            return await Task.FromResult("Healthy");
        }
    }
}
