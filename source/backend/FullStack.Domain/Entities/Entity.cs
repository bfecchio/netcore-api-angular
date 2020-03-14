using System;
using System.Runtime.Serialization;
using FullStack.Domain.Interfaces.Entities;

namespace FullStack.Domain.Entities
{
    [DataContract]
    [Serializable]
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        #region Public Properties

        [DataMember]
        public TKey Id { get; set; }

        #endregion

        #region Public Methods

        public virtual object Clone() => MemberwiseClone();

        #endregion
    }
}
