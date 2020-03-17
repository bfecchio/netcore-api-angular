using FullStack.Domain.Contracts.Schemas;

namespace FullStack.Api.Contracts.Schemas
{
    public sealed class AirlineSchema : BaseSchema
    {
        #region Public Properties

        public int AirlineId { get; set; }
        public string Name { get; set; }
        public string ICAO { get; set; }
        public string IATA { get; set; }
        public string Image { get; set; }
        public string Callsign { get; set; }

        #endregion
    }
}
