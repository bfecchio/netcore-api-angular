using System;
using FullStack.Domain.Contracts.Schemas;

namespace FullStack.Api.Contracts.Schemas
{
    public sealed class TicketSchema : BaseSchema
    {
        #region Public Properties

        public int TicketId { get; set; }
        public AirlineSchema Airline { get; set; }
        public string Flight { get; set; }
        public string Gate { get; set; }
        public AirportSchema Origin { get; set; }
        public AirportSchema Destination { get; set; }
        public DateTime Scheduled { get; set; }
        public string Passenger { get; set; }

        #endregion
    }
}
