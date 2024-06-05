using ImdbWebApi.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ImdbWebApi.Middlewares
{
    public class CustomExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (CustomException ex)
            {
                _logger.LogError($"Error while processing the request, Message: ${ex.Message}, StackTrace: ${ex.StackTrace}");
                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsJsonAsync(new { Data = ex.Message });
            }
        }
    }
}
