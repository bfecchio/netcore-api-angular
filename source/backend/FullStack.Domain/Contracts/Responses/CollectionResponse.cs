using Newtonsoft.Json;
using System.Collections.Generic;
using FullStack.Domain.Interfaces.Contracts.Schemas;
using FullStack.Domain.Interfaces.Contracts.Responses;

namespace FullStack.Domain.Contracts.Responses
{
    public abstract class CollectionResponse<TSchema> : ICollectionResponse<TSchema>
        where TSchema : class, ISchema
    {
        #region Public Properties

        [JsonProperty("data")]
        public IEnumerable<TSchema> Data { get; set; }

        #endregion

        #region Constructors

        public CollectionResponse()
            : this(data: null)
        { }

        public CollectionResponse(IEnumerable<TSchema> data)
        {
            Data = data ?? new List<TSchema>();
        }

        #endregion
    }
}
