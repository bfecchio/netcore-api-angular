using System.Collections.Generic;
using FullStack.Domain.Interfaces.Contracts.Schemas;

namespace FullStack.Domain.Interfaces.Contracts.Responses
{
    public interface ICollectionResponse<TSchema> : IBaseResponse
        where TSchema : class, ISchema
    {
        #region ICollectionResponse Members

        IEnumerable<TSchema> Data { get; set; }

        #endregion
    }
}
