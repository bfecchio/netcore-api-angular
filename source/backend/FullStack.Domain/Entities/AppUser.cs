using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;
using FullStack.Domain.Interfaces.Entities;

namespace FullStack.Domain.Entities
{
    [DataContract]
    [Serializable]
    public class AppUser : IdentityUser, IBaseEntity
    {
        #region Navigation Properties

        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();

        #endregion

        #region Public Methods

        public object Clone() => MemberwiseClone();

        #endregion
    }
}
