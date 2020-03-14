using Newtonsoft.Json;
using System.Collections.Generic;
using FullStack.Domain.Interfaces.Contracts.Schemas;
using FullStack.Domain.Interfaces.Contracts.Responses;

namespace FullStack.Domain.Contracts.Responses
{
    public abstract class PaginatedResponse<TSchema> : IPaginatedResponse<TSchema>
        where TSchema : class, ISchema
    {
        #region Public Properties

        [JsonProperty("length")]
        public int Length { get; }
        [JsonProperty("pageIndex")]
        public int PageIndex { get; }
        [JsonProperty("pageSize")]
        public int PageSize { get; }
        [JsonProperty("data")]
        public IEnumerable<TSchema> Data { get; set; }

        #endregion

        #region Constructors

        public PaginatedResponse(int pageIndex, int pageSize, int length)
            : this(pageIndex, pageSize, length, data: null)
        { }

        public PaginatedResponse(int pageIndex, int pageSize, int length, IEnumerable<TSchema> data)
        {
            Length = length;
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = data ?? new List<TSchema>();
        }

        #endregion
    }
}
