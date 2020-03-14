using System;
using System.Runtime.Serialization;
using FullStack.Domain.Interfaces.Entities;

namespace FullStack.Domain.Entities
{
    [DataContract]
    [Serializable]
    public abstract class BaseEntity : IBaseEntity
    {
        #region Public Methods

        public object Clone() => MemberwiseClone();

        #endregion
    }
}
