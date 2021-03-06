﻿using FullStack.Domain.Contracts.Schemas;

namespace FullStack.Api.Contracts.Schemas
{
    public sealed class AirportSchema : BaseSchema
    {
        #region Public Properties

        public int AirportId { get; set; }

        public string Name { get; set; }
        
        public string City { get; set; }
        
        public string State { get; set; }
        
        public string ICAO { get; set; }
        
        public string IATA { get; set; }

        #endregion
    }
}
