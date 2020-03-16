using FullStack.Domain.Interfaces.Contracts.Schemas;

namespace FullStack.Api.Contracts.Schemas
{
    public class AirportSchema : ISchema
    {
        #region Public Properties
        
        public string Name { get; set; }
        
        public string City { get; set; }
        
        public string State { get; set; }
        
        public string ICAO { get; set; }
        
        public string IATA { get; set; }

        #endregion
    }
}
