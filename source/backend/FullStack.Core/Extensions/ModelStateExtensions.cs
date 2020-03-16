using System.Linq;
using FullStack.Domain.Contracts.Responses;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FullStack.Core.Extensions
{
    public static class ModelStateExtensions
    {
        #region Extension Methods

        public static ErrorResponse ToErrorResponse(this ModelStateDictionary modelState)
        {
            var output = ErrorResponse.DefaultBadRequestResponse();

            output.Errors = modelState.SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();

            return output;
        }

        #endregion
    }
}
