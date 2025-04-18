using System;
using System.Net;
using Azure;
using Azure.Core;
using Domain.Exceptions;
using Shared.ErrorModels;

namespace E_Commerce.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        public RequestDelegate _next { get; }
        public ILogger<GlobalErrorHandlingMiddleware> _logger { get; }

        public GlobalErrorHandlingMiddleware(RequestDelegate next,ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if(httpContext.Response.StatusCode== (int)HttpStatusCode.NotFound)
                    await HandleNotFoundPointAsync(httpContext);

            }
            catch (Exception exception) {
                _logger.LogError($"SomeThing went Wrong {exception}");
                await HandleExceptionAsync(httpContext, exception);
            }
        }
        public async Task HandleNotFoundPointAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                ErrorMessage = $"The End Point Not Fount:{httpContext.Request.Path}"
            }.ToString();
            await httpContext.Response.WriteAsync(response);

        }
        public async Task HandleExceptionAsync(HttpContext httpContext,Exception exception)
        {
           
            //httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                ErrorMessage = exception.Message
            };
            httpContext.Response.StatusCode = exception switch
            {
                UnauthorizedException=> (int)HttpStatusCode.Unauthorized,
                NotFoundException => (int)HttpStatusCode.NotFound,
                RegisterValidationException ValidationException => HandleValidationException(ValidationException,response),
                _ => (int)HttpStatusCode.InternalServerError
            };
            response.StatusCode=httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsync(response.ToString());
        }

        private int HandleValidationException(RegisterValidationException validationException, ErrorDetails response)
        {
            response.Errors=validationException.Errors;
            return (int) HttpStatusCode.BadRequest;
        }
    }
}
