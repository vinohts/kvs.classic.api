using Microsoft.AspNetCore.Mvc;

// Namespace must match project name
// Otherwise controller will not be discovered
namespace kvs.classic.api.Controllers
{
    // Marks this as an API controller
    // Enables automatic model binding & validation
    [ApiController]

    // Base route for this controller
    // Final URL: /api/workflows
    [Route("kvs/api/workflows")]
    public class WorkflowController : ControllerBase
    {
        // Handles HTTP GET requests
        // GET /api/workflows
        [HttpGet]
        public IActionResult Get()
        {
            // Simple response to confirm routing works
            return Ok("KVS Workflow API is working");
        }
    }
}
