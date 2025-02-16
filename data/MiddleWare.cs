namespace step_buy_server.data;

public class ApiRouteLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiRouteLoggingMiddleware> _logger;

    public ApiRouteLoggingMiddleware(RequestDelegate next, ILogger<ApiRouteLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"API Route: {context.Request.Method} {context.Request.Path}");
        await _next(context);
    }
}

public static class ApiRouteLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseApiRouteLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiRouteLoggingMiddleware>();
    }
}