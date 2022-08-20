using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace webapi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HealthCheckController : ControllerBase
{
    private readonly ILogger<HealthCheckController> logger;

    public HealthCheckController(ILogger<HealthCheckController> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    ///     Health check endpoint
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <response code="200">Service is available</response>
    /// <response code="503">Service is not available</response>
    [HttpHead]
    public ActionResult Get()
    {
        logger.LogInformation("Health check called");

        return Ok();
    }
}