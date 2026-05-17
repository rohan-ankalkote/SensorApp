using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SensorApp
{
    public class UnhandledExceptionMiddleware(ILogger<UnhandledExceptionMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Internal Server Error");

                var problemDetails = new ProblemDetails()
                {
                    Title = "Error occured",
                    Detail = ex.Message,
                    Status = (int)HttpStatusCode.InternalServerError,
                };

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
