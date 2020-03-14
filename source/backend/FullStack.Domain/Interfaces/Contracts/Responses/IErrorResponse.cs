using System.Collections.Generic;
using FullStack.Domain.Enumerations;

namespace FullStack.Domain.Interfaces.Contracts.Responses
{
    public interface IErrorResponse : IBaseResponse
    {
        #region IErrorResponse Members

        object Id { get; }
        string Message { get; }
        EnumErrorCodes Code { get; }
        IEnumerable<object> Errors { get; set; }

        #endregion
    }
}
