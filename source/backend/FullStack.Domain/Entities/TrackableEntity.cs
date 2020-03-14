using System;
using System.Runtime.Serialization;
using FullStack.Domain.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace FullStack.Domain.Entities
{
    [DataContract]
    [Serializable]
    public abstract class TrackableEntity<TKey> : ITrackableEntity<TKey>
    {
        #region Public Properties

        [DataMember]
        public TKey Id { get; set; }

        [DataMember, Required]
        public string CreatedBy { get; set; }

        [DataMember, Required]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string ModifiedBy { get; set; }

        [DataMember]
        public DateTime? ModifiedDate { get; set; }

        #endregion

        #region Navigation Properties

        public virtual AppUser Creator { get; set; }
        public virtual AppUser Modifier { get; set; }

        #endregion

        #region Public Methods

        public virtual object Clone() => MemberwiseClone();

        #endregion
    }
}
