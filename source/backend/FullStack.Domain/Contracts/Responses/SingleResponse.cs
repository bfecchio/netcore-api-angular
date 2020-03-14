using Newtonsoft.Json;
using FullStack.Domain.Interfaces.Contracts.Schemas;
using FullStack.Domain.Interfaces.Contracts.Responses;

namespace FullStack.Domain.Contracts.Responses
{
    public abstract class SingleResponse<TSchema> : ISingleResponse<TSchema>
        where TSchema : class, ISchema
    {
        #region Public Properties

        [JsonProperty("data")]
        public TSchema Data { get; set; }

        #endregion

        #region Constructors

        public SingleResponse() { }
        public SingleResponse(TSchema schema) => Data = schema;

        #endregion
    }
}
