using System.Net;
using System.Text.Json;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            var response = new ApiResponse
            {
                Success = false,
            };

            switch (exception)
            {
                case InvalidOperationException:
                    statusCode = HttpStatusCode.BadRequest;
                    response.Message = "Validation failed.";
                    response.Errors = [new ValidationErrorDetail() { Detail = exception.Message }];
                    break;
                case ValidationException:
                    statusCode = HttpStatusCode.BadRequest;
                    response.Message = "Validation failed.";
                    response.Errors = ((ValidationException)exception).Errors
                        .Select(error => (ValidationErrorDetail)error);
                    break;
                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    response.Message = "Resource not found.";
                    response.Errors = [new ValidationErrorDetail() { Detail = exception.Message }];
                    break;
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Forbidden;
                    response.Message = "Unauthorized access.";
                    response.Errors = [new ValidationErrorDetail() { Detail = exception.Message }];
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    response.Message = "An unexpected error occurred.";
                    response.Errors = [new ValidationErrorDetail() { Detail = exception.Message }];
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
    }
}
