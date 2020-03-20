using FullStack.Domain.Contracts.Requests;
using System;

namespace FullStack.Api.Contracts.Requests
{
    public sealed class TicketListRequest : PaginatedRequest
    {
        #region Public Properties

        public int? AirlineId { get; set; }
        public int? OriginId { get; set; }
        public int? DestinationId { get; set; }
        public DateTime? Scheduled { get; set; }

        #endregion
    }
}
