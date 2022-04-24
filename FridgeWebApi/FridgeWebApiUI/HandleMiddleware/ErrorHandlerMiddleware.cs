using System;
using System.Net;
using System.Threading.Tasks;
using FridgeWebApiBL.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FridgeWebApiUI.HandleMiddleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHostEnvironment environment;
        private readonly ILogger<ErrorHandlerMiddleware> logger;

        public ErrorHandlerMiddleware(RequestDelegate next, IHostEnvironment environment, ILogger<ErrorHandlerMiddleware> logger)
        {
            this.next = next;
            this.environment = environment;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                if (error is TaskCanceledException)
                {
                    logger.LogInformation(error.Message);
                }
                else
                {
                    logger.LogError($"Error Message {error.Message}, StackTrace {error.StackTrace}");
                }

                await ErrorHandler(context, error, environment);
            }
        }

        private static async Task ErrorHandler(HttpContext context, Exception error, IHostEnvironment environment)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = error switch
            {
                ElementByIdNotFoundException => (int)HttpStatusCode.NotFound,
                ElementOutOfRangeException
                    or ElementNullReferenceException
                    or ElementNotAssignException => (int)HttpStatusCode.BadRequest,
                //TaskCanceledException => BaseModels.HttpStatusCode.ClientClosedRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new
            {
                message = error.Message,
                details = environment.IsDevelopment() ? error.StackTrace : null,
            });

            await response.WriteAsync(result);
        }
    }
}

