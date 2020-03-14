using Newtonsoft.Json;
using FullStack.Domain.Interfaces.Contracts.Requests;

namespace FullStack.Domain.Contracts.Requests
{
    public class PagingParams : IPagingParams
    {
        #region Public Properties

        /// <summary>
        /// Quantidade de itens por página
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Índice da página para exibição
        /// </summary>
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; } = 1;

        #endregion
    }
}
