using System;
using FullStack.Api.Contracts.Schemas;
using FullStack.Domain.Contracts.Requests;

namespace FullStack.Api.Contracts.Requests
{
    public sealed class TicketPostRequest : BaseRequest
    {
        #region Public Properties

        public DateTime Scheduled { get; set; }
        public string Passenger { get; set; }
        public AirlineSchema Airline { get; set; }
        public AirportSchema Origin { get; set; }
        public AirportSchema Destination { get; set; }
        public string Flight { get; set; }
        public string Gate { get; set; }

        #endregion
    }
}
