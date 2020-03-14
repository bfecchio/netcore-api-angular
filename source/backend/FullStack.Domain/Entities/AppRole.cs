using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using FullStack.Domain.Interfaces.Entities;

namespace FullStack.Domain.Entities
{
    public class AppRole : IdentityRole, IBaseEntity
    {
        #region Navigation Properties

        public virtual ICollection<IdentityUserRole<string>> Users { get; } = new List<IdentityUserRole<string>>();

        #endregion

        #region Public Methods

        public object Clone() => MemberwiseClone();

        #endregion
    }
}
