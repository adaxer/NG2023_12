using MovieBase.Api.Middleware;

namespace MovieBase.Api.Middleware
{

    public class RequestLogger
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLogger> _logger;

        public RequestLogger(RequestDelegate next, ILogger<RequestLogger> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Handling request: {context.Request.Method} {context.Request.Path}");
            await _next(context);
            _logger.LogInformation($"Finished handling request: {context.Request.Method} {context.Request.Path}");
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class RequestLoggerExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app) 
        {
            app.UseMiddleware<RequestLogger>();
            return app;
        }
    }
}
