using System;
using FullStack.Domain.Contracts.Requests;

namespace FullStack.Api.Contracts.Requests
{
    public sealed class TicketPutRequest : BaseRequest
    {
        #region Public Properties

        public int AirlineId { get; set; }
        public string Flight { get; set; }
        public string Gate { get; set; }
        public DateTime Scheduled { get; set; }
        public string Passenger { get; set; }
        public int OriginId { get; set; }
        public int DestinationId { get; set; }

        #endregion
    }
}
