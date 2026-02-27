
using Azure;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Timers;

namespace Restaurants.API.Middlewares;

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
