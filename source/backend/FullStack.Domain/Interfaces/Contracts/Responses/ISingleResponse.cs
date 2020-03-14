using FullStack.Domain.Interfaces.Contracts.Schemas;

namespace FullStack.Domain.Interfaces.Contracts.Responses
{
    public interface ISingleResponse<TSchema> : IBaseResponse
        where TSchema : class, ISchema
    {
        #region ISingleResponse Members

        TSchema Data { get; set; }

        #endregion
    }
}
