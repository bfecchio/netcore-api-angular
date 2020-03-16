using System;
using System.Runtime.Serialization;

namespace FullStack.Domain.Entities
{
    [DataContract]
    [Serializable]
    public class Airline : Entity<int>
    {
        #region Public Properties

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string ICAO { get; set; }
        [DataMember]
        public string IATA { get; set; }
        [DataMember]
        public string Image { get; set; }
        [DataMember]
        public string Callsign { get; set; }

        #endregion
    }
}
