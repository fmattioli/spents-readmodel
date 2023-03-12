using Microsoft.AspNetCore.Http;
using Serilog;
using System.Text.Json;

namespace Spents.ReadModel.Crosscutting.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        public GlobalExceptionHandlingMiddleware(ILogger logger)
        {
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);

                var ErrorDetails = new
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "Internal Server error",
                    Detail = "An internal error ocurred",
                    ErrorMessage = e.Message
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(ErrorDetails));
            }
        }
    }
}
