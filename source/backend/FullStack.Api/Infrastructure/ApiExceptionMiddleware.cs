using System;
using System.Net;
using System.Linq;
using FluentValidation;
using System.Threading.Tasks;
using FullStack.Domain.Helpers;
using Microsoft.AspNetCore.Http;
using FullStack.Core.Extensions;
using Microsoft.Extensions.Logging;
using FullStack.Domain.Contracts.Responses;

namespace FullStack.Api.Infrastructure
{
    public class ApiExceptionMiddleware
    {
        #region Private Read-Only Fields

        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        #endregion

        #region Constructors

        public ApiExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory.CreateLogger<ApiExceptionMiddleware>();
        }

        #endregion

        #region Public Methods

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        #endregion

        #region Private Methods

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var response = default(ErrorResponse);
            httpContext.Response.ContentType = "application/json";

            if (exception is ArgumentNullException || exception is ArgumentException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = ErrorResponse.DefaultBadRequestResponse();
            }
            else if (exception is ValidationException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                response = ErrorResponse.DefaultBadRequestResponse();
                response.Errors = (exception as ValidationException).Errors.Select(x => x.ErrorMessage);
            }
            else
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response = ErrorResponse.DefaultInternalServerErrorResponse();
            }

            if (!response.Errors.Any() && exception.GetInnerExceptions().Any())
                response.Errors = exception.GetInnerExceptions().Select(x => x.Message).ToList();

            _logger.LogError(exception, response.Message);
            return httpContext.Response.WriteAsync(SerializationHelper.SerializeToJson(response));
        }

        #endregion
    }
}
