using System;
using System.Runtime.Serialization;
using FullStack.Domain.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;

namespace FullStack.Domain.Entities
{
    [DataContract]
    [Serializable]
    public abstract class ManagedEntity<TKey> : IManagedEntity<TKey>
    {
        #region Public Properties

        [DataMember]
        public TKey Id { get; set; }

        [DataMember, Required]
        public string CreatedBy { get; set; }

        [DataMember, Required]
        public DateTime CreatedDate { get; set; }

        #endregion

        #region Navigation Properties

        public virtual AppUser Creator { get; set; }

        #endregion

        #region Public Methods

        public virtual object Clone() => MemberwiseClone();

        #endregion
    }
}
