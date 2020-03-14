using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using FullStack.Domain.Enumerations;
using FullStack.Domain.Interfaces.Contracts.Responses;

namespace FullStack.Domain.Contracts.Responses
{
    public sealed class ErrorResponse : IErrorResponse
    {
        #region IErrorResponse Members

        [JsonProperty("id")]
        public object Id { get; }
        [JsonProperty("message")]
        public string Message { get; }
        [JsonProperty("code")]
        public EnumErrorCodes Code { get; }
        [JsonProperty("errors")]
        public IEnumerable<object> Errors { get; set; }

        #endregion

        #region Constructors

        public ErrorResponse(string message, EnumErrorCodes code)
            : this(id: null, message, code, errors: null)
        { }

        public ErrorResponse(object id, string message, EnumErrorCodes code)
            : this(id, message, code, errors: null)
        { }

        public ErrorResponse(object id, string message, EnumErrorCodes code, IEnumerable<string> errors)
        {
            Code = code;
            Message = message;
            Id = id ?? Guid.NewGuid();
            Errors = errors ?? new List<string>();
        }

        #endregion

        #region Public Static Methods

        public static ErrorResponse DefaultUnauthorizedResponse()
            => new ErrorResponse("O recurso solicitado requer autenticação.", EnumErrorCodes.Generic);

        public static ErrorResponse DefaultForbiddenResponse()
            => new ErrorResponse("O servidor recusou a solicitação.", EnumErrorCodes.Generic);

        public static ErrorResponse DefaultNotFoundResponse()
            => new ErrorResponse("O recurso solicitado não foi localizado no servidor.", EnumErrorCodes.Generic);

        public static ErrorResponse DefaultBadRequestResponse()
            => new ErrorResponse("O servidor não conseguiu entender a solicitação.", EnumErrorCodes.Generic);

        public static ErrorResponse DefaultMethodNotAllowedResponse()
            => new ErrorResponse("O método da solicitação não é permitido para o recurso solicitado.", EnumErrorCodes.Generic);

        public static ErrorResponse DefaultRequestTimeoutResponse()
            => new ErrorResponse("O servidor atingiu o tempo limite da solicitação.", EnumErrorCodes.Generic);

        public static ErrorResponse DefaultInternalServerErrorResponse()
            => new ErrorResponse("Ocorreu um erro genérico no servidor.", EnumErrorCodes.Generic);

        #endregion
    }
}
