namespace ChatHistory.ChatHistoryAPI.Authentication;

public class ApiKeyAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeader, out var apiKeyHeader))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key is missing.");
            return;
        }
        var apiKey = _configuration.GetValue<string>(AuthConstants.Authentication);
        if (apiKey is not null && !apiKey.Equals(apiKeyHeader, StringComparison.InvariantCultureIgnoreCase))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key is invalid.");
            return;
        }
        await _next(context);
    }
}