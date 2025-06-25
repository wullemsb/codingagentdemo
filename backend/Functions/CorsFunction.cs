#nullable enable

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Backend.Functions;

public class CorsFunction
{
    private readonly ILogger<CorsFunction> _logger;

    public CorsFunction(ILogger<CorsFunction> logger)
    {
        _logger = logger;
    }

    [Function("CorsEvents")]
    public IActionResult CorsEvents([HttpTrigger(AuthorizationLevel.Anonymous, "options", Route = "events")] HttpRequest req)
    {
        _logger.LogInformation("CORS preflight request for events");
        return CreateCorsResponse();
    }

    [Function("CorsEventsById")]
    public IActionResult CorsEventsById([HttpTrigger(AuthorizationLevel.Anonymous, "options", Route = "events/{id}")] HttpRequest req)
    {
        _logger.LogInformation("CORS preflight request for events by ID");
        return CreateCorsResponse();
    }

    [Function("CorsEventRegistrations")]
    public IActionResult CorsEventRegistrations([HttpTrigger(AuthorizationLevel.Anonymous, "options", Route = "events/{eventId}/register")] HttpRequest req)
    {
        _logger.LogInformation("CORS preflight request for event registrations");
        return CreateCorsResponse();
    }

    [Function("CorsEventRegistrationsList")]
    public IActionResult CorsEventRegistrationsList([HttpTrigger(AuthorizationLevel.Anonymous, "options", Route = "events/{eventId}/registrations")] HttpRequest req)
    {
        _logger.LogInformation("CORS preflight request for event registrations list");
        return CreateCorsResponse();
    }

    private static IActionResult CreateCorsResponse()
    {
        var response = new OkResult();
        return response;
    }
}