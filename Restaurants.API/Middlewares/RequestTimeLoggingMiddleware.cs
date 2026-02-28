using System.Diagnostics;

namespace Restaurants.API.Middlewares;
public class RequestTimeLoggingMiddlewre(ILogger<RequestTimeLoggingMiddlewre> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        
            var timer = Stopwatch.StartNew();

            await next(context);

            timer.Stop();
            var elapsedMilliseconds = TimeSpan.FromSeconds(4);
            if (timer.Elapsed > elapsedMilliseconds)
            {
                       logger.LogWarning(
                       "Request {Method} {Path} took {Elapsed} ms",
                       context.Request.Method,
                       context.Request.Path,
                       elapsedMilliseconds);

            }

       




    }
}
