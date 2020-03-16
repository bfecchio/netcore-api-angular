using System;
using System.Runtime.Serialization;

namespace FullStack.Domain.Entities
{
    [DataContract]
    [Serializable]
    public class Airport : Entity<int>
    {
        #region Public Properties

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string ICAO { get; set; }
        [DataMember]
        public string IATA { get; set; }        

        #endregion
    }
}
