using Microsoft.AspNetCore.Mvc;

namespace kvs.classic.api.Controllers
{
    [ApiController]

    // Standard REST route
    // Final URL: /api/workflows
    [Route("api/[controller]")]
    public class WorkflowController : ControllerBase
    {
        /// <summary>
        /// GET: api/workflows
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                message = "KVS Classic API Deployed by ECS",
                version = "2.0",
                status = "Running",
                timestampUtc = DateTime.UtcNow
            });
        }

        /// <summary>
        /// GET: api/workflows/{id}
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(new
            {
                workflowId = id,
                name = "Sample Workflow",
                status = "Active",
                createdOnUtc = DateTime.UtcNow.AddDays(-5)
            });
        }

        /// <summary>
        /// POST: api/workflows
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody] object workflow)
        {
            return Ok(new
            {
                message = "Workflow created successfully",
                data = workflow,
                createdOnUtc = DateTime.UtcNow
            });
        }
    }
}
