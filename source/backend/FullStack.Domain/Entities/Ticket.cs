using System;
using System.Runtime.Serialization;

namespace FullStack.Domain.Entities
{
    [DataContract]
    [Serializable]
    public class Ticket : ManagedEntity<int>
    {
        #region Public Properties

        [DataMember]
        public int AirlineId { get; set; }
        [DataMember]
        public string Flight { get; set; }
        [DataMember]
        public string Gate { get; set; }
        [DataMember]
        public int OriginId { get; set; }
        [DataMember]
        public int DestinationId { get; set; }
        [DataMember]
        public DateTime Scheduled { get; set; }
        [DataMember]
        public string Passenger { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Airline Airline { get; set; }
        public virtual Airport Origin { get; set; }
        public virtual Airport Destination { get; set; }

        #endregion
    }
}
