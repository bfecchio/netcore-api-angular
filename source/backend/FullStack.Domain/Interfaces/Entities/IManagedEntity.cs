using System;

namespace FullStack.Domain.Interfaces.Entities
{
    public interface IManagedEntity<TKey> : IBaseEntity
    {
        #region IManagedEntity Members

        TKey Id { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }

        #endregion
    }
}
