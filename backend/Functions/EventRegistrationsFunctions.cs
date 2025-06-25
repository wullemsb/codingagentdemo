#nullable enable

using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Backend.Functions;

public class EventRegistrationsFunctions
{
    private readonly ILogger<EventRegistrationsFunctions> _logger;
    private readonly EventsDbContext _context;

    public EventRegistrationsFunctions(ILogger<EventRegistrationsFunctions> logger, EventsDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [Function("RegisterForEvent")]
    public async Task<IActionResult> RegisterForEvent([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "events/{eventId:guid}/register")] HttpRequest req, Guid eventId)
    {
        try
        {
            _logger.LogInformation("Registering for event with ID: {EventId}", eventId);

            // Check if event exists
            var eventExists = await _context.Events.AnyAsync(e => e.Id == eventId);
            if (!eventExists)
            {
                return new NotFoundObjectResult("Event not found");
            }

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var registrationData = JsonSerializer.Deserialize<EventRegistration>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (registrationData == null || string.IsNullOrEmpty(registrationData.Name) || string.IsNullOrEmpty(registrationData.Email))
            {
                return new BadRequestObjectResult("Invalid registration data");
            }

            // Check if user already registered for this event
            var existingRegistration = await _context.EventRegistrations
                .FirstOrDefaultAsync(er => er.EventId == eventId && er.Email == registrationData.Email);
            
            if (existingRegistration != null)
            {
                return new ConflictObjectResult("User already registered for this event");
            }

            var newRegistration = new EventRegistration
            {
                Id = Guid.NewGuid(),
                EventId = eventId,
                Name = registrationData.Name,
                Email = registrationData.Email,
                Pronouns = registrationData.Pronouns,
                OptInForCommunication = registrationData.OptInForCommunication,
                CreatedAt = DateTime.UtcNow
            };

            _context.EventRegistrations.Add(newRegistration);
            await _context.SaveChangesAsync();

            var response = new CreatedResult($"/api/events/{eventId}/registrations/{newRegistration.Id}", newRegistration);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering for event with ID: {EventId}", eventId);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [Function("GetEventRegistrations")]
    public async Task<IActionResult> GetEventRegistrations([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "events/{eventId:guid}/registrations")] HttpRequest req, Guid eventId)
    {
        try
        {
            _logger.LogInformation("Getting registrations for event with ID: {EventId}", eventId);

            // Check if event exists
            var eventExists = await _context.Events.AnyAsync(e => e.Id == eventId);
            if (!eventExists)
            {
                return new NotFoundObjectResult("Event not found");
            }

            var registrations = await _context.EventRegistrations
                .Where(er => er.EventId == eventId)
                .OrderBy(er => er.CreatedAt)
                .ToListAsync();

            var response = new OkObjectResult(registrations);
            response.ContentTypes.Add("application/json");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting registrations for event with ID: {EventId}", eventId);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}