﻿namespace FootballLeague.Middlewares
{
    using System.Net;

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new
            {
                Attention = "THIS IS JUST FOR TESTING PURPOSES, EXCEPTION SHOULD BE LOG IN DATABASE IF FUTURE VERSION!",
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware.",
                Exception = exception
            }
            .ToString());
        }
    }
}
