using Application.Exceptions;
using AuthApi.BaseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthApi.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHostEnvironment environment;
        private readonly ILogger<ErrorMiddleware> logger;

        public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger, IHostEnvironment environment)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            var response = context.Response;
            response.ContentType = ContentTypeApplication.ApplicationJson;
            response.StatusCode = error switch
            {
                RestException exception => (int)exception.StatusCode,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var errorMessage = error is RestException restException ? restException.ErrorMessage : error.Message;
            logger.LogError(error, $"ErrorMessage: {errorMessage} StackTrace: {error?.StackTrace}");

            if (errorMessage is not null)
            {
                var result = JsonSerializer.Serialize(new
                {
                    message = errorMessage,
                    details = environment.IsDevelopment() ? error?.StackTrace : null,
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
