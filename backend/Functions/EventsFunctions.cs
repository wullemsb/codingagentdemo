#nullable enable

using Backend.Data;
using Backend.Helpers;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Backend.Functions;

public class EventsFunctions
{
    private readonly ILogger<EventsFunctions> _logger;
    private readonly EventsDbContext _context;

    public EventsFunctions(ILogger<EventsFunctions> logger, EventsDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [Function("GetEvents")]
    public async Task<IActionResult> GetEvents([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "events")] HttpRequest req)
    {
        try
        {
            _logger.LogInformation("Getting all events");

            var dateFromParam = req.Query["dateFrom"].FirstOrDefault();
            var dateToParam = req.Query["dateTo"].FirstOrDefault();
            var locationParam = req.Query["location"].FirstOrDefault();

            var query = _context.Events.AsQueryable();

            if (DateTime.TryParse(dateFromParam, out var dateFrom))
            {
                query = query.Where(e => e.Date >= dateFrom);
            }

            if (DateTime.TryParse(dateToParam, out var dateTo))
            {
                query = query.Where(e => e.Date <= dateTo);
            }

            if (!string.IsNullOrEmpty(locationParam))
            {
                query = query.Where(e => e.Location.Contains(locationParam));
            }

            var events = await query.OrderBy(e => e.Date).ToListAsync();

            return CorsHelper.CreateOkResponseWithCors(events, req);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting events");
            return CorsHelper.CreateErrorResponseWithCors(req);
        }
    }

    [Function("GetEvent")]
    public async Task<IActionResult> GetEvent([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "events/{id:guid}")] HttpRequest req, Guid id)
    {
        try
        {
            _logger.LogInformation("Getting event with ID: {EventId}", id);

            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
            {
                return CorsHelper.CreateNotFoundResponseWithCors(req);
            }

            return CorsHelper.CreateOkResponseWithCors(eventItem, req);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting event with ID: {EventId}", id);
            return CorsHelper.CreateErrorResponseWithCors(req);
        }
    }

    [Function("CreateEvent")]
    public async Task<IActionResult> CreateEvent([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "events")] HttpRequest req)
    {
        try
        {
            _logger.LogInformation("Creating new event");

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var eventData = JsonSerializer.Deserialize<Event>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (eventData == null || string.IsNullOrEmpty(eventData.Name) || string.IsNullOrEmpty(eventData.Location))
            {
                return CorsHelper.CreateBadRequestResponseWithCors(req, "Invalid event data");
            }

            var newEvent = new Event
            {
                Id = Guid.NewGuid(),
                Name = eventData.Name,
                Location = eventData.Location,
                Date = eventData.Date,
                StartTime = eventData.StartTime,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return CorsHelper.CreateCreatedResponseWithCors($"/api/events/{newEvent.Id}", newEvent, req);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating event");
            return CorsHelper.CreateErrorResponseWithCors(req);
        }
    }

    [Function("UpdateEvent")]
    public async Task<IActionResult> UpdateEvent([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "events/{id:guid}")] HttpRequest req, Guid id)
    {
        try
        {
            _logger.LogInformation("Updating event with ID: {EventId}", id);

            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
            {
                return new NotFoundResult();
            }

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var eventData = JsonSerializer.Deserialize<Event>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (eventData == null || string.IsNullOrEmpty(eventData.Name) || string.IsNullOrEmpty(eventData.Location))
            {
                return new BadRequestObjectResult("Invalid event data");
            }

            existingEvent.Name = eventData.Name;
            existingEvent.Location = eventData.Location;
            existingEvent.Date = eventData.Date;
            existingEvent.StartTime = eventData.StartTime;
            existingEvent.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var response = new OkObjectResult(existingEvent);
            response.ContentTypes.Add("application/json");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating event with ID: {EventId}", id);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [Function("DeleteEvent")]
    public async Task<IActionResult> DeleteEvent([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "events/{id:guid}")] HttpRequest req, Guid id)
    {
        try
        {
            _logger.LogInformation("Deleting event with ID: {EventId}", id);

            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
            {
                return new NotFoundResult();
            }

            _context.Events.Remove(eventItem);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting event with ID: {EventId}", id);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}