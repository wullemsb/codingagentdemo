#nullable enable

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace Backend.Middleware;

public class CorsMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<CorsMiddleware> _logger;

    public CorsMiddleware(ILogger<CorsMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        await next(context);

        // Add CORS headers to response
        if (context.GetHttpResponseData() != null)
        {
            var response = context.GetHttpResponseData()!;
            response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
            response.Headers.Add("Access-Control-Allow-Credentials", "true");
        }
    }
}