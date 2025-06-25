#nullable enable

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Helpers;

public static class CorsHelper
{
    public static void AddCorsHeaders(HttpRequest request)
    {
        request.HttpContext.Response.Headers["Access-Control-Allow-Origin"] = "http://localhost:4200";
        request.HttpContext.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";
        request.HttpContext.Response.Headers["Access-Control-Allow-Headers"] = "Content-Type, Authorization";
        request.HttpContext.Response.Headers["Access-Control-Allow-Credentials"] = "true";
    }

    public static IActionResult CreateOkResponseWithCors<T>(T data, HttpRequest request)
    {
        AddCorsHeaders(request);
        var response = new OkObjectResult(data);
        response.ContentTypes.Add("application/json");
        return response;
    }

    public static IActionResult CreateCreatedResponseWithCors<T>(string location, T data, HttpRequest request)
    {
        AddCorsHeaders(request);
        return new CreatedResult(location, data);
    }

    public static IActionResult CreateNoContentResponseWithCors(HttpRequest request)
    {
        AddCorsHeaders(request);
        return new NoContentResult();
    }

    public static IActionResult CreateNotFoundResponseWithCors(HttpRequest request, string? message = null)
    {
        AddCorsHeaders(request);
        return message == null ? new NotFoundResult() : new NotFoundObjectResult(message);
    }

    public static IActionResult CreateBadRequestResponseWithCors(HttpRequest request, string message)
    {
        AddCorsHeaders(request);
        return new BadRequestObjectResult(message);
    }

    public static IActionResult CreateConflictResponseWithCors(HttpRequest request, string message)
    {
        AddCorsHeaders(request);
        return new ConflictObjectResult(message);
    }

    public static IActionResult CreateErrorResponseWithCors(HttpRequest request)
    {
        AddCorsHeaders(request);
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
}