using System.Collections.Generic;
using FullStack.Domain.Interfaces.Contracts.Schemas;

namespace FullStack.Domain.Interfaces.Contracts.Responses
{
    public interface IPaginatedResponse<TSchema> : IBaseResponse
        where TSchema : class, ISchema
    {
        #region IPaginatedResponse Members

        int Length { get; }
        int PageIndex { get; }
        int PageSize { get; }
        IEnumerable<TSchema> Data { get; set; }

        #endregion
    }
}
